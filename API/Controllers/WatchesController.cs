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
    public class WatchesController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Watches
        public IQueryable<Watches> GetWatches()
        {
            return db.Watches;
        }

        // GET: api/Watches/5
        [ResponseType(typeof(Watches))]
        public IHttpActionResult GetWatches(int id)
        {
            Watches watches = db.Watches.Find(id);
            if (watches == null)
            {
                return NotFound();
            }

            return Ok(watches);
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

            return CreatedAtRoute("DefaultApi", new { id = watches.id }, watches);
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
    }
}