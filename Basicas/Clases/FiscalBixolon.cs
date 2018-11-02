using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Data.Linq;
using System.Linq;
using HK;
using HK.Clases;

namespace Bixolon
{
    public class FiscalBixolon 
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
        ~ FiscalBixolon()
        {
         //   this.AuditMensaje.Hide();

        }
    //    FrmInformacion AuditMensaje = new FrmInformacion();
        #region Delaraciones
        //declaracion de funciones de la DLL
        [DllImport("tfhkaif.dll")]
        public static extern bool OpenFpctrl(string lpPortName);

        [DllImport("tfhkaif.dll")]
        public static extern bool CloseFpctrl();

        [DllImport("tfhkaif.dll")]
        public static extern bool CheckFprinter();

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool ReadFpStatus(int* status, int* error);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool SendCmd(int* status, int* error, string cmd);

        [DllImport("tfhkaif.dll")]
        public static extern int SendNcmd(int status, int error, string buffer);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern int SendFileCmd(int* status, int* error, string file);

        [DllImport("tfhkaif.dll")]
        unsafe public static extern bool UploadStatusCmd(int* status, int* error, string cmd, string file);

        // Variables para monitorear retorno de funciones de la dll
        public  int iError=0;
        public  int iStatus=0;
        public  bool bRet;
        #endregion
        public void DetectarImpresora()
        {
            unsafe
            {
                try
                {
                    int Error = 0;
                    int Status = 0;
                    if (OpenFpctrl(Basicas.parametros().PuertoImpresoraFiscal))
                    {
                        if (CheckFprinter())
                        {
                            if (ReadFpStatus(&Status, &Error))
                            {
                                iError = Error;
                                iStatus = Status;
                                CloseFpctrl();
                                return;
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format("Error en la impresora fiscal {0} status {1}", Error.ToString(),Status.ToString()));
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("Imposible abrir puerto {0} para la impresora fiscal", Basicas.parametros().PuertoImpresoraFiscal));
                    }
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        }
        public void ImprimeFactura(Factura documento)
        {
            int error;
            int status;
            string sCmd;
            bool bRet;            
            unsafe
            {
                try
                {
                    if (documento == null)
                    {
                        throw new Exception("Documento en blanco no se puede imprimir");
                    }
                    if (documento.MontoTotal <=0)
                    {
                        throw new Exception("Esta factura no tiene productos");
                    }
                    ConectarImpresora();
              //     bRet = SendCmd(&status, &error, "810  ==========");
                    double SubTotal = 0;
                    double MontoIva = 0;
                    bRet = SendCmd(&status, &error, "i01Cedula/Rif:");
                    bRet = SendCmd(&status, &error, "i02"+ documento.CedulaRif);
                    bRet = SendCmd(&status, &error, "i03Razon Social:");
                    bRet = SendCmd(&status, &error, "i04"+ documento.RazonSocial);
                    if (documento.Direccion != null)
                    {
                        bRet = SendCmd(&status, &error, "i05Direccion:");
                        sCmd = "i06" + documento.Direccion;
                        bRet = SendCmd(&status, &error, sCmd);
                    }                   
                    sCmd = string.Format("i07 MESA {0}",documento.Mesonero);
                    bRet = SendCmd(&status, &error, sCmd);
                    var Acumulado = from p in documento.FacturasPlatos
                                    group p by new { p.Descripcion, p.TasaIva, p.Precio, p.PrecioConIva } into itemResumido
                                    select new
                                    {
                                        Descripcion = itemResumido.Key.Descripcion,
                                        TasaIva = itemResumido.Key.TasaIva,
                                        Cantidad = itemResumido.Sum(x => x.Cantidad),
                                        Precio = itemResumido.Key.Precio,
                                        PrecioConIva = itemResumido.Key.PrecioConIva
                                    };
                    foreach (var d in Acumulado)
                    {
                        sCmd = "!";
                        if (d.TasaIva == 0)
                        {
                            sCmd = " ";
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIva)
                        {
                            sCmd = "!";
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIvaB)
                        {
                            sCmd = '"'.ToString();
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIvaC)
                        {
                            sCmd = '#'.ToString();
                        }
                        //sCmd = d.TasaIva == 0 ? " " : "!";
                        SubTotal += ((double)d.Cantidad.GetValueOrDefault(0) * (double)d.PrecioConIva.GetValueOrDefault(0));
                        MontoIva += ((double)d.Cantidad.GetValueOrDefault(0) * ((double)d.PrecioConIva.GetValueOrDefault(0)) - (double)d.Precio.GetValueOrDefault(0));
                        string Precio = "0000000000";
                        if (Basicas.parametros().TipoIva == "INCLUIDO")
                        {
                            Precio = (d.PrecioConIva.GetValueOrDefault(0)*100).ToString("0000000000");
                        }
                        else
                        {
                            Precio = (d.Precio.GetValueOrDefault(0)*100).ToString("0000000000");
                        }
                        string Cantidad = (d.Cantidad.GetValueOrDefault(0) * 1000).ToString("00000000");
                        string Descripcion = d.Descripcion.PadRight(37);
                        if (d.Descripcion.Length <= 37)
                        {
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + d.Descripcion);
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}", d.Descripcion));
                            }
                        }
                        else
                        {
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + Descripcion.Substring(0, 37));
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}", d.Descripcion));
                            }
                            string Descripcion2 = d.Descripcion.Substring(37, (d.Descripcion.Length - 37));
                            bRet = SendCmd(&status, &error, "@" + Descripcion2);
                        }
                    }
                    //if (documento.DescuentoBs.GetValueOrDefault(0) != 0)
                    //{

                    //    documento.DescuentoBs = documento.MontoTotal.GetValueOrDefault(0) - SubTotal - MontoIva;
                    //    documento.DescuentoBs = documento.DescuentoBs.GetValueOrDefault(0) * -1;
                    //    documento.DescuentoPorcentaje = (documento.DescuentoBs.GetValueOrDefault(0) * 100) / (SubTotal + MontoIva);
                    //    bRet = SendCmd(&status, &error, "3");
                    //    string DescuentoPorcentaje = (documento.DescuentoPorcentaje.GetValueOrDefault(0) * 100).ToString("0000");
                    //    bRet = SendCmd(&status, &error, "p-" + DescuentoPorcentaje);
                    //}
                    //Pagos
                   // CargarS2();
                    
                    if(documento.MontoServicio.GetValueOrDefault(0)>0)
                    {
                        sCmd = 0== 0 ? " " : "!";
                        string Precio = (documento.MontoServicio.GetValueOrDefault(0)*100).ToString("0000000000");
                        bRet = SendCmd(&status, &error, sCmd + Precio + "00001000" + "SERVICIO 10%");
                    }
                    double TotalPagos = 0;
                    if (documento.Efectivo.GetValueOrDefault(0) != 0)
                    {
                        double pago = documento.Efectivo.GetValueOrDefault(0);
                        sCmd = "201" + (pago * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Efectivo.Value;
                    }
                    if (documento.Cheque.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "205" + ((double)documento.Cheque * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Cheque.Value;
                    }
                    if (documento.Retencion.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "206" + ((double)documento.Retencion * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Retencion.Value;
                    }
                    if (documento.Transferencia.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "207" + ((double)documento.Transferencia * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Transferencia.Value;
                    }
                    if (documento.Tarjeta.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "209" + ((double)documento.Tarjeta * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Tarjeta.Value;
                    }
                    CargarS2();
                    if (this.montoPorPagar > 0)
                    {
                        sCmd = "116" + (montoPorPagar * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                    }
                    System.Threading.Thread.Sleep(1000);
                    CargarS1(false);
                    documento.Fecha = this.Fecha;
                    documento.Hora = DateTime.Now;                  
                    documento.Numero = this.numeroFactura;
                    documento.NumeroZ = (this.ultimoZ + 1).ToString("0000");
                    this.LiberarImpresora();            
                }
                catch (Exception x)
                {
                    System.Threading.Thread.Sleep(1000);                    
                    throw x;
                }
            }            
        }        
        public void ImprimeDevolucion(Factura documento)
        {
            if (documento == null)
            {
                throw new Exception("Documento en blanco no se puede imprimir");
            }
            this.ConectarImpresora();
            unsafe
            {
                try
                {
                    int error = 0;
                    int status = 0;
                    string sCmd;
                    bool bRet = false;
                    bRet = SendCmd(&status, &error, "i01Cedula/Rif:" + documento.CedulaRif);
                    bRet = SendCmd(&status, &error, "i02Razon Social:");
                    sCmd = "i03" + documento.RazonSocial;
                    bRet = SendCmd(&status, &error, sCmd);
                    bRet = SendCmd(&status, &error, "i04Direccion:");
                    sCmd = "i05" + documento.Direccion;
                    bRet = SendCmd(&status, &error, sCmd);
                    if (documento.Direccion != null)
                     {
                         if (documento.Direccion.Length > 40)
                         {
                             sCmd = "i05" + documento.Direccion;
                             bRet = SendCmd(&status, &error, sCmd);
                             bRet = SendCmd(&status, &error, "i06" + documento.Direccion.Substring(40, documento.Direccion.Length - 40));
                         }
                         else
                         {
                             bRet = SendCmd(&status, &error, "i06" + documento.Direccion);
                         }

                     }
                    sCmd = "i07 # FACTURA AFECTADA:" + documento.Numero;
                    bRet = SendCmd(&status, &error, sCmd);
                   
                    // Agrego el servicio en la ultima fila
                    // DS.ImpresionTicket.AddImpresionTicketRow(1, 1, 1, 1, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, 1, "", 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, "SERVICIO 10%", 1, documento[0].MontoServicio, 0, "", "", "");
                    double SubTotal = 0;
                    double MontoIva = 0;

                    foreach (FacturasPlato d in documento.FacturasPlatos)
                    {
                        sCmd = "d1";
                        if (d.TasaIva == 0)
                        {
                            sCmd = "d0";
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIva)
                        {
                            sCmd = "d1";
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIvaB)
                        {
                            sCmd = "d2";
                        }
                        else if (d.TasaIva == Basicas.parametros().TasaIvaC)
                        {
                            sCmd = "d3";
                        }
                        SubTotal += ((double)d.Cantidad.GetValueOrDefault(0) * (double)d.PrecioConIva.GetValueOrDefault(0));
                        MontoIva += ((double)d.Cantidad.GetValueOrDefault(0) * ((double)d.PrecioConIva.GetValueOrDefault(0)) - (double)d.Precio.GetValueOrDefault(0));
                        string Precio = "0000000000";
                        if (Basicas.parametros().TipoIva == "INCLUIDO")
                        {
                            Precio = (d.PrecioConIva.GetValueOrDefault(0) * 100).ToString("0000000000");
                        }
                        else
                        {
                            Precio = (d.Precio.GetValueOrDefault(0) * 100).ToString("0000000000");
                        }
                        string Cantidad = (d.Cantidad.GetValueOrDefault(0) * 1000).ToString("00000000");
                        string Descripcion = d.Descripcion.PadRight(37);
                        if (d.Descripcion.Length <= 37)
                        {
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + d.Descripcion);
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}", d.Descripcion));
                            }
                        }
                        else
                        {
                            bRet = SendCmd(&status, &error, sCmd + Precio + Cantidad + Descripcion.Substring(0, 37));
                            if (!bRet)
                            {
                                throw new Exception(string.Format("No es posible imprimir en producto:{0}", d.Descripcion));
                            }
                            string Descripcion2 = d.Descripcion.Substring(37, (d.Descripcion.Length - 37));
                            bRet = SendCmd(&status, &error, "@" + Descripcion2);
                        }
                    }
                    //if (documento.DescuentoBs != 0)
                    //{

                    //    documento.DescuentoBs = documento.MontoTotal - SubTotal - MontoIva;
                    //    documento.DescuentoBs = documento.DescuentoBs * -1;
                    //    documento.DescuentoPorcentaje = (documento.DescuentoBs * 100) / (SubTotal + MontoIva);
                    //    bRet = SendCmd(&status, &error, "3");
                    //    string DescuentoPorcentaje = ((double)documento.DescuentoPorcentaje * 100).ToString("0000");
                    //    bRet = SendCmd(&status, &error, "p-" + DescuentoPorcentaje);
                    //}
                    //Pagos
                    double TotalPagos = 0;
                    if (documento.Efectivo.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "f01" + ((double)documento.Efectivo * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Efectivo.Value;
                    }
                    if (documento.Cheque.GetValueOrDefault(0) != 0)
                    {
                        sCmd = "f05" + ((double)documento.Cheque * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);
                        TotalPagos += documento.Cheque.Value;
                    }
                    if (documento.MontoTotal.GetValueOrDefault(0) > TotalPagos)
                    {
                        sCmd = "f16" + ((double)documento.MontoTotal - (double)TotalPagos * 100).ToString("000000000000");
                        bRet = SendCmd(&status, &error, sCmd);

                    }
                    CargarX(false);
                    iError = error;
                    iStatus = status;
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        }
        public bool ImprimeFacturaCopia(string Numero)
        {
            int error=0;
            int status=0;
            bool bRet;

            unsafe
            {
                System.Threading.Thread.Sleep(1000);
                this.ConectarImpresora();
                string n = Numero.Substring(1, 7);
                bRet = SendCmd(&status, &error, "RF" +  n + n);
                if (!bRet)
                {
                    throw new Exception("Esta impresora no puede imprimir ese resumen esa funcion es de la Bixolon 350 exclusivamente");
                }
                iError = error;
                iStatus = status;
                this.LiberarImpresora();
                return bRet;
            }
        }
        public void ReporteX()
        {
            int error=0;
            int status=0;
            string sCmd;
            bool bRet;
            ConectarImpresora();
            unsafe
            {
                //************ Imprimir Reporte X *******************
                sCmd = "I0X";
                bRet = SendCmd(&status, &error, sCmd);
            }
            iError = error;
            iStatus = status;
            LiberarImpresora();
        }
        public void ReporteZ()
        {
            int error=0;
            int status=0;
            string sCmd;
            bool bRet;
            ConectarImpresora();
            unsafe
            {
                //************ Imprimir Reporte Z *******************
                sCmd = "I0Z";
                bRet = SendCmd(&status, &error, sCmd);
           }
            iError = error;
            iStatus = status;
            this.LiberarImpresora();
        }
        public void CargarX(bool conectar)
        {
            try
            {
                unsafe
                {   
                    int error;
                    int status;
                    iError = error = 0;
                    iStatus = status = 0;
                    if (!UploadStatusCmd(&status, &error, "U0X", "ReporteX.TXT"))
                    {
                        throw new Exception("Imposible leer datos desde Impresora Fiscal");
                    }
                    using (StreamReader sr = new StreamReader("ReporteX.TXT"))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            this.UltimaDevolucion=line.Substring(168, 8);
                        }
            
                    }
                }
            }
            catch (Exception x)
            {
                Basicas.ManejarError(x);
            }
        }
        public void CargarS1(bool conectar)
        {
            try
            {
                { }
                if (conectar)
                {
                    ConectarImpresora();
                }
                unsafe
                {
                    int error;
                    int status;
                    iError = error = 0;
                    iStatus = status = 0;
                    if (!UploadStatusCmd(&status, &error, "S1", "StatusS1.TXT"))
                    {
                        throw new Exception("Imposible leer datos desde Impresora Fiscal");
                    }
                    using (StreamReader sr = new StreamReader("StatusS1.TXT"))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            DateTime tFecha;
                            this.numeroFactura = line.Substring(21, 8);
                            this.ultimoZ = Convert.ToInt32(line.Substring(47, 4));
                            this.numeroRegistro = line.Substring(66, 10);
                            this.RIF = line.Substring(55, 11);
                            tFecha = Convert.ToDateTime(line.Substring(82, 2) + "/" + line.Substring(84, 2) + "/" + line.Substring(86, 2));
                            this.fecha = tFecha;

                        }
                    }
                }
                if (conectar)
                {
                    this.LiberarImpresora();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Imposible leer datos desde Impresora Fiscal\n" + e.Message );
            }            
        }
        private void CargarS2()
        {
            int error=0;
            int status=0;
            try
            {
                unsafe
                {
                    iError = error;
                    iStatus = status;
                    if (!UploadStatusCmd(&status, &error, "S2", "StatusS2.TXT"))
                    {
                        throw new Exception("Imposible leer datos desde Impresora Fiscal");
                    }
                    using (StreamReader sr = new StreamReader("StatusS2.TXT"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            this.subtotalBases = strToDouble(line.Substring(4, 13));
                            this.subtotalIva = strToDouble(line.Substring(18, 13));
                            this.montoPorPagar = strToDouble(line.Substring(52, 13));                           
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Imposible leer datos desde Impresora Fiscal\n" + e.Message);
            }
            return;
        }
        private double strToDouble(string p)
        {
            double Base = Convert.ToDouble(p.Substring(0,10));
            double Decimales = Convert.ToDouble(p.Substring(10,2));
            return Base + (Decimales/100);
        }
        public void ReporteMensualIVA(DateTime dateTime, DateTime dateTime_2)
        {
            int error;
            int status;
            string sCmd;
            bool bRet;
            unsafe
            {
                //************ Imprimir Reporte Z *******************
                try
                {
                    this.ConectarImpresora();
                    string Inicio = dateTime.Day.ToString("00") + dateTime.Month.ToString("00") + dateTime.Year.ToString("00").Substring(2, 2);
                    string Final = dateTime_2.Day.ToString("00") + dateTime_2.Month.ToString("00") + dateTime_2.Year.ToString("00").Substring(2, 2);
                    sCmd = "I2M" + Inicio + Final;
                    bRet = SendCmd(&status, &error, sCmd);
                    if (bRet == false)
                    {
                        throw new Exception("Esta impresora no puede imprimir ese resumen esa funcion es de la Bixolon 350 exclusivamente");
                    }
                    iError = error;
                    iStatus = status;
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
        }
        public bool IsOK()
        {
            if (iError == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DocumentoNoFiscal(string[] Texto)
        {
            int error;
            int status;
            unsafe
            {
                try
                {
                    this.ConectarImpresora();
                    foreach (string linea in Texto)
                    {
                        bRet = SendCmd(&status, &error, "800" + linea);
                    }
                    bRet = SendCmd(&status, &error, "810");
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
            
        }
        public void ImprimeVale(Vale documento)
        {
            int error;
            int status;
            unsafe
            {
                try
                {                    
                    this.ConectarImpresora();
                    bRet = SendCmd(&status, &error, string.Format("800 VALE DE CAJA:{0}",documento.Numero));
                    bRet = SendCmd(&status, &error, "800" + "CAJERO:"+ documento.Cajero);
                    bRet = SendCmd(&status, &error, "800" + " ");
                    bRet = SendCmd(&status, &error, "800" + documento.Concepto);
                    bRet = SendCmd(&status, &error, "800" + " ");
                    bRet = SendCmd(&status, &error, "800" + string.Format("MONTO BS.:{0} ",documento.Monto.Value.ToString("n2")));
                    bRet = SendCmd(&status, &error, "800" + " ");
                    bRet = SendCmd(&status, &error, "800" + " ");
                    bRet = SendCmd(&status, &error, "800 --------------------");
                    bRet = SendCmd(&status, &error, "810         FIRMA       ");
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    bRet = SendCmd(&status, &error, "810  ERROR");
                    throw x;
                }
            }

        }
        public void ImprimeOrden(Factura documento)
        {
            int error;
            int status;
            unsafe
            {
                try
                {
                    this.ConectarImpresora();
                    bRet = SendCmd(&status, &error, string.Format( "800 CLIENTE:{0}\n", documento.RazonSocial));
                    bRet = SendCmd(&status, &error, "800" + "      COMANDA    ");
                    bRet = SendCmd(&status, &error, "800" + documento.Tipo +" " + documento.NumeroOrden);
                    foreach (var item in documento.FacturasPlatos)
                    {
                        if (item.Cantidad.GetValueOrDefault(0)>1)
                        {
                            bRet = SendCmd(&status, &error, "800" + string.Format(" X {0}", item.Cantidad.Value.ToString("N0"))); 
                        }
                        bRet = SendCmd(&status, &error, "800" + string.Format(item.Descripcion ));
                        if (item.Comentarios!=null)
                        {
                            foreach (string p in item.Comentarios)
                            {
                                bRet = SendCmd(&status, &error, "800" + p);
                            }
                        }
                        if (item.Contornos!=null)
                        {
                            foreach (string p in item.Contornos)
                            {
                                bRet = SendCmd(&status, &error, "800" + p);
                            }
                        }
                    }
                    bRet = SendCmd(&status, &error, "810  ==========");
                   // bRet = SendCmd(&status, &error, "800 USTED SERA LLAMADO POR EL NUMERO=>" + documento.Numero.Substring(5,3));
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    bRet = SendCmd(&status, &error, "810  ==========");
                    throw x;
                }
            }

        }
        public void ImprimeCorte(MesasAbierta documento)
        {
            int error;
            int status;
            unsafe
            {
                try
                {
                    this.ConectarImpresora();
                    bRet = SendCmd(&status, &error, "800" + "               CORTE DE CUENTA    ");
                    bRet = SendCmd(&status, &error, "800" + "MESA:"+ documento.Mesa );
                    bRet = SendCmd(&status, &error, "800" + "MESONERO:" + documento.Mesonero);
                    bRet = SendCmd(&status, &error, "800" + "CANT  DESCRIPCION                MONTO" + documento.Mesonero);
                    bRet = SendCmd(&status, &error, "800" + "======================================");
                    using (var db = new DatosEntities())
                    {
                        var mesaPlatos = from p in db.MesasAbiertasPlatos
                                         where p.IdMesaAbierta == documento.IdMesaAbierta
                                         select p;
                        foreach (var item in mesaPlatos)
                        {
                            bRet = SendCmd(&status, &error, string.Format("800" + "{0} {1} {2}", item.Cantidad.Value.ToString("000"),item.Descripcion.PadRight(22).Substring(0,22), (item.Cantidad.GetValueOrDefault(0)*item.Precio.GetValueOrDefault(0)).ToString("n2").PadLeft(8)));
                        }
                    }
                    bRet = SendCmd(&status, &error, "800" + "======================================");
                    bRet = SendCmd(&status, &error, "800" + string.Format("MONTO SERVICIO:{0}".PadLeft(30), documento.MontoServicio.GetValueOrDefault(0).ToString("N2").PadLeft(8)));
                    bRet = SendCmd(&status, &error, "800" + string.Format("  MONTO EXENTO:{0}".PadLeft(30), documento.MontoExento.GetValueOrDefault(0).ToString("N2").PadLeft(8)));
                    bRet = SendCmd(&status, &error, "800" + string.Format("MONTO GRAVABLE:{0}".PadLeft(30), documento.MontoGravable.GetValueOrDefault(0).ToString("N2").PadLeft(8)));
                    bRet = SendCmd(&status, &error, "800" + string.Format("     MONTO IVA:{0}".PadLeft(30), documento.MontoIva.GetValueOrDefault(0).ToString("N2").PadLeft(8)));
                    bRet = SendCmd(&status, &error, "800" + string.Format("   MONTO TOTAL:{0}".PadLeft(30), documento.MontoTotal.GetValueOrDefault(0).ToString("N2").PadLeft(8)));
                    bRet = SendCmd(&status, &error, "800" + "==================================");
                    bRet = SendCmd(&status, &error, "800"+"POR EXIGENCIAS DEL SENIAT ");
                    bRet = SendCmd(&status, &error, "800"+"NECESITAMOS LO SIGUIENTE:");
                    bRet = SendCmd(&status, &error, "800"+"CEDULA/RIF:__________________________");
                    bRet = SendCmd(&status, &error, "800"+"    NOMBRE:__________________________");
                    bRet = SendCmd(&status, &error, "810" + "  ");
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    bRet = SendCmd(&status, &error, "810  ");
                    throw x;
                }
            }

        }
        public void ImprimeComanda(MesasAbierta documento, List<MesasAbiertasPlato> items)
        {
            int error;
            int status;
            unsafe
            {
                try
                {
                    if (!OpenFpctrl("COM1"))
                    {
                        throw (new Exception("Error de conexión, verifique el puerto por favor..."));
                    }
                    bRet = SendCmd(&status, &error, "800" + "      COMANDA    ");
                    bRet = SendCmd(&status, &error, "800" + "  ");
                    bRet = SendCmd(&status, &error, "800" + string.Format("TICKET:{0} COMANDA:{1}", documento.Numero, FactoryContadores.GetMax("Comanda")));
                    bRet = SendCmd(&status, &error, "800" + string.Format("FECHA :{0}    HORA:{1}", DateTime.Today.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                    bRet = SendCmd(&status, &error, "800" + string.Format("MESA:{0}", documento.Mesa));
                    bRet = SendCmd(&status, &error, "800" + string.Format("MESONERO:{0}", documento.Mesonero));
                    bRet = SendCmd(&status, &error, "800" + string.Format("========================================"));
                    foreach(var item in items)
                    {
                        bRet = SendCmd(&status, &error, "800" + string.Format(" {0}) {1} ", item.Cantidad.Value.ToString("N0"), item.Descripcion.PadRight(25).Substring(0,25)));
                        if (item.Comentarios != null)
                        {
                            foreach (string p in item.Comentarios)
                            {
                                bRet = SendCmd(&status, &error, "800" + p);
                            }
                        }
                        if (item.Contornos != null)
                        {
                            foreach (string p in item.Contornos)
                            {
                                bRet = SendCmd(&status, &error, "800" + p);
                            }
                        }
                    }
                    bRet = SendCmd(&status, &error, "800" + "========================================");
                    bRet = SendCmd(&status, &error, "810" + "  ");
                    this.LiberarImpresora();
                }
                catch (Exception x)
                {
                    bRet = SendCmd(&status, &error, "810  ");
                    throw x;
                }
            }

        }
        private void ImprimeComentarios(string s)
        {
            int error = 0;
            int status = 0;
            bool bRet;
            unsafe
            {
                if (s.Length <= 40 && s.Length > 0)
                    bRet = SendCmd(&status, &error, "@" + s);
                if (s.Length > 40)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(0, 40));
                    bRet = SendCmd(&status, &error, "@" + s.Substring(40, s.Length - 40));
                }
                if (s.Length > 80)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(80, s.Length - 80));
                }
                if (s.Length > 120)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(120, s.Length - 120));
                }
                if (s.Length > 160)
                {
                    bRet = SendCmd(&status, &error, "@" + s.Substring(160, s.Length - 160));
                }
                iError = error;
                iStatus = status;
            }
        }
        private void ConectarImpresora()
        {
            if (!OpenFpctrl(Basicas.parametros().PuertoImpresoraFiscal))
            {
                throw (new Exception("Error de conexión, verifique el puerto por favor..."));
            }
        }
        private void LiberarImpresora()
        {
            //   System.Threading.Thread.Sleep(1000);
            if (!CloseFpctrl())
            {
                throw (new Exception("Error de desconexión, verifique el puerto por favor..."));
            }
        }

    }
}
