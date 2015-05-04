using CakesMVC.Model;
using CakesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Adapters.Interfaces
{
    public interface ICakeAdapter
    {
        List<CakeViewModel> GetAllCakes();
        CakeViewModel GetDetails(int id);
        int AddCake(Cake model);
        Cake EditCake(int id);
        int EditCake(Cake model);
        int DeleteCake(int id);
    }
}
