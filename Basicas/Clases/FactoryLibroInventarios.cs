using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK;

namespace HK.Clases
{
    public class FactoryLibroInventarios
    {        
        public static List<LibroInventario> getItems(int mes, int año)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroInventarios
                        orderby p.Producto
                        where p.Mes == mes && p.Año == año
                        select p;
                return q.ToList();
            }
        }
        public static List<LibroInventario> getItems(DatosEntities db, int mes, int año)
        {
            var q = from p in db.LibroInventarios
                    orderby p.Producto
                    where p.Mes == mes && p.Año == año
                    select p;
            return q.ToList();
        }
        public static LibroInventario Item(string IdLibroInventarios)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.LibroInventarios
                        where p.IdLibroInventarios == IdLibroInventarios
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static LibroInventario Item(DatosEntities db, string IdLibroInventarios)
        {
            var q = from p in db.LibroInventarios
                    where p.IdLibroInventarios == IdLibroInventarios
                    select p;
            return q.FirstOrDefault();
        }
        public static void RegistrarCompra(Compra compra)
        {
            if(compra.LibroInventarios==true)
                return;
            if (compra.IncluirLibroCompras.GetValueOrDefault(false) == false)
                return;
            using (DatosEntities db = new DatosEntities())
            {
                try
                {
                    foreach (ComprasIngrediente item in compra.ComprasIngredientes)
                    {
                        LibroInventario q = FactoryLibroInventarios.Item(db,FactoryLibroInventarios.CrearItem(compra, item).IdLibroInventarios);
                        q.Entradas += item.CantidadNeta;
                        q.Final = q.Entradas + q.Inicio - q.Salidas;
                        q.InventarioFisico = q.Final;
                        q.Costo = item.CostoNeto;
                        q.Ajustes = 0;
                        db.SaveChanges();
                    }
                    compra = FactoryCompras.Item(db, compra.IdCompra);
                    compra.LibroInventarios = true;
                    db.SaveChanges();
                 }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }
        public static void RevertirCompra(Compra compra)
        {
            //if (compra.IncluirLibroCompras != true)
            //    return;
            //if (compra.LibroInventarios == true)
            //    return;
            if (compra.IncluirLibroCompras.GetValueOrDefault(false) == false)
                return;
            using (DatosEntities db = new DatosEntities())
            {
                try
                {
                    foreach (ComprasIngrediente item in compra.ComprasIngredientes)
                    {
                        LibroInventario q = FactoryLibroInventarios.Item(db,FactoryLibroInventarios.CrearItem(compra, item).IdLibroInventarios);
                        q.Entradas -= item.Cantidad;
                        q.Final = q.Entradas + q.Inicio - q.Salidas;
                        q.InventarioFisico = q.Final;
                        q.Costo = item.CostoNeto;
                        q.Ajustes = 0;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
            }
        }  
        public static LibroInventario CrearItem(Compra factura, ComprasIngrediente item)
        {
            using (var db = new DatosEntities())
            {
                DateTime FechaInventario = Convert.ToDateTime("01/" + factura.Mes.Value.ToString("00") + "/" + factura.Año.Value.ToString("0000"));
                LibroInventario q = (from p in db.LibroInventarios
                                     where factura.Mes == p.Mes && factura.Año == p.Año && p.IdProducto == item.IdIngrediente
                                     select p).FirstOrDefault();
                if (q == null)
                {
                    LibroInventario ant = (from p in db.LibroInventarios
                                           where p.Fecha < FechaInventario && p.IdProducto == item.IdIngrediente
                                           select p).FirstOrDefault();

                    q = new LibroInventario();
                    q.IdProducto = item.IdIngrediente;
                    q.Fecha = factura.Fecha;
                    q.Inicio = ant == null ? 0 : ant.InventarioFisico;
                    q.Entradas = 0;
                    q.Salidas = 0;
                    q.Final = 0;
                    q.InventarioFisico = 0;
                    q.Ajustes = 0;
                    q.Producto = item.Ingrediente;
                    q.Mes = factura.Mes;
                    q.Año = factura.Año;
                    q.IdLibroInventarios = FactoryContadores.GetMax("IdLibroInventarios");
                    db.LibroInventarios.AddObject(q);
                    db.SaveChanges();
                }
                return q;
            }
        }
        public static LibroInventario CrearItem(int? mes, int? año, PlatosIngrediente item)
        {
            using (var db = new DatosEntities())
            {
                DateTime FechaInventario = Convert.ToDateTime("01/" + mes.Value.ToString("00") + "/" + año.Value.ToString("0000"));
                LibroInventario ant = (from p in db.LibroInventarios
                                        where p.Fecha < FechaInventario && p.IdProducto == item.IdPlato
                                        select p).FirstOrDefault();

                LibroInventario q = new LibroInventario();
                q.IdProducto = item.IdIngrediente;
                q.Fecha = FechaInventario;
                q.Inicio = ant == null ? 0 : ant.InventarioFisico;
                q.Entradas = 0;
                q.Salidas = 0;
                q.Final = 0;
                q.InventarioFisico = 0;
                q.Ajustes = 0;
                q.Mes = mes;
                q.Año = año;
                q.Producto = item.Ingrediente;
                return q;
            }
        }
        public static void Validar(LibroInventario registro)
        {
            if (string.IsNullOrEmpty(registro.IdProducto))
                throw new Exception("Error debe elegir el producto");
        }
        public static void AjustarSalidas(int mes, int año)
        {
            using (var db = new DatosEntities())
            {
                var oldLibro = from p in db.LibroInventarios
                               where p.Mes == mes && p.Año == año
                               select p;
                foreach (var item in oldLibro)
                {
                    db.LibroInventarios.DeleteObject(item);
                }
                db.SaveChanges();
                var q = from factura in db.Facturas
                        join facturaplato in db.FacturasPlatos on factura.IdFactura equals facturaplato.IdFactura
                        where factura.Fecha.Value.Month == mes && factura.Fecha.Value.Year == año && factura.Anulado == false  && (factura.Tipo=="FACTURA" ||factura.Tipo=="CONSUMO") 
                        select new VentasxPlato
                        {
                            IdPlato = facturaplato.Idplato,
                            Descripcion = facturaplato.Descripcion,
                            PlatosVendidos = facturaplato.Cantidad.Value
                        };

                var ResumenxPlato = from p in q.ToList()
                                    group p by new { p.IdPlato, p.Descripcion } into ventaxPlato
                                    select new VentasxPlato
                                    {
                                        IdPlato = ventaxPlato.Key.IdPlato,
                                        Descripcion = ventaxPlato.Key.Descripcion,
                                        PlatosVendidos = ventaxPlato.Sum(x => x.PlatosVendidos)
                                    };

                foreach (var plato in ResumenxPlato)
                {
                    List<PlatosIngrediente> ingredientes = FactoryPlatos.getIngredientes(plato.IdPlato);
                    foreach (PlatosIngrediente ingrediente in ingredientes)
                    {
                        LibroInventario itemMes = (from item in db.LibroInventarios
                                                                where item.Mes == mes && item.IdProducto == ingrediente.IdIngrediente
                                                                select item).FirstOrDefault();
                        if (itemMes == null)
                        {
                            itemMes=CrearItem(mes, año, ingrediente);
                            itemMes.Costo = (from x in db.Ingredientes
                                            where x.IdIngrediente == itemMes.IdProducto
                                            select x.Costo).FirstOrDefault();
                        }
                        itemMes.Salidas = itemMes.Salidas + (ingrediente.Cantidad * plato.PlatosVendidos);
                        itemMes.Final = itemMes.Inicio + itemMes.Entradas - itemMes.Salidas;
                        itemMes.InventarioFisico = itemMes.Final;
                        itemMes.Ajustes = 0;
                        if (itemMes.IdLibroInventarios == null)
                        {
                            itemMes.IdLibroInventarios = FactoryContadores.GetMax("IdLibroInventarios");
                            db.LibroInventarios.AddObject(itemMes);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
        public static void PasarComprasLibro(int Mes,int Año)
        {
            using (DatosEntities db = new DatosEntities())
            {
                foreach (var item in db.LibroInventarios.Where(x=>x.Mes==Mes && x.Año==Año))
                {
                    item.Entradas = 0;
                }
                db.SaveChanges();
                foreach (var item in db.Compras.Where(x=>x.Mes==Mes && x.Año==Año))
                {
                    item.LibroInventarios = false;
                    FactoryLibroInventarios.RegistrarCompra(item);
                }
            }
        }
    }
}
