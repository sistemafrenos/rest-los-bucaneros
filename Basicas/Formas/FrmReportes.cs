using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WinForms;
using System.Data.Linq;
using HK;
using HK.Clases;
using System.Linq;

namespace HK.Formas
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FrmReportes_FormClosing);
        }
        void FrmReportes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
        public void ListadoClientes(List<Cliente> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoClientes.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Clientes", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoPlatos(List<Plato> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoPlatos.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Platos", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoPlatosIngredientes()
        {
            using (var db = new DatosEntities())
            {
                var recetas = from plato in db.Platos
                              join ingredientes in db.PlatosIngredientes on plato.IdPlato equals ingredientes.IdPlato
                              select new Receta
                              {
                                  PlatoGrupo = plato.Grupo,
                                  PlatoDescripcion = plato.Descripcion,
                                  Ingrediente = ingredientes.Ingrediente,
                                  Cantidad = ingredientes.Cantidad,
                                  CostoRacion = ingredientes.CostoRacion,
                                  Total = ingredientes.Total
                              };
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoPlatosIngredientes.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Recetas", recetas.ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void ListadoUsuarios(List<Usuario> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoUsuarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Usuarios", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoMesoneros(List<Mesonero> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoMesoneros.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Mesoneros", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoCajeros(List<Usuario> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoCajeros.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Usuarios", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoProveedor(List<Proveedore> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoProveedor.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Proveedores", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void CierreDeCaja()
        {
            FrmFechaCajero f = new FrmFechaCajero();
            f.Text = "Cierre De Caja x Usuario";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            Usuario cajero = f.cajero;
            if (cajero == null)
            {
                List<TotalxFormaPago> pagos = Basicas.VentasxLapso(f.fecha, f.fecha);
                List<VentasxPlato> listaVentasxPlato = Basicas.VentasLapsoxPlatos(f.fecha, f.fecha);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\CierreDeCaja.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", listaVentasxPlato));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxFormaPago", pagos));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.fecha, f.fecha)));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Basicas.FacturasDiariasxLapso(f.fecha, f.fecha)));
                ReportParameter pUsuario = new
                        ReportParameter("cajero", string.Format("TODOS LOS CAJEROS"));
                this.reportViewer1.LocalReport.SetParameters(pUsuario);
                double? Vales = Basicas.ValesDiariosxLapso(f.fecha, f.fecha).Sum(x => x.Monto);
                double? EfectivoEnCaja = pagos.FirstOrDefault(x => x.FormaPago == "EFECTIVO").Bolivares;
                this.reportViewer1.LocalReport.SetParameters(new
                    ReportParameter("EfectivoEnCaja", (EfectivoEnCaja.GetValueOrDefault(0) - Vales.GetValueOrDefault(0)).ToString()));
                this.reportViewer1.LocalReport.SetParameters(new
                   ReportParameter("MontoEnVales", Vales.GetValueOrDefault(0).ToString()));
                //this.reportViewer1.LocalReport.SetParameters(new
                //   ReportParameter("Detallado", f.DetallePlatos.ToString()));
                //double coeficiente = 0;
                //try
                //{
                //    coeficiente = listaVentasxPlato.Average(x => x.MontoPlatosVendidos / x.CostoPlatosVendidos);
                //} catch {}
                //this.reportViewer1.LocalReport.SetParameters(new
                //   ReportParameter("Coeficiente", coeficiente.ToString()));
            }
            else
            {
                List<TotalxFormaPago> pagos = Basicas.VentasxLapso(f.fecha, f.fecha, cajero);
                List<VentasxPlato> listaVentasxPlato = Basicas.VentasLapsoxPlatos(f.fecha, f.fecha, cajero);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\CierreDeCaja.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", listaVentasxPlato));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxFormaPago", pagos));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.fecha, f.fecha, cajero)));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Basicas.FacturasDiariasxLapso(f.fecha, f.fecha, cajero)));
                ReportParameter pUsuario = new
                        ReportParameter("cajero", string.Format("CAJERO:{0}", cajero.Nombre));
                this.reportViewer1.LocalReport.SetParameters(pUsuario);
                double? Vales = Basicas.ValesDiariosxLapso(f.fecha, f.fecha, f.cajero).Sum(x => x.Monto);
                double? EfectivoEnCaja = pagos.FirstOrDefault(x => x.FormaPago == "EFECTIVO").Bolivares;
                this.reportViewer1.LocalReport.SetParameters(new
                    ReportParameter("EfectivoEnCaja", EfectivoEnCaja.GetValueOrDefault(0).ToString()));
                this.reportViewer1.LocalReport.SetParameters(new
                   ReportParameter("MontoEnVales", Vales.GetValueOrDefault(0).ToString()));
                // this.reportViewer1.LocalReport.SetParameters(new
                //    ReportParameter("Detallado", f.DetallePlatos.ToString()));
                //double coeficiente = listaVentasxPlato.Average(x=>x.MontoPlatosVendidos/x.CostoPlatosVendidos);
                // this.reportViewer1.LocalReport.SetParameters(new
                //    ReportParameter("Coeficiente", coeficiente.ToString()));
            }
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void CierreDeCajaRestaurant()
        {
            FrmFechaCajero f = new FrmFechaCajero();
            f.Text = "Cierre De Caja x Usuario";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            Usuario cajero = f.cajero;
            using (var db = new DatosEntities())
            {
                List<MesasAbierta> MesasAbiertas = (from p in db.MesasAbiertas
                                                    orderby p.Numero
                                                    select p).ToList();
                if (cajero == null)
                {
                    List<TotalxFormaPago> pagos = Basicas.VentasxLapso(f.fecha, f.fecha);
                    List<VentasxPlato> listaVentasxPlato = Basicas.VentasLapsoxPlatos(f.fecha, f.fecha);
                    List<Factura> Facturas = FactoryFacturas.getFacturasLapso(f.fecha, f.fecha);
                    List<Factura> consumoInterno = FactoryFacturas.getConsumoLapso(f.fecha, f.fecha);
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\CierreDeCajaRestaurant.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Facturas));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MesasAbiertas", MesasAbiertas));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ConsumoInterno", consumoInterno));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", listaVentasxPlato));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxFormaPago", pagos));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.fecha, f.fecha)));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Basicas.FacturasDiariasxLapso(f.fecha, f.fecha)));
                    ReportParameter pUsuario = new
                            ReportParameter("cajero", string.Format("TODOS LOS CAJEROS"));
                    this.reportViewer1.LocalReport.SetParameters(pUsuario);
                    double? Vales = Basicas.ValesDiariosxLapso(f.fecha, f.fecha).Sum(x => x.Monto);
                    double? EfectivoEnCaja = pagos.FirstOrDefault(x => x.FormaPago == "EFECTIVO").Bolivares;
                    this.reportViewer1.LocalReport.SetParameters(new
                        ReportParameter("EfectivoEnCaja", (EfectivoEnCaja.GetValueOrDefault(0) - Vales.GetValueOrDefault(0)).ToString()));
                    this.reportViewer1.LocalReport.SetParameters(new
                       ReportParameter("MontoEnVales", Vales.GetValueOrDefault(0).ToString()));
                    //this.reportViewer1.LocalReport.SetParameters(new
                    ////   ReportParameter("Detallado", f.DetallePlatos.ToString()));
                    //double coeficiente = 0;
                    //try
                    //{
                    //    coeficiente = listaVentasxPlato.Average(x => x.MontoPlatosVendidos / x.CostoPlatosVendidos);
                    //}
                    //catch { }
                    //this.reportViewer1.LocalReport.SetParameters(new
                    //   ReportParameter("Coeficiente", coeficiente.ToString()));
                }
                else
                {
                    List<TotalxFormaPago> pagos = Basicas.VentasxLapso(f.fecha, f.fecha, cajero);
                    List<VentasxPlato> listaVentasxPlato = Basicas.VentasLapsoxPlatos(f.fecha, f.fecha, cajero);
                    List<Factura> Facturas = FactoryFacturas.getFacturasLapso(f.fecha, f.fecha, cajero);
                    List<Factura> consumoInterno = FactoryFacturas.getConsumoLapso(f.fecha, f.fecha, cajero);
                    reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\CierreDeCajaRestaurant.rdlc";
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Facturas));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("MesasAbiertas", MesasAbiertas));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ConsumoInterno", consumoInterno));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", listaVentasxPlato));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxFormaPago", pagos));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.fecha, f.fecha, cajero)));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Basicas.FacturasDiariasxLapso(f.fecha, f.fecha, cajero)));
                    ReportParameter pUsuario = new
                            ReportParameter("cajero", string.Format("CAJERO:{0}", cajero.Nombre));
                    this.reportViewer1.LocalReport.SetParameters(pUsuario);
                    double? Vales = Basicas.ValesDiariosxLapso(f.fecha, f.fecha, f.cajero).Sum(x => x.Monto);
                    double? EfectivoEnCaja = pagos.FirstOrDefault(x => x.FormaPago == "EFECTIVO").Bolivares;
                    this.reportViewer1.LocalReport.SetParameters(new
                        ReportParameter("EfectivoEnCaja", EfectivoEnCaja.GetValueOrDefault(0).ToString()));
                    this.reportViewer1.LocalReport.SetParameters(new
                       ReportParameter("MontoEnVales", Vales.GetValueOrDefault(0).ToString()));
                    //this.reportViewer1.LocalReport.SetParameters(new
                    //   ReportParameter("Detallado", f.DetallePlatos.ToString()));
                    //double coeficiente = listaVentasxPlato.Average(x => x.MontoPlatosVendidos / x.CostoPlatosVendidos);
                    //this.reportViewer1.LocalReport.SetParameters(new
                    //   ReportParameter("Coeficiente", coeficiente.ToString()));
                }
                this.reportViewer1.RefreshReport();
            }
            this.ShowDialog();
        }
        public void CierreDeCajaHoras()
        {
            FrmLapsoHoras f = new FrmLapsoHoras();
            f.Text = "Corte De Caja x Horas";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\CierreDeCajaHoras.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", Basicas.VentasHorasxPlatos(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxFormaPago", Basicas.VentasxHoras(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            ReportParameter desde = new
                    ReportParameter("desde", f.desde.ToString("t"));
            ReportParameter hasta = new
                    ReportParameter("hasta", f.hasta.ToString("t"));
            ReportParameter fecha = new
                    ReportParameter("fecha", DateTime.Today.ToString("d"));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { desde, hasta, fecha });
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void VentasxLapso()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Ventas x Lapso";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\VentasxLapso.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ComprasxLapso()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Compras x Lapso";
            f.Detallado = true;
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            List<IngredientesConsumo> lista = Basicas.ComprasxLapso(f.desde, f.hasta);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ComprasxLapso.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ingredientes", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Lapso", string.Format("Desde:{0} Hasta:{1}", f.desde.ToShortDateString(), f.hasta.ToShortDateString())));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Detallado", f.Detallado.ToString()));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ConsumoxLapso()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Ventas x Lapso";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            List<IngredientesConsumo> lista = Basicas.ConsumoPlatosxLapso(f.desde, f.hasta);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ConsumoxLapso.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Consumo", Basicas.ConsumoPlatosxLapso(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Lapso", string.Format("Desde:{0} Hasta:{1}", f.desde.ToShortDateString(), f.hasta.ToShortDateString())));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ConsumoxFecha()
        {
            FrmFechaCajero f = new FrmFechaCajero();
            f.Text = "Consumo x Fecha y Cajero";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            Usuario cajero = f.cajero;
            if (cajero == null)
            {
                List<IngredientesConsumo> lista = Basicas.ConsumoPorFecha(f.fecha);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ConsumoxLapso.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Consumo", Basicas.ConsumoPorFecha(f.fecha)));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Lapso", string.Format("Fecha:{0}", f.fecha.ToShortDateString())));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
            else
            {
                List<IngredientesConsumo> lista = Basicas.ConsumoPorFechaCajero(f.fecha, f.cajero);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ConsumoxLapso.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Consumo", lista));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Lapso", string.Format("Cajero:{0} Fecha:{1}", f.cajero.Nombre, f.fecha.ToShortDateString())));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void VentasxProducto()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Ventas x Producto";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\VentasxPlatos.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxPlato", Basicas.VentasLapsoxPlatos(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoIngredientes(List<Ingrediente> lista)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoIngredientes.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ingredientes", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoInventarios()
        {
            List<Ingrediente> lista = FactoryIngredientes.getItemsConInventario();
            lista = (from x in lista
                     orderby x.Grupo, x.Descripcion
                     select x).ToList();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoInventarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ingredientes", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("TotalInventarios", lista.Sum(x => x.CostoTotal).Value.ToString("n2")));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void IngredienteInventarios(List<IngredientesInventario> lista)
        {
            DateTime? desde = lista.Min(x => x.FechaInicio);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\IngredientesInventarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("IngredientesInventarios", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", string.Format("desde {0} hasta {1}", desde.Value.ToShortDateString(), DateTime.Today.ToShortDateString())));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ListadoMesas(List<Mesa> Lista)
        {
            if (Lista.Count < 1)
                return;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoMesas.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Mesas", Lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ReporteFacturasNaturalesyJuridicas()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Listado de Facturas Naturales y Juridicas";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            List<Factura> Lista = FactoryFacturas.getNaturalesJuridicas(f.desde, f.hasta);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ListadoFacturasNaturalesJuridicas.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Facturas", Lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        #region LibrosFiscales
        public void LibroDeVentas(List<LibroVenta> Lista, string periodo)
        {
            foreach (var item in Lista)
            {
                if (item.CedulaRif[0] == 'J' || item.CedulaRif[0] == 'G')
                {
                    item.MontoGravableContribuyentes = item.MontoGravable;
                    item.MontoIvaContribuyentes = item.MontoIva;
                    item.TasaIvaContribuyentes = item.TasaIva;
                }
                else
                {
                    item.MontoGravableNoContribuyentes = item.MontoGravable;
                    item.MontoIvaNoContribuyentes = item.MontoIva;
                    item.TasaIvaNoContribuyentes = item.TasaIva;
                }
            }
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\LibroDeVentas.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("LibroVentas", Lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", periodo));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void LibroDeCompras()
        {
            FrmMesyAño f = new FrmMesyAño();
            f.Text = "Libro de Compras";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            using (var db = new DatosEntities())
            {
                int Mes = f.fecha.Date.Month;
                int Año = f.fecha.Date.Year;
                var consulta = from q in db.LibroCompras
                               where (q.Fecha.Value.Month == Mes && q.Fecha.Value.Year == Año)
                               orderby q.Fecha
                               select q;
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\LibroDeCompras.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Compras", consulta.ToList()));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void LibroDeCompras(List<LibroCompra> Lista, string periodo)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\LibroDeCompras.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Compras", Lista.ToList()));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", periodo));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void LibroDeVentasResumido()
        {
            FrmMesyAño f = new FrmMesyAño();
            f.Text = "Libro de Ventas Resumido";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            using (var db = new DatosEntities())
            {
                int Mes = f.fecha.Date.Month;
                int Año = f.fecha.Date.Year; ;
                List<LibroVenta> libro = new List<LibroVenta>();
                var consulta = from q in db.LibroVentas
                               where (q.Mes == Mes && q.Año == Año)
                               orderby q.Factura
                               select q;
                if (consulta.FirstOrDefault() == null)
                    return;
                LibroVenta itemActual = null;
                LibroVenta itemInicial = null;
                LibroVenta ultimoItem = null;
                string Inicio = "";
                foreach (LibroVenta item in consulta)
                {
                    if (itemActual == null)
                    {
                        itemActual = item;
                        ultimoItem = item;
                        Inicio = item.Factura;
                    }
                    else
                    {
                        if (item.NumeroZ == itemActual.NumeroZ && (item.CedulaRif.Substring(0, 1) == "V" || item.CedulaRif.Substring(0, 1) == "E"))
                        {
                            itemActual.MontoIva = itemActual.MontoIva + item.MontoIva;
                            itemActual.MontoTotal = itemActual.MontoTotal + item.MontoTotal;
                            itemActual.MontoExento = itemActual.MontoExento + item.MontoExento;
                            itemActual.MontoGravable = itemActual.MontoGravable + item.MontoGravable;
                            itemActual.RazonSocial = "CONTADO";
                            itemActual.CedulaRif = "V000000000";
                            itemActual.Factura = Inicio + " " + item.Factura;
                            ultimoItem = item;
                        }
                        else
                        {
                            if (item.CedulaRif.Substring(0, 1) == "V" || item.CedulaRif.Substring(0, 1) == "E")
                            {
                                libro.Add(itemActual);
                                itemActual = item;
                                itemInicial = item;
                                ultimoItem = item;
                                Inicio = item.Factura;
                            }
                            else
                            {
                                if (item.IdLibroVentas != itemActual.IdLibroVentas)
                                {
                                    libro.Add(itemActual);
                                }
                                libro.Add(item);
                                itemActual = null;
                            }
                        }
                    }
                }
                if (itemActual!=null)
                {
                    libro.Add(itemActual);
                }
                foreach (var item in libro)
                {
                    if (item.CedulaRif[0] == 'J' || item.CedulaRif[0] == 'G')
                    {
                        item.MontoGravableContribuyentes = item.MontoGravable;
                        item.MontoIvaContribuyentes = item.MontoIva;
                        item.TasaIvaContribuyentes = item.TasaIva;
                    }
                    else
                    {
                        item.MontoGravableNoContribuyentes = item.MontoGravable;
                        item.MontoIvaNoContribuyentes = item.MontoIva;
                        item.TasaIvaNoContribuyentes = item.TasaIva;
                    }
                }
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\LibroDeVentasResumido.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("LibroVentas", libro));
                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", string.Format("Mes {0} Año {1}", Mes, Año)));
                this.reportViewer1.RefreshReport();
                this.ShowDialog();
            }
        }
        public void LibroDeInventarios(List<LibroInventario> Lista, string periodo)
        {
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\LibroDeInventarios.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("LibroInventarios", Lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Periodo", periodo));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        #endregion
        public void VentasxPorMesonero()
        {
            FrmLapso f = new FrmLapso();
            f.Detallado = false;
            f.Text = "Ventas x Lapso";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\VentasxMesonero.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VentasxDia", Basicas.VentasDiariasxLapso(f.desde, f.hasta)));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ComisionesMesonerosFecha()
        {
            FrmLapso f = new FrmLapso();
            f.Text = "Comisiones x Mesoneros";
            f.ShowDialog();
            if (f.DialogResult != System.Windows.Forms.DialogResult.OK)
                return;
            var lista = Basicas.TotalFacturasDiariasxLapso(f.desde, f.hasta);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\ComisionesMesoneros.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalxDia", lista.ToList()));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", new Parametro[] { Basicas.parametros() }));
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter("Lapso", string.Format("Desde:{0} Hasta:{1}", f.desde.ToShortDateString(), f.hasta.ToShortDateString())));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencion(Retencione item)
        {
            List<Retencione> lista = new List<Retencione>();
            using (DatosEntities d = new DatosEntities())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
            }
            List<Parametro> listaparametros = new List<Parametro>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Retencion.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencionDoble(Retencione item)
        {
            List<Retencione> lista = new List<Retencione>();
            using (DatosEntities d = new DatosEntities())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
            }
            List<Parametro> listaparametros = new List<Parametro>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\RetencionDoble.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
        public void ImprimirRetencionCH(Retencione item)
        {
            List<Retencione> lista = new List<Retencione>();
            using (DatosEntities d = new DatosEntities())
            {
                var q = from p in d.Retenciones
                        where p.NumeroComprobante == item.NumeroComprobante
                        orderby p.Id
                        select p;
                lista = q.ToList();
            }
            List<Parametro> listaparametros = new List<Parametro>();
            listaparametros.Add(Basicas.parametros());
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\RetencionCH.rdlc";
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Retencion", lista));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Parametros", listaparametros));
            this.reportViewer1.RefreshReport();
            this.ShowDialog();
        }
      }
    }

