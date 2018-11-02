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
    public partial class FrmAnulacionDePlato : Form
    {
        Usuario usuario = new Usuario();
        public MesasAbiertasPlatosAnulado platoAnulado;
        public FrmAnulacionDePlato()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmAnulacionDePlato_Load);
            this.Aceptar.Click+=new EventHandler(Aceptar_Click);
            this.Cancelar.Click+=new EventHandler(Cancelar_Click);
        }
        void FrmAnulacionDePlato_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmAnulacionDePlato_KeyDown);
            this.mesasAbiertasPlatosAnuladoBindingSource.DataSource = platoAnulado;
            this.mesasAbiertasPlatosAnuladoBindingSource.ResetBindings(true);
            this.txtClave.Properties.CharacterCasing = CharacterCasing.Upper;
            this.txtUsuario.Properties.CharacterCasing = CharacterCasing.Upper;
            this.txtUsuario.Properties.Items.AddRange(FactoryUsuarios.getAdministradores());
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            usuario = FactoryUsuarios.Item(this.txtUsuario.Text, this.txtClave.Text);
            if (usuario == null)
            {
                return;
            }
            if (usuario.TipoUsuario != "ADMINISTRADOR")
            {
                return;
            }
            platoAnulado.IdUsuario = usuario.IdUsuario;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        void FrmAnulacionDePlato_KeyDown(object sender, KeyEventArgs e)
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
