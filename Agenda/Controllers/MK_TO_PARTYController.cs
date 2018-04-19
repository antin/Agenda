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
    public class MK_TO_PARTYController : Controller
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: MK_TO_PARTY
        public ActionResult Index()
        {
            var mK_TO_PARTY = db.MK_TO_PARTY.Include(m => m.MK1).Include(m => m.PARTY1);
            return View(mK_TO_PARTY.ToList());
        }

        // GET: MK_TO_PARTY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_TO_PARTY mK_TO_PARTY = db.MK_TO_PARTY.Find(id);
            if (mK_TO_PARTY == null)
            {
                return HttpNotFound();
            }
            return View(mK_TO_PARTY);
        }

        // GET: MK_TO_PARTY/Create
        public ActionResult Create()
        {
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name");
            ViewBag.party = new SelectList(db.PARTY, "Id", "party1");
            return View();
        }

        // POST: MK_TO_PARTY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,mk,party,fromDate,toDate,knessetNumber")] MK_TO_PARTY mK_TO_PARTY)
        {
            if (ModelState.IsValid)
            {
                db.MK_TO_PARTY.Add(mK_TO_PARTY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_TO_PARTY.mk);
            ViewBag.party = new SelectList(db.PARTY, "Id", "party1", mK_TO_PARTY.party);
            return View(mK_TO_PARTY);
        }

        // GET: MK_TO_PARTY/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_TO_PARTY mK_TO_PARTY = db.MK_TO_PARTY.Find(id);
            if (mK_TO_PARTY == null)
            {
                return HttpNotFound();
            }
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_TO_PARTY.mk);
            ViewBag.party = new SelectList(db.PARTY, "Id", "party1", mK_TO_PARTY.party);
            return View(mK_TO_PARTY);
        }

        // POST: MK_TO_PARTY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,mk,party,fromDate,toDate,knessetNumber")] MK_TO_PARTY mK_TO_PARTY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mK_TO_PARTY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mk = new SelectList(db.MK, "Id", "mk_name", mK_TO_PARTY.mk);
            ViewBag.party = new SelectList(db.PARTY, "Id", "party1", mK_TO_PARTY.party);
            return View(mK_TO_PARTY);
        }

        // GET: MK_TO_PARTY/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK_TO_PARTY mK_TO_PARTY = db.MK_TO_PARTY.Find(id);
            if (mK_TO_PARTY == null)
            {
                return HttpNotFound();
            }
            return View(mK_TO_PARTY);
        }

        // POST: MK_TO_PARTY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MK_TO_PARTY mK_TO_PARTY = db.MK_TO_PARTY.Find(id);
            db.MK_TO_PARTY.Remove(mK_TO_PARTY);
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
