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
using API.Models;

namespace API.Controllers
{
    public class Account_TypeController : ApiController
    {
        private BeeWatchDataBaseEntities db = new BeeWatchDataBaseEntities();

        // GET: api/Account_Type
        public IQueryable<Account_Type> GetAccount_Type()
        {
            return db.Account_Type;
        }

        // GET: api/Account_Type/5
        [ResponseType(typeof(Account_Type))]
        public IHttpActionResult GetAccount_Type(int id)
        {
            Account_Type account_Type = db.Account_Type.Find(id);
            if (account_Type == null)
            {
                return NotFound();
            }

            return Ok(account_Type);
        }

        // PUT: api/Account_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccount_Type(int id, Account_Type account_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account_Type.id)
            {
                return BadRequest();
            }

            db.Entry(account_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Account_TypeExists(id))
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

        // POST: api/Account_Type
        [ResponseType(typeof(Account_Type))]
        public IHttpActionResult PostAccount_Type(Account_Type account_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Account_Type.Add(account_Type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = account_Type.id }, account_Type);
        }

        // DELETE: api/Account_Type/5
        [ResponseType(typeof(Account_Type))]
        public IHttpActionResult DeleteAccount_Type(int id)
        {
            Account_Type account_Type = db.Account_Type.Find(id);
            if (account_Type == null)
            {
                return NotFound();
            }

            db.Account_Type.Remove(account_Type);
            db.SaveChanges();

            return Ok(account_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Account_TypeExists(int id)
        {
            return db.Account_Type.Count(e => e.id == id) > 0;
        }
    }
}