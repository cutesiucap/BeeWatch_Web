using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Watches_Categories
    {
        public int id_Watch { get; set; }
        public int id_Category { get; set; }
        public string Detail { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual Watches Watches { get; set; }
    }
}