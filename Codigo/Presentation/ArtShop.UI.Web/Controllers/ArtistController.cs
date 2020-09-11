using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtShop.UI.Web.Controllers
{
    public class ArtistController : Controller
    {

        private ArtistProcess artistProcess = new ArtistProcess();
        public ActionResult Index()
        {
            var artists = artistProcess.ListarTodosLosArtistas();
            return View(artists);
        }

        public ActionResult Add()
        {
            var artist = artistProcess.Add();
            return View(artist);
        }


    }
}