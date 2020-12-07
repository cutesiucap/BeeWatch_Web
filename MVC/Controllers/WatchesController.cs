using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class WatchesController : Controller
    {
        // GET: Watches
        public ActionResult Index()
        {
            return View();
        }
    }
}