using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.UI.Ecommerce.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        OrderProcess order = new OrderProcess();
        public ActionResult Index()
        {
            
            return View(order.GetByCookie(User.Identity.Name));
        }
    }
}