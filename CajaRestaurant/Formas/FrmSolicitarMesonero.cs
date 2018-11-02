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
    public partial class FrmSolicitarMesonero : Form
    {
        public Mesonero mesonero = new Mesonero();
        public FrmSolicitarMesonero()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmSolicitarMesonero_Load);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
        }
        void FrmSolicitarMesonero_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmSolicitarMesonero_KeyDown);
            this.txtClave.Validating += new CancelEventHandler(txtClave_Validating);
        }
        void txtClave_Validating(object sender, CancelEventArgs e)
        {
            Mesonero usuario = FactoryMesoneros.MesoneroxCodigo(txtClave.Text);
            if (usuario != null)
            {
                mesonero = usuario;
                txtNombreNesonero.Text = mesonero.Nombre;
                Aceptar.PerformClick();
            }
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            Mesonero usuario = FactoryMesoneros.MesoneroxCodigo(txtClave.Text);
            if (usuario == null)
            {
                MessageBox.Show("Codigo de mesonero invalido");
                return;
            }
            mesonero = usuario;
            txtNombreNesonero.Text = mesonero.Nombre;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        void FrmSolicitarMesonero_KeyDown(object sender, KeyEventArgs e)
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
