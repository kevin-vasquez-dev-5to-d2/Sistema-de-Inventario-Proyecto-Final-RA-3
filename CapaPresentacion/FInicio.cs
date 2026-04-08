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
using CapaPresentacion;

namespace CapaPresentacion
{
    public partial class FInicio : Form
    {
        public FInicio()
        {
            InitializeComponent();
        }

        private void FInicio_Load(object sender, EventArgs e)
        {
            // Verificar que el usuario esté autenticado
            if (!SesionDto.Autenticado)
            {
                MessageBox.Show("Debe iniciar sesión primero.", "Acceso Denegado", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Configurar el formulario con información del usuario
            ConfigurarInterfazSegunRol();
            MostrarInfoUsuario();
        }

        /// <summary>
        /// Configura la interfaz según el rol del usuario
        /// </summary>
        private void ConfigurarInterfazSegunRol()
        {
            // Mostrar todos los botones a todos los usuarios
            MostrarTodasLasOpciones();
        }

        /// <summary>
        /// Muestra toda la información del usuario en el formulario
        /// </summary>
        private void MostrarInfoUsuario()
        {
            this.Text = $"Sistema de Ventas - {SesionDto.NombreUsuario} ({SesionDto.NombreRol})";
        }

        /// <summary>
        /// Oculta todos los botones
        /// </summary>
        private void OcultarTodosBotones()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    btn.Visible = false;
                }
            }
        }

        /// <summary>
        /// Muestra todas las opciones (Admin)
        /// </summary>
        private void MostrarTodasLasOpciones()
        {
            // Buscar y mostrar todos los botones
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    btn.Visible = true;
                }
            }
        }

        /// <summary>
        /// Muestra solo opciones de Almacén
        /// </summary>
        private void MostrarOpcionesAlmacen()
        {
            // Buscar botones de Movimientos y Productos
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    // Solo mostrar opciones relacionadas a almacén
                    if (btn.Name.ToLower().Contains("movimiento") || 
                        btn.Name.ToLower().Contains("producto") ||
                        btn.Name.ToLower().Contains("categoria") ||
                        btn.Name.ToLower().Contains("proveedor") ||
                        btn.Name.ToLower().Contains("consulta"))
                    {
                        btn.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Muestra solo opciones de Usuario Común
        /// </summary>
        private void MostrarOpcionesUsuarioComun()
        {
            // Buscar solo opciones de visualización
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    // Solo mostrar opciones de consulta/visualización
                    if (btn.Name.ToLower().Contains("consulta") ||
                        btn.Name.ToLower().Contains("reporte") ||
                        btn.Name.ToLower().Contains("venta"))
                    {
                        btn.Visible = true;
                    }
                }
            }
        }

        // Efectos visuales al pasar el mouse sobre los botones
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Underline);
                btn.Cursor = Cursors.Hand;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Font = new Font(btn.Font, btn.Font.Style & ~FontStyle.Underline);
                btn.Cursor = Cursors.Default;
            }
        }

        // Manejador de clic para abrir el formulario de Movimientos
        private void FormMovimientos_Click(object sender, EventArgs e)
        {
            try
            {
                // Solo Admin y Almacén pueden crear movimientos
                if (!SesionDto.EsAdmin() && !SesionDto.EsAlmacen())
                {
                    MessageBox.Show("Acceso Denegado: Solo Admin y Almacén pueden acceder a Movimientos.", 
                        "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                frmMovimiento frmMovimiento = new frmMovimiento();
                frmMovimiento.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de movimientos: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejador de clic para abrir el formulario de Categorías
        private void FormCategorias_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar permisos - Solo Admin
                if (!SesionDto.EsAdmin())
                {
                    MessageBox.Show("No tienes permiso para acceder a Categorías. Solo Admin puede acceder.", 
                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCategoria frmCategoria = new frmCategoria();
                frmCategoria.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de categorías: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejador de clic para abrir el formulario de Productos
        private void FormProductos_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar permisos - Solo Admin
                if (!SesionDto.EsAdmin())
                {
                    MessageBox.Show("No tienes permiso para acceder a Productos. Solo Admin puede acceder.", 
                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmProducto frmProducto = new frmProducto();
                frmProducto.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de productos: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejador de clic para abrir el formulario de Consulta de Movimientos
        private void btnConsultaMovimientos_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar acceso usando ControlAccesoFormularios
                if (!ControlAccesoFormularios.TieneAcceso("frmConsultaMovimientos", SesionDto.Username))
                {
                    MessageBox.Show("Acceso Denegado: Solo los usuarios 'kevin' y 'alopez' pueden acceder a este formulario.", 
                        "Acceso Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                frmConsultaMovimientos frmConsulta = new frmConsultaMovimientos();
                frmConsulta.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de consulta de movimientos: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cierra sesión y regresa al login
        /// </summary>
        private void CerrarSesion()
        {
            DialogResult resultado = MessageBox.Show("¿Deseas cerrar sesión?", 
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                SesionDto.CerrarSesion();
                this.Close();
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar permisos - Solo Admin
                if (!SesionDto.EsAdmin())
                {
                    MessageBox.Show("No tienes permiso para acceder a Clientes. Solo Admin puede acceder.", 
                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmClientes frmClientes = new frmClientes();
                frmClientes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de clientes: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar permisos - Solo Admin
                if (!SesionDto.EsAdmin())
                {
                    MessageBox.Show("No tienes permiso para acceder a Proveedores. Solo Admin puede acceder.", 
                        "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmProveedores frmProveedores = new frmProveedores();
                frmProveedores.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de proveedores: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar autenticación
                if (!SesionDto.Autenticado)
                {
                    MessageBox.Show("Debe iniciar sesión primero.", 
                        "Sesión Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir formulario de Usuarios - Acceso para todos
                frmUsuarios frmUsuarios = new frmUsuarios();
                frmUsuarios.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de usuarios: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {
            // Este método está disponible para personalizar la apariencia del panel
            // Puede usarse para dibujar bordes, colores, etc.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}