using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Watches
    {
        public int id { get; set; }
        public string Url_Image { get; set; }
        public Nullable<int> id_Commodities { get; set; }
        public Nullable<int> id_Firms { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> id_discounts { get; set; }
        public Nullable<int> id_Seller { get; set; }
        public string Information { get; set; }
        public Nullable<bool> Lock { get; set; }
        public Nullable<double> Rate { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Bought Bought { get; set; }
        public virtual Commodities Commodities { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual Firms Firms { get; set; }
        public virtual Hot_Trend Hot_Trend { get; set; }
        public virtual ICollection<WatchesinCarts> WatchesinCarts { get; set; }
    }
}