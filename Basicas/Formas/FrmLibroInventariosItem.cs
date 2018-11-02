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
    public partial class FrmLibroInventariosItem : Form
    {

        public FrmLibroInventariosItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmLibroInventariosItem_Load);
        }
        public LibroInventario registro = new LibroInventario();
        private Ingrediente ingrediente = new Ingrediente();
        void FrmLibroInventariosItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmLibroInventariosItem_KeyDown);
            this.EntradasCalcEdit.Validating+=new CancelEventHandler(CalcEdit_Validating);
            this.SalidasCalcEdit.Validating += new CancelEventHandler(CalcEdit_Validating);
            this.InicioCalcEdit.Validating += new CancelEventHandler(CalcEdit_Validating);
            this.InventarioFisicoCalcEdit.Validating += new CancelEventHandler(InventarioFisicoCalcEdit_Validating);
            this.AjustesCalcEdit.Validating += new CancelEventHandler(AjustesCalcEdit_Validating);
            this.ProductoTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(ProductoTextEdit_ButtonClick);
            this.ProductoTextEdit.Validating += new CancelEventHandler(ProductoTextEdit_Validating);
        }
        void ProductoTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit Editor = (DevExpress.XtraEditors.ButtonEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            if (UbicarProducto(Texto))
            {
                if (ingrediente == null)
                {
                    Editor.Undo();
                    e.Cancel = true;
                    registro.Costo = ingrediente.Costo;
                    registro.IdProducto = ingrediente.IdIngrediente;
                    Editor.Text = ingrediente.Descripcion;
                    return;
                }
            }
            else
            {
                registro.Costo = null;
                registro.IdProducto = null;
                Editor.Undo();
            }
        }
        void ProductoTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            UbicarProducto("");
        }
        private bool UbicarProducto(string Texto)
        {
            List<Ingrediente> T = new List<Ingrediente>();
            T = FactoryIngredientes.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    if (MessageBox.Show("Producto no Encontrado, Desea crear uno nuevo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        ingrediente = new Ingrediente();
                        return false;
                    }
                    FrmIngredientesItem nuevo = new FrmIngredientesItem();
                    nuevo.descripcion = Texto;
                    nuevo.Incluir();
                    if (nuevo.DialogResult == DialogResult.OK)
                    {
                        using (var db = new DatosEntities())
                        {
                            nuevo.registro.IdIngrediente = FactoryContadores.GetMax("IdIngrediente");
                            db.Ingredientes.AddObject(nuevo.registro);
                            db.SaveChanges();
                        }
                    }
                    if (nuevo.DialogResult == DialogResult.OK)
                    {
                        ingrediente = nuevo.registro;
                    }
                    else
                    {
                        ingrediente = new Ingrediente();
                        return false;
                    }
                    break;
                case 1:
                    ingrediente = T[0];
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarIngredientes(Texto);
                    ingrediente = (Ingrediente)F.registro;
                    if (ingrediente == null)
                    {

                    }
                    break;
            }
            registro.Costo = ingrediente.Costo;
            registro.IdProducto = ingrediente.IdIngrediente;
            registro.Producto = ingrediente.Descripcion;
            return true;
        }
        void AjustesCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            //DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            //if (!editor.IsModified)
            //    return;
            this.InventarioFisicoCalcEdit.Value = this.FinalCalcEdit.Value + this.AjustesCalcEdit.Value;
        }
        void InventarioFisicoCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            //if (!editor.IsModified)
            //    return;            
            this.AjustesCalcEdit.Value = this.InventarioFisicoCalcEdit.Value - this.FinalCalcEdit.Value;
        }
        void CalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            //if (!editor.IsModified)
            //    return;
            this.libroInventarioBindingSource.EndEdit();
            registro.Calcular();            
        }
        private void Limpiar()
        {
            registro = new LibroInventario();

        }
        public void Incluir()
        {
            Limpiar();
            Enlazar();
            this.ShowDialog();
        }
        public void Modificar()
        {
            Enlazar();
            this.ProductoTextEdit.Enabled = false;
            this.InicioCalcEdit.Enabled = false;
            this.EntradasCalcEdit.Enabled = false;
            this.SalidasCalcEdit.Enabled = false;
            this.FinalCalcEdit.Enabled = false;
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.libroInventarioBindingSource.DataSource = registro;
            this.libroInventarioBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                libroInventarioBindingSource.EndEdit();
                registro = (LibroInventario)libroInventarioBindingSource.Current;
                registro.Final = registro.Inicio.GetValueOrDefault(0) + registro.Entradas.GetValueOrDefault(0) - registro.Salidas.GetValueOrDefault(0);
                registro.Ajustes = registro.Final.GetValueOrDefault(0) - registro.InventarioFisico.GetValueOrDefault(0);
                FactoryLibroInventarios.Validar(registro);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar los datos \n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.libroInventarioBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FrmLibroInventariosItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F12:
                    this.Aceptar.PerformClick();
                    e.Handled = true;
                    break;
            }
        }

    }
}
