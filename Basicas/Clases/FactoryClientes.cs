using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryClientes
    {
        public static List<string> getClientes()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Clientes
                        orderby p.RazonSocial
                        select p.RazonSocial;
                return q.ToList();
            }
        }
        public static List<Cliente> getItems(string texto)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Clientes
                        orderby p.RazonSocial
                        where (p.CedulaRif.Contains(texto) || p.RazonSocial.Contains(texto) || texto.Length == 0) 
                        select p;
                return q.ToList();
            }
        }
        public static List<Cliente> getItems(DatosEntities db, string texto)
        {
            var q = from p in db.Clientes
                    orderby p.RazonSocial
                    where (p.CedulaRif.Contains(texto) || p.RazonSocial.Contains(texto) || texto.Length == 0) 
                    select p;
            return q.ToList();
        }
        public static Cliente Item(string cedularif)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Clientes
                        orderby p.RazonSocial
                        where p.CedulaRif==cedularif
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Cliente Item(DatosEntities db,string cedularif)
        {
                var q = from p in db.Clientes
                        orderby p.RazonSocial
                        where p.CedulaRif == cedularif
                        select p;
                return q.FirstOrDefault();
        }
    }
}
