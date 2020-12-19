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
    public class Shop_SellerController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Shop_Seller
        public IQueryable<Shop_Seller> GetShop_Seller()
        {
            return db.Shop_Seller;
        }

        // GET: api/Shop_Seller/5
        [ResponseType(typeof(Shop_Seller))]
        public IHttpActionResult GetShop_Seller(int id)
        {
            Shop_Seller shop_Seller = db.Shop_Seller.Find(id);
            if (shop_Seller == null)
            {
                return NotFound();
            }

            return Ok(shop_Seller);
        }

        // PUT: api/Shop_Seller/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShop_Seller(int id, Shop_Seller shop_Seller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shop_Seller.id_Shop)
            {
                return BadRequest();
            }

            db.Entry(shop_Seller).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Shop_SellerExists(id))
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

        // POST: api/Shop_Seller
        [ResponseType(typeof(Shop_Seller))]
        public IHttpActionResult PostShop_Seller(Shop_Seller shop_Seller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shop_Seller.Add(shop_Seller);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Shop_SellerExists(shop_Seller.id_Shop))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shop_Seller.id_Shop }, shop_Seller);
        }

        // DELETE: api/Shop_Seller/5
        [ResponseType(typeof(Shop_Seller))]
        public IHttpActionResult DeleteShop_Seller(int id)
        {
            Shop_Seller shop_Seller = db.Shop_Seller.Find(id);
            if (shop_Seller == null)
            {
                return NotFound();
            }

            db.Shop_Seller.Remove(shop_Seller);
            db.SaveChanges();

            return Ok(shop_Seller);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Shop_SellerExists(int id)
        {
            return db.Shop_Seller.Count(e => e.id_Shop == id) > 0;
        }
    }
}