using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.OrderDetail
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetById(int id)
        {
            try
            {
                IEnumerable<view_AllOrderDetail> view_AllOrderDetails = GlobalVariables.HttpClient.GetAsync("OrderDetail/Getview_OrderDetail/" + id).Result.Content.ReadAsAsync<IEnumerable<view_AllOrderDetail>>().Result;
                return Json(view_AllOrderDetails, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}