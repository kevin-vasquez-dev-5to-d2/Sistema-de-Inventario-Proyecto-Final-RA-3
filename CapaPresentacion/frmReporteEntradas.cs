using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmReporteEntradas : Form
    {
        private DataTable dtReporte;
        private string usuario;
        private int idMovimiento;

        public frmReporteEntradas(int idMovimiento)
        {
            InitializeComponent();
            this.idMovimiento = idMovimiento;
            this.usuario = SesionDto.NombreUsuario;
        }

        private void frmReporteEntradas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporte()
        {
            try
            {
                // Obtener detalles del movimiento CON datos de productos (JOINs)
                // Esto trae: IdDetalle, IdMovimiento, IdProducto, NombreProducto, Cantidad, PrecioUnitario, Subtotal
                dtReporte = MovimientoBL.ObtenerDetallesMovimientoConProductos(idMovimiento);

                if (dtReporte == null || dtReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para este movimiento de entrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // Limpiar reportes anteriores
                reportViewer1.Clear();

                // Configurar el procesamiento local
                reportViewer1.ProcessingMode = ProcessingMode.Local;

                // Establecer la ruta del archivo RDLC
                string rutaRdlc = BuscarArchivoRDLC();

                if (string.IsNullOrEmpty(rutaRdlc))
                {
                    MessageBox.Show(
                        "No se encontró el archivo RDLC de reporte de entradas.\n\n" +
                        "El sistema buscó:\n" +
                        "- ReporteEntrada.rdlc\n" +
                        "- ReporteEntradas.rdlc\n" +
                        "- ReporteEntradaMovimiento.rdlc\n\n" +
                        "Por favor, asegúrate de que alguno de estos archivos exista en la carpeta 'Reportes'.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                reportViewer1.LocalReport.ReportPath = rutaRdlc;

                // Limpiar datasources anteriores
                reportViewer1.LocalReport.DataSources.Clear();

                // Determinar el nombre del DataSet
                string dataSetName = "DataSet1";
                try
                {
                    var doc = XDocument.Load(rutaRdlc);
                    var ns = doc.Root.Name.Namespace;
                    var ds = doc.Descendants(ns + "DataSet").FirstOrDefault();
                    if (ds != null && ds.Attribute("Name") != null)
                    {
                        dataSetName = ds.Attribute("Name").Value;
                    }
                }
                catch
                {
                    dataSetName = "DataSet1";
                }

                // Validar columnas
                List<string> columnasRd = new List<string>();
                try
                {
                    var doc = XDocument.Load(rutaRdlc);
                    var ns = doc.Root.Name.Namespace;
                    var campos = doc.Descendants(ns + "Field");
                    foreach (var f in campos)
                    {
                        var nameAttr = f.Attribute("Name");
                        if (nameAttr != null)
                            columnasRd.Add(nameAttr.Value);
                    }
                }
                catch { }

                // Agregar columnas faltantes
                foreach (var col in columnasRd)
                {
                    if (!dtReporte.Columns.Contains(col))
                    {
                        dtReporte.Columns.Add(col, typeof(string));
                    }
                }

                ReportDataSource rds = new ReportDataSource(dataSetName, dtReporte);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Agregar parámetros del reporte
                try
                {
                    List<ReportParameter> parametros = new List<ReportParameter>();

                    // Parámetro de fecha actual
                    parametros.Add(new ReportParameter("FechaActual", DateTime.Now.ToString("dd/MM/yyyy")));

                    // Parámetro del usuario creador del reporte
                    parametros.Add(new ReportParameter("UsuarioReporte", this.usuario));

                    reportViewer1.LocalReport.SetParameters(parametros);
                }
                catch
                {
                    // Si los parámetros no existen en el RDLC, continuar sin ellos
                }

                // Refrescar el reporte
                reportViewer1.RefreshReport();

                // Configurar el título de la ventana
                this.Text = "Reporte de Entrada de Inventario - ID: " + idMovimiento;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error configurando el reporte: " + ex.Message + "\n\nDetalles: " + ex.InnerException?.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca el archivo RDLC en múltiples ubicaciones
        /// </summary>
        private string BuscarArchivoRDLC()
        {
            string[] posiblesNombres = new[] { "ReporteEntrada.rdlc", "ReporteEntradas.rdlc", "ReporteEntradaMovimiento.rdlc", "ReporteM.rdlc" };

            foreach (var nombre in posiblesNombres)
            {
                // Opción 1: Buscar en Reportes\ (relativo a bin\Debug)
                var ruta1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes\\", nombre);
                if (System.IO.File.Exists(ruta1))
                {
                    return ruta1;
                }

                // Opción 2: Buscar en la carpeta del proyecto
                var ruta2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Reportes\\", nombre));
                if (System.IO.File.Exists(ruta2))
                {
                    return ruta2;
                }

                // Opción 3: Buscar directamente en bin\Debug\Reportes
                var ruta3 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombre);
                if (System.IO.File.Exists(ruta3))
                {
                    return ruta3;
                }
            }

            return null;
        }

        /// <summary>
        /// Convierte una lista de DetalleMovimientoDto a DataTable
        /// </summary>
        private DataTable ConvertirListaADataTable(List<DetalleMovimientoDto> lista)
        {
            DataTable dt = new DataTable();

            // Agregar columnas
            dt.Columns.Add("IdDetalle", typeof(int));
            dt.Columns.Add("IdMovimiento", typeof(int));
            dt.Columns.Add("IdProducto", typeof(int));
            dt.Columns.Add("NombreProducto", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("PrecioUnitario", typeof(decimal));
            dt.Columns.Add("Subtotal", typeof(decimal));

            // Agregar filas
            foreach (var item in lista)
            {
                DataRow row = dt.NewRow();
                row["IdDetalle"] = item.IdDetalle;
                row["IdMovimiento"] = item.IdMovimiento;
                row["IdProducto"] = item.IdProducto;
                row["NombreProducto"] = item.NombreProducto ?? "";
                row["Cantidad"] = item.Cantidad;
                row["PrecioUnitario"] = item.PrecioUnitario;
                row["Subtotal"] = item.Subtotal;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"Reporte_Entrada_{idMovimiento}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] bytes = reportViewer1.LocalReport.Render("PDF");
                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show("Reporte exportado exitosamente a: " + saveDialog.FileName,
                        "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"Reporte_Entrada_{idMovimiento}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] bytes = reportViewer1.LocalReport.Render("Excel");
                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show("Reporte exportado exitosamente a: " + saveDialog.FileName,
                        "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar Excel: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.PrintDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}