using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuariosBL
    {
        public static bool InsertarUsuario(UsuariosDto usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es requerido.");
            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new ArgumentException("El nombre de usuario es requerido.");
            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException("La contraseña es requerida.");
            if (usuario.IdRol <= 0)
                throw new ArgumentException("El rol es requerido.");

            return UsuariosDAL.InsertarUsuario(usuario);
        }

        public static List<UsuariosDto> ListarUsuarios()
        {
            return UsuariosDAL.ListarUsuarios();
        }

        public static bool ActualizarUsuario(UsuariosDto usuario)
        {
            if (usuario.IdUsuario <= 0)
                throw new ArgumentException("El ID del usuario es inválido.");
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es requerido.");
            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new ArgumentException("El nombre de usuario es requerido.");
            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException("La contraseña es requerida.");
            if (usuario.IdRol <= 0)
                throw new ArgumentException("El rol es requerido.");

            return UsuariosDAL.ActualizarUsuario(usuario);
        }

        public static bool EliminarUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El ID del usuario es inválido.");

            return UsuariosDAL.EliminarUsuario(idUsuario);
        }

        /// <summary>
        /// Valida el login del usuario
        /// </summary>
        public static UsuariosDto ValidarLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario es requerido.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es requerida.");

            UsuariosDto usuario = UsuariosDAL.ValidarLogin(username, password);

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (!usuario.Estado)
                throw new Exception("El usuario está inactivo. Contacta al administrador.");

            return usuario;
        }
    }
}