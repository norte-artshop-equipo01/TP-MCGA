using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ArtShop.Entities;
using ArtShop.Entities.Model;

using MercadoPago;
using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;
using iText.Kernel.Pdf;
using System.IO;
using iText.Layout;
using iText.Kernel.Geom;
using System.Web.UI.WebControls;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using System.Globalization;

namespace ArtShop.UI.Ecommerce.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private ProductProcess productprocess = new ProductProcess();
        private CartProcess cartprocess = new CartProcess();
        private CartItemProcess itemsprocess = new CartItemProcess();
        private ShippingProcess shippingprocess = new ShippingProcess();
        private OrderProcess orderprocess = new OrderProcess();
        

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
            cartnew=cartprocess.AgregarCart(cartnew);
            cartitem.CartId = cartnew.Id;
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
            {
                ship = new Shipping();
                CheckAuditPattern(ship, true);
            }
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
        public JsonResult GetCartitem()
        {
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var cart = Json(itemsprocess.GetByCartId(carrito.Id), JsonRequestBehavior.AllowGet);
            return cart;
        }

        [HttpPost]
        public ActionResult procesar_pago(FormCollection Request)
        {
            var token = Request["token"].Split(',')[0];
            var payment_method_id = Request["payment_method_id"].Split(',')[0];
            var installments = Request["installments"].Split(',')[0];
            var issuer_id = Request["issuer_id"].Split(',')[0];
            if (MercadoPago.SDK.AccessToken == null)
            {
                MercadoPago.SDK.SetAccessToken("TEST-6196665787772204-120903-7e3696caa9ad7207eae686fc4d423f53-684823230");
            }
            var carrito = cartprocess.GetByCookie(User.Identity.Name);
            var listadoitems = itemsprocess.GetByCartId(carrito.Id);
             var Total = sum_items(listadoitems);
            //MercadoPago.SDK.SetAccessToken(token);
            Payment payment = new Payment();


            payment.TransactionAmount = (float)Total;
            payment.Token = token;
            payment.Description = "Spark-Art";
            payment.Installments = int.Parse(installments); //Convert.ToInt32(installments);
            payment.PaymentMethodId = payment_method_id;
            payment.IssuerId = issuer_id;
               payment.Payer = new MercadoPago.DataStructures.Payment.Payer()
                {
                    Email = "test_user_46978510@testuser.com"
                
                
                
            };
            var payret =payment.Save();
            
            ViewBag.Confirmacion = (payment.Status);
            ViewBag.Cuotas = payment.Installments;
            ViewBag.Monto = payment.TransactionAmount;

            //Impacto y creacion de la orden y lineas de orden
            Entities.Model.Order order = new Entities.Model.Order();
            CheckAuditPattern(order, true);
            var ship = shippingprocess.GetByCookie(User.Identity.Name);
            order.ShippingId= ship.Id;
            order.Email = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = Total;
            order.ItemCount = carrito.CartItem.Count();
            order=orderprocess.AgregarOrder(order);
            var pdf=crear_pdf(order, listadoitems, ship);
            order.Pdf = pdf;
            orderprocess.EditarOrder(order);
            ViewBag.items = listadoitems;
            ViewBag.pago = payment;
            foreach (CartItem item in listadoitems)
            {
                OrderDetail orderdetail = new OrderDetail();
                CheckAuditPattern(orderdetail, true);
                orderdetail.OrderId = order.Id;
                orderdetail.ProductId = item.ProductId;
                orderdetail.Price = item.Price;
                orderdetail.Quantity = item.Quantity;
                orderprocess.AgregarLinea(orderdetail);
                itemsprocess.EliminarCartItem(item.Id);
            }
            cartprocess.EliminarCart(carrito);

            ViewBag.items = listadoitems;
            
            return View("procesar_pago", order);
        }



        public string crear_pdf(Entities.Model.Order order, List<CartItem> carrito, Shipping ship)
        {
            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfdocument = new PdfDocument(pw);
            Document doc = new Document(pdfdocument, PageSize.A4);
            doc.SetMargins(75, 35, 70, 35);

            //PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            string pathlogo = Server.MapPath("~/Content/assets/img/core-img/logo.png");
            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(pathlogo));

            //pdfdocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(img));

            iText.Layout.Element.Table table = new iText.Layout.Element.Table(2).UseAllAvailableWidth().SetBorder(Border.NO_BORDER);
            iText.Layout.Element.Cell cell = new iText.Layout.Element.Cell(4, 1).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
            cell.Add(img.ScaleToFit(150,150));
            table.AddCell(cell);
            cell = new iText.Layout.Element.Cell(1,2).Add(new Paragraph("Factura No: 0177-" + string.Format("{0:00000000}", order.Id)))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);
            cell = new iText.Layout.Element.Cell(1,2).Add(new Paragraph("Fecha: " + DateTime.Today.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            doc.Add(table);
           //separador
            //Line line = new Line();
            
            //var ls = new LineSeparator(Line.SetWidth(3);
            //doc.Add(ls);
            //Datos Cliente
            iText.Layout.Element.Table _clienttable = new iText.Layout.Element.Table(1).UseAllAvailableWidth().SetMarginTop(20).SetBorder(Border.NO_BORDER);

            iText.Layout.Element.Cell _clientecell = new iText.Layout.Element.Cell(1, 1);
                cell.Add(new Paragraph("Nombre: "+ ship.FirstName+" "+ship.LastName))
                .SetTextAlignment(TextAlignment.LEFT)
                .SetMarginBottom(10).SetBorder(Border.NO_BORDER);
            _clienttable.AddCell(_clientecell);
            _clientecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Dirección: " + ship.Address))
                .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetBorder(Border.NO_BORDER); 

            _clienttable.AddCell(_clientecell);
            _clientecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Ciudad: " + ship.City))
                .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetBorder(Border.NO_BORDER); 
            _clienttable.AddCell(_clientecell);
            _clientecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("País: " + ship.Country))
                .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetBorder(Border.NO_BORDER); 
            _clienttable.AddCell(_clientecell);
            _clientecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Email: " + ship.Email))
                .SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10).SetBorder(Border.NO_BORDER); 
            _clienttable.AddCell(_clientecell);
            doc.Add(_clienttable);

            
            //doc.Add(ls);
            //Datos Product Encabezado
            iText.Layout.Element.Table _prodtable = new iText.Layout.Element.Table(6).UseAllAvailableWidth().SetMarginTop(20).SetBorder(Border.NO_BORDER);

            iText.Layout.Element.Cell _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("# Item"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);
            _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Codigo Articulo"))
               .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);
            _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Descripcion"))
                .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);
            _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Precio Unitario"))
                .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);
            _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Cantidad"))
                .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);
            _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Subtotal"))
                .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
            _prodtable.AddCell(_prodecell);

            int x = 0;

            foreach(CartItem item in carrito)
            {
                x++;
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph(x.ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10); 
                _prodtable.AddCell(_prodecell);
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph(item.ProductId.ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10); 
                _prodtable.AddCell(_prodecell);
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph(item.Product.Title.ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10); 
                _prodtable.AddCell(_prodecell);
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph(item.Price.ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
                _prodtable.AddCell(_prodecell);
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph(item.Quantity.ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10);
                _prodtable.AddCell(_prodecell);
                _prodecell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph((item.Quantity*item.Price).ToString())).SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(10); 
                _prodtable.AddCell(_prodecell);

            }
            doc.Add(_prodtable);

            //Total 
            iText.Layout.Element.Table totaltable = new iText.Layout.Element.Table(1).UseAllAvailableWidth().SetMarginTop(20).SetBorder(Border.NO_BORDER);

            iText.Layout.Element.Cell totalcell = new iText.Layout.Element.Cell(1, 1).Add(new Paragraph("Total: $" +order.TotalPrice))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetMarginBottom(10).SetBorder(Border.NO_BORDER);
            totaltable.AddCell(totalcell);
            doc.Add(totaltable);
            //pdfdocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new HeaderEventHandler1(img));

            //iText.Layout.Element.Table _table = new iText.Layout.Element.Table(1).UseAllAvailableWidth();

            doc.Close();

            byte[] byteStream = ms.ToArray();
            var inputAsString = Convert.ToString(Convert.ToBase64String(ms.ToArray()));
            
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;
            var pdf = new FileStreamResult(ms, "application/pdf");
            
            /*return new FileStreamResult(ms, "application/pdf")*/;

            return inputAsString.ToString();



        }
        
    }
}