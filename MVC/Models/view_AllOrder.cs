using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_AllOrder
    {
        public Nullable<int> id { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public string Fullname { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public Nullable<int> Status { get; set; }
        public string Address_District { get; set; }
        public string Address_Province { get; set; }
        public string AddressDetail { get; set; }
        public Nullable<int> Count { get; set; }
        public string Payment { get; set; }
        public Nullable<double> Ship_fee { get; set; }
        public Nullable<int> id_Shop { get; set; }
        public Nullable<double> Value___ { get; set; }
        public Nullable<double> Value___1 { get; set; }
        public string Code_Discounts { get; set; }
    }
}