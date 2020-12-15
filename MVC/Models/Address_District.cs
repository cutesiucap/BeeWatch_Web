using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Address_District
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string id_Province { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual Address_Province Address_Province { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}