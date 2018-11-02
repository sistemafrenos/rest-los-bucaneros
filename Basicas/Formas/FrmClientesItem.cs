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
    public partial class FrmClientesItem : Form
    {
        public Cliente registro = new Cliente();
        public FrmClientesItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmClientesItem_Load);
        }
        void FrmClientesItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.CedulaRifTextEdit.Validating += new CancelEventHandler(CedulaRifTextEdit_Validating);
            this.TipoPrecioComboBoxEdit.Properties.Items.AddRange(new object[] { "PRECIO 1", "PRECIO 2" });
        }
        void CedulaRifTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            Editor.Text = Basicas.CedulaRif(Editor.Text);
            this.bs.EndEdit();
        }
        private void Limpiar()
        {
            registro = new Cliente();
            registro.Activo = true;
            registro.LimiteCredito = 0;
            registro.DiasCredito = 0;
          //  registro.TipoPrecio = "PRECIO 1";
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
            this.bs.DataSource = registro;
            this.bs.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                bs.EndEdit();
                registro = (Cliente)bs.Current;
                if (registro.Errores().Count > 0)
                {
                    MessageBox.Show(registro.ErroresStr(), "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.bs.ResetCurrentItem();
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
