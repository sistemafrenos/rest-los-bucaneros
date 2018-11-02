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
    public partial class FrmLapso : Form
    {
        public  DateTime desde = DateTime.Today;
        public  DateTime hasta = DateTime.Today;
        public  bool Detallado = false;
        public FrmLapso()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmLapso_Load);
        }

        void FrmLapso_Load(object sender, EventArgs e)
        {
            this.txtDesde.DateTime = desde;
            this.txtHasta.DateTime = hasta;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmLapso_KeyDown);
            this.Aceptar.Click+=new EventHandler(Aceptar_Click);
            this.Cancelar.Click+=new EventHandler(Cancelar_Click);
            this.txtDetallado.Checked = Detallado;
            this.txtDetallado.Visible = Detallado;
        }

        void FrmLapso_KeyDown(object sender, KeyEventArgs e)
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
            this.desde = txtDesde.DateTime;
            this.hasta = txtHasta.DateTime;
            this.Detallado = txtDetallado.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
