using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Discounts
{
    public class DiscountsController : Controller
    {
        // GET: Discounts
        public ActionResult Index()
        {
            return View();
        }

        //Discounts/EnterCODE
        [HttpPost]
        public ActionResult EnterCODE(string CODE, double Sum)
        {
            Models.Discounts discount = new Models.Discounts()
            {
                Code = CODE,
                TypeDiscounts = new Models.TypeDiscounts()
                {
                    Min_Bill = Sum,
                }
            };
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Discounts>("Discounts/EnterCODE",discount).Result;
            if(httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Models.Discounts discounts = httpResponseMessage.Content.ReadAsAsync<Models.Discounts>().Result;
                return Json(discounts, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}