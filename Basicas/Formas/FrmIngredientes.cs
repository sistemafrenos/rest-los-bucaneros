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
    public partial class FrmIngredientes : Form
    {
        public FrmIngredientes()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmIngredientes_Load);
        }
        DatosEntities db = new DatosEntities();
        List<Ingrediente> Lista = new List<Ingrediente>();
        public string filtro;
        void FrmIngredientes_Load(object sender, EventArgs e)
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
            this.btnImprimirExistencias.Click += new EventHandler(btnImprimirExistencias_Click);
            this.FormClosed += new FormClosedEventHandler(FrmIngredientes_FormClosed);
            Busqueda();
        }

        void btnImprimirExistencias_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoInventarios();
        }
        void FrmIngredientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.IngredientesLista = null;
        }
        void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoIngredientes(Lista);
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
            Pantallas.IngredientesLista = null;
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            db = new DatosEntities();
            Lista = FactoryIngredientes.getItems(db, this.txtBuscar.Text);
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
            FrmIngredientesItem F = new FrmIngredientesItem();
            F.Incluir();
            if (F.DialogResult == DialogResult.OK)
            {
                F.registro.IdIngrediente = FactoryContadores.GetMax("IdIngrediente");
                F.registro.Activo = true;
                db.Ingredientes.AddObject(F.registro);
                db.SaveChanges();
                Busqueda();
            }
        }
        private void EditarRegistro()
        {
            FrmIngredientesItem F = new FrmIngredientesItem();
            Ingrediente registro = (Ingrediente)this.bs.Current;
            if (registro == null)
                return;
            F.registro = registro;
            F.Modificar();
            if (F.DialogResult == DialogResult.OK)
            {
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
                Ingrediente Registro = (Ingrediente)this.bs.Current;
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
