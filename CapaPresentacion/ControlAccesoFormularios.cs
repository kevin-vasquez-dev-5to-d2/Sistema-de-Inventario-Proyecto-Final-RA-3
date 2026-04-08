using System;
using System.Collections.Generic;

namespace CapaPresentacion
{
    /// <summary>
    /// Clase para controlar el acceso a formularios específicos
    /// por usuario
    /// </summary>
    public static class ControlAccesoFormularios
    {
        // Diccionario de formularios restringidos y sus usuarios autorizados
        private static Dictionary<string, List<string>> formulariosRestringidos = new Dictionary<string, List<string>>()
        {
            // Formulario de Consulta de Movimientos: Solo kevin y alopez
            { "frmConsultaMovimientos", new List<string> { "kevin", "alopez" } },

            // Formulario de Movimientos: Todos excepto alopez
            { "frmMovimiento", new List<string> { "admin", "almacen", "user" } },

            // Agregar más formularios según sea necesario
            // { "frmOtroFormulario", new List<string> { "usuario1", "usuario2" } }
        };

        /// <summary>
        /// Verifica si un usuario tiene acceso a un formulario
        /// </summary>
        /// <param name="nombreFormulario">Nombre del formulario (clase)</param>
        /// <param name="nombreUsuario">Nombre del usuario</param>
        /// <returns>true si tiene acceso, false si no</returns>
        public static bool TieneAcceso(string nombreFormulario, string nombreUsuario)
        {
            // Si el formulario no está en la lista de restringidos, permite acceso
            if (!formulariosRestringidos.ContainsKey(nombreFormulario))
            {
                return true;
            }

            // Verificar si el usuario está en la lista de autorizados
            List<string> usuariosAutorizados = formulariosRestringidos[nombreFormulario];
            
            // Comparación insensible a mayúsculas/minúsculas
            return usuariosAutorizados.Contains(nombreUsuario.ToLower());
        }

        /// <summary>
        /// Obtiene la lista de usuarios autorizados para un formulario
        /// </summary>
        /// <param name="nombreFormulario">Nombre del formulario</param>
        /// <returns>Lista de usuarios autorizados</returns>
        public static List<string> ObtenerUsuariosAutorizados(string nombreFormulario)
        {
            if (formulariosRestringidos.ContainsKey(nombreFormulario))
            {
                return formulariosRestringidos[nombreFormulario];
            }
            return new List<string>();
        }

        /// <summary>
        /// Verifica si un formulario tiene restricción de acceso
        /// </summary>
        /// <param name="nombreFormulario">Nombre del formulario</param>
        /// <returns>true si el formulario está restringido</returns>
        public static bool EstaRestringido(string nombreFormulario)
        {
            return formulariosRestringidos.ContainsKey(nombreFormulario);
        }
    }
}
