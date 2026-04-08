using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProveedoresDAL
    {
        public static bool InsertarProveedor(ProveedoresDto proveedor)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_proveedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre_proveedor", proveedor.NombreProveedor);
                        cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                        cmd.Parameters.AddWithValue("@email", proveedor.Email);
                        cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion);
                        cmd.Parameters.AddWithValue("@estado", proveedor.Estado);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar proveedor: " + ex.Message);
            }
        }

        public static List<ProveedoresDto> ListarProveedores()
        {
            List<ProveedoresDto> lista = new List<ProveedoresDto>();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_listar_proveedores", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProveedoresDto proveedor = new ProveedoresDto
                                {
                                    IdProveedor = (int)reader["id_proveedor"],
                                    NombreProveedor = reader["nombre_proveedor"].ToString(),
                                    Telefono = reader["telefono"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Direccion = reader["direccion"].ToString(),
                                    Estado = (bool)reader["estado"]
                                };
                                lista.Add(proveedor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar proveedores: " + ex.Message);
            }
            return lista;
        }

        public static bool ActualizarProveedor(ProveedoresDto proveedor)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_actualizar_proveedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_proveedor", proveedor.IdProveedor);
                        cmd.Parameters.AddWithValue("@nombre_proveedor", proveedor.NombreProveedor);
                        cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                        cmd.Parameters.AddWithValue("@email", proveedor.Email);
                        cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion);
                        cmd.Parameters.AddWithValue("@estado", proveedor.Estado);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar proveedor: " + ex.Message);
            }
        }

        public static bool EliminarProveedor(int idProveedor)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_eliminar_proveedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar proveedor: " + ex.Message);
            }
        }
    }
}