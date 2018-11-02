using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using HK.Clases;

namespace HK
{
    #region Extencion de Tablas
    public partial class Cliente : EntityObject
    {
        public List<string> Errores()
        {
            List<string> retorno = new List<string>();
            if (string.IsNullOrEmpty(this.CedulaRif))
                retorno.Add("Cedula o Rif vacia");
            else
                if (this.CedulaRif.Length != 10)
                    retorno.Add("El campo cedula o Rif debe tener 10 caracteres");
            if (string.IsNullOrEmpty(this.RazonSocial))
                retorno.Add("La razon social no puede estar vacio");
            else
                if (this.RazonSocial.Length > 100)
                    retorno.Add("El campo razon social no puede tener mas de 100 caracteres");
            return retorno;
        }
        public string ErroresStr()
        {
            string retorno = "";
            foreach (string x in this.Errores())
            {
                retorno += "\n" + x;
            }
            return retorno;
        }
    }
    public partial class LibroCompra:EntityObject
    {
        public void Calcular()
        {
            this.MontoTotal = this.MontoExento.GetValueOrDefault(0) + this.MontoGravable.GetValueOrDefault(0) + this.MontoIva.GetValueOrDefault(0);
        }        
    }
    public partial class Compra : EntityObject
    {
        public List<string> Errores()
        {
           List<string> retorno = new List<string>();
           if (this.IncluirLibroCompras.GetValueOrDefault(false) == true)
            {
                if (string.IsNullOrEmpty(this.Numero))
                   retorno.Add("Numero de factura vacia");
                if (string.IsNullOrEmpty(this.CedulaRif))
                    retorno.Add("Cedula o Rif vacia");
                else
                if (this.CedulaRif.Length > 20)
                    retorno.Add("El campo cedula no puede tener mas de 20 caracteres");
                if (string.IsNullOrEmpty(this.RazonSocial))
                    retorno.Add("La razon social no puede estar vacio");
                else
                if (this.RazonSocial.Length > 100)
                    retorno.Add("El campo razon social no puede tener mas de 100 caracteres");
                if (this.MontoTotal.GetValueOrDefault(0) == 0)
                    retorno.Add("El monto total no puede ser cero");
            }
            return retorno;
           }
        public string ErroresStr()
        {
            string retorno = "";
            foreach (string x in this.Errores())
            {
                retorno += "\n"+x;
            }
            return retorno;
        }        
        public void Totalizar()
        {
            this.MontoExento = this.ComprasIngredientes.Where(x => x.TasaIva.GetValueOrDefault(0) == 0).Sum(x => x.Cantidad * x.Costo);
            this.MontoGravable = this.ComprasIngredientes.Where(x => x.TasaIva.GetValueOrDefault(0) > 0).Sum(x => x.Cantidad * x.Costo);
            this.MontoIva =  this.ComprasIngredientes.Where(x => x.TasaIva.GetValueOrDefault(0) > 0).Sum(x => x.Cantidad * x.Costo * x.TasaIva /100);
            this.MontoTotal = this.MontoExento.GetValueOrDefault(0) 
                + this.MontoGravable.GetValueOrDefault(0) 
                + this.MontoIva.GetValueOrDefault(0)
                + this.MontoImpuestosLicores.GetValueOrDefault(0);
        }
    }
    public partial class Factura : EntityObject
    {
        public void calcularSaldo()
        {
            this.Saldo = this.MontoTotal.GetValueOrDefault(0) - (
                this.Efectivo.GetValueOrDefault(0) + this.Transferencia.GetValueOrDefault(0) + 
                this.Tarjeta.GetValueOrDefault(0) + this.Cheque.GetValueOrDefault(0) + 
                this.Cambio.GetValueOrDefault(0) + this.ConsumoInterno.GetValueOrDefault(0) + 
                this.Credito.GetValueOrDefault(0) + this.Retencion.GetValueOrDefault()
            );
            if (this.Saldo < 0)
            {
                this.Cambio = (double)decimal.Round((decimal)Saldo * -1, 2);
                this.Saldo = null;
            }
            else
            {
                this.Cambio = 0;
            }
        }
        public void Totalizar(bool Servicio, double descuento)
        {
            double tasaIva = Basicas.parametros().TasaIva.GetValueOrDefault();
            foreach (FacturasPlato item in this.FacturasPlatos.Where(x => x.TasaIva > 0))
            {
                item.TasaIva = tasaIva;
                item.PrecioConIva = Basicas.Round(item.Precio + (item.Precio * tasaIva / 100));
                item.Total = item.PrecioConIva * item.Cantidad;
            }
            this.MontoExento = this.FacturasPlatos.Where(x => x.TasaIva == 0).Sum(x => x.Cantidad * x.Precio);
            this.MontoGravable = this.FacturasPlatos.Where(x => x.TasaIva > 0).Sum(x => x.Cantidad * x.Precio);
            if (Servicio)
            {
                this.MontoServicio = Basicas.Round((MontoGravable.GetValueOrDefault(0) + MontoExento.GetValueOrDefault(0)) * 0.1);
            }

            this.MontoIva = this.FacturasPlatos.Where(x => x.TasaIva > 0).Sum(x => x.Cantidad * x.Precio * tasaIva / 100);
            this.MontoTotal = this.MontoGravable.GetValueOrDefault(0) + this.MontoExento.GetValueOrDefault(0) + this.MontoIva.GetValueOrDefault(0) + this.MontoServicio.GetValueOrDefault(0);
            this.MontoTotal = this.MontoTotal - (this.MontoTotal * descuento / 100);
            this.calcularSaldo();
        }
    }
    public partial class MesasAbierta:EntityObject
    {
        public void Totalizar(bool Servicio,List<MesasAbiertasPlato>platos,double? descuento)
        {
            this.MontoExento = platos.Where(x => x.TasaIva.GetValueOrDefault(0) == 0).Sum(x => x.Cantidad * x.Precio);
            this.MontoGravable = platos.Where(x => x.TasaIva.GetValueOrDefault(0) > 0).Sum(x => x.Cantidad * x.Precio);
            if (Servicio)
            {
                this.MontoServicio = (this.MontoGravable.GetValueOrDefault(0) + this.MontoExento.GetValueOrDefault(0)) * 0.1;
            }
            this.MontoIva = platos.Where(x => x.TasaIva.GetValueOrDefault(0) > 0).Sum(x => x.Cantidad * x.Precio * x.TasaIva / 100);
            this.MontoTotal = this.MontoTotal - (this.MontoTotal * descuento.GetValueOrDefault(0) / 100);
            this.MontoTotal = this.MontoGravable.GetValueOrDefault(0) + this.MontoExento.GetValueOrDefault(0) + this.MontoIva.GetValueOrDefault(0) + this.MontoServicio.GetValueOrDefault(0);
        }       
    }
    public partial class Mesonero : EntityObject
    {
        string error = null;
        public string Error
        {
            set { error = value; }
            get
            {
                if (string.IsNullOrEmpty(this.Cedula))
                    return("Error el campo cedula no puede estar vacio");
                if (this.Cedula.Length > 20)
                    return("Error el campo cedula no puede tener mas de 20 caracteres");
                if (string.IsNullOrEmpty(this.Codigo))
                    return("Error el codigo no puede estar vacio");
                if (this.Codigo.Length > 20)
                    return("Error codigo no puede tener mas de 20 caracteres");
                if (string.IsNullOrEmpty(this.Nombre))
                    return("Error el Nombre  no puede estar vacio");
                if (this.Nombre.Length > 100)
                    return "Error el campo cedula no puede tener mas de 100 caracteres";
                return null;
            }
        }
    }
    public partial class Usuario : EntityObject
    {
        string error = null;
        public string Error
        {
            set { error = value; }
            get
            {
                if (string.IsNullOrEmpty(this.Cedula))
                    return ("Error el campo cedula no puede estar vacio");
                if (this.Cedula.Length > 20)
                    return ("Error el campo cedula no puede tener mas de 20 caracteres");
                if (string.IsNullOrEmpty(this.Nombre))
                    return ("Error el Nombre  no puede estar vacio");
                if (this.Nombre.Length > 100)
                    return "Error el campo cedula no puede tener mas de 100 caracteres";
                if (string.IsNullOrEmpty(this.TipoUsuario))
                    return ("Error el Tipo usuario no puede estar vacio");
                return null;
            }
        }
    }
    public partial class Ingrediente : EntityObject
    {
        string error = null;
        public string Error
        {
            set { error = value; }
            get
            {
                if (string.IsNullOrEmpty(this.Grupo))
                    return ("Error el grupo no puede estar vacio");
                if (this.Grupo.Length > 20)
                    return ("Error el campo Grupo no puede tener mas de 20 caracteres");
                if (string.IsNullOrEmpty(this.Descripcion))
                    return ("Error el descripcion no puede estar vacio");
                if (this.Descripcion.Length > 100)
                    return ("Error el campo Descripcion no puede tener mas de 100 caracteres");
                if (string.IsNullOrEmpty(this.UnidadMedida))
                    return ("Error el UnidadMedida  no puede estar vacio");
                if (this.UnidadMedida.Length > 20)
                    return "Error el campo UnidadMedida) no puede tener mas de 100 caracteres";
                return null;
            }
        }
        public Nullable<global::System.Double> CostoTotal
        {
            get
            {
                return this.Costo.GetValueOrDefault(0)  * this.Existencia.GetValueOrDefault(0);
            }
            set
            {
                _CostoTotal = value;
            }
        }
        private Nullable<global::System.Double> _CostoTotal;
    }
    public partial class LibroInventario : EntityObject
    {
        public void Calcular()
        {
            this.Final = this.Inicio.GetValueOrDefault(0) + this.Entradas.GetValueOrDefault(0) - this.Salidas.GetValueOrDefault(0);
            this.InventarioFisico = this.Final.GetValueOrDefault() + this.Ajustes.GetValueOrDefault(0);
        }
    }
    public partial class LibroVenta
    {
        public void Calcular()
        {
            this.MontoTotal = this.MontoExento.GetValueOrDefault(0) + this.MontoGravable.GetValueOrDefault(0) + this.MontoIva.GetValueOrDefault(0);
        }
    }
    public partial class Receta : PlatosIngrediente
    {
        string platoDescripcion;

