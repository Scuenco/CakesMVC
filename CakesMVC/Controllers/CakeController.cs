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
    public class CakeController : Controller
    {
        // GET: Cake
        /// <summary>
        /// Displays a list of Cakes
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CakeViewModel> model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Cakes.Where(x => x.IsDeleted == false).Select(c => new CakeViewModel()
                {
                    CakeId = c.CakeId,
                    Image = c.Image,
                    IsDeleted = c.IsDeleted,
                    Remarks = c.Remarks,
                    Thumbnail = c.Thumbnail,
                    Title = c.Title
                }).ToList();
            }
            return View(model);
        }
        /// <summary>
        /// Displays Cake details
        /// </summary>
        /// <returns>Cake details with a list of albums</returns>
        public ActionResult Details(int id)
        {
            CakeViewModel model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Cakes.Where(x => x.CakeId == id).Select(c => new CakeViewModel()
                {
                   CakeId = c.CakeId,
                   Image = c.Image,
                   IsDeleted = c.IsDeleted,
                   Remarks = c.Remarks,
                   Thumbnail = c.Thumbnail,
                   Title = c.Title,
                   Albums = c.Cakes_Albums.Select(x => new AlbumViewModel()
                   {
                       AlbumId = x.Album.AlbumId,
                       IsDeleted = x.Album.IsDeleted,
                       Thumbnail = x.Album.Thumbnail,
                       Title = x.Album.Title
                   }).ToList()
                }).FirstOrDefault();
            }
            return View(model);
        }

        public ActionResult AddCake()
        {
            ViewBag.Message = "Add A Cake";
            Cake model = new Cake()
            {
                Image = "~/Images/Project/ivory134.jpg",
                //Thumbnail = "~/Images/Thumbnails/IvoryWedding-sm.jpg"
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult AddCake(Cake model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Cakes.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditCake(int id)
        {
            ViewBag.Message = "Edit Cake Details";
            Cake model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Cakes.FirstOrDefault(x => x.CakeId == id);
            }
            return View("AddCake", model);
        }
        [HttpPost]
        public ActionResult EditCake(Cake model)
        {
            Cake cake;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cake = db.Cakes.FirstOrDefault(x => x.CakeId == model.CakeId);
                cake.Title = model.Title;
                cake.Remarks = model.Remarks;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCake(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Cake model = db.Cakes.FirstOrDefault(x => x.CakeId == id);
                model.IsDeleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}