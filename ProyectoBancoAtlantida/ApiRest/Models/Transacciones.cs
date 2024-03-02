using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Data
{
    public class Transacciones
    {
        public int TransaccionId { get; set; }
        public int IdTarjetaCredito { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public string Tipo { get; set; }
    }
}