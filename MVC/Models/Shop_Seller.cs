using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Shop_Seller
    {
        public int id_Shop { get; set; }
        public int id_Seller { get; set; }
        public bool IsCheck { get; set; }
        public bool IsLock { get; set; }

        public virtual Sellers Sellers { get; set; }
        public virtual Shops Shops { get; set; }
    }
}