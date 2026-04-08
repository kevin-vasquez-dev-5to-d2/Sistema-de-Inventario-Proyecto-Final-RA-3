using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        private List<UsuariosDto> usuarios = new List<UsuariosDto>();
        private List<RolesDto> roles = new List<RolesDto>();
        private UsuariosDto usuarioSeleccionado = null;
        private bool enEdicion = false;

        // Controles
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlUsuario;
        private Label lblNombreUsuario;
        private Label lblUsernameUsuario;
        private Label lblRolUsuario;
        private Panel pnlFormulario;
        private Label lblNombre, lblUsername, lblPassword, lblRol;
        private TextBox txtNombre, txtUsername, txtPassword;
        private ComboBox cmbRol;
        private CheckBox chkActivo;
        private Panel pnlBotones;
        private Button btnAgregar, btnGuardar, btnEditar, btnEliminar, btnCancelar, btnSalir;
        private Panel pnlDataGrid;
        private DataGridView dgvUsuarios;

        public frmUsuarios()
        {
            InitializeComponent();
            CrearControles();
            ConfigurarEventos();
        }

        private void CrearControles()
        {
            // Panel Encabezado
            pnlEncabezado = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 80 };
            lblTitulo = new Label { AutoSize = true, Text = "Gestión de Usuarios", Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(15, 15) };

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
            pnlFormulario = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 140 };

            lblNombre = new Label { AutoSize = true, Text = "Nombre:", Location = new Point(15, 15) };
            txtNombre = new TextBox { Location = new Point(100, 12), Size = new Size(280, 20) };

            lblUsername = new Label { AutoSize = true, Text = "Usuario:", Location = new Point(420, 15) };
            txtUsername = new TextBox { Location = new Point(490, 12), Size = new Size(180, 20) };

            lblPassword = new Label { AutoSize = true, Text = "Contraseña:", Location = new Point(15, 45) };
            txtPassword = new TextBox { Location = new Point(100, 42), Size = new Size(280, 20), PasswordChar = '*' };

            lblRol = new Label { AutoSize = true, Text = "Rol:", Location = new Point(420, 45) };
            cmbRol = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(490, 42), Size = new Size(180, 21) };

            chkActivo = new CheckBox { AutoSize = true, Checked = true, Text = "Activo", Location = new Point(100, 75) };

            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblUsername);
            pnlFormulario.Controls.Add(txtUsername);
            pnlFormulario.Controls.Add(lblPassword);
            pnlFormulario.Controls.Add(txtPassword);
            pnlFormulario.Controls.Add(lblRol);
            pnlFormulario.Controls.Add(cmbRol);
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
            dgvUsuarios = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            pnlDataGrid.Controls.Add(dgvUsuarios);

            // Agregar panels al formulario
            this.Controls.Add(pnlDataGrid);
            this.Controls.Add(pnlBotones);
            this.Controls.Add(pnlFormulario);
            this.Controls.Add(pnlEncabezado);

            this.Text = "Gestión de Usuarios";
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
            this.Load += FrmUsuarios_Load;
            btnAgregar.Click += BtnAgregar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnSalir.Click += BtnSalir_Click;
            dgvUsuarios.CellClick += DgvUsuarios_CellClick;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            if (!SesionDto.Autenticado)
            {
                MessageBox.Show("Debe iniciar sesión primero.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Acceso abierto para todos - sin restricción de rol
            MostrarInfoUsuario();
            CargarRoles();
            ConfigurarDataGrid();
            CargarUsuarios();
        }

        private void MostrarInfoUsuario()
        {
            lblNombreUsuario.Text = SesionDto.NombreUsuario;
            lblUsernameUsuario.Text = $"@{SesionDto.Username}";
            lblRolUsuario.Text = SesionDto.NombreRol.ToUpper();
        }

        private void CargarRoles()
        {
            try
            {
                roles = RolesBL.ListarRoles();
                cmbRol.DataSource = roles;
                cmbRol.DisplayMember = "NombreRol";
                cmbRol.ValueMember = "IdRol";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGrid()
        {
            dgvUsuarios.Columns.Clear();
            dgvUsuarios.Columns.Add("IdUsuario", "ID");
            dgvUsuarios.Columns.Add("Nombre", "Nombre");
            dgvUsuarios.Columns.Add("Username", "Usuario");
            dgvUsuarios.Columns.Add("NombreRol", "Rol");
            dgvUsuarios.Columns.Add("Estado", "Estado");

            dgvUsuarios.Columns["IdUsuario"].Width = 0;
            dgvUsuarios.Columns["Nombre"].Width = 150;
            dgvUsuarios.Columns["Username"].Width = 120;
            dgvUsuarios.Columns["NombreRol"].Width = 120;
            dgvUsuarios.Columns["Estado"].Width = 80;
        }

        private void CargarUsuarios()
        {
            try
            {
                usuarios = UsuariosBL.ListarUsuarios();
                dgvUsuarios.Rows.Clear();

                foreach (var usuario in usuarios)
                {
                    dgvUsuarios.Rows.Add(
                        usuario.IdUsuario,
                        usuario.Nombre,
                        usuario.Username,
                        usuario.NombreRol,
                        usuario.Estado ? "Activo" : "Inactivo"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRol.SelectedIndex = -1;
            chkActivo.Checked = true;
            usuarioSeleccionado = null;
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

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) || cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var nuevoUsuario = new UsuariosDto
                {
                    Nombre = txtNombre.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    IdRol = (int)cmbRol.SelectedValue,
                    Estado = chkActivo.Checked
                };

                if (UsuariosBL.InsertarUsuario(nuevoUsuario))
                {
                    MessageBox.Show("Usuario agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al agregar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario para guardar cambios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                usuarioSeleccionado.Nombre = txtNombre.Text.Trim();
                usuarioSeleccionado.Username = txtUsername.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    usuarioSeleccionado.Password = txtPassword.Text;
                }
                usuarioSeleccionado.IdRol = (int)cmbRol.SelectedValue;
                usuarioSeleccionado.Estado = chkActivo.Checked;

                if (UsuariosBL.ActualizarUsuario(usuarioSeleccionado))
                {
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            enEdicion = true;
            btnGuardar.Enabled = true;
            btnAgregar.Text = "Cancelar";
            txtNombre.Focus();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Desea eliminar el usuario {usuarioSeleccionado.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (UsuariosBL.EliminarUsuario(usuarioSeleccionado.IdUsuario))
                    {
                        MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < usuarios.Count)
            {
                usuarioSeleccionado = usuarios[e.RowIndex];
                txtNombre.Text = usuarioSeleccionado.Nombre;
                txtUsername.Text = usuarioSeleccionado.Username;
                txtPassword.Clear();
                cmbRol.SelectedValue = usuarioSeleccionado.IdRol;
                chkActivo.Checked = usuarioSeleccionado.Estado;
                enEdicion = false;
                btnGuardar.Enabled = false;
                btnAgregar.Text = "+ Agregar";
            }
        }
    }
}
