using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class WatchinHomeViewModel
    {
        int id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        string ValueDiscount { get; set; }
        double Rate { get; set; }
        int Luotmua { get; set; }
        string UrlImage { get; set; }
    }
}