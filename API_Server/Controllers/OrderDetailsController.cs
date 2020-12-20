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
    public class OrderDetailsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/OrderDetails
        public IQueryable<view_OrderDetail> GetOrderDetails()
        {
            return db.view_OrderDetail;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(view_OrderDetail))]
        public IHttpActionResult GetOrderDetails(int id)
        {
            view_OrderDetail orderDetails = db.view_OrderDetail.Where(x => x.idAccount == id).FirstOrDefault();
            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderDetails(int id, OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetails.id_Order)
            {
                return BadRequest();
            }

            db.Entry(orderDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
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

        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetails))]
        public IHttpActionResult PostOrderDetails(OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderDetails);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailsExists(orderDetails.id_Order))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderDetails.id_Order }, orderDetails);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetails))]
        public IHttpActionResult DeleteOrderDetails(int id)
        {
            OrderDetails orderDetails = db.OrderDetails.Find(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderDetails);
            db.SaveChanges();

            return Ok(orderDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailsExists(int id)
        {
            return db.OrderDetails.Count(e => e.id_Order == id) > 0;
        }
    }
}