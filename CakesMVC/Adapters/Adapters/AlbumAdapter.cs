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
    public class AlbumAdapter : IAlbumAdapter
    {
        public List<Models.AlbumIndexViewModel> GetAllAlbums()
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
            return model;
        }
        public AlbumIndexViewModel GetDetails(int id)
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
                        //Thumbnail = c.Cake.Thumbnail,
                        IsDeleted = c.Cake.IsDeleted,
                        Image = c.Cake.Image
                    }).ToList()
                }).FirstOrDefault();
            }
            return model;
        }
        public AlbumViewModel AddAlbum()
        {
            AlbumViewModel model = new AlbumViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model.Thumbnails =  db.Photos.ToList();
            }
            return model;
        }

        public int AddAlbum(AlbumViewModel viewmodel)
        {
            int result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //check if album already exists
                Album album_exists = db.Albums.Where(x => x.Title.ToLower() == viewmodel.Title.ToLower()).FirstOrDefault();
                if (album_exists != null)
                {
                    result = 0;
                }
                else
                {
                    Album model = new Album()
                    {
                        Title = viewmodel.Title,
                        Thumbnail = viewmodel.Thumbnail
                    };
                    db.Albums.Add(model);
                    result = db.SaveChanges();
                }
            }
            return result;
        }

        public AlbumViewModel EditAlbum(int id)
        {
            AlbumViewModel model;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model = db.Albums.Where(x => x.AlbumId == id).Select(a => new AlbumViewModel()
                {
                    AlbumId = a.AlbumId,
                    IsDeleted = a.IsDeleted,
                    Thumbnail = a.Thumbnail,
                    Title = a.Title,
                    Thumbnails = db.Photos.ToList()
                }).FirstOrDefault();
            }
            return model;
        }

        public int EditAlbum(AlbumViewModel model)
        {
            Album album;
            int result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                album = db.Albums.FirstOrDefault(x => x.AlbumId == model.AlbumId);
                album.Title = model.Title;
                album.Thumbnail = model.Thumbnail;
                result = db.SaveChanges();
            }
            return result;
        }

        public int DeleteAlbum(int id)
        {
            int result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Album model = db.Albums.FirstOrDefault(x => x.AlbumId == id);
                //db.Albums.Remove(model);
                model.IsDeleted = true;
                result = db.SaveChanges();
            }
            return result;
        }

        public AddCakeToAlbumViewModel AddCakeToAlbum(int id)
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
            return model;
        }

        public int AddCakeToAlbum(AddCakeToAlbumViewModel data)
        {
            int result;
            Cakes_Albums model = new Cakes_Albums()
            {
                AlbumId = data.AlbumId,
                CakeId = data.SelectedCakeId
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CakesAlbums.Add(model);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}