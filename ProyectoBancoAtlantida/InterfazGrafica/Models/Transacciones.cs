using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace InterfazGrafica.Data
{
    public class Transacciones
    {
        public int TransaccionId { get; set; }
        public int IdTarjetaCredito { get; set; } 
        public DateTime FechaCompra {  get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }

        public string Tipo {  get; set; }
    }
}