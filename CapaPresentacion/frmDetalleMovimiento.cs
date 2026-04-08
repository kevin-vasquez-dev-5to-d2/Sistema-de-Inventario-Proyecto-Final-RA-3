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
    public partial class frmDetalleMovimiento : Form
    {
        private int idMovimiento;
        private DataTable dtDetalles;

        public frmDetalleMovimiento(int idMovimiento)
        {
            InitializeComponent();
            this.idMovimiento = idMovimiento;
            ConfigurarDataGrid();
        }

        private void ConfigurarDataGrid()
        {
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.Columns.Clear();

            DataGridViewTextBoxColumn colIdDetalle = new DataGridViewTextBoxColumn();
            colIdDetalle.Name = "id_detalle";
            colIdDetalle.HeaderText = "ID Detalle";
            colIdDetalle.Width = 70;
            dgvDetalles.Columns.Add(colIdDetalle);

            DataGridViewTextBoxColumn colProducto = new DataGridViewTextBoxColumn();
            colProducto.Name = "nombre_producto";
            colProducto.HeaderText = "Producto";
            colProducto.Width = 200;
            dgvDetalles.Columns.Add(colProducto);

            DataGridViewTextBoxColumn colCategoria = new DataGridViewTextBoxColumn();
            colCategoria.Name = "nombre_categoria";
            colCategoria.HeaderText = "Categoría";
            colCategoria.Width = 120;
            dgvDetalles.Columns.Add(colCategoria);

            DataGridViewTextBoxColumn colCantidad = new DataGridViewTextBoxColumn();
            colCantidad.Name = "cantidad";
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Width = 80;
            dgvDetalles.Columns.Add(colCantidad);

            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.Name = "precio_unitario";
            colPrecio.HeaderText = "Precio Unit.";
            colPrecio.Width = 100;
            dgvDetalles.Columns.Add(colPrecio);

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn();
            colTotal.Name = "total";
            colTotal.HeaderText = "Total";
            colTotal.Width = 100;
            dgvDetalles.Columns.Add(colTotal);
        }

        private void frmDetalleMovimiento_Load(object sender, EventArgs e)
        {
            try
            {
                CargarInformacionMovimiento();
                CargarDetallesMovimiento();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarInformacionMovimiento()
        {
            try
            {
                MovimientoDto movimiento = MovimientoBL.ObtenerMovimientoPorIdSP(idMovimiento);
                if (movimiento != null)
                {
                    lblIdMovimiento.Text = movimiento.IdMovimiento.ToString();
                    lblTipo.Text = movimiento.TipoMovimiento;
                    lblFecha.Text = movimiento.Fecha.ToString("dd/MM/yyyy HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar información: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetallesMovimiento()
        {
            try
            {
                dtDetalles = MovimientoBL.ObtenerDetallesMovimientoConProductosSP(idMovimiento);
                dgvDetalles.DataSource = dtDetalles;
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            try
            {
                if (dtDetalles == null || dtDetalles.Rows.Count == 0)
                {
                    lblResumen.Text = "No hay detalles para este movimiento.";
                    return;
                }

                int cantidadItems = dtDetalles.Rows.Count;
                int cantidadTotal = 0;
                decimal montoTotal = 0;

                foreach (DataRow row in dtDetalles.Rows)
                {
                    if (row["cantidad"] != DBNull.Value)
                        cantidadTotal += Convert.ToInt32(row["cantidad"]);
                    if (row["total"] != DBNull.Value)
                        montoTotal += Convert.ToDecimal(row["total"]);
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Cantidad de Ítems: {cantidadItems}");
                sb.AppendLine($"Cantidad Total: {cantidadTotal} unidades");
                sb.AppendLine($"Monto Total: {montoTotal:C2}");

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
                if (dgvDetalles.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                saveFileDialog.FileName = $"Movimiento_{idMovimiento}_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportarACSV(saveFileDialog.FileName);
                    MessageBox.Show("Archivo exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                writer.WriteLine($"MOVIMIENTO #{idMovimiento}");
                writer.WriteLine($"Tipo: {lblTipo.Text}");
                writer.WriteLine($"Fecha: {lblFecha.Text}");
                writer.WriteLine();

                StringBuilder headerLine = new StringBuilder();
                foreach (DataGridViewColumn column in dgvDetalles.Columns)
                {
                    if (column.Visible)
                    {
                        headerLine.Append($"\"{column.HeaderText}\",");
                    }
                }
                writer.WriteLine(headerLine.ToString().TrimEnd(','));

                foreach (DataGridViewRow row in dgvDetalles.Rows)
                {
                    StringBuilder dataLine = new StringBuilder();
                    foreach (DataGridViewColumn column in dgvDetalles.Columns)
                    {
                        if (column.Visible)
                        {
                            object cellValue = row.Cells[column.Index].Value ?? "";
                            dataLine.Append($"\"{cellValue}\",");
                        }
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
