using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class WatchesinCarts
    {
        public int id_Carts { get; set; }
        public int id_Watches { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<bool> Check { get; set; }

        public virtual Carts Carts { get; set; }
        public virtual Watches Watches { get; set; }
    }
}