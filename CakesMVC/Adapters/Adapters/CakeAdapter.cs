using CakesMVC.Adapters.Interfaces;
using CakesMVC.Data;
using CakesMVC.Model;
using CakesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakesMVC.Adapters.Adapters
{
    public class CakeAdapter : ICakeAdapter
    {
        public List<Models.CakeViewModel> GetAllCakes()
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
                    //Thumbnail = c.Thumbnail,
                    Title = c.Title
                }).ToList();
            }
            return model;
        }

        public Models.CakeViewModel GetDetails(int id)
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
                    //Thumbnail = c.Thumbnail,
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
            return model;
        }

        public int AddCake(Model.Cake model)
        {
            int result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //check if a record already exists
                Cake cake = db.Cakes.Where(x => x.Title.ToLower() == model.Title.ToLower()).First();
                if (cake != null)
                {
                    result = 0;
                }
                else
                {
                    db.Cakes.Add(model);
                    result = db.SaveChanges();
                }
               
            }
            return result;
        }

        public Model.Cake EditCake(int id)
        {
            Cake model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Cakes.FirstOrDefault(x => x.CakeId == id);
            }
            return model;
        }

        public int EditCake(Model.Cake model)
        {
            int result;
            Cake cake;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cake = db.Cakes.FirstOrDefault(x => x.CakeId == model.CakeId);
                cake.Title = model.Title;
                cake.Remarks = model.Remarks;
                result = db.SaveChanges();
            }
            return result;
        }

        public int DeleteCake(int id)
        {
            int result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Cake model = db.Cakes.FirstOrDefault(x => x.CakeId == id);
                model.IsDeleted = true;
                result = db.SaveChanges();
            }
            return result;
        }
    }
}