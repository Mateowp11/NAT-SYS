using System;
using System.Collections.Generic;
using System.Text;

namespace NatSys.Entidades
{
    public class Pasaje
    {
        public int IdPasaje { get; set; }
        public int NroTramo { get; set; }
        public string Tiempo { get; set; } // parcial recomendado, mm:ss.cc
        public int Distancia { get; set; }

        public int IdPlanPasaje { get; set; }
        public PlanPasaje Plan { get; set; }

        public decimal ConvertirASegundos()
        {
            var partes = Tiempo.Split(':', '.');
            var minutos = decimal.Parse(partes[0]);
            var segundos = decimal.Parse(partes[1]);
            var centesimas = decimal.Parse(partes[2]);
            return minutos * 60 + segundos + centesimas / 100;
        }

        public override string ToString()
        {
            return $"Tramo {NroTramo} ({Distancia}m): {Tiempo}";
        }
    }
}
