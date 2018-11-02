using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK.Formas;
using System.Threading;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace HK.Clases
{
    public partial class Basicas
    {
        public static string TipoFiscal = "BIXOLON";
        public static void ManejarError(Exception x)
        {
            string s = null;            
            if (x.InnerException != null)
                s = string.Format("Error:{0}\n{1}:{2}",x.StackTrace,DateTime.Today, x.InnerException.Message);
            else
                s = string.Format("Error:{0}\n{1}:{2}",x.StackTrace,DateTime.Today, x.Message);
            try
            {
                MessageBox.Show(s);
                System.IO.StreamWriter log = System.IO.File.AppendText(Application.ExecutablePath + "Errores.txt");
                log.WriteLine(s);
                log.Flush();
                log.Close();
            }
            catch (Exception x2)
            {
                MessageBox.Show(x2.Message);
            }
        }
        public static Boolean ImpresoraActiva = false;
        public static double Round(double? valor)
        {
            try
            {
                decimal myValor = (decimal)valor;
                myValor = decimal.Round(myValor, 2);
                return (double)myValor;
            }
            catch
            {
                return 0;
            }
        }
        public static void DisposeImage(Image image)
        {
            image.Dispose();
            image = null;
            GC.Collect();
        }
        public static string CedulaRif(string Texto)
        {
            if (string.IsNullOrEmpty(Texto))
            {
                return Texto;
            }
            Texto = Texto.ToUpper();
            Texto = Texto.Replace(":", "");
            Texto = Texto.Replace("-", "").Replace(".", "").Replace(" ", "").Replace(",", "").Replace("CI", "").Replace("RIF", "").Replace("C", "");
            if (IsNumeric(Texto))
            {
                return "V" + Texto.PadLeft(9, '0');
            }
            switch (Texto[0])
            {
                case 'J':
                    if (Texto.Length > 10)
                    {
                        Texto.Substring(0, 10);
                    }
                    break;
                case 'G':
                    if (Texto.Length > 10)
                    {
                        Texto.Substring(0, 10);
                    }
                    break;
                case 'E':
                    Texto = Texto.Replace("E", "");
                    Texto = "E" + Texto.PadLeft(9, '0');
                    break;
                case 'V':
                    Texto = Texto.Replace("V", "");
                    Texto = "V" + Texto.PadLeft(9, '0');
                    break;
            }
            return Texto;
        }
        public static string PrintFix(string Texto, int Largo, int Alineacion)
        {
            string x = "                                        ";
            Texto = Texto.Trim();
            if (Texto.Length > Largo)
            {
                return Texto.Substring(0, Texto.Length);
            }
            switch (Alineacion)
            {
                case 1:
                    return Texto.PadRight(Largo);

                case 2:
                    return Texto.PadLeft(Largo);

                case 3:
                    int Suplemento = (Largo - Texto.Length) / 2;
                    return x.Substring(0, Suplemento) + Texto + x.Substring(0, Suplemento);

            }
            return Texto;
        }
        public static string PrintNumero(double? d, int len)
        {
            if (!d.HasValue)
            {
                d = 0;
            }
            return d.Value.ToString("n2").PadLeft(len);
        }
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static Parametro parametros()
        {
            using (var db = new DatosEntities())
            {
                Parametro p = db.Parametros.FirstOrDefault();
                return p;
            }
        }
        public static void ValidarImpresoraFiscal()
        {
            Fiscal fiscal = new Fiscal();
            try
            {
                fiscal.DetectarImpresora();
                ImpresoraActiva = true;
            }
            catch
            {
                ImpresoraActiva = false;
            }
        }
    }
}
namespace PassEncrypt
{
        interface Crypto
        {
            /// <summary>
            ///  Gets the Hash key and Salt for a specific
            ///  password. 
            /// </summary>
            /// <returns>
            /// Will return a string with three elements      
            ///    1. Hash Key
            ///    2. Salt
            /// </returns>
            string[] GenerateKeySalt(string plainTxtPass);


            /// <summary>
            ///  Change the password.
            /// 
            /// </summary>
            /// <returns>
            /// Will return a string with three elements
            ///    1. New Password
            ///    2. Hash Key
            ///    3. Salt
            /// </returns>

            string GetHashKey(string input);

        }
    public class Encrypt : Crypto
    {
 
        #region Generate the Hash key and salt for a given password
 
        /// <summary>
        /// Returns a salt value and a hash key to be stored
        /// in the database for the given user.
        /// </summary>
        /// <param name=”plainTxtPass”></param>
        /// <returns></returns>
        public string[] GenerateKeySalt(string plainTxtPass)
        {
            string[] s = new string[2];
 
            string salt = GenerateRandomSalt();
            string passAndSalt = plainTxtPass + salt;
            string hash = GetHashKey(passAndSalt);
 
            s[0] = hash;
            s[1] = salt;
            return s;
        }
 
        #endregion
 
 
        #region Generate random salt
 
        /// <summary>
        /// Generates a cryptographically strong salt string
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomSalt()
        {
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] saltInBytes = new byte[5];
            crypto.GetBytes(saltInBytes);
 
            return Convert.ToBase64String(saltInBytes);
        }
 
        #endregion
 
        #region Get the hash key for a given string
 
        public string GetHashKey(string input)
        {           
            byte[] tmpSource;
            byte[] tmpHash;
 
            //Convert input to a Byte Array (needed for hash function)
            tmpSource = ASCIIEncoding.ASCII.GetBytes(input);
 
            //Compute hash based on source data.
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
 
            return Convert.ToBase64String(tmpHash);
        }
 
        #endregion
 
    }
}
