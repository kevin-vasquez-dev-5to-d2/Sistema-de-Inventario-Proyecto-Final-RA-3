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
    public partial class frmMovimiento : Form
    {
        private List<DetalleMovimientoDto> detallesMovimiento = new List<DetalleMovimientoDto>();
        private List<ProveedoresDto> proveedores = new List<ProveedoresDto>();
        private List<ClientesDto> clientes = new List<ClientesDto>();
        private List<ProductosDto> productos = new List<ProductosDto>();
        private List<CategoriaDto> categorias = new List<CategoriaDto>();
        private int filaEnEdicion = -1;  // Rastrear la fila que está siendo editada

        public frmMovimiento()
        {
            InitializeComponent();
            ConfigurarDataGrid();
        }

        private void ConfigurarDataGrid()
        {
            dgvMovimientos.AutoGenerateColumns = false;
            dgvMovimientos.Columns.Clear();

            // Columna oculta para almacenar el ID del producto (necesario para operaciones)
            DataGridViewTextBoxColumn colIdProducto = new DataGridViewTextBoxColumn();
            colIdProducto.Name = "IdProducto";
            colIdProducto.HeaderText = "IdProducto";
            colIdProducto.Visible = false;
            dgvMovimientos.Columns.Add(colIdProducto);

            // Columna oculta para almacenar el ID de la categoría
            DataGridViewTextBoxColumn colIdCategoria = new DataGridViewTextBoxColumn();
            colIdCategoria.Name = "IdCategoria";
            colIdCategoria.HeaderText = "IdCategoria";
            colIdCategoria.Visible = false;
            dgvMovimientos.Columns.Add(colIdCategoria);

            // Columna visible del nombre del producto
            DataGridViewTextBoxColumn colNombreProducto = new DataGridViewTextBoxColumn();
            colNombreProducto.Name = "NombreProducto";
            colNombreProducto.HeaderText = "Producto";
            colNombreProducto.Width = 200;
            dgvMovimientos.Columns.Add(colNombreProducto);

            // Columna de categoría
            DataGridViewTextBoxColumn colNombreCategoria = new DataGridViewTextBoxColumn();
            colNombreCategoria.Name = "NombreCategoria";
            colNombreCategoria.HeaderText = "Categoría";
            colNombreCategoria.Width = 150;
            dgvMovimientos.Columns.Add(colNombreCategoria);

            // Columna de cantidad
            DataGridViewTextBoxColumn colCantidad = new DataGridViewTextBoxColumn();
            colCantidad.Name = "Cantidad";
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Width = 100;
            dgvMovimientos.Columns.Add(colCantidad);

            // Agregar evento de clic en el datagrid para editar filas
            dgvMovimientos.CellClick += DgvMovimientos_CellClick;
        }

        private void frmMovimiento_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            // Configurar fecha: siempre la actual
            dtpFecha.Value = DateTime.Now;
            dtpFecha.Enabled = false;  // No permitir cambios
            CargarTiposMovimiento();
            CargarProvedores();
            CargarClientes();
            CargarProductos();
            CargarCategorias();
            CargarSesionUsuario();
            ConfigurarVisibilidadCampos();
            LimpiarControles();
        }

        private void ConfigurarVisibilidadCampos()
        {
            // Por defecto, mostrar Proveedor (ENTRADA)
            label4.Text = "Proveedor:";
            label4.Visible = true;
            cmbProveedor.Visible = true;
            labelCliente.Visible = false;
            cmbCliente.Visible = false;
            labelMotivo.Visible = false;
            cmbMotivo.Visible = false;
        }

        private void cmbTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoMovimiento.SelectedIndex == -1)
                return;

            string tipoMovimiento = cmbTipoMovimiento.SelectedItem.ToString();

            if (tipoMovimiento == "ENTRADA")
            {
                // Mostrar Proveedor, ocultar Cliente y Motivo
                label4.Visible = true;
                cmbProveedor.Visible = true;
                labelCliente.Visible = false;
                cmbCliente.Visible = false;
                labelMotivo.Visible = false;
                cmbMotivo.Visible = false;

                cmbProveedor.SelectedIndex = -1;
                cmbCliente.SelectedIndex = -1;
                cmbMotivo.SelectedIndex = -1;
            }
            else if (tipoMovimiento == "SALIDA")
            {
                // Mostrar Cliente y Motivo, ocultar Proveedor
                label4.Visible = false;
                cmbProveedor.Visible = false;
                labelCliente.Visible = true;
                cmbCliente.Visible = true;
                labelMotivo.Visible = true;
                cmbMotivo.Visible = true;

                cmbProveedor.SelectedIndex = -1;
                cmbCliente.SelectedIndex = -1;
                cmbMotivo.SelectedIndex = -1;
            }
        }

        private void CargarClientes()
        {
            try
            {
                // Lista de clientes disponibles
                clientes = new List<ClientesDto>
                {
                    new ClientesDto { IdCliente = 1, Nombre = "Cliente 1", TipoCliente = "FISICO", Estado = true },
                    new ClientesDto { IdCliente = 2, Nombre = "Cliente 2", TipoCliente = "EMPRESA", Estado = true },
                    new ClientesDto { IdCliente = 3, Nombre = "Cliente 3", TipoCliente = "FISICO", Estado = true }
                };

                if (clientes != null && clientes.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = clientes;
                    cmbCliente.DataSource = bs;
                    cmbCliente.DisplayMember = "Nombre";
                    cmbCliente.ValueMember = "IdCliente";
                    cmbCliente.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientes = new List<ClientesDto>();
            }
        }

        private void CargarSesionUsuario()
        {
            try
            {
                if (SesionDto.Autenticado)
                {
                    lblNombreUsuario.Text = SesionDto.NombreUsuario;
                    lblUsernameUsuario.Text = "@" + SesionDto.Username;
                    lblRolUsuario.Text = SesionDto.NombreRol.ToUpper();
                }
                else
                {
                    lblNombreUsuario.Text = "No autenticado";
                    lblUsernameUsuario.Text = "";
                    lblRolUsuario.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar información del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTiposMovimiento()
        {
            try
            {
                cmbTipoMovimiento.Items.Clear();
                cmbTipoMovimiento.Items.Add("ENTRADA");
                cmbTipoMovimiento.Items.Add("SALIDA");
                cmbTipoMovimiento.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de movimiento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProvedores()
        {
            try
            {
                proveedores = ProveedoresBL.ListarProveedores();
                if (proveedores != null && proveedores.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = proveedores;
                    cmbProveedor.DataSource = bs;
                    cmbProveedor.DisplayMember = "NombreProveedor";
                    cmbProveedor.ValueMember = "IdProveedor";
                    cmbProveedor.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No hay proveedores disponibles en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Crear una lista vacía para evitar problemas
                proveedores = new List<ProveedoresDto>();
            }
        }

        private void CargarProductos()
        {
            try
            {
                productos = ProductoBL.ListarProductos();
                if (productos != null && productos.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = productos;
                    cmbProducto.DataSource = bs;
                    cmbProducto.DisplayMember = "Nombre";
                    cmbProducto.ValueMember = "IdProducto";
                    cmbProducto.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No hay productos disponibles en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Crear una lista vacía para evitar problemas
                productos = new List<ProductosDto>();
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex == -1)
            {
                // Si no hay proveedor seleccionado, mostrar todos los productos
                CargarProductos();
                cmbProducto.SelectedIndex = -1;
                return;
            }

            try
            {
                // Obtener el proveedor seleccionado directamente
                ProveedoresDto proveedorSeleccionado = (ProveedoresDto)cmbProveedor.SelectedItem;
                int idProveedor = proveedorSeleccionado.IdProveedor;

                // Obtener productos del proveedor seleccionado
                List<ProductosDto> productosProveedor = ProductoBL.ListarProductosPorProveedor(idProveedor);

                if (productosProveedor != null && productosProveedor.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = productosProveedor;
                    cmbProducto.DataSource = bs;
                    cmbProducto.DisplayMember = "Nombre";
                    cmbProducto.ValueMember = "IdProducto";
                    cmbProducto.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Este proveedor no tiene productos disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbProducto.DataSource = null;
                    cmbProducto.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                categorias = CategoriaBL.ListarCategorias();
                if (categorias != null && categorias.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = categorias;
                    cmbCategoria.DataSource = bs;
                    cmbCategoria.DisplayMember = "Nombre";
                    cmbCategoria.ValueMember = "IdCategoria";
                    cmbCategoria.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No hay categorías disponibles en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Crear una lista vacía para evitar problemas
                categorias = new List<CategoriaDto>();
            }
        }

        private bool ValidarCampos()
        {
            // Validar Tipo de Movimiento
            if (cmbTipoMovimiento.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de movimiento (ENTRADA o SALIDA).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoMovimiento.Focus();
                return false;
            }

            string tipoMovimiento = cmbTipoMovimiento.SelectedItem.ToString();

            // Validaciones específicas por tipo de movimiento
            if (tipoMovimiento == "ENTRADA")
            {
                if (cmbProveedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbProveedor.Focus();
                    return false;
                }
            }
            else if (tipoMovimiento == "SALIDA")
            {
                if (cmbCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCliente.Focus();
                    return false;
                }

                if (cmbMotivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un motivo de salida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMotivo.Focus();
                    return false;
                }
            }

            // Validar Fecha
            // Nota: La fecha ahora es controlada automáticamente por el sistema
            // Se usa siempre DateTime.Now y el control está deshabilitado

            // Validar usuario autenticado
            if (!SesionDto.Autenticado)
            {
                MessageBox.Show("Debe estar autenticado para realizar esta operación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que haya al menos un producto en el datagrid
            if (dgvMovimientos.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidarProducto()
        {
            // Validar Producto seleccionado
            if (cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProducto.Focus();
                return false;
            }

            // Validar Categoría seleccionada
            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                return false;
            }

            // Validar Cantidad
            if (txtCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número válido mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return false;
            }

            // Validar cantidad vs stock para SALIDA
            string tipoMovimiento = cmbTipoMovimiento.SelectedItem != null ? cmbTipoMovimiento.SelectedItem.ToString() : "";
            if (tipoMovimiento == "SALIDA")
            {
                int idProducto = Convert.ToInt32(cmbProducto.SelectedValue);
                var producto = productos.FirstOrDefault(p => p.IdProducto == idProducto);

                if (producto != null && txtCantidad.Value > producto.Stock)
                {
                    MessageBox.Show(
                        $"La cantidad ingresada ({txtCantidad.Value}) es superior al stock disponible ({producto.Stock}).\n\n" +
                        $"Por favor, ingrese una cantidad menor o igual al stock actual.",
                        "Validación - Stock Insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCantidad.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Si está en modo edición, guardar los cambios de la fila
            if (filaEnEdicion >= 0)
            {
                if (!ValidarProducto())
                    return;

                try
                {
                    int idProducto = Convert.ToInt32(cmbProducto.SelectedValue);
                    int idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                    int cantidad = (int)txtCantidad.Value;

                    // Actualizar el detalle en la lista
                    detallesMovimiento[filaEnEdicion].IdProducto = idProducto;
                    detallesMovimiento[filaEnEdicion].IdCategoria = idCategoria;
                    detallesMovimiento[filaEnEdicion].Cantidad = cantidad;

                    ActualizarDataGrid();
                    filaEnEdicion = -1;
                    LimpiarProductos();

                    // Restaurar el botón a su estado original
                    btnAgregarProducto.Text = "+ Agregar Producto";
                    btnAgregarProducto.BackColor = SystemColors.Control;

                    MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Modo normal: agregar nuevo producto
                if (!ValidarProducto())
                    return;

                try
                {
                    int idProducto = Convert.ToInt32(cmbProducto.SelectedValue);
                    string nombreProducto = cmbProducto.Text;
                    int idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                    int cantidad = (int)txtCantidad.Value;

                    // Agregar a la lista
                    DetalleMovimientoDto detalle = new DetalleMovimientoDto
                    {
                        IdProducto = idProducto,
                        IdCategoria = idCategoria,
                        Cantidad = cantidad
                    };

                    detallesMovimiento.Add(detalle);
                    ActualizarDataGrid();
                    LimpiarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarDataGrid()
        {
            dgvMovimientos.Rows.Clear();

            foreach (var detalle in detallesMovimiento)
            {
                // Obtener el nombre del producto desde la lista de productos cargados
                var producto = productos.FirstOrDefault(p => p.IdProducto == detalle.IdProducto);
                string nombreProducto = producto != null ? producto.Nombre : "Desconocido";

                // Obtener el nombre de la categoría desde la lista de categorías cargadas
                var categoria = categorias.FirstOrDefault(c => c.IdCategoria == detalle.IdCategoria);
                string nombreCategoria = categoria != null ? categoria.Nombre : "Desconocida";

                // Agregar una fila con IdProducto (oculto), IdCategoria (oculto), NombreProducto, NombreCategoria y Cantidad
                dgvMovimientos.Rows.Add(detalle.IdProducto, detalle.IdCategoria, nombreProducto, nombreCategoria, detalle.Cantidad);
            }
        }

        private void LimpiarProductos()
        {
            cmbProducto.SelectedIndex = -1;
            cmbCategoria.SelectedIndex = -1;
            txtCantidad.Value = 0;
            cmbProducto.Focus();
        }

        private void DgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < detallesMovimiento.Count)
            {
                filaEnEdicion = e.RowIndex;
                var detalle = detallesMovimiento[e.RowIndex];

                try
                {
                    // Cargar los datos en los controles para editar
                    cmbProducto.SelectedValue = detalle.IdProducto;
                    cmbCategoria.SelectedValue = detalle.IdCategoria;
                    txtCantidad.Value = detalle.Cantidad;

                    // Cambiar el texto del botón para indicar que está en modo edición
                    btnAgregarProducto.Text = "✓ Guardar Cambios";
                    btnAgregarProducto.BackColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvMovimientos.SelectedRows.Count > 0)
            {
                int index = dgvMovimientos.SelectedRows[0].Index;
                if (index >= 0 && index < detallesMovimiento.Count)
                {
                    detallesMovimiento.RemoveAt(index);
                    ActualizarDataGrid();
                    filaEnEdicion = -1;
                    LimpiarProductos();

                    // Restaurar el botón a su estado original si estaba en edición
                    if (btnAgregarProducto.Text == "✓ Guardar Cambios")
                    {
                        btnAgregarProducto.Text = "+ Agregar Producto";
                        btnAgregarProducto.BackColor = SystemColors.Control;
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                string tipoMovimiento = cmbTipoMovimiento.SelectedItem.ToString();
                int idProveedor = tipoMovimiento == "ENTRADA" ? Convert.ToInt32(cmbProveedor.SelectedValue) : 0;
                int idCliente = tipoMovimiento == "SALIDA" ? Convert.ToInt32(cmbCliente.SelectedValue) : 0;
                string motivo = tipoMovimiento == "SALIDA" ? cmbMotivo.SelectedItem.ToString() : "";

                // Crear el movimiento principal (SIEMPRE con la fecha actual)
                MovimientoDto movimiento = new MovimientoDto
                {
                    TipoMovimiento = tipoMovimiento,
                    Fecha = DateTime.Now,  // Siempre la fecha actual del sistema
                    IdUsuario = ObtenerIdUsuarioActual(),
                    IdProveedor = tipoMovimiento == "ENTRADA" ? (int?)idProveedor : null,
                    IdCliente = tipoMovimiento == "SALIDA" ? (int?)idCliente : null
                };

                int idMovimiento = MovimientoBL.InsertarMovimiento(movimiento);

                if (idMovimiento > 0)
                {
                    // Actualizar el label con el ID del movimiento
                    lblNumMovimiento.Text = idMovimiento.ToString();

                    // Insertar detalles del movimiento
                    foreach (var detalle in detallesMovimiento)
                    {
                        detalle.IdMovimiento = idMovimiento;
                        MovimientoBL.InsertarDetalleMovimiento(detalle);

                        // Actualizar stock del producto
                        MovimientoBL.ActualizarStockProducto(detalle.IdProducto, detalle.Cantidad, tipoMovimiento);
                    }

                    MessageBox.Show("Movimiento guardado exitosamente con ID: " + idMovimiento, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Generar reporte automáticamente
                    GenerarReporteMovimiento(idMovimiento, tipoMovimiento);

                    // Refrescar el formulario de consulta de movimientos si está abierto
                    RefrescarConsultaMovimientos();

                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al guardar el movimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteMovimiento(int idMovimiento, string tipoMovimiento)
        {
            try
            {
                // Crear un DataTable con los detalles del movimiento
                DataTable dtReporte = new DataTable();

                // Agregar columnas que coincidan con los campos del RDLC (ReporteM.rdlc)
                // Campos esperados por ReporteM.rdlc: Tipo, Producto, Cantidad, Fecha, Usuario, ProveedorCliente
                dtReporte.Columns.Add("tipo_movimiento", typeof(string));
                dtReporte.Columns.Add("nombre_producto", typeof(string));
                dtReporte.Columns.Add("cantidad", typeof(int));
                dtReporte.Columns.Add("fecha_movimiento", typeof(DateTime));
                dtReporte.Columns.Add("usuario", typeof(string));
                dtReporte.Columns.Add("proveedor", typeof(string));

                // Columnas adicionales para compatibilidad con otros reportes
                dtReporte.Columns.Add("id_detalle_movimiento", typeof(int));
                dtReporte.Columns.Add("id_movimiento", typeof(int));
                dtReporte.Columns.Add("id_producto", typeof(int));

                // Columnas con nombres coincidentes con ReporteM.rdlc (sin underscore)
                dtReporte.Columns.Add("Tipo", typeof(string));
                dtReporte.Columns.Add("Producto", typeof(string));
                dtReporte.Columns.Add("Cantidad", typeof(int));
                dtReporte.Columns.Add("Fecha", typeof(DateTime));
                dtReporte.Columns.Add("Usuario", typeof(string));
                dtReporte.Columns.Add("ProveedorCliente", typeof(string));

                // Llenar el DataTable con los detalles del movimiento actual
                foreach (var detalle in detallesMovimiento)
                {
                    // Obtener el nombre del producto
                    var producto = productos.FirstOrDefault(p => p.IdProducto == detalle.IdProducto);
                    string nombreProducto = producto != null ? producto.Nombre : "Desconocido";

                    // Determinar Proveedor/Cliente según el tipo de movimiento
                    string proveedorCliente = "";
                    if (tipoMovimiento == "ENTRADA" && cmbProveedor.SelectedItem != null)
                    {
                        proveedorCliente = cmbProveedor.SelectedItem.ToString();
                    }
                    else if (tipoMovimiento == "SALIDA" && cmbCliente.SelectedItem != null)
                    {
                        proveedorCliente = cmbCliente.SelectedItem.ToString();
                    }

                    // Crear una fila en el reporte
                    DataRow row = dtReporte.NewRow();

                    // Llenar columnas con underscore
                    row["tipo_movimiento"] = tipoMovimiento;
                    row["nombre_producto"] = nombreProducto;
                    row["cantidad"] = detalle.Cantidad;
                    row["fecha_movimiento"] = DateTime.Now;
                    row["usuario"] = SesionDto.NombreUsuario;
                    row["proveedor"] = proveedorCliente;
                    row["id_detalle_movimiento"] = 0; // Temporal
                    row["id_movimiento"] = idMovimiento;
                    row["id_producto"] = detalle.IdProducto;

                    // Llenar columnas sin underscore (para ReporteM.rdlc)
                    row["Tipo"] = tipoMovimiento;
                    row["Producto"] = nombreProducto;
                    row["Cantidad"] = detalle.Cantidad;
                    row["Fecha"] = DateTime.Now;
                    row["Usuario"] = SesionDto.NombreUsuario;
                    row["ProveedorCliente"] = proveedorCliente;

                    dtReporte.Rows.Add(row);
                }

                if (dtReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No hay detalles para generar el reporte.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Abrir el nuevo formulario con ReportViewer RDLC
                frmReporteMovimientosRdlc frmReporte = new frmReporteMovimientosRdlc(dtReporte);
                frmReporte.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerIdUsuarioActual()
        {
            // Retorna el ID del usuario autenticado en la sesión
            return SesionDto.IdUsuario;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar todos los campos EXCEPTO el DataGrid con los productos
            cmbTipoMovimiento.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
            cmbMotivo.SelectedIndex = -1;
            lblNumMovimiento.Text = "---";
            dtpFecha.Value = DateTime.Now;
            cmbProducto.SelectedIndex = -1;
            txtCantidad.Value = 0;
            ConfigurarVisibilidadCampos();
            cmbTipoMovimiento.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarControles()
        {
            cmbTipoMovimiento.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
            cmbMotivo.SelectedIndex = -1;
            lblNumMovimiento.Text = "---";
            dtpFecha.Value = DateTime.Now;
            cmbProducto.SelectedIndex = -1;
            cmbCategoria.SelectedIndex = -1;
            txtCantidad.Value = 0;
            dgvMovimientos.DataSource = null;
            detallesMovimiento.Clear();
            ConfigurarVisibilidadCampos();
            cmbTipoMovimiento.Focus();
        }

        private void RefrescarConsultaMovimientos()
        {
            try
            {
                // Buscar si existe una instancia del formulario de consulta abierta
                foreach (Form form in Application.OpenForms)
                {
                    if (form is frmConsultaMovimientos)
                    {
                        frmConsultaMovimientos consultaForm = (frmConsultaMovimientos)form;
                        consultaForm.RefrescarDatos();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Si hay error, simplemente no refrescamos, pero no mostramos error
                System.Diagnostics.Debug.WriteLine("Error al refrescar consulta de movimientos: " + ex.Message);
            }
        }
    }
}
