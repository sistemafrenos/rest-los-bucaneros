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
    public partial class FrmMesasItem : Form
    {
        public FrmMesasItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmMesasItem_Load);
        }
        public Mesa registro = new Mesa();
        void FrmMesasItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmMesasItem_KeyDown);
            this.UbicacionComboBoxEdit.Properties.Items.AddRange(FactoryMesas.getUbicaciones());
            this.btnCrearCodigo.Click += new EventHandler(btnCrearCodigo_Click);
        }

        void btnCrearCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                string contador = FactoryContadores.GetMax(this.UbicacionComboBoxEdit.Text);
                registro.Codigo = this.UbicacionComboBoxEdit.Text.Substring(0, 2) + contador.Substring(3, 3);
            }
            catch { }
        }
        private void Limpiar()
        {
            registro = new Mesa();
            registro.CobraServicio = true;
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
            this.mesaBindingSource.DataSource = registro;
            this.mesaBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                mesaBindingSource.EndEdit();
                registro = (Mesa)mesaBindingSource.Current;
                FactoryMesas.Validar(registro);
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
            this.mesaBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FrmMesasItem_KeyDown(object sender, KeyEventArgs e)
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
