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
    public partial class FrmIngredientesInventario : Form
    {
        DateTime dia = DateTime.Today;
        DatosEntities db = new DatosEntities();
        List<IngredientesInventario> Lista = new List<IngredientesInventario>();
        public string filtro;
        public FrmIngredientesInventario()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmIngredientesInventario_Load);
        }

        void FrmIngredientesInventario_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(FrmIngredientesInventario_FormClosed);
            this.FormClosed+=new FormClosedEventHandler(FrmIngredientesInventario_FormClosed);
            this.btnImprimir.Click += new EventHandler(btnImprimir_Click);
            this.gridView1.RowUpdated+=new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(gridView1_RowUpdated);
            this.gridView1.ValidateRow+=new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gridView1_ValidateRow);
            this.repositoryItemCalcEdit1.ValidateOnEnterKey = true;
            this.repositoryItemCalcEdit1.Validating += new CancelEventHandler(repositoryItemCalcEdit1_Validating);
            this.btnIniciarInventario.Click += new EventHandler(btnIniciarInventario_Click);
            this.btnImprimirExistencias.Click += new EventHandler(btnImprimirExistencias_Click);
            Busqueda();
        }
        void btnImprimirExistencias_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ListadoInventarios();
        }
        void btnIniciarInventario_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.bs.DataSource = new List<IngredientesInventario>();
            this.bs.ResetBindings(true);
            object result = Jacksonsoft.WaitWindow.Show(this.WorkerMethod, "Iniciando Inventarios");
            this.Cursor = Cursors.Default;
            Busqueda();
        }
        private void WorkerMethod(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            e.Window.Message = "Cuardando Inventarios";
            CargarLibro();
            e.Window.Message = "Cargando Nuevos  Productos";
            CargarNuevosProductos();
            if (e.Arguments.Count > 0)
            {
                e.Result = e.Arguments[0].ToString();
            }
            else
            {
                e.Result = "Ok";
            }
            Busqueda();
        }
        private void CargarLibro()
        {
            foreach (var Item in Lista)
            {
                IngredientesInventariosHistorial nuevo = new IngredientesInventariosHistorial();
                nuevo.Ajuste = Item.Ajuste;
                nuevo.Entradas = Item.Entradas;
                nuevo.FechaFinal = DateTime.Today;
                nuevo.FechaInicio = Item.FechaInicio;
                nuevo.Final = Item.Final;
                nuevo.Grupo = Item.Grupo;
                nuevo.IdIngrediente = Item.IdIngrediente;
                nuevo.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteHistorial");
                nuevo.Ingrediente = Item.Ingrediente;
                nuevo.Inicio = Item.Inicio;
                nuevo.InventarioFisico = Item.InventarioFisico;
                nuevo.Salidas = Item.Salidas;
                db.IngredientesInventariosHistorials.AddObject(nuevo);
                Item.Ajuste = 0;
                Item.Entradas = 0;
                Item.FechaInicio = DateTime.Today;
                Item.Salidas = 0;
                Item.Inicio = Item.InventarioFisico;
                Item.Final = Item.Inicio;
                Item.InventarioFisico = Item.Final;
            }
            db.SaveChanges();
        }
        void CargarNuevosProductos()
        {
            foreach (var ingrediente in FactoryIngredientes.getItemsConInventario())
            {
                IngredientesInventario item = Lista.FirstOrDefault(x => x.IdIngrediente == ingrediente.IdIngrediente);
                if (item == null)
                {
                    IngredientesInventario nuevoItem = new IngredientesInventario();
                    nuevoItem.Ajuste = 0;
                    nuevoItem.Entradas = 0;
                    nuevoItem.FechaInicio = DateTime.Today;
                    nuevoItem.Grupo = ingrediente.Grupo;
                    nuevoItem.IdIngrediente = ingrediente.IdIngrediente;
                    nuevoItem.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteInventario");
                    nuevoItem.Ingrediente = ingrediente.Descripcion;
                    nuevoItem.Inicio = ingrediente.Existencia.GetValueOrDefault(0);
                    nuevoItem.Salidas = 0;
                    nuevoItem.Final = nuevoItem.Inicio + nuevoItem.Entradas - nuevoItem.Salidas;
                    nuevoItem.InventarioFisico = nuevoItem.Final;
                    db.IngredientesInventarios.AddObject(nuevoItem);
                }
            }
            db.SaveChanges();
        }

        void repositoryItemCalcEdit1_Validating(object sender, CancelEventArgs e)
        {
            Calcular();
            this.bs.ResetCurrentItem();
        }

        void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.IngredienteInventarios(Lista);
        }
        
        void btnGuardar_Click(object sender, EventArgs e)
        {
            this.bs.EndEdit();
            foreach (IngredientesInventario i in Lista)
            {
                Ingrediente item = FactoryIngredientes.Item(i.IdIngrediente);
                i.Ajuste = i.InventarioFisico - i.Final;
                item.Existencia = i.InventarioFisico;
            }
            this.db.SaveChanges();
        }

        void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda()
        {
            db = new DatosEntities();
            Lista = (from q in db.IngredientesInventarios
                     orderby q.Ingrediente
                     select q).ToList();
            DateTime? FechaInicio = Lista.Min(x => x.FechaInicio);
            if (FechaInicio != null)
            {
                this.txtFecha.Text = string.Format("Inventario iniciado el {0}", FechaInicio.Value.ToShortDateString());
            }
            else
            {
                this.txtFecha.Text = "";
            }
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        void FrmIngredientesInventario_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.IngredientesInventario = null;
        }
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            this.bs.EndEdit();
            IngredientesInventario Item = (IngredientesInventario)e.Row;
            this.btnGuardar.Visible = true;
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                Calcular();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                e.Valid = false;
            }
        }
        private void Calcular()
        {
            try
            {
                bs.EndEdit();
                IngredientesInventario Item = (IngredientesInventario)bs.Current;
                Item.Final = Item.Inicio.GetValueOrDefault(0) + Item.Entradas.GetValueOrDefault(0) - Item.Salidas.GetValueOrDefault(0);
                Item.Ajuste = Item.InventarioFisico - Item.Final;
                db.SaveChanges();
            }
            catch (Exception x)
            {
                Basicas.ManejarError(x);
            }
        }
    }
}