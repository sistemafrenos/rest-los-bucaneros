using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmPlatos : Form
    {
        public FrmPlatos()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPlatos_Load);
        }
        DatosEntities db = new DatosEntities();
        List<Plato> Lista = new List<Plato>();
        public string filtro;
        void FrmPlatos_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(Frm_FormClosed);
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnBuscar.Click += new EventHandler(btnBuscar_Click);
            this.txtBuscar.Validating += new CancelEventHandler(txtBuscar_Validating);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.btnRecetas.Click += new EventHandler(btnRecetas_Click);
            this.FormClosed += new FormClosedEventHandler(FrmPlatos_FormClosed);
            btnActualizarCostos.Click += new EventHandler(btnActualizarCostos_Click);
            Busqueda();
        }

        void btnActualizarCostos_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod, "Actualizando Costos");
            this.Cursor = Cursors.Default;
            Busqueda();
        }
        private void WorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            FactoryPlatos.ActualizarCostos();
            if (e.Arguments.Count > 0)
            {
                e.Result = e.Arguments[0].ToString();
            }
            else
            {
                e.Result = "Ok";
            }
        }
        void FrmPlatos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.PlatosLista = null;
        }
        void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoPlatos(Lista);
            f = null;
        }
        void btnRecetas_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoPlatosIngredientes();
            f = null;
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
        void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.PlatosLista = null;
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            db = new DatosEntities();
            Lista = FactoryPlatos.getItems(db,this.txtBuscar.Text);
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
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
        private void AgregarRegistro()
        {
            do
            {
                FrmPlatosItem F = new FrmPlatosItem();
                F.Incluir();
                if (F.DialogResult != DialogResult.OK)
                    return;
                 Busqueda();
            } while (true);
        }
        private void EditarRegistro()
        {
            FrmPlatosItem F = new FrmPlatosItem();
            Plato registro = (Plato)this.bs.Current;
            if (registro == null)
                return;
            F.registro = FactoryPlatos.ActualizarCosto( registro );
            F.Modificar();
            if (F.DialogResult == DialogResult.OK)
            {
                Busqueda();
            }

        }
        private void EliminarRegistro()
        {
            if (this.gridView1.IsFocusedView)
            {
                Plato Registro = (Plato)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    db.DeleteObject(Registro);
                    db.SaveChanges();
                    Busqueda();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        private void Nuevo_Click(object sender, EventArgs e)
        {
            AgregarRegistro();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }

    }
}
