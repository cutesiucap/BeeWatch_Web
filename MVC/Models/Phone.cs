using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Phone
    {
        public int id_Account { get; set; }
        public string Phone1 { get; set; }
        public int id { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}