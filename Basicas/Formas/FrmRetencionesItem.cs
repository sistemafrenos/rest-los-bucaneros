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
    public partial class FrmRetencionesItem : Form
    {
        public FrmRetencionesItem()
        {
            InitializeComponent();
            this.Load+=new EventHandler(FrmRetencionesItem_Load);
        }
        public Proveedore proveedor = new Proveedore();
        public Retencione registro = new Retencione();
        string Mes="";
        string Año="";
        string Periodo = "";
        private void FrmRetencionesItem_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Frm_KeyDown);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.BaseImponibleCalcEdit.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.MontoExentoIvaCalcEdit.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.MontoIvaCalcEdit.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.AlicuotaCalcEdit.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.txtPorcentajeRetencion.Validating += new CancelEventHandler(BaseImponibleCalcEdit_Validating);
            this.CedulaRifTextEdit.Validating += new CancelEventHandler(CedulaRifTextEdit_Validating);
            this.CedulaRifTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CedulaRifTextEdit_ButtonClick);
            this.NumeroDocumentoTextEdit.TextChanged += new EventHandler(registroForNumeroDocumento_TextChanged);
        }
        void registroForNumeroDocumento_TextChanged(object sender, EventArgs e)
        {
            this.retencioneBindingSource.EndEdit();
            registro.NumeroDocumentoAfectado = registro.NumeroDocumento;
        }

        void CedulaRifTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades F = new FrmBuscarEntidades();
            F.BuscarProveedores("");
            if (F.registro != null)
            {
                registro.CedulaRif = ((Proveedore)F.registro).CedulaRif;
                registro.NombreRazonSocial = ((Proveedore)F.registro).RazonSocial;
            }
            else
            {
                registro.CedulaRif = null;
                registro.NombreRazonSocial = null;
            }
        }

        void CedulaRifTextEdit_Validating(object sender, CancelEventArgs e)
        {
            DevExpress.XtraEditors.TextEdit Editor = (DevExpress.XtraEditors.TextEdit)sender;
            if (!Editor.IsModified)
                return;
            string Texto = Editor.Text;
            List<Proveedore> T = FactoryProveedores.getItems(Texto);
            switch (T.Count)
            {
                case 0:
                    Editor.Text = Basicas.CedulaRif(Texto);
                    break;
                case 1:
                    registro.CedulaRif = T[0].CedulaRif;
                    registro.NombreRazonSocial = T[0].RazonSocial;
                    break;
                default:
                    FrmBuscarEntidades F = new FrmBuscarEntidades();
                    F.BuscarProveedores(Texto);
                    if (F.registro != null)
                    {
                        registro.CedulaRif = ((Proveedore)F.registro).CedulaRif;
                        registro.NombreRazonSocial = ((Proveedore)F.registro).RazonSocial;
                    }
                    else
                    {
                        registro.CedulaRif = null;
                        registro.NombreRazonSocial = null;
                    }
                    break;
            }
        }
        void BaseImponibleCalcEdit_Validating(object sender, CancelEventArgs e)
        {
            retencioneBindingSource.EndEdit();
            Calcular();
        }
        private void Limpiar()
        {
            registro = new Retencione();
            registro.FechaDocumento = DateTime.Today;
            registro.FechaComprobante = DateTime.Today;
            registro.Alicuota = Basicas.parametros().TasaIva;
            registro.BaseImponible = 0;
            registro.MontoExentoIva = 0;
            registro.MontoIvaRetenido = 0;
            registro.NumeroComprobante = FactoryContadores.GetMaxComprobante(Mes, Año);
            registro.Id = FactoryContadores.GetMax("ID");
            registro.MontoDocumento = 0;
            registro.PeriodoImpositivo = Año + Mes;
            registro.RifAgenteRetencion = Basicas.parametros().EmpresaRif;
            registro.TipoDocumento = "01";
            registro.TipoOperacion = "C";
            registro.NumeroExpediente = "0";
            registro.NumeroDocumentoAfectado = "0";
            registro.PorcentajeRetencion = Basicas.parametros().PorcentajeRetencion;
            if (DateTime.Today.Date.Day < 15)
            {
                registro.Periodo = "PRIMER PERIODO";
            }
            else
            {
                registro.Periodo = "SEGUNDO PERIODO";
            }
        }
        public void Incluir(string mes,string año)
        {
            Mes = mes;
            Año = año;
            Limpiar();
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
            this.retencioneBindingSource.DataSource = registro;
            this.retencioneBindingSource.ResetBindings(true);
        }
        private void Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                retencioneBindingSource.EndEdit();
                Calcular();
                if (string.IsNullOrEmpty(registro.NumeroComprobante))
                    throw new Exception("Error el numero de comprobante no puede estar vacio");
                if (string.IsNullOrEmpty(registro.NumeroControlDocumento))
                    throw new Exception("Error el numero de control no puede estar vacio");
                if (string.IsNullOrEmpty(registro.NumeroDocumento))
                    throw new Exception("Error el numero de documento no puede estar vacio");
                if (string.IsNullOrEmpty(registro.NumeroExpediente))
                    registro.NumeroExpediente = "0";
                registro = (Retencione)retencioneBindingSource.Current;
                this.DialogResult = DialogResult.OK;
                try
                {
                    string num = registro.NumeroComprobante.Substring(6, 8);
                    int numero = Convert.ToInt32(num);
                    FactoryContadores.SetMaxComprobante(Mes, Año, numero);
                }
                catch 
                { 
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos \n" + ex.Source + "\n" + ex.Message, "Atencion", MessageBoxButtons.OK);
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.retencioneBindingSource.ResetCurrentItem();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
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
        private void Calcular()
        {
            registro.MontoIva = registro.BaseImponible.GetValueOrDefault(0) * registro.Alicuota.GetValueOrDefault(0) / 100;
            registro.MontoIvaRetenido = registro.MontoIva * registro.PorcentajeRetencion/100;
            registro.MontoDocumento = registro.BaseImponible.GetValueOrDefault(0) + registro.MontoExentoIva.GetValueOrDefault(0) + registro.MontoIva.GetValueOrDefault(0);
        }

        internal void CrearRetencion(Compra compra)
        {
            Mes = compra.Mes.Value.ToString("00");
            Año = compra.Año.Value.ToString("00");
            Limpiar();
            registro.Alicuota = compra.TasaIva;
            registro.BaseImponible = compra.MontoGravable;
            registro.CedulaRif = compra.CedulaRif;
            registro.FechaComprobante = DateTime.Today;
            registro.FechaDocumento = compra.Fecha;
            registro.MontoDocumento = compra.MontoTotal;
            registro.MontoIva = compra.MontoIva;
            registro.NombreRazonSocial = compra.RazonSocial;
            registro.NumeroControlDocumento = compra.NumeroControl;
            registro.NumeroDocumento = compra.Numero;
            registro.PeriodoImpositivo = compra.Año.Value.ToString("00") + compra.Mes.Value.ToString("00");
            registro.PorcentajeRetencion = Basicas.parametros().PorcentajeRetencion;
            registro.RifAgenteRetencion = Basicas.CedulaRif( Basicas.parametros().EmpresaRif);
            registro.TipoDocumento = "01";
            registro.TipoOperacion = "C";
            if (compra.Fecha.Value.Day < 15)
            {
                registro.Periodo = "PRIMER PERIODO";
            }
            else
            {
                registro.Periodo = "SEGUNDO PERIODO";
            }
            Calcular();
            Enlazar();
            this.ShowDialog();
        }
    }
}
