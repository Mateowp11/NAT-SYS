// Proyecto: NatSys.UI
//
// Arma los PDF con MigraDoc (paquete NuGet PDFsharp-MigraDoc-GDI).
//
// Esta en la UI y no en la BLL porque un PDF es otra forma de MOSTRAR la
// informacion, igual que una pantalla: los datos ya llegan procesados
// desde la BLL (FachadaPasajes y GestorReportes). Asi la BLL no depende de
// ninguna libreria de PDF.
//
// Como se arma un PDF con MigraDoc:
//   Document (el archivo) -> Section (las hojas) -> Paragraph / Table / Image
//   y al final PdfDocumentRenderer lo "dibuja" y lo guarda.

using System.Diagnostics;
using System.Globalization;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using NatSys.BLL;

// Alias: "cuando diga Document, Section, Table, Row o Unit, me refiero a
// los de MigraDoc". .NET tiene otras clases que se llaman igual (por
// ejemplo System.Reflection.Metadata.Document) y sin esto el compilador
// no sabe cual usar (error CS0104 "referencia ambigua").
using Document = MigraDoc.DocumentObjectModel.Document;
using Section = MigraDoc.DocumentObjectModel.Section;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;
using Row = MigraDoc.DocumentObjectModel.Tables.Row;
using Unit = MigraDoc.DocumentObjectModel.Unit;

namespace NatSys.UI
{
    public static class ExportadorPdf
    {
        // ---------- Para usar desde las pantallas ----------

