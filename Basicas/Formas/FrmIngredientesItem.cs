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
    public partial class FrmIngredientesItem : Form
    {
        public Ingrediente registro = new Ingrediente();
        public string descripcion;
        public FrmIngredientesItem()
        {
            InitializeComponent();
            this.Load+=new System.EventHandler(FrmIngredientesItem_Load);
        }

        void  FrmIngredientesItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.GrupoComboBoxEdit.Properties.Items.AddRange(FactoryIngredientes.getArrayGrupos());
            this.UnidadMedidaTextEdit.Properties.Items.AddRange(FactoryIngredientes.getArrayUnidadesMedida());
        }
        private void Limpiar()
        {
            registro = new Ingrediente();
            registro.TasaIva = Basicas.parametros().TasaIva;
            registro.Costo = 0;
            registro.LlevaInventario = true;
            registro.Insumo = true;
            registro.Activo = true;
        }
        public void Incluir()
        {
            Limpiar();
            registro.Descripcion = descripcion;
            Enlazar();
            this.ShowDialog();
        }
        public void Modificar()
        {
            Enlazar();
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.ingredienteBindingSource.DataSource = registro;
            this.ingredienteBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ingredienteBindingSource.EndEdit();
                if (string.IsNullOrEmpty(registro.Descripcion))
                    throw new Exception("Error Descripcion no puede estar vacio");
                if (string.IsNullOrEmpty(registro.Grupo))
                    throw new Exception("Error el grupo no puede estar vacio");
                if (string.IsNullOrEmpty(registro.UnidadMedida))
                    throw new Exception("Error la unidad de medida no puede estar vacio");
                if (registro.Descripcion.Length>100)
                    throw new Exception("Error descripcion no puede tener mas de 100 caracteres");
                if (registro.UnidadMedida.Length>20)
                    throw new Exception("Error unidad de medida  no puede tener mas de 20 caracteres");
                if (registro.Grupo.Length > 20)
                    throw new Exception("Error el grupo  no puede tener mas de 20 caracteres");
                registro = (Ingrediente)ingredienteBindingSource.Current;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.ingredienteBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
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

