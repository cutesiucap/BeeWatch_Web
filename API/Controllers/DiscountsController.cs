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
using API.Models;

namespace API.Controllers
{
    public class DiscountsController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

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
    }
}