        // Pregunta donde guardar, genera el PDF y ofrece abrirlo.
        // "generar" es el metodo que arma el PDF en la ruta elegida.
        public static void GuardarConDialogo(string nombreSugerido, Action<string> generar)
        {
            using var dialogo = new SaveFileDialog
            {
                Title = "Guardar PDF",
                Filter = "Documento PDF (*.pdf)|*.pdf",
                FileName = LimpiarNombreArchivo(nombreSugerido) + ".pdf"
            };

            if (dialogo.ShowDialog() != DialogResult.OK) return;

            try
            {
                generar(dialogo.FileName);
            }
            catch (Exception ex) // ej: el archivo esta abierto en otro programa
            {
                MessageBox.Show("No se pudo generar el PDF:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var abrir = MessageBox.Show("PDF generado. ¿Querés abrirlo?", "Listo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (abrir == DialogResult.Yes)
                Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
        }

        // ---------- Plan de pasajes (CU-01: "disponible para exportacion") ----------

        public static void ExportarPlanPasajes(string ruta, string atleta, string prueba, string entrenador,
                                               string marcaObjetivo, ResultadoCalculoPasajes resultado)
        {
            var documento = CrearDocumento("Plan de pasajes");
            var seccion = documento.LastSection!;

            AgregarTitulo(seccion, "Plan de pasajes");
            AgregarDatos(seccion,
                ("Atleta:", atleta),
                ("Prueba:", prueba),
                ("Pileta:", resultado.LargoPileta + " m"),
                ("Marca objetivo:", marcaObjetivo),
                ("Entrenador:", entrenador),
                ("Fecha:", resultado.Plan.Fecha.ToString("dd/MM/yyyy")));

            AgregarSubtitulo(seccion, "Parciales recomendados");
            AgregarTabla(seccion,
                new[] { "Tramo", "Metros", "Parcial", "Acumulado" },
                new[] { 2.5, 3.0, 3.5, 3.5 },
                resultado.Filas.Select(f => new[] { f.Tramo.ToString(), f.Metros, f.Parcial, f.Acumulado }));

            if (resultado.TieneRecordPersonal)
            {
                AgregarParrafo(seccion, $"Récord personal en pileta de {resultado.LargoPileta} m: " +
                    $"{resultado.TiempoRecordPersonal} ({resultado.FechaRecordPersonal:dd/MM/yyyy}).");
                AgregarParrafo(seccion, "Viabilidad del objetivo: " + resultado.Viabilidad + ".", true);
            }
            else
            {
                AgregarParrafo(seccion, $"Récord personal en pileta de {resultado.LargoPileta} m: SV (sin marcas previas).");
            }

            AgregarNota(seccion, "Distribución calculada con los parciales reales de los finalistas de " +
                "Mundiales y Juegos Olímpicos en esta misma prueba.");

            Guardar(documento, ruta);
        }

        // ---------- RD-06.1 Viabilidad de marca ----------

        public static void ExportarReporteViabilidad(string ruta, string atleta, ReporteViabilidad r)
        {
            var documento = CrearDocumento("Reporte de viabilidad de marca");
            var seccion = documento.LastSection!;

            AgregarTitulo(seccion, "Reporte de viabilidad de marca");
            AgregarDatos(seccion,
                ("Atleta:", atleta),
                ("Prueba:", r.Prueba),
                ("Pileta:", r.LargoPileta + " m"),
                ("Marca objetivo:", r.MarcaObjetivo),
                ("Fecha objetivo:", r.FechaObjetivo.ToString("dd/MM/yyyy")),
                ("Marcas analizadas:", r.CantidadMarcas.ToString()));

            AgregarSubtitulo(seccion, "Resultado");
            AgregarDatos(seccion,
                ("Récord personal:", $"{r.RecordPersonal} ({r.FechaRecordPersonal:dd/MM/yyyy})"),
                ("Tendencia:", TextoTendencia(r)),
                ("Proyección:", $"{r.Proyeccion} al {r.FechaObjetivo:dd/MM/yyyy}"),
                ("Veredicto:", r.Veredicto.ToUpper()));
            AgregarParrafo(seccion, r.Conclusion);

            AgregarSubtitulo(seccion, "Progresión del récord personal");
            AgregarTabla(seccion,
                new[] { "Fecha", "Tiempo", "Tipo", "Mejora" },
                new[] { 3.5, 3.5, 3.0, 4.0 },
                r.Progresion.Select(p => new[] { p.Fecha, p.Tiempo, p.Tipo, p.Mejora }));

            AgregarNota(seccion, "La tendencia es una regresión lineal de las marcas de los últimos 12 meses. " +
                "La meta es ambiciosa si pide hasta un 3 % más que la proyección, y poco realista si pide más.");

            Guardar(documento, ruta);
        }

        // Tambien la usa la pantalla del reporte
        public static string TextoTendencia(ReporteViabilidad r)
        {
            if (!r.TieneTendencia) return "sin datos suficientes";
            if (r.MejoraPorMes > 0) return "mejora " + Decimales(r.MejoraPorMes) + " s por mes";
            if (r.MejoraPorMes < 0) return "empeora " + Decimales(-r.MejoraPorMes) + " s por mes";
            return "estable";
        }

        // ---------- RF-06.2 Evolucion por estilo ----------

        // rutaGrafico: imagen PNG del grafico (la genera la pantalla con ScottPlot)
        public static void ExportarReporteEvolucion(string ruta, string atleta, ReporteEvolucion r, string? rutaGrafico)
        {
            var documento = CrearDocumento("Análisis de evolución por estilo");
            var seccion = documento.LastSection!;

            AgregarTitulo(seccion, "Análisis de evolución por estilo");
            AgregarDatos(seccion,
                ("Atleta:", atleta),
                ("Pileta:", r.LargoPileta + " m"),
                ("Estilo:", r.Estilo),
                ("Período:", $"{r.Desde:dd/MM/yyyy} al {r.Hasta:dd/MM/yyyy}"));

            if (rutaGrafico != null && File.Exists(rutaGrafico))
            {
                var imagen = seccion.AddImage(rutaGrafico);
                imagen.Width = Unit.FromCentimeter(17);
                imagen.LockAspectRatio = true;
                seccion.AddParagraph();
            }

            AgregarSubtitulo(seccion, "Resumen por prueba");
            AgregarTabla(seccion,
                new[] { "Prueba", "Marcas", "Primera", "Mejor", "Última", "Mejora", "Estado" },
                new[] { 3.2, 1.6, 2.1, 2.1, 2.1, 2.0, 3.9 },
                r.Resumen.Select(f => new[] { f.Prueba, f.Marcas.ToString(), f.Primera, f.Mejor, f.Ultima, f.Mejora, f.Estado }));

            AgregarNota(seccion, "Mejora = cuánto bajó la mejor marca respecto de la primera del período. " +
                "Mejora notable: 2 % o más. Mejora: entre 0.5 % y 2 %. Estancado: menos de 0.5 %. " +
                "En retroceso: la última marca es más de 0.5 % más lenta que la primera.");

            Guardar(documento, ruta);
        }

        // ---------- Piezas comunes ----------

        private static Document CrearDocumento(string titulo)
        {
            var documento = new Document();
            documento.Info.Title = titulo;
            documento.Info.Author = "NAT-SYS";

            // Fuente de todo el documento (Arial viene instalada en Windows)
            var normal = documento.Styles[StyleNames.Normal]!;
            normal.Font.Name = "Arial";
            normal.Font.Size = 10;

            var seccion = documento.AddSection();
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(2);

            // Pie de pagina, se repite en todas las hojas
            var pie = seccion.Footers.Primary.AddParagraph($"Generado por NAT-SYS el {DateTime.Now:dd/MM/yyyy HH:mm}");
            pie.Format.Font.Size = 8;
            pie.Format.Font.Color = Colors.Gray;
            pie.Format.Alignment = ParagraphAlignment.Center;

            return documento;
        }

        private static void AgregarTitulo(Section seccion, string titulo)
        {
            var sistema = seccion.AddParagraph("NAT-SYS");
            sistema.Format.Font.Size = 9;
            sistema.Format.Font.Color = Colors.Gray;

            var parrafo = seccion.AddParagraph(titulo);
            parrafo.Format.Font.Size = 16;
            parrafo.Format.Font.Bold = true;
            parrafo.Format.Font.Color = Colors.DarkBlue;
            parrafo.Format.SpaceAfter = Unit.FromPoint(10);
        }

        private static void AgregarSubtitulo(Section seccion, string texto)
        {
            var parrafo = seccion.AddParagraph(texto);
            parrafo.Format.Font.Size = 12;
            parrafo.Format.Font.Bold = true;
            parrafo.Format.SpaceBefore = Unit.FromPoint(8);
            parrafo.Format.SpaceAfter = Unit.FromPoint(4);
        }

        private static void AgregarParrafo(Section seccion, string texto, bool negrita = false)
        {
            var parrafo = seccion.AddParagraph(texto);
            parrafo.Format.Font.Bold = negrita;
            parrafo.Format.SpaceAfter = Unit.FromPoint(4);
        }

        private static void AgregarNota(Section seccion, string texto)
        {
            var parrafo = seccion.AddParagraph(texto);
            parrafo.Format.Font.Size = 8;
            parrafo.Format.Font.Italic = true;
            parrafo.Format.Font.Color = Colors.Gray;
            parrafo.Format.SpaceBefore = Unit.FromPoint(8);
        }

        // Pares "etiqueta: valor" en dos columnas, sin bordes
        private static void AgregarDatos(Section seccion, params (string Etiqueta, string Valor)[] datos)
        {
            var tabla = seccion.AddTable();
            tabla.AddColumn(Unit.FromCentimeter(4));
            tabla.AddColumn(Unit.FromCentimeter(13));

            foreach (var (etiqueta, valor) in datos)
            {
                var fila = tabla.AddRow();
                fila.Cells[0].AddParagraph(etiqueta).Format.Font.Bold = true;
                fila.Cells[1].AddParagraph(valor);
            }

            seccion.AddParagraph().Format.SpaceAfter = Unit.FromPoint(4);
        }

        // Tabla con encabezado gris y bordes finos
        private static void AgregarTabla(Section seccion, string[] encabezados, double[] anchosCm,
                                         IEnumerable<string[]> filas)
        {
            Table tabla = seccion.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.Gray;

            foreach (double ancho in anchosCm)
                tabla.AddColumn(Unit.FromCentimeter(ancho));

            Row encabezado = tabla.AddRow();
            encabezado.HeadingFormat = true; // se repite si la tabla sigue en otra hoja
            encabezado.Format.Font.Bold = true;
            encabezado.Shading.Color = Colors.LightGray;
            for (int i = 0; i < encabezados.Length; i++)
                encabezado.Cells[i].AddParagraph(encabezados[i]);

            foreach (string[] datos in filas)
            {
                Row fila = tabla.AddRow();
                for (int i = 0; i < datos.Length; i++)
                    fila.Cells[i].AddParagraph(datos[i]);
            }

            seccion.AddParagraph().Format.SpaceAfter = Unit.FromPoint(4);
        }

        // MigraDoc "dibuja" el documento y PDFsharp lo guarda como PDF
        private static void Guardar(Document documento, string ruta)
        {
            var renderer = new PdfDocumentRenderer { Document = documento };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(ruta);
        }

        // Siempre con punto decimal, aunque Windows este en español
        private static string Decimales(decimal valor) => valor.ToString("0.00", CultureInfo.InvariantCulture);

        // Saca los caracteres que Windows no acepta en un nombre de archivo
        private static string LimpiarNombreArchivo(string nombre)
        {
            return string.Join("_", nombre.Split(Path.GetInvalidFileNameChars()));
        }
    }
}