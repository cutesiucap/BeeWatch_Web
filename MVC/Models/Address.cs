using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Address
    {
        public int id { get; set; }
        public int id_Account { get; set; }
        public string id_Province { get; set; }
        public string id_District { get; set; }
        public string AddressDetail { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Address_District Address_District { get; set; }
        public virtual Address_Province Address_Province { get; set; }
    }
}