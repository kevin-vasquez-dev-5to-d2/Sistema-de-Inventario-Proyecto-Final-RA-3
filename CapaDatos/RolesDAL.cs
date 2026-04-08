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
    public class RolesDAL
    {
        public static bool InsertarRol(RolesDto rol)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_rol", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre_rol", rol.NombreRol);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar rol: " + ex.Message);
            }
        }

        public static List<RolesDto> ListarRoles()
        {
            List<RolesDto> lista = new List<RolesDto>();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_listar_roles", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RolesDto rol = new RolesDto
                                {
                                    IdRol = (int)reader["id_rol"],
                                    NombreRol = reader["nombre_rol"].ToString()
                                };
                                lista.Add(rol);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar roles: " + ex.Message);
            }
            return lista;
        }

        public static bool ActualizarRol(RolesDto rol)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_actualizar_rol", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_rol", rol.IdRol);
                        cmd.Parameters.AddWithValue("@nombre_rol", rol.NombreRol);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar rol: " + ex.Message);
            }
        }

        public static bool EliminarRol(int idRol)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_eliminar_rol", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_rol", idRol);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar rol: " + ex.Message);
            }
        }
    }
}