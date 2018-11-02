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
    public partial class FrmPedirContornos : Form
    {
        DatosEntities db = new DatosEntities();
        Plato plato = new Plato();
        public List<string> Contornos = new List<string>();
        public List<string> Comentarios = new List<string>();
        List<Button> presentaciones = new List<Button>();
        public string codigoPlato;
        public string presentacion;
        public double precio;
        public FrmPedirContornos()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPedirContornos_Load);
        }
        void FrmPedirContornos_Load(object sender, EventArgs e)
        {
            presentaciones.AddRange(new Button[] { Presentacion0, Presentacion1, Presentacion2, Presentacion3 });
            plato = FactoryPlatos.ItemxCodgo(db, codigoPlato);
            this.platosContornoBindingSource.DataSource = plato.PlatosContornos;
            this.platosContornoBindingSource.ResetBindings(true);
            this.platosComentarioBindingSource.DataSource = plato.PlatosComentarios;
            this.platosComentarioBindingSource.ResetBindings(true);
            if (platosContornoBindingSource.List.Count > 0)
            {
                txtContornos.Visible = true;
            }
            if (platosComentarioBindingSource.List.Count > 0)
            {
                txtComentarios.Visible = true;
            }
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmPedirContorno_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
        }
        void FrmPedirContorno_KeyDown(object sender, KeyEventArgs e)
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
        private void Aceptar_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            try
            {
                foreach (var x in this.txtContornos.CheckedItems)
                {
                    Contornos.Add(((PlatosContorno)x).Contorno);
                }
                foreach (var x in this.txtComentarios.CheckedItems)
                {
                    Comentarios.Add(((PlatosComentario)x).Comentario);
                }
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
