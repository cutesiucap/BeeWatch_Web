using MVC.Mail;
using MVC.Models;
using MVC.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Sellers
{
    public class SellersController : Controller
    {
        // GET: Sellers
        public ActionResult Index()
        {
            IEnumerable<Accounts> accounts;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Accounts").Result;
            accounts = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Accounts>>().Result;
            return View(accounts);
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult CreateSeller()
        {
            return View();
        }

        public ActionResult LoginSeller()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(FormCollection formCollection)
        {
            if (formCollection["toGmail"] == null)
            {
                return Content("false");
            }
            string CODE = RandomTool.RandomString(5);
            bool result = MailHelper.SendMail(formCollection["toGmail"], "MÃ XÁC NHẬN TÀI KHOẢNG", CODE);
            if (result)
            {
                return Content(CODE);
            }
            else return Content("false");
        }
        public ActionResult CreateWatches()
        {
            return View();
        }
    }
}