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
using API_Local.Models;

namespace API_Local.Controllers
{
    public class view_WatchesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Watches
        public IQueryable<view_Watches> GetWatches()
        {
            return db.view_Watches;
        }



        // GET: api/Watches/5
        [ResponseType(typeof(view_Watches))]
        public async Task<IHttpActionResult> GetWatches(int id)
        {
            view_Watches viewWatches = db.view_Watches.Where(x => x.id == id).FirstOrDefault();
            if (viewWatches == null)
            {
                return NotFound();
            }

            return Ok(viewWatches);
        }

        [Route("api/view_Watches/listwatches/{page:int}")]
        [HttpGet]
        public IQueryable<view_Watches> GetListWatches(int page)
        {

            int pageSize = 20;
            int totalRecord;
            int totalPage;
            var query = new List<view_Watches>();
            totalRecord = db.view_Watches.Count();
            totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            return db.view_Watches.OrderBy(a => a.id).Skip(((page - 1) * pageSize)).Take(pageSize);

        }
    }
}