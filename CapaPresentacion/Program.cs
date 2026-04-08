using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cargar ensamblados nativos necesarios (si la librería está referenciada)
            try
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch
            {
                // Ignorar si no está disponible en el entorno de diseño o si no está referenciada.
            }

            // Mostrar formulario de login primero
            Application.Run(new frmLogin());
        }
    }
}
