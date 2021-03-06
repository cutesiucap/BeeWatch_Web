﻿using MVC.Mail;
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
            if (Session["Account"] == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }
            int id = (Session["Account"] as Models.Accounts).id;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Orders/ShowOrder/" + id).Result;
            Models.Accounts accounts = httpResponseMessage.Content.ReadAsAsync<Models.Accounts>().Result;
            return View(accounts);
        }

        public ActionResult EnterOrder()
        {
            return View();
        }

        //Order/EnterOrder
        [HttpPost]
        public ActionResult EnterOrder(string listCode, string listID, string payment)
        {
            List<string> Codes = listCode.Split(' ').ToList();
            List<string> IDs = listID.Split(' ').ToList();

            List<Models.Discounts> discounts = new List<Models.Discounts>();
            for(int i = 0; i < Codes.Count() - 1; i++)
            {
                discounts.Add(new Models.Discounts()
                {
                    Code = Codes[i],
                    id = int.Parse(IDs[i]),
                    Detail = payment
                });
            }
            var x = discounts.AsEnumerable();
            int id = (Session["Account"] as Models.Accounts).id;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<IEnumerable<Models.Discounts>>("Orders/EnterOrder/" + id, discounts.AsEnumerable()).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Models.Accounts accounts = httpResponseMessage.Content.ReadAsAsync<Models.Accounts>().Result;
                return View(accounts);
            }
            return Content("Giao dịch thất bại");
        }


        [HttpPost]
        public ActionResult UpStatus(int id)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsync("UpStatus/" + id, null).Result;
            return Content("success");
        }

        [HttpPost]
        public ActionResult CancelById(int id)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsync("CancelStatus/" + id, null).Result;
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