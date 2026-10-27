// Proyecto: NatSys.UI
// Iteracion 2 - achica las fotos que se adjuntan a un trabajo diario
//
// Una foto de celular pesa entre 3 y 5 MB. Para leer un pizarron o una
// planilla alcanza con 1600 pixeles del lado mas largo en JPEG, que pesa
// unas 10 veces menos. Asi la base en la nube no se llena de fotos.
//
// Esta en la UI (como ExportadorPDF) porque usa System.Drawing, que es de
// Windows: es un detalle tecnico de como se prepara el archivo, no una
// regla de negocio. El tope de tamaño si es regla, y esta en el gestor.

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NatSys.UI
{
    public static class AchicadorImagenes
    {
        private const int LadoMaximo = 1600;    // pixeles
        private const long CalidadJpeg = 80;     // de 0 a 100
        private const int TamanioYaChico = 1024 * 1024; // 1 MB

        // Etiqueta EXIF donde el celular anota como estaba girado al sacar la foto
        private const int EtiquetaOrientacion = 0x0112;

        private static readonly string[] Extensiones = { ".jpg", ".jpeg", ".png" };

        public static bool EsImagen(string nombreArchivo)
        {
            string extension = Path.GetExtension(nombreArchivo).ToLowerInvariant();
            return Extensiones.Contains(extension);
        }

        // Devuelve la imagen lista para guardar. Si ya es chica (menos de 1600
        // px y menos de 1 MB) la deja como esta; si no, la achica y la pasa a
        // JPEG, y el nombre cambia a .jpg.
        public static byte[] Achicar(byte[] original, ref string nombreArchivo)
        {
            try
            {
                using var entrada = new MemoryStream(original);
                using var imagen = Image.FromStream(entrada);

                bool yaEsChica = Math.Max(imagen.Width, imagen.Height) <= LadoMaximo
                              && original.Length <= TamanioYaChico;
                if (yaEsChica)
                    return original;

                CorregirOrientacion(imagen);

                double escala = Math.Min(1.0, (double)LadoMaximo / Math.Max(imagen.Width, imagen.Height));
                int ancho = Math.Max(1, (int)Math.Round(imagen.Width * escala));
                int alto = Math.Max(1, (int)Math.Round(imagen.Height * escala));

                using var achicada = new Bitmap(ancho, alto);
                using (var lienzo = Graphics.FromImage(achicada))
                {
                    lienzo.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    lienzo.SmoothingMode = SmoothingMode.HighQuality;
                    lienzo.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    lienzo.Clear(Color.White); // un PNG transparente queda sobre fondo blanco
                    lienzo.DrawImage(imagen, 0, 0, ancho, alto);
                }

                var codificadorJpeg = ImageCodecInfo.GetImageEncoders()
                    .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                using var parametros = new EncoderParameters(1);
                parametros.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, CalidadJpeg);

                using var salida = new MemoryStream();
                achicada.Save(salida, codificadorJpeg, parametros);

                nombreArchivo = Path.ChangeExtension(nombreArchivo, ".jpg");
                return salida.ToArray();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is OutOfMemoryException
                                       || ex is System.Runtime.InteropServices.ExternalException)
            {
                // Lo que tira System.Drawing cuando los bytes no son una imagen
                // que pueda leer (curiosamente, a veces como "falta de memoria").
                // Se traduce a un mensaje para el usuario.
                throw new ArgumentException("El archivo no es una imagen válida o no se pudo procesar.");
            }
        }

        // El celular no gira los pixeles: guarda la foto como salio del sensor
        // y anota en el EXIF como hay que mostrarla. Al redibujarla esa nota
        // se pierde, asi que hay que aplicar el giro antes; si no, una foto
        // sacada vertical quedaria acostada.
        private static void CorregirOrientacion(Image imagen)
        {
            if (!imagen.PropertyIdList.Contains(EtiquetaOrientacion)) return;

            byte[]? valor = imagen.GetPropertyItem(EtiquetaOrientacion)?.Value;
            if (valor == null || valor.Length == 0) return;

            RotateFlipType giro = valor[0] switch
            {
                2 => RotateFlipType.RotateNoneFlipX,
                3 => RotateFlipType.Rotate180FlipNone,
                4 => RotateFlipType.Rotate180FlipX,
                5 => RotateFlipType.Rotate90FlipX,
                6 => RotateFlipType.Rotate90FlipNone,
                7 => RotateFlipType.Rotate270FlipX,
                8 => RotateFlipType.Rotate270FlipNone,
                _ => RotateFlipType.RotateNoneFlipNone
            };

            if (giro != RotateFlipType.RotateNoneFlipNone)
                imagen.RotateFlip(giro);
        }
    }
}
