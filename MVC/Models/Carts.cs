using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Carts
    {
        public int id { get; set; }
        public Nullable<int> id_discounts { get; set; }
        public string Address { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Sum { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual ICollection<WatchesinCarts> WatchesinCarts { get; set; }
    }
}