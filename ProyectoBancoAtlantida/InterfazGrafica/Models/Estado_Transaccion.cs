using ApiRest.Models;
using InterfazGrafica.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfazGrafica.Models
{
    public class Estado_Transaccion
    {
        public List<Transacciones> TransaccionesUsuario { get; set; }
        public EstadoCuenta EstadoCuenta { get; set; }
    }
}