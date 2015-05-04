using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CakesMVC.Model; //for AddOrUpdate

namespace CakesMVC.Data
{
    public static class Seeder
    {
        public static void Seed(ApplicationDbContext db, bool roles = false, bool users = false, bool albums = false, bool cakes = false, bool cakes_albums = false, bool photos = false)
        {
            if (roles) SeedRoles(db);
            if (users) SeedUsers(db);
            if (albums) SeedAlbums(db);
            if (cakes) SeedCakes(db);
            if (cakes_albums) SeedCakes_Albums(db);
            if (photos) SeedPhotos(db);
        }
        private static void SeedRoles(ApplicationDbContext db)
        {
            var store = new RoleStore<IdentityRole>(db);
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!manager.RoleExists("User"))
            {
                manager.Create(new IdentityRole() { Name = "User" });
            }
            if (!manager.RoleExists("Admin"))
            {
                manager.Create(new IdentityRole() { Name = "Admin" });
            }
        }
        private static void SeedUsers(ApplicationDbContext db)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (!db.Users.Any(x => x.UserName == "admin"))
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "admin",
                    //UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "Admin");
            }
            if (!db.Users.Any(x => x.UserName == "test"))
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "test",
                    //UserName = "test@test.com",
                    Email = "test@test.com"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "User");
            }
        }
        private static void SeedAlbums(ApplicationDbContext db)
        {
            db.Albums.AddOrUpdate(x => x.AlbumId,
                new Album() { AlbumId = 1, Title = "Children's Cakes", Thumbnail = "~/Images/Project/baby.jpg" },
                new Album() { AlbumId = 2, Title = "Sculpted Cakes", Thumbnail = "~/Images/Project/dome134.jpg" },
                new Album() { AlbumId = 3, Title = "Tiered Cakes", Thumbnail = "~/Images/Project/nipa134.jpg" }
               // new Album() { AlbumId = 4, Title = "Competition Cakes", Thumbnail = "img" }
                );
        }
        private static void SeedCakes(ApplicationDbContext db)
        {
            db.Cakes.AddOrUpdate(x => x.CakeId,
                new Cake() { CakeId = 1, Title = "It's a Boy", Remarks = "It's a boy.", Image = "~/Images/Project/babyshower134.jpg" },
                new Cake() { CakeId = 2, Title = "The Dome at Dusk", Remarks = "The dome of St. Peter's Basilica.", Image = "~/Images/Project/dome134.jpg" },
                new Cake() { CakeId = 3, Title = "Cinderella", Remarks = "A cake for a princess.", Image = "~/Images/Project/cinderella134.jpg" },
                new Cake() { CakeId = 4, Title = "Lampshade", Remarks = "This cake is 14 inches in diameter.", Image = "~/Images/Project/lampshade1.jpg" },
                new Cake() { CakeId = 5, Title = "Purse", Remarks = "Ladies's purse and shoe", Image = "~/Images/Project/brownpurse.jpg" },
                new Cake() { CakeId = 6, Title = "Baby Shower", Remarks = "", Image = "~/Images/Project/babyshower2_134.jpg" },
                new Cake() { CakeId = 7, Title = "Chicken Little", Remarks = "", Image = "~/Images/Project/chickenlittle134.jpg" },
                new Cake() { CakeId = 8, Title = "Golf", Remarks = "", Image = "~/Images/Project/golf134.jpg" },
                new Cake() { CakeId = 9, Title = "Green Purse", Remarks = "", Image = "~/Images/Project/greenpurse134.jpg" },
                new Cake() { CakeId = 10, Title = "Ruffles", Remarks = "", Image = "~/Images/Project/ruffled134.jpg" }
                );
        }
        private static void SeedCakes_Albums(ApplicationDbContext db)
        {
            db.CakesAlbums.AddOrUpdate(x => x.Cakes_AlbumsId,
                new Cakes_Albums() { Cakes_AlbumsId = 1, AlbumId = 1, CakeId = 1 },
                new Cakes_Albums() { Cakes_AlbumsId = 2, AlbumId = 2, CakeId = 2 },
                new Cakes_Albums() { Cakes_AlbumsId = 3, AlbumId = 3, CakeId = 3 }
                );
        }
        private static void SeedPhotos(ApplicationDbContext db)
        {
            db.Photos.AddOrUpdate(x => x.ImageId,
                new Photo() { ImageId = 1, Source = "~/Images/Project/babyshower134.jpg", Name = "It's A Boy"},
                new Photo() { ImageId = 2, Source = "~/Images/Project/babyshower2_134.jpg", Name = "Baby Shower" },
                new Photo() { ImageId = 3, Source = "~/Images/Project/brownpurse.jpg", Name = "Brown Purse" },
                new Photo() { ImageId = 4, Source = "~/Images/Project/chickenlittle134.jpg.jpg", Name = "Chicken Little" },
                new Photo() { ImageId = 5, Source = "~/Images/Project/cinderella134.jpg", Name = "Princess" },
                new Photo() { ImageId = 6, Source = "~/Images/Project/dome134.jpg", Name = "Dome" },
                new Photo() { ImageId = 7, Source = "~/Images/Project/golf134.jpg", Name = "Golf" },
                new Photo() { ImageId = 8, Source = "~/Images/Project/greenpurse134.jpg", Name = "Green Purse" },
                new Photo() { ImageId = 9, Source = "~/Images/Project/ivory134.jpg", Name = "Wedding" },
                new Photo() { ImageId = 10, Source = "~/Images/Project/lampshade1.jpg", Name = "Lampshade" },
                new Photo() { ImageId = 11, Source = "~/Images/Project/nipa134.jpg", Name = "Nipa Hut" },
                new Photo() { ImageId = 12, Source = "~/Images/Project/ruffled134.jpg", Name = "Ruffled" },
                new Photo() { ImageId = 13, Source = "~/Images/Project/wine134.jpg", Name = "Wine" }
                );
        }
    }
}
