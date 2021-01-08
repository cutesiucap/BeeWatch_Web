using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class HomeViewModel
    {
        public string search { get; set; }
        public int valueWatch { get; set; }
        public int idFirm { get; set; }
        public int idCategori { get; set; }
        public int idSex { get; set; }
        public int sortBy { get; set; }
        public int numPage { get; set; }
        public int leftPage { get; set; }
        public int rightPage { get; set; }
        public List<string> lvalue { get; set; }
        public List<string> lsortBy { get; set; }
    }
}