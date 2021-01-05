using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Accounts
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Models.Accounts accounts, Models.Address address, FormCollection formCollection)
        {
            accounts.id_Account_Type = 2;
            accounts.Address = new List<Address>();
            accounts.Address.Add(address);
            accounts.Password = GetMD5(accounts.Password);
            var result = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Accounts>("Accounts/Register", accounts).Result;
            Session["Account"] = accounts;
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult LoginSeller()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Models.Accounts accounts)
        {
            accounts.Password = GetMD5(accounts.Password);

            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Accounts>("Accounts/Login", accounts).Result;

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Content("false");
            }
            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Content("IsLock");
            }
            Models.Accounts result = httpResponseMessage.Content.ReadAsAsync<Models.Accounts>().Result;
            Session["Account"] = result;
            return Content("../../HomePage");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Logout(Models.Accounts accounts)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsync("UpStatus/" + (Session["Account"] as Models.Accounts).id, null).Result;
            return Content("success");
        }

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