using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmProducto : Form
    {
        private List<ProductosDto> productos = new List<ProductosDto>();
        private List<CategoriaDto> categorias = new List<CategoriaDto>();
        private List<ProveedoresDto> proveedores = new List<ProveedoresDto>();
        private ProductosDto productoSeleccionado = null;
        private bool enEdicion = false;

        // Controles
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlUsuario;
        private Label lblNombreUsuario;
        private Label lblUsernameUsuario;
        private Label lblRolUsuario;
        private Panel pnlFormulario;
        private Label lblNombre, lblCategoria, lblPrecio, lblStock, lblProveedor;
        private TextBox txtNombre, txtPrecio, txtStock;
        private ComboBox cmbCategoria, cmbProveedor;
        private CheckBox chkActivo;
        private Panel pnlBotones;
        private Button btnAgregar, btnGuardar, btnEditar, btnEliminar, btnCancelar, btnSalir;
        private Panel pnlDataGrid;
        private DataGridView dgvProductos;

        public frmProducto()
        {
            InitializeComponent();
            CrearControles();
            ConfigurarEventos();
        }

        // Minimal InitializeComponent stub because designer file is not present
        private void InitializeComponent()
        {
            // Controls are created dynamically in CrearControles
        }

        private void CrearControles()
        {
            // Panel Encabezado
            pnlEncabezado = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 80 };
            lblTitulo = new Label { AutoSize = true, Text = "Gestión de Productos", Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(15, 15) };
            
            pnlUsuario = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Size = new Size(230, 60), Location = new Point(750, 8) };
            lblNombreUsuario = new Label { AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold), Location = new Point(8, 6) };
            lblUsernameUsuario = new Label { AutoSize = true, Font = new Font("Arial", 8), ForeColor = Color.Gray, Location = new Point(8, 26) };
            lblRolUsuario = new Label { AutoSize = true, BackColor = Color.FromArgb(52, 152, 219), Font = new Font("Arial", 7.5f, FontStyle.Bold), ForeColor = Color.White, Location = new Point(8, 42), Padding = new Padding(4, 2, 4, 2) };
            
            pnlUsuario.Controls.Add(lblNombreUsuario);
            pnlUsuario.Controls.Add(lblUsernameUsuario);
            pnlUsuario.Controls.Add(lblRolUsuario);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Controls.Add(pnlUsuario);

            // Panel Formulario
            pnlFormulario = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 150 };
            
            lblNombre = new Label { AutoSize = true, Text = "Nombre:", Location = new Point(15, 15) };
            txtNombre = new TextBox { Location = new Point(80, 12), Size = new Size(280, 20) };
            
            lblCategoria = new Label { AutoSize = true, Text = "Categoría:", Location = new Point(380, 15) };
            cmbCategoria = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(450, 12), Size = new Size(200, 21) };
            
            lblPrecio = new Label { AutoSize = true, Text = "Precio:", Location = new Point(15, 45) };
            txtPrecio = new TextBox { Location = new Point(80, 42), Size = new Size(150, 20) };
            
            lblStock = new Label { AutoSize = true, Text = "Stock:", Location = new Point(380, 45) };
            txtStock = new TextBox { Location = new Point(450, 42), Size = new Size(150, 20) };
            
            lblProveedor = new Label { AutoSize = true, Text = "Proveedor:", Location = new Point(15, 75) };
            cmbProveedor = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(80, 72), Size = new Size(200, 21) };
            
            chkActivo = new CheckBox { AutoSize = true, Checked = true, Text = "Activo", Location = new Point(450, 75) };
            
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblCategoria);
            pnlFormulario.Controls.Add(cmbCategoria);
            pnlFormulario.Controls.Add(lblPrecio);
            pnlFormulario.Controls.Add(txtPrecio);
            pnlFormulario.Controls.Add(lblStock);
            pnlFormulario.Controls.Add(txtStock);
            pnlFormulario.Controls.Add(lblProveedor);
            pnlFormulario.Controls.Add(cmbProveedor);
            pnlFormulario.Controls.Add(chkActivo);

            // Panel Botones
            pnlBotones = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 60 };
            btnAgregar = CrearBoton("+ Agregar", Color.LimeGreen, 15, 15);
            btnGuardar = CrearBoton("Guardar", Color.DodgerBlue, 105, 15);
            btnEditar = CrearBoton("Editar", Color.Orange, 195, 15);
            btnEliminar = CrearBoton("Eliminar", Color.Red, 285, 15);
            btnCancelar = CrearBoton("Cancelar", Color.Gray, 375, 15);
            btnSalir = CrearBoton("Salir", Color.Maroon, 900, 15);
            
            pnlBotones.Controls.Add(btnAgregar);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnEditar);
            pnlBotones.Controls.Add(btnEliminar);
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnSalir);

            // Panel DataGrid
            pnlDataGrid = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Fill, Padding = new Padding(15) };
            dgvProductos = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = false };
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProducto", HeaderText = "ID", Visible = false });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Producto", Width = 200, ReadOnly = true });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", HeaderText = "Categoría", Width = 120, ReadOnly = true });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", HeaderText = "Precio", Width = 90, ReadOnly = true });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Stock", HeaderText = "Stock", Width = 80, ReadOnly = true });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", Width = 70, ReadOnly = true });
            
            pnlDataGrid.Controls.Add(dgvProductos);

            Controls.Add(pnlDataGrid);
            Controls.Add(pnlBotones);
            Controls.Add(pnlFormulario);
            Controls.Add(pnlEncabezado);
            
            ClientSize = new Size(1000, 600);
            Text = "Gestión de Productos";
        }

        private Button CrearBoton(string texto, Color color, int x, int y)
        {
            return new Button
            {
                Text = texto,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 30),
                Location = new Point(x, y)
            };
        }

        private void ConfigurarEventos()
        {
            Load += FrmProducto_Load;
            btnAgregar.Click += BtnAgregar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnSalir.Click += BtnSalir_Click;
            dgvProductos.CellDoubleClick += DgvProductos_CellDoubleClick;
        }

        private void ConfigurarDataGrid()
        {
            dgvProductos.AutoGenerateColumns = false;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            CargarSesionUsuario();
            CargarCategorias();
            CargarProveedores();
            CargarProductos();
            LimpiarControles();
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
            }
            catch { }
        }

        private void CargarCategorias()
        {
            try
            {
                categorias = CategoriaBL.ListarCategorias();
                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "IdCategoria";
                cmbCategoria.SelectedIndex = -1;
            }
            catch { }
        }

        private void CargarProveedores()
        {
            try
            {
                proveedores = ProveedoresBL.ListarProveedores();
                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "NombreProveedor";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.SelectedIndex = -1;
            }
            catch { }
        }

        private void CargarProductos()
        {
            try
            {
                productos = ProductoBL.ListarProductos();
                ActualizarDataGrid();
            }
            catch { }
        }

        private void ActualizarDataGrid()
        {
            dgvProductos.Rows.Clear();
            foreach (var prod in productos)
            {
                string estado = prod.Estado ? "Activo" : "Inactivo";
                dgvProductos.Rows.Add(prod.IdProducto, prod.Nombre, prod.Categoria ?? "N/A", prod.Precio.ToString("C"), prod.Stock, estado);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { MessageBox.Show("Nombre requerido"); return false; }
            if (cmbCategoria.SelectedIndex == -1) { MessageBox.Show("Seleccione categoría"); return false; }
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0) { MessageBox.Show("Precio inválido"); return false; }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0) { MessageBox.Show("Stock inválido"); return false; }
            if (cmbProveedor.SelectedIndex == -1) { MessageBox.Show("Seleccione proveedor"); return false; }
            return true;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            enEdicion = false;
            productoSeleccionado = null;
            LimpiarControles();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (enEdicion && productoSeleccionado != null)
                {
                    productoSeleccionado.Nombre = txtNombre.Text;
                    productoSeleccionado.IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                    productoSeleccionado.Precio = decimal.Parse(txtPrecio.Text);
                    productoSeleccionado.Stock = int.Parse(txtStock.Text);
                    productoSeleccionado.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                    productoSeleccionado.Estado = chkActivo.Checked;

                    if (ProductoBL.ActualizarProducto(productoSeleccionado))
                    {
                        MessageBox.Show("Actualizado");
                        CargarProductos();
                        LimpiarControles();
                    }
                }
                else
                {
                    var nuevo = new ProductosDto
                    {
                        Nombre = txtNombre.Text,
                        IdCategoria = Convert.ToInt32(cmbCategoria.SelectedValue),
                        Precio = decimal.Parse(txtPrecio.Text),
                        Stock = int.Parse(txtStock.Text),
                        IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                        CreadoPor = SesionDto.IdUsuario,
                        FechaCreacion = DateTime.Now,
                        Estado = chkActivo.Checked
                    };

                    if (ProductoBL.InsertarProducto(nuevo))
                    {
                        MessageBox.Show("Creado");
                        CargarProductos();
                        LimpiarControles();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;

            try
            {
                int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                productoSeleccionado = productos.FirstOrDefault(p => p.IdProducto == id);
                if (productoSeleccionado != null)
                {
                    enEdicion = true;
                    txtNombre.Text = productoSeleccionado.Nombre;
                    cmbCategoria.SelectedValue = productoSeleccionado.IdCategoria;
                    txtPrecio.Text = productoSeleccionado.Precio.ToString();
                    txtStock.Text = productoSeleccionado.Stock.ToString();
                    cmbProveedor.SelectedValue = productoSeleccionado.IdProveedor;
                    chkActivo.Checked = productoSeleccionado.Estado;
                }
            }
            catch { }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;

            try
            {
                int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                if (MessageBox.Show("¿Confirma?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ProductoBL.EliminarProducto(id))
                    {
                        MessageBox.Show("Eliminado");
                        CargarProductos();
                        LimpiarControles();
                    }
                }
            }
            catch { }
        }

        private void BtnCancelar_Click(object sender, EventArgs e) => LimpiarControles();

        private void BtnSalir_Click(object sender, EventArgs e) => Close();

        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            chkActivo.Checked = true;
            productoSeleccionado = null;
            enEdicion = false;
            dgvProductos.ClearSelection();
        }

        private void DgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) BtnEditar_Click(null, null);
        }
    }
}
