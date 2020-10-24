using ArtShop.Business;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class ArtistProcess : ProcessComponent
    {
        private ArtistBusiness biz = new ArtistBusiness ();
        public List<Artist> ListarTodosLosArtistas()
        {
            //return biz.ListarTodosLosArtistas();
            var response = HttpGet<List<Artist>>("/api/Artist/listar", new Dictionary<string, object>(), MediaType.Json);
            return response;
        }

        public Artist EditarArtista(Artist artist)
        {
            //return biz.EditarArtista(artist);
            var response = HttpPut<Artist>("/api/Artist/Editar", artist, MediaType.Json);
            return response;
        }

        public Artist AgregarArtista(Artist artist)
        {
            //return biz.nuevo(artist);
            var response = HttpPost<Artist>("/api/Artist/Agregar", artist, MediaType.Json);
            return response;
        }

        public Artist GetById(int id)
        {
            var response = HttpGet<Artist>("/api/Artist/GetById", id, MediaType.Json);
            return response;
            // return biz.GetbyID(id);
        }

        public void EliminarArtista(Artist artist)
        {
            var response = HttpDelete<Artist>("/api/Artist/Eliminar", artist.Id, MediaType.Json);
            //return response;
           // biz.Borrar(artist);
        }

    }
}
