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
    public class view_AllOrderController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/view_AllOrder
        [Route("api/view_AllOrder/{id}")]
        [HttpGet]
        public IQueryable<view_AllOrder> Getview_AllOrder(int id)
        {
            return db.view_AllOrder.Where(x => x.id_Shop == id).OrderBy(x => x.Date_Create);
        }

        // GET: api/view_AllOrder/5
        [ResponseType(typeof(view_AllOrder))]
        public IHttpActionResult Getview_AllOrder(string id)
        {
            view_AllOrder view_AllOrder = db.view_AllOrder.Find(id);
            if (view_AllOrder == null)
            {
                return NotFound();
            }

            return Ok(view_AllOrder);
        }

        // PUT: api/view_AllOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putview_AllOrder(string id, view_AllOrder view_AllOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != view_AllOrder.Fullname)
            {
                return BadRequest();
            }

            db.Entry(view_AllOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!view_AllOrderExists(id))
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

        // POST: api/view_AllOrder
        [ResponseType(typeof(view_AllOrder))]
        public IHttpActionResult Postview_AllOrder(view_AllOrder view_AllOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_AllOrder.Add(view_AllOrder);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (view_AllOrderExists(view_AllOrder.Fullname))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = view_AllOrder.Fullname }, view_AllOrder);
        }

        // DELETE: api/view_AllOrder/5
        [ResponseType(typeof(view_AllOrder))]
        public IHttpActionResult Deleteview_AllOrder(string id)
        {
            view_AllOrder view_AllOrder = db.view_AllOrder.Find(id);
            if (view_AllOrder == null)
            {
                return NotFound();
            }

            db.view_AllOrder.Remove(view_AllOrder);
            db.SaveChanges();

            return Ok(view_AllOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_AllOrderExists(string id)
        {
            return db.view_AllOrder.Count(e => e.Fullname == id) > 0;
        }
    }
}