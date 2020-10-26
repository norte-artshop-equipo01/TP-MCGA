using ArtShop.UI.Process;
using ArtShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.UI.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductProcess prd = new ProductProcess();
        // GET: Product
        public ActionResult Index()
        {
            return View(prd.ListarTodosLosProductos());
        }

        
    }
}