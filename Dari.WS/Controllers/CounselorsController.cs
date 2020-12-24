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
using Dari.Data;
using Dari.Domain;

namespace Dari.WS.Controllers
{
    public class CounselorsController : ApiController
    {
        private DariContext db = new DariContext();

        // GET: api/Counselors
        public IQueryable<Counselor> GetCounselors()
        {
            return (db.Counselors);
        }

        // GET: api/Counselors/5
        [ResponseType(typeof(Counselor))]
        public IHttpActionResult GetCounselor(int id)
        {
            Counselor counselor = db.Counselors.Find(id);
            if (counselor == null)
            {
                return NotFound();
            }

            return Ok(counselor);
        }

        // PUT: api/Counselors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounselor(int id, Counselor counselor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != counselor.Id)
            {
                return BadRequest();
            }

            db.Entry(counselor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounselorExists(id))
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

        // POST: api/Counselors
        [ResponseType(typeof(Counselor))]
        public IHttpActionResult PostCounselor(Counselor counselor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Counselors.Add(counselor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = counselor.Id }, counselor);
        }

        // DELETE: api/Counselors/5
        [ResponseType(typeof(Counselor))]
        public IHttpActionResult DeleteCounselor(int id)
        {
            Counselor counselor = db.Counselors.Find(id);
            if (counselor == null)
            {
                return NotFound();
            }

            db.Counselors.Remove(counselor);
            db.SaveChanges();

            return Ok(counselor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CounselorExists(int id)
        {
            return db.Counselors.Count(e => e.Id == id) > 0;
        }
    }
}