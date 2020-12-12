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
            Carts cart;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Carts/"+8).Result;
            cart = httpResponseMessage.Content.ReadAsAsync<Carts>().Result;
            return View(cart);
        }
    }
}