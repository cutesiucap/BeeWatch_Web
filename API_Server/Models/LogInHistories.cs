//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API_Server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogInHistories
    {
        public int id { get; set; }
        public Nullable<int> id_Account { get; set; }
        public Nullable<System.DateTime> DateLogin { get; set; }
    
        public virtual Accounts Accounts { get; set; }
    }
}