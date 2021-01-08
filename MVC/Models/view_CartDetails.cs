using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_CartDetails
    {
        public int id_Cart { get; set; }
        public int id_Watch { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int SoLuong { get; set; }
        public Nullable<double> ThanhTien { get; set; }
    }
}