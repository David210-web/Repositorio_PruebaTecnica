using ApiRest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiRest.Controllers
{
    public class TransaccionesController : ApiController
    {
        // GET: api/Transacciones
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Transacciones/5
        public IEnumerable<Transacciones> Get(int id)
        {
            return TransaccionesData.GetTransacciones(id);
        }

        
    }
}
