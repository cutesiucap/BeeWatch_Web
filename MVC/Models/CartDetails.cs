using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CartDetails
    {
        public int id_Watch { get; set; }
        public int id_Cart { get; set; }
        public int Count { get; set; }
        public Nullable<bool> IsCheck { get; set; }

        public virtual Carts Carts { get; set; }
        public virtual Watches Watches { get; set; }
    }
}