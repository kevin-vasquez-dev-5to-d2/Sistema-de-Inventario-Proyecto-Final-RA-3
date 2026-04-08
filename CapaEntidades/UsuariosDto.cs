using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class UsuariosDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public bool Estado { get; set; }

        public class Usuarios : UsuariosDto
        {
        }
    }
}