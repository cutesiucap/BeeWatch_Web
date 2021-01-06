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
    public class SexController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Sex
        public IQueryable<view_Sex> GetSex()
        {
            return db.view_Sex;
        }

        // GET: api/Sex/5
        [ResponseType(typeof(Sex))]
        public IHttpActionResult GetSex(int id)
        {
            Sex sex = db.Sex.Find(id);
            if (sex == null)
            {
                return NotFound();
            }

            return Ok(sex);
        }

        // PUT: api/Sex/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSex(int id, Sex sex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sex.id)
            {
                return BadRequest();
            }

            db.Entry(sex).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexExists(id))
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

        // POST: api/Sex
        [ResponseType(typeof(Sex))]
        public IHttpActionResult PostSex(Sex sex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sex.Add(sex);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SexExists(sex.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sex.id }, sex);
        }

        // DELETE: api/Sex/5
        [ResponseType(typeof(Sex))]
        public IHttpActionResult DeleteSex(int id)
        {
            Sex sex = db.Sex.Find(id);
            if (sex == null)
            {
                return NotFound();
            }

            db.Sex.Remove(sex);
            db.SaveChanges();

            return Ok(sex);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SexExists(int id)
        {
            return db.Sex.Count(e => e.id == id) > 0;
        }
    }
}