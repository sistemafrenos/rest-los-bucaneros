using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryMesoneros
    {
        public static List<string> getMesoneros()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesoneros
                        orderby p.Nombre
                        where p.Activo == true 
                        select p.Nombre;
                return q.ToList();
            }
        }
        public static List<Mesonero> getItems(string texto)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesoneros
                        orderby p.Nombre
                        where (p.Cedula.Contains(texto) || p.Nombre.Contains(texto) || texto.Length == 0) && p.Activo == true
                        select p;
                return q.ToList();
            }
        }
        public static List<Mesonero> getItems(DatosEntities db,string texto)
        {
            var q = from p in db.Mesoneros
                    orderby p.Nombre
                    where (p.Cedula.Contains(texto) || p.Nombre.Contains(texto) || texto.Length == 0) && p.Activo == true
                    select p;
            return q.ToList();
        }
        public static Mesonero Item(string ID)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesoneros
                        where p.IdMesonero == ID
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Mesonero MesoneroxCodigo(string codigo)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesoneros
                        where p.Codigo == codigo && p.Activo == true
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Mesonero ItemNombre(string nombre)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Mesoneros
                        where p.Nombre == nombre && p.Activo==true
                        select p;
                return q.FirstOrDefault();
            }
        }
    }
}
