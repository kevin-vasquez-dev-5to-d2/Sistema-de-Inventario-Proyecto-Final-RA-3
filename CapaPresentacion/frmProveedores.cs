using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmProveedores : Form
    {
        private List<ProveedoresDto> proveedores = new List<ProveedoresDto>();
        private ProveedoresDto proveedorSeleccionado = null;
        private bool enEdicion = false;

        // Controles
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlUsuario;
        private Label lblNombreUsuario;
        private Label lblUsernameUsuario;
        private Label lblRolUsuario;
        private Panel pnlFormulario;
        private Label lblNombre, lblTelefono, lblEmail, lblDireccion;
        private TextBox txtNombre, txtTelefono, txtEmail, txtDireccion;
        private CheckBox chkActivo;
        private Panel pnlBotones;
        private Button btnAgregar, btnGuardar, btnEditar, btnEliminar, btnCancelar, btnSalir;
        private Panel pnlDataGrid;
        private DataGridView dgvProveedores;

        public frmProveedores()
        {
            InitializeComponent();
            CrearControles();
            ConfigurarEventos();
        }

        private void CrearControles()
        {
            // Panel Encabezado
            pnlEncabezado = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 80 };
            lblTitulo = new Label { AutoSize = true, Text = "Gestión de Proveedores", Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(15, 15) };

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
            txtNombre = new TextBox { Location = new Point(100, 12), Size = new Size(350, 20) };

            lblTelefono = new Label { AutoSize = true, Text = "Teléfono:", Location = new Point(15, 45) };
            txtTelefono = new TextBox { Location = new Point(100, 42), Size = new Size(200, 20) };

            lblEmail = new Label { AutoSize = true, Text = "Email:", Location = new Point(350, 45) };
            txtEmail = new TextBox { Location = new Point(420, 42), Size = new Size(250, 20) };

            lblDireccion = new Label { AutoSize = true, Text = "Dirección:", Location = new Point(15, 75) };
            txtDireccion = new TextBox { Location = new Point(100, 72), Size = new Size(570, 50), Multiline = true };

            chkActivo = new CheckBox { AutoSize = true, Checked = true, Text = "Activo", Location = new Point(100, 125) };

            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblTelefono);
            pnlFormulario.Controls.Add(txtTelefono);
            pnlFormulario.Controls.Add(lblEmail);
            pnlFormulario.Controls.Add(txtEmail);
            pnlFormulario.Controls.Add(lblDireccion);
            pnlFormulario.Controls.Add(txtDireccion);
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
            dgvProveedores = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            pnlDataGrid.Controls.Add(dgvProveedores);

            // Agregar panels al formulario
            this.Controls.Add(pnlDataGrid);
            this.Controls.Add(pnlBotones);
            this.Controls.Add(pnlFormulario);
            this.Controls.Add(pnlEncabezado);

            this.Text = "Gestión de Proveedores";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = SystemIcons.Application;
        }

        private Button CrearBoton(string texto, Color color, int x, int y)
        {
            return new Button
            {
                Text = texto,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(80, 30),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };
        }

        private void ConfigurarEventos()
        {
            this.Load += FrmProveedores_Load;
            btnAgregar.Click += BtnAgregar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnSalir.Click += BtnSalir_Click;
            dgvProveedores.CellClick += DgvProveedores_CellClick;
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            if (!SesionDto.Autenticado)
            {
                MessageBox.Show("Debe iniciar sesión primero.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            MostrarInfoUsuario();
            ConfigurarDataGrid();
            CargarProveedores();
        }

        private void MostrarInfoUsuario()
        {
            lblNombreUsuario.Text = SesionDto.NombreUsuario;
            lblUsernameUsuario.Text = $"@{SesionDto.Username}";
            lblRolUsuario.Text = SesionDto.NombreRol.ToUpper();
        }

        private void ConfigurarDataGrid()
        {
            dgvProveedores.Columns.Clear();
            dgvProveedores.Columns.Add("IdProveedor", "ID");
            dgvProveedores.Columns.Add("NombreProveedor", "Nombre");
            dgvProveedores.Columns.Add("Telefono", "Teléfono");
            dgvProveedores.Columns.Add("Email", "Email");
            dgvProveedores.Columns.Add("Direccion", "Dirección");
            dgvProveedores.Columns.Add("Estado", "Estado");

            dgvProveedores.Columns["IdProveedor"].Width = 0;
            dgvProveedores.Columns["NombreProveedor"].Width = 150;
            dgvProveedores.Columns["Telefono"].Width = 120;
            dgvProveedores.Columns["Email"].Width = 150;
            dgvProveedores.Columns["Direccion"].Width = 200;
            dgvProveedores.Columns["Estado"].Width = 80;
        }

        private void CargarProveedores()
        {
            try
            {
                proveedores = ProveedoresBL.ListarProveedores();
                dgvProveedores.Rows.Clear();

                foreach (var proveedor in proveedores)
                {
                    dgvProveedores.Rows.Add(
                        proveedor.IdProveedor,
                        proveedor.NombreProveedor,
                        proveedor.Telefono,
                        proveedor.Email,
                        proveedor.Direccion,
                        proveedor.Estado ? "Activo" : "Inactivo"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            chkActivo.Checked = true;
            proveedorSeleccionado = null;
            enEdicion = false;
            btnGuardar.Enabled = false;
            btnAgregar.Text = "+ Agregar";
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (enEdicion)
            {
                LimpiarFormulario();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor complete el campo Nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var nuevoProveedor = new ProveedoresDto
                {
                    NombreProveedor = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Estado = chkActivo.Checked
                };

                if (ProveedoresBL.InsertarProveedor(nuevoProveedor))
                {
                    MessageBox.Show("Proveedor agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProveedores();
                }
                else
                {
                    MessageBox.Show("Error al agregar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (proveedorSeleccionado == null)
            {
                MessageBox.Show("Seleccione un proveedor para guardar cambios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                proveedorSeleccionado.NombreProveedor = txtNombre.Text.Trim();
                proveedorSeleccionado.Telefono = txtTelefono.Text.Trim();
                proveedorSeleccionado.Email = txtEmail.Text.Trim();
                proveedorSeleccionado.Direccion = txtDireccion.Text.Trim();
                proveedorSeleccionado.Estado = chkActivo.Checked;

                if (ProveedoresBL.ActualizarProveedor(proveedorSeleccionado))
                {
                    MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProveedores();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (proveedorSeleccionado == null)
            {
                MessageBox.Show("Seleccione un proveedor para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            enEdicion = true;
            btnGuardar.Enabled = true;
            btnAgregar.Text = "Cancelar";
            txtNombre.Focus();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (proveedorSeleccionado == null)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Desea eliminar el proveedor {proveedorSeleccionado.NombreProveedor}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (ProveedoresBL.EliminarProveedor(proveedorSeleccionado.IdProveedor))
                    {
                        MessageBox.Show("Proveedor eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarProveedores();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < proveedores.Count)
            {
                proveedorSeleccionado = proveedores[e.RowIndex];
                txtNombre.Text = proveedorSeleccionado.NombreProveedor;
                txtTelefono.Text = proveedorSeleccionado.Telefono;
                txtEmail.Text = proveedorSeleccionado.Email;
                txtDireccion.Text = proveedorSeleccionado.Direccion;
                chkActivo.Checked = proveedorSeleccionado.Estado;
                enEdicion = false;
                btnGuardar.Enabled = false;
                btnAgregar.Text = "+ Agregar";
            }
        }
    }
}
