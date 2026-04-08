using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MovimientoBL
    {
        public static int InsertarMovimiento(MovimientoDto movimiento)
        {
            if (string.IsNullOrWhiteSpace(movimiento.TipoMovimiento))
                throw new ArgumentException("El tipo de movimiento es requerido.");
            if (movimiento.TipoMovimiento.ToUpper() != "ENTRADA" && movimiento.TipoMovimiento.ToUpper() != "SALIDA")
                throw new ArgumentException("El tipo de movimiento debe ser 'ENTRADA' o 'SALIDA'.");
            if (movimiento.IdUsuario <= 0)
                throw new ArgumentException("El usuario es requerido.");

            return MovimientoDAL.InsertarMovimiento(movimiento);
        }

        public static bool InsertarDetalleMovimiento(DetalleMovimientoDto detalle)
        {
            if (detalle.IdMovimiento <= 0)
                throw new ArgumentException("El movimiento es requerido.");
            if (detalle.IdProducto <= 0)
                throw new ArgumentException("El producto es requerido.");
            if (detalle.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            return MovimientoDAL.InsertarDetalleMovimiento(detalle);
        }

        public static bool ActualizarStockProducto(int idProducto, int cantidad, string tipoMovimiento)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El producto es inválido.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            return MovimientoDAL.ActualizarStockProducto(idProducto, cantidad, tipoMovimiento);
        }

        public static MovimientoDto ObtenerMovimientoPorId(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerMovimientoPorId(idMovimiento);
        }

        public static List<DetalleMovimientoDto> ObtenerDetallesMovimiento(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerDetallesMovimiento(idMovimiento);
        }

        public static DataTable ObtenerTodosLosMovimientos()
        {
            return MovimientoDAL.ObtenerTodosLosMovimientos();
        }

        public static DataTable ObtenerDetallesMovimientoConProductos(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerDetallesMovimientoConProductos(idMovimiento);
        }

        public static DataTable ObtenerDetallesSalidaConProductos(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerDetallesSalidaConProductos(idMovimiento);
        }

        public static DataTable ObtenerReporteMovimientosPorTipo(string tipoMovimiento, DateTime fechaInicio, DateTime fechaFin)
        {
            if (string.IsNullOrWhiteSpace(tipoMovimiento))
                throw new ArgumentException("El tipo de movimiento es requerido.");
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin.");

            return MovimientoDAL.ObtenerReporteMovimientosPorTipo(tipoMovimiento, fechaInicio, fechaFin);
        }

        public static DataTable ObtenerResumenMovimientos(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin.");

            return MovimientoDAL.ObtenerResumenMovimientos(fechaInicio, fechaFin);
        }

        public static MovimientoDto ObtenerMovimientoPorIdSP(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerMovimientoPorIdSP(idMovimiento);
        }

        public static DataTable ObtenerDetallesMovimientoConProductosSP(int idMovimiento)
        {
            if (idMovimiento <= 0)
                throw new ArgumentException("El ID del movimiento es inválido.");

            return MovimientoDAL.ObtenerDetallesMovimientoConProductos(idMovimiento);
        }

        public static DataTable ObtenerReporteMovimientosPorTipoSP(string tipoMovimiento, DateTime fechaInicio, DateTime fechaFin)
        {
            if (string.IsNullOrWhiteSpace(tipoMovimiento))
                throw new ArgumentException("El tipo de movimiento es requerido.");
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha inicio no puede ser mayor que la fecha fin.");

            return MovimientoDAL.ObtenerReporteMovimientosPorTipo(tipoMovimiento, fechaInicio, fechaFin);
        }

        public static DataTable ObtenerTodosLosMovimientosSP()
        {
            return MovimientoDAL.ObtenerTodosLosMovimientos();
        }

        public static bool ValidarAccesoMovimientos(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es requerido.");

            return MovimientoDAL.ValidarAccesoMovimientos(nombreUsuario);
        }

        public static void RegistrarAccesoNoAutorizado(string nombreUsuario, string formulario, string razon)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es requerido.");
            if (string.IsNullOrWhiteSpace(formulario))
                throw new ArgumentException("El nombre del formulario es requerido.");

            MovimientoDAL.RegistrarAccesoNoAutorizado(nombreUsuario, formulario, razon);
        }

        public static void RegistrarAccesoExitoso(string nombreUsuario, string formulario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es requerido.");
            if (string.IsNullOrWhiteSpace(formulario))
                throw new ArgumentException("El nombre del formulario es requerido.");

            MovimientoDAL.RegistrarAccesoExitoso(nombreUsuario, formulario);
        }
    }
}
