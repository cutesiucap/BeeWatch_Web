using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Local.ViewModels
{
    public class SellerViewModels
    {
        public int New_Order { get; set; }
        public double? Average_Rate { get; set; }
        public int Sum_Customer { get; set; }
        public int Sum_Watches { get; set; }
    }
}