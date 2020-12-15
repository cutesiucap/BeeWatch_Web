using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class TypeDiscounts
    {
        public int id { get; set; }
        public Nullable<double> Min_Bill { get; set; }
        public Nullable<double> Max_Discount { get; set; }
        public string Payment_Method { get; set; }
        public virtual ICollection<Discounts> Discounts { get; set; }
    }
}