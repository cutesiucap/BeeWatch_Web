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
    public class FirmsController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Firms
        public IQueryable<Firms> GetFirms()
        {
            return db.Firms;
        }

        // GET: api/Firms/5
        [ResponseType(typeof(Firms))]
        public IHttpActionResult GetFirms(int id)
        {
            Firms firms = db.Firms.Find(id);
            if (firms == null)
            {
                return NotFound();
            }

            return Ok(firms);
        }

        // PUT: api/Firms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFirms(int id, Firms firms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != firms.id)
            {
                return BadRequest();
            }

            db.Entry(firms).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmsExists(id))
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

        // POST: api/Firms
        [ResponseType(typeof(Firms))]
        public IHttpActionResult PostFirms(Firms firms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Firms.Add(firms);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = firms.id }, firms);
        }

        // DELETE: api/Firms/5
        [ResponseType(typeof(Firms))]
        public IHttpActionResult DeleteFirms(int id)
        {
            Firms firms = db.Firms.Find(id);
            if (firms == null)
            {
                return NotFound();
            }

            db.Firms.Remove(firms);
            db.SaveChanges();

            return Ok(firms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FirmsExists(int id)
        {
            return db.Firms.Count(e => e.id == id) > 0;
        }
    }
}