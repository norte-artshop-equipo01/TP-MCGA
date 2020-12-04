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
        private BaseDataService<Product> db2 = new BaseDataService<Product>();
        public List<CartItem> ListarTodosCartItems()
        {
            var cartitemlistt = db.Get();
            foreach (CartItem cartitem in cartitemlistt)
            {
                cartitem.Product = db2.GetById(cartitem.ProductId);
            }
            return cartitemlistt;
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
            var cartitem= db.GetById(id);
            cartitem.Product = db2.GetById(cartitem.ProductId);
            return cartitem;
        }

        public List<CartItem> GetByCartId(int id)
        {
            var listitem= db.Get().Where(x => x.CartId == id).ToList();
            foreach (CartItem item in listitem)
            {
                item.Product = db2.GetById(item.ProductId);
            }

            return listitem;
        }


        public void Borrar(CartItem cartitem)
        {
            db.Delete(cartitem.Id);
        }
    }
}
