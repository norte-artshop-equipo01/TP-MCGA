using ArtShop.Entities.Model;
using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.UI.Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        ProductProcess prd = new ProductProcess();
        // GET: Product
        public ActionResult Index()
        {
            return View(prd.ListarTodosLosProductos());
        }
        public void ProductDetail2(int id)
        {
            var prod = prd.GetById(id);
            ProductDetail(prod);
        }
        public ActionResult ProductDetail(Product product)
        {
            
            
            return View(prd.GetById(product.Id));
        }
    }
}