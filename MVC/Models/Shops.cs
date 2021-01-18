using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Shops
    {
          public int id { get; set; }
        public string Name { get; set; }
        public Nullable<int> id_Master { get; set; }
        public string Address { get; set; }
        public Nullable<double> Point { get; set; }
        public Nullable<bool> Status { get; set; }
        public string UrlAvatar { get; set; }
    
        public virtual ICollection<Hot_Trend> Hot_Trend { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Sellers Sellers { get; set; }
        public virtual ICollection<Shop_Seller> Shop_Seller { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }
    }
}