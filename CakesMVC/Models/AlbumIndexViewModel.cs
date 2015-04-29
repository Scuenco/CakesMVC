using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakesMVC.Models
{
    /// <summary>
    /// An Album class with a list of cakes
    /// </summary>
    public class AlbumIndexViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public string Thumbnail { get; set; }

        //list of cakes
        public List<CakeViewModel> Cakes {get; set;}
    }
}