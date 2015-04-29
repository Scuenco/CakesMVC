using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakesMVC.Models
{
    /// <summary>
    /// Album class w/o the virtual property
    /// </summary>
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public string Thumbnail { get; set; }
    }
}