using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            IEnumerable<Accounts> accounts;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Accounts").Result;
            accounts = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Accounts>>().Result;
            return View(accounts);
        }

        public ActionResult Create(int id = 0)
        {
            return View(new Accounts());
        }

        [HttpPost]
        public ActionResult Create(Accounts accounts)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync("Accounts", accounts).Result;
            return RedirectToAction("Index");
        }
    }
}