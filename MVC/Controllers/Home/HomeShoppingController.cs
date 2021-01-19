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
    public class HomeShoppingController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewModels.AllHomeViewModel allHomeViewModel = new ViewModels.AllHomeViewModel()
            {
                homeViewModel = new ViewModels.HomeViewModel()
                {
                    search = "",
                    valueWatch = 0,
                    idFirm = 0,
                    idCategori = 0,
                    idSex = 0,
                    sortBy = 0,
                    leftPage = 0,
                    numPage = 1,
                    rightPage = 0
                },
            };       
            
            var result = GlobalVariables.HttpClient.PostAsJsonAsync<ViewModels.AllHomeViewModel>("lWatchHome", allHomeViewModel);
            result.Wait();
            if (result.Result.IsSuccessStatusCode)
            {
                allHomeViewModel = result.Result.Content.ReadAsAsync<ViewModels.AllHomeViewModel>().Result;
            }

            try
            {
                allHomeViewModel.categories = GlobalVariables.HttpClient.GetAsync("Categories").Result.Content.ReadAsAsync<IEnumerable<Models.Categories>>().Result;
            }
            catch
            {
                allHomeViewModel.categories = null;
            }
            try
            {
                allHomeViewModel.firms = GlobalVariables.HttpClient.GetAsync("Firms").Result.Content.ReadAsAsync<IEnumerable<Models.Firms>>().Result;
            }
            catch
            {
                allHomeViewModel.firms = null;
            }
            try
            {
                allHomeViewModel.sex = GlobalVariables.HttpClient.GetAsync("Sex").Result.Content.ReadAsAsync<IEnumerable<Models.Sex>>().Result;
            }
            catch
            {
                allHomeViewModel.sex = null;
            }

            allHomeViewModel.homeViewModel.lvalue = new List<string>();
            allHomeViewModel.homeViewModel.lvalue.Add("Mọi loại Giá");
            allHomeViewModel.homeViewModel.lvalue.Add("Dưới 500 Nghìn");
            allHomeViewModel.homeViewModel.lvalue.Add("500 Nghìn - 1 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("1 Triệu - 2 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("2 Triệu - 3 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("3 Triệu trở lên");

            allHomeViewModel.homeViewModel.lsortBy = new List<string>();
            allHomeViewModel.homeViewModel.lsortBy.Add("Bán chạy nhất");
            allHomeViewModel.homeViewModel.lsortBy.Add("Rate Đánh giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Giảm dần giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Tăng dần giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Giảm giá Hot");
            allHomeViewModel.homeViewModel.lsortBy.Add("Mới nhất");
            return View(allHomeViewModel);
        }

        [HttpPost]
        public ActionResult Index(ViewModels.HomeViewModel homeViewModel)
        {
            if (homeViewModel.search != null)
            {
                homeViewModel.valueWatch = 0;
                homeViewModel.idCategori = 0;
                homeViewModel.idFirm = 0;
                homeViewModel.idSex = 0;
                homeViewModel.sortBy = 0;
                homeViewModel.leftPage = 0;
                homeViewModel.rightPage = 0;
                homeViewModel.numPage = 1;
            }
            else
            {
                homeViewModel.search = "";
            }
            ViewModels.AllHomeViewModel allHomeViewModel = new ViewModels.AllHomeViewModel()
            {
                homeViewModel = homeViewModel,
            };

            var result = GlobalVariables.HttpClient.PostAsJsonAsync<ViewModels.AllHomeViewModel>("lWatchHome", allHomeViewModel);
            result.Wait();
            if (result.Result.IsSuccessStatusCode)
            {
                allHomeViewModel = result.Result.Content.ReadAsAsync<ViewModels.AllHomeViewModel>().Result;
            }

            try
            {
                allHomeViewModel.categories = GlobalVariables.HttpClient.GetAsync("Categories").Result.Content.ReadAsAsync<IEnumerable<Models.Categories>>().Result;
            }
            catch
            {
                allHomeViewModel.categories = null;
            }
            try
            {
                allHomeViewModel.firms = GlobalVariables.HttpClient.GetAsync("Firms").Result.Content.ReadAsAsync<IEnumerable<Models.Firms>>().Result;
            }
            catch
            {
                allHomeViewModel.firms = null;
            }
            try
            {
                allHomeViewModel.sex = GlobalVariables.HttpClient.GetAsync("Sex").Result.Content.ReadAsAsync<IEnumerable<Models.Sex>>().Result;
            }
            catch
            {
                allHomeViewModel.sex = null;
            }

            allHomeViewModel.homeViewModel.lvalue = new List<string>();
            allHomeViewModel.homeViewModel.lvalue.Add("Mọi loại Giá");
            allHomeViewModel.homeViewModel.lvalue.Add("Dưới 500 Nghìn");
            allHomeViewModel.homeViewModel.lvalue.Add("500 Nghìn - 1 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("1 Triệu - 2 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("2 Triệu - 3 Triệu");
            allHomeViewModel.homeViewModel.lvalue.Add("3 Triệu trở lên");

            allHomeViewModel.homeViewModel.lsortBy = new List<string>();
            allHomeViewModel.homeViewModel.lsortBy.Add("Bán chạy nhất");
            allHomeViewModel.homeViewModel.lsortBy.Add("Rate Đánh giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Giảm dần giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Tăng dần giá");
            allHomeViewModel.homeViewModel.lsortBy.Add("Giảm giá Hot");
            allHomeViewModel.homeViewModel.lsortBy.Add("Mới nhất");
            return View(allHomeViewModel);
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