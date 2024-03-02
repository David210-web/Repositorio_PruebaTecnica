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
    public class TarjetaController : ApiController
    {
        // GET: api/Tarjeta
        public IEnumerable<Tarjeta> Get()
        {
            return TarjetasData.GetTarjetas();
        }

        // GET: api/Tarjeta/5
        public Tarjeta Get(int id)
        {
            return TarjetasData.GetTarjetaPorId(id);
        }

        
    }
}
