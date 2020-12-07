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
    public class CommoditiesController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Commodities
        public IQueryable<Commodities> GetCommodities()
        {
            return db.Commodities;
        }

        // GET: api/Commodities/5
        [ResponseType(typeof(Commodities))]
        public IHttpActionResult GetCommodities(int id)
        {
            Commodities commodities = db.Commodities.Find(id);
            if (commodities == null)
            {
                return NotFound();
            }

            return Ok(commodities);
        }

        // PUT: api/Commodities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommodities(int id, Commodities commodities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commodities.id)
            {
                return BadRequest();
            }

            db.Entry(commodities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommoditiesExists(id))
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

        // POST: api/Commodities
        [ResponseType(typeof(Commodities))]
        public IHttpActionResult PostCommodities(Commodities commodities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commodities.Add(commodities);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = commodities.id }, commodities);
        }

        // DELETE: api/Commodities/5
        [ResponseType(typeof(Commodities))]
        public IHttpActionResult DeleteCommodities(int id)
        {
            Commodities commodities = db.Commodities.Find(id);
            if (commodities == null)
            {
                return NotFound();
            }

            db.Commodities.Remove(commodities);
            db.SaveChanges();

            return Ok(commodities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommoditiesExists(int id)
        {
            return db.Commodities.Count(e => e.id == id) > 0;
        }
    }
}