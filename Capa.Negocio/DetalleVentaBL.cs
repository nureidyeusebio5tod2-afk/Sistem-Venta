using Capa.Datos;
using Capa.Entidades;
using System.Collections.Generic;

namespace Capa.Negocio
{
    public class DetalleVentaBL
    {
        private DetalleVentaDAL dal = new DetalleVentaDAL();

        public void GuardarDetalle(int idVenta, int idProducto, int cantidad, decimal precio)
        {
            dal.InsertarDetalle(idVenta, idProducto, cantidad, precio);

        }

        public List<DetalleVenta> Listar(int idVenta)
        {
            return dal.Listar(idVenta);
        }

        public void Actualizar(DetalleVenta d)
        {
            if (d.Id_detalle <= 0)
                throw new System.Exception("Detalle inválido");

            dal.Actualizar(d);
        }
    }
}

