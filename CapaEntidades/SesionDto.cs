using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    /// <summary>
    /// Clase estática para manejar la sesión del usuario actual
    /// </summary>
    public static class SesionDto
    {
        public static int IdUsuario { get; set; }
        public static string NombreUsuario { get; set; }
        public static string Username { get; set; }
        public static int IdRol { get; set; }
        public static string NombreRol { get; set; }
        public static bool Autenticado { get; set; }
        public static DateTime FechaLogin { get; set; }

        /// <summary>
        /// Inicia una sesión con los datos del usuario
        /// </summary>
        public static void IniciarSesion(UsuariosDto usuario)
        {
            IdUsuario = usuario.IdUsuario;
            NombreUsuario = usuario.Nombre;
            Username = usuario.Username;
            IdRol = usuario.IdRol;
            NombreRol = usuario.NombreRol;
            Autenticado = true;
            FechaLogin = DateTime.Now;
        }

        /// <summary>
        /// Cierra la sesión actual
        /// </summary>
        public static void CerrarSesion()
        {
            IdUsuario = 0;
            NombreUsuario = "";
            Username = "";
            IdRol = 0;
            NombreRol = "";
            Autenticado = false;
            FechaLogin = DateTime.MinValue;
        }

        /// <summary>
        /// Verifica si el usuario tiene permiso para una función específica
        /// </summary>
        public static bool TienePermiso(string rol)
        {
            return NombreRol.Equals(rol, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Verifica si el usuario es administrador
        /// </summary>
        public static bool EsAdmin()
        {
            // Si no está autenticado, no es admin
            if (!Autenticado)
                return false;

            // Verificar por nombre de rol
            if (!string.IsNullOrEmpty(NombreRol))
            {
                string rol = NombreRol.Trim().ToLower().Replace(" ", "");

                // Verificar múltiples formas del rol admin
                if (rol == "admin" || 
                    rol == "administrador" || 
                    rol == "administrator" ||
                    rol == "adminsistema" ||
                    rol == "admin-sistema" ||
                    rol.Contains("admin"))
                {
                    return true;
                }
            }

            // Verificar por username (fallback)
            if (!string.IsNullOrEmpty(Username))
            {
                string user = Username.ToLower();
                if (user == "admin" || user == "kevin")
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Verifica si el usuario es de almacén
        /// </summary>
        public static bool EsAlmacen()
        {
            if (string.IsNullOrEmpty(NombreRol))
                return false;

            string rol = NombreRol.Trim().ToLower();
            return rol == "almacén" || 
                   rol == "almacen" || 
                   rol == "warehouse" ||
                   rol == "almacen - operario" ||
                   rol.Contains("almacen");
        }

        /// <summary>
        /// Verifica si el usuario es usuario común
        /// </summary>
        public static bool EsUsuarioComun()
        {
            return TienePermiso("Usuario") || TienePermiso("Usuario Común");
        }

        /// <summary>
        /// Verifica si el usuario es alopez (usuario especial)
        /// </summary>
        public static bool EsAlopez()
        {
            return Username.Equals("alopez", StringComparison.OrdinalIgnoreCase);
        }
    }
}
