using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Http;
using System.Net;


namespace ApiRest.Data
{
    public class CompraData
    {
        //Recuperando las compras hechas en base al id de la tarjeta de credito de los clientes
        public static List<Compra> GetComprasPorUsuario(int id)
        {
            List<Compra> compras = new List<Compra>();
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                string nombreProcedimiento = "sp_GetComprasMes";
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDTarjetaCredito", id);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Compra c = new Compra()
                            {
                                FechaCompra = Convert.ToDateTime(reader["FechaCompra"].ToString()),
                                Descripcion = reader["Descripcion"].ToString(),
                                Monto = decimal.Parse(reader["Monto"].ToString())
                            };
                            compras.Add(c);

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
                    throw new HttpResponseException(
                        HttpStatusCode.BadRequest);
                }
                return compras;
            }
        }

        //Creando una nueva compra y registrandola en la base de datos
        public static string CreateCompra(Compra c)
        {
            using(SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarCompra", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdTarjetaCredito", c.IdTarjeta);
                        cmd.Parameters.AddWithValue("@FechaCompra", c.FechaCompra);
                        cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion);
                        cmd.Parameters.AddWithValue("@Monto", c.Monto);
                        cmd.ExecuteNonQuery();
                    }
                    return "Compra almacenada";
                }
                catch (SqlException sqlex)
                {
                    throw new HttpResponseException(
                        HttpStatusCode.InternalServerError);

                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(
                        HttpStatusCode.BadRequest);
                }
            }
        }
        //Obtener cuanto gasto el cliente en las compras en lo que va del mes
        public static int ObtenerTotalComprasMes(int id)
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CalcularCompraMesActual", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdTarjetaCredito", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                total = Convert.ToInt32(reader["Compra Total"]);
                            }
                        }
                    }
                    return total;
                }
                catch (SqlException sqlex)
                {
                    throw new HttpResponseException(
                        HttpStatusCode.InternalServerError);

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        //Obtener cuanto gasto el cliente en compras el mes anterior
        public static int ObtenerTotalComprasMesAnterior(int id)
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CalcularCompraMesAnterior", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdTarjetaCredito", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                total = Convert.ToInt32(reader["Compra Total"]);
                            }
                        }
                    }
                    return total;
                }
                catch (SqlException sqlex)
                {
                    throw new HttpResponseException(
                        HttpStatusCode.InternalServerError);
                }

                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        //Obtener todas las compras de todos los clientes (para exportar en un archivo excel)
        public static List<Compra> GetCompras()
        {
            List<Compra> comprasTotales = new List<Compra>();
            using(SqlConnection conn = new SqlConnection(Conexion.UrlConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerComprasTotales", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Compra c = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["IDCompra"]),
                                AutorCompra = reader["Autor compra"].ToString(),
                                FechaCompra = Convert.ToDateTime(reader["FechaCompra"].ToString()),
                                Descripcion = reader["Descripcion"].ToString(),
                                Monto = decimal.Parse(reader["Monto"].ToString())
                            };
                            comprasTotales.Add(c);

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
                    throw new HttpResponseException(
                        HttpStatusCode.BadRequest);
                }
                return comprasTotales;
            }
        }
    }
}