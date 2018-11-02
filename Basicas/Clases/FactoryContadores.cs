using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryContadores
    {
        public static string GetMax(string Variable)
        {
            try
            {
                using (var oEntidades = new DatosEntities())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                    }
                    else
                    {                        
                        Contador.Valor++;

                    }
                    oEntidades.SaveChanges();
                    return ((int)Contador.Valor).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
        public static string GetMaxDiario(string Variable)
        {
            try
            {
                string Fecha = DateTime.Today.ToShortDateString().Replace("/","");
                using (var oEntidades = new DatosEntities())
                {
                    string texto = "C_" + Fecha;
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == texto);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = texto;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                    }
                    else
                    {
                        Contador.Valor++;

                    }
                    oEntidades.SaveChanges();
                    return ((int)Contador.Valor).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                Basicas.ManejarError(ex);
            }
            return "";
        }
        public static string GetMaxComprobante(string mes, string año)
        {
            return año + mes + "00" + GetMaxII("COMPROBANTE_" + año + mes);
        }
        public static void SetMaxComprobante(string mes, string año, int valor)
        {
            SetMax("COMPROBANTE_" + año + mes, valor);
        }
        public static string GetMaxII(string Variable)
        {
            try
            {
                using (var oEntidades = new DatosEntities())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                        Contador.Valor = 1;
                        oEntidades.Contadores.AddObject(Contador);
                        oEntidades.SaveChanges();
                    }
                    else
                    {
                        Contador.Valor++;
                    }
                    return ((int)Contador.Valor).ToString("000000");
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return "";
        }
        public static void SetMax(string Variable, int Valor)
        {
            try
            {
                using (var oEntidades = new DatosEntities())
                {
                    Contadore Contador = oEntidades.Contadores.FirstOrDefault(x => x.Variable == Variable);
                    if (Contador == null)
                    {
                        Contador = new Contadore();
                        Contador.Variable = Variable;
                    }
                    else
                    {
                        Contador.Valor++;

                    }
                    Contador.Valor = Valor;
                    oEntidades.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }
}
