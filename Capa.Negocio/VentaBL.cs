using Capa.Datos;
using Capa.Entidades;
using System.Collections.Generic;

namespace Capa.Negocio
{
    public class VentaBL
    {
        private VentaDAL dal = new VentaDAL();

        // INSERTAR VENTA
        public int GuardarVenta(int idCliente, decimal total)
        {
            return dal.InsertarVenta(idCliente, total);

        }


        // LISTAR VENTAS
        public List<Venta> Listar()
        {
            return dal.Listar();
        }

        // ACTUALIZAR VENTA
        public void Actualizar(Venta v)
        {
            if (v.Id_Venta <= 0)
                throw new System.Exception("Venta inválida");

            dal.Actualizar(v);
        }
    }
}

