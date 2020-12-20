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
    public class AuthorizasController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Authorizas
        public IQueryable<Authoriza> GetAuthoriza()
        {
            return db.Authoriza;
        }

        [Route("api/Authorizas/Account_Type_By_id_Account/{id}")]
        [HttpGet]
        public IQueryable<Authoriza> Authoriza_By_id_AccountType(int id)
        {
            return db.Authoriza.Where(x => x.id_Account_Type == id);
        }

        // GET: api/Authorizas/5
        [ResponseType(typeof(Authoriza))]
        public IHttpActionResult GetAuthoriza(int id)
        {
            Authoriza authoriza = db.Authoriza.Find(id);
            if (authoriza == null)
            {
                return NotFound();
            }

            return Ok(authoriza);
        }

        // PUT: api/Authorizas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthoriza(int id, Authoriza authoriza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authoriza.id)
            {
                return BadRequest();
            }

            db.Entry(authoriza).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizaExists(id))
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

        // POST: api/Authorizas
        [ResponseType(typeof(Authoriza))]
        public IHttpActionResult PostAuthoriza(Authoriza authoriza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authoriza.Add(authoriza);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = authoriza.id }, authoriza);
        }

        // DELETE: api/Authorizas/5
        [ResponseType(typeof(Authoriza))]
        public IHttpActionResult DeleteAuthoriza(int id)
        {
            Authoriza authoriza = db.Authoriza.Find(id);
            if (authoriza == null)
            {
                return NotFound();
            }

            db.Authoriza.Remove(authoriza);
            db.SaveChanges();

            return Ok(authoriza);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorizaExists(int id)
        {
            return db.Authoriza.Count(e => e.id == id) > 0;
        }
    }
}