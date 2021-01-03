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

namespace MVC.Controllers.Sellers
{
    public class SellersController : Controller
    {
        // GET: Sellers 
       
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
        public ActionResult CreateSeller(Models.Accounts accounts, Models.Sellers sellers, Models.Address address, FormCollection formCollection)
        {
            accounts.id_Account_Type = 2;
            accounts.Address = new List<Address>();
            accounts.Address.Add(address);
            var result = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Accounts>("Accounts/Register", accounts).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var x = result.Content.ReadAsStringAsync().Result;
                return Content(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Accounts newaccount = result.Content.ReadAsAsync<Models.Accounts>().Result;
                sellers.id = newaccount.id;

                var resultseller = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Sellers>("Sellers", sellers);
                resultseller.Wait();

                Shop_Seller shop_Seller = new Shop_Seller();

                if (formCollection["name_Shop"].ToString() == null && int.Parse(formCollection["id_Shop"].ToString()) != 0)
                {
                    shop_Seller.id_Shop = int.Parse(formCollection["id_Shop"].ToString());
                    shop_Seller.id_Seller = sellers.id;
                }
                else
                {
                    Shops shops = new Shops();
                    shops.Name = formCollection["name_Shop"].ToString();
                    shops.id_Master = sellers.id;
                    shops.id = sellers.id;

                    var resultshop = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Shops>("Shops", shops);
                    resultshop.Wait();

                    shop_Seller.id_Shop = shops.id;
                    shop_Seller.id_Seller = sellers.id;
                }

                var resultshop_seller = GlobalVariables.HttpClient.PostAsJsonAsync<Models.Shop_Seller>("Shop_Seller", shop_Seller);
                resultshop_seller.Wait();

                Session["Account"] = accounts;
                return RedirectToAction("Index");
            }


        }
        [AllowAnonymous]
        public ActionResult LoginSeller()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginSeller(Accounts accounts)
        {
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.PostAsJsonAsync<Accounts>("Accounts/Login", accounts).Result;

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Content("false");
            }
            else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Content("IsLock");
            }
            Accounts result = httpResponseMessage.Content.ReadAsAsync<Accounts>().Result;
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

            CreateWatchModel watchModel = new CreateWatchModel();
            return View(watchModel);
        }

        [HttpPost]
        public ActionResult CreateWatches(Watches request, FormCollection formCollection)
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
            request.id_Shop = (Session["Account"] as Accounts).Sellers.Shop_Seller.FirstOrDefault().id_Shop;
            var result = GlobalVariables.HttpClient.PostAsJsonAsync<Watches>("Watches", request);
            result.Wait();
            return RedirectToAction("Index");
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

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

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
            public IEnumerable<view_Watch> listwatches { get; set; }
            public IEnumerable<Categories> listcategories { get; set; }
            public CreateWatchModel()
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
                    listwatches = GlobalVariables.HttpClient.GetAsync("Watches").Result.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
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
    }
}