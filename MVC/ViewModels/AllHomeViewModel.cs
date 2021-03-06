﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVC.ViewModels
{
    public class AllHomeViewModel
    {
        public IEnumerable<Models.view_WatchDetails> view_WatchDetails { get; set; }
        public ViewModels.HomeViewModel homeViewModel { get; set; }
        public IEnumerable<Models.Firms> firms { get; set; }
        public IEnumerable<Models.Categories> categories { get; set; }
        public IEnumerable<Models.Sex> sex { get; set; }        
        public Models.Carts carts { get; set; }
    }
    public class ViewCartIntro
    {        
        public int id { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> Sum { get; set; }
        public virtual ICollection<ViewCartDetailIntro> viewCartDetailIntros { get; set; }
    }
    public class ViewCartDetailIntro
    {
        public int id_Watch { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string Url_Image { get; set; }
    }
}