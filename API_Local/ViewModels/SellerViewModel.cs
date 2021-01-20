using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Local.ViewModels
{
    public class SellerViewModel
    {
        public int? Sum_New_Order { get; set; }
        public double? Average_Rate { get; set; }
        public int? Sum_Customer { get; set; }
        public int? Sum_Watches { get; set; }
    }
}