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
    public class CakeController : Controller
    {
        // GET: Cake
        private ICakeAdapter adapter;
        public CakeController()
        {
            adapter = new CakeAdapter();
        }
        public CakeController(ICakeAdapter _adapter)
        {
            adapter = _adapter;
        }
        /// <summary>
        /// Displays a list of Cakes
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<CakeViewModel> model = adapter.GetAllCakes();
            return View(model);
        }
        /// <summary>
        /// Displays Cake details
        /// </summary>
        /// <returns>Cake details with a list of albums</returns>
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            CakeViewModel model = adapter.GetDetails(id);
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
            ViewBag.Message = "Add A Cake";
            int result = adapter.AddCake(model);
            if (result != 1)
            {
                ViewBag.ErrorMessage = "A cake with the title '" + model.Title + "' already exists.";
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditCake(int id)
        {
            ViewBag.Message = "Edit Cake Details";
            Cake model = adapter.EditCake(id);
            return View("AddCake", model);
        }
        [HttpPost]
        public ActionResult EditCake(Cake model)
        {
            
            //Cake cake;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    cake = db.Cakes.FirstOrDefault(x => x.CakeId == model.CakeId);
            //    cake.Title = model.Title;
            //    cake.Remarks = model.Remarks;
            //    db.SaveChanges();
            //}
            int result = adapter.EditCake(model);
            if (result != 1)
            {
                ViewBag.Message = "An error occurred in editing a cake.";
                return View("AddCake", model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCake(int id)
        {
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    Cake model = db.Cakes.FirstOrDefault(x => x.CakeId == id);
            //    model.IsDeleted = true;
            //    db.SaveChanges();
            //}
            int result = adapter.DeleteCake(id);
            if (result != 1)
            {
                ViewBag.Message = "An error occurred in deleting a cake.";
                return View("Index");
            }
            return RedirectToAction("Index");
        }
    }
}