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
    public partial class frmLogin : Form
    {
        private int intentosFallidos = 0;
        private const int INTENTOS_MAXIMOS = 3;
        private const int TIEMPO_BLOQUEO = 30000; // 30 segundos en milisegundos

        public frmLogin()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Login - Sistema de Inventario";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Icon = SystemIcons.Shield;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            txtPassword.UseSystemPasswordChar = true;
            intentosFallidos = 0;
            ActualizarEtiquetaIntentos();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validar que no se haya excedido el límite de intentos
            if (intentosFallidos >= INTENTOS_MAXIMOS)
            {
                BloquearFormulario();
                return;
            }

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Por favor ingresa usuario y contraseña.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar credenciales
                UsuariosDto usuario = UsuariosBL.ValidarLogin(username, password);

                if (usuario != null)
                {
                    // Iniciar sesión
                    SesionDto.IniciarSesion(usuario);

                    MessageBox.Show($"¡Bienvenido {usuario.Nombre}!", "Login Exitoso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir formulario principal
                    FInicio formPrincipal = new FInicio();
                    this.Hide();
                    formPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    IncrementarIntentosFallidos();
                }
            }
            catch (Exception ex)
            {
                IncrementarIntentosFallidos();
                MessageBox.Show("Error: " + ex.Message, "Error de Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        /// <summary>
        /// Incrementa el contador de intentos fallidos
        /// </summary>
        private void IncrementarIntentosFallidos()
        {
            intentosFallidos++;
            ActualizarEtiquetaIntentos();

            if (intentosFallidos >= INTENTOS_MAXIMOS)
            {
                MessageBox.Show(
                    $"Has excedido el máximo de intentos ({INTENTOS_MAXIMOS}).\n\nEl acceso ha sido bloqueado.\n\nLa aplicación se cerrará.",
                    "Seguridad - Acceso Bloqueado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                BloquearFormulario();
            }
            else
            {
                int intentosRestantes = INTENTOS_MAXIMOS - intentosFallidos;
                MessageBox.Show(
                    $"Usuario o contraseña incorrectos.\n\nIntentos restantes: {intentosRestantes}",
                    "Login Fallido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            txtPassword.Clear();
            txtUsername.Focus();
        }

        /// <summary>
        /// Bloquea el formulario y cierra la aplicación
        /// </summary>
        private void BloquearFormulario()
        {
            // Bloquear controles
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            chkMostrarContraseña.Enabled = false;
            btnCancelar.Enabled = false;

            // Cambiar colores para indicar bloqueo
            txtUsername.BackColor = Color.LightGray;
            txtPassword.BackColor = Color.LightGray;
            btnLogin.BackColor = Color.DarkGray;
            btnCancelar.BackColor = Color.DarkGray;

            // Mostrar mensaje de bloqueo
            lblMensajeBloqueo.Visible = true;
            lblMensajeBloqueo.Text = "ACCESO BLOQUEADO\nLa aplicación se cerrará en 5 segundos...";
            lblMensajeBloqueo.ForeColor = Color.Red;

            // Timer para cerrar la aplicación
            Timer timerCierre = new Timer();
            timerCierre.Interval = 5000; // 5 segundos
            timerCierre.Tick += (s, e) =>
            {
                timerCierre.Stop();
                Application.Exit();
            };
            timerCierre.Start();
        }

        /// <summary>
        /// Actualiza la etiqueta con el número de intentos
        /// </summary>
        private void ActualizarEtiquetaIntentos()
        {
            if (intentosFallidos > 0)
            {
                int intentosRestantes = INTENTOS_MAXIMOS - intentosFallidos;
                lblIntentos.Text = $"Intentos restantes: {intentosRestantes}/{INTENTOS_MAXIMOS}";
                lblIntentos.ForeColor = intentosRestantes <= 1 ? Color.Red : Color.Orange;
            }
            else
            {
                lblIntentos.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
                e.Handled = true;
            }
        }

        private void chkMostrarContraseña_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkMostrarContraseña.Checked;
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
        }
    }
}