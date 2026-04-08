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
    public class MovimientoDAL
    {
        public static int InsertarMovimiento(MovimientoDto movimiento)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO MOVIMIENTOS (tipo_movimiento, fecha, id_usuario, id_proveedor, id_cliente) VALUES (@tipo_movimiento, @fecha, @id_usuario, @id_proveedor, @id_cliente); SELECT SCOPE_IDENTITY();", conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@tipo_movimiento", movimiento.TipoMovimiento);
                        cmd.Parameters.AddWithValue("@fecha", movimiento.Fecha);
                        cmd.Parameters.AddWithValue("@id_usuario", movimiento.IdUsuario);
                        cmd.Parameters.AddWithValue("@id_proveedor", movimiento.IdProveedor.HasValue ? (object)movimiento.IdProveedor.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_cliente", movimiento.IdCliente.HasValue ? (object)movimiento.IdCliente.Value : DBNull.Value);
                        conexion.Open();
                        int idMovimiento = Convert.ToInt32(cmd.ExecuteScalar());
                        return idMovimiento;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar movimiento: " + ex.Message);
            }
        }

        public static bool InsertarDetalleMovimiento(DetalleMovimientoDto detalle)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO DETALLE_MOVIMIENTOS (id_movimiento, id_producto, cantidad) VALUES (@id_movimiento, @id_producto, @cantidad)", conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_movimiento", detalle.IdMovimiento);
                        cmd.Parameters.AddWithValue("@id_producto", detalle.IdProducto);
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar detalle de movimiento: " + ex.Message);
            }
        }

        public static bool ActualizarStockProducto(int idProducto, int cantidad, string tipoMovimiento)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_actualizar_stock_producto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);
                        cmd.Parameters.AddWithValue("@cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@tipo_movimiento", tipoMovimiento);
                        conexion.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar stock del producto: " + ex.Message);
            }
        }

        public static MovimientoDto ObtenerMovimientoPorId(int idMovimiento)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT id_movimiento, tipo_movimiento, fecha, id_usuario FROM MOVIMIENTOS WHERE id_movimiento = @id_movimiento", conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_movimiento", idMovimiento);
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MovimientoDto movimiento = new MovimientoDto
                                {
                                    IdMovimiento = (int)reader["id_movimiento"],
                                    TipoMovimiento = reader["tipo_movimiento"].ToString(),
                                    Fecha = (DateTime)reader["fecha"],
                                    IdUsuario = (int)reader["id_usuario"]
                                };
                                return movimiento;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener movimiento: " + ex.Message);
            }
            return null;
        }

        public static List<DetalleMovimientoDto> ObtenerDetallesMovimiento(int idMovimiento)
        {
            List<DetalleMovimientoDto> lista = new List<DetalleMovimientoDto>();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_obtener_detalles_movimiento", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_movimiento", idMovimiento);
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DetalleMovimientoDto detalle = new DetalleMovimientoDto
                                {
                                    IdDetalle = (int)reader["id_detalle"],
                                    IdMovimiento = (int)reader["id_movimiento"],
                                    IdProducto = (int)reader["id_producto"],
                                    Cantidad = (int)reader["cantidad"]
                                };
                                lista.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles del movimiento: " + ex.Message);
            }
            return lista;
        }

        public static DataTable ObtenerTodosLosMovimientos()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerTodosLosMovimientos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los movimientos: " + ex.Message);
            }
            return dt;
        }

        public static DataTable ObtenerDetallesMovimientoConProductos(int idMovimiento)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_ENTRADAS", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles del movimiento con productos: " + ex.Message);
            }
            return dt;
        }

        public static DataTable ObtenerDetallesSalidaConProductos(int idMovimiento)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_SALIDAS", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles de salida con productos: " + ex.Message);
            }
            return dt;
        }

        public static DataTable ObtenerReporteMovimientosPorTipo(string tipoMovimiento, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerReporteMovimientosPorTipo", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tipoMovimiento", tipoMovimiento);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte de movimientos: " + ex.Message);
            }
            return dt;
        }

        public static DataTable ObtenerResumenMovimientos(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerResumenMovimientos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener resumen de movimientos: " + ex.Message);
            }
            return dt;
        }

        public static MovimientoDto ObtenerMovimientoPorIdSP(int idMovimiento)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerMovimientoPorId", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MovimientoDto movimiento = new MovimientoDto
                                {
                                    IdMovimiento = (int)reader["id_movimiento"],
                                    TipoMovimiento = reader["tipo_movimiento"].ToString(),
                                    Fecha = (DateTime)reader["fecha"],
                                    IdUsuario = (int)reader["id_usuario"]
                                };
                                return movimiento;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener movimiento: " + ex.Message);
            }
            return null;
        }

        public static bool ValidarAccesoMovimientos(string nombreUsuario)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ValidarAccesoMovimientos", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int tieneAcceso = Convert.ToInt32(reader["TieneAcceso"]);
                                return tieneAcceso == 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar acceso: " + ex.Message);
            }
            return false;
        }

        public static void RegistrarAccesoNoAutorizado(string nombreUsuario, string formulario, string razon)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarIntentoAccesoNoAutorizado", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@formulario", formulario);
                        cmd.Parameters.AddWithValue("@razon", razon);
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar intento de acceso: " + ex.Message);
            }
        }

        public static void RegistrarAccesoExitoso(string nombreUsuario, string formulario)
        {
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarAccesoExitoso", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@formulario", formulario);
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar acceso exitoso: " + ex.Message);
            }
        }
    }
}
