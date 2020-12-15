using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Orders
    {
        public int id { get; set; }
        public int id_Shop { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public string Address_Province { get; set; }
        public string Address_District { get; set; }
        public string AddressDetail { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<double> Ship_fee { get; set; }
        public Nullable<int> id_Discount { get; set; }
        public string Payment { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Address_District Address_District1 { get; set; }
        public virtual Address_Province Address_Province1 { get; set; }
        public virtual Discounts Discounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Shops Shops { get; set; }
    }
}