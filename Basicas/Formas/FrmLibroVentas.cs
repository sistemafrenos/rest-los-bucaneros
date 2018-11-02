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
    public partial class FrmLibroVentas : Form
    {
        DatosEntities db = new DatosEntities();
        List<LibroVenta> Lista = new List<LibroVenta>();
        public FrmLibroVentas()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmLibroVentas_Load);
            this.FormClosed+=new FormClosedEventHandler(FrmLibroVentas_FormClosed);
        }
        void FrmLibroVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.LibroVentas = null;
        }
        void FrmLibroVentas_Load(object sender, EventArgs e)
        {
            #region EventosPantalla
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnCargar.Click += new EventHandler(btnBuscar_Click);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.btnCargarFacturas.Click += new EventHandler(btnCargarFacturas_Click);
            this.btnImprimirResumido.Click += new EventHandler(btnImprimirResumido_Click);
            #endregion
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
            Busqueda();
        }

        void btnImprimirResumido_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.LibroDeVentasResumido();
        }
        void btnCargarFacturas_Click(object sender, EventArgs e)
        {
            FactoryFacturas.PasarFacturasLibro(int.Parse(txtMes.Text), int.Parse(txtAño.Text));
            Busqueda();
        }
        #region ManejoDePantalla
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Return)
                {
                    EditarRegistro();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    EliminarRegistro();
                }
                if (e.KeyCode == Keys.Insert)
                {
                    AgregarRegistro();
                }
            }
        }
        void txtBuscar_Validating(object sender, CancelEventArgs e)
        {
            Busqueda();
        }
        void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }
        void btnNuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion
        private void Imprimir()
        {
            FrmReportes f = new FrmReportes();
            f.LibroDeVentas(Lista, string.Format("Mes {0} Año {1}",txtMes.Text,txtAño.Text));
            f = null;
        }
        private void Busqueda()
        {
            db = new DatosEntities();
            Lista = FactoryLibroVentas.getItems(db,Convert.ToInt16(txtMes.Text),Convert.ToInt16(txtAño.Text));
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void AgregarRegistro()
        {
            FrmLibroVentasItem F;
            do
            {
                F = new FrmLibroVentasItem();
                F.Incluir(txtMes.Text,txtAño.Text);
                if (F.DialogResult == DialogResult.OK)
                {
                    F.registro.Calcular();
                    F.registro.IdLibroVentas = FactoryContadores.GetMax("IdLibroVentas");
                    db.LibroVentas.AddObject(F.registro);
                    db.SaveChanges();
                    Busqueda();
                }
            } while (F.DialogResult != System.Windows.Forms.DialogResult.Cancel);
        }
        private void EditarRegistro()
        {
            FrmLibroVentasItem F = new FrmLibroVentasItem();
            LibroVenta registro = (LibroVenta)this.bs.Current;
            if (registro == null)
                return;
            F.registro = registro;
            F.registro.Mes = Convert.ToInt16(txtMes.Text);
            F.registro.Año = Convert.ToInt16(txtAño.Text);
            F.Modificar();
            if (F.DialogResult == DialogResult.OK)
            {
                F.registro.Calcular();
                db.SaveChanges();
                Busqueda();
            }
            else
            {
                db.Refresh(System.Data.Objects.RefreshMode.StoreWins, registro);
            }
        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                LibroVenta Registro = (LibroVenta)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    db.LibroVentas.DeleteObject(Registro);
                    db.SaveChanges();
                    Busqueda();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }

    }
}
