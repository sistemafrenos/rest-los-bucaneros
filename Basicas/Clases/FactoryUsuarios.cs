using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HK.Clases
{
    public class FactoryUsuarios
    {
        public static Usuario UsuarioActivo = null;
        public static List<string> getUsuarios()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        orderby p.Nombre
                        where p.Activo == true
                        select p.Nombre;
                return q.ToList();
            }
        }
        public static List<string> getAdministradores()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        orderby p.Nombre
                        where p.Activo == true && p.TipoUsuario == "ADMINISTRADOR"
                        select p.Nombre;
                return q.ToList();
            }
        }
        public static List<string> getCajeros()
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        orderby p.Nombre
                        where p.Activo == true && (p.TipoUsuario == "CAJERO")
                        select p.Nombre;
                return q.ToList();
            }
        }
        public static List<Usuario> getItems(string texto,string Tipo)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        orderby p.Nombre
                        where (p.Cedula.Contains(texto) || p.Nombre.Contains(texto) || texto.Length == 0) && p.Activo == true && p.TipoUsuario == Tipo
                        select p;
                return q.ToList();
            }
        }
        public static List<Usuario> getItems(DatosEntities db,string texto,string Tipo)
        {
            var q = from p in db.Usuarios
                    orderby p.Nombre
                    where (p.Cedula.Contains(texto) || p.Nombre.Contains(texto) || texto.Length == 0) && p.Activo == true && p.TipoUsuario == Tipo
                    select p;
            return q.ToList();
        }
        public static Usuario Item(string ID)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        where p.IdUsuario == ID
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Usuario ItemNombre(string nombre)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        where p.Nombre == nombre && p.Activo==true
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Usuario Item(string Usuario,string Contraseña)
        {
            using (var db = new DatosEntities())
            {
                var q = from p in db.Usuarios
                        where p.Nombre == Usuario && p.Clave == Contraseña && p.Activo == true
                        select p;
                return q.FirstOrDefault();
            }
        }
        public static Usuario CrearUsuario(string TipoUsuario)
        {
            Usuario usuario = new Usuario();
            usuario.Activo = true;
            usuario.Nombre = TipoUsuario;
            usuario.Cedula = "V0123456789";
            usuario.TipoUsuario = TipoUsuario;
            usuario.IdUsuario = FactoryContadores.GetMax("IdUsuario");
            usuario.Clave = "**";
            usuario.PuedeDarConsumoInterno = true;
            usuario.PuedePedirCorteDeCuenta = true;
            usuario.PuedeRegistrarPago = true;
            usuario.PuedeSepararCuentas = true;
            return usuario;
        }
    }
}
