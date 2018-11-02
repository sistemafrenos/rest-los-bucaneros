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
    public partial class FrmFacturas : Form
    {
        DatosEntities db = new DatosEntities();
        List<Factura> Lista = new List<Factura>();
        public FrmFacturas()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmFacturas_Load);
        }
        void FrmFacturas_Load(object sender, EventArgs e)
        {
            Busqueda();
            Buscar.Click += new EventHandler(Buscar_Click);
            gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            VerFactura.Click += new EventHandler(VerFactura_Click);
            Eliminar.Click += new EventHandler(Eliminar_Click);
            txtBuscar.KeyDown += new KeyEventHandler(txtBuscar_KeyDown);
            Duplicar.Click += new EventHandler(Duplicar_Click);
            this.FormClosed += new FormClosedEventHandler(FrmFacturasLista_FormClosed);
            gridView1.OptionsLayout.Columns.Reset();
            this.Height = Screen.GetBounds(this).Height - 50;
            this.Width = Screen.GetBounds(this).Width - 50;
            this.CenterToScreen();
        }        
        void VerFactura_Click(object sender, EventArgs e)
        {
            if (this.bs.Current == null)
                return;
            Factura documento = (Factura)this.bs.Current;
            FrmFacturasItem f = new FrmFacturasItem();
            f.factura = documento;
            f.Ver();
        }
        void FrmFacturasLista_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.Facturaslista = null;
        }        
        private void Busqueda()
        {
            db = new DatosEntities();
            var clave = Basicas.parametros().Clave;
            switch (txtFiltro.Text)
            {
                case "TODAS":
                    Lista = (from p in db.Facturas
                             where txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO"
                             orderby p.Fecha
                             select p).ToList();
                    break;
                case "AYER":
                    DateTime ayer = DateTime.Today.AddDays(-1);
                    Lista = (from p in db.Facturas
                             where (txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO") && p.Fecha.Value == ayer
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "HOY":
                    Lista = (from p in db.Facturas
                             where (txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO") && p.Fecha.Value == DateTime.Today
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "ESTA SEMANA":
                    DateTime estaSemana = DateTime.Today.AddDays(-7);
                    Lista = (from p in db.Facturas
                             where (txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO") && p.Fecha.Value >= estaSemana
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "ESTE MES":
                    Lista = (from p in db.Facturas
                             where (txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO") && p.Fecha.Value.Month == DateTime.Today.Month && p.Fecha.Value.Year == DateTime.Today.Year
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "ESTE AÑO":
                    Lista = (from p in db.Facturas
                             where (txtBuscar.Text.ToUpper() != clave ? (p.Tipo == "FACTURA" || p.Tipo == "DEVOLUCION") : p.Tipo == "CONSUMO") && p.Fecha.Value.Year == DateTime.Today.Year
                             orderby p.Numero
                             select p).ToList();
                    break;
            }
            this.colConsumoInterno.Visible = (txtBuscar.Text.ToUpper() == clave);
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void DuplicarRegistro()
        {
            if (this.bs.Current == null)
                return;
            Factura documento = (Factura)this.bs.Current;
            try
            {
                Fiscal f = new Fiscal();
                f.ImprimeFacturaCopia(documento.Numero);
                f = null;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        private void EliminarRegistro()
        {
            if (this.bs.Current == null)
                return;
            Factura documento = (Factura)this.bs.Current;
            string FacturaAfectada = documento.Numero;
            if (documento.Anulado.GetValueOrDefault(false) == true)
            {
                if (MessageBox.Show("Esta operacion ya fue devuelta,Desea realizar la devolucion de nuevo", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }
            try
            {
                Fiscal f = new Fiscal();
                f.ImprimeDevolucion(documento);
                Factura Devolucion = new Factura();
                Devolucion.Cajero = documento.Cajero;
                Devolucion.CedulaRif = documento.CedulaRif;
                Devolucion.Transferencia = documento.Transferencia*-1;
                Devolucion.Retencion = documento.Retencion * -1;
                Devolucion.Cheque = documento.Cheque*-1;
                Devolucion.ConsumoInterno = documento.ConsumoInterno*-1;
                Devolucion.Direccion = documento.Direccion;
                Devolucion.Efectivo = documento.Efectivo*-1;
                Devolucion.Tarjeta = documento.Tarjeta * -1;
                Devolucion.Email = documento.Email;
                Devolucion.Fecha = DateTime.Today;
                Devolucion.LibroVentas = true;
                Devolucion.Hora = null;
                Devolucion.IdCajero = documento.IdCajero;
                Devolucion.MaquinaFiscal = documento.MaquinaFiscal;
                Devolucion.MontoExento = documento.MontoExento*-1;
                Devolucion.MontoGravable = documento.MontoGravable*-1;
                Devolucion.MontoIva = documento.MontoIva*-1;
                Devolucion.MontoTotal = documento.MontoTotal*-1;
                Devolucion.Numero = documento.Numero;
                Devolucion.NumeroZ = documento.NumeroZ;
                Devolucion.RazonSocial = documento.RazonSocial;
                Devolucion.TasaIva = documento.TasaIva;
              //  Devolucion.Numero = f.UltimaDevolucion;
                Devolucion.Tipo = "DEVOLUCION";
                //using (var db = new DatosEntities())
                //{
                //    db.Facturas.AddObject(Devolucion);
                //    db.SaveChanges();
                //}
                FactoryLibroVentas.EscribirItemDevolucion(Devolucion,documento.Numero);
                FactoryFacturas.DevolverInventario(documento);
                using (var db = new DatosEntities())
                {
                    Devolucion.IdFactura = FactoryContadores.GetMax("IdFactura");
                    db.Facturas.AddObject(Devolucion);
                    db.SaveChanges();
                }
                f = null;
            }
            catch (Exception x)
            {
                Basicas.ManejarError(x);
            }
            Mesa mesa = FactoryMesas.ItemDescripcion(documento.Mesonero);
            MesasAbierta mesaAbierta = new MesasAbierta();
            mesaAbierta.Apertura = DateTime.Now;
            mesaAbierta.Estatus = "ABIERTA";
            mesaAbierta.Mesa = documento.Mesonero;
            mesaAbierta.Numero = "F";
            mesaAbierta.Personas = 1;
            mesaAbierta.IdMesaAbierta = FactoryContadores.GetMax("IdMesaAbierta");
            mesaAbierta.IdMesa = mesa.IdMesa;
            foreach (var item in documento.FacturasPlatos)
            {
                MesasAbiertasPlato p = new MesasAbiertasPlato();
                p.Cantidad = item.Cantidad;
                p.Codigo = item.Codigo;
                p.Comentarios = item.Comentarios;
                p.Contornos = item.Contornos;
                p.Costo = item.Costo;
                p.Descripcion = item.Descripcion;
              //  p.EnviarComanda = item.e
                p.Grupo = item.Grupo;
                p.IdMesaAbiertaPlato = FactoryContadores.GetMax("IdMesaAbiertaPlato");
                p.Idplato = item.Idplato;
                p.Precio = item.Precio;
                p.PrecioConIva = item.PrecioConIva;
                p.TasaIva = item.TasaIva;
                p.Total = item.Total;
                p.TotalBase = item.Cantidad * item.Precio;
                mesaAbierta.MesasAbiertasPlatos.Add(p);
            }
            using (var db = new DatosEntities())
            {
                mesaAbierta.Totalizar(mesa.CobraServicio.Value,mesaAbierta.MesasAbiertasPlatos.ToList(),0);
                db.MesasAbiertas.AddObject(mesaAbierta);                
                db.SaveChanges();
            }
        }
        #region Eventos
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        VerFactura.PerformClick();
                        break;
                    case Keys.Delete:
                        Eliminar.PerformClick();
                        break;
                    case Keys.Subtract:
                        Eliminar.PerformClick();
                        break;
                    case Keys.P:
                        Duplicar.PerformClick();
                        break;
                }
            }
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }
        private void Duplicar_Click(object sender, EventArgs e)
        {
            DuplicarRegistro();
        }
        #endregion
    }
}
