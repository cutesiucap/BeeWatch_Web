﻿using System;
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
    public class WatchesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Watches
        public IQueryable<view_WatchDetails> GetWatches()
        {
            return db.view_WatchDetails;
        }

        // GET: api/Watches/5
        [ResponseType(typeof(Watches))]
        public IHttpActionResult GetWatches(int id)
        {
            Watches watches = db.Watches.Find(id);
            if (watches == null)
            {
                return NotFound();
            }

            return Ok(watches);
        }

        [Route("api/GetListWatch/{id}")]
        public IQueryable<view_WatchDetails> GetListWatch(int id)
        {
            return db.view_WatchDetails.Where(x => x.id_Shop == id);
        }

        // PUT: api/Watches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWatches(int id, Watches watches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != watches.id)
            {
                return BadRequest();
            }

            db.Entry(watches).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchesExists(id))
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

        // POST: api/Watches
        [ResponseType(typeof(Watches))]
        public IHttpActionResult PostWatches(Watches watches)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Watches.Add(watches);
            db.SaveChanges();

            return Ok(watches.id);
        }

        // DELETE: api/Watches/5
        [ResponseType(typeof(Watches))]
        public IHttpActionResult DeleteWatches(int id)
        {
            Watches watches = db.Watches.Find(id);
            if (watches == null)
            {
                return NotFound();
            }

            db.Watches.Remove(watches);
            db.SaveChanges();

            return Ok(watches);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WatchesExists(int id)
        {
            return db.Watches.Count(e => e.id == id) > 0;
        }

        [Route("api/Watches/SearchName")]
        [HttpGet]
        // GET: api/Watches
        public IQueryable<view_Watches> Search([FromUri] string name)
        {
            fn_SearchWatch_Result watches = db.fn_SearchWatch(name).FirstOrDefault();
            return db.view_Watches;
        }
    }
}