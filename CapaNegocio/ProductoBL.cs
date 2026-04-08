using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoBL
    {
        public static bool InsertarProducto(ProductosDto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es requerido.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            if (producto.IdCategoria <= 0)
                throw new ArgumentException("La categoría es requerida.");
            if (producto.IdProveedor <= 0)
                throw new ArgumentException("El proveedor es requerido.");
            if (producto.CreadoPor <= 0)
                throw new ArgumentException("El usuario que crea es requerido.");

            return ProductoDAL.InsertarProducto(producto);
        }

        public static List<ProductosDto> ListarProductos()
        {
            return ProductoDAL.ListarProductos();
        }

        public static bool ActualizarProducto(ProductosDto producto)
        {
            if (producto.IdProducto <= 0)
                throw new ArgumentException("El ID del producto es inválido.");
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es requerido.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            if (producto.IdCategoria <= 0)
                throw new ArgumentException("La categoría es requerida.");
            if (producto.IdProveedor <= 0)
                throw new ArgumentException("El proveedor es requerido.");

            return ProductoDAL.ActualizarProducto(producto);
        }

        public static bool EliminarProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto es inválido.");

            return ProductoDAL.EliminarProducto(idProducto);
        }

        public static List<ProductosDto> ListarProductosPorProveedor(int idProveedor)
        {
            if (idProveedor <= 0)
                throw new ArgumentException("El ID del proveedor es inválido.");

            return ProductoDAL.ListarProductosPorProveedor(idProveedor);
        }
    }
}