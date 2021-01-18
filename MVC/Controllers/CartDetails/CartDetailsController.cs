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

        [HttpPost]
        public ActionResult Change(Models.CartDetails cartDetails)
        {
            if (Session["Account"] == null)
            {
                return Content("login");
            }
            cartDetails.id_Cart = (Session["Account"] as Models.Accounts).id;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.CartDetails>("CartDetails/Change", cartDetails).Result;
            
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Content("notfound");
            }
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                cartDetails = httpResponseMessage.Content.ReadAsAsync<Models.CartDetails>().Result;
                return Json(cartDetails, JsonRequestBehavior.AllowGet);
            }
            return Content("fail");

        }

        [HttpGet]
        public ActionResult Show()
        {
            if (Session["Account"] == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<Models.view_CartDetailHome> CartDetails;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("GetbyIdAccount/" + (Session["Account"] as Models.Accounts).id).Result;
            CartDetails = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Models.view_CartDetailHome>>().Result;
            return Json(CartDetails, JsonRequestBehavior.AllowGet);
        }
    }
}