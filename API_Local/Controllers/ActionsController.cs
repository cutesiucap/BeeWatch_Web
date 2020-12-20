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
    public class ActionsController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Actions
        public IQueryable<Models.Action> GetAction()
        {
            return db.Action;
        }

        // GET: api/Actions/5
        [ResponseType(typeof(Models.Action))]
        public IHttpActionResult GetAction(int id)
        {
            Models.Action action = db.Action.Find(id);
            if (action == null)
            {
                return NotFound();
            }

            return Ok(action);
        }

        // PUT: api/Actions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAction(int id, Models.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != action.id)
            {
                return BadRequest();
            }

            db.Entry(action).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(id))
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

        // POST: api/Actions
        [ResponseType(typeof(Models.Action))]
        public IHttpActionResult PostAction(Models.Action action)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Action.Add(action);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ActionExists(action.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = action.id }, action);
        }

        // DELETE: api/Actions/5
        [ResponseType(typeof(Models.Action))]
        public IHttpActionResult DeleteAction(int id)
        {
            Models.Action action = db.Action.Find(id);
            if (action == null)
            {
                return NotFound();
            }

            db.Action.Remove(action);
            db.SaveChanges();

            return Ok(action);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActionExists(int id)
        {
            return db.Action.Count(e => e.id == id) > 0;
        }
    }
}