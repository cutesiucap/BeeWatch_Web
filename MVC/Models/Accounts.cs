using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Accounts
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public int id_Account_Type { get; set; }
        public string Url_Image_Avatar { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public Nullable<double> Point { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public Nullable<System.DateTime> Time_LastAccess { get; set; }
        public Nullable<bool> IsLock { get; set; }
        public Nullable<bool> Status { get; set; }

        public virtual Account_Type Account_Type { get; set; }
        public virtual Carts Carts { get; set; }
        public virtual Sellers Sellers { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<LogInHistories> LogInHistories { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}