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
  
    [RoutePrefix("api/Cart")]
    public class CartService : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public Cart Add(Cart cart)
        {
            try
            {
                var cartbs = new CartBusiness();
                return cartbs.nuevo(cart);
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
        public Cart Edit(Cart cart)
        {
            try
            {
                var cartbs = new CartBusiness();
                return cartbs.Editar(cart);
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
        public List<Cart> List()
        {
            try
            {
                var cartbs = new CartBusiness();
                return cartbs.ListarTodas();
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

                var cartbs = new CartBusiness();
                var cart = cartbs.GetbyID(id);
                cartbs.Borrar(cart);
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
        public Cart GetById(int id)
        {
            try
            {
                var cartbs = new CartBusiness();
                return cartbs.GetbyID(id);
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
        [Route("GetByCookie")]
        public Cart GetbyCookie(string cookie)
        {
            try
            {
                var cartbs = new CartBusiness();
                return cartbs.GetbyCookie(cookie);
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
