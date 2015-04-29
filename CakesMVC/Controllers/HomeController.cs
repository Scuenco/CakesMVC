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

        /// <summary>
        /// Displays a list of Albums
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<AlbumIndexViewModel> model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Albums.Where(x => x.IsDeleted == false).Select(a => new AlbumIndexViewModel()
                    {
                        AlbumId = a.AlbumId,
                        Title = a.Title,
                        Thumbnail = a.Thumbnail,
                        IsDeleted = a.IsDeleted,

                    }).ToList();
            }
            return View(model);
        }
        /// <summary>
        /// Displays an album details
        /// </summary>
        /// <returns>An album with a list of cakes</returns>
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            AlbumIndexViewModel model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Albums.Where(x => x.AlbumId == id).Select(a => new AlbumIndexViewModel()
                    {
                        AlbumId = a.AlbumId,
                        Title = a.Title,
                        Thumbnail = a.Thumbnail,
                        IsDeleted = a.IsDeleted,
                        Cakes = a.Cakes_Albums.Select(c => new CakeViewModel()
                        {
                            CakeId = c.Cake.CakeId,
                            Title = c.Cake.Title,
                            Remarks = c.Cake.Remarks,
                            Thumbnail = c.Cake.Thumbnail,
                            IsDeleted = c.Cake.IsDeleted,
                            Image = c.Cake.Image
                        }).ToList()
                    }).FirstOrDefault();
            }
            return View(model);
        }
        public ActionResult AddAlbum()
        {
            ViewBag.Message = "Add An Album";
            Album model = new Album()
            {
                Thumbnail = "~/Images/IvoryWedding-med.jpg"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult AddAlbum(Album model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Albums.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditAlbum(int id)
        {
            ViewBag.Message = "Edit Album";
            Album album;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                album = db.Albums.FirstOrDefault(x => x.AlbumId == id);
            }
            return View("AddAlbum", album);
        }
        [HttpPost]
        public ActionResult EditAlbum(Album model)
        {
            ViewBag.Message = "Edit Album";
            Album album;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                album = db.Albums.FirstOrDefault(x => x.AlbumId == model.AlbumId);
                album.Title = model.Title;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAlbum(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Album model = db.Albums.FirstOrDefault(x => x.AlbumId == id);
                //db.Albums.Remove(model);
                model.IsDeleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddCakeToAlbum(int id) //AlbumId
        {
            AddCakeToAlbumViewModel model = new AddCakeToAlbumViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //model.Albums = db.Albums.ToList();
                Album album = db.Albums.FirstOrDefault(x => x.AlbumId == id);

                model.AlbumId = id;
                model.Title = album.Title;
                model.Cakes = db.Cakes.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCakeToAlbum(AddCakeToAlbumViewModel data)
        {
            Cakes_Albums model = new Cakes_Albums()
            {
                AlbumId = data.AlbumId,
                CakeId = data.SelectedCakeId
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CakesAlbums.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = data.AlbumId });
        }
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