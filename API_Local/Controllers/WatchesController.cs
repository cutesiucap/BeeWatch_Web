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
        [ResponseType(typeof(ViewModels.WatchDetailViewModel))]
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
            watchDetailViewModel.watch.Luotmua = watches.LuotMua ?? 0;
            watchDetailViewModel.watch.Count = watches.Count;
            watchDetailViewModel.watch.Luotdanhgia = db.OrderDetails.Where(x => x.Rate != null && x.Status == 3).Count();
            watchDetailViewModel.watch.Detail = db.Watches.Find(id).Information ?? "Chưa có Thông tin gì về sản phẩm này";
            watchDetailViewModel.watch.Url_Image = watches.Url_Image ?? "https://cdn.shopify.com/s/files/1/1081/2826/products/anicorn-redundant-watch-moma-04_960x.jpg?v=1581349580";

            Models.Shops shops = db.Shops.Find(watches.id_Shop);
            watchDetailViewModel.shop = new ViewModels.Shop()
            {
                id = shops.id ,
                Name = shops.Name,
                UrlAvatar = shops.UrlAvatar,
                Rate = db.view_WatchDetails.AsNoTracking().Where(x => x.id_Shop == shops.id && x.Rate != null).Average(x => x.Rate) ?? 0,
                Luotdanhgia = db.OrderDetails.AsNoTracking().Where(x => x.Rate != null && x.Status == 3).Count(),
                Daban = db.OrderDetails.AsNoTracking().Where(x => x.Status == 3).Sum(x => x.Count) ?? 0,
                Dangkinhdoanh = shops.Watches.Where(x => x.IsLock == false).Count(),
                Thoigiantao = shops.Sellers.Accounts.Date_Create,
                Khachhang = db.Orders.AsNoTracking().Where(x => x.Status != 4).GroupBy(x=>x.id_Accounts).Count(),
                BestRate = db.view_WatchDetails.AsNoTracking().Where(x => x.id_Shop == shops.id).Max(x=>x.Rate) ?? 0,
                Master_Name = shops.Sellers.Accounts.Fullname,
                id_Master = shops.Sellers.id,
                Phone = shops.Sellers.Accounts.Phone.FirstOrDefault().Phone1 ?? "-",
                Gmail = shops.Sellers.Accounts.Email ?? "-",
            };

            var image = db.Image.Where(x => x.id_Watches == id);
            watchDetailViewModel.Images = new List<Image>();
            foreach(var i in image)
            {
                Models.Image newimage = new Image();
                newimage.id = i.id;
                newimage.id_Watches = i.id_Watches;
                newimage.Url_Image = i.Url_Image;
                watchDetailViewModel.Images.Add(newimage);
            };
            watchDetailViewModel.danhgias = new List<ViewModels.Danhgia>();
            foreach(var item in db.OrderDetails.AsNoTracking().Where(x=>x.id_Watches == id && x.Rate != null))
            {
                watchDetailViewModel.danhgias.Add(new ViewModels.Danhgia()
                {
                    id_Account = item.Orders.id_Accounts,
                    id_Watch = item.id_Watches,
                    Rate = item.Rate,
                });
            };

            watchDetailViewModel.Gianhangcungban = db.view_WatchDetails.AsNoTracking().Where(x => x.id_Shop == shops.id).OrderBy(x => x.LuotMua).Skip(0).Take(20);
            watchDetailViewModel.Sanphamtuongtu = db.view_WatchDetails.AsNoTracking().Where(x => (x.id_Firms == watches.id_Firms || x.Name_Categories.IndexOf(watches.Name_Categories) >= 0 || x.id_Sex == watches.id_Sex) && x.id_Shop != watches.id_Shop).OrderBy(x => x.LuotMua).Skip(0).Take(20);

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