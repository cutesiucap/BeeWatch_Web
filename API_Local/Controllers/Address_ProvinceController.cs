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
    public class Address_ProvinceController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Address_Province
        public IQueryable<view_Province> GetAddress_Province()
        {
            return db.view_Province;
        }

        // GET: api/Address_Province/5
        [ResponseType(typeof(Address_Province))]
        public IHttpActionResult GetAddress_Province(string id)
        {
            Address_Province address_Province = db.Address_Province.Find(id);
            if (address_Province == null)
            {
                return NotFound();
            }

            return Ok(address_Province);
        }

        // PUT: api/Address_Province/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress_Province(string id, Address_Province address_Province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address_Province.id)
            {
                return BadRequest();
            }

            db.Entry(address_Province).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Address_ProvinceExists(id))
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

        // POST: api/Address_Province
        [ResponseType(typeof(Address_Province))]
        public IHttpActionResult PostAddress_Province(Address_Province address_Province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Address_Province.Add(address_Province);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Address_ProvinceExists(address_Province.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = address_Province.id }, address_Province);
        }

        // DELETE: api/Address_Province/5
        [ResponseType(typeof(Address_Province))]
        public IHttpActionResult DeleteAddress_Province(string id)
        {
            Address_Province address_Province = db.Address_Province.Find(id);
            if (address_Province == null)
            {
                return NotFound();
            }

            db.Address_Province.Remove(address_Province);
            db.SaveChanges();

            return Ok(address_Province);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Address_ProvinceExists(string id)
        {
            return db.Address_Province.Count(e => e.id == id) > 0;
        }
    }
}