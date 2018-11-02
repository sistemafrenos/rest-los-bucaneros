using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HK.Clases;
using HK.Formas;

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
            login.Sistema = "OK RESTAURANT - ADMINISTRACION";
            login.TipoUsuario = "ADMINISTRADOR";
            login.ShowDialog();
            if (login.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (FactoryUsuarios.UsuarioActivo !=null)
                {
                    Application.Run(new FrmPrincipal());
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
