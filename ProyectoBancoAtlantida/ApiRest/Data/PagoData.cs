using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRest.Data
{
    public class PagoData
    {
        public static string CreatePago(Pago p)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Sp_insertarPago", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdTarjetaCredito", p.IdTarjetaCredito);
                        cmd.Parameters.AddWithValue("@FechaPago", p.FechaPago);
                        cmd.Parameters.AddWithValue("@Monto", p.Monto);
                        cmd.ExecuteNonQuery();
                    }
                    return "Pago almacenado";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}