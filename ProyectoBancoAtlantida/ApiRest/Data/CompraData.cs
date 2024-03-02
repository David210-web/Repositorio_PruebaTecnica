using ApiRest.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;

namespace ApiRest.Data
{
    public class CompraData
    {
        public static List<Compra> GetCompras(int id)
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
                                Monto = double.Parse(reader["Monto"].ToString())
                            };
                            compras.Add(c);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return compras;
            }
        }

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
                }catch(Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}