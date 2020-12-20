using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_Watch
    {
        public int id { get; set; }
        public string WatchName { get; set; }
        public int Count { get; set; }
        public string Url_Image { get; set; }
        public double Price { get; set; }
        public Nullable<double> Rate { get; set; }
        public string Information { get; set; }
        public int id_Shop { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public string NameofSex { get; set; }
        public string NameofFirm { get; set; }
        public Nullable<int> LuotMua { get; set; }
    }
}