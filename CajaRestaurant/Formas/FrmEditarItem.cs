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
    public partial class FrmEditarItem : Form
    {
        public MesasAbiertasPlato item;
        public FrmEditarItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmEditarItem_Load);
        }
        void FrmEditarItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmSolicitarMesonero_KeyDown);
            this.mesasAbiertasPlatoBindingSource.DataSource = item;
            this.mesasAbiertasPlatoBindingSource.ResetBindings(true);
            PrecioCalcEdit.Validated += new EventHandler(PrecioCalcEdit_Validated);
            if (FactoryUsuarios.UsuarioActivo.PuedeCambiarCantidad.GetValueOrDefault(false)==false)
            {
                this.CantidadCalcEdit.Enabled = false;
            }
            if (FactoryUsuarios.UsuarioActivo.PuedeCambiarPrecios.GetValueOrDefault(false)==false)
            {
                this.PrecioCalcEdit.Enabled = false;
            }
        }

        void PrecioCalcEdit_Validated(object sender, EventArgs e)
        {
            Aceptar.PerformClick();
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.mesasAbiertasPlatoBindingSource.CancelEdit();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            this.mesasAbiertasPlatoBindingSource.EndEdit();
            item.Total = item.PrecioConIva.GetValueOrDefault(0) * item.Cantidad;
            item.TotalBase = item.Precio.GetValueOrDefault(0) * item.Cantidad;
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
