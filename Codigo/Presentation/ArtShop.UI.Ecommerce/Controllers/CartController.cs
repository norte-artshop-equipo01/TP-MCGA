using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtShop.Entities;
using ArtShop.Entities.Model;
using MercadoPago;
using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;


namespace ArtShop.UI.Ecommerce.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private ProductProcess productprocess = new ProductProcess();
        private CartProcess cartprocess = new CartProcess();
        private CartItemProcess itemsprocess = new CartItemProcess();
        private ShippingProcess shippingprocess = new ShippingProcess();

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
            ViewBag.Ship = shippingprocess.GetByCookie(User.Identity.Name);
            if (MercadoPago.SDK.AccessToken == null)
            {
                MercadoPago.SDK.AccessToken = "TEST-6196665787772204-120903-7e3696caa9ad7207eae686fc4d423f53-684823230";
            }
            Preference preference = new Preference();
            
            foreach(CartItem item in listadoitems)
            {
                preference.Items.Add(
                    new MercadoPago.DataStructures.Preference.Item()
                    {
                        Title = item.Product.Title,
                        Quantity = item.Quantity,
                        CurrencyId = MercadoPago.Common.CurrencyId.ARS,
                        UnitPrice=Convert.ToDecimal(item.Price)

                    }) ;
            }
            preference.Save();
            ViewBag.PrefId = preference;
            return View(listadoitems);
            
        }
        public ActionResult pago_partial()
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
            ViewBag.Total = sum_items(listadoitems);
            ViewBag.Ship = shippingprocess.GetByCookie(User.Identity.Name);
            if (MercadoPago.SDK.AccessToken == null)
            {
                MercadoPago.SDK.SetAccessToken("TEST-6196665787772204-120903-7e3696caa9ad7207eae686fc4d423f53-684823230");
            }
            
            Preference preference = new Preference();
            
            preference.BackUrls = new BackUrls()
            {
                Success = "~/cart/cerrar_orden",
            };

            foreach (CartItem item in listadoitems)
            {
                preference.Items.Add(
                    new MercadoPago.DataStructures.Preference.Item()
                    {
                        Title = item.Product.Title,
                        Quantity = item.Quantity,
                        CurrencyId = MercadoPago.Common.CurrencyId.ARS,
                        UnitPrice = Convert.ToDecimal(item.Price)

                    });
            }
            preference.Save();
            return View(preference);
        }
       
        public ActionResult cerrar_orden(FormCollection json)
        {
            
            return View(json);
        }

        public ActionResult Eliminar(int id)
        {
           itemsprocess.EliminarCartItem(id);
            var carrito = cartprocess.GetByCookie(User.Identity.Name);

            ViewBag.Total = sum_items(carrito.CartItem.ToList());
            return View("Index",carrito.CartItem.ToList());

        }

        public ActionResult ShipingIndex()
        {
            var ship = shippingprocess.GetByCookie(User.Identity.Name);
            if (ship == null || ship.Id == 0)
                ship = new Shipping();
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
            ViewBag.Total = sum_items(listadoitems);
            return View(ship);
        }

        public ActionResult AddShipping()
        {
            var ship=shippingprocess.GetByCookie(User.Identity.Name);
            if (ship==null || ship.Id==0)
             ship = new Shipping();
            
            return View(ship);
        }
        [HttpPost]
        public ActionResult AddShipping(Shipping ship)
        {
            if (ship.Id != 0)
            {
                CheckAuditPattern(ship);
                ship = shippingprocess.Editar(ship);
            }
            else
            {
                CheckAuditPattern(ship, true);
                ship = shippingprocess.Agregar(ship);
                
            }

            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
            ViewBag.Total = sum_items(listadoitems);
            return View("Checkout", listadoitems);
        }


        public JsonResult GetShip()
        {
            var ship = Json(shippingprocess.GetByCookie(User.Identity.Name), JsonRequestBehavior.AllowGet);
            return ship;
        }
        [HttpPost]
       public ActionResult procesar_pago(FormCollection Request)
        {
            var token = Request["token"];
            var payment_method_id = Request["payment_method_id"];
            var installments = Request["installments"];
            var issuer_id = Request["issuer_id"];
           
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
             var Total = sum_items(listadoitems);
            MercadoPago.SDK.SetAccessToken(token);
            Payment payment = new Payment()
            {
                TransactionAmount = float.Parse(Total.ToString()),
                Token = token,
                Description = "Spark-Art",
                Installments = Convert.ToInt32(installments),
                PaymentMethodId = payment_method_id,
                IssuerId = issuer_id,
                Payer = new MercadoPago.DataStructures.Payment.Payer()
                {
                    Email = "test_user_46978510@testuser.com"
                }
                
            };
            var payret =payment.Save();
            ViewBag.Confirmacion = (payment.Status);
            return View(payment);
        }
      
       
    }
}