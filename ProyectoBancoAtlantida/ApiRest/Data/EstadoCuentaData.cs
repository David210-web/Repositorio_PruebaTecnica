using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiRest.Data
{
    public class EstadoCuentaData
    {

        public static EstadoCuenta GetEstadoCuenta(int id)
        {
            EstadoCuenta estadosCuenta = new EstadoCuenta();
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEstadoCuenta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDTarjetaCredito", id);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            estadosCuenta.NombreTitular = reader.GetString(0);
                            estadosCuenta.NumeroTarjeta = reader.GetString(1);
                            estadosCuenta.SaldoActual = Convert.ToDouble(reader.GetDecimal(2));
                            estadosCuenta.LimiteCredito = Convert.ToDouble(reader.GetDecimal(3));
                            estadosCuenta.SaldoDisponible = Convert.ToDouble(reader.GetDecimal(4));
                            estadosCuenta.InteresBonificable = Convert.ToDouble(reader.GetDecimal(5));
                            estadosCuenta.CuotaMinima = Convert.ToDouble(reader.GetDecimal(6));
                            estadosCuenta.MontoTotalApagar = Convert.ToDouble(reader.GetDecimal(2));
                            estadosCuenta.MontoTotalContadoIntereses = Convert.ToDouble(reader.GetDecimal(2));



                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return estadosCuenta;
            }
        }
    }
}