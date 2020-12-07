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
    public class BoughtsController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Boughts
        public IQueryable<Bought> GetBought()
        {
            return db.Bought;
        }

        // GET: api/Boughts/5
        [ResponseType(typeof(Bought))]
        public IHttpActionResult GetBought(int id)
        {
            Bought bought = db.Bought.Find(id);
            if (bought == null)
            {
                return NotFound();
            }

            return Ok(bought);
        }

        // PUT: api/Boughts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBought(int id, Bought bought)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bought.id)
            {
                return BadRequest();
            }

            db.Entry(bought).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoughtExists(id))
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

        // POST: api/Boughts
        [ResponseType(typeof(Bought))]
        public IHttpActionResult PostBought(Bought bought)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bought.Add(bought);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BoughtExists(bought.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bought.id }, bought);
        }

        // DELETE: api/Boughts/5
        [ResponseType(typeof(Bought))]
        public IHttpActionResult DeleteBought(int id)
        {
            Bought bought = db.Bought.Find(id);
            if (bought == null)
            {
                return NotFound();
            }

            db.Bought.Remove(bought);
            db.SaveChanges();

            return Ok(bought);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoughtExists(int id)
        {
            return db.Bought.Count(e => e.id == id) > 0;
        }
    }
}