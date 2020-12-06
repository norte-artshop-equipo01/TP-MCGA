using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtShop.Entities;
using ArtShop.Entities.Model;

namespace ArtShop.UI.Ecommerce.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private ProductProcess productprocess = new ProductProcess();
        private CartProcess cartprocess = new CartProcess();
        private CartItemProcess itemsprocess = new CartItemProcess();
        
        public ActionResult Index()
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            if (carrito ==null || carrito.Id ==0)
            {
                var cartnew = new Cart();
                cartnew.Cookie = User.Identity.Name;
                cartnew.ItemCount = 0;
                cartnew.Disabled = false;
                cartnew.CartDate = DateTime.Now;
                CheckAuditPattern(cartnew, true);
                cartprocess.AgregarCart(cartnew);
                ViewBag.Total = 0;
                return View(itemsprocess.GetByCartId(cartnew.Id));
            }
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
            ViewBag.Total = sum_items(listadoitems);

            return View(listadoitems);
        }
        public ActionResult AddCartItem()
        {
            return RedirectToAction("Index", "Cart", cartprocess.GetByCookie(User.Identity.Name));
        }

        [HttpPost]
        public ActionResult AddCartItem(FormCollection item1, Product product)
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var producto = productprocess.GetById(product.Id);
            

            var item = new CartItem();
            item.ProductId = producto.Id;
            item.Price = producto.Price;
            item.Quantity = Convert.ToInt32(item1["quantity"]);
            CheckAuditPattern(item, true);

            if (carrito == null)
            {
                NuevoCarrito(item);
            }
            else
            {
                item.CartId = carrito.Id;
                CarritoExistente(carrito, item);
            }
            return RedirectToAction("Index", "Cart", cartprocess.GetByCookie(User.Identity.Name));
        }

        private void CarritoExistente(Cart cart, CartItem cartItem)
        {
            bool flag = true;
            for (int i = 0; i < cart.CartItem.Count; i++)
            {
                if (cart.CartItem.ElementAt(i).ProductId == cartItem.ProductId)
                {
                    flag = false;
                    cart.CartItem.ElementAt(i).Quantity+=cartItem.Quantity;
                    itemsprocess.EditarCartItem(cart.CartItem.ElementAt(i));
                }
            }
            if (flag)
            {
                cart.CartItem.Add(cartItem);
                itemsprocess.AgregarCartItem(cartItem);
                //cartItem.Cart = cart;
                
            }
        }

        public void NuevoCarrito(CartItem cartitem)
        {
            var cartnew = new Cart();
            cartnew.Cookie = User.Identity.Name;
            cartnew.ItemCount = 0;
            cartnew.Disabled = false;
            cartnew.CartDate = DateTime.Now;
            CheckAuditPattern(cartnew, true);
            //cartnew.CartItem.Add(cartitem);
            cartprocess.AgregarCart(cartnew);
            itemsprocess.AgregarCartItem(cartitem);
        }

        public double sum_items(List<CartItem> cartitems)
        {
            
            double suma = 0;
            for (int i = 0; i < cartitems.Count(); i++)
            {
                suma += cartitems.ElementAt(i).Price*cartitems.ElementAt(i).Quantity;
            }
            return suma;
        }

        public ActionResult Checkout()
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
            ViewBag.Total = sum_items(listadoitems);
            return View(listadoitems);
            
        }

    }
}