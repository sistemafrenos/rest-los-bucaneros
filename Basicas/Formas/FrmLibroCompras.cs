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
    public partial class FrmLibroCompras : Form
    {
        DatosEntities db = new DatosEntities();
        List<LibroCompra> Lista = new List<LibroCompra>();
        int Mes = 0;
        int Año = 0;
        public FrmLibroCompras()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmLibroCompras_Load);
            this.FormClosed += new FormClosedEventHandler(FrmLibroCompras_FormClosed);
        }

        void FrmLibroCompras_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.LibroCompras = null;
        }

        void FrmLibroCompras_Load(object sender, EventArgs e)
        {
            #region EventosPantalla
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnCargar.Click += new EventHandler(btnBuscar_Click);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.btnCargarCompras.Click += new EventHandler(btnCargarCompras_Click);
            #endregion
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
            Busqueda();
        }

        void btnCargarCompras_Click(object sender, EventArgs e)
        {
            Mes = Convert.ToInt16(txtMes.Text);
            Año = Convert.ToInt16(txtAño.Text);
            this.Cursor = Cursors.WaitCursor;
            object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod, "Actualizando libro Compras");
            this.Cursor = Cursors.Default;
            Busqueda();
        }
        private void WorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            e.Window.Message = "Registrando Compras";
            FactoryCompras.PasarComprasLibro();
            if (e.Arguments.Count > 0)
            {
                e.Result = e.Arguments[0].ToString();
            }
            else
            {
                e.Result = "Ok";
            }
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
            f.LibroDeCompras(Lista, string.Format("Mes {0} Año {1}",txtMes.Text,txtAño.Text));
            f = null;
        }
        private void Busqueda()
        {
            Mes = Convert.ToInt16(txtMes.Text);
            Año = Convert.ToInt16(txtAño.Text);
            db = new DatosEntities();
            Lista = FactoryLibroCompras.getItems(db,Mes,Año);
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void AgregarRegistro()
        {
            FrmLibroComprasItem F;
            do
            {
                F = new FrmLibroComprasItem();
                F.Incluir(txtMes.Text, txtAño.Text);
                if (F.DialogResult == DialogResult.OK)
                {
                    F.registro.Calcular();
                    F.registro.IdLibroCompras = FactoryContadores.GetMax("IdLibroCompras");
                    db.LibroCompras.AddObject(F.registro);
                    db.SaveChanges();
                    Busqueda();
                }
            } while (F.DialogResult != System.Windows.Forms.DialogResult.Cancel);
        }
        private void EditarRegistro()
        {
            FrmLibroComprasItem F = new FrmLibroComprasItem();
            LibroCompra registro = (LibroCompra)this.bs.Current;
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
                LibroCompra Registro = (LibroCompra)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    db.LibroCompras.DeleteObject(Registro);
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

