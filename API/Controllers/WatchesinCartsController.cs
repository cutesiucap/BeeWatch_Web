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
    public class WatchesinCartsController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/WatchesinCarts
        public IQueryable<WatchesinCarts> GetWatchesinCarts()
        {
            return db.WatchesinCarts;
        }

        // GET: api/WatchesinCarts/5
        [ResponseType(typeof(WatchesinCarts))]
        public IHttpActionResult GetWatchesinCarts(int id)
        {
            WatchesinCarts watchesinCarts = db.WatchesinCarts.Find(id);
            if (watchesinCarts == null)
            {
                return NotFound();
            }

            return Ok(watchesinCarts);
        }

        // PUT: api/WatchesinCarts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWatchesinCarts(int id, WatchesinCarts watchesinCarts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != watchesinCarts.id_Carts)
            {
                return BadRequest();
            }

            db.Entry(watchesinCarts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchesinCartsExists(id))
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

        // POST: api/WatchesinCarts
        [ResponseType(typeof(WatchesinCarts))]
        public IHttpActionResult PostWatchesinCarts(WatchesinCarts watchesinCarts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WatchesinCarts.Add(watchesinCarts);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WatchesinCartsExists(watchesinCarts.id_Carts))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = watchesinCarts.id_Carts }, watchesinCarts);
        }

        // DELETE: api/WatchesinCarts/5
        [ResponseType(typeof(WatchesinCarts))]
        public IHttpActionResult DeleteWatchesinCarts(int id)
        {
            WatchesinCarts watchesinCarts = db.WatchesinCarts.Find(id);
            if (watchesinCarts == null)
            {
                return NotFound();
            }

            db.WatchesinCarts.Remove(watchesinCarts);
            db.SaveChanges();

            return Ok(watchesinCarts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WatchesinCartsExists(int id)
        {
            return db.WatchesinCarts.Count(e => e.id_Carts == id) > 0;
        }
    }
}