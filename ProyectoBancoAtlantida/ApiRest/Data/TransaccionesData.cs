using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;

namespace ApiRest.Data
{
    public class TransaccionesData
    {
        //Mostrando todas las compras y los pagos de los clientes en base al id de la tarjeta del cliente
        public static List<Transacciones> GetTransacciones(int id)
        {
            List<Transacciones> transacciones = new List<Transacciones>();
           using(SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
           {
                string nombreParametro = "Sp_ObtenerHistorialTransacciones";
                SqlCommand cmd = new SqlCommand(nombreParametro, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDTarjetaCredito",id);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transacciones transacciones1 = new Transacciones()
                            {
                                //TransaccionId = Int32.Parse(reader["TransaccionId"].ToString()),
                                IdTarjetaCredito = Int32.Parse(reader["IDTarjetaCredito"].ToString()),
                                FechaCompra = DateTime.Parse(reader["FechaCompra"].ToString()),
                                Descripcion = reader["Descripcion"].ToString(),
                                Monto = double.Parse(reader["Monto"].ToString()),
                                Tipo = reader["Tipo"].ToString()
                            };
                            transacciones.Add(transacciones1);
                        }
                    }
                }
                catch (SqlException sqlex)
                {
                    throw new HttpResponseException(
                        HttpStatusCode.InternalServerError);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return transacciones;
           }
        }
    }
}