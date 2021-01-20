using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class WatchDetailViewModel
    {
        public Watch watch { get; set; }
        public Shop shop { get; set; }
        public ICollection<Danhgia> danhgias { get; set; }
        public IEnumerable<Models.view_WatchDetails> Gianhangcungban { get; set; }
        public IEnumerable<Models.view_WatchDetails> Sanphamtuongtu { get; set; }
        public ICollection<Models.Image> Images { get; set; }
    }

    public class Watch
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Luotmua { get; set; }
        public double? Rate { get; set; }
        public int? Luotdanhgia { get; set; }
        public int? Count { get; set; }
        public string Detail { get; set; }
        public string Url_Image { get; set; }
    }

    public class Shop
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string UrlAvatar { get; set; }
        public double? Rate { get; set; }
        public double? BestRate { get; set; }
        public int? Luotdanhgia { get; set; }
        public int? Dangkinhdoanh { get; set; }
        public int? Daban { get; set; }
        public DateTime? Thoigiantao { get; set; }
        public int? Khachhang { get; set; }
        public string Master_Name { get; set; }
        public int id_Master { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
    }

    public class Danhgia
    {
        public int? id_Account { get; set; }
        public string Name_Account { get; set; }
        public string UrlAvatar_Account { get; set; }
        public double? Rate { get; set; }
        public string Detail { get; set; }
        public int? id_Watch { get; set; }
        public string Name_Watch { get; set; }
        public string Url_Watch { get; set; }
    }
}