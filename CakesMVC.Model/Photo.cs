using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Model
{
    public class Photo
    {
        [Key]
        public int ImageId { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
    }
}
