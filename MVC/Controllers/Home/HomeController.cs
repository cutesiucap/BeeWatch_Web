using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int id)
        {
            IEnumerable<view_Watch> view_watches;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/listwatches/" + id).Result;
            view_watches = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watches);
        }

        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Message = "Đăng nhập";
            return View();
        }
        public ActionResult Login(Models.Accounts accounts)
        {
            accounts.Password = GetMD5(accounts.Password);
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Accounts>("Accounts/Login", accounts).Result;
            Models.Accounts result = httpResponseMessage.Content.ReadAsAsync<Models.Accounts>().Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return Content("false");
            }
            Session["Account"] = accounts;
            if (Session["BackAction"] != null && Session["BackAction"].ToString() != "")
            {
                return RedirectToAction(Session["BackAction"].ToString());
            }
            return RedirectToAction("Home/");
        }

        //Mã hóa MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        } 
    }
}