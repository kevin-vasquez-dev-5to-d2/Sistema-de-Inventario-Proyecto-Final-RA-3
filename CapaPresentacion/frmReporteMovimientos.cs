using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmReporteMovimientos : Form
    {
        private DataTable dtReporte;
        private string tipoMovimiento;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public frmReporteMovimientos(DataTable datos, string tipoMovimiento, DateTime fechaInicio, DateTime fechaFin)
        {
            InitializeComponent();
            this.tipoMovimiento = tipoMovimiento;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.dtReporte = datos;
        }

        private void frmReporteMovimientos_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarReporte();
                CargarDatosReporte();
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarReporte()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Reporte de Movimientos - Período: {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}");
            if (tipoMovimiento != "Todos")
                sb.AppendLine($"Tipo de Movimiento: {tipoMovimiento}");
            else
                sb.AppendLine("Tipo de Movimiento: ENTRADA Y SALIDA");

            lblFiltro.Text = sb.ToString();
        }

        private void CargarDatosReporte()
        {
            try
            {
                dgvReporte.DataSource = dtReporte;
                
                foreach (DataGridViewColumn column in dgvReporte.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            try
            {
                if (dtReporte == null || dtReporte.Rows.Count == 0)
                {
                    lblResumen.Text = "No hay datos para el reporte.";
                    return;
                }

                DataTable dtResumen = MovimientoBL.ObtenerResumenMovimientos(fechaInicio, fechaFin);
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("RESUMEN DEL REPORTE:");
                sb.AppendLine();

                decimal montoGrandTotal = 0;
                int cantidadGrandTotal = 0;

                foreach (DataRow row in dtResumen.Rows)
                {
                    string tipo = row["tipo_movimiento"].ToString();
                    int cantidadMovimientos = Convert.ToInt32(row["cantidad_movimientos"]);
                    int cantidadItems = row["cantidad_items"] != DBNull.Value ? Convert.ToInt32(row["cantidad_items"]) : 0;
                    int cantidadTotal = row["cantidad_total"] != DBNull.Value ? Convert.ToInt32(row["cantidad_total"]) : 0;
                    decimal montoTotal = row["monto_total"] != DBNull.Value ? Convert.ToDecimal(row["monto_total"]) : 0;

                    sb.AppendLine($"╔═══ {tipo} ═══════════════════════╗");
                    sb.AppendLine($"║ Movimientos: {cantidadMovimientos,-20} ║");
                    sb.AppendLine($"║ Ítems procesados: {cantidadItems,-16} ║");
                    sb.AppendLine($"║ Cantidad Total: {cantidadTotal,-18} ║");
                    sb.AppendLine($"║ Monto Total: {montoTotal.ToString("C2"),-20} ║");
                    sb.AppendLine("╚═══════════════════════════════════╝");
                    sb.AppendLine();

                    montoGrandTotal += montoTotal;
                    cantidadGrandTotal += cantidadTotal;
                }

                sb.AppendLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                sb.AppendLine($"┃ MONTO TOTAL GENERAL: {montoGrandTotal.ToString("C2"),-13} ┃");
                sb.AppendLine($"┃ CANTIDAD TOTAL: {cantidadGrandTotal,-20} ┃");
                sb.AppendLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

                lblResumen.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                lblResumen.Text = "Error al actualizar resumen: " + ex.Message;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                saveFileDialog.FileName = $"Reporte_Movimientos_{tipoMovimiento}_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportarACSV(saveFileDialog.FileName);
                    MessageBox.Show("Reporte exportado a CSV exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarACSV(string ruta)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ruta, false, Encoding.UTF8))
            {
                writer.WriteLine("REPORTE DE MOVIMIENTOS");
                writer.WriteLine($"Período: {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}");
                writer.WriteLine($"Tipo de Movimiento: {(tipoMovimiento == "Todos" ? "ENTRADA Y SALIDA" : tipoMovimiento)}");
                writer.WriteLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                writer.WriteLine();

                StringBuilder headerLine = new StringBuilder();
                foreach (DataGridViewColumn column in dgvReporte.Columns)
                {
                    headerLine.Append($"\"{column.HeaderText}\",");
                }
                writer.WriteLine(headerLine.ToString().TrimEnd(','));

                foreach (DataGridViewRow row in dgvReporte.Rows)
                {
                    StringBuilder dataLine = new StringBuilder();
                    foreach (DataGridViewColumn column in dgvReporte.Columns)
                    {
                        object cellValue = row.Cells[column.Index].Value ?? "";
                        dataLine.Append($"\"{cellValue}\",");
                    }
                    writer.WriteLine(dataLine.ToString().TrimEnd(','));
                }

                writer.WriteLine();
                writer.WriteLine("RESUMEN");
                writer.WriteLine(lblResumen.Text.Replace(Environment.NewLine, " | "));
            }
        }
    }
}
