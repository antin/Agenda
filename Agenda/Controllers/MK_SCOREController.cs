using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda.Models;

namespace Agenda.Controllers
{
    public class MK_SCOREController : Controller
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: MK_SCORE
        public ActionResult Index()
        {
            var mK_SCORE = db.MK_SCORE.Include(m => m.MK1).Include(m => m.AGENDA);
            return View(mK_SCORE.ToList());
        }

        // GET: MK_SCORE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            if (mK_SCORE == null)
            {
                return HttpNotFound();
            }
            return View(mK_SCORE);
        }

        // GET: MK_SCORE/Create
        public ActionResult Create()
        {
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name");
            ViewBag.agendaId = new SelectList(db.AGENDA, "Id", "name");
            return View();
        }

        // POST: MK_SCORE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,mk,score,volume,fromDate,toDate,agendaId")] MK_SCORE mK_SCORE)
        {
            if (ModelState.IsValid)
            {
                db.MK_SCORE.Add(mK_SCORE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_SCORE.mk);
            ViewBag.agendaId = new SelectList(db.AGENDA, "Id", "name", mK_SCORE.agendaId);
            return View(mK_SCORE);
        }

        // GET: MK_SCORE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            if (mK_SCORE == null)
            {
                return HttpNotFound();
            }
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_SCORE.mk);
            ViewBag.agendaId = new SelectList(db.AGENDA, "Id", "name", mK_SCORE.agendaId);
            return View(mK_SCORE);
        }

        // POST: MK_SCORE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,mk,score,volume,fromDate,toDate,agendaId")] MK_SCORE mK_SCORE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mK_SCORE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_SCORE.mk);
            ViewBag.agendaId = new SelectList(db.AGENDA, "Id", "name", mK_SCORE.agendaId);
            return View(mK_SCORE);
        }

        // GET: MK_SCORE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            if (mK_SCORE == null)
            {
                return HttpNotFound();
            }
            return View(mK_SCORE);
        }

        // POST: MK_SCORE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MK_SCORE mK_SCORE = db.MK_SCORE.Find(id);
            db.MK_SCORE.Remove(mK_SCORE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
