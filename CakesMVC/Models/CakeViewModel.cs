using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakesMVC.Models
{
    /// <summary>
    /// A Cake class w/o the virtual property
    /// </summary>
    public class CakeViewModel
    {
        public int CakeId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public List<AlbumViewModel> Albums {get; set;}
    }
}