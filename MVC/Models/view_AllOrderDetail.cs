using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class view_AllOrderDetail
    {
        public Nullable<int> id_Order { get; set; }
        public Nullable<int> id_Accounts { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Address_District { get; set; }
        public string Address_Province { get; set; }
        public string AddressDetail { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<int> SoLuongTong { get; set; }
        public string Payment { get; set; }
        public int id_OrderDetail { get; set; }
        public Nullable<int> id_Watches { get; set; }
        public Nullable<int> SoLuongSP { get; set; }
        public Nullable<double> Price { get; set; }
        public int TonKho { get; set; }
        public Nullable<int> id_Discount { get; set; }
        public Nullable<double> Value___ { get; set; }
        public Nullable<double> Value___1 { get; set; }
        public string Code_Discounts { get; set; }
    }
}