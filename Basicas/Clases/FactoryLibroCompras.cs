using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;

namespace HK.Clases
{
    public class FactoryLibroCompras
    {
        public static List<LibroCompra> getItems(int mes, int año)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroCompras
                        orderby p.Fecha
                        where p.Mes == mes && p.Año == año
                        select p;
                return q.ToList();
            }
        }
        public static List<LibroCompra> getItems(DatosEntities db, int mes, int año)
        {
            var q = from p in db.LibroCompras
                    orderby p.Fecha
                    where p.Mes == mes && p.Año == año
                    select p;
            return q.ToList();
        }
        public static LibroCompra Item(string IdLibroCompras)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroCompras
                        where p.IdLibroCompras == IdLibroCompras
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static LibroCompra Item(DatosEntities db, string IdLibroCompras)
        {
            var q = from p in db.LibroCompras
                    where p.IdLibroCompras == IdLibroCompras
                    select p;
            return q.FirstOrDefault();
        }
        public static void EscribirItem(Compra factura)
        {
            if (factura.IncluirLibroCompras.GetValueOrDefault(false) == false)
                return;
            using (var db = new DatosEntities())
            {
                try
                {
                    LibroCompra item = new LibroCompra();
                    item.Año = factura.Año;
                    item.CedulaRif = factura.CedulaRif;
                    item.Fecha = factura.Fecha;
                    item.Mes = factura.Mes;
                    item.MontoExento = factura.MontoExento;
                    item.MontoGravable = factura.MontoGravable;
                    item.MontoIva = factura.MontoIva;
                    item.MontoTotal = factura.MontoTotal;
                    item.Numero = factura.Numero;
                    item.NumeroControl = factura.NumeroControl;
                    item.RazonSocial = factura.RazonSocial;
                    item.TasaIva = factura.TasaIva;
                    item.IdLibroCompras = FactoryContadores.GetMax("idLibroCompras");
                    db.LibroCompras.AddObject(item);
                    var compra = (from c in db.Compras
                                 where c.IdCompra == factura.IdCompra
                                 select c).FirstOrDefault();
                    if (compra != null)
                    {
                        compra.LibroCompras = true;
                    }
                    db.SaveChanges();
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }
        public static void BorrarItem(Compra compra)
        {
            using (var db = new DatosEntities())
            {
                try
                {
                    LibroCompra item = (from p in db.LibroCompras
                                       where p.Numero == compra.Numero && p.CedulaRif == compra.CedulaRif
                                       select p).FirstOrDefault();
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
        public  static bool Existe(Compra item)
        {
            using (var db = new DatosEntities())
            {
                var x = (from p in db.LibroCompras
                         where p.Numero == item.Numero && p.CedulaRif == item.CedulaRif
                         select p).FirstOrDefault();
                if (x == null)
                    return false;
                return true;
            }
        }
    }
}
