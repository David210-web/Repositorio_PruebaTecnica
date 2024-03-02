using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Compra
    {
        public int IdTarjeta { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
    }
}