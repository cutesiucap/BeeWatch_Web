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
    public class view_CartDetailsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/view_CartDetails/5
        [ResponseType(typeof(view_CartDetails))]
        public IHttpActionResult Getview_CartDetails(int id)
        {
            view_CartDetails view_CartDetails = db.view_CartDetails.Find(id);
            if (view_CartDetails == null)
            {
                return NotFound();
            }

            return Ok(view_CartDetails);
        }

        // PUT: api/view_CartDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putview_CartDetails(int id, view_CartDetails view_CartDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != view_CartDetails.id_Cart)
            {
                return BadRequest();
            }

            db.Entry(view_CartDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!view_CartDetailsExists(id))
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

        // POST: api/view_CartDetails
        [ResponseType(typeof(view_CartDetails))]
        public IHttpActionResult Postview_CartDetails(view_CartDetails view_CartDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_CartDetails.Add(view_CartDetails);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (view_CartDetailsExists(view_CartDetails.id_Cart))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = view_CartDetails.id_Cart }, view_CartDetails);
        }

        // DELETE: api/view_CartDetails/5
        [ResponseType(typeof(view_CartDetails))]
        public IHttpActionResult Deleteview_CartDetails(int id)
        {
            view_CartDetails view_CartDetails = db.view_CartDetails.Find(id);
            if (view_CartDetails == null)
            {
                return NotFound();
            }

            db.view_CartDetails.Remove(view_CartDetails);
            db.SaveChanges();

            return Ok(view_CartDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_CartDetailsExists(int id)
        {
            return db.view_CartDetails.Count(e => e.id_Cart == id) > 0;
        }
    }
}