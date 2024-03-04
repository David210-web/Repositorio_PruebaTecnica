using ApiRest.Data;
using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    public class PagoController : ApiController
    {
        // GET: api/Pago
        public string Get()
        {
            return "No hay metodo get";
        }

        // GET: api/Pago/5
        public string Get(int id)
        {
            return "No hay metodo get";
        }

        // POST: api/Pago
        public string Post([FromBody]Pago p)
        {
            return PagoData.CreatePago(p);
        }

       
    }
}
