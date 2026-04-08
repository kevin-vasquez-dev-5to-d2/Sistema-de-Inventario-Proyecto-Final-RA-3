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
    public class ClienteDAL
    {
        public static List<ClientesDto> ListarClientes()
        {
            List<ClientesDto> clientes = new List<ClientesDto>();

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("sp_ListarClientes", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new ClientesDto
                                {
                                    IdCliente = Convert.ToInt32(reader["id_cliente"]),
                                    TipoCliente = reader["tipo_cliente"].ToString(),
                                    Nombre = reader["nombre"].ToString(),
                                    Apellido = reader["apellido"].ToString(),
                                    NombreEmpresa = reader["nombre_empresa"].ToString(),
                                    Cedula = reader["cedula"].ToString(),
                                    Rnc = reader["rnc"].ToString(),
                                    Telefono = reader["telefono"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Direccion = reader["direccion"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes: " + ex.Message);
            }

            return clientes;
        }

        public static bool InsertarCliente(ClientesDto cliente)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("sp_InsertarCliente", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@tipo_cliente", cliente.TipoCliente);
                        comando.Parameters.AddWithValue("@nombre", cliente.Nombre ?? "");
                        comando.Parameters.AddWithValue("@apellido", cliente.Apellido ?? "");
                        comando.Parameters.AddWithValue("@nombre_empresa", cliente.NombreEmpresa ?? "");
                        comando.Parameters.AddWithValue("@cedula", cliente.Cedula ?? "");
                        comando.Parameters.AddWithValue("@rnc", cliente.Rnc ?? "");
                        comando.Parameters.AddWithValue("@telefono", cliente.Telefono ?? "");
                        comando.Parameters.AddWithValue("@email", cliente.Email ?? "");
                        comando.Parameters.AddWithValue("@direccion", cliente.Direccion ?? "");
                        comando.Parameters.AddWithValue("@estado", cliente.Estado);

                        var resultado = comando.ExecuteScalar();
                        return resultado != null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar cliente: " + ex.Message);
            }
        }

        public static bool ActualizarCliente(ClientesDto cliente)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("sp_ActualizarCliente", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@id_cliente", cliente.IdCliente);
                        comando.Parameters.AddWithValue("@tipo_cliente", cliente.TipoCliente);
                        comando.Parameters.AddWithValue("@nombre", cliente.Nombre ?? "");
                        comando.Parameters.AddWithValue("@apellido", cliente.Apellido ?? "");
                        comando.Parameters.AddWithValue("@nombre_empresa", cliente.NombreEmpresa ?? "");
                        comando.Parameters.AddWithValue("@cedula", cliente.Cedula ?? "");
                        comando.Parameters.AddWithValue("@rnc", cliente.Rnc ?? "");
                        comando.Parameters.AddWithValue("@telefono", cliente.Telefono ?? "");
                        comando.Parameters.AddWithValue("@email", cliente.Email ?? "");
                        comando.Parameters.AddWithValue("@direccion", cliente.Direccion ?? "");
                        comando.Parameters.AddWithValue("@estado", cliente.Estado);

                        var resultado = comando.ExecuteScalar();
                        return (int)resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente: " + ex.Message);
            }
        }

        public static bool EliminarCliente(int idCliente)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("sp_EliminarCliente", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_cliente", idCliente);

                        var resultado = comando.ExecuteScalar();
                        return (int)resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }
    }
}