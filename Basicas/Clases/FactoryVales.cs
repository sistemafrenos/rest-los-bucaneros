using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryVales
    {
        public static List<string> getConceptos()
        {
            using (var db = new DatosEntities())
            {
                var q = (from p in db.Vales
                        orderby p.Concepto
                        where p.Concepto != null
                        select p.Concepto).Distinct();
                return q.ToList();
            }
        }
        public static List<Vale> getItems(DatosEntities db, string idCajero, DateTime fecha)
        {
            var q = from p in db.Vales
                    orderby p.Concepto
                    where (p.IdCajero == idCajero && p.Fecha == fecha)
                    select p;
            return q.ToList();
        }
        public static List<Vale> getItems(DatosEntities db, DateTime fecha)
        {
            var q = from p in db.Vales
                    orderby p.Concepto
                    where (p.Fecha == fecha)
                    select p;
            return q.ToList();
        }
        public static Vale Item(string ID)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Vales
                        where p.IdVale == ID
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Vale Item(DatosEntities db, string ID)
        {
                var q = from p in db.Vales
                        where p.IdVale == ID
                        select p;
                return q.FirstOrDefault();
        }
        public static void Validar(Vale registro)
        {
            if (string.IsNullOrEmpty(registro.Concepto))
                throw new Exception("Error el concepto no puede estar vacio");
            if (registro.Concepto.Length > 100)
                throw new Exception("Error codigo no puede tener mas de 20 caracteres");
        }
    }
}
