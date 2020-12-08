using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
     public class ShippingBusiness
     {
        private BaseDataService<Shipping> db = new BaseDataService<Shipping>();
        
        public List<Shipping> ListarTodas()
        {
            var ship = db.Get();
            
            return ship;

        }
        public Shipping GetbyCookie(string cookie)
        {
            var ship = db.Get(x => x.Email == cookie).FirstOrDefault();
            
            return ship;
        }

        public Shipping Editar(Shipping shipping)
        {
            return db.Update(shipping, shipping.Id);
        }


        public Shipping nuevo(Shipping shipping)
        {
            var ship = db.Create(shipping);

            return (ship);
        }
        public Shipping GetbyID(int id)
        {
            return db.GetById(id);
        }


        public void Borrar(Shipping shipping)
        {
            db.Delete(shipping.Id);
        }
     }
}
