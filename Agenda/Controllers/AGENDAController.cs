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
    public class AGENDAController : Controller
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: AGENDA
        public ActionResult Index()
        {
            return View(db.AGENDA.ToList());
        }

        // GET: AGENDA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENDA aGENDA = db.AGENDA.Find(id);
            if (aGENDA == null)
            {
                return HttpNotFound();
            }
            return View(aGENDA);
        }

        // GET: AGENDA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AGENDA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,description")] AGENDA aGENDA)
        {
            if (ModelState.IsValid)
            {
                db.AGENDA.Add(aGENDA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aGENDA);
        }

        // GET: AGENDA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENDA aGENDA = db.AGENDA.Find(id);
            if (aGENDA == null)
            {
                return HttpNotFound();
            }
            return View(aGENDA);
        }

        // POST: AGENDA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,description")] AGENDA aGENDA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aGENDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aGENDA);
        }

        // GET: AGENDA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGENDA aGENDA = db.AGENDA.Find(id);
            if (aGENDA == null)
            {
                return HttpNotFound();
            }
            return View(aGENDA);
        }

        // POST: AGENDA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGENDA aGENDA = db.AGENDA.Find(id);
            db.AGENDA.Remove(aGENDA);
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
