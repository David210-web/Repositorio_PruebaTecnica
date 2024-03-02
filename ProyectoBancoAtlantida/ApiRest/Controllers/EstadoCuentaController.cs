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
    public class EstadoCuentaController : ApiController
    {
        // GET: api/EstadoCuenta
        public string Get()
        {
            return "Tienes que elegir un Id";
        }

        // GET: api/EstadoCuenta/5
        public EstadoCuenta Get(int id)
        {
            return EstadoCuentaData.GetEstadoCuenta(id);
        }

       
    }
}
