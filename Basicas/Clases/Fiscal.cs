using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bixolon;

namespace HK.Clases
{
    public class Fiscal
    {
#region campos
        private string ultimaDevolucion;
        public string UltimaDevolucion
        {
            get { return ultimaDevolucion; }
            set { ultimaDevolucion = value; }
        }
        private string rIF;
        public string RIF
        {
            get { return rIF; }
            set { rIF = value; }
        }
        private string numeroFactura;
        public string NumeroFactura
        {
            get { return numeroFactura; }
            set { numeroFactura = value; }
        }
        private int ultimoZ = 0;
        public int UltimoZ
        {
            get { return ultimoZ; }
            set { ultimoZ = value; }
        }
        private string numeroRegistro;
        public string NumeroRegistro
        {
            get { return numeroRegistro; }
            set { numeroRegistro = value; }
        }
        private DateTime fecha;
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private DateTime hora;
        public DateTime Hora
        {
            get { return hora; }
            set { hora = value; }
        }
        private double subtotalBases = 0;
        public double SubtotalBases
        {
            get { return subtotalBases; }
            set { subtotalBases = value; }
        }
        private double subtotalIva = 0;
        public double SubtotalIva
        {
            get { return subtotalIva; }
            set { subtotalIva = value; }
        }
        private double montoPorPagar = 0;
        public double MontoPorPagar
        {
            get { return montoPorPagar; }
            set { montoPorPagar = value; }
        }
        private double? montoFactura = 0;
        public double? MontoFactura
        {
            get { return montoFactura; }
            set { montoFactura = value; }
        }
#endregion
        public void DetectarImpresora()
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.DetectarImpresora();
            }   
        }
        public void ImprimeFactura(Factura documento)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeFactura(documento);
            }   
        }
        public void ImprimeDevolucion(Factura documento)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeDevolucion(documento);
            }
        }
        public void ReporteX()
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ReporteX();
            }
        }
        public void ReporteZ()
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ReporteZ();
            }
        }
        public void DocumentoNoFiscal(string[] Texto)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.DocumentoNoFiscal(Texto);
            }
        }
        public void ImprimeVale(Vale documento)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeVale(documento);
            }
        }
        public void ImprimeOrden(Factura documento)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeOrden(documento);
            }
        }
        public void ImprimeCorte(MesasAbierta documento)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeCorte(documento);
            }
        }
        public void ImprimeComanda(MesasAbierta documento, List<MesasAbiertasPlato> items)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeComanda(documento,items);
            }
        }

        internal void ImprimeFacturaCopia(string p)
        {
            if (Basicas.TipoFiscal == "BIXOLON")
            {
                FiscalBixolon f = new FiscalBixolon();
                f.ImprimeFacturaCopia(p);
            }
        }
    }
}
