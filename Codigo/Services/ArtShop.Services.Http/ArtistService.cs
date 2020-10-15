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
    [RoutePrefix("api/Artist")]
    public class ArtistService: ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public Artist Add(Artist artist)
        {
            try
            {
                var artbs = new ArtistBusiness();
                return artbs.nuevo(artist);
            }
            catch(Exception ex)
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
        [RouteAttribute ("Editar")]
        public Artist Edit(Artist artist)
        {
            try
            {
                var artbs = new ArtistBusiness();
                return artbs.EditarArtista(artist);
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
        public List<Artist> List()
        {
            try
            {
                var artbs = new ArtistBusiness();
                return artbs.ListarTodosLosArtistas();
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
        public void Remove(Artist artist)
        {
            try
            {
                var artbs = new ArtistBusiness();
                artbs.Borrar(artist);
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
