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
    public class CartController : BaseController
    {
        private CartProcess cartprocess = new CartProcess();
        private CartItemProcess itemsprocess = new CartItemProcess();
        // GET: Cart
        public ActionResult Index()
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            if (carrito == null)
            {
                var cartnew = new Cart();
                cartnew.Cookie = User.Identity.Name;
                cartnew.ItemCount = 0;
                cartnew.CartDate = DateTime.Now;
                CheckAuditPattern(cartnew, true);
                cartprocess.AgregarCart(cartnew);
                ViewBag.Total = 0;
                return View();
            }

            ViewBag.Total = sum_items(cartprocess.GetByCookie(User.Identity.Name));

            return View(cartprocess.GetByCookie(User.Identity.Name));
        }





        public double sum_items(Cart cart)
        {
            
            double suma = 0;
            for (int i = 0; i < cart.CartItem.Count(); i++)
            {
                suma += cart.CartItem.ElementAt(i).Price*cart.CartItem.ElementAt(i).Quantity;
            }
            return suma;
        }



    }
}