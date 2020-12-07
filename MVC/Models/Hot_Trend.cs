using System;

namespace MVC.Models
{
    public class Hot_Trend
    {
        public int id { get; set; }
        public string Image { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual Watches Watches { get; set; }
    }
}