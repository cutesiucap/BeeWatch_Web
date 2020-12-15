using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Carts
    {
        public int id { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Sum { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }
}