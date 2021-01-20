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
    public class view_AllOrderDetailController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/view_AllOrderDetail
        public IQueryable<view_AllOrderDetail> Getview_AllOrderDetail()
        {
            return db.view_AllOrderDetail;
        }

        [Route("api/OrderDetail/Getview_OrderDetail/{id}")]
        [ResponseType(typeof(IEnumerable<view_AllOrderDetail>))]
        // GET: api/view_AllOrderDetail
        public IQueryable<view_AllOrderDetail> Getview_AllOrderDetail(int id)
        {
            return db.view_AllOrderDetail.AsNoTracking().Where(x=>x.id_Order == id);
        }

        // GET: api/view_AllOrderDetail/5
        [ResponseType(typeof(view_AllOrderDetail))]
        public IHttpActionResult Getview_AllOrderDetail(string id)
        {
            view_AllOrderDetail view_AllOrderDetail = db.view_AllOrderDetail.Find(id);
            if (view_AllOrderDetail == null)
            {
                return NotFound();
            }

            return Ok(view_AllOrderDetail);
        }

        // PUT: api/view_AllOrderDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putview_AllOrderDetail(string id, view_AllOrderDetail view_AllOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != view_AllOrderDetail.Fullname)
            {
                return BadRequest();
            }

            db.Entry(view_AllOrderDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!view_AllOrderDetailExists(id))
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

        // POST: api/view_AllOrderDetail
        [ResponseType(typeof(view_AllOrderDetail))]
        public IHttpActionResult Postview_AllOrderDetail(view_AllOrderDetail view_AllOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_AllOrderDetail.Add(view_AllOrderDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (view_AllOrderDetailExists(view_AllOrderDetail.Fullname))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = view_AllOrderDetail.Fullname }, view_AllOrderDetail);
        }

        // DELETE: api/view_AllOrderDetail/5
        [ResponseType(typeof(view_AllOrderDetail))]
        public IHttpActionResult Deleteview_AllOrderDetail(string id)
        {
            view_AllOrderDetail view_AllOrderDetail = db.view_AllOrderDetail.Find(id);
            if (view_AllOrderDetail == null)
            {
                return NotFound();
            }

            db.view_AllOrderDetail.Remove(view_AllOrderDetail);
            db.SaveChanges();

            return Ok(view_AllOrderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_AllOrderDetailExists(string id)
        {
            return db.view_AllOrderDetail.Count(e => e.Fullname == id) > 0;
        }
    }
}