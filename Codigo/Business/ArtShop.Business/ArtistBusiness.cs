using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class ArtistBusiness
    {
        private BaseDataService<Artist> db = new BaseDataService<Artist>();
        public List<Artist> ListarTodosLosArtistas()
        {
            return db.Get();            
        }

        public Artist EditarArtista(Artist artist)
        {
            return db.Update(artist, artist.Id);
        }

        public Artist nuevo(Artist artist)
        {
            artist=db.Create(artist);
            
            return (artist);
        }
        public Artist GetbyID(int id)
        {
            return db.GetById(id);
        }
        public void Borrar(Artist artist)
        {
            //for (int i = 0; i < artist.Product.Count; i++)
            //{
            //    _database.Remove(artist.Product.ElementAt(i));
            //}
            
            db.Delete(artist.Id);
        }

    }
}
