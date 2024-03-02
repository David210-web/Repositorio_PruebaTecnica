using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public double SaldoActual { get; set; }
        public double LimiteCredito { get; set; }
        public double SaldoDisponible { get; set; }
    }
}