using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_WatchDetails
    {
        public int id { get; set; }
        public string Url_Image { get; set; }
        public Nullable<int> id_Sex { get; set; }
        public int id_Firms { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string Information { get; set; }
        public int id_Shop { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Date_LastEdit { get; set; }
        public string LastEdit_By { get; set; }
        public Nullable<bool> IsLock { get; set; }
        public Nullable<bool> IsCheck { get; set; }
        public Nullable<bool> IsExist { get; set; }
        public string Name_Categories { get; set; }
        public string Name_Phai { get; set; }
        public string Name_Firms { get; set; }
        public Nullable<int> LuotMua { get; set; }
    }
}