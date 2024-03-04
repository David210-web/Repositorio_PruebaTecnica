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
        public IEnumerable<Compra> Get()
        {
            return CompraData.GetCompras();
        }

        // GET: api/Compra/5
        public IEnumerable<Compra> Get(int id)
        {
            return CompraData.GetComprasPorUsuario(id);
        }

        // POST: api/Compra
        public string Post([FromBody]Compra c)
        {
            return CompraData.CreateCompra(c);
        }

        
    }
}
