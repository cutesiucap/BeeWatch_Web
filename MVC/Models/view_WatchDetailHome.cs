using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_WatchDetailHome
    {
        public int id_Watch { get; set; }
        public int id_Cart { get; set; }
        public int Count { get; set; }
        public Nullable<bool> IsCheck { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Url_Image { get; set; }
    }
}