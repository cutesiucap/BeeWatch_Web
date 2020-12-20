//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API_Local.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts()
        {
            this.Address = new HashSet<Address>();
            this.LogInHistories = new HashSet<LogInHistories>();
            this.Orders = new HashSet<Orders>();
            this.Phone = new HashSet<Phone>();
        }
    
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogInHistories> LogInHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phone> Phone { get; set; }
    }
}