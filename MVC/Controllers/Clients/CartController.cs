using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers.Clients
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            IEnumerable<Carts> carts;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Carts").Result;
            carts = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Carts>>().Result;
            return View(carts);
        }
    }
}