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
    public partial class FrmPedirNumeroOrden : Form
    {
        public string numeroOrden;
        public double cambio = 0;
        public FrmPedirNumeroOrden()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Load += new EventHandler(FrmPedirNumeroOrden_Load);
        }
        void FrmPedirNumeroOrden_Load(object sender, EventArgs e)
        {
            if (cambio == 0)
            {
                this.ItemForCambio.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                this.txtCambio.Value = (decimal)cambio; 
            }
            this.txtNumeroOrden.Validated += new EventHandler(txtNumeroOrden_Validated);
            this.KeyDown += new KeyEventHandler(FrmPedirNumeroOrden_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.txtNumeroOrden.Value = Convert.ToInt16(FactoryContadores.GetMax("NumeroOrden"));
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void FrmPedirNumeroOrden_KeyDown(object sender, KeyEventArgs e)
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

        void txtNumeroOrden_Validated(object sender, EventArgs e)
        {
            salir();
        }

        void Aceptar_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void salir()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            numeroOrden = this.txtNumeroOrden.Value.ToString();
        }

    }
}
