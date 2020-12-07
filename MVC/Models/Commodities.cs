using System.Collections.Generic;

namespace MVC.Models
{
    public class Commodities
    {
        public int id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }
    }
}