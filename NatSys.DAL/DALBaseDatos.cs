// Proyecto: NatSys.DAL
//
// Operaciones sobre la base de datos en si, no sobre una tabla:
// - probar que se puede conectar
// - comparar las migraciones que ya estan aplicadas en la base con las
//   que trae esta version del programa (asi se detecta si el programa y
//   la base son de versiones distintas).

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NatSys.DAL
{
    public class DALBaseDatos
    {
        private readonly string _connectionString;

        public DALBaseDatos(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Abre y cierra una conexion. Se hace dentro de la "estrategia de
        // ejecucion" para que use los reintentos configurados en
        // NatSysDbContext (por ejemplo, mientras Azure despierta la base).
        // Si igual falla, la excepcion sigue de largo y la BLL decide que
        // mensaje mostrar.
        public void ProbarConexion()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var estrategia = contexto.Database.CreateExecutionStrategy();

            estrategia.Execute(() =>
            {
                contexto.Database.OpenConnection();
                contexto.Database.CloseConnection();
            });
        }

        // Migraciones que trae el programa y que la base todavia no tiene
        // (la base es de una version ANTERIOR al programa)
        public List<string> ObtenerMigracionesPendientes()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            return contexto.Database.GetPendingMigrations().ToList();
        }

        // Migraciones que la base tiene y que este programa no conoce
        // (la base es de una version MAS NUEVA que el programa)
        public List<string> ObtenerMigracionesDesconocidas()
        {
            using var contexto = new NatSysDbContext(_connectionString);
            var aplicadasEnLaBase = contexto.Database.GetAppliedMigrations();
            var delPrograma = contexto.Database.GetMigrations();
            return aplicadasEnLaBase.Except(delPrograma).ToList();
        }
    }
}