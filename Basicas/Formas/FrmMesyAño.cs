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
    public partial class FrmMesyAño : Form
    {
        public FrmMesyAño()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmMesyAño_Load);
        }
        public DateTime fecha = DateTime.Today;
        void FrmMesyAño_Load(object sender, EventArgs e)
        {
            this.txtFecha.DateTime = fecha;;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmMesyAño_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
        }

        void FrmMesyAño_KeyDown(object sender, KeyEventArgs e)
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
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void Aceptar_Click(object sender, EventArgs e)
        {
            this.fecha = txtFecha.DateTime;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
