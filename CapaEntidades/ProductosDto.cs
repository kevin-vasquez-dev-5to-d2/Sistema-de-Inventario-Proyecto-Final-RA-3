using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int CreadoPor { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
    }

    // Alias class present in UI code
    public class ProductosDto : ProductoDto
    {
    }
}