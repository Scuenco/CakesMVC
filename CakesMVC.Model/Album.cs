﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Model
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required(ErrorMessage = "Please enter an album title.")]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public string Thumbnail { get; set; }
        public virtual List<Cakes_Albums> Cakes_Albums { get; set; }
    }
}
