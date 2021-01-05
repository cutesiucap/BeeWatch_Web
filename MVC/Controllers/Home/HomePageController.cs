using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage

        public ActionResult Page(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/listwatches/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
        public ActionResult PageSortByDate(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/ListWatchesSortByDate/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
        public ActionResult PageSortByPriceUp(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/ListWatchesSortByPriceUp/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
        public ActionResult PageSortByPriceDown(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/ListWatchesSortByPriceDown/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
        public ActionResult PageSortByRate(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/ListWatchesSortByRate/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
        public ActionResult PageSortByLuotMua(int id)
        {
            IEnumerable<view_Watch> view_watch;
            HttpResponseMessage httpResponseMessage = GlobalVariables.HttpClient.GetAsync("view_Watches/ListWatchesSortByLuotMua/" + id).Result;
            view_watch = httpResponseMessage.Content.ReadAsAsync<IEnumerable<view_Watch>>().Result;
            return View(view_watch);
        }
    }
}