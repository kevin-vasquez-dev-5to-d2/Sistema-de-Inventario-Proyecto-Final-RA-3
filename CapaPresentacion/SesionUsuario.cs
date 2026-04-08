using System;

namespace CapaPresentacion
{
    public static class SesionUsuario
    {
        private static string usuarioActual = "";

        public static void EstablecerUsuario(string usuario)
        {
            usuarioActual = usuario;
        }

        public static string ObtenerUsuarioActual()
        {
            if (string.IsNullOrWhiteSpace(usuarioActual))
            {
                return "invitado";
            }
            return usuarioActual;
        }

        public static void LimpiarSesion()
        {
            usuarioActual = "";
        }
    }
}
