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

    public partial class FrmPlatosItemContornos : Form
    {
        public Plato plato = new Plato();
        public FrmPlatosItemContornos()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmContornos_Load);
        }
        void  FrmContornos_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmContornos_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.platosContornoBindingSource.DataSource = plato.PlatosContornos;
            this.platosContornoBindingSource.ResetBindings(true);
            this.txtContorno.Properties.Items.AddRange(FactoryPlatos.getListContornos());
            this.txtContorno.Validating += new CancelEventHandler(txtContorno_Validating);            
        }
        void txtContorno_Validating(object sender, CancelEventArgs e)
        {
            if (plato.PlatosContornos.FirstOrDefault(x=> x.Contorno == txtContorno.Text)==null)
            {
                PlatosContorno contorno = new PlatosContorno();
                contorno.Contorno = txtContorno.Text;
                this.plato.PlatosContornos.Add(contorno);
            }
            txtContorno.Text="";
        }
        void FrmContornos_KeyDown(object sender, KeyEventArgs e)
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
        void btnAgregar_Click(object sender, EventArgs e)
        {
            PlatosContorno item = new PlatosContorno();
            item.Contorno = this.txtContorno.Text;
            this.txtContorno.Text = "";
        }
        void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
            {
                if (this.listBoxContornos.Focused)
                {
                    try
                    {
                        plato.PlatosContornos.Remove((PlatosContorno)this.platosContornoBindingSource.Current);
                    }
                    catch { }
                }
                e.Handled = true;
            }
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.platosContornoBindingSource.EndEdit();
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
            this.platosContornoBindingSource.ResetBindings(true);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
