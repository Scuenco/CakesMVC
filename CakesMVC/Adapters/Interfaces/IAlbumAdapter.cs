using CakesMVC.Model;
using CakesMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakesMVC.Adapters.Interfaces
{
    public interface IAlbumAdapter
    {
        List<AlbumIndexViewModel> GetAllAlbums();
        AlbumIndexViewModel GetDetails(int id);
        AlbumViewModel AddAlbum();
        int AddAlbum(AlbumViewModel model);
        AlbumViewModel EditAlbum(int id);
        int EditAlbum(AlbumViewModel model);
        int DeleteAlbum(int id);
        AddCakeToAlbumViewModel AddCakeToAlbum(int id);
        int AddCakeToAlbum(AddCakeToAlbumViewModel data);
    }
}
