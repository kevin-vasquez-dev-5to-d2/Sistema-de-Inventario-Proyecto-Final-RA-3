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
    public partial class frmConsultaMovimientos : Form
    {
        private DataTable dtMovimientos;
        private string usuarioActual;

        public frmConsultaMovimientos()
        {
            InitializeComponent();
        }

        private void ConfigurarDataGrid()
        {
            dgvMovimientos.AutoGenerateColumns = false;
            dgvMovimientos.Columns.Clear();

            // Columna de ID (OCULTA)
            DataGridViewTextBoxColumn colIdMovimiento = new DataGridViewTextBoxColumn();
            colIdMovimiento.Name = "id_movimiento";
            colIdMovimiento.HeaderText = "ID";
            colIdMovimiento.DataPropertyName = "id_movimiento";
            colIdMovimiento.Width = 0;
            colIdMovimiento.Visible = false;
            dgvMovimientos.Columns.Add(colIdMovimiento);

            // Columna de Tipo (VISIBLE)
            DataGridViewTextBoxColumn colTipo = new DataGridViewTextBoxColumn();
            colTipo.Name = "tipo_movimiento";
            colTipo.HeaderText = "Tipo";
            colTipo.DataPropertyName = "tipo_movimiento";
            colTipo.Width = 80;
            colTipo.ReadOnly = true;
            dgvMovimientos.Columns.Add(colTipo);

            // Columna de Fecha (VISIBLE)
            DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
            colFecha.Name = "fecha";
            colFecha.HeaderText = "Fecha";
            colFecha.DataPropertyName = "fecha";
            colFecha.Width = 100;
            colFecha.ReadOnly = true;
            dgvMovimientos.Columns.Add(colFecha);

            // Columna de Usuario (VISIBLE)
            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn();
            colUsuario.Name = "usuario";
            colUsuario.HeaderText = "Usuario";
            colUsuario.DataPropertyName = "usuario";
            colUsuario.Width = 100;
            colUsuario.ReadOnly = true;
            dgvMovimientos.Columns.Add(colUsuario);

            // Columna de Cantidad de Ítems (VISIBLE)
            DataGridViewTextBoxColumn colItems = new DataGridViewTextBoxColumn();
            colItems.Name = "cantidad_items";
            colItems.HeaderText = "Ítems";
            colItems.DataPropertyName = "cantidad_items";
            colItems.Width = 60;
            colItems.ReadOnly = true;
            dgvMovimientos.Columns.Add(colItems);

            // Columna de Cantidad Total (VISIBLE)
            DataGridViewTextBoxColumn colCantidad = new DataGridViewTextBoxColumn();
            colCantidad.Name = "cantidad_total";
            colCantidad.HeaderText = "Cantidad Total";
            colCantidad.DataPropertyName = "cantidad_total";
            colCantidad.Width = 100;
            colCantidad.ReadOnly = true;
            dgvMovimientos.Columns.Add(colCantidad);

            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
        }

        private void frmConsultaMovimientos_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar el DataGrid primero
                ConfigurarDataGrid();

                // Validar que el usuario esté autenticado
                if (!SesionDto.Autenticado)
                {
                    MessageBox.Show("Debe iniciar sesión para acceder a este formulario.",
                        "Sesión Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // Obtener usuario actual de la sesión
                usuarioActual = SesionDto.Username;

                // Validar acceso usando ControlAccesoFormularios
                if (!ControlAccesoFormularios.TieneAcceso("frmConsultaMovimientos", usuarioActual))
                {
                    MessageBox.Show($"Acceso Denegado.\n\nUsuario actual: '{usuarioActual}'\n\nContacta al administrador si crees que esto es un error.",
                        "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    try
                    {
                        MovimientoBL.RegistrarAccesoNoAutorizado(usuarioActual, "frmConsultaMovimientos",
                            "Usuario no autorizado - intento de acceso denegado");
                    }
                    catch { }

                    this.Close();
                    return;
                }

                // Registrar acceso exitoso
                try
                {
                    MovimientoBL.RegistrarAccesoExitoso(usuarioActual, "frmConsultaMovimientos");
                }
                catch { }

                // Configurar fechas por defecto
                dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
                dtpFechaFin.Value = DateTime.Now;

                // Configurar combo de tipo de movimiento
                ConfigurarComboTipo();

                // Cargar todos los movimientos
                CargarTodosLosMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar movimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarComboTipo()
        {
            if (cmbTipoMovimiento.Items.Count == 0)
            {
                cmbTipoMovimiento.Items.Add("Todos");
                cmbTipoMovimiento.Items.Add("ENTRADA");
                cmbTipoMovimiento.Items.Add("SALIDA");
                cmbTipoMovimiento.SelectedIndex = 0;
            }
        }

        private bool ValidarAccesoUsuario()
        {
            try
            {
                return MovimientoBL.ValidarAccesoMovimientos(usuarioActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar acceso: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CargarTodosLosMovimientos()
        {
            try
            {
                dtMovimientos = MovimientoBL.ObtenerTodosLosMovimientos();

                if (dtMovimientos != null && dtMovimientos.Rows.Count > 0)
                {
                    dgvMovimientos.DataSource = dtMovimientos;
                    ActualizarResumen();
                    lblTotal.Text = $"Se cargaron {dtMovimientos.Rows.Count} movimientos";
                }
                else
                {
                    dgvMovimientos.DataSource = null;
                    lblTotal.Text = "RESUMEN DE MOVIMIENTOS:\n\nNo hay datos disponibles.";
                    MessageBox.Show("No hay movimientos registrados en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                dgvMovimientos.DataSource = null;
                lblTotal.Text = "Error al cargar datos";
                MessageBox.Show("Error al cargar movimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTodosLosMovimientos();
        }

        public void RefrescarDatos()
        {
            CargarTodosLosMovimientos();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoMovimiento.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecciona un tipo de movimiento para filtrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tipoMovimiento = cmbTipoMovimiento.SelectedItem.ToString();
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show("La fecha inicio no puede ser mayor que la fecha fin.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dtFiltrado = null;

                if (tipoMovimiento == "Todos")
                {
                    // Obtener entrada y salida
                    DataTable dt1 = MovimientoBL.ObtenerReporteMovimientosPorTipo("ENTRADA", fechaInicio, fechaFin);
                    DataTable dt2 = MovimientoBL.ObtenerReporteMovimientosPorTipo("SALIDA", fechaInicio, fechaFin);

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        dtFiltrado = dt1.Copy();
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt2.Rows)
                            {
                                dtFiltrado.ImportRow(row);
                            }
                        }
                    }
                    else if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        dtFiltrado = dt2.Copy();
                    }
                }
                else
                {
                    dtFiltrado = MovimientoBL.ObtenerReporteMovimientosPorTipo(tipoMovimiento, fechaInicio, fechaFin);
                }

                if (dtFiltrado != null && dtFiltrado.Rows.Count > 0)
                {
                    dgvMovimientos.DataSource = dtFiltrado;
                    ActualizarResumen();
                    MessageBox.Show($"Se encontraron {dtFiltrado.Rows.Count} movimientos de tipo '{tipoMovimiento}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvMovimientos.DataSource = null;
                    lblTotal.Text = "RESUMEN DE MOVIMIENTOS:\n\nNo hay datos disponibles para el filtro seleccionado.";
                    MessageBox.Show($"No hay movimientos de tipo '{tipoMovimiento}' entre las fechas seleccionadas.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aplicar filtro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMovimientos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecciona un movimiento para ver sus detalles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idMovimiento = Convert.ToInt32(dgvMovimientos.SelectedRows[0].Cells["id_movimiento"].Value);
                MostrarDetalleMovimiento(idMovimiento);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleMovimiento(int idMovimiento)
        {
            try
            {
                // frmDetalleMovimiento frmDetalle = new frmDetalleMovimiento(idMovimiento);
                // frmDetalle.ShowDialog();
                MessageBox.Show("Función de detalle no disponible aún.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir ventana de detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMovimientos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecciona un movimiento para ver el reporte.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idMovimiento = Convert.ToInt32(dgvMovimientos.SelectedRows[0].Cells["id_movimiento"].Value);
                string tipoMovimiento = dgvMovimientos.SelectedRows[0].Cells["tipo_movimiento"].Value.ToString();

                // Si es una entrada, mostrar el reporte de entradas
                if (tipoMovimiento.ToUpper() == "ENTRADA")
                {
                    frmReporteEntradas frmReporte = new frmReporteEntradas(idMovimiento);
                    frmReporte.ShowDialog();
                }
                // Si es una salida, mostrar el reporte de salidas
                else if (tipoMovimiento.ToUpper() == "SALIDA")
                {
                    frmReporteSalidas frmReporte = new frmReporteSalidas(idMovimiento);
                    frmReporte.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tipo de movimiento no reconocido: " + tipoMovimiento, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            try
            {
                DataTable dtResumen = MovimientoBL.ObtenerResumenMovimientos(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);

                if (dtResumen == null || dtResumen.Rows.Count == 0)
                {
                    lblTotal.Text = "RESUMEN DE MOVIMIENTOS:\n\nNo hay datos disponibles.";
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("RESUMEN DE MOVIMIENTOS:");
                sb.AppendLine();

                foreach (DataRow row in dtResumen.Rows)
                {
                    string tipoMovimiento = row["tipo_movimiento"].ToString();
                    int cantidadMovimientos = Convert.ToInt32(row["cantidad_movimientos"]);
                    int cantidadItems = row["cantidad_items"] != DBNull.Value ? Convert.ToInt32(row["cantidad_items"]) : 0;
                    int cantidadTotal = row["cantidad_total"] != DBNull.Value ? Convert.ToInt32(row["cantidad_total"]) : 0;
                    decimal montoTotal = row["monto_total"] != DBNull.Value ? Convert.ToDecimal(row["monto_total"]) : 0;

                    sb.AppendLine($"{tipoMovimiento}:");
                    sb.AppendLine($"  • Movimientos: {cantidadMovimientos}");
                    sb.AppendLine($"  • Ítems: {cantidadItems}");
                    sb.AppendLine($"  • Cantidad Total: {cantidadTotal}");
                    sb.AppendLine($"  • Monto Total: {montoTotal:C2}");
                    sb.AppendLine();
                }

                lblTotal.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                lblTotal.Text = "Error al actualizar resumen: " + ex.Message;
            }
        }

        private void dgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvMovimientos.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}