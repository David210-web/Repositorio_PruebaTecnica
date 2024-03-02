using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class EstadoCuenta
    {
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public double SaldoActual { get; set; }
        public double LimiteCredito { get; set; }
        public double SaldoDisponible { get; set; }
        public double InteresBonificable { get; set; }
        public double CuotaMinima { get; set; }
        public double MontoTotalApagar { get; set;}
        public double MontoTotalContadoIntereses { get; set; }

    }
}