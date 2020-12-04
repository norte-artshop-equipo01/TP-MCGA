using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class OrderProcess: ProcessComponent
    {
        public List<Order> ListarOrder()
        {
            var response = HttpGet<List<Order>>("/api/Order/Listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public Order EditarOrder(Order order)
        {
            var response = HttpPut<Order>("/api/Order/Editar", order, MediaType.Json);
            return response;
        }

        public Order AgregarOrder(Order order)
        {
            var response = HttpPost<Order>("/api/Order/Agregar", order, MediaType.Json);
            return response;
        }

        public Order GetById(int id)
        {
            var response = HttpGet<Order>("/api/Order/GetById", id, MediaType.Json);
            return response;

        }

        public void EliminarOrder(Order order)
        {
            var response = HttpDelete<Order>("/api/Order/Eliminar", order.Id, MediaType.Json);

        }


        public List<OrderDetail> ListarOrderDetail()
        {
            var response = HttpGet<List<OrderDetail>>("/api/Order/Listar_Todas_Lineas", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public OrderDetail EditarLinea(OrderDetail orderdetail)
        {
            var response = HttpPut<OrderDetail>("/api/Order/Editar_Linea_Orden", orderdetail, MediaType.Json);
            return response;
        }

        public OrderDetail AgregarLinea(OrderDetail orderdetail)
        {
            var response = HttpPost<OrderDetail>("/api/Order/Agregar_Linea_Orden", orderdetail, MediaType.Json);
            return response;
        }

        public OrderDetail GetLinea_By_Id(int id)
        {
            var response = HttpGet<OrderDetail>("/api/Order/Eliminar_Linea_Orden", id, MediaType.Json);
            return response;

        }

        public void EliminarOrder(OrderDetail orderdetail)
        {
            var response = HttpDelete<OrderDetail>("/api/Order/Eliminar_Linea_Orden", orderdetail.Id, MediaType.Json);

        }

        public List<OrderDetail> GetLinea_by_OrderID(int id)
        {
            var response = HttpGet<List<OrderDetail>>("/api/Order/GetLineasOrden_by_OrdenID", id, MediaType.Json);
            return response;

        }

    }
}
