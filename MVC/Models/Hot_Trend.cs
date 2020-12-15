using System;

namespace MVC.Models
{
    public class Hot_Trend
    {
        public int id { get; set; }
        public Nullable<int> id_Watch { get; set; }
        public Nullable<int> id_Shop { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsCheck { get; set; }
        public Nullable<bool> IsLock { get; set; }

        public virtual Shops Shops { get; set; }
        public virtual Watches Watches { get; set; }
    }
}