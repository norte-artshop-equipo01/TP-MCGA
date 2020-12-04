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
    [RoutePrefix("api/CartItem")]
    public class CartItemService : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public CartItem Add(CartItem cartitem)
        {
            try
            {
                var cartitembs = new CartItemBusiness();
                return cartitembs.nuevo(cartitem);
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
        public CartItem Edit(CartItem cartitem)
        {
            try
            {
                var cartitembs = new CartItemBusiness();
                return cartitembs.Editar(cartitem);
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
        public List<CartItem> List()
        {
            try
            {
                var cartitembs = new CartItemBusiness();
                return cartitembs.ListarTodosCartItems();
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

                var cartitembs = new CartItemBusiness();
                var cartitem = cartitembs.GetbyID(id);
                cartitembs.Borrar(cartitem);
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
        public CartItem GetById(int id)
        {
            try
            {
                var cartitembs = new CartItemBusiness();
                return cartitembs.GetbyID(id);
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
