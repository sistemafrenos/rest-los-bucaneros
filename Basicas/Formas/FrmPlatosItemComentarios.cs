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
    public partial class FrmPlatosItemComentarios : Form
    {
        public Plato plato = new Plato();
        public FrmPlatosItemComentarios()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPlatosItemComentarios_Load);
        }

        void FrmPlatosItemComentarios_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmContornos_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.platosComentarioBindingSource.DataSource = plato.PlatosComentarios;
            this.platosComentarioBindingSource.ResetBindings(true);
            this.txtComentario.Properties.Items.AddRange(FactoryPlatos.getArrayComentarios());
            this.txtComentario.Validating += new CancelEventHandler(txtContorno_Validating);
        }
        void txtContorno_Validating(object sender, CancelEventArgs e)
        {
            if (plato.PlatosComentarios.FirstOrDefault(x => x.Comentario == txtComentario.Text) == null)
            {
                PlatosComentario comentario = new PlatosComentario();
                comentario.Comentario = txtComentario.Text;
                this.plato.PlatosComentarios.Add(comentario);
            }
            txtComentario.Text = "";
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
            PlatosComentario item = new PlatosComentario();
            item.Comentario = this.txtComentario.Text;
            this.txtComentario.Text = "";
        }
        void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Subtract)
            {
                if (this.listBoxComentarios.Focused)
                {
                    try
                    {
                        plato.PlatosComentarios.Remove((PlatosComentario)this.platosComentarioBindingSource.Current);
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
                this.platosComentarioBindingSource.EndEdit();
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
            this.platosComentarioBindingSource.ResetBindings(true);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
