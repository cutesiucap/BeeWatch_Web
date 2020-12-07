using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DangNhap()
        {
            ViewBag.Message = "Đăng nhập";

            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string taikhoan = f["username"].ToString();
            string matkhau = f["password"].ToString();
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Accounts?username=" + taikhoan + "&?password=" + matkhau).Result;
            Accounts accounts = new Accounts();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                int id = httpResponseMessage.Content.ReadAsAsync<int>().Result;
                HttpResponseMessage httpResponseMessage1 = GlobalVariables.HttpClient.GetAsync("Accounts/" + id).Result;
                accounts = httpResponseMessage1.Content.ReadAsAsync<Accounts>().Result;
            }

            //nếu user nhập đúng mật khẩu
            if (accounts.Username != null)
            {
                if (accounts.Status == false)
                {
                    return Content("er_block");
                }
                else
                {
                    Session["user"] = accounts;
                    /*try
                    { //lấy thời gian đăng nhập lưu vào hệ thống
                        //us.lastvisitdate = DateTime.Now;
                        db.SaveChanges();
                    }
                    catch { };*/

                    return Content("/Accounts/Index");

                    /*if (us.usertype == "3")
                    {
                        return Content("/SinhVien/QuanLyTaiKhoan");
                    }
                    if (us.usertype == "1")
                    {
                        return Content("/Home/Index");
                    }
                    if (us.usertype == "2")
                    {
                        return Content("/GiangVien/QuanLyTaiKhoan");
                    }
                    if (us.usertype == "4")
                    {
                        return Content("/TruongBoMon/QuanLyTaiKhoan");
                    }*/
                }
            }
            return Content("false");
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