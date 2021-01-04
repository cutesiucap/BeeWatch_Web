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
    public class ImagesController : ApiController
    {
        private BeeWatchDBEntities db = new BeeWatchDBEntities();

        // GET: api/Images
        public IQueryable<Image> GetImage()
        {
            var lImage = db.Image;
            foreach(var item in lImage)
            {
                item.Watches = null;
            }
            return lImage;
        }

        // GET: api/Images/5
        [ResponseType(typeof(Image))]
        public IHttpActionResult GetImage(int id)
        {
            Image image = db.Image.Find(id);
            if (image == null)
            {
                return NotFound();
            }
            image.Watches = null;
            return Ok(image);
        }

        // PUT: api/Images/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutImage(int id, Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != image.id)
            {
                return BadRequest();
            }

            db.Entry(image).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // POST: api/Images
        [ResponseType(typeof(Image))]
        public IHttpActionResult PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Image.Add(image);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = image.id }, image);
        }

        // DELETE: api/Images/5
        [ResponseType(typeof(Image))]
        public IHttpActionResult DeleteImage(int id)
        {
            Image image = db.Image.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            db.Image.Remove(image);
            db.SaveChanges();

            return Ok(image);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageExists(int id)
        {
            return db.Image.Count(e => e.id == id) > 0;
        }
    }
}