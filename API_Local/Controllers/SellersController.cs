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
    public class SellersController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Sellers
        public IQueryable<Sellers> GetSellers()
        {
            return db.Sellers;
        }

        // GET: api/Sellers/5
        [ResponseType(typeof(Sellers))]
        public IHttpActionResult GetSellers(int id)
        {
            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return NotFound();
            }

            return Ok(sellers);
        }

        // PUT: api/Sellers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSellers(int id, Sellers sellers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sellers.id)
            {
                return BadRequest();
            }

            db.Entry(sellers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellersExists(id))
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

        // POST: api/Sellers
        [ResponseType(typeof(Sellers))]
        public IHttpActionResult PostSellers(Sellers sellers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sellers.Add(sellers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SellersExists(sellers.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sellers.id }, sellers);
        }

        // DELETE: api/Sellers/5
        [ResponseType(typeof(Sellers))]
        public IHttpActionResult DeleteSellers(int id)
        {
            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return NotFound();
            }

            db.Sellers.Remove(sellers);
            db.SaveChanges();

            return Ok(sellers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SellersExists(int id)
        {
            return db.Sellers.Count(e => e.id == id) > 0;
        }
    }
}