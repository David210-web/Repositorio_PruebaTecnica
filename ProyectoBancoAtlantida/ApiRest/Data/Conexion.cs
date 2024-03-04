using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Data
{
    public class Conexion
    {
        //Url de la conexion: Verificar si la instancia de Sql server es SQLEXPRESS sino cambiarla
        public static string UrlConexion = "Data Source = .\\SQLEXPRESS; Initial Catalog = ProyectoBancoAtlantida; Integrated security = true";

    }
}