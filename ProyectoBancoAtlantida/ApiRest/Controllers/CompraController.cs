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
    public class CompraController : ApiController
    {
        // GET: api/Compra
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Compra/5
        public IEnumerable<Compra> Get(int id)
        {
            return CompraData.GetCompras(id);
        }

        // POST: api/Compra
        public string Post([FromBody]Compra c)
        {
            return CompraData.CreateCompra(c);
        }

        // PUT: api/Compra/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Compra/5
        public void Delete(int id)
        {
        }
    }
}
