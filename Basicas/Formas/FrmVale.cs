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
    public partial class FrmVale : Form
    {
        public FrmVale()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmVale_Load);
        }
        public Vale registro = new Vale();
        void FrmVale_Load(object sender, EventArgs e)
        {            
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmValesItem_KeyDown);
            this.ConceptoTextEdit.Properties.Items.AddRange(FactoryVales.getConceptos());
        }
        private void Limpiar()
        {
            registro = new Vale();
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
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.valeBindingSource.DataSource = registro;
            this.valeBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                valeBindingSource.EndEdit();
                registro = (Vale)valeBindingSource.Current;
                FactoryVales.Validar(registro);
                using( var db = new DatosEntities())
                {
                    try
                    {
                        registro.Fecha = DateTime.Today;
                        registro.IdVale = FactoryContadores.GetMax("IdVale");
                        registro.IdCajero = FactoryUsuarios.UsuarioActivo.IdUsuario;
                        registro.Cajero = FactoryUsuarios.UsuarioActivo.Nombre;
                        registro.Concepto = this.ConceptoTextEdit.Text;
                        registro.Numero = FactoryContadores.GetMax("NumeroVale");
                        db.Vales.AddObject(registro);
                        db.SaveChanges();
                        Fiscal F = new Fiscal();
                        F.ImprimeVale(registro);
                        F = null;
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message);
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar los datos \n"  + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.valeBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FrmValesItem_KeyDown(object sender, KeyEventArgs e)
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

