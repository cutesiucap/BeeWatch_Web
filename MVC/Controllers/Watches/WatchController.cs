using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Watches
{
    public class WatchController : Controller
    {
        // GET: Watch
        public ActionResult Index(int id)
        {
            ViewModels.WatchDetailViewModel watchDetailViewModel = new ViewModels.WatchDetailViewModel();
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("Watches/" + id).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                watchDetailViewModel = httpResponseMessage.Content.ReadAsAsync<ViewModels.WatchDetailViewModel>().Result;
            }
            return View(watchDetailViewModel);
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}