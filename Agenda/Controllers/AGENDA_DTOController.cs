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
    public class AGENDA_DTOController : ApiController
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: api/AGENDA_DTO
        public IQueryable<AGENDA_DTO> GetAGENDA_DTO()
        {
            // http://www.asp.net/web-api/overview/data/using-web-api-with-entity-framework/part-5
            var AGENDA_DTOs = from b in db.AGENDA
                        select new AGENDA_DTO()
                        {
                            Id = b.Id,
                            name = b.name,
                            description = b.description
                        };

            return AGENDA_DTOs;
            //http://stackoverflow.com/questions/16261667/asp-net-web-api-handling-2-relating-tables
            //return db.AGENDA;
            //return db.AGENDA.SelectMany(x => new AGENDA_DTO {
            //    x.Id,
            //    x.name,
            //    x.description
            //});
        }

        // GET: api/AGENDA_DTO/5
        [ResponseType(typeof(AGENDA_DTO))]
        public IHttpActionResult GetAGENDA_DTO(int id)
        {
            //AGENDA_DTO aGENDA_DTO = db.AGENDA_DTO.Find(id);
            AGENDA_DTO aGENDA_DTO = null;
            AGENDA aAGENDA = db.AGENDA.Find(id);
            
            //.Select(x => new AGENDA_DTO {
            //    x.Id,
            //    x.name,
            //    x.description
            //});
            if (aGENDA_DTO == null)
            {
                return NotFound();
            }

            return Ok(aGENDA_DTO);
        }

        //// PUT: api/AGENDA_DTO/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAGENDA_DTO(int id, AGENDA_DTO aGENDA_DTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != aGENDA_DTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(aGENDA_DTO).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AGENDA_DTOExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/AGENDA_DTO
        //[ResponseType(typeof(AGENDA_DTO))]
        //public IHttpActionResult PostAGENDA_DTO(AGENDA_DTO aGENDA_DTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AGENDA_DTO.Add(aGENDA_DTO);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = aGENDA_DTO.Id }, aGENDA_DTO);
        //}

        //// DELETE: api/AGENDA_DTO/5
        //[ResponseType(typeof(AGENDA_DTO))]
        //public IHttpActionResult DeleteAGENDA_DTO(int id)
        //{
        //    AGENDA_DTO aGENDA_DTO = db.AGENDA_DTO.Find(id);
        //    if (aGENDA_DTO == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AGENDA_DTO.Remove(aGENDA_DTO);
        //    db.SaveChanges();

        //    return Ok(aGENDA_DTO);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool AGENDA_DTOExists(int id)
        //{
        //    return db.AGENDA_DTO.Count(e => e.Id == id) > 0;
        //}
    }
}