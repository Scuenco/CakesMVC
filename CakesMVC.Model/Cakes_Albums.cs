using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Model
{
    //Lookup table class
    public class Cakes_Albums
    {
        public int Cakes_AlbumsId { get; set; }
        public int CakeId { get; set; }
        public virtual Cake Cake { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
