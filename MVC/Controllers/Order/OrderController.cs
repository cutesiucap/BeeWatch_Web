using MVC.Mail;
using MVC.Models;
using MVC.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers.Order
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpStatus(int id)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsync("UpStatus/" + id, null).Result;
            return Content("success");
        }

        public ActionResult AllOrder()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllOrder()
        {
            int id_Shop = (Session["Account"] as Models.Accounts).Sellers.Shop_Seller.FirstOrDefault().id_Shop;
            ViewModel_Order viewModel_Order = new ViewModel_Order(id_Shop);
            return Json(viewModel_Order.view_AllOrders, JsonRequestBehavior.AllowGet);
        }
    }
    public class ViewModel_Order
    {
        public IEnumerable<view_AllOrder> view_AllOrders { get; set; }
        public ViewModel_Order(int id_Shop)
        {
            view_AllOrders = GlobalVariables.HttpClient.GetAsync("view_AllOrder/" + id_Shop).Result.Content.ReadAsAsync<IEnumerable<view_AllOrder>>().Result;
        }
    }
}