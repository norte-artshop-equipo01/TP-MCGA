using ArtShop.Business;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtShop.Services.Http
{
    [RoutePrefix("api/Order")]
    public class OrderService : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public Order Add(Order order)
        {
            try
            {
                var orderbs = new OrderBusiness();
                return orderbs.nuevo(order);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(httpError);
            }
        }
        [HttpPut]
        [RouteAttribute("Editar")]
        public Order Edit(Order order)
        {
            try
            {
                var orderbs = new OrderBusiness();
                return orderbs.EditarOrder(order);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }

        }
        [HttpGet]
        [Route("Listar")]
        public List<Order> List()
        {
            try
            {
                var orderbs = new OrderBusiness();
                return orderbs.ListarTodas();
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }
        [HttpDelete]
        [Route("Eliminar")]
        public void Remove(int id)
        {
            try
            {

                var orderbs = new OrderBusiness();
               orderbs.Borrar(orderbs.GetbyID(id));
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }
        [HttpGet]
        [Route("GetById")]
        public Order GetById(int id)
        {
            try
            {
                var orderbs = new OrderBusiness();
                return orderbs.GetbyID(id);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("Agregar_Linea_Orden")]
        public OrderDetail AddOrderdetail(OrderDetail orderdetail)
        {
            try
            {
                var orderdetailbs = new OrderDetailBusiness();
                return orderdetailbs.nuevo(orderdetail);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(httpError);
            }
        }
        [HttpPut]
        [RouteAttribute("Editar_Linea_Orden")]
        public OrderDetail Edit(OrderDetail orderdetail)
        {
            try
            {
                var orderdetailbs = new OrderDetailBusiness();
                return orderdetailbs.EditarOrderDetail(orderdetail);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }

        }
        [HttpDelete]
        [Route("Eliminar_Linea_Orden")]
        public void RemoveOrderDetail(int id)
        {
            try
            {

                var orderdetailrbs = new OrderDetailBusiness();
                orderdetailrbs.Borrar(orderdetailrbs.GetbyID(id));
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }
        [HttpGet]
        [Route("GetById_Linea_Orden")]
        public OrderDetail GetByIdOrderDetail(int id)
        {
            try
            {
                var orderdetailbs = new OrderDetailBusiness();
                return orderdetailbs.GetbyID(id);
            }
            catch (Exception ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }

    }
}
