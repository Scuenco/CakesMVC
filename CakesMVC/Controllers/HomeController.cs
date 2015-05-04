using CakesMVC.Adapters.Adapters;
using CakesMVC.Adapters.Interfaces;
using CakesMVC.Data;
using CakesMVC.Model;
using CakesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakesMVC.Controllers
{
     [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
         private IAlbumAdapter adapter;
         public HomeController()
         {
             adapter = new AlbumAdapter();
         }
         public HomeController(IAlbumAdapter _adapter)
         {
             adapter = _adapter;
         }

        /// <summary>
        /// Displays a list of Albums
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<AlbumIndexViewModel> model = adapter.GetAllAlbums();
            return View(model);
        }
        /// <summary>
        /// Displays an album details
        /// </summary>
        /// <returns>An album with a list of cakes</returns>
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            AlbumIndexViewModel model = adapter.GetDetails(id);
            return View(model);
        }
        public ActionResult AddAlbum()
        {
            ViewBag.Message = "Add An Album";
            AlbumViewModel model = adapter.AddAlbum();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAlbum(AlbumViewModel model)
        {
            ViewBag.Message = "Add An Album";
            if (ModelState.IsValid)
            {
                int result = adapter.AddAlbum(model);
                if (result != 1)//album already exists
                {
                    ViewBag.ErrorMessage = "An album with the title '" + model.Title + "' already exists.";
                    //populate the dropdownlist
                    model.Thumbnails = adapter.AddAlbum().Thumbnails;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            else //model is not valid
            {
                ViewBag.ErrorMessage = "Please enter an album title.";
                //populate the dropdownlist
                model.Thumbnails = adapter.AddAlbum().Thumbnails;
                return View(model);
            }
        }
        public ActionResult EditAlbum(int id)
        {
            ViewBag.Message = "Edit Album";
            AlbumViewModel album = adapter.EditAlbum(id);
            return View("AddAlbum", album);
        }
        [HttpPost]
        public ActionResult EditAlbum(AlbumViewModel model)
        {
            ViewBag.Message = "Edit Album";
            int result = adapter.EditAlbum(model);
            if (result != 1)
            {
                ViewBag.Message = "An error occurred while editing an album.";
                return View("AddAlbum", model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAlbum(int id)
        {
            int result = adapter.DeleteAlbum(id);
            if (result != 1)
            {
                ViewBag.Message = "An error occurred while deleting an album.";
                return View("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddCakeToAlbum(int id) //AlbumId
        {
            AddCakeToAlbumViewModel model = adapter.AddCakeToAlbum(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCakeToAlbum(AddCakeToAlbumViewModel data)
        {
            int result = adapter.AddCakeToAlbum(data);
            if (result != 1)
            {
                ViewBag.Message = "An error occurred while adding a cake to the album.";
                return View();
            }
            return RedirectToAction("Details", new { id = data.AlbumId });
        }
         /*
        public ActionResult DeleteCakeFromAlbum(int albumid, int cakeid)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Cakes_Albums model =  db.CakesAlbums.FirstOrDefault(x => x.AlbumId == albumid && x.CakeId == cakeid );
                db.CakesAlbums.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        */

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}