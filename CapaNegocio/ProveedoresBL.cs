using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProveedoresBL
    {
        public static bool InsertarProveedor(ProveedoresDto proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.NombreProveedor))
                throw new ArgumentException("El nombre del proveedor es requerido.");
            if (string.IsNullOrWhiteSpace(proveedor.Telefono))
                throw new ArgumentException("El teléfono es requerido.");
            if (string.IsNullOrWhiteSpace(proveedor.Email))
                throw new ArgumentException("El email es requerido.");

            return ProveedoresDAL.InsertarProveedor(proveedor);
        }

        public static List<ProveedoresDto> ListarProveedores()
        {
            return ProveedoresDAL.ListarProveedores();
        }

        public static bool ActualizarProveedor(ProveedoresDto proveedor)
        {
            if (proveedor.IdProveedor <= 0)
                throw new ArgumentException("El ID del proveedor es inválido.");
            if (string.IsNullOrWhiteSpace(proveedor.NombreProveedor))
                throw new ArgumentException("El nombre del proveedor es requerido.");
            if (string.IsNullOrWhiteSpace(proveedor.Telefono))
                throw new ArgumentException("El teléfono es requerido.");
            if (string.IsNullOrWhiteSpace(proveedor.Email))
                throw new ArgumentException("El email es requerido.");

            return ProveedoresDAL.ActualizarProveedor(proveedor);
        }

        public static bool EliminarProveedor(int idProveedor)
        {
            if (idProveedor <= 0)
                throw new ArgumentException("El ID del proveedor es inválido.");

            return ProveedoresDAL.EliminarProveedor(idProveedor);
        }
    }
}