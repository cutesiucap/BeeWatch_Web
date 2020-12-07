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
    public class Hot_TrendController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Hot_Trend
        public IQueryable<Hot_Trend> GetHot_Trend()
        {
            return db.Hot_Trend;
        }

        // GET: api/Hot_Trend/5
        [ResponseType(typeof(Hot_Trend))]
        public IHttpActionResult GetHot_Trend(int id)
        {
            Hot_Trend hot_Trend = db.Hot_Trend.Find(id);
            if (hot_Trend == null)
            {
                return NotFound();
            }

            return Ok(hot_Trend);
        }

        // PUT: api/Hot_Trend/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHot_Trend(int id, Hot_Trend hot_Trend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hot_Trend.id)
            {
                return BadRequest();
            }

            db.Entry(hot_Trend).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hot_TrendExists(id))
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

        // POST: api/Hot_Trend
        [ResponseType(typeof(Hot_Trend))]
        public IHttpActionResult PostHot_Trend(Hot_Trend hot_Trend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hot_Trend.Add(hot_Trend);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Hot_TrendExists(hot_Trend.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hot_Trend.id }, hot_Trend);
        }

        // DELETE: api/Hot_Trend/5
        [ResponseType(typeof(Hot_Trend))]
        public IHttpActionResult DeleteHot_Trend(int id)
        {
            Hot_Trend hot_Trend = db.Hot_Trend.Find(id);
            if (hot_Trend == null)
            {
                return NotFound();
            }

            db.Hot_Trend.Remove(hot_Trend);
            db.SaveChanges();

            return Ok(hot_Trend);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Hot_TrendExists(int id)
        {
            return db.Hot_Trend.Count(e => e.id == id) > 0;
        }
    }
}