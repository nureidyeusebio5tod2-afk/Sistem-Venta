using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Capa.Datos
{
    public class ClienteDAL
    {
        private string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        // Listar clientes
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cliente
                    {
                        Id_cliente = Convert.ToInt32(dr["Id_cliente"]),
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Estado_cliente = dr["Estado_cliente"].ToString()
                    });
                }
            }

            return lista;
        }

        // Insertar cliente
        public void Insertar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", c.Correo ?? (object)DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar cliente
        public void Actualizar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", c.Id_cliente);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", c.Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado_cliente", c.Estado_cliente);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar cliente (cambia estado a Inactivo)
        public void Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

