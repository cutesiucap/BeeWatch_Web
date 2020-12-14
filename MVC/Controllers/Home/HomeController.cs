using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Message = "Đăng nhập";

            return View();
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
        //xử lý đăng nhập
        /*public ActionResult DangNhap(FormCollection f)
        {

            string taikhoan = f["username"].ToString();
            string matkhau = f["password"].ToString();


            //nếu user nhập đúng mật khẩu
            if (ad != null)
            {
                if (ad.Status == false)
                {
                    return Content("er_block");
                }
                else
                {
                    Session["user"] = ad;
                    *//*try
                    { //lấy thời gian đăng nhập lưu vào hệ thống
                        //us.lastvisitdate = DateTime.Now;
                        db.SaveChanges();
                    }
                    catch { };*//*

                    return Content("/Users/Index");

                    *//*if (us.usertype == "3")
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
                    }*//*
                }
            }
            return Content("false");
        }*/
    }
}