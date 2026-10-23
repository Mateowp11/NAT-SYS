// Proyecto: NatSys.Entidades
// Iteracion 2 - el tiempo de UNA repeticion dentro de un Resultado

using System;
using System.Globalization;

namespace NatSys.Entidades
{
    public class TiempoRepeticion
    {
        public int IdTiempo { get; set; }
        public int NroRepeticion { get; set; } // 1, 2, 3... (puede haber huecos si la carga es parcial)

        // En SEGUNDOS: 1:05.30 se guarda como 65.30. A diferencia de Marca
        // (que guarda texto), aca se guarda como numero porque los
        // informes de la iteracion 2 promedian y comparan tiempos.
        public decimal Tiempo { get; set; }

        // Solo la columna de la clave foranea hacia Resultado
        public int IdResultado { get; set; }

        // Constructor sin parametros: lo usa EF Core cuando lee de la base
        private TiempoRepeticion()
        {
        }

        public TiempoRepeticion(int nroRepeticion, decimal tiempo)
        {
            NroRepeticion = nroRepeticion;
            Tiempo = tiempo;
        }

        // Lo muestra como lo escribe un nadador, igual que
        // MotorPasajes.FormatearParaMostrar: "32.50" o "01:05.30".
        // InvariantCulture asegura el punto decimal aunque Windows este en español.
        public string Formatear()
        {
            if (Tiempo < 60)
                return Tiempo.ToString("0.00", CultureInfo.InvariantCulture);

            int minutos = (int)(Tiempo / 60);
            decimal segundos = Tiempo - minutos * 60;
            return $"{minutos:00}:" + segundos.ToString("00.00", CultureInfo.InvariantCulture);
        }
    }
}
