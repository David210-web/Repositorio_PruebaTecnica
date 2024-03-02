using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfazGrafica.Models
{
    public class Estado_Compra
    {
        public List<Compra> comprasUsuario { get; set; }
        public EstadoCuenta EstadoCuenta { get; set; }
    }
}