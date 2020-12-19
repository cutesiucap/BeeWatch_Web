using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Sex
    {
        public int id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}