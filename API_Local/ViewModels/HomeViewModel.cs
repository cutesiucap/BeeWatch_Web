using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Local.ViewModels
{
    public class HomeViewModel
    {
        public string search { get; set; }
        public int valueWatch { get; set; }
        public int idFirm { get; set; }
        public int idCategori { get; set; }
        public int idSex { get; set; }
        public int sortBy { get; set; }
        public int numPage { get; set; }
        public int leftPage { get; set; }
        public int rightPage { get; set; }
    }
    public class AllHomeViewModel
    {
        public IEnumerable<Models.view_WatchDetails> view_WatchDetails { get; set; }
        public ViewModels.HomeViewModel homeViewModel { get; set; }
        public Models.Firms firms { get; set; }
        public Models.Categories categories { get; set; }
        public Models.Sex sex { get; set; }
        public Models.Carts carts { get; set; }
    }
    public class ViewCartIntro
    {
        public int id { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Sum { get; set; }
        public virtual ICollection<ViewCartDetailIntro> viewCartDetailIntros { get; set; }
    }
    public class ViewCartDetailIntro
    {
        public int id_Watch { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string Url_Image { get; set; }
    }
}