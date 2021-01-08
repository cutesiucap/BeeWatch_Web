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

        public ActionResult Create()
        {
            Sellers.SellersController.CreateSellersModel createSellersModel = new Sellers.SellersController.CreateSellersModel();
            return View(createSellersModel);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(Models.Accounts accounts, Models.Phone phone, Models.Address address, FormCollection formCollection)
        {
            accounts.id_Account_Type = 2;
            accounts.Password = GetMD5(accounts.Password);
            accounts.IsLock = false;
            accounts.Status = true;

            HttpResponseMessage result = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Accounts>("Accounts", accounts).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var x = result.Content.ReadAsStringAsync().Result;
                return Content(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Models.Accounts newaccount = result.Content.ReadAsAsync<Models.Accounts>().Result;
                address.id_Account = newaccount.id;
                address.id_District = address.id_District.Trim();
                address.id_Province = address.id_Province.Trim();
                address.AddressDetail = "Detail";
                phone.id_Account = newaccount.id;

                var resultaddress = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Address>("Addresses", address);
                resultaddress.Wait();

                if (resultaddress.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var x = resultaddress.Result.Content.ReadAsStringAsync().Result;
                    return Content(result.Content.ReadAsStringAsync().Result);
                }

                var resultPhone = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Phone>("Phones", phone);
                resultPhone.Wait();

                if (resultPhone.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var x = resultPhone.Result.Content.ReadAsStringAsync().Result;
                    return Content(result.Content.ReadAsStringAsync().Result);
                }

                Session["Account"] = GlobalVariables.HttpClient.GetAsync("Accounts/" + newaccount.id).Result.Content.ReadAsAsync<Models.Accounts>().Result;
                return Content("success");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
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
            return Content("../../HomeShopping/");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("~/HomeShopping/Index");
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