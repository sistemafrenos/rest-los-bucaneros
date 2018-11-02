using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    class FactoryMesasAbiertas
    {
        public static List<MesasAbierta> getMesasAbiertas()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.MesasAbiertas
                        orderby p.Numero
                        select p;
                return q.ToList();
            }
        }
    }
}
