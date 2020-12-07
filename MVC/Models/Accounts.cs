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
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Avatar")]
        public string Url_Image_Avatar { get; set; }

        [Required(ErrorMessage ="The Fied is not null")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Fied is not null")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Fied is not null")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Fied is not null")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Fied is not null")]
        [Display(Name = "Type Account")]
        public Nullable<int> id_Account_Type { get; set; }
        public Nullable<bool> Lock { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Address { get; set; }
        public Nullable<double> Point { get; set; }
        public string Fullname { get; set; }
        public virtual Account_Type Account_Type { get; set; }
    }
}