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
    public class DiscountsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Discounts
        public IQueryable<Discounts> GetDiscounts()
        {
            return db.Discounts;
        }

        // GET: api/Discounts/5
        [ResponseType(typeof(Discounts))]
        public IHttpActionResult GetDiscounts(int id)
        {
            Discounts discounts = db.Discounts.Find(id);
            if (discounts == null)
            {
                return NotFound();
            }

            return Ok(discounts);
        }

        // PUT: api/Discounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscounts(int id, Discounts discounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discounts.id)
            {
                return BadRequest();
            }

            db.Entry(discounts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountsExists(id))
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

        // POST: api/Discounts
        [ResponseType(typeof(Discounts))]
        public IHttpActionResult PostDiscounts(Discounts discounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discounts.Add(discounts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = discounts.id }, discounts);
        }

        // DELETE: api/Discounts/5
        [ResponseType(typeof(Discounts))]
        public IHttpActionResult DeleteDiscounts(int id)
        {
            Discounts discounts = db.Discounts.Find(id);
            if (discounts == null)
            {
                return NotFound();
            }

            db.Discounts.Remove(discounts);
            db.SaveChanges();

            return Ok(discounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscountsExists(int id)
        {
            return db.Discounts.Count(e => e.id == id) > 0;
        }

        [Route("api/Discounts/EnterCODE")]
        [ResponseType(typeof(Discounts))]
        [HttpPost]
        public IHttpActionResult EnterCODE(Discounts discount)
        {
            Discounts discounts = db.Discounts.Where(x => x.Code == discount.Code).FirstOrDefault();
            if(discounts == null)
            {
                return Ok(new Discounts()
                {
                    Detail = "Không tồn tại Mã Giảm giá này!!",
                    Status = false
                });
            }
            Discounts newdiscount;
            if (discounts.Status == false)
            {
                newdiscount = new Discounts()
                {
                    id = discounts.id,
                    Detail = "CODE này đã ngưng hoạt động!!",
                    Status = false,
                };
                return Ok(newdiscount);
            }
            if (discounts.DateFrom > DateTime.Now || discounts.DateTo < DateTime.Now)
            {
                newdiscount = new Discounts()
                {
                    id = discounts.id,
                    Detail = "CODE này đã hết hạn Sử dụng!!",
                    Status = false,
                };
                return Ok(newdiscount);
            }
            if (discounts.TypeDiscounts.Min_Bill > discount.TypeDiscounts.Min_Bill)
            {
                newdiscount = new Discounts()
                {
                    id = discounts.id,
                    Detail = "Bạn chưa đạt giới hạn Bill " + discounts.TypeDiscounts.Min_Bill.ToString() + "!!",
                    Status = false,
                };
                return Ok(newdiscount);
            }
            newdiscount = new Discounts()
            {
                id = discounts.id,
                Detail = discounts.Detail,
                Status = true,
            };
            double? Value = discount.TypeDiscounts.Min_Bill - (discount.TypeDiscounts.Min_Bill * discounts.Value___) / 100 - discounts.Value___1;
            if (Value > discounts.TypeDiscounts.Max_Discount)
            {
                Value = discounts.TypeDiscounts.Max_Discount;
            }
            newdiscount.Value___ = Value;
            return Ok(newdiscount);
        }
    }
}