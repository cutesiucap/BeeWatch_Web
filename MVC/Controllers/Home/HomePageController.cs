using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("watches/listwatches/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }    

    }
}