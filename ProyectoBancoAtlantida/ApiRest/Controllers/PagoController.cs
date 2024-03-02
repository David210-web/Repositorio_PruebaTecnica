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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pago/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pago
        public string Post([FromBody]Pago p)
        {
            return PagoData.CreatePago(p);
        }

        // PUT: api/Pago/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pago/5
        public void Delete(int id)
        {
        }
    }
}
