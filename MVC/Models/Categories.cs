using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Categories
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }
    }
}