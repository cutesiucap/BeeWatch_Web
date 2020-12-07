using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Bills
    {
        public int id { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public Nullable<int> id_Discounts { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<double> Ship_fee { get; set; }
        public string Payment { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual ICollection<Bought> Bought { get; set; }
    }
}