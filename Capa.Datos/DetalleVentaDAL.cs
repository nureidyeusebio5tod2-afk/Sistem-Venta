using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Capa.Entidades;

namespace Capa.Datos
{
    public class DetalleVentaDAL
    {
        private string cadena = ConfigurationManager
                                .ConnectionStrings["cn"]
                                .ConnectionString;

        public void InsertarDetalle(int idVenta, int idProducto, int cantidad, decimal precio)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarDetalleVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_venta", idVenta);
                cmd.Parameters.AddWithValue("@Id_producto", idProducto);
                cmd.Parameters.AddWithValue("@Cant", cantidad);
                cmd.Parameters.AddWithValue("@Precio", precio);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public List<DetalleVenta> Listar(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ListarDetalleVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        Id_detalle = (int)dr["Id_detalle"],
                        Id_producto = (int)dr["Id_producto"],
                        Cant = (int)dr["Cant"],
                        Precio = (decimal)dr["Precio"]
                    });
                }
            }

            return lista;
        }

        public void Actualizar(DetalleVenta d)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarDetalleVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_detalle", d.Id_detalle);
                cmd.Parameters.AddWithValue("@Cant", d.Cant);
                cmd.Parameters.AddWithValue("@Precio", d.Precio);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

