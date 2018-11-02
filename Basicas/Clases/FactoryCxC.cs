using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;

namespace HK.Clases
{
    public class FactoryCxC
    {
        public static void CrearCxC(Factura factura)
        {
            //using (var db = new DatosEntities())
            //{
            //    ClientesMovimiento cxc = new ClientesMovimiento();
            //    Cliente cliente = FactoryClientes.Item(factura.CedulaRif);
            //    cxc.Año = factura.Fecha.Value.Year;
            //    cxc.CedulaRif = factura.CedulaRif;
            //    cxc.Concepto = string.Format("FACTURA {0}", factura.Numero);
            //    cxc.Debito = factura.Credito;
            //    cxc.Fecha = factura.Fecha;
            //    cxc.IdUsuario = FactoryUsuarios.UsuarioActivo.IdUsuario;
            //    cxc.Mes = factura.Fecha.Value.Month;
            //    cxc.Monto = factura.Credito;
            //    cxc.RazonSocial = factura.RazonSocial;
            //    cxc.Saldo = factura.Credito;
            //    cxc.Vence =cxc.Fecha.Value.AddDays(cliente==null?30:cliente.DiasCredito.GetValueOrDefault(0));
            //    cxc.Tipo = "FACTURA";
            //    cxc.IdClienteMovimiento = FactoryContadores.GetMax("IdClienteMovimiento");
            //    db.ClientesMovimientos.AddObject(cxc);
            //    db.SaveChanges();
            //}
        }
    }
}
