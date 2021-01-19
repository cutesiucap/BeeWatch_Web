using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_Local.Models;

namespace API_Local.Controllers
{
    public class WatchesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Watches
        public IQueryable<view_WatchDetails> GetWatches()
        {
            return db.view_WatchDetails;
        }

        // GET: api/Watches/5
        [ResponseType(typeof(Watches))]
        public IHttpActionResult GetWatches(int id)
        {
            Models.view_WatchDetails watches = db.view_WatchDetails.Where(x => x.id == id).FirstOrDefault();
            if (watches == null)
            {
                return NotFound();
            }
            ViewModels.WatchDetailViewModel watchDetailViewModel = new ViewModels.WatchDetailViewModel();
            watchDetailViewModel.watch = new ViewModels.Watch();
            watchDetailViewModel.watch.id = watches.id;
            watchDetailViewModel.watch.Name = watches.Name;
            watchDetailViewModel.watch.Price = watches.Price;
            watchDetailViewModel.watch.Rate = watches.Rate;
            watchDetailViewModel.watch.Luotmua = watches.LuotMua;
            watchDetailViewModel.watch.Count = watches.Count;
            watchDetailViewModel.watch.Luotdanhgia = db.OrderDetails.Where(x => x.Rate != null && x.Status == 3).Count();
            watchDetailViewModel.watch.Detail = db.Watches.Find(id).Information;

            Models.Shops shops = db.Shops.Find(watches.id_Shop);
            watchDetailViewModel.shop = new ViewModels.Shop()
            {
                id = shops.id,
                Name = shops.Name,
                UrlAvatar = shops.UrlAvatar,
                Rate = db.view_WatchDetails.Where(x => x.id_Shop == shops.id && x.Rate != null).Average(x => x.Rate),
                Luotdanhgia = db.OrderDetails.Where(x => x.Rate != null && x.Status == 3).Count(),
                Daban = db.OrderDetails.Where(x => x.Status == 3).Sum(x => x.Count),
                Dangkinhdoanh = shops.Watches.Where(x => x.IsLock == false).Count(),
                Thoigiantao = shops.Sellers.Accounts.Date_Create,
                Khachhang = db.Orders.Where(x => x.Status != 4).GroupBy(x=>x.id_Accounts).Count(),
                BestRate = db.view_WatchDetails.Where(x => x.id_Shop == shops.id).Max(x=>x.Rate),
                Master_Name = shops.Sellers.Accounts.Fullname,
                id_Master = shops.Sellers.id,
                Phone = shops.Sellers.Accounts.Phone.FirstOrDefault().Phone1,
                Gmail = shops.Sellers.Accounts.Email,
            };

            foreach(var item in db.OrderDetails.Where(x=>x.id_Watches == id && x.Rate != null))
            {
                watchDetailViewModel.danhgias.Add(new ViewModels.Danhgia()
                {
                    id_Account = item.Orders.id_Accounts,
                    id_Watch = item.id_Watches,
                    Rate = item.Rate,
                });
            };

            watchDetailViewModel.Gianhangcungban = db.view_WatchDetails.Where(x => x.id_Shop == shops.id).OrderBy(x => x.LuotMua).Skip(0).Take(20);
            watchDetailViewModel.Sanphamtuongtu = db.view_WatchDetails.Where(x => x.id_Firms == watchDetailViewModel.watch.id || x.Name_Categories.IndexOf(db.view_WatchDetails.Where(y => y.id == watchDetailViewModel.watch.id).FirstOrDefault().Name_Categories) >= 0).OrderBy(x => x.LuotMua).Skip(0).Take(20);

            return Ok(watchDetailViewModel);
        }

        [Route("api/GetListWatch/{id}")]
        public IQueryable<view_WatchDetails> GetListWatch(int id)
        {
            return db.view_WatchDetails.Where(x => x.id_Shop == id);
        }

        // PUT: api/Watches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWatches(int id, Watches watches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != watches.id)
            {
                return BadRequest();
            }

            db.Entry(watches).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Watches
        [ResponseType(typeof(Watches))]
        public IHttpActionResult PostWatches(Watches watches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Watches.Add(watches);
            db.SaveChanges();

            return Ok(watches.id);
        }

        // DELETE: api/Watches/5
        [ResponseType(typeof(Watches))]
        public IHttpActionResult DeleteWatches(int id)
        {
            Watches watches = db.Watches.Find(id);
            if (watches == null)
            {
                return NotFound();
            }

            db.Watches.Remove(watches);
            db.SaveChanges();

            return Ok(watches);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WatchesExists(int id)
        {
            return db.Watches.Count(e => e.id == id) > 0;
        }

        [Route("api/Watches/SearchName")]
        [HttpGet]
        // GET: api/Watches
        public IQueryable<view_WatchDetails> Search([FromUri] string name)
        {
            fn_SearchWatch_Result watches = db.fn_SearchWatch(name).FirstOrDefault();
            return db.view_WatchDetails;
        }

    }
}