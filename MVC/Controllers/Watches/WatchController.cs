using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Watches
{
    public class WatchController : Controller
    {
        // GET: Watch
        public ActionResult Index(int id)
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}