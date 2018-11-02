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
    public partial class FrmCajerosItem : Form
    {
               public Usuario registro = null;
 
        public FrmCajerosItem()
        {
            InitializeComponent();
            this.Load +=new EventHandler(FrmCajerosItem_Load);
        }

        void  FrmCajerosItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.CedulaTextEdit.Validating += new CancelEventHandler(CedulaTextEdit_Validating);
        }
        void CedulaTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Editor.Text = Basicas.CedulaRif(Editor.Text);
            this.cajeroBindingSource.EndEdit();
        }
        private void Limpiar()
        {
            registro = new Usuario();
            registro.TipoUsuario = "CAJERO";
            registro.PuedeDarConsumoInterno = true;
            registro.PuedePedirCorteDeCuenta = true;
            registro.PuedeSepararCuentas = true;
            registro.PuedeRegistrarPago = true;
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
            this.cajeroBindingSource.DataSource = registro;
            this.cajeroBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                cajeroBindingSource.EndEdit();
                registro = (Usuario)cajeroBindingSource.Current;
                if (registro.Error != null)
                {
                    MessageBox.Show(registro.Error, "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
            this.cajeroBindingSource.ResetCurrentItem();
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
