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
    public class MKsController : Controller
    {
        private AgendaEntities db = new AgendaEntities();

        // GET: MKs
        public ActionResult Index()
        {
            return View(db.MK.ToList());
        }

        // GET: MKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK mK = db.MK.Find(id);
            if (mK == null)
            {
                return HttpNotFound();
            }
            return View(mK);
        }

        // GET: MKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,mk_name")] MK mK)
        {
            if (ModelState.IsValid)
            {
                db.MK.Add(mK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mK);
        }

        // GET: MKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK mK = db.MK.Find(id);
            if (mK == null)
            {
                return HttpNotFound();
            }
            return View(mK);
        }

        // POST: MKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,mk_name")] MK mK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mK);
        }

        // GET: MKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MK mK = db.MK.Find(id);
            if (mK == null)
            {
                return HttpNotFound();
            }
            return View(mK);
        }

        // POST: MKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MK mK = db.MK.Find(id);
            db.MK.Remove(mK);
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
