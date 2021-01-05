using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Cart
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddWatch(int id)
        {
            int id_cart = (Session["Account"] as Models.Accounts).id;
            Models.CartDetails cartDetails = new Models.CartDetails()
            {
                id_Cart = id_cart,
                id_Watch = id,
                IsCheck = true,
                Count = 1
            };

            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.CartDetails>("CartDetails/AddCartDetail", cartDetails).Result;
            return Content("success");
        }
    }
}