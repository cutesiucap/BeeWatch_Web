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
using API_Server.Models;

namespace API_Server.Controllers
{
    public class ShopsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Shops
        public IQueryable<Shops> GetShops()
        {
            return db.Shops;
        }

        // GET: api/Shops/5
        [ResponseType(typeof(Shops))]
        public IHttpActionResult GetShops(int id)
        {
            Shops shops = db.Shops.Find(id);
            if (shops == null)
            {
                return NotFound();
            }

            return Ok(shops);
        }

        // PUT: api/Shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShops(int id, Shops shops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shops.id)
            {
                return BadRequest();
            }

            db.Entry(shops).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopsExists(id))
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

        // POST: api/Shops
        [ResponseType(typeof(Shops))]
        public IHttpActionResult PostShops(Shops shops)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shops.Add(shops);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shops.id }, shops);
        }

        // DELETE: api/Shops/5
        [ResponseType(typeof(Shops))]
        public IHttpActionResult DeleteShops(int id)
        {
            Shops shops = db.Shops.Find(id);
            if (shops == null)
            {
                return NotFound();
            }

            db.Shops.Remove(shops);
            db.SaveChanges();

            return Ok(shops);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopsExists(int id)
        {
            return db.Shops.Count(e => e.id == id) > 0;
        }
    }
}