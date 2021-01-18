using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Discounts
    {
        public int id { get; set; }
        public string Code { get; set; }
        public Nullable<int> id_Type { get; set; }
        public Nullable<double> Value___ { get; set; }
        public Nullable<double> Value___1 { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Detail { get; set; }

        public virtual TypeDiscounts TypeDiscounts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}