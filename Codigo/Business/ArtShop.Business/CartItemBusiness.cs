using ArtShop.Data;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtShop.Business
{
    public class CartItemBusiness
    {
        private BaseDataService<CartItem> db = new BaseDataService<CartItem>();
        public List<CartItem> ListarTodosCartItems()
        {
            return db.Get();
        }

        public CartItem Editar(CartItem cartitem)
        {
            return db.Update(cartitem, cartitem.Id);
        }

        public CartItem nuevo(CartItem cartitem)
        {
            cartitem = db.Create(cartitem);

            return (cartitem);
        }
        public CartItem GetbyID(int id)
        {
            return db.GetById(id);
        }
        public void Borrar(CartItem cartitem)
        {
            db.Delete(cartitem.Id);
        }
    }
}
