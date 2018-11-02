using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmLibroVentasItem : Form
    {
        public LibroVenta registro = new LibroVenta();
        private Cliente proveedor = new Cliente();
        public FrmLibroVentasItem()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmLibroVentasItem_Load);
        }

        void FrmLibroVentasItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmLibroVentasItem_KeyDown);
            #region Proveedor
            this.CedulaRifButtonEdit.Validating += new CancelEventHandler(CedulaRifButtonEdit_Validating);
            this.CedulaRifButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CedulaRifButtonEdit_ButtonClick);
            #endregion
            this.MontoGravableCalcEdit.Validating += new CancelEventHandler(MontoGravableCalcEdit_Validating);
            this.TasaIvaCalcEdit.Validating += new CancelEventHandler(TasaIvaCalcEdit_Validating);
            this.MontoExentoCalcEdit.Validating += new CancelEventHandler(MontoExentoCalcEdit_Validating);
            this.MontoIvaCalcEdit.Validating += new CancelEventHandler(MontoIvaCalcEdit_Validating);
        }
        void MontoIvaCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            //if (!editor.IsModified)
            //    return;
            this.libroVentaBindingSource.EndEdit();
            registro.Calcular();
        }
        void MontoExentoCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            if (!editor.IsModified)
                return;
            this.libroVentaBindingSource.EndEdit();
            registro.Calcular();
        }
        void TasaIvaCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            if (!editor.IsModified)
                return;
            this.libroVentaBindingSource.EndEdit();
            registro.MontoIva = registro.MontoGravable * registro.TasaIva / 100;
            registro.Calcular();
        }
        void MontoGravableCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            //if (!editor.IsModified)
            //    return;
            this.libroVentaBindingSource.EndEdit();
            registro.MontoIva = registro.MontoGravable * registro.TasaIva / 100;
            registro.Calcular();
        }
        #region Proveedor
        void CedulaRifButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.BuscarClientes("");
            if (F.registro != null)
            {
                proveedor = (Cliente)F.registro;
                proveedor = FactoryClientes.Item(proveedor.CedulaRif);
                LeerProveedor();
            }
            else
            {
                proveedor = new Cliente();
            }
        }
        void CedulaRifButtonEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            this.libroVentaBindingSource.EndEdit();
            List<Cliente> T = FactoryClientes.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    proveedor = new Cliente();
                    proveedor.CedulaRif = Basicas.CedulaRif(Texto);
                    break;
                case 1:
                    proveedor = T[0];
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarClientes(Texto);
                    if (F.registro != null)
                    {
                        proveedor = (Cliente)F.registro;
                        proveedor = FactoryClientes.Item(proveedor.CedulaRif);
                    }
                    else
                    {
                        proveedor = null;
                    }
                    break;
            }
            LeerProveedor();
        }
        private void LeerProveedor()
        {
            registro.CedulaRif = proveedor.CedulaRif;
            registro.RazonSocial = proveedor.RazonSocial;
            registro.Direccion = proveedor.Direccion;
            this.libroVentaBindingSource.ResetCurrentItem();
        }
        #endregion
        private void Limpiar()
        {
            registro = new LibroVenta();
            proveedor = new Cliente();
        }
        public void Incluir(string mes, string año)
        {
            Limpiar();
            registro.Mes = Convert.ToInt16(mes);
            registro.Año = Convert.ToInt16(año);
            registro.TasaIva = Basicas.parametros().TasaIva;
            Enlazar();
            this.ShowDialog();
        }
        public void Modificar()
        {
            Enlazar();
            this.ShowDialog();
        }
        private void Enlazar()
        {
            if (registro == null)
            {
                Limpiar();
            }
            this.libroVentaBindingSource.DataSource = registro;
            this.libroVentaBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                libroVentaBindingSource.EndEdit();
                registro = (LibroVenta)libroVentaBindingSource.Current;
                FactoryLibroVentas.Validar(registro);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar los datos \n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.libroVentaBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FrmLibroVentasItem_KeyDown(object sender, KeyEventArgs e)
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
