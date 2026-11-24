// Proyecto: NatSys.UI
//
// Configuracion de cada instalacion: sale del archivo natsys.config.json,
// que va en la MISMA carpeta que NatSys.UI.exe. Cada instalacion tiene el
// suyo (la PC del entrenador apunta a Azure, tu PC a LocalDB).
//
// IMPORTANTE: natsys.config.json tiene la contrasena de la base, por eso
// NO se sube a Git (esta en el .gitignore). En el repositorio queda solo
// natsys.config.ejemplo.json como modelo.
//
// Si el archivo no existe, se usa la base local de desarrollo (LocalDB),
// asi en Visual Studio todo sigue funcionando como antes.

using System.Text.Json;

namespace NatSys.UI
{
    internal static class ConfiguracionApp
    {
        public const string NombreArchivo = "natsys.config.json";

        // Rama iteracion-2: base aparte (NatSysDB_Diploma) para no tocar la de
        // Ingenieria de Software (NatSysDB). Tiene que coincidir con la de
        // NatSysDbFactory, que es la que usan Add-Migration y Update-Database.
        private const string CadenaDesarrollo =
            @"Server=(localdb)\mssqllocaldb;Database=NatSysDB_Diploma;Trusted_Connection=True;TrustServerCertificate=True;";

        public static string RutaArchivo => Path.Combine(AppContext.BaseDirectory, NombreArchivo);

        public static string ObtenerCadenaConexion()
        {
            if (!File.Exists(RutaArchivo))
                return CadenaDesarrollo;

            // Se permiten comentarios (//) y comas de mas, para que sea
            // facil de editar a mano con el Bloc de notas
            var opciones = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            using var documento = JsonDocument.Parse(File.ReadAllText(RutaArchivo), opciones);

            if (!documento.RootElement.TryGetProperty("CadenaConexion", out var valor)
                || string.IsNullOrWhiteSpace(valor.GetString()))
                throw new InvalidOperationException($"Al archivo {NombreArchivo} le falta el dato \"CadenaConexion\".");

            return valor.GetString()!;
        }
    }
}