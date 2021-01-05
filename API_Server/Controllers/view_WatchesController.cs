using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_Server.Models;

namespace API_Server.Controllers
{
    public class view_WatchesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/view_Watches
        public IQueryable<view_WatchDetails> GetView_Watches()
        {
            return db.view_WatchDetails;
        }



        // GET: api/view_Watches/5
        [ResponseType(typeof(view_WatchDetails))]
        public async Task<IHttpActionResult> Getview_Watches(int id)
        {
            view_WatchDetails viewWatches = db.view_WatchDetails.Where(x => x.id == id).FirstOrDefault();
            if (viewWatches == null)
            {
                return NotFound();
            }

            return Ok(viewWatches);
        }

        [Route("api/view_Watches/listwatches/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatches(int page)
        {

            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderBy(a => a.id).Skip(((page - 1) * pageSize)).Take(pageSize);
        }
        [Route("api/view_Watches/ListWatchesSortByRate/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatchesSortByRate(int page)
        {
            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderByDescending(a => a.Rate).Skip(((page - 1) * pageSize)).Take(pageSize);
        }
        [Route("api/view_Watches/ListWatchesSortByDate/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatchesSortByDate(int page)
        {
            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderBy(a => a.Date_Create).Skip(((page - 1) * pageSize)).Take(pageSize);
        }
        /*[Route("api/view_Watches/ListWatchesSortByLuotMua/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatchesSortByRateLuotMua(int page)
        {
            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderBy(a => a.).Skip(((page - 1) * pageSize)).Take(pageSize);
        }*/
        [Route("api/view_Watches/ListWatchesSortByPriceUp/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatchesSortByRatePriceUp(int page)
        {
            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderBy(a => a.Price).Skip(((page - 1) * pageSize)).Take(pageSize);
        }
        [Route("api/view_Watches/ListWatchesSortByPriceDown/{page:int}")]
        [HttpGet]
        public IQueryable<view_WatchDetails> GetListWatchesSortByRatePriceDown(int page)
        {
            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_WatchDetails>();
            totalRecord = db.view_WatchDetails.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_WatchDetails.OrderByDescending(a => a.Price).Skip(((page - 1) * pageSize)).Take(pageSize);
        }
    }
}