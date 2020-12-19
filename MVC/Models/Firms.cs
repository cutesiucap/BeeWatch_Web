using System.Collections.Generic;

namespace MVC.Models
{
    public class Firms
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Watches> Watches { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}