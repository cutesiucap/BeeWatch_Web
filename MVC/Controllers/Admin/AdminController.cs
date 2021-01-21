using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Admin
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShopManager()
        {
            return View();
        }

        public ActionResult WatchManager()
        {
            return View();
        }

        public ActionResult AccountManager()
        {
            return View();
        }
        
        public ActionResult DiscountManager()
        {
            return View();
        }

        public ActionResult RevenueStatistics()
        {
            return View();
        }

        public ActionResult CustomerStatistics()
        {
            return View();
        }

        public ActionResult WatchStatistics()
        {
            return View();
        }
    }
}