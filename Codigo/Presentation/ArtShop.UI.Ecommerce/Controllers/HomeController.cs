using System.Web.Mvc;
using ArtShop.UI.Process;
using IdentityArtShop.Models;

namespace IdentityArtShop.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        ProductProcess prod = new ProductProcess();
        [HttpGet]
        public ActionResult Index()
        {
            
            return View(prod.ListarTodosLosProductos());
        }

        [HttpGet]
        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        
    }
}
