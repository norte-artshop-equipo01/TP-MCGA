using ArtShop.UI.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtShop.Entities;

namespace ArtShop.UI.Web.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {

        private ArtistProcess artistProcess = new ArtistProcess();
        public ActionResult Index()
        {
            var artists = artistProcess.ListarTodosLosArtistas();
            return View("Index", artists);
        }

        public PartialViewResult Add()
        {
            Entities.Model.Artist artist = new Entities.Model.Artist();
            //var artist = artistProcess.Add();
            return PartialView(artist) ;
        }
        [HttpPost]
        public ActionResult Add(ArtShop.Entities.Model.Artist artist)
        {
            artist.CreatedOn= DateTime.Now;
            artist.CreatedBy = "emmanuel";
            artistProcess.AgregarArtista(artist);
            
            var artistas = artistProcess.ListarTodosLosArtistas();
            return View("Index", artistas);
        }
      
       
        public PartialViewResult Edit(int id)
        {
            var artist = artistProcess.GetById(id);
            return PartialView(artistProcess.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(ArtShop.Entities.Model.Artist artist)
        {
            artistProcess.EditarArtista(artist);
            var artistas = artistProcess.ListarTodosLosArtistas();
            return View("Index",artistas);
        }
        
        public ActionResult Eliminar(int id)
        {
            var artist = artistProcess.GetById(id);
            //artistProcess.EliminarArtista(artist);
            //var artistas = artistProcess.ListarTodosLosArtistas();
            return View("Eliminar", artist);
        }

        [HttpPost]
        public ActionResult Eliminar(Entities.Model.Artist artist)
        {
            artistProcess.EliminarArtista(artist);
            var artistas = artistProcess.ListarTodosLosArtistas();
            return View("Index", artistas);
        }
    }
}