using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class LogInHistories
    {
        public int id { get; set; }
        public Nullable<int> id_Account { get; set; }
        public Nullable<System.DateTime> DateLogin { get; set; }

        public virtual Accounts Accounts { get; set; }
    }
}