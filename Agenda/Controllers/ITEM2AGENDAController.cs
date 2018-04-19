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
using System.Web.Http.Cors;

namespace Agenda.Controllers
{
    [EnableCors(origins: "http://localhost:54563", headers: "*", methods: "*")]
    public class ITEM2AGENDAController : ApiController
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: api/ITEM2AGENDA
        public IQueryable<ITEM2AGENDA> GetITEM2AGENDA()
        {
            return db.ITEM2AGENDA;
        }

        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
        [HttpGet]
        [ResponseType(typeof(ITEM2AGENDA))]
        [Route("api/ITEM2AGENDA/{itemId}/{agendaId}")]
        public IHttpActionResult GetITEM2AGENDA(int itemId,int agendaId)
        {
            ITEM2AGENDA iTEM2AGENDA = db.ITEM2AGENDA.Where(i => i.item == itemId && i.agenda == agendaId).FirstOrDefault();
            if (iTEM2AGENDA == null)
            {
                return NotFound();
            }

            return Ok(iTEM2AGENDA);
        }

        // GET: api/ITEM2AGENDA/5
        [ResponseType(typeof(ITEM2AGENDA))]
        public IHttpActionResult GetITEM2AGENDA(int id)
        {
            ITEM2AGENDA iTEM2AGENDA = db.ITEM2AGENDA.Find(id);
            if (iTEM2AGENDA == null)
            {
                return NotFound();
            }

            return Ok(iTEM2AGENDA);
        }

        // PUT: api/ITEM2AGENDA/5  // https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
        [HttpPut]
        //[ResponseType(typeof(ITEM2AGENDA))]
        [Route("api/ITEM2AGENDA/{itemId}/{agendaId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutITEM2AGENDA(int itemId, int agendaId, ITEM2AGENDA iTEM2AGENDA)
        {
            ITEM2AGENDA tmpITEM2AGENDA;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (itemId != iTEM2AGENDA.item || agendaId != iTEM2AGENDA.agenda)
            {
                return BadRequest();
            }

            tmpITEM2AGENDA = db.ITEM2AGENDA.FirstOrDefault(e => e.item == itemId && e.agenda == agendaId);
            if (tmpITEM2AGENDA == null)
            {
                // if record not exist, we will crate it
                tmpITEM2AGENDA = new ITEM2AGENDA();
                tmpITEM2AGENDA.item = iTEM2AGENDA.item;
                tmpITEM2AGENDA.agenda = iTEM2AGENDA.agenda;
                tmpITEM2AGENDA.description = iTEM2AGENDA.description;
                tmpITEM2AGENDA.opinion = iTEM2AGENDA.opinion;
                tmpITEM2AGENDA.importance = iTEM2AGENDA.importance;
                tmpITEM2AGENDA.lastUpdate = DateTime.Now;

                db.Entry(tmpITEM2AGENDA).State = EntityState.Added;

            }
            else
            {
                tmpITEM2AGENDA.description = iTEM2AGENDA.description;
                tmpITEM2AGENDA.opinion = iTEM2AGENDA.opinion;
                tmpITEM2AGENDA.importance = iTEM2AGENDA.importance;
                tmpITEM2AGENDA.lastUpdate = DateTime.Now;

                db.Entry(tmpITEM2AGENDA).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ITEM2AGENDAExists(itemId, agendaId))
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

        // POST: api/ITEM2AGENDA
        [ResponseType(typeof(ITEM2AGENDA))]
        public IHttpActionResult PostITEM2AGENDA(ITEM2AGENDA iTEM2AGENDA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ITEM2AGENDA.Add(iTEM2AGENDA);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iTEM2AGENDA.Id }, iTEM2AGENDA);
        }

        // DELETE: api/ITEM2AGENDA/5
        [HttpDelete]
        [Route("api/ITEM2AGENDA/{itemId}/{agendaId}")]
        [ResponseType(typeof(ITEM2AGENDA))]
        public IHttpActionResult DeleteITEM2AGENDA(int itemId, int agendaId)
        {
            if (itemId == 0 || agendaId == 0 )
            {
                return BadRequest();
            }
            ITEM2AGENDA iTEM2AGENDA = db.ITEM2AGENDA.FirstOrDefault(e => e.item == itemId && e.agenda == agendaId);
            if (iTEM2AGENDA == null)
            {
                return NotFound();
            }

            db.ITEM2AGENDA.Remove(iTEM2AGENDA);
            db.SaveChanges();

            return Ok(iTEM2AGENDA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ITEM2AGENDAExists(int itemId, int agendaId)
        {
            return db.ITEM2AGENDA.Count(e => e.item == itemId && e.agenda == agendaId) > 0;
        }
    }
}