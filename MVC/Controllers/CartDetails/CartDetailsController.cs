using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.CartDetails
{
    public class CartDetailsController : Controller
    {
        // GET: CartDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Models.CartDetails cartDetails)
        {
            if(Session["Account"] == null)
            {
                return Content("login");
            }
            cartDetails.id_Cart = (Session["Account"] as Models.Accounts).id;
            cartDetails.Count = 1;
            cartDetails.IsCheck = true;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.CartDetails>("CartDetails/Add", cartDetails).Result;
            if (httpResponseMessage.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return Content("addnew");
            }

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Content("addold");
            }

            return Content("fail");
           
        }
    }
}