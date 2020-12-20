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
    public class view_OrdersController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/view_Orders
        public IQueryable<view_Orders> Getview_Orders()
        {
            return db.view_Orders;
        }

        // GET: api/view_Orders/5
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult Getview_Orders(int id)
        {
            view_Orders view_Orders = db.view_Orders.Find(id);
            if (view_Orders == null)
            {
                return NotFound();
            }

            return Ok(view_Orders);
        }

        // POST: api/view_Orders
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult Postview_Orders(view_Orders view_Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.view_Orders.Add(view_Orders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = view_Orders.id }, view_Orders);
        }

        // DELETE: api/view_Orders/5
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult Deleteview_Orders(int id)
        {
            view_Orders view_Orders = db.view_Orders.Find(id);
            if (view_Orders == null)
            {
                return NotFound();
            }

            db.view_Orders.Remove(view_Orders);
            db.SaveChanges();

            return Ok(view_Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool view_OrdersExists(int id)
        {
            return db.view_Orders.Count(e => e.id == id) > 0;
        }
    }
}