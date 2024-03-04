using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ApiRest.Data
{
    public class TarjetasData
    {
        //Pidiendo los datos de la tabla tarjetas a sql server por medio de un proceso
        public static List<Tarjeta> GetTarjetas()
        {
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodasTarjetasCredito", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarjeta tarjeta = new Tarjeta();
                            tarjeta.Id = reader.GetInt32(0);
                            tarjeta.NombreTitular = reader.GetString(1);
                            tarjeta.NumeroTarjeta = reader.GetString(2);
                            tarjeta.SaldoActual = Convert.ToDouble(reader.GetDecimal(3));
                            tarjeta.LimiteCredito = Convert.ToDouble(reader.GetDecimal(4));
                            tarjeta.SaldoDisponible = Convert.ToDouble(reader.GetDecimal(5));
                            tarjetas.Add(tarjeta);
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
                return tarjetas;
            }

        }


        public static Tarjeta GetTarjetaPorId(int id)
        {
            Tarjeta tarjeta = new Tarjeta();
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodasTarjetasCreditoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tarjeta.Id = reader.GetInt32(0);
                            tarjeta.NombreTitular = reader.GetString(1);
                            tarjeta.NumeroTarjeta = reader.GetString(2);
                            tarjeta.SaldoActual = Convert.ToDouble(reader.GetDecimal(3));
                            tarjeta.LimiteCredito = Convert.ToDouble(reader.GetDecimal(4));
                            tarjeta.SaldoDisponible = Convert.ToDouble(reader.GetDecimal(5));

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
                return tarjeta;
            }
        }
    }
}