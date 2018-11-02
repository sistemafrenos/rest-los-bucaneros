using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmPlatosAjustePrecios : Form
    {
        DatosEntities db = new DatosEntities();
        List<Plato> Lista = new List<Plato>();
        public FrmPlatosAjustePrecios()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPlatosAjustePrecios_Load);
            this.FormClosed += new FormClosedEventHandler(FrmPlatosAjustePrecios_FormClosed);
        }

        void FrmPlatosAjustePrecios_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.PlatosAjustePrecios = null;
        }

        void FrmPlatosAjustePrecios_Load(object sender, EventArgs e)
        {
            this.btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.btnAumento.Click += new EventHandler(btnAumento_Click);
            this.btnGuardarCambios.Click += new EventHandler(btnGuardarCambios_Click);
            this.txtPrecio.Validating += new CancelEventHandler(txtPrecio_Validating);
            this.txtPrecioConIva.Validating += new CancelEventHandler(txtPrecioConIva_Validating);            
        }

        void txtPrecioConIva_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            Plato plato = (Plato)this.bs.Current;
            plato.Precio = Basicas.Round((double)Editor.Value / (1 + (plato.TasaIva) / 100));
            double? Iva = plato.Precio * plato.TasaIva / 100;
            Editor.Value =(decimal)(plato.Precio + Iva);
        }
        void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            Plato plato = (Plato)this.bs.Current;
            plato.Precio = Basicas.Round((double)Editor.Value);
            double? Iva = plato.Precio * plato.TasaIva / 100;
            plato.PrecioConIva = Basicas.Round(plato.Precio + Iva);
        }
        void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                this.bs.EndEdit();
                db.SaveChanges();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        void btnAumento_Click(object sender, EventArgs e)
        {
            if (!Basicas.IsNumeric(toolPorcentaje.Text))
            {
                toolPorcentaje.Text = "0";
                return;
            }
            double Aumento = Convert.ToDouble(toolPorcentaje.Text);
            foreach (Plato p in db.Platos)
            {
                p.PrecioConIva = p.PrecioConIva + (p.PrecioConIva * Aumento / 100);
                p.Precio = Basicas.Round( p.PrecioConIva.GetValueOrDefault(0) / (1 + (p.TasaIva.GetValueOrDefault(0) / 100)));
            }
            this.gridControl1.RefreshDataSource();
        }
        void btnBuscar_Click(object sender, EventArgs e)
        {
            db = new DatosEntities();
            Lista = FactoryPlatos.getItems(db, this.txtBuscar.Text);
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
    }
}
