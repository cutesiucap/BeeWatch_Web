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
            IEnumerable<Models.Accounts> accounts;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Accounts").Result;
            accounts = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Models.Accounts>>().Result;
            return View(accounts);
        }
    }
}