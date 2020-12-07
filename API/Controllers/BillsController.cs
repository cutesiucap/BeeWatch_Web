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
    public class BillsController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Bills
        public IQueryable<Bills> GetBills()
        {
            return db.Bills;
        }

        // GET: api/Bills/5
        [ResponseType(typeof(Bills))]
        public IHttpActionResult GetBills(int id)
        {
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return NotFound();
            }

            return Ok(bills);
        }

        // PUT: api/Bills/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBills(int id, Bills bills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bills.id)
            {
                return BadRequest();
            }

            db.Entry(bills).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillsExists(id))
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

        // POST: api/Bills
        [ResponseType(typeof(Bills))]
        public IHttpActionResult PostBills(Bills bills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bills.Add(bills);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bills.id }, bills);
        }

        // DELETE: api/Bills/5
        [ResponseType(typeof(Bills))]
        public IHttpActionResult DeleteBills(int id)
        {
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return NotFound();
            }

            db.Bills.Remove(bills);
            db.SaveChanges();

            return Ok(bills);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BillsExists(int id)
        {
            return db.Bills.Count(e => e.id == id) > 0;
        }
    }
}