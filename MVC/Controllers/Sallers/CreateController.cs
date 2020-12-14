using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Sallers
{
    public class CreateController : Controller
    {
        // GET: Create
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Sellers sellers, Accounts accounts)
        {
            return View();
        }
    }
}