        public string PlatoDescripcion
        {
            get { return platoDescripcion; }
            set { platoDescripcion = value; }
        }
        string platoGrupo;

        public string PlatoGrupo
        {
            get { return platoGrupo; }
            set { platoGrupo = value; }
        }
    }
    #endregion
    #region consultas
    public class VentasxPlato : Plato
        {
            double costoPlatosVendidos = 0;

            public double CostoPlatosVendidos
            {
                get { return costoPlatosVendidos; }
                set { costoPlatosVendidos = value; }
            }
            double platosVendidos = 0;

            public double PlatosVendidos
            {
                get { return platosVendidos; }
                set { platosVendidos = value; }
            }

            double montoPlatosVendidos = 0;

            public double MontoPlatosVendidos
            {
                get { return montoPlatosVendidos; }
                set { montoPlatosVendidos = value; }
            }
        }
        public class TotalxFormaPago
        {
            string formaPago;

            public string FormaPago
            {
                get { return formaPago; }
                set { formaPago = value; }
            }
            double bolivares = 0;

            public double Bolivares
            {
                get { return bolivares; }
                set { bolivares = value; }
            }
        }
        public class TotalxDia
        {
            int facturas = 0;

            public int Facturas
            {
                get { return facturas; }
                set { facturas = value; }
            }

