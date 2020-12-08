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

    [RoutePrefix("api/Shipping")]
    public class ShippingService : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public Shipping Add(Shipping ship)
        {
            try
            {
                var shipbs = new ShippingBusiness();
                return shipbs.nuevo(ship);
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
        public Shipping Edit(Shipping ship)
        {
            try
            {
                var shipbs = new ShippingBusiness();
                return shipbs.Editar(ship);
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
        public List<Shipping> List()
        {
            try
            {
                var shipbs = new ShippingBusiness();
                return shipbs.ListarTodas();
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

                var shipbs = new ShippingBusiness();
                var ship = shipbs.GetbyID(id);
                shipbs.Borrar(ship);
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
        public Shipping GetById(int id)
        {
            try
            {
                var shipbs = new ShippingBusiness();
                return shipbs.GetbyID(id);
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
        public Shipping GetbyCookie(string cookie)
        {
            try
            {
                var shipbs = new ShippingBusiness();
                return shipbs.GetbyCookie(cookie);
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