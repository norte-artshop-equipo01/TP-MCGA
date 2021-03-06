﻿using ArtShop.UI.Process;
using ArtShop.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ArtShop.UI.Web.Controllers
{
    public class ProductController : BaseController
    {
        ProductProcess prd = new ProductProcess();
        ArtistProcess art = new ArtistProcess();
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Artist = art_bag();
            return View(prd.ListarTodosLosProductos());
        }

        public ActionResult Add()
        {
            Product product = new Product();
            ViewBag.Artist = art_bag();
            return View(product);
        }

       [HttpPost]
        public ActionResult Add(Product product, HttpPostedFileBase Image2)
        {
            //Product prdnew = new Product();
            //UpdateModel(prdnew);
            //Image2.SaveAs("./"+Image2.FileName);
            var image = ParseCv(Image2);
            product.Image = image;
            CheckAuditPattern(product, true);
            prd.AgregarProducto(product);




            ViewBag.Artistas = art_bag();
            return View("Index", prd.ListarTodosLosProductos());
        }

        public PartialViewResult Edit(int id)
        {
            var product = prd.GetById(id);
            ViewBag.Artist = art_bag();
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            prd.EditarProduct(product);
            var productos = prd.ListarTodosLosProductos();
            return View("Index", productos);
        }
        public string ParseCv(HttpPostedFileBase cvFile)
        {
            byte[] fileInBytes = new byte[cvFile.ContentLength];
            using (BinaryReader theReader = new BinaryReader(cvFile.InputStream))
            {
                fileInBytes = theReader.ReadBytes(cvFile.ContentLength);
            }
            //string fileAsString = Convert.ToBase64String(fileInBytes);
            return Convert.ToBase64String(fileInBytes); 
        }



        //public static string Conversor(string Path)
        //{
        //    byte[] imageArray = System.IO.File.ReadAllBytes(Path);
        //    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
        //    return base64ImageRepresentation;
        //}

        private List<SelectListItem> art_bag()
        {
            List<SelectListItem> artistas = art.ListarTodosLosArtistas().ConvertAll(
               d =>
               {
                   return new SelectListItem()
                   {
                       Text = d.FullName,
                       Value = d.Id.ToString()
                   };
               }).ToList();

            return artistas;
        }
        public ActionResult Eliminar(int id)
        {
            var product = prd.GetById(id);
            product.Artist = art.GetById(product.ArtistId);
            return View("Eliminar", product);
        }

        [HttpPost]
        public ActionResult Eliminar(Product product)
        {
            
            prd.EliminarProduct(product);
            var productos = prd.ListarTodosLosProductos();
            return View("Index", productos);
        }

    }
}