// Proyecto: NatSys.DAL
//
// EF Core necesita crear una instancia de NatSysDbContext para poder
// generar migraciones (Add-Migration) o aplicarlas (Update-Database).
// Como el constructor de NatSysDbContext exige una cadena de conexion
// (para que la app real funcione con distintos clientes), la herramienta
// no sabe de donde sacarla - esta clase se la provee SOLO para ese
// momento de diseno. EF Core la detecta automaticamente por implementar
// esta interfaz, no hace falta registrarla en ningun lado.
//
// La app real NUNCA usa esta clase - Program.cs sigue siendo el unico
// lugar que decide la cadena de conexion cuando el programa corre.

using Microsoft.EntityFrameworkCore.Design;

namespace NatSys.DAL
{
    public class NatSysDbContextFactory : IDesignTimeDbContextFactory<NatSysDbContext>
    {
        public NatSysDbContext CreateDbContext(string[] args)
        {
            string connectionStringDiseno =
                @"Server=(localdb)\mssqllocaldb;Database=NatSysDB;Trusted_Connection=True;TrustServerCertificate=True;";

            return new NatSysDbContext(connectionStringDiseno);
        }
    }
}