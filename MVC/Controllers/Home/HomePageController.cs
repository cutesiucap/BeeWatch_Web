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
            IEnumerable<Watches> watches;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/listwatches/" + id).Result;
            watches = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Watches>>().Result;
            return View(watches);
        }

        

    }
}