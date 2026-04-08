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

namespace CapaPresentacion
{
    public partial class frmReporteMovimientosRdlc : Form
    {
        private DataTable dtReporte;
        private string usuario;

        public frmReporteMovimientosRdlc(DataTable datos)
        {
            InitializeComponent();
            this.dtReporte = datos;
            this.usuario = SesionDto.NombreUsuario;
        }

        private void frmReporteMovimientosRdlc_Load(object sender, EventArgs e)
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
                // Limpiar reportes anteriores
                reportViewer1.Clear();

                // Configurar el procesamiento local
                reportViewer1.ProcessingMode = ProcessingMode.Local;

                // Establecer la ruta del archivo RDLC (buscar en múltiples ubicaciones y nombres)
                string rutaRdlc = "";

                string[] posiblesNombres = new[] { "ReporteM.rdlc", "ReporteMovimiento.rdlc", "ReporteMovimientos.rdlc", "ReporteMovimientoos.rdlc" };

                foreach (var nombre in posiblesNombres)
                {
                    // Opción 1: Buscar en Reportes\ (relativo a bin\Debug)
                    var ruta1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes\\", nombre);
                    if (System.IO.File.Exists(ruta1))
                    {
                        rutaRdlc = ruta1;
                        break;
                    }

                    // Opción 2: Buscar en la carpeta del proyecto (..\..\Reportes)
                    var ruta2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Reportes\\", nombre));
                    if (System.IO.File.Exists(ruta2))
                    {
                        rutaRdlc = ruta2;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(rutaRdlc))
                {
                    MessageBox.Show("No se encontró el archivo RDLC de reporte. Buscado: ReporteM.rdlc, ReporteMovimiento.rdlc, ReporteMovimientos.rdlc, ReporteMovimientoos.rdlc", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = rutaRdlc;

                // Mostrar ruta y esquema detectado en caso de depuración
                try
                {
                    var content = File.ReadAllText(rutaRdlc, Encoding.UTF8);
                    var head = content.Length > 200 ? content.Substring(0, 200) : content;
                    // solo para depuración durante ejecución local - no bloquear la UI
                    System.Diagnostics.Trace.WriteLine($"RDLC cargado: {rutaRdlc}");
                    System.Diagnostics.Trace.WriteLine(head);
                }
                catch { }

                // Limpiar datasources anteriores
                reportViewer1.LocalReport.DataSources.Clear();

                // Determinar el nombre del DataSet definido en el RDLC (para evitar desajustes entre distintos archivos)
                string dataSetName = "DataSet1";
                try
                {
                    var doc = System.Xml.Linq.XDocument.Load(rutaRdlc);
                    var ns = doc.Root.Name.Namespace;
                    var ds = doc.Descendants(ns + "DataSet").FirstOrDefault();
                    if (ds != null && ds.Attribute("Name") != null)
                    {
                        dataSetName = ds.Attribute("Name").Value;
                    }
                    else
                    {
                        // si no se detecta DataSet, intentar leer DataSets en la estructura 2008
                        var ds2 = doc.Descendants(doc.Root.Name.Namespace + "DataSets").Descendants(doc.Root.Name.Namespace + "DataSet").FirstOrDefault();
                        if (ds2 != null && ds2.Attribute("Name") != null)
                        {
                            dataSetName = ds2.Attribute("Name").Value;
                        }
                    }
                }
                catch
                {
                    // Si hay problema leyendo el RDLC, mantenemos el nombre por defecto
                    dataSetName = "DataSet1";
                }

                // Validar que las columnas del DataTable coincidan con las esperadas por el RDLC
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

                // Si faltan columnas en dtReporte, intentar renombrarlas o crear columnas vacías
                foreach (var col in columnasRd)
                {
                    if (!dtReporte.Columns.Contains(col))
                    {
                        // crear columna vacía para evitar errores de binding
                        dtReporte.Columns.Add(col, typeof(string));
                    }
                }

                ReportDataSource rds = new ReportDataSource(dataSetName, dtReporte);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Agregar parámetro del usuario creador del reporte
                try
                {
                    ReportParameter paramUsuario = new ReportParameter("UsuarioReporte", this.usuario);
                    reportViewer1.LocalReport.SetParameters(paramUsuario);
                }
                catch
                {
                    // Si el parámetro no existe en el RDLC, continuar sin él
                }

                // Refrescar el reporte
                reportViewer1.RefreshReport();

                // Configurar el título de la ventana
                this.Text = "Reporte de Movimiento de Inventario";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error configurando el reporte: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"Reporte_Movimiento_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

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
                saveDialog.FileName = $"Reporte_Movimiento_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

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