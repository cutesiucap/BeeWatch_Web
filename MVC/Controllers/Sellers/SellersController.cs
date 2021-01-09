using MVC.Mail;
using MVC.Models;
using MVC.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Sellers
{
    public class SellersController : Controller
    {
        // GET: Sellers 
       [AuthorizeCustom]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateSeller()
        {
            CreateSellersModel createSellersModel = new CreateSellersModel();
            return View(createSellersModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateSeller(Models.Accounts accounts, Models.Sellers sellers, Models.Phone phone, Models.Address address, FormCollection formCollection)
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
                sellers.id = newaccount.id;

                var resultseller = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Sellers>("Sellers", sellers);
                resultseller.Wait();

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

                Shop_Seller shop_Seller = new Shop_Seller();

                if (formCollection["name_Shop"] == null && formCollection["id_Shop"] != null)
                {
                    shop_Seller.id_Shop = int.Parse(formCollection["id_Shop"].ToString().Trim());
                    shop_Seller.id_Seller = sellers.id;
                    shop_Seller.IsCheck = true;
                }
                else
                {
                    Shops shops = new Shops();
                    shops.Name = formCollection["name_Shop"].ToString();
                    shops.id_Master = sellers.id;
                    shops.id = sellers.id;

                    var resultshop = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Shops>("Shops", shops);
                    resultshop.Wait();

                    shop_Seller.id_Shop = resultshop.Result.Content.ReadAsAsync<Models.Shops>().Result.id;
                    shop_Seller.id_Seller = sellers.id;
                }

                var resultshop_seller = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Shop_Seller>("Shop_Seller", shop_Seller);
                resultshop_seller.Wait();

                Session["Account"] = GlobalVariables.HttpClient.GetAsync("Accounts/"+newaccount.id).Result.Content.ReadAsAsync<Models.Accounts>().Result;
                return Content("success");
            }


        }
        
        [AllowAnonymous]
        public ActionResult LoginSeller()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginSeller(Models.Accounts accounts)
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
            //result.Account_Type = GlobalVariables.HttpClient.GetAsync("Account_Type/Account_Type_By_id_Account/" + result.id_Account_Type).Result.Content.ReadAsAsync<Account_Type>().Result;
            //result.Account_Type.Authoriza = GlobalVariables.HttpClient.GetAsync("Authorizas/Account_Type_By_id_Account/{id}" + result.Account_Type.id).Result.Content.ReadAsAsync<List<Authoriza>>().Result;
            Session["Account"] = result;
            return Content("../../Sellers");
            //var a = Session["BackAction"];
            /*if (Session["BackAction"] != null && Session["BackAction"].ToString() != "")
            {
                return RedirectToRoute(Session["BackAction"].ToString());
            }*/
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendMail(FormCollection formCollection)
        {
            if (formCollection["toGmail"] == null)
            {
                return Content("false");
            }
            string CODE = RandomTool.RandomString(5);
            bool result = MailHelper.SendMail(formCollection["toGmail"], "MÃ XÁC NHẬN TÀI KHOẢN", CODE);
            if (result)
            {
                return Content(CODE);
            }
            else return Content("false");
        }
        public ActionResult Create_new_Watch()
        {
            return View();
        }
  
        public ActionResult CreateWatches()
        {

            CreateWatchModel watchModel = new CreateWatchModel((Session["Account"] as Models.Accounts).Sellers.Shop_Seller.FirstOrDefault().id_Shop);
            return View(watchModel);
        }

        [HttpPost]
        public ActionResult CreateWatches(Models.Watches request, FormCollection formCollection)
        {
            var list_categories = formCollection["list_Categories"].Replace(',', ' ').ToList();
            request.Watches_Categories = new List<Watches_Categories>();
            foreach (var item in list_categories)
            {
                if (item.ToString() != " ")
                {
                    request.Watches_Categories.Add(new Watches_Categories() { id_Category = int.Parse(item.ToString()) });
                }
            }
            request.id_Shop = (Session["Account"] as Models.Accounts).Sellers.Shop_Seller.FirstOrDefault().id_Shop;
            request.Create_By = (Session["Account"] as Models.Accounts).Fullname;
            var result = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Watches>("Watches", request);
            result.Wait();
            Session["id_newWatch"] = result.Result.Content.ReadAsAsync<int>().Result;
            return Content("success");
        }


        [HttpPost]
        public ActionResult DeleteFile(FormCollection formCollection)
        {
            string strPhysicalFolder = Server.MapPath("..\\Source\\Watch\\Image\\");

            string strFileFullPath = strPhysicalFolder + formCollection["file_name"];

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
                return Content("Xóa Ảnh Thành Công");
            }
            else
            {
                return Content("Ảnh hiện tại chỉ được xóa trên Review, thực tế không tồn tại trên Server!!");
            }            
        }
        [HttpGet]
        public ActionResult GetByid_Province(string id)
        {
            var x = id;
            IEnumerable<Address_District> address_Districts = GlobalVariables.HttpClient.GetAsync("Address_District/District_by_id_Province/" + id.ToString()).Result.Content.ReadAsAsync<IEnumerable<Address_District>>().Result;
            if (address_Districts.Count() == 0)
            {
                return Content("false");
            }
            @ViewBag.list_Districts = address_Districts;
            return Json(address_Districts);
        }

        [HttpPost]
        public ActionResult UpLoadFile()
        {
            var lFile = Request.Files;
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Source\\Watch", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Image");

                        var fileName1 = Path.GetFileName(Session["id_newWatch"] + "_" + file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, fileName1);
                        file.SaveAs(path);

                        Image image = new Image
                        {
                            id_Watches = int.Parse(Session["id_newWatch"].ToString()),
                            Url_Image = "../../Source/Watch/Image/" + fileName1,
                        };
                        var result = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Image>("Images", image);
                    }

                }
                return Content("success");
            }
            catch {
                return Content("fail");
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("LoginSeller");
        }

        public class CreateWatchModel
        {
            public IEnumerable<Firms> listfirms { get; set; }
            public IEnumerable<Sex> listsex { get; set; }
            public IEnumerable<view_WatchDetails> listwatches { get; set; }
            public IEnumerable<Categories> listcategories { get; set; }
            public CreateWatchModel(int id)
            {
                try
                {
                    HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Firms").Result;
                    listfirms = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Firms>>().Result;
                }
                catch
                {
                    listfirms = null;
                }

                try
                {
                    listcategories = GlobalVariables.HttpClient.GetAsync("Categories").Result.Content.ReadAsAsync<IEnumerable<Categories>>().Result;
                }
                catch
                {
                    listfirms = null;
                }

                try
                {
                    listsex = GlobalVariables.HttpClient.GetAsync("Sex").Result.Content.ReadAsAsync<IEnumerable<Sex>>().Result;
                }
                catch
                {
                    listsex = null;
                }

                try
                {
                    listwatches = GlobalVariables.HttpClient.GetAsync("GetListWatch/"  + id).Result.Content.ReadAsAsync<IEnumerable<view_WatchDetails>>().Result;
                }
                catch
                {
                    listwatches = null;
                }

            }
        }
        public class CreateSellersModel
        {
            public IEnumerable<Address_Province> address_Provinces { get; set; }
            public IEnumerable<Address_District> address_Districts { get; set; }
            public CreateSellersModel()
            {
                address_Provinces = GlobalVariables.HttpClient.GetAsync("Address_Province").Result.Content.ReadAsAsync<IEnumerable<Address_Province>>().Result;

                address_Districts = GlobalVariables.HttpClient.GetAsync("Address_District").Result.Content.ReadAsAsync<IEnumerable<Address_District>>().Result;
            }
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