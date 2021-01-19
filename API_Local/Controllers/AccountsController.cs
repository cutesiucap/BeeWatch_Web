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

            if (db.Accounts.Where(x => x.Email == accounts.Email).FirstOrDefault() != null)
            {
                return BadRequest("Gmail đã được sử dụng");
            }    

            accounts.Address = null;
            accounts.Phone = null;
            accounts.Sellers = null;
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
            view_Account result = db.view_Account.Where(x => x.Username == accounts.Username && x.Password == accounts.Password).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            else if (result.IsLock == true)
            {
                return BadRequest();
            }
            Accounts new_accounts = new Accounts()
            {
                id = result.id,
                Fullname = result.Fullname,
                id_Account_Type = result.id_Account_Type,
                Account_Type = new Account_Type()
                {
                    id = result.id_Account_Type,
                    Authoriza = new List<Authoriza>(),
                }
            };

            foreach (var item in db.Authoriza.Where(x => x.id_Account_Type == result.id_Account_Type))
            {
                new_accounts.Account_Type.Authoriza.Add(new Authoriza()
                {
                    id = item.id,
                    id_Account_Type = item.id_Account_Type,
                    id_Action = item.id_Action,
                    Action = new Models.Action() { id = item.id_Action, Name = db.view_Action.Where(x => x.id == item.id_Action).FirstOrDefault().Name }
                });
            };

            if (new_accounts.id_Account_Type == 2)
            {
                new_accounts.Sellers = new Sellers()
                {
                    Shop_Seller = new List<Shop_Seller>(),
                };
                foreach ( var item in db.Shop_Seller.Where(x => x.id_Seller == new_accounts.id))
                {
                    new_accounts.Sellers.Shop_Seller.Add(new Shop_Seller()
                    {
                        id_Shop = item.id_Shop,
                        id_Seller = item.id_Seller,
                        IsCheck = item.IsCheck,
                        IsLock = item.IsLock,
                    });
                }
            }
            return Ok(new_accounts);
        }

        [HttpPost]
        public IHttpActionResult Logout(int id)
        {
            var idParam = new SqlParameter("id", SqlDbType.Int);
            idParam.Value = id;
            db.Database.ExecuteSqlCommand("EXEC [dbo].[sp_LogOutAcc] @id", idParam);
            return Ok();
        }
    }
}