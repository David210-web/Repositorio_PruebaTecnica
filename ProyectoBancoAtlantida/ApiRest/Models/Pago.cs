using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int IdTarjetaCredito { get; set; }
        public DateTime FechaPago { get; set; }
        public double Monto { get; set; }
    }
}