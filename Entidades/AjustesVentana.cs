// Proyecto: NatSys.UI
//
// Comodidades comunes a todas las ventanas. Cada formulario lo activa con
// una linea en su constructor:
//     AjustesVentana.Activar(this, abrirMaximizada: true);
//
// Que hace:
// - El tamaño con que se diseño la ventana pasa a ser el MINIMO: se puede
//   agrandar o maximizar, pero no achicar tanto que se encimen los controles.
//   (Lo que crece al agrandar lo decide el Anchor de cada control.)
// - Se acuerda del tamaño, el lugar y si estaba maximizada, y la vuelve a
//   abrir asi. Se guarda por usuario de Windows en
//   C:\Users\<usuario>\AppData\Local\NatSys\ventanas.json
// - La primera vez, las de listas y reportes se abren maximizadas.

using System.Text.Json;

namespace NatSys.UI
{
    internal static class AjustesVentana
    {
        private static readonly string Ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NatSys", "ventanas.json");

        // Se lee el archivo una sola vez y se guarda en memoria
        private static Dictionary<string, PosicionVentana>? _posiciones;

        public static void Activar(Form ventana, bool abrirMaximizada)
        {
            ventana.Load += (sender, e) => AlAbrir(ventana, abrirMaximizada);
            ventana.FormClosing += (sender, e) => AlCerrar(ventana);
        }

        private static void AlAbrir(Form ventana, bool abrirMaximizada)
        {
            // En Load ya se aplico la escala de la letra, asi que este es el
            // tamaño real de diseño. Nunca mas grande que la pantalla, para
            // que en notebooks chicas la ventana igual entre.
            var area = Screen.FromControl(ventana).WorkingArea;
            ventana.MinimumSize = new Size(
                Math.Min(ventana.Width, area.Width),
                Math.Min(ventana.Height, area.Height));

            if (Posiciones().TryGetValue(ventana.Name, out var guardada) && EntraEnAlgunaPantalla(guardada))
            {
                ventana.StartPosition = FormStartPosition.Manual;
                ventana.Bounds = new Rectangle(
                    guardada.X,
                    guardada.Y,
                    Math.Max(guardada.Ancho, ventana.MinimumSize.Width),
                    Math.Max(guardada.Alto, ventana.MinimumSize.Height));

                if (guardada.Maximizada)
                    ventana.WindowState = FormWindowState.Maximized;
            }
            else if (abrirMaximizada)
            {
                ventana.WindowState = FormWindowState.Maximized;
            }
        }

        private static void AlCerrar(Form ventana)
        {
            // RestoreBounds = el tamaño "normal" aunque este maximizada o minimizada
            var r = ventana.WindowState == FormWindowState.Normal ? ventana.Bounds : ventana.RestoreBounds;

            Posiciones()[ventana.Name] = new PosicionVentana
            {
                X = r.X,
                Y = r.Y,
                Ancho = r.Width,
                Alto = r.Height,
                Maximizada = ventana.WindowState == FormWindowState.Maximized
            };

            Guardar();
        }

        // Si se desconecto un monitor, la posicion guardada puede quedar fuera
        // de la pantalla: en ese caso se ignora (se ve al menos la barra de titulo)
        private static bool EntraEnAlgunaPantalla(PosicionVentana p)
        {
            var barraDeTitulo = new Rectangle(p.X, p.Y, p.Ancho, 40);
            return Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(barraDeTitulo));
        }

        private static Dictionary<string, PosicionVentana> Posiciones()
        {
            if (_posiciones == null)
            {
                try
                {
                    _posiciones = File.Exists(Ruta)
                        ? JsonSerializer.Deserialize<Dictionary<string, PosicionVentana>>(File.ReadAllText(Ruta))
                        : null;
                }
                catch
                {
                    _posiciones = null; // archivo roto: se empieza de cero
                }

                _posiciones ??= new Dictionary<string, PosicionVentana>();
            }
            return _posiciones;
        }

        private static void Guardar()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Ruta)!);
                File.WriteAllText(Ruta, JsonSerializer.Serialize(_posiciones, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch
            {
                // Si no se puede guardar, no pasa nada: la proxima vez se
                // abre con el tamaño de siempre
            }
        }
    }
}
