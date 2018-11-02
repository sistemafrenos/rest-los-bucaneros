using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;


namespace HK.Clases
{
    public class FactoryLibroVentas
    {
        public static List<LibroVenta> getItems(int mes, int año)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroVentas
                        orderby p.Fecha
                        where p.Mes == mes && p.Año == año
                        select p;
                return q.ToList();
            }
        }
        public static List<LibroVenta> getItems(DatosEntities db, int mes, int año)
        {
            var q = from p in db.LibroVentas
                    orderby p.Fecha
                    where p.Mes == mes && p.Año == año
                    select p;
            return q.ToList();
        }
        public static LibroVenta Item(string IdLibroVentas)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroVentas
                        where p.IdLibroVentas == IdLibroVentas
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static LibroVenta Item(DatosEntities db, string IdLibroVentas)
        {
            var q = from p in db.LibroVentas
                    where p.IdLibroVentas == IdLibroVentas
                    select p;
            return q.FirstOrDefault();
        }
        public static void Validar(LibroVenta registro)
        {
            if (string.IsNullOrEmpty(registro.CedulaRif))
                throw new Exception("Error el campo cedula Rif no puede estar vacio");
            if (registro.CedulaRif.Length > 20)
                throw new Exception("Error el campo cedula no puede tener mas de 20 caracteres");
            if (string.IsNullOrEmpty(registro.RazonSocial))
                throw new Exception("Error el razon social no puede estar vacio");
            if (registro.MontoTotal.GetValueOrDefault(0) == 0)
                throw new Exception("Error monto total no puede ser cero");
            if (string.IsNullOrEmpty(registro.RazonSocial))
                throw new Exception("Error el Nombre  no puede estar vacio");
        }
        public static void EscribirItemFactura(Factura factura)
        {
            using (var db = new DatosEntities())
            {
                try
                {
                    LibroVenta item = new LibroVenta();
                    item.Año = factura.Fecha.Value.Year;
                    item.Mes = factura.Fecha.Value.Month;
                    item.CedulaRif = factura.CedulaRif;
                 //   item.Comprobante = "";
                    item.Factura = factura.Numero;
                 //   item.FacturaAfectada = null;
                    item.Fecha = factura.Fecha;
                    item.IdLibroVentas = FactoryContadores.GetMax("idLibroVentas");
                //    item.IvaRetenido = null;
                    item.MontoExento = factura.MontoExento;
                    item.MontoGravable = factura.MontoGravable;
                    item.MontoIva = factura.MontoIva;
                    item.MontoTotal = factura.MontoTotal;
                    item.NumeroZ = factura.NumeroZ;
                    item.RazonSocial = factura.RazonSocial;
                    item.RegistroMaquinaFiscal = factura.MaquinaFiscal;
                    item.TasaIva = Basicas.parametros().TasaIva;
                    item.TipoOperacion = "01";
                    item.MontoServicio =factura.MontoServicio;
                    if (item.CedulaRif[0] == 'J' || item.CedulaRif[0] == 'G')
                    {
                        item.MontoGravableContribuyentes = item.MontoGravable;
                        item.MontoIvaContribuyentes = item.MontoIva;
                    }
                    else
                    {
                        item.MontoGravableNoContribuyentes = item.MontoGravable;
                        item.MontoIvaNoContribuyentes = item.MontoIva;
                    }
                    db.LibroVentas.AddObject(item);
                    db.SaveChanges();
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }
        public static void EscribirItemDevolucion(Factura factura,string facturaAfectada)
        {
            using (var db = new DatosEntities())
            {
                try
                {
                    LibroVenta item = new LibroVenta();
                    item.Año = factura.Fecha.Value.Year;
                    item.Mes = factura.Fecha.Value.Month;
                    item.CedulaRif = factura.CedulaRif;
                    //   item.Comprobante = "";
                    //    item.IvaRetenido = null;
                    item.Factura = factura.Numero;
                    item.Fecha = factura.Fecha;
                    item.IdLibroVentas = FactoryContadores.GetMax("idLibroVentas");
                    item.MontoExento = factura.MontoExento;
                    item.MontoGravable = factura.MontoGravable;
                    item.MontoIva = factura.MontoIva;
                    item.MontoTotal = factura.MontoTotal;
                    item.NumeroZ = factura.NumeroZ;
                    item.RazonSocial = factura.RazonSocial;
                    item.RegistroMaquinaFiscal = factura.MaquinaFiscal;
                    item.TasaIva = Basicas.parametros().TasaIva;
                    item.TipoOperacion = "02";
                    item.FacturaAfectada = facturaAfectada;
                    db.LibroVentas.AddObject(item);
                    db.SaveChanges();
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }
        public static void BorrarItem(Factura factura)
        {
            using (var db = new DatosEntities())
            {
                try
                {
                    LibroVenta item = Item(db, factura.IdFactura);
                    if (item != null)
                    {
                        db.DeleteObject(item);
                        db.SaveChanges();
                    }
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }
        public static bool Existe(Factura item)
        {
            using (var db = new DatosEntities())
            {
                var x = (from p in db.LibroVentas
                         where p.Factura == item.Numero
                         select p).FirstOrDefault();
                if (x == null)
                    return false;
                return true;
            }
        }
        }
    }

