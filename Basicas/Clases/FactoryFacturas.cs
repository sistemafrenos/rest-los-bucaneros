using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;


namespace HK.Clases
{

    public class FactoryFacturas
    {
        public static Factura Item(string id)
        {
            using (DatosEntities db= new DatosEntities())
            {
                var item = (from x in db.Facturas
                            where (x.IdFactura == id)
                            select x).FirstOrDefault();
                return item;
            }
        }
        public static Factura Item(DatosEntities db, string id)
        {
            var item = (from x in db.Facturas
                        where (x.IdFactura == id)
                        select x).FirstOrDefault();
            return item;
        }
        public static List<Factura> getFacturasPendientes(DatosEntities db, string texto)
        {
            var mFacturas = (from x in db.Facturas
                           orderby x.IdFactura
                           where (x.Tipo=="PENDIENTE")
                           select x).ToList();
            return mFacturas;
        }
        public static List<Factura> getFacturasLapso( DateTime Desde, DateTime Hasta)
        {
            using (DatosEntities db = new DatosEntities())
            {
                var mFacturas = (from x in db.Facturas
                                 orderby x.Numero
                                 where (x.Tipo == "FACTURA")
                                 where x.Fecha >= Desde && x.Fecha<= Hasta
                                 select x).ToList();
                return mFacturas.ToList();
            }
        }
        public static List<Factura> getConsumoLapso(DateTime Desde, DateTime Hasta)
        {
            using (DatosEntities db = new DatosEntities())
            {
                var mFacturas = (from x in db.Facturas
                                 orderby x.Numero
                                 where (x.Tipo == "CONSUMO")
                                 where x.Fecha >= Desde && x.Fecha <= Hasta
                                 select x).ToList();
                return mFacturas;
            }
        }
        public static List<Factura> getConsumoLapso(DateTime Desde, DateTime Hasta, Usuario cajero)
        {
            using (DatosEntities db = new DatosEntities())
            {
                var mFacturas = (from x in db.Facturas
                                 where x.IdCajero == cajero.IdUsuario && x.Fecha >= Desde && x.Fecha<= Hasta
                                 where (x.Tipo == "CONSUMO")
                                 orderby x.Numero
                                 select x).ToList();
                return mFacturas;
            }
        }
        public static List<Factura> getFacturasLapso(DateTime Desde, DateTime Hasta, Usuario cajero)
        {
            using (DatosEntities db = new DatosEntities())
            {
                var mFacturas = (from x in db.Facturas
                                 where x.IdCajero == cajero.IdUsuario && x.Fecha >= Desde && x.Fecha <= Hasta
                                 where (x.Tipo == "CONSUMO")
                                 orderby x.Numero
                                 select x).ToList();
                return mFacturas;
            }
        }
        public static List<Factura> getNaturalesJuridicas(DateTime inicio,DateTime final)
        {
            using (DatosEntities db = new DatosEntities())
            {
            var mFacturas = (from x in db.Facturas
                             orderby  x.Numero 
                             where (x.Fecha>=inicio && x.Fecha<=final )
                             select x).ToList();
            return mFacturas;
           }
        }
        public static void DescontarInventario(Factura factura)
        {
            using (DatosEntities db = new DatosEntities())
            {
                factura = (from x in db.Facturas
                           where x.IdFactura == factura.IdFactura
                           select x).FirstOrDefault();
                foreach (FacturasPlato plato in factura.FacturasPlatos)
                {
                    foreach(PlatosCombo p in db.PlatosCombos.Where(x=>x.IdPlato==plato.Idplato))
                    {
                        Plato platoDescontar=(from x in db.Platos 
                                                where x.IdPlato==p.IdPlato
                                                select x).FirstOrDefault();
                        DescontarPlato(platoDescontar,p.Cantidad.GetValueOrDefault(0)*plato.Cantidad.GetValueOrDefault(0));
                    }
                    Plato platoDescontar2=(from x in db.Platos 
                        where x.IdPlato==plato.Idplato
                        select x).FirstOrDefault();
                    DescontarPlato(platoDescontar2, plato.Cantidad.GetValueOrDefault(0));
                }
                factura.Inventarios = true;
                db.SaveChanges();
            }
        }
        public static void DescontarPlato(Plato plato,double cantidad)
        {
            if (plato == null)
                return;
            using (DatosEntities db = new DatosEntities())
            {
                List<PlatosIngrediente> ingredientes = FactoryPlatos.getIngredientes(plato.IdPlato);
                foreach (PlatosIngrediente ingrediente in ingredientes)
                {
                    IngredientesInventario item = (from x in db.IngredientesInventarios
                                                    where x.IdIngrediente == ingrediente.IdIngrediente
                                                    select x).FirstOrDefault();
                    if (item == null)
                    {
                        string Grupo = (from x in db.Ingredientes
                                        where x.IdIngrediente == ingrediente.IdIngrediente
                                        select x.Grupo).FirstOrDefault();
                        item = new IngredientesInventario();
                        item.IdIngrediente = ingrediente.IdIngrediente;
                        item.Ingrediente = ingrediente.Ingrediente;
                        item.FechaInicio = DateTime.Today;
                        item.Ajuste = 0;
                        item.Entradas = 0;
                        item.Final = 0;
                        item.Grupo = Grupo;
                        item.Inicio = 0;
                        item.Salidas = 0;
                    }
                    item.Salidas = item.Salidas + (ingrediente.Cantidad * cantidad);
                    item.Final = item.Inicio + item.Entradas - item.Salidas;
                    item.InventarioFisico = item.Final;
                    item.Ajuste = 0;
                    if (item.IdIngredienteInventario == null)
                    {
                        item.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteInventario");
                        db.IngredientesInventarios.AddObject(item);
                    }
                    db.SaveChanges();
                }
            }
        }
        public static void DevolverInventario(Factura factura)
        {
            foreach (FacturasPlato plato in factura.FacturasPlatos)
            {
                List<PlatosIngrediente> ingredientes = FactoryPlatos.getIngredientes(plato.Idplato);
                foreach (PlatosIngrediente ingrediente in ingredientes)
                {
                    using (DatosEntities db = new DatosEntities())
                    {
                        IngredientesInventario item = (from x in db.IngredientesInventarios
                                                       where x.IdIngrediente == ingrediente.IdIngrediente
                                                       select x).FirstOrDefault();
                        if (item == null)
                        {
                            string Grupo = (from x in db.Ingredientes
                                            where x.IdIngrediente == ingrediente.IdIngrediente
                                            select x.Grupo).FirstOrDefault();
                            item = new IngredientesInventario();
                            item.IdIngrediente = ingrediente.IdIngrediente;
                            item.Ingrediente = ingrediente.Ingrediente;
                            item.FechaInicio = factura.Fecha;
                            item.Ajuste = 0;
                            item.Entradas = 0;
                            item.Final = 0;
                            item.Grupo = Grupo;
                            item.Inicio = 0;
                            item.Salidas = 0;
                        }
                        item.Salidas = item.Salidas - (ingrediente.Cantidad * plato.Cantidad);
                        item.Final = item.Inicio + item.Entradas - item.Salidas;
                        item.InventarioFisico = item.Final;
                        item.Ajuste = 0;
                        if (item.IdIngredienteInventario == null)
                        {
                            item.IdIngredienteInventario = FactoryContadores.GetMax("IdIngredienteInventario");
                            db.IngredientesInventarios.AddObject(item);
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
        public static void PasarFacturasLibro(int month, int year)
        {
            using (DatosEntities db = new DatosEntities())
            {
                var x = from p in db.Facturas
                        where p.Tipo=="FACTURA" 
                        && p.Fecha.Value.Year == year && p.Fecha.Value.Month == month
                        select p;
                foreach (var item in x)
                {
                    if (!FactoryLibroVentas.Existe(item))
                    {
                        FactoryLibroVentas.EscribirItemFactura(item);
                        item.LibroVentas = true;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
