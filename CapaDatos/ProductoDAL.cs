using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class ProductoDAL
    {
        public static bool InsertarProducto(ProductosDto producto)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_producto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                        cmd.Parameters.AddWithValue("@creado_por", producto.CreadoPor);
                        cmd.Parameters.AddWithValue("@estado", producto.Estado);
                        cmd.Parameters.AddWithValue("@id_proveedor", producto.IdProveedor);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar producto: " + ex.Message);
            }
        }

        public static List<ProductosDto> ListarProductos()
        {
            List<ProductosDto> lista = new List<ProductosDto>();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_listar_productos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductosDto producto = new ProductosDto();

                                // Leer campos obligatorios
                                if (!reader.IsDBNull(reader.GetOrdinal("id_producto")))
                                    producto.IdProducto = (int)reader["id_producto"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre")))
                                    producto.Nombre = reader["nombre"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("precio")))
                                    producto.Precio = (decimal)reader["precio"];

                                if (!reader.IsDBNull(reader.GetOrdinal("stock")))
                                    producto.Stock = (int)reader["stock"];

                                if (!reader.IsDBNull(reader.GetOrdinal("id_categoria")))
                                    producto.IdCategoria = (int)reader["id_categoria"];

                                if (!reader.IsDBNull(reader.GetOrdinal("categoria")))
                                    producto.Categoria = reader["categoria"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("creado_por")))
                                    producto.CreadoPor = (int)reader["creado_por"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre_usuario")))
                                    producto.NombreUsuario = reader["nombre_usuario"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("fecha_creacion")))
                                    producto.FechaCreacion = (DateTime)reader["fecha_creacion"];

                                if (!reader.IsDBNull(reader.GetOrdinal("estado")))
                                    producto.Estado = (bool)reader["estado"];

                                if (!reader.IsDBNull(reader.GetOrdinal("id_proveedor")))
                                    producto.IdProveedor = (int)reader["id_proveedor"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre_proveedor")))
                                    producto.NombreProveedor = reader["nombre_proveedor"].ToString();

                                lista.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos: " + ex.Message);
            }
            return lista;
        }

        public static bool ActualizarProducto(ProductosDto producto)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_actualizar_producto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                        cmd.Parameters.AddWithValue("@id_proveedor", producto.IdProveedor);
                        cmd.Parameters.AddWithValue("@estado", producto.Estado);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        public static bool EliminarProducto(int idProducto)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_eliminar_producto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        public static List<ProductosDto> ListarProductosPorProveedor(int idProveedor)
        {
            List<ProductosDto> lista = new List<ProductosDto>();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_listar_productos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductosDto producto = new ProductosDto();

                                // Leer campos obligatorios
                                if (!reader.IsDBNull(reader.GetOrdinal("id_producto")))
                                    producto.IdProducto = (int)reader["id_producto"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre")))
                                    producto.Nombre = reader["nombre"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("precio")))
                                    producto.Precio = (decimal)reader["precio"];

                                if (!reader.IsDBNull(reader.GetOrdinal("stock")))
                                    producto.Stock = (int)reader["stock"];

                                if (!reader.IsDBNull(reader.GetOrdinal("id_categoria")))
                                    producto.IdCategoria = (int)reader["id_categoria"];

                                if (!reader.IsDBNull(reader.GetOrdinal("categoria")))
                                    producto.Categoria = reader["categoria"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("creado_por")))
                                    producto.CreadoPor = (int)reader["creado_por"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre_usuario")))
                                    producto.NombreUsuario = reader["nombre_usuario"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("fecha_creacion")))
                                    producto.FechaCreacion = (DateTime)reader["fecha_creacion"];

                                if (!reader.IsDBNull(reader.GetOrdinal("estado")))
                                    producto.Estado = (bool)reader["estado"];

                                if (!reader.IsDBNull(reader.GetOrdinal("id_proveedor")))
                                    producto.IdProveedor = (int)reader["id_proveedor"];

                                if (!reader.IsDBNull(reader.GetOrdinal("nombre_proveedor")))
                                    producto.NombreProveedor = reader["nombre_proveedor"].ToString();

                                // Solo agregar si el producto pertenece al proveedor seleccionado
                                if (producto.IdProveedor == idProveedor)
                                {
                                    lista.Add(producto);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos por proveedor: " + ex.Message);
            }
            return lista;
        }
    }
}