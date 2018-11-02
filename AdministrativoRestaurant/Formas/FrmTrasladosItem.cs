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
    public partial class FrmTrasladosItem : Form
    {
        private bool esNuevo = true;
        public Traslado registro = new Traslado();
        private Ingrediente ingrediente = null;
        private TrasladosIngrediente registroDetalle = null;
        FeriaEntities db = new FeriaEntities();
        public FrmTrasladosItem()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmTrasladosItem_Load);
        }

        void  FrmTrasladosItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmTrasladosItem_KeyDown);
            #region Ingredientes
            txtIngrediente.Validating += new CancelEventHandler(txtIngrediente_Validating);
            txtIngrediente.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtIngrediente_ButtonClick);
            txtCantidad.Validating += new CancelEventHandler(txtCantidad_Validating);
            this.trasladosIngredienteBindingSource.ListChanged += new ListChangedEventHandler(trasladosIngredienteBindingSource_ListChanged);
            this.gridControl1.KeyDown+=new KeyEventHandler(gridControl1_KeyDown);
            #endregion
        }

        private void Limpiar()
        {
            registro = new Traslado();
            registro.Fecha = DateTime.Today;
        }
        public void Incluir()
        {
            esNuevo = true;
            Limpiar();
            Enlazar();
            this.ShowDialog();
        }
        public void Ver()
        {
            Enlazar();
            this.Aceptar.Enabled = false;
            this.ShowDialog();
        }
        public void Modificar()
        {
            esNuevo = false;
            Enlazar();
            esNuevo = false;
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.trasladoBindingSource.DataSource = registro;
            this.trasladoBindingSource.ResetBindings(true);
            this.trasladosIngredienteBindingSource.DataSource = registro;
            this.trasladosIngredienteBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                trasladoBindingSource.EndEdit();
                trasladosIngredienteBindingSource.EndEdit();
                registro = (Traslado)trasladoBindingSource.Current;                
                FactoryTraslados.Validar(registro);
                this.Guadar();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar los datos \n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Guadar()
        {
            this.trasladoBindingSource.EndEdit();
            registro.IdUsuario = FactoryUsuarios.UsuarioActivo.IdUsuario;
            if (esNuevo)
            {
                registro.IdTraslado = FactoryContadores.GetMax("IdTraslado");
            }
            foreach (HK.TrasladosIngrediente p in registro.TrasladosIngredientes)
            {
                if (p.IdTrasladoIngrediente == null)
                {
                    p.IdTrasladoIngrediente = FactoryContadores.GetMax("IdTrasladoIngrediente");
                }
            }
            if (esNuevo)
            {
                db.Traslados.AddObject(registro);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.InnerException.Message);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.trasladoBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FrmTrasladosItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    
                    //this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F12:
                    this.Aceptar.PerformClick();
                    e.Handled = true;
                    break;
            }
        }
        #region ManejoDocumentoProductos
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                gridView1.MoveBy(0);
            }

            if (gridView1.ActiveEditor == null)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
                {
                    if (this.gridView1.IsFocusedView)
                    {                       
                        TrasladosIngrediente i = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
                        registro.TrasladosIngredientes.Remove(i);
                    }
                    e.Handled = true;
                }
            }
        }
        void trasladosIngredienteBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.trasladoBindingSource.ResetCurrentItem();
        }
        private void txtIngrediente_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            registroDetalle = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
            if (UbicarProducto(Texto))
            {
                if (ingrediente == null)
                    return;
                LeerIngrediente(false);
                Editor.Text = ingrediente.Descripcion;
                if (string.IsNullOrEmpty(ingrediente.Descripcion))
                {
                    Editor.Undo();
                    e.Cancel = true;
                }
            }
            else
            {
                LeerIngrediente(false);
                Editor.Undo();
            }
        }
        void txtIngrediente_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            registroDetalle = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
            UbicarProducto("");
            LeerIngrediente(false);
        }
        private void LeerIngrediente(bool Buscar)
        {
            if (ingrediente == null)
            {
                return;
            }
            registroDetalle = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
            registroDetalle.Cantidad = 1;
            registroDetalle.ExistenciaAnterior = ingrediente.Existencia;
            registroDetalle.IdIngrediente = ingrediente.IdIngrediente;
            registroDetalle.Ingrediente = ingrediente.Descripcion;
            registroDetalle.UnidadMedida = ingrediente.UnidadMedida;
        }
        private bool UbicarProducto(string Texto)
        {
            List<Ingrediente> T = new List<Ingrediente>();
            T = FactoryIngredientes.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    if (MessageBox.Show("Suministro no Encontrado, Desea crear uno nuevo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        ingrediente = new Ingrediente();
                        return false;
                    }
                    FrmIngredientesItem nuevo = new FrmIngredientesItem();
                    nuevo.descripcion = Texto;
                    nuevo.Incluir();
                    if (nuevo.DialogResult == DialogResult.OK)
                    {
                        using (var db = new FeriaEntities())
                        {
                            nuevo.registro.IdIngrediente = FactoryContadores.GetMax("IdIngrediente");
                            nuevo.registro.Activo = true;
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
                    break;
            }
            LeerIngrediente(false);
            return true;
        }
        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            if ((double)Editor.Value <= 0)
            {
                Editor.Value = 1;
            }
            if (this.trasladosIngredienteBindingSource.Current == null)
                return;
            registroDetalle = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
            registroDetalle.Cantidad = (double)Editor.Value;
        }
        void txtCostoIva_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit Editor = (DevExpress.XtraEditors.CalcEdit)this.gridControl1.MainView.ActiveEditor;
            if (!Editor.IsModified)
                return;
            if (this.trasladosIngredienteBindingSource.Current == null)
                return;
            registroDetalle = (TrasladosIngrediente)this.trasladosIngredienteBindingSource.Current;
        }
        #endregion

    }
}
