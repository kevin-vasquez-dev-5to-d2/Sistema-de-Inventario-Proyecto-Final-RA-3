using System;

namespace CapaEntidades
{
    public class DetalleMovimientoDto
    {
        public int IdDetalle { get; set; }
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    // Alias/plural class matching project conventions
    public class DetalleMovimientosDto : DetalleMovimientoDto
    {
    }
}
