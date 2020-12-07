using System;

namespace MVC.Models
{
    public class Authoriza
    {
        public int id { get; set; }
        public Nullable<int> id_Account_Type { get; set; }
        public string Controller_Action { get; set; }

        public virtual Account_Type Account_Type { get; set; }
    }
}