using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class OrderDetails
    {
        public int id_Order { get; set; }
        public int id_Watches { get; set; }
        public string WatchName { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<System.DateTime> Date_Bought { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual Watches Watches { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Watches Watches1 { get; set; }
    }
}