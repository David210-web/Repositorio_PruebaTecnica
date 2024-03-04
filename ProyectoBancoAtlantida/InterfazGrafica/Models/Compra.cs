using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Models
{
    public class Compra
    {
        public int IdTarjeta { get; set; }
        public int IdCompra { get; set; }
        public string AutorCompra {  get; set; }
        public DateTime FechaCompra { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}