using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using HK;

//namespace HK
//{
//    public partial class Traslado : EntityObject
//    {
//        public void Totalizar()
//        {
//            this.MontoTotal = (double)decimal.Round((decimal)this.TrasladosIngredientes.Sum(x => x.Cantidad * x.CostoIva));
//            this.MontoExento = (double)decimal.Round((decimal)this.TrasladosIngredientes.Where(x => x.TasaIva.GetValueOrDefault(0) == 0).Sum(x => x.Cantidad * x.CostoIva));
//            this.MontoGravable = (double)decimal.Round((decimal)this.TrasladosIngredientes.Where(x => x.TasaIva.GetValueOrDefault(0) > 0).Sum(x => x.Cantidad * x.Costo));
//            this.MontoIva = (double)decimal.Round((decimal)this.MontoTotal.GetValueOrDefault(0) - (decimal)this.MontoGravable.GetValueOrDefault(0) - (decimal)this.MontoExento.GetValueOrDefault(0), 2);
//        }
//    }
//}
namespace HK.Clases
{
    public class FactoryTraslados
    {
        public static Traslado Item(string id)
        {
            using (FeriaEntities db= new FeriaEntities())
            {
                var item = (from x in db.Traslados
                            where (x.IdTraslado == id)
                            select x).FirstOrDefault();
                return item;
            }
        }
        public static Traslado Item(FeriaEntities db, string id)
        {
            var item = (from x in db.Traslados
                        where (x.IdTraslado == id)
                        select x).FirstOrDefault();
            return item;
        }
    
        public static void Validar(Traslado registro)
        {
            if( registro.TrasladosIngredientes.Count==0)
            {
                throw new Exception("Traslado no tiene elementos");
            }
            if (string.IsNullOrEmpty(registro.Destino))
            {
                throw new Exception("Destino invalido");
            }
            if (string.IsNullOrEmpty(registro.Comentarios))
            {
                throw new Exception("Por favor escriba un comentario de este traslado");
            }
        }

        public static void Inventario(Traslado factura)
        {
            using (FeriaEntities db = new FeriaEntities())
            {
                foreach (TrasladosIngrediente item in factura.TrasladosIngredientes)
                    {
                    IngredientesInventario q = (from p in db.IngredientesInventarios
                            where p.Fecha == factura.FechaInventario && p.IdIngrediente == item.IdIngrediente
                            select p).FirstOrDefault();
                            if(q==null)
                            {
                                IngredientesInventario ant = (from p in db.IngredientesInventarios
                                                              where p.Fecha < factura.FechaInventario && p.IdIngrediente == item.IdIngrediente
                                                            select p).FirstOrDefault();

                                q = new IngredientesInventario();
                                q.IdIngrediente = item.IdIngrediente;
                                q.Fecha = factura.FechaInventario;
                                q.Inicio = ant==null?0: ant.InventarioFisico;
                                q.Entradas = 0;
                                q.Salidas = 0;
                                q.Final = 0;
                                q.InventarioFisico = 0;
                                q.Ajuste = 0;
                            }
                            q.Entradas+= item.Cantidad;
                            q.Final =  q.Entradas+q.Inicio-q.Salidas;
                            q.InventarioFisico = q.Final;
                            q.Ajuste = 0;
                            if (q.IdIngredienteInventario == null)
                            {
                                q.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteInventario");
                                db.IngredientesInventarios.AddObject(q);
                            }

                            var ingr = (from prod in db.Ingredientes
                                        where prod.IdIngrediente == item.IdIngrediente
                                        select prod).FirstOrDefault();
                            if (ingr != null)
                            {
                                ingr.Existencia = q.Final;
                            }
                            db.SaveChanges();
                    }
                factura.ActualizadoInventario = true;
                db.SaveChanges();
                }
            }
        public static void InventarioDevolver(Traslado factura)
        {
            using (FeriaEntities db = new FeriaEntities())
            {
                foreach (TrasladosIngrediente item in factura.TrasladosIngredientes)
                {
                    IngredientesInventario q = (from p in db.IngredientesInventarios
                                                where p.Fecha == factura.Fecha && p.IdIngrediente == item.IdIngrediente
                                                select p).FirstOrDefault();
                    if (q != null)
                    {
                        q.Entradas -= item.Cantidad;
                        q.Final = q.Entradas + q.Inicio - q.Salidas;
                        q.InventarioFisico = q.Final;
                        q.Ajuste = 0;
                    }
                }
                db.SaveChanges();
            }
        }
   }
}

