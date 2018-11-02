using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Formas
{
    public partial class FrmFacturasItem : Form
    {
        public Factura factura = null;
        public FrmFacturasItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmFacturasItem_Load);
        }

        void FrmFacturasItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmFacturasItem_KeyDown);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
        }

        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void FrmFacturasItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
            }  
        }            
        internal void Ver()
        {
 	        facturaBindingSource.DataSource = factura;
            facturaBindingSource.ResetBindings(true);
            facturaProductoBindingSource.DataSource = factura.FacturasPlatos;
            facturaProductoBindingSource.ResetBindings(true);
            this.ShowDialog();
        }

        private void FrmFacturasItem_Load_1(object sender, EventArgs e)
        {

        }
    }
}
