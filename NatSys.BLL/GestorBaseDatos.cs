// Proyecto: NatSys.BLL
//
// Controla, ANTES del login, que la base se pueda usar:
//   1. que haya conexion (internet, firewall de Azure, usuario y clave)
//   2. que la base y el programa sean de la misma version
//      (mismas migraciones aplicadas)
// Traduce los errores tecnicos de SQL Server a mensajes entendibles.
//
// Singleton, igual que el resto de los gestores.

using System;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using NatSys.DAL;

namespace NatSys.BLL
{
    public class GestorBaseDatos
    {
        private static GestorBaseDatos? _instancia;
        private readonly DALBaseDatos _baseDatosDAL;

        // Numeros de error de SQL Server / Azure que significan "no hay
        // conexion" (y no un error de datos, como una clave foranea)
        private static readonly int[] ErroresDeConexion =
        {
            -2,     // tiempo de espera agotado
            53, 40, 121, 233, 10053, 10054, 10060, 11001, // red / servidor no encontrado
            4060,   // no se puede abrir la base pedida
            18456,  // usuario o clave de la base incorrectos
            40613, 40197, 40501, 49918, 49919, 49920, // Azure: base no disponible por el momento
            40615   // Azure: la IP no esta habilitada en el firewall
        };

        private GestorBaseDatos(string connectionString)
        {
            _baseDatosDAL = new DALBaseDatos(connectionString);
        }

        public static void Inicializar(string connectionString)
        {
            if (_instancia == null)
                _instancia = new GestorBaseDatos(connectionString);
        }

        public static GestorBaseDatos GetInstancia()
        {
            if (_instancia == null)
                throw new InvalidOperationException("GestorBaseDatos no fue inicializado.");
            return _instancia;
        }

        public ResultadoVerificacionBase Verificar()
        {
            // 1. Conexion
            try
            {
                _baseDatosDAL.ProbarConexion();
            }
            catch (Exception ex)
            {
                return ResultadoVerificacionBase.Error(MensajeDeConexion(ex), sePuedeReintentar: true);
            }

            // 2. Version de la base contra version del programa
            try
            {
                if (_baseDatosDAL.ObtenerMigracionesPendientes().Count > 0)
                    return ResultadoVerificacionBase.Error(
                        "La base de datos todavía no se actualizó para esta versión del programa.\n\n" +
                        "Avisale a quien administra el sistema para que la actualice.",
                        sePuedeReintentar: false);

                if (_baseDatosDAL.ObtenerMigracionesDesconocidas().Count > 0)
                    return ResultadoVerificacionBase.Error(
                        "Esta versión del programa es anterior a la base de datos.\n\n" +
                        "Pedile la versión nueva a quien administra el sistema.",
                        sePuedeReintentar: false);
            }
            catch (Exception ex)
            {
                return ResultadoVerificacionBase.Error(MensajeDeConexion(ex), sePuedeReintentar: true);
            }

            return ResultadoVerificacionBase.Ok();
        }

        // true si el error (o alguno de los que tiene adentro) es de conexion.
        // Lo usa tambien el manejador global de errores de la UI.
        public static bool EsErrorDeConexion(Exception ex)
        {
            if (BuscarRetryLimit(ex)) return true; // se agotaron los reintentos

            var sql = BuscarSqlException(ex);
            return sql != null && Array.IndexOf(ErroresDeConexion, sql.Number) >= 0;
        }

        private static string MensajeDeConexion(Exception ex)
        {
            var sql = BuscarSqlException(ex);

            if (sql?.Number == 40615)
            {
                // Azure dice que IP intento entrar: se la mostramos al usuario
                // para que se la pase al administrador
                var ip = Regex.Match(sql.Message, @"IP address '([^']+)'").Groups[1].Value;
                return "Tu conexión a internet no tiene permiso para entrar a la base de datos.\n\n" +
                       (ip != "" ? $"Pasale este número a quien administra el sistema: {ip}" : "Avisale a quien administra el sistema.");
            }

            if (sql?.Number == 18456)
                return "El usuario o la contraseña de la base de datos no son correctos.\n\n" +
                       "Revisá el archivo natsys.config.json.";

            if (sql?.Number == 4060)
                return "No se encontró la base de datos, o el usuario no tiene acceso a ella.\n\n" +
                       "Revisá el archivo natsys.config.json.";

            return "No se pudo conectar con la base de datos.\n\n" +
                   "Revisá tu conexión a internet y probá de nuevo. " +
                   "Si hace rato que nadie usa el sistema, la base puede tardar hasta un minuto en responder.";
        }

        private static SqlException? BuscarSqlException(Exception? ex)
        {
            while (ex != null)
            {
                if (ex is SqlException sql) return sql;
                ex = ex.InnerException;
            }
            return null;
        }

        private static bool BuscarRetryLimit(Exception? ex)
        {
            while (ex != null)
            {
                if (ex is RetryLimitExceededException) return true;
                ex = ex.InnerException;
            }
            return false;
        }
    }
}