using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{

    public class FactoryIngredientes
    {
        public static List<Ingrediente> getItems(string texto)
        {
            using (DatosEntities db= new DatosEntities())
            {
                var items = (from x in db.Ingredientes
                               orderby x.Descripcion
                               where (x.Descripcion.Contains(texto) || texto.Length == 0) && x.Activo==true
                               select x).ToList();
                return items;
            }
        }
        public static List<Ingrediente> getItemsConInventario()
        {
            using (DatosEntities db = new DatosEntities())
            {
                var items = (from x in db.Ingredientes
                               orderby x.Descripcion
                               where  x.Activo == true && x.LlevaInventario==true
                               select x).ToList();
                return items;
            }
        }
        public static List<Ingrediente> getItems(DatosEntities db, string texto)
        {
            var items = (from x in db.Ingredientes
                           orderby x.Descripcion
                           where (x.Descripcion.Contains(texto) || texto.Length == 0) && x.Activo == true
                           select x).ToList();
            return items;
        }

        public static Ingrediente Item(string id)
        {
            using (DatosEntities db= new DatosEntities())
            {
                var item = (from x in db.Ingredientes
                            where (x.IdIngrediente == id)
                            select x).FirstOrDefault();
                return item;
            }
        }
        public static Ingrediente Item(DatosEntities db, string id)
        {
            var item = (from x in db.Ingredientes
                        where (x.IdIngrediente == id)
                        select x).FirstOrDefault();
            return item;
        }

        public static object[] getArrayUnidadesMedida()
        {
            using (DatosEntities db= new DatosEntities())
            {
                var mgrupos = (from x in db.Ingredientes
                               orderby x.UnidadMedida
                               where x.UnidadMedida!=null
                               select x.UnidadMedida).Distinct();
                return mgrupos.ToArray();
            }
        }
        public static List<string> getIngredientes()
        {
            using (DatosEntities db= new DatosEntities())
            {
                var mgrupos = (from x in db.Ingredientes
                               orderby x.Descripcion
                               where x.Activo == true
                               select x.Descripcion).Distinct();
                return mgrupos.ToList();
            }
        }

        public static Ingrediente ItemxDescripcion(string texto)
        {
            using (DatosEntities db= new DatosEntities())
            {
                var item = (from x in db.Ingredientes
                            where (x.Descripcion == texto) && x.Activo == true
                            select x).FirstOrDefault();
                return item;
            }
        }

        public static object[] getArrayGrupos()
        {
            using (DatosEntities db= new DatosEntities())
            {
                var mgrupos = (from x in db.Ingredientes
                               orderby x.Grupo
                               select x.Grupo).Distinct();
                return mgrupos.ToArray();
            }
        }

        internal static void RegistrarInventario(ComprasIngrediente item)
        {
            using (DatosEntities db= new DatosEntities())
            {
            Ingrediente i = FactoryIngredientes.Item(db, item.IdIngrediente);
            if (i != null)
            {
                i.Existencia = i.Existencia.GetValueOrDefault(0) + item.CantidadNeta;
                i.Costo = item.CostoNeto;
            }
            IngredientesInventario inventario = (from x in db.IngredientesInventarios
                                                where x.IdIngrediente == item.IdIngrediente
                                                select x).FirstOrDefault();
            if (inventario == null)
            {
                IngredientesInventario nuevoItem = new IngredientesInventario();
                nuevoItem.Ajuste = 0;
                nuevoItem.Entradas = item.CantidadNeta; ;
                nuevoItem.FechaInicio = DateTime.Today;
                nuevoItem.Grupo = i.Grupo;
                nuevoItem.IdIngrediente = i.IdIngrediente;
                nuevoItem.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteInventario");
                nuevoItem.Ingrediente = i.Descripcion;
                nuevoItem.Inicio = 0;
                nuevoItem.Salidas = 0;
                nuevoItem.Final = nuevoItem.Inicio + nuevoItem.Entradas - nuevoItem.Salidas;
                nuevoItem.InventarioFisico = nuevoItem.Final;
                db.IngredientesInventarios.AddObject(nuevoItem);
            }
            else
            {
                inventario.Entradas += item.CantidadNeta;
                inventario.Final = inventario.Inicio + inventario.Entradas - inventario.Salidas;
                inventario.InventarioFisico = inventario.Final;
 
            }
            db.SaveChanges();
          }
        }
        internal static void DevolverInventario(ComprasIngrediente item)
        {
            using (DatosEntities db = new DatosEntities())
            {
                Ingrediente i = FactoryIngredientes.Item(db, item.IdIngrediente);
                if (i != null)
                {
                    i.Existencia = i.Existencia.GetValueOrDefault(0) - item.CantidadNeta;
                    i.Costo = item.CostoNeto;
                }
                IngredientesInventario inventario = (from x in db.IngredientesInventarios
                                                     where x.IdIngrediente == item.IdIngrediente
                                                     select x).FirstOrDefault();
                if (inventario != null)
                {
                    inventario.Entradas -= item.CantidadNeta;
                    inventario.Final = inventario.Inicio + inventario.Entradas - inventario.Salidas;
                    inventario.InventarioFisico = inventario.Final;
                }
                db.SaveChanges();
            }
        }
    }
}
