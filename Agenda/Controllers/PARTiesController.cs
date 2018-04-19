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
    public class PARTiesController : Controller
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: PARTies
        public ActionResult Index()
        {
            return View(db.PARTY.ToList());
        }

        // GET: PARTies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTY pARTY = db.PARTY.Find(id);
            if (pARTY == null)
            {
                return HttpNotFound();
            }
            return View(pARTY);
        }

        // GET: PARTies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PARTies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,party1")] PARTY pARTY)
        {
            if (ModelState.IsValid)
            {
                db.PARTY.Add(pARTY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pARTY);
        }

        // GET: PARTies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTY pARTY = db.PARTY.Find(id);
            if (pARTY == null)
            {
                return HttpNotFound();
            }
            return View(pARTY);
        }

        // POST: PARTies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,party1")] PARTY pARTY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pARTY);
        }

        // GET: PARTies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTY pARTY = db.PARTY.Find(id);
            if (pARTY == null)
            {
                return HttpNotFound();
            }
            return View(pARTY);
        }

        // POST: PARTies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PARTY pARTY = db.PARTY.Find(id);
            db.PARTY.Remove(pARTY);
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
