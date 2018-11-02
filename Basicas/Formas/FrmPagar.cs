using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;
using HK.Formas;

namespace HK
{
    public partial class FrmPagar : Form
    {
        public Factura factura = new Factura();
        public Cliente cliente = new Cliente();
        public bool? cobraServicio=false;
        public double descuento = 0;
        public FrmPagar()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPagar_Load);
        }
        void txtCedulaRif_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.BuscarClientes("");
            if (F.registro != null)
            {
                cliente = (Cliente)F.registro;
                cliente = FactoryClientes.Item(cliente.CedulaRif);
                factura.CedulaRif = cliente.CedulaRif;
                LeerCliente();
            }
            else
            {
                cliente = FactoryClientes.Item(Basicas.CedulaRif("0"));
                LeerCliente();
            }
        }
        void txtCedulaRif_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            if(string.IsNullOrEmpty(Editor.Text))
                Editor.Text = Basicas.CedulaRif("0");
            string Texto = Editor.Text;            
            this.facturaBindingSource.EndEdit();
            List<Cliente> T = FactoryClientes.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    cliente = new Cliente();
                    cliente.CedulaRif = Basicas.CedulaRif(Texto);
                    break;
                case 1:
                    cliente = T[0];
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarClientes(Texto);
                    if (F.registro != null)
                    {
                        cliente = (Cliente)F.registro;
                        cliente = FactoryClientes.Item(cliente.CedulaRif);
                    }
                    else
                    {
                        cliente = FactoryClientes.Item(Basicas.CedulaRif("0"));
                    }
                    break;
            }
            LeerCliente();
        }
        private void LeerCliente()
        {
            if (cliente == null)
            {
                cliente = new Cliente();
                cliente.Direccion = Basicas.parametros().Ciudad;
                cliente.CedulaRif = Basicas.CedulaRif("0");
                cliente.RazonSocial = "CONTADO";
            }
            factura.CedulaRif = cliente.CedulaRif;
            factura.RazonSocial = cliente.RazonSocial;
            factura.Direccion = cliente.Direccion;
            this.facturaBindingSource.ResetCurrentItem();
        }
        void ConsumoInterno_Click(object sender, EventArgs e)
        {
            PagarConsumoInterno();
        }
        void Transferencia_Click(object sender, EventArgs e)
        {
            PagarTransferencia();
        }
        void Retencion_Click(object sender, EventArgs e)
        {
            PagarRetencion();
        }
        void Tarjeta_Click(object sender, EventArgs e)
        {
            PagarTarjeta();
        }
        void Cheque_Click(object sender, EventArgs e)
        {
            PagarCheque();
        }
        void Efectivo_Click(object sender, EventArgs e)
        {
            PagarEfectivo();
        }
        void TarjetaTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            factura.Tarjeta = (double)editor.Value;
            factura.TasaIva = CalcularTasaIva();
            factura.Totalizar(false, 0);
            this.facturaBindingSource.ResetCurrentItem();
        }
        void txtTransferencia_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            factura.Transferencia = (double)editor.Value;
            factura.TasaIva = CalcularTasaIva();
            factura.Totalizar(false, 0);
            this.facturaBindingSource.ResetCurrentItem();
        }
        void txtRetencion_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            factura.Retencion = (double)editor.Value;
            factura.calcularSaldo();
            this.facturaBindingSource.ResetCurrentItem();
        }
        void ChequeTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            factura.Cheque = (double)editor.Value;
            this.facturaBindingSource.ResetCurrentItem();
        }
        void EfectivoTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            factura.Efectivo = (double)editor.Value;
            factura.calcularSaldo();
            this.facturaBindingSource.ResetCurrentItem();
        }
        void Editor_Enter(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CalcEdit editor = (DevExpress.XtraEditors.CalcEdit)sender;
            editor.Value = (decimal)factura.Saldo.GetValueOrDefault();
            editor.SelectAll();
        }
        void FrmPagar_KeyDown(object sender, KeyEventArgs e)
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
                case Keys.F4:
                    PagarEfectivo();
                    e.Handled = true;
                    break;
                case Keys.F5:
                    PagarTarjeta();
                    e.Handled = true;
                    break;
                case Keys.F6:
                    PagarTransferencia();
                    e.Handled = true;
                    break;
                case Keys.F7:
                    PagarCheque();
                    e.Handled = true;
                    break;
                case Keys.F8:
                    PagarConsumoInterno();
                    e.Handled = true;
                    break;
            } 
        }
        private void PagarCheque()
        {
            factura.calcularSaldo();
            ChequeTextEdit.Value = (decimal)factura.Saldo;
            ChequeTextEdit.SelectAll();
            this.ChequeTextEdit.Focus();
        }
        private double CalcularTasaIva()
        {
            var mtasa = Basicas.parametros().TasaIva;
            return mtasa.GetValueOrDefault();
        }
        private void PagarTransferencia()
        {
            LimpiarPagos();
            factura.Transferencia = factura.MontoTotal;
            factura.TasaIva = CalcularTasaIva();
            factura.Totalizar(false, 0);
            factura.Transferencia= factura.MontoTotal;
            factura.calcularSaldo();
        }
        private void PagarRetencion()
        {
            LimpiarPagos();
            factura.Retencion =(factura.MontoIva * 0.75);
            factura.calcularSaldo();
        }
        private void PagarTarjeta()
        {
            LimpiarPagos();
            factura.Tarjeta= factura.MontoTotal;
            factura.TasaIva = CalcularTasaIva();
            factura.Totalizar(false, 0);
            factura.Tarjeta = factura.MontoTotal;
            factura.calcularSaldo();
        }
        private void PagarEfectivo()
        {
            LimpiarPagos();
            EfectivoTextEdit.Value = (decimal)factura.Saldo;
            EfectivoTextEdit.SelectAll();
            this.EfectivoTextEdit.Focus();
        }
        private void PagarConsumoInterno()
        {
            LimpiarPagos();
            factura.ConsumoInterno = factura.MontoTotal;
        }
        private void LimpiarPagos()
        {
            factura.Cheque = null;
            factura.Tarjeta = null;
            factura.Transferencia = null;
            factura.Retencion = null;
            factura.Efectivo = null;
            factura.ConsumoInterno = null;
            factura.Credito = null;
            factura.TasaIva = CalcularTasaIva();
            factura.Totalizar(cobraServicio.Value, descuento);
            factura.calcularSaldo();
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarPagos();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            facturaBindingSource.EndEdit();
            if (factura.CedulaRif == null)
            {
                MessageBox.Show("Cedula o rif no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCedulaRif.Focus();
                return;
            }
            if (factura.RazonSocial == null)
            {
                MessageBox.Show("El nombre no puede estar vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtRazonSocial.Focus();
                return;
            }
            if (factura.Efectivo.HasValue)
            {
                if (factura.Cambio.GetValueOrDefault(0) > factura.Efectivo.GetValueOrDefault(0))
                {
                    MessageBox.Show("El cambio no puede ser mayor al monto en efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                factura.Efectivo = factura.Efectivo.GetValueOrDefault(0) - factura.Cambio.GetValueOrDefault(0);
            }
            if ((factura.ConsumoInterno.HasValue && FactoryUsuarios.UsuarioActivo.PuedeDarConsumoInterno == false) || (factura.Credito.HasValue && FactoryUsuarios.UsuarioActivo.PuedeDarCreditos == false))
            {
                Usuario Autorizado = FactoryUsuarios.Item(txtAutorizadoPor.Text, txtContraseña.Text);
                if (Autorizado == null)
                {
                    MessageBox.Show("Es necesario elejir un usuario con autorizacion para otorgar el consumo interno");
                    return;
                }
                if (Autorizado.PuedeDarConsumoInterno == false)
                {
                    MessageBox.Show("El usuario elegido no tiene de autorizado el consumo interno");
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        void FrmPagar_Load(object sender, EventArgs e)
        {
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyDown += new KeyEventHandler(FrmPagar_KeyDown);
            this.EfectivoTextEdit.Enter += new EventHandler(Editor_Enter);
            this.ChequeTextEdit.Enter += new EventHandler(Editor_Enter);
            this.TarjetaTextEdit.Enter += new EventHandler(Editor_Enter);
            this.txtTransferencia.Enter += new EventHandler(Editor_Enter);
            this.EfectivoTextEdit.Validating += new CancelEventHandler(EfectivoTextEdit_Validating);
            this.ChequeTextEdit.Validating += new CancelEventHandler(ChequeTextEdit_Validating);
            this.txtTransferencia.Validating += new CancelEventHandler(txtTransferencia_Validating);
            this.txtRetencion.Validating += new CancelEventHandler(txtRetencion_Validating);
            this.TarjetaTextEdit.Validating += new CancelEventHandler(TarjetaTextEdit_Validating);
            this.Efectivo.Click += new EventHandler(Efectivo_Click);
            this.Cheque.Click += new EventHandler(Cheque_Click);
            this.Tarjeta.Click += new EventHandler(Tarjeta_Click);
            this.ConsumoInterno.Click += new EventHandler(ConsumoInterno_Click);
            this.Credito.Click += new EventHandler(Credito_Click);
            this.Transferencia.Click += new EventHandler(Transferencia_Click);
            this.Retencion.Click += new EventHandler(Retencion_Click);
            this.txtCedulaRif.Validating += new CancelEventHandler(txtCedulaRif_Validating);
            this.txtCedulaRif.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtCedulaRif_ButtonClick);
            factura.CedulaRif = Basicas.CedulaRif("0");
            factura.RazonSocial = "CONTADO";
            factura.Direccion = Basicas.parametros().Ciudad;
            this.KeyPreview = true;
            if (FactoryUsuarios.UsuarioActivo.PuedeDarConsumoInterno == true)
            {
                this.txtAutorizadoPor.Visible = false;
                this.txtContraseña.Visible = false;
            }
            factura.TasaIva = CalcularTasaIva();
            factura.Saldo = factura.MontoTotal;
            this.facturaBindingSource.DataSource = factura;
            this.facturaBindingSource.ResetBindings(true);
        }

        void Credito_Click(object sender, EventArgs e)
        {
            LimpiarPagos();
            factura.Credito = factura.MontoTotal;
        }
    }
}
