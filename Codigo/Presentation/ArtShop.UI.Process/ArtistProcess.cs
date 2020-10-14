using ArtShop.Business;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.UI.Process
{
    public class ArtistProcess
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
            return biz.EditarArtista(artist);
        }

        public Artist AgregarArtista(Artist artist)
        {
            return biz.nuevo(artist);
        }

        public Artist GetById(int id)
        {
            return biz.GetbyID(id);
        }
        
        public void EliminarArtista(Artist artist)
        {
            biz.Borrar(artist);
        }
    }
}
