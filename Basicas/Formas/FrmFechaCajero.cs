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
    public partial class FrmFechaCajero : Form
    {
        public Usuario cajero;
        public DateTime fecha = DateTime.Today;
        public bool DetallePlatos = false;
        public FrmFechaCajero()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmFechaCajero_Load);
        }
        void FrmFechaCajero_Load(object sender, EventArgs e)
        {
            this.txtFecha.DateTime = fecha;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmLapso_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.txtCajero.Properties.Items.AddRange(FactoryUsuarios.getCajeros());
            this.txtCajero.Properties.CharacterCasing = CharacterCasing.Upper;
            this.txtCajero.Validating += new CancelEventHandler(txtCajero_Validating);
            this.detallePlatos.Checked = DetallePlatos;
            this.detallePlatos.Visible = DetallePlatos;
        }

        void txtCajero_Validating(object sender, CancelEventArgs e)
        {
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
            cajero = null;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void Aceptar_Click(object sender, EventArgs e)
        {
            this.fecha = txtFecha.DateTime;
            this.cajero = FactoryUsuarios.ItemNombre(txtCajero.Text);
            this.DetallePlatos = this.detallePlatos.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
