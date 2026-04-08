using System;

namespace CapaEntidades
{
    public class MovimientoDto
    {
        public int IdMovimiento { get; set; }
        public string TipoMovimiento { get; set; } // "Entrada" o "Salida"
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int? IdProveedor { get; set; } // Para movimientos de ENTRADA
        public int? IdCliente { get; set; }   // Para movimientos de SALIDA
    }

    // Alias/plural class used in some UI code patterns in this project
    public class MovimientosDto : MovimientoDto
    {
    }
}
