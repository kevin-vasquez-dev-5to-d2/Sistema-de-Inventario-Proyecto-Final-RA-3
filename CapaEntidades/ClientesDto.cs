using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ClientesDto
    {
        public int IdCliente { get; set; }

        public string TipoCliente { get; set; }  // FISICO o EMPRESA

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NombreEmpresa { get; set; }

        public string Cedula { get; set; }
        public string Rnc { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public bool Estado { get; set; }
    }
}