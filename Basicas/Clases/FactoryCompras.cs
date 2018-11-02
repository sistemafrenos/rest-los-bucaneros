using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HK;

namespace HK.Clases
{
    public class FactoryCompras
    {
        public static Compra Item(string id)
        {
            using (DatosEntities db= new DatosEntities())
            {
                var item = (from x in db.Compras
                            where (x.IdCompra == id)
                            select x).FirstOrDefault();
                return item;
            }
        }
        public static Compra Item(DatosEntities db, string id)
        {
            var item = (from x in db.Compras
                        where (x.IdCompra == id)
                        select x).FirstOrDefault();
            return item;
        }
        public static List<Compra> getComprasEspera(DatosEntities db, string texto)
        {
            var mFacturas = (from x in db.Compras
                             orderby x.IdCompra
                             where (x.Estatus == "ESPERA")
                             select x).ToList();
            return mFacturas;
        }
        public static void Inventario(Compra factura)
        {
            foreach (ComprasIngrediente item in factura.ComprasIngredientes)
            {
                FactoryIngredientes.RegistrarInventario(item);
            }
        }            
        public static void InventarioDevolver(Compra factura)
        {
            foreach (ComprasIngrediente item in factura.ComprasIngredientes)
            {
                FactoryIngredientes.DevolverInventario(item);
            }
        }
        public static void PasarComprasLibro()
        {
            using (DatosEntities db = new DatosEntities())
            {
                var x = (from p in db.Compras
                        where p.IncluirLibroCompras==true && p.LibroCompras==false
                        select p).ToList();
                foreach (var item in x)
                { 
                    if(!FactoryLibroCompras.Existe(item))
                    {
                        FactoryLibroCompras.EscribirItem(item);
                    }
                }
            }
        }
    }
}

