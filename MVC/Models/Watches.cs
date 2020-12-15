using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Watches
    {
        public int id { get; set; }
        public string Url_Image { get; set; }
        public Nullable<int> id_Sex { get; set; }
        public int id_Firms { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string Information { get; set; }
        public int id_Shop { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Date_LastEdit { get; set; }
        public string LastEdit_By { get; set; }
        public Nullable<bool> IsLock { get; set; }
        public Nullable<bool> IsCheck { get; set; }
        public Nullable<bool> IsExist { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
        public virtual Firms Firms { get; set; }
        public virtual ICollection<Hot_Trend> Hot_Trend { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails1 { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
    }
}