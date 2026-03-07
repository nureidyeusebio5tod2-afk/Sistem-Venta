using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Capa.Datos
{
    public class VentaDAL
    {
        private string cadena = ConfigurationManager
                                .ConnectionStrings["cn"]
                                .ConnectionString;

        
        public int InsertarVenta(int idCliente, decimal total)
        {
            int idVenta;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", idCliente);
                cmd.Parameters.AddWithValue("@Total_general", total);

                cn.Open();
                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return idVenta;
        }

        // LISTAR VENTAS
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ListarVentas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Venta
                    {
                        Id_Venta = Convert.ToInt32(dr["Id_Venta"]),
                        Fecha_venta = Convert.ToDateTime(dr["Fecha_venta"]),
                        NombreCliente = dr["Nombre"].ToString(),
                        Total_general = Convert.ToDecimal(dr["Total_general"]),
                        Estado_venta = dr["Estado_venta"].ToString()
                    });
                }
            }

            return lista;
        }

        // ACTUALIZAR VENTA
        public void Actualizar(Venta v)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Venta", v.Id_Venta);
                cmd.Parameters.AddWithValue("@Total_general", v.Total_general);
                cmd.Parameters.AddWithValue("@Estado_venta", v.Estado_venta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}


