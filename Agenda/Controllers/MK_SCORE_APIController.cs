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
using Agenda.Models;

namespace Agenda.Controllers
{
    public class MK_SCORE_APIController : ApiController
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: api/MK_SCORE_API
        public IQueryable<MK_SCORE> GetMK_SCORE()
        {
            return db.MK_SCORE;
        }

        // GET: api/MK_SCORE_API/5
        [ResponseType(typeof(MK_SCORE))]
        public IHttpActionResult GetMK_SCORE(int id)
        {
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            if (mK_SCORE == null)
            {
                return NotFound();
            }

            return Ok(mK_SCORE);
        }

        // PUT: api/MK_SCORE_API/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMK_SCORE(int id, MK_SCORE mK_SCORE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mK_SCORE.Id)
            {
                return BadRequest();
            }

            db.Entry(mK_SCORE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MK_SCOREExists(id))
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

        // POST: api/MK_SCORE_API
        [ResponseType(typeof(MK_SCORE))]
        public IHttpActionResult PostMK_SCORE(MK_SCORE mK_SCORE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MK_SCORE.Add(mK_SCORE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mK_SCORE.Id }, mK_SCORE);
        }

        // DELETE: api/MK_SCORE_API/5
        [ResponseType(typeof(MK_SCORE))]
        public IHttpActionResult DeleteMK_SCORE(int id)
        {
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            if (mK_SCORE == null)
            {
                return NotFound();
            }

            db.MK_SCORE.Remove(mK_SCORE);
            db.SaveChanges();

            return Ok(mK_SCORE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MK_SCOREExists(int id)
        {
            return db.MK_SCORE.Count(e => e.Id == id) > 0;
        }
    }
}