            DateTime fecha;

            public DateTime Fecha
            {
                get { return fecha; }
                set { fecha = value; }
            }
            double bolivares;

            public double Bolivares
            {
                get { return bolivares; }
                set { bolivares = value; }
            }
            double promedio;

            public double Promedio
            {
                get { return promedio; }
                set { promedio = value; }
            }

            private double? montoGravable;

            public double? MontoGravable
            {
                get { return montoGravable; }
                set { montoGravable = value; }
            }
            private double? montoIva;

            public double? MontoIva
            {
                get { return montoIva; }
                set { montoIva = value; }
            }
            private double? montoServicio;

            public double? MontoServicio
            {
                get { return montoServicio; }
                set { montoServicio = value; }
            }
            private double? montoTotal;

            public double? MontoTotal
            {
                get { return montoTotal; }
                set { montoTotal = value; }
            }
        }
        public class TotalxDiaMesonero:TotalxDia
        {
            string mesonero;

            public string Mesonero
            {
                get { return mesonero; }
                set { mesonero = value; }
            }
        }
        public class Valores
        {
            string variable;

            public string Variable
            {
                get { return variable; }
                set { variable = value; }
            }
            double? valor = 0;

            public double? Valor
            {
                get { return valor; }
                set { valor = value; }
            }
        }
        public class IngredientesConsumo : Ingrediente
        {
            double cantidad = 0;

            public double Cantidad
            {
                get { return cantidad; }
                set { cantidad = value; }
            }
        }
        partial class FacturasPlato
        {
            List<string> comentarios;

            public List<string> Comentarios
            {
                get { return comentarios; }
                set { comentarios = value; }
            }
            List<string> contornos;

            public List<string> Contornos
            {
                get { return contornos; }
                set { contornos = value; }
            }
        }
        partial class MesasAbiertasPlato
        {
            List<string> comentarios;

            public List<string> Comentarios
            {
                get { return comentarios; }
                set { comentarios = value; }
            }
            List<string> contornos;

            public List<string> Contornos
            {
                get { return contornos; }
                set { contornos = value; }
            }
        }
        partial class Plato
        {
            double? coeficiente = 0;

            public double? Coeficiente
            {
                get
                {
                    if (Costo.GetValueOrDefault(0) > 0)
                        return Precio / Costo;
                    else
                        return 0;
                }
                set { coeficiente = value; }
            }
        }
        partial class Mesa
        {
            double? monto = 0;

            public double? Monto
            {
                get { return monto; }
                set { monto = value; }
            }
            bool impresa = false;

            public bool Impresa
            {
                get { return impresa; }
                set { impresa = value; }
            }
            DateTime apertura = DateTime.Now;

            public DateTime Apertura
            {
                get { return apertura; }
                set { apertura = value; }
            }
            string numero;

            public string Numero
            {
                get { return numero; }
                set { numero = value; }
            }

            string mesonero;

            public string Mesonero
            {
                get { return mesonero; }
                set { mesonero = value; }
            }
        }
        #endregion
}
