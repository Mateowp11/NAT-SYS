// Proyecto: NatSys.UI
//
// Ultima red de seguridad: si algo falla y ninguna pantalla lo atrapo,
// en vez del cartel tecnico de .NET (que ademas cierra el programa) se
// muestra un mensaje entendible y el detalle queda guardado en un archivo
// de log, para poder ver despues que paso.

using NatSys.BLL;

namespace NatSys.UI
{
    internal static class ManejadorErrores
    {
        // C:\Users\<usuario>\AppData\Local\NatSys\errores.log
        public static string RutaLog => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NatSys", "errores.log");

        // Se llama una sola vez, al principio de Main, antes de abrir ventanas
        public static void Activar()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Errores en las pantallas (por ejemplo, al tocar un boton)
            Application.ThreadException += (sender, e) => Mostrar(e.Exception);

            // Errores fuera de las pantallas (ultimo recurso)
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex) Mostrar(ex);
            };
        }

        public static void Mostrar(Exception ex)
        {
            GuardarEnLog(ex);

            string mensaje = GestorBaseDatos.EsErrorDeConexion(ex)
                ? "Se perdió la conexión con la base de datos.\n\n" +
                  "Revisá tu conexión a internet y volvé a intentar. " +
                  "Si el programa estuvo un rato sin usarse, la base puede tardar hasta un minuto en responder."
                : "Ocurrió un error inesperado y puede que la última operación no se haya guardado.\n\n" +
                  "El detalle quedó registrado en:\n" + RutaLog;

            MessageBox.Show(mensaje, "NAT-SYS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void GuardarEnLog(Exception ex)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(RutaLog)!);
                File.AppendAllText(RutaLog, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}");
            }
            catch
            {
                // Si no se puede escribir el log, no hacemos nada: el
                // manejador de errores nunca puede tirar otro error
            }
        }
    }
}