using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Discounts
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Info { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<double> Limit_bill { get; set; }
        public string Payment_methods { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual ICollection<Bills> Bills { get; set; }
        public virtual ICollection<Carts> Cart { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }
    }
}