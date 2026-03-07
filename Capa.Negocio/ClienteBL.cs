using Capa.Datos;
using Capa.Entidades;
using System.Collections.Generic;

namespace Capa.Negocio
{
    public class ClienteBL
    {
        private ClienteDAL dal = new ClienteDAL();

        // Listar clientes
        public List<Cliente> Listar()
        {
            return dal.Listar();
        }

        // Guardar cliente (insertar o actualizar)
        public void Guardar(Cliente c)
        {
            if (c.Id_cliente == 0)
            {
                dal.Insertar(c); // SP_InsertarCliente
            }
            else
            {
                dal.Actualizar(c); // SP_ActualizarCliente
            }
        }

        // Eliminar cliente (pone Estado_cliente = 'Inactivo')
        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
    }
}

