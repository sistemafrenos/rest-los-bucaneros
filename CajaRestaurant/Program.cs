using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HK.Formas;
using HK.Clases;

namespace HK
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin login = new FrmLogin();
            login.Sistema = "OK Restaurant - Caja";
            login.TipoUsuario = "CAJERO";
            login.ShowDialog();
            if (login.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if(FactoryUsuarios.UsuarioActivo.TipoUsuario=="CAJERO" )
                {
                    //Basicas.CargarParametros();
                    FrmMesasAbiertas f = new FrmMesasAbiertas();
                    do
                    {
                        f = new FrmMesasAbiertas();
                        f.ShowDialog();
                    } while (f.DialogResult != System.Windows.Forms.DialogResult.Cancel);
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
