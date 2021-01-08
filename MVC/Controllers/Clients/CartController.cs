using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers.Clients
{
    public class CartController : Controller
    {
        [AuthorizeCustom]
        // GET: Cart
        public ActionResult Index()
        {
            /*Carts cart;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Carts/"+8).Result;
            cart = httpResponseMessage.Content.ReadAsAsync<Carts>().Result;
            return View(cart);*/
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

        [HttpGet]
        public ActionResult Show()
        {
            if (Session["Account"] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<Models.view_WatchDetailHome> CartDetails;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("GetbyIdAccount/" + (Session["Account"] as Models.Accounts).id).Result;
            CartDetails = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Models.view_WatchDetailHome>>().Result;
            return Json(CartDetails, JsonRequestBehavior.AllowGet);
        }
    }
}