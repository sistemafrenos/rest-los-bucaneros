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
    public partial class FrmLapsoHoras : Form
    {
        public DateTime desde;
        public DateTime hasta;
        public FrmLapsoHoras()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmLapsoHoras_Load);
        }

        void  FrmLapsoHoras_Load(object sender, EventArgs e)
        {
            desde = DateTime.Now.AddHours(-8);
            hasta = DateTime.Now;
            this.txtDesde.Time = desde;
            this.txtHasta.Time = hasta;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmLapso_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
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
            this.desde = txtDesde.Time;
            this.hasta = txtHasta.Time;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
