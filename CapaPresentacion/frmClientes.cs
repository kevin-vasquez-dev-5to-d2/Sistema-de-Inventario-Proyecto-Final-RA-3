using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmClientes : Form
    {
        private List<ClientesDto> clientes = new List<ClientesDto>();
        private List<ClientesDto> clientesCompletos = new List<ClientesDto>(); // Copia de TODOS los clientes
        private ClientesDto clienteSeleccionado = null;
        private bool enEdicion = false;

        // Controles
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlUsuario;
        private Label lblNombreUsuario;
        private Label lblUsernameUsuario;
        private Label lblRolUsuario;
        private Panel pnlFormulario;
        private Label lblTipo, lblNombre, lblApellido, lblEmpresa, lblCedula, lblRnc, lblTelefono, lblEmail, lblDireccion;
        private ComboBox cmbTipo;
        private TextBox txtNombre, txtApellido, txtEmpresa, txtCedula, txtRnc, txtTelefono, txtEmail, txtDireccion;
        private CheckBox chkActivo;
        private Panel pnlBotones;
        private Button btnAgregar, btnGuardar, btnEditar, btnEliminar, btnCancelar, btnSalir;
        private Panel pnlDataGrid;
        private DataGridView dgvClientes;

        public frmClientes()
        {
            CrearControles();
            ConfigurarEventos();
        }

        private void CrearControles()
        {
            // Panel Encabezado
            pnlEncabezado = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 80 };
            lblTitulo = new Label { AutoSize = true, Text = "Gestión de Clientes", Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(15, 15) };

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
            pnlFormulario = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 200 };

            lblTipo = new Label { AutoSize = true, Text = "Tipo:", Location = new Point(15, 15) };
            cmbTipo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(80, 12), Size = new Size(150, 21) };
            cmbTipo.Items.AddRange(new[] { "FISICO", "EMPRESA" });

            lblNombre = new Label { AutoSize = true, Text = "Nombre:", Location = new Point(280, 15) };
            txtNombre = new TextBox { Location = new Point(350, 12), Size = new Size(250, 20) };

            lblApellido = new Label { AutoSize = true, Text = "Apellido:", Location = new Point(15, 45) };
            txtApellido = new TextBox { Location = new Point(80, 42), Size = new Size(200, 20) };

            lblEmpresa = new Label { AutoSize = true, Text = "Empresa:", Location = new Point(280, 45) };
            txtEmpresa = new TextBox { Location = new Point(350, 42), Size = new Size(250, 20) };

            lblCedula = new Label { AutoSize = true, Text = "Cédula:", Location = new Point(15, 75) };
            txtCedula = new TextBox { Location = new Point(80, 72), Size = new Size(150, 20) };

            lblRnc = new Label { AutoSize = true, Text = "RNC:", Location = new Point(280, 75) };
            txtRnc = new TextBox { Location = new Point(350, 72), Size = new Size(150, 20) };

            lblTelefono = new Label { AutoSize = true, Text = "Teléfono:", Location = new Point(15, 105) };
            txtTelefono = new TextBox { Location = new Point(80, 102), Size = new Size(150, 20) };

            lblEmail = new Label { AutoSize = true, Text = "Email:", Location = new Point(280, 105) };
            txtEmail = new TextBox { Location = new Point(350, 102), Size = new Size(250, 20) };

            lblDireccion = new Label { AutoSize = true, Text = "Dirección:", Location = new Point(15, 135) };
            txtDireccion = new TextBox { Location = new Point(80, 132), Size = new Size(520, 40), Multiline = true };

            chkActivo = new CheckBox { AutoSize = true, Checked = true, Text = "Activo", Location = new Point(80, 175) };

            pnlFormulario.Controls.Add(lblTipo);
            pnlFormulario.Controls.Add(cmbTipo);
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblApellido);
            pnlFormulario.Controls.Add(txtApellido);
            pnlFormulario.Controls.Add(lblEmpresa);
            pnlFormulario.Controls.Add(txtEmpresa);
            pnlFormulario.Controls.Add(lblCedula);
            pnlFormulario.Controls.Add(txtCedula);
            pnlFormulario.Controls.Add(lblRnc);
            pnlFormulario.Controls.Add(txtRnc);
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
            dgvClientes = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, ReadOnly = true, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, BackgroundColor = Color.White };
            pnlDataGrid.Controls.Add(dgvClientes);

            // Agregar panels al formulario
            this.Controls.Add(pnlDataGrid);
            this.Controls.Add(pnlBotones);
            this.Controls.Add(pnlFormulario);
            this.Controls.Add(pnlEncabezado);

            this.Text = "Gestión de Clientes";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = SystemIcons.Application;
        }

        private Button CrearBoton(string texto, Color color, int x, int y)
        {
            return new Button { Text = texto, BackColor = color, ForeColor = Color.White, Font = new Font("Arial", 10, FontStyle.Bold), Size = new Size(80, 30), Location = new Point(x, y), Cursor = Cursors.Hand };
        }

        private void ConfigurarEventos()
        {
            this.Load += (s, e) => FrmClientes_Load();
            btnAgregar.Click += BtnAgregar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnSalir.Click += BtnSalir_Click;
            dgvClientes.CellClick += DgvClientes_CellClick;
            cmbTipo.SelectedIndexChanged += CmbTipo_SelectedIndexChanged;
        }

        private void FrmClientes_Load()
        {
            if (!SesionDto.Autenticado)
            {
                MessageBox.Show("Debe iniciar sesión primero.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            MostrarInfoUsuario();
            ConfigurarDataGrid();
            ConfigurarVisibilidadCampos("FISICO");
            CargarClientes();
            LimpiarFormulario();
        }

        private void MostrarInfoUsuario()
        {
            lblNombreUsuario.Text = SesionDto.NombreUsuario;
            lblUsernameUsuario.Text = $"@{SesionDto.Username}";
            lblRolUsuario.Text = SesionDto.NombreRol.ToUpper();
        }

        private void ConfigurarDataGrid()
        {
            dgvClientes.Columns.Clear();
            dgvClientes.Columns.Add("IdCliente", "ID");
            dgvClientes.Columns.Add("TipoCliente", "Tipo");
            dgvClientes.Columns.Add("Nombre", "Nombre");
            dgvClientes.Columns.Add("Apellido", "Apellido");
            dgvClientes.Columns.Add("Cedula", "Cédula");
            dgvClientes.Columns.Add("NombreEmpresa", "Empresa");
            dgvClientes.Columns.Add("Rnc", "RNC");
            dgvClientes.Columns.Add("Telefono", "Teléfono");
            dgvClientes.Columns.Add("Email", "Email");
            dgvClientes.Columns.Add("Estado", "Estado");

            // Ocultar la columna ID
            dgvClientes.Columns["IdCliente"].Visible = false;

            dgvClientes.Columns["TipoCliente"].Width = 60;
            dgvClientes.Columns["Nombre"].Width = 100;
            dgvClientes.Columns["Apellido"].Width = 100;
            dgvClientes.Columns["Cedula"].Width = 100;
            dgvClientes.Columns["NombreEmpresa"].Width = 120;
            dgvClientes.Columns["Rnc"].Width = 100;
            dgvClientes.Columns["Telefono"].Width = 100;
            dgvClientes.Columns["Email"].Width = 120;
            dgvClientes.Columns["Estado"].Width = 60;
        }

        private void ConfigurarVisibilidadCampos(string tipoCliente)
        {
            if (tipoCliente == "FISICO")
            {
                // Mostrar: nombre, apellido, cédula
                lblNombre.Visible = true;
                txtNombre.Visible = true;
                lblApellido.Visible = true;
                txtApellido.Visible = true;
                lblCedula.Visible = true;
                txtCedula.Visible = true;

                // Ocultar: empresa, RNC
                lblEmpresa.Visible = false;
                txtEmpresa.Visible = false;
                lblRnc.Visible = false;
                txtRnc.Visible = false;

                lblNombre.Text = "Nombre:";
                lblApellido.Text = "Apellido:";

                // Configurar DataGrid para FISICO
                dgvClientes.Columns["Apellido"].Visible = true;
                dgvClientes.Columns["Cedula"].Visible = true;
                dgvClientes.Columns["NombreEmpresa"].Visible = false;
                dgvClientes.Columns["Rnc"].Visible = false;
            }
            else if (tipoCliente == "EMPRESA")
            {
                // Mostrar: empresa, RNC
                lblNombre.Visible = false;
                txtNombre.Visible = false;
                lblEmpresa.Visible = true;
                txtEmpresa.Visible = true;
                lblRnc.Visible = true;
                txtRnc.Visible = true;

                // Ocultar: nombre, apellido, cédula
                lblApellido.Visible = false;
                txtApellido.Visible = false;
                lblCedula.Visible = false;
                txtCedula.Visible = false;

                // Configurar DataGrid para EMPRESA
                dgvClientes.Columns["Apellido"].Visible = false;
                dgvClientes.Columns["Cedula"].Visible = false;
                dgvClientes.Columns["NombreEmpresa"].Visible = true;
                dgvClientes.Columns["Rnc"].Visible = true;
            }
        }

        private void CmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedItem != null)
            {
                string tipoSeleccionado = cmbTipo.SelectedItem.ToString();
                ConfigurarVisibilidadCampos(tipoSeleccionado);

                // Recargar datos filtrados por tipo
                CargarClientes();
            }
            else
            {
                // Si no hay tipo seleccionado, mostrar FISICO por defecto
                ConfigurarVisibilidadCampos("FISICO");

                // Mostrar todas las columnas del DataGrid
                dgvClientes.Columns["Apellido"].Visible = true;
                dgvClientes.Columns["Cedula"].Visible = true;
                dgvClientes.Columns["NombreEmpresa"].Visible = true;
                dgvClientes.Columns["Rnc"].Visible = true;
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            try
            {
                // Solo cargar de la BD la primera vez o si clientesCompletos está vacío
                if (clientesCompletos.Count == 0)
                {
                    clientesCompletos = ClienteBL.ListarClientes();
                    clientes = new List<ClientesDto>(clientesCompletos); // Copiar lista
                }
                else
                {
                    clientes = new List<ClientesDto>(clientesCompletos); // Usar copia de todos
                }

                dgvClientes.Rows.Clear();

                // Obtener el tipo de cliente seleccionado para filtrar la visualización
                string tipoSeleccionado = cmbTipo.SelectedItem?.ToString();

                // Filtrar según el tipo seleccionado
                List<ClientesDto> clientesFiltrados = clientes;
                if (!string.IsNullOrEmpty(tipoSeleccionado))
                {
                    clientesFiltrados = clientes.Where(c => c.TipoCliente == tipoSeleccionado).ToList();
                }

                // Mostrar en el DataGrid
                foreach (var cliente in clientesFiltrados)
                {
                    dgvClientes.Rows.Add(
                        cliente.IdCliente,
                        cliente.TipoCliente,
                        cliente.Nombre,
                        cliente.Apellido,
                        cliente.Cedula,
                        cliente.NombreEmpresa,
                        cliente.Rnc,
                        cliente.Telefono,
                        cliente.Email,
                        cliente.Estado ? "Activo" : "Inactivo"
                    );
                }

                // Aplicar la visibilidad de columnas según el tipo seleccionado
                if (!string.IsNullOrEmpty(tipoSeleccionado))
                {
                    ConfigurarVisibilidadCampos(tipoSeleccionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            cmbTipo.SelectedIndex = -1;
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmpresa.Clear();
            txtCedula.Clear();
            txtRnc.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            chkActivo.Checked = true;
            clienteSeleccionado = null;
            enEdicion = false;
            btnGuardar.Enabled = false;
            btnAgregar.Text = "+ Agregar";

            // Restablecer etiquetas al estado inicial
            lblNombre.Text = "Nombre:";
            lblApellido.Text = "Apellido:";

            // Mostrar todas las columnas nuevamente
            dgvClientes.Columns["Apellido"].Visible = true;
            dgvClientes.Columns["Cedula"].Visible = true;
            dgvClientes.Columns["NombreEmpresa"].Visible = true;
            dgvClientes.Columns["Rnc"].Visible = true;
            CargarClientes();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (enEdicion) { LimpiarFormulario(); return; }
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbTipo.SelectedIndex == -1) { MessageBox.Show("Por favor complete los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var nuevoCliente = new ClientesDto
                {
                    TipoCliente = cmbTipo.SelectedItem.ToString(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    NombreEmpresa = txtEmpresa.Text.Trim(),
                    Cedula = txtCedula.Text.Trim(),
                    Rnc = txtRnc.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Estado = chkActivo.Checked
                };

                if (ClienteBL.InsertarCliente(nuevoCliente)) 
                { 
                    MessageBox.Show("Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    clientesCompletos.Clear(); // Limpiar caché para recargar
                    LimpiarFormulario(); 
                }
                else { MessageBox.Show("Error al agregar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null) { MessageBox.Show("Seleccione un cliente para guardar cambios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                clienteSeleccionado.TipoCliente = cmbTipo.SelectedItem?.ToString() ?? "";
                clienteSeleccionado.Nombre = txtNombre.Text.Trim();
                clienteSeleccionado.Apellido = txtApellido.Text.Trim();
                clienteSeleccionado.NombreEmpresa = txtEmpresa.Text.Trim();
                clienteSeleccionado.Cedula = txtCedula.Text.Trim();
                clienteSeleccionado.Rnc = txtRnc.Text.Trim();
                clienteSeleccionado.Telefono = txtTelefono.Text.Trim();
                clienteSeleccionado.Email = txtEmail.Text.Trim();
                clienteSeleccionado.Direccion = txtDireccion.Text.Trim();
                clienteSeleccionado.Estado = chkActivo.Checked;

                if (ClienteBL.ActualizarCliente(clienteSeleccionado)) 
                { 
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    clientesCompletos.Clear(); // Limpiar caché para recargar
                    LimpiarFormulario(); 
                }
                else { MessageBox.Show("Error al actualizar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null) { MessageBox.Show("Seleccione un cliente para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            enEdicion = true;
            btnGuardar.Enabled = true;
            btnAgregar.Text = "Cancelar";
            cmbTipo.Focus();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null) { MessageBox.Show("Seleccione un cliente para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show($"¿Desea eliminar el cliente {clienteSeleccionado.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (ClienteBL.EliminarCliente(clienteSeleccionado.IdCliente)) 
                    { 
                        MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        clientesCompletos.Clear(); // Limpiar caché para recargar
                        LimpiarFormulario(); 
                    }
                    else { MessageBox.Show("Error al eliminar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e) => LimpiarFormulario();

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener el ID de la celda seleccionada en el DataGrid
                int idClienteSeleccionado = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["IdCliente"].Value);

                // Buscar el cliente en la lista COMPLETA (no en la filtrada)
                clienteSeleccionado = clientesCompletos.FirstOrDefault(c => c.IdCliente == idClienteSeleccionado);

                if (clienteSeleccionado != null)
                {
                    cmbTipo.SelectedItem = clienteSeleccionado.TipoCliente;
                    ConfigurarVisibilidadCampos(clienteSeleccionado.TipoCliente);
                    txtNombre.Text = clienteSeleccionado.Nombre;
                    txtApellido.Text = clienteSeleccionado.Apellido;
                    txtEmpresa.Text = clienteSeleccionado.NombreEmpresa;
                    txtCedula.Text = clienteSeleccionado.Cedula;
                    txtRnc.Text = clienteSeleccionado.Rnc;
                    txtTelefono.Text = clienteSeleccionado.Telefono;
                    txtEmail.Text = clienteSeleccionado.Email;
                    txtDireccion.Text = clienteSeleccionado.Direccion;
                    chkActivo.Checked = clienteSeleccionado.Estado;
                    enEdicion = false;
                    btnGuardar.Enabled = false;
                    btnAgregar.Text = "+ Agregar";
                }
            }
        }
    }
}
