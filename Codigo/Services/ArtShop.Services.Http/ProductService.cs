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

    [RoutePrefix("api/Product")]
    public class ProductService : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public Product Add(Product product)
        {
            try
            {
                var pdcbs = new ProductBusiness();
                return pdcbs.Nuevo(product);
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
        public Product Edit(Product product)
        {
            try
            {
                var pdcbs = new ProductBusiness();
                return pdcbs.EditarProduct(product);
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
        public List<Product> List()
        {
            try
            {
                var pdcbs = new ProductBusiness();
                return pdcbs.ListarTodosProductos();
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

                var pdcbs = new ProductBusiness();
                var product = pdcbs.GetbyID(id);
                pdcbs.Borrar(product);
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
        public Product GetById(int id)
        {
            try
            {
                var pdcbs = new ProductBusiness();
                return pdcbs.GetbyID(id);
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

    


