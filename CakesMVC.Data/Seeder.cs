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
        public static void Seed(ApplicationDbContext db, bool roles = false, bool users = false, bool albums = false, bool cakes = false, bool cakes_albums = false)
        {
            if (roles) SeedRoles(db);
            if (users) SeedUsers(db);
            if (albums) SeedAlbums(db);
            if (cakes) SeedCakes(db);
            if (cakes_albums) SeedCakes_Albums(db);
        }
        private static void SeedRoles(ApplicationDbContext db)
        {
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
                    Email = "test@test.com"
                };
                manager.Create(user, "123123");
                manager.AddToRole(user.Id, "User");
            }
        }
        private static void SeedAlbums(ApplicationDbContext db)
        {
            db.Albums.AddOrUpdate(x => x.AlbumId,
                new Album() { AlbumId = 1, Title = "Children's Cakes", Thumbnail = "~/Images/Project/baby640.jpg" },
                new Album() { AlbumId = 2, Title = "Sculpted Cakes", Thumbnail = "~/Images/Project/dome134.jpg" },
                new Album() { AlbumId = 3, Title = "Tiered Cakes", Thumbnail = "~/Images/Project/nipa.jpg" }
               // new Album() { AlbumId = 4, Title = "Competition Cakes", Thumbnail = "img" }
                );
        }
        private static void SeedCakes(ApplicationDbContext db)
        {
            db.Cakes.AddOrUpdate(x => x.CakeId,
                new Cake() { CakeId = 1, Title = "Baby Shower", Remarks = "It's a boy.", Thumbnail = "~/Images/Thumbnails/baby-sm.jpg", Image = "~/Images/Project/babyshower134.jpg" },
                new Cake() { CakeId = 2, Title = "The Dome at Dusk", Remarks = "The dome of St. Peter's Basilica.", Thumbnail = "~/Images/Thumbnails/Dome-sm.jpg", Image = "~/Images/Project/dome134.jpg" },
                new Cake() { CakeId = 3, Title = "Cinderella", Remarks = "A cake for a princess.", Thumbnail = "~/Images/Thumbnails/cinderella-sm.jpg", Image = "~/Images/Project/cinderella134.jpg" },
                new Cake() { CakeId = 4, Title = "Lampshade", Remarks = "This cake is 14 inches in diameter.", Thumbnail = "~/Images/Thumbnails/lampshade-sm.jpg", Image = "~/Images/Project/lampshade1.jpg" },
                new Cake() { CakeId = 5, Title = "Purse", Remarks = "Ladies's purse and shoe", Thumbnail = "~/Images/Thumbnails/ShoeHandbag-sm.jpg", Image = "~/Images/Project/brownpurse.jpg" },
                new Cake() { CakeId = 6, Title = "Baby Shower", Remarks = "", Thumbnail = "url", Image = "~/Images/Project/babyshower2_134.jpg" },
                new Cake() { CakeId = 7, Title = "Chicken Little", Remarks = "", Thumbnail = "url", Image = "~/Images/Project/chickenlittle134.jpg" },
                new Cake() { CakeId = 8, Title = "Golf", Remarks = "", Thumbnail = "url", Image = "~/Images/Project/golf134.jpg" },
                new Cake() { CakeId = 9, Title = "Green Purse", Remarks = "", Thumbnail = "url", Image = "~/Images/Project/greenpurse134.jpg" },
                new Cake() { CakeId = 10, Title = "Ruffles", Remarks = "", Thumbnail = "url", Image = "~/Images/Project/ruffled134.jpg" }
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
    }
}
