// Proyecto: NatSys.BLL
//
// Todo lo que devuelve FachadaPasajes.CalcularPlan en un solo objeto:
// el plan (para poder guardarlo despues), las filas para la grilla y la
// comparacion con el record personal (CU-01 paso 11).

using System;
using System.Collections.Generic;
using NatSys.Entidades;

namespace NatSys.BLL
{
    public class ResultadoCalculoPasajes
    {
        // El plan calculado, todavia sin guardar
        public PlanPasaje Plan { get; set; } = new PlanPasaje();

        // Parcial y acumulado de cada tramo, ya formateados
        public List<FilaPasaje> Filas { get; set; } = new List<FilaPasaje>();

        public int LargoPileta { get; set; }

        // ---- Comparacion con el RP (CA-02: si es SV, no hay comparacion) ----

        public bool TieneRecordPersonal { get; set; }
        public string TiempoRecordPersonal { get; set; } = ""; // ej "01:05.32"
        public DateTime FechaRecordPersonal { get; set; }

        // "Alcanzable", "Ambiciosa" o "Poco realista" (vacio si no hay RP)
        public string Viabilidad { get; set; } = "";

        // Segundos de diferencia entre el RP y la marca objetivo:
        // positivo = el objetivo es mas rapido que el RP
        public decimal DiferenciaConRP { get; set; }

        // EXT-01: la pantalla usa esto para mostrar la advertencia
        public bool EsPocoRealista => Viabilidad == "Poco realista";
    }
}