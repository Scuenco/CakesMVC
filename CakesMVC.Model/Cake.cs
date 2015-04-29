using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Model
{
    public class Cake
    {
        public int CakeId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Cakes_Albums> Cakes_Albums { get; set; }

    }
}
