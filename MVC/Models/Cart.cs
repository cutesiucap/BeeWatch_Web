using System;

namespace MVC.Models
{
    public class Cart
    {
        public int id { get; set; }
        public Nullable<bool> Check { get; set; }
        public Nullable<int> id_Watches { get; set; }
        public string Name_Watches { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> id_discounts { get; set; }
        public string Address { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual Watches Watches { get; set; }
    }
}