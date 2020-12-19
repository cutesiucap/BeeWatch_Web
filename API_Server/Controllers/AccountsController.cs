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
    public class AccountsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Accounts
        public IQueryable<view_Account> GetAccounts()
        {
            return db.view_Account;
        }

        // GET: api/Accounts/5
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult GetAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccounts(int id, Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accounts.id)
            {
                return BadRequest();
            }

            db.Entry(accounts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(id))
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

        // POST: api/Accounts
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult PostAccounts(Accounts accounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Accounts.Where(x => x.Username == accounts.Username).FirstOrDefault() != null)
            {
                return BadRequest("Username đã có");
            }

            db.Accounts.Add(accounts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accounts.id }, accounts);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult DeleteAccounts(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(accounts);
            db.SaveChanges();

            return Ok(accounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountsExists(int id)
        {
            return db.Accounts.Count(e => e.id == id) > 0;
        }
        [Route("api/Accounts/Login")]
        [HttpPost]
        [ResponseType(typeof(Accounts))]
        public IHttpActionResult Login(Accounts accounts)
        {
            Accounts result = db.Accounts.Where(x => x.Username == accounts.Username && x.Password == accounts.Password).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else if (result.IsLock == true)
            {
                return BadRequest();
            }
            else return Ok(result);
        }
        [Route("api/Accounts/Register")]
        [HttpPost]
        [ResponseType(typeof(view_Account))]
        public HttpResponseMessage Register(Accounts accounts)
        {
            int id = 0;
            try
            {
                id = db.sp_InsertAccount(accounts.Username, accounts.Password, "", accounts.Email,
                    accounts.Sex, accounts.Address.FirstOrDefault().id_Province, accounts.Address.FirstOrDefault().id_District,
                    accounts.Address.FirstOrDefault().AddressDetail);
            }
            catch(Exception e)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest,e.Message);
            }

            view_Account result = db.view_Account.Find(id);
           
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }
        [HttpPost]
        public IHttpActionResult Logout(int id)
        {
            return BadRequest("sdfghjkl");
        }
    }
}