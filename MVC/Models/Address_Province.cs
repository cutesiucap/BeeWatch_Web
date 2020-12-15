using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Address_Province
    {
        public string id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Address_District> Address_District { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}