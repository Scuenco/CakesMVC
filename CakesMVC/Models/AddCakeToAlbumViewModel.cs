using CakesMVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CakesMVC.Models
{
    /// <summary>
    /// View Model for AddCakeToAlbumViewModel
    /// </summary>
    public class AddCakeToAlbumViewModel
    {
        [Display(Name= "Cakes")]
        public int SelectedCakeId { get; set; }
        public IEnumerable<Cake> Cakes { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        //public IEnumerable<Album> Albums { get; set; }
    }
}