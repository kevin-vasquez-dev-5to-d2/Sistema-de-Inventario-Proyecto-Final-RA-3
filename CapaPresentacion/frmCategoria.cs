using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class frmCategoria : Form
    {
        private List<CategoriaDto> categorias = new List<CategoriaDto>();
        private CategoriaDto categoriaSeleccionada = null;
        private bool enEdicion = false;

        // Controles
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Panel pnlUsuario;
        private Label lblNombreUsuario;
        private Label lblUsernameUsuario;
        private Label lblRolUsuario;
        private Panel pnlFormulario;
        private Label lblNombre;
        private TextBox txtNombre;
        private CheckBox chkActivo;
        private Panel pnlBotones;
        private Button btnAgregar;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnSalir;
        private Panel pnlDataGrid;
        private DataGridView dgvCategorias;

        public frmCategoria()
        {
            InitializeComponent();
            CrearControles();
            ConfigurarEventos();
        }

        // Minimal InitializeComponent stub because designer file is not present
        private void InitializeComponent()
        {
            // Intentionally left blank - controls are created in CrearControles
        }

        private void CrearControles()
        {
            // Panel Encabezado
            pnlEncabezado = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 80 };
            lblTitulo = new Label { AutoSize = true, Text = "Gestión de Categorías", Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Location = new Point(15, 15) };
            
            // Panel Usuario
            pnlUsuario = new Panel { BackColor = Color.FromArgb(240, 240, 240), BorderStyle = BorderStyle.FixedSingle, Size = new Size(230, 60), Location = new Point(650, 8) };
            lblNombreUsuario = new Label { AutoSize = true, Font = new Font("Arial", 10, FontStyle.Bold), Location = new Point(8, 6) };
            lblUsernameUsuario = new Label { AutoSize = true, Font = new Font("Arial", 8), ForeColor = Color.Gray, Location = new Point(8, 26) };
            lblRolUsuario = new Label { AutoSize = true, BackColor = Color.FromArgb(52, 152, 219), Font = new Font("Arial", 7.5f, FontStyle.Bold), ForeColor = Color.White, Location = new Point(8, 42), Padding = new Padding(4, 2, 4, 2) };
            
            pnlUsuario.Controls.Add(lblNombreUsuario);
            pnlUsuario.Controls.Add(lblUsernameUsuario);
            pnlUsuario.Controls.Add(lblRolUsuario);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Controls.Add(pnlUsuario);

            // Panel Formulario
            pnlFormulario = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 100 };
            lblNombre = new Label { AutoSize = true, Text = "Nombre:", Location = new Point(15, 15) };
            txtNombre = new TextBox { Location = new Point(80, 12), Size = new Size(350, 20), TabIndex = 1 };
            chkActivo = new CheckBox { AutoSize = true, Checked = true, Text = "Activo", Location = new Point(80, 45), TabIndex = 2 };
            
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(chkActivo);

            // Panel Botones
            pnlBotones = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Top, Padding = new Padding(15), Height = 60 };
            btnAgregar = CrearBoton("+ Agregar", Color.LimeGreen, 15, 15);
            btnGuardar = CrearBoton("Guardar", Color.DodgerBlue, 105, 15);
            btnEditar = CrearBoton("Editar", Color.Orange, 195, 15);
            btnEliminar = CrearBoton("Eliminar", Color.Red, 285, 15);
            btnCancelar = CrearBoton("Cancelar", Color.Gray, 375, 15);
            btnSalir = CrearBoton("Salir", Color.Maroon, 800, 15);
            
            pnlBotones.Controls.Add(btnAgregar);
            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnEditar);
            pnlBotones.Controls.Add(btnEliminar);
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnSalir);

            // Panel DataGrid
            pnlDataGrid = new Panel { BorderStyle = BorderStyle.FixedSingle, Dock = DockStyle.Fill, Padding = new Padding(15) };
            dgvCategorias = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = false };
            dgvCategorias.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCategoria", HeaderText = "ID", Visible = false });
            dgvCategorias.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre", Width = 250, ReadOnly = true });
            dgvCategorias.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", Width = 80, ReadOnly = true });
            
            pnlDataGrid.Controls.Add(dgvCategorias);

            // Configurar Formulario
            Controls.Add(pnlDataGrid);
            Controls.Add(pnlBotones);
            Controls.Add(pnlFormulario);
            Controls.Add(pnlEncabezado);
            
            ClientSize = new Size(900, 590);
            Text = "Gestión de Categorías";
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
            Load += FrmCategoria_Load;
            btnAgregar.Click += BtnAgregar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnSalir.Click += BtnSalir_Click;
            dgvCategorias.CellDoubleClick += DgvCategorias_CellDoubleClick;
        }

        private void ConfigurarDataGrid()
        {
            dgvCategorias.AutoGenerateColumns = false;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarSesionUsuario();
            CargarCategorias();
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
                ActualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ActualizarDataGrid()
        {
            dgvCategorias.Rows.Clear();
            foreach (var cat in categorias)
            {
                string estado = cat.Estado == 1 ? "Activo" : "Inactivo";
                dgvCategorias.Rows.Add(cat.IdCategoria, cat.Nombre, estado);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Length < 3)
            {
                MessageBox.Show("El nombre debe tener al menos 3 caracteres");
                return false;
            }
            return true;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            enEdicion = false;
            categoriaSeleccionada = null;
            LimpiarControles();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (enEdicion && categoriaSeleccionada != null)
                {
                    categoriaSeleccionada.Nombre = txtNombre.Text;
                    categoriaSeleccionada.Estado = chkActivo.Checked ? 1 : 0;
                    if (CategoriaBL.ActualizarCategoria(categoriaSeleccionada))
                    {
                        MessageBox.Show("Actualizado");
                        CargarCategorias();
                        LimpiarControles();
                    }
                }
                else
                {
                    if (CategoriaBL.InsertarCategoria(new CategoriaDto { Nombre = txtNombre.Text, Estado = chkActivo.Checked ? 1 : 0 }))
                    {
                        MessageBox.Show("Creado");
                        CargarCategorias();
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
            if (dgvCategorias.SelectedRows.Count == 0) return;

            try
            {
                int id = Convert.ToInt32(dgvCategorias.SelectedRows[0].Cells["IdCategoria"].Value);
                categoriaSeleccionada = categorias.FirstOrDefault(c => c.IdCategoria == id);
                if (categoriaSeleccionada != null)
                {
                    enEdicion = true;
                    txtNombre.Text = categoriaSeleccionada.Nombre;
                    chkActivo.Checked = categoriaSeleccionada.Estado == 1;
                }
            }
            catch { }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0) return;

            try
            {
                int id = Convert.ToInt32(dgvCategorias.SelectedRows[0].Cells["IdCategoria"].Value);
                if (MessageBox.Show("¿Confirma?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CategoriaBL.EliminarCategoria(id))
                    {
                        MessageBox.Show("Eliminado");
                        CargarCategorias();
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
            chkActivo.Checked = true;
            categoriaSeleccionada = null;
            enEdicion = false;
            dgvCategorias.ClearSelection();
        }

        private void DgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) BtnEditar_Click(null, null);
        }
    }
}
