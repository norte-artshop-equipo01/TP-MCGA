using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class CartBusiness
    {
        private BaseDataService<CartItem> db2 = new BaseDataService<CartItem>();
        private BaseDataService<Cart> db = new BaseDataService<Cart>();
        public List<Cart> ListarTodas()
        {
            var cart = db.Get();
            foreach (Cart car in cart)
            {
                car.CartItem=(db2.Get(x=>x.CartId==car.Id).ToList());
                car.ItemCount = car.CartItem.Count();
            }
            return cart;
            
        }
        public Cart GetbyCookie (string cookie)
        {
            var cart= db.Get(x => x.Cookie == cookie).FirstOrDefault();
            cart.CartItem = db2.Get(x => x.CartId == cart.Id).ToList();
            cart.ItemCount = cart.CartItem.Count();
            return cart;
        }

        public Cart Editar(Cart cart)
        {
            return db.Update(cart, cart.Id);
        }


        public Cart nuevo(Cart cart)
        {
            cart = db.Create(cart);

            return (cart);
        }
        public Cart GetbyID(int id)
        {
            return db.GetById(id);
        }

        
        public void Borrar(Cart cart)

        {            
            for (int i = 0; i < cart.CartItem.Count; i++)
            {
                db2.Delete(cart.CartItem.ElementAt(i));
            }

            db.Delete(cart.Id);
        }
    }
}
