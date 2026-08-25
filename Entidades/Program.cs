// Proyecto: NatSys.UI

using NatSys.BLL;

namespace NatSys.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // TODO: cuando armemos el instalador, esta cadena va a venir de
            // un archivo de configuracion externo (distinta para cada
            // cliente). Por ahora, para desarrollar y probar, la dejamos
            // fija aca. Ajustá el nombre del servidor a tu SQL Server local.
            string connectionString =
                @"Server=(localdb)\mssqllocaldb;Database=NatSysDB;Trusted_Connection=True;TrustServerCertificate=True;";

            GestorSeguridad.Inicializar(connectionString);
            GestorUsuarios.Inicializar(connectionString);
            GestorGrupos.Inicializar(connectionString);
            GestorAtleta.Inicializar(connectionString);
            MotorPasajes.Inicializar(connectionString);

            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}