// Proyecto: NatSys.UI

using NatSys.BLL;

namespace NatSys.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Primero el manejador global de errores, antes de abrir cualquier ventana
            ManejadorErrores.Activar();
            ApplicationConfiguration.Initialize();

            // Letra un poco mas grande en todo el sistema (antes 9 pt). Como los
            // formularios usan AutoScaleMode.Font, cada ventana se agranda en la
            // misma proporcion y nada queda encimado. Tiene que ir antes de
            // abrir la primera ventana.
            Application.SetDefaultFont(new Font("Segoe UI", 10F));

            // La cadena de conexion sale de natsys.config.json (al lado del
            // .exe). Si ese archivo no existe, se usa LocalDB (desarrollo).
            string connectionString;
            try
            {
                connectionString = ConfiguracionApp.ObtenerCadenaConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo leer el archivo de configuración {ConfiguracionApp.NombreArchivo}.\n\n{ex.Message}",
                    "NAT-SYS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GestorBaseDatos.Inicializar(connectionString);
            GestorSeguridad.Inicializar(connectionString);
            GestorUsuarios.Inicializar(connectionString);
            GestorGrupos.Inicializar(connectionString);
            GestorAtleta.Inicializar(connectionString);
            GestorTorneos.Inicializar(connectionString);
            GestorPruebas.Inicializar(connectionString);
            GestorMarcas.Inicializar(connectionString);
            MotorPasajes.Inicializar(connectionString);
            GestorReportes.Inicializar(connectionString);

            // Antes del login: ¿hay conexion y la base es de esta version?
            // Si no, frmConexion ya le explico el problema al usuario y se sale.
            using (var frmConexion = new frmConexion())
            {
                if (frmConexion.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new frmLogin());
        }
    }
}