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
    
    public partial class fn_LoadBillforCancel_Result
    {
        public int id { get; set; }
        public int id_Shop { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public string Address_Province { get; set; }
        public string Address_District { get; set; }
        public string AddressDetail { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Ship_fee { get; set; }
        public Nullable<int> id_Discount { get; set; }
        public string Payment { get; set; }
        public Nullable<System.DateTime> Date_Create { get; set; }
        public Nullable<System.DateTime> Date_Check { get; set; }
        public string User_Check { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<double> Sum { get; set; }
    }
}
