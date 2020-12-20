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
    public class Watches_CategoriesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Watches_Categories
        public IQueryable<Watches_Categories> GetWatches_Categories()
        {
            return db.Watches_Categories;
        }

        // GET: api/Watches_Categories/5
        [ResponseType(typeof(Watches_Categories))]
        public IHttpActionResult GetWatches_Categories(int id)
        {
            Watches_Categories watches_Categories = db.Watches_Categories.Find(id);
            if (watches_Categories == null)
            {
                return NotFound();
            }

            return Ok(watches_Categories);
        }

        // PUT: api/Watches_Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWatches_Categories(int id, Watches_Categories watches_Categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != watches_Categories.id_Watch)
            {
                return BadRequest();
            }

            db.Entry(watches_Categories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Watches_CategoriesExists(id))
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

        // POST: api/Watches_Categories
        [ResponseType(typeof(Watches_Categories))]
        public IHttpActionResult PostWatches_Categories(Watches_Categories watches_Categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Watches_Categories.Add(watches_Categories);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Watches_CategoriesExists(watches_Categories.id_Watch))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = watches_Categories.id_Watch }, watches_Categories);
        }

        // DELETE: api/Watches_Categories/5
        [ResponseType(typeof(Watches_Categories))]
        public IHttpActionResult DeleteWatches_Categories(int id)
        {
            Watches_Categories watches_Categories = db.Watches_Categories.Find(id);
            if (watches_Categories == null)
            {
                return NotFound();
            }

            db.Watches_Categories.Remove(watches_Categories);
            db.SaveChanges();

            return Ok(watches_Categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Watches_CategoriesExists(int id)
        {
            return db.Watches_Categories.Count(e => e.id_Watch == id) > 0;
        }
    }
}