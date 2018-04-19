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
    public class VOTE_TO_AGENDAController : ApiController
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: api/VOTE_TO_AGENDA
        public IQueryable<VOTE_TO_AGENDA> GetVOTE_TO_AGENDA()
        {
            return db.VOTE_TO_AGENDA;
        }

        // GET: api/VOTE_TO_AGENDA/5
        [ResponseType(typeof(VOTE_TO_AGENDA))]
        public IHttpActionResult GetVOTE_TO_AGENDA(int id)
        {
            VOTE_TO_AGENDA vOTE_TO_AGENDA = db.VOTE_TO_AGENDA.Find(id);
            if (vOTE_TO_AGENDA == null)
            {
                return NotFound();
            }

            return Ok(vOTE_TO_AGENDA);
        }

        // PUT: api/VOTE_TO_AGENDA/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVOTE_TO_AGENDA(int id, VOTE_TO_AGENDA vOTE_TO_AGENDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vOTE_TO_AGENDA.Id)
            {
                return BadRequest();
            }

            db.Entry(vOTE_TO_AGENDA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VOTE_TO_AGENDAExists(id))
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

        // POST: api/VOTE_TO_AGENDA
        [ResponseType(typeof(VOTE_TO_AGENDA))]
        public IHttpActionResult PostVOTE_TO_AGENDA(VOTE_TO_AGENDA vOTE_TO_AGENDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VOTE_TO_AGENDA.Add(vOTE_TO_AGENDA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vOTE_TO_AGENDA.Id }, vOTE_TO_AGENDA);
        }

        // DELETE: api/VOTE_TO_AGENDA/5
        [ResponseType(typeof(VOTE_TO_AGENDA))]
        public IHttpActionResult DeleteVOTE_TO_AGENDA(int id)
        {
            VOTE_TO_AGENDA vOTE_TO_AGENDA = db.VOTE_TO_AGENDA.Find(id);
            if (vOTE_TO_AGENDA == null)
            {
                return NotFound();
            }

            db.VOTE_TO_AGENDA.Remove(vOTE_TO_AGENDA);
            db.SaveChanges();

            return Ok(vOTE_TO_AGENDA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VOTE_TO_AGENDAExists(int id)
        {
            return db.VOTE_TO_AGENDA.Count(e => e.Id == id) > 0;
        }
    }
}