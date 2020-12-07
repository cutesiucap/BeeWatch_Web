using System;

namespace MVC.Models
{
    public class Bought
    {
        public int id { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public Nullable<int> id_Bills { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Bills Bills { get; set; }
        public virtual Watches Watches { get; set; }
    }
}