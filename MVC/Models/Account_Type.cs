﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Account_Type
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<Authoriza> Authoriza { get; set; }
    }
}