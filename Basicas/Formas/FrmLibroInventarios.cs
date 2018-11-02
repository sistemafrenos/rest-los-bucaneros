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
    public partial class FrmLibroInventarios : Form
    {
        DatosEntities db = new DatosEntities();
        List<LibroInventario> Lista = new List<LibroInventario>();
        int Mes = 0;
        int Año = 0;
        public FrmLibroInventarios()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmLibroInventarios_Load);
            this.FormClosed += new FormClosedEventHandler(FrmLibroInventarios_FormClosed);
        }

        void FrmLibroInventarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.LibroInventarios = null;
        }

        void FrmLibroInventarios_Load(object sender, EventArgs e)
        {
            #region EventosPantalla
            this.gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
            this.btnNuevo.Click += new EventHandler(btnNuevo_Click);
            this.btnEditar.Click += new EventHandler(btnEditar_Click);
            this.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            this.btnCargar.Click += new EventHandler(btnBuscar_Click);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            #endregion
            this.txtAño.Text = DateTime.Today.Year.ToString();
            this.txtMes.Text = DateTime.Today.Month.ToString();
            btnCalcularConsumo.Click += new EventHandler(btnCalcularConsumo_Click);
            Busqueda();
        }

        void btnCalcularConsumo_Click(object sender, EventArgs e)
        {
            Mes = Convert.ToInt16( txtMes.Text );
            Año = Convert.ToInt16(txtAño.Text);
            this.Cursor = Cursors.WaitCursor;
            object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod, "Actualizando libro Inventarios");
            this.Cursor = Cursors.Default;
            Busqueda();
        }
        private void WorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            e.Window.Message = "Calculando Salidas";
            FactoryLibroInventarios.AjustarSalidas(Mes, Año);
            e.Window.Message = "Calculando Entradas";
            
            FactoryLibroInventarios.PasarComprasLibro(Mes,Año);

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
            f.LibroDeInventarios(Lista, string.Format("Mes {0} Año {1}",txtMes.Text,txtAño.Text));
            f = null;
        }
        private void Busqueda()
        {
            db = new DatosEntities();
            Lista = FactoryLibroInventarios.getItems(db,Convert.ToInt16(txtMes.Text),Convert.ToInt16(txtAño.Text));
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void AgregarRegistro()
        {
            FrmLibroInventariosItem F;
            do
            {
                F = new FrmLibroInventariosItem();
                F.Incluir();
                if (F.DialogResult == DialogResult.OK)
                {
                    F.registro.Mes = Convert.ToInt16(txtMes.Text);
                    F.registro.Año = Convert.ToInt16(txtAño.Text);
                    F.registro.Calcular();
                    F.registro.Ajustes = F.registro.InventarioFisico - F.registro.Final;
                    F.registro.IdLibroInventarios = FactoryContadores.GetMax("IdLibroInventarios");
                    db.LibroInventarios.AddObject(F.registro);
                    db.SaveChanges();
                    Busqueda();
                }
            } while (F.DialogResult != System.Windows.Forms.DialogResult.Cancel);
        }
        private void EditarRegistro()
        {
            FrmLibroInventariosItem F = new FrmLibroInventariosItem();
            LibroInventario registro = (LibroInventario)this.bs.Current;
            if (registro == null)
                return;
            F.registro = registro;
            F.Modificar();
            if (F.DialogResult == DialogResult.OK)
            {
                F.registro.Mes = Convert.ToInt16(txtMes.Text);
                F.registro.Año = Convert.ToInt16(txtAño.Text);
                F.registro.Calcular();
                F.registro.Ajustes = F.registro.InventarioFisico - F.registro.Final;
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
                LibroInventario Registro = (LibroInventario)this.bs.Current;
                if (Registro == null)
                    return;
                if (MessageBox.Show("Esta seguro de eliminar este registro", "Atencion", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                try
                {
                    db.LibroInventarios.DeleteObject(Registro);
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
