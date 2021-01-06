using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_Local.Models;

namespace API_Local.Controllers
{
    public class OrdersController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Orders
        public IQueryable<view_Orders> GetOrders()
        {
            return db.view_Orders;
        }

        [Route("api/UpStatus/{id}")]
        [HttpPost]
        public IHttpActionResult UpStatus(int id)
        {
            var idParam = new SqlParameter("id", SqlDbType.Int);
            idParam.Value = id;
            db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_UpStatus] @id", idParam); 
            return Ok();
            
        }

        // GET: api/Orders/5
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult GetOrders(int id)
        {
            view_Orders orders = db.view_Orders.Where(x => x.id == id).FirstOrDefault();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders(int id, Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.id)
            {
                return BadRequest();
            }

            db.Entry(orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        [ResponseType(typeof(Orders))]
        public IHttpActionResult PostOrders(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(orders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orders.id }, orders);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Orders))]
        public IHttpActionResult DeleteOrders(int id)
        {
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orders);
            db.SaveChanges();

            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.id == id) > 0;
        }

        [Route("api/Orders/OrderofCustomer/{id:int}")]
        [HttpGet]
        [ResponseType(typeof(view_Orders))]
        public IHttpActionResult OrderofCustomer(int id)
        {
            view_Orders orders = db.view_Orders.Where(x => x.id_Accounts == id).FirstOrDefault();
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [Route("api/Orders/OrdersofShop/{id:int}")]
        public IQueryable<view_Orders> OrdersofShop(int id)
        {
            return db.view_Orders.Where(x => x.id_Shop == id);
        }
    }
}