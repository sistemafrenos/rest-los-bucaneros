using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryMesas
    {
        public static List<string> getMesas()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesas
                        orderby p.Descripcion
                        where p.Descripcion != null
                        select p.Descripcion;
                return q.ToList();
            }
        }
        public static List<string> getUbicaciones()
        {
            using (var db = new DatosEntities())
            {
                var q = (from p in db.Mesas
                        orderby p.Ubicacion
                        where p.Ubicacion!=null
                        select p.Ubicacion).Distinct();
                return q.ToList();
            }
        }
        public static Mesa getItemxCodigo(string codigo)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesas
                        where p.IdMesa==codigo
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static List<Mesa> getItems(string texto)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesas
                        orderby p.Descripcion
                        where (p.Descripcion.Contains(texto) || p.Ubicacion.Contains(texto) || texto.Length == 0)
                        select p;
                return q.ToList();
            }
        }
        public static List<Mesa> getItems(DatosEntities db, string texto)
        {
            var q = from p in db.Mesas
                    orderby p.Descripcion
                    where (p.Descripcion.Contains(texto) || p.Ubicacion.Contains(texto) || texto.Length == 0)
                    select p;
            return q.ToList();
        }
        public static List<Mesa> getMesasDisponibles(DatosEntities db)
        {
            List<Mesa> retorno = new List<Mesa>();
            var MesasAbiertas = (from xx in db.MesasAbiertas
                                 select xx).ToList();
            var mMesas = (from x in db.Mesas
                          orderby x.Descripcion
                          select x).ToList();
            foreach (var m in mMesas)
            {
                MesasAbierta mesa = MesasAbiertas.FirstOrDefault(x => x.IdMesa == m.IdMesa);
                if (mesa == null)
                {
                    retorno.Add(m);
                }
            }
            return retorno;
        }
        public static Mesa Item(string ID)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesas
                        orderby p.Descripcion
                        where p.IdMesa == ID
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Mesa Item(DatosEntities db, string ID)
        {
                var q = from p in db.Mesas
                        orderby p.Descripcion
                        where p.IdMesa == ID
                        select p;
                return q.FirstOrDefault();
        }
        public static void Validar(Mesa registro)
        {
            if (string.IsNullOrEmpty(registro.Codigo))
                throw new Exception("Error el codigo no puede estar vacio");
            if (registro.Codigo.Length > 20)
                throw new Exception("Error codigo no puede tener mas de 20 caracteres");
            if (string.IsNullOrEmpty(registro.Ubicacion))
                throw new Exception("Error la Ubicacion  no puede estar vacia");
            if (registro.Ubicacion.Length > 100)
                throw new Exception("Error la ubicacion no puede tener mas de 100 caracteres");
            if (string.IsNullOrEmpty(registro.Descripcion))
                throw new Exception("Error la Descripcion no puede estar vacia");
            if (registro.Descripcion.Length > 100)
                throw new Exception("Error Descripcion no puede tener mas de 100 caracteres");
        }
        public static MesasAbierta MesaAbiertaItem(DatosEntities db, MesasAbierta mesasAbierta )
        {
                 var q = from p in db.MesasAbiertas
                        where p.IdMesaAbierta == mesasAbierta.IdMesaAbierta
                        select p;
                return q.FirstOrDefault();
        }
        public static MesasAbierta MesaAbierta(DatosEntities db, Mesa mesa)
        {
            var q = from p in db.MesasAbiertas
                    where p.IdMesa == mesa.IdMesa
                    select p;
            return q.FirstOrDefault();
        }

        public static Mesa ItemDescripcion(string descripcion)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesas
                        where p.Descripcion == descripcion
                        select p;
                return q.FirstOrDefault();
            }
        }
    }
}
