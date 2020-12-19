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
    public class Address_DistrictController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Address_District
        public IQueryable<view_District> GetAddress_District()
        {
            return db.view_District;
        }

        // GET: api/Address_District/5
        [ResponseType(typeof(Address_District))]
        public IHttpActionResult GetAddress_District(string id)
        {
            Address_District address_District = db.Address_District.Find(id);
            if (address_District == null)
            {
                return NotFound();
            }

            return Ok(address_District);
        }

        // PUT: api/Address_District/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress_District(string id, Address_District address_District)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address_District.id)
            {
                return BadRequest();
            }

            db.Entry(address_District).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Address_DistrictExists(id))
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

        // POST: api/Address_District
        [ResponseType(typeof(Address_District))]
        public IHttpActionResult PostAddress_District(Address_District address_District)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Address_District.Add(address_District);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Address_DistrictExists(address_District.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = address_District.id }, address_District);
        }

        // DELETE: api/Address_District/5
        [ResponseType(typeof(Address_District))]
        public IHttpActionResult DeleteAddress_District(string id)
        {
            Address_District address_District = db.Address_District.Find(id);
            if (address_District == null)
            {
                return NotFound();
            }

            db.Address_District.Remove(address_District);
            db.SaveChanges();

            return Ok(address_District);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Address_DistrictExists(string id)
        {
            return db.Address_District.Count(e => e.id == id) > 0;
        }
    }
}