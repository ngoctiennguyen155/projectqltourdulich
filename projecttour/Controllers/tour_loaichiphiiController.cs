using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projecttour.Models;

namespace projecttour.Controllers
{
    public class tour_loaichiphiiController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_loaichiphii
        public ActionResult Index()
        {
            return View(db.tour_loaichiphii.ToList());
        }

        // GET: tour_loaichiphii/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_loaichiphii tour_loaichiphii = db.tour_loaichiphii.Find(id);
            if (tour_loaichiphii == null)
            {
                return HttpNotFound();
            }
            return View(tour_loaichiphii);
        }

        // GET: tour_loaichiphii/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tour_loaichiphii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cp_ten,cp_mota")] tour_loaichiphii tour_loaichiphii)
        {
            if (ModelState.IsValid)
            {
                int generateId = 0;
                if (db.tour_loaichiphii.ToList().Count>0) generateId = (from id in db.tour_loaichiphii select id).Max(e => e.cp_id);
                tour_loaichiphii.cp_id = generateId + 1;
                db.tour_loaichiphii.Add(tour_loaichiphii);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour_loaichiphii);
        }

        // GET: tour_loaichiphii/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_loaichiphii tour_loaichiphii = db.tour_loaichiphii.Find(id);
            if (tour_loaichiphii == null)
            {
                return HttpNotFound();
            }
            return View(tour_loaichiphii);
        }

        // POST: tour_loaichiphii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cp_id,cp_ten,cp_mota")] tour_loaichiphii tour_loaichiphii)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_loaichiphii).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour_loaichiphii);
        }

        // GET: tour_loaichiphii/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_loaichiphii tour_loaichiphii = db.tour_loaichiphii.Find(id);
            if (tour_loaichiphii == null)
            {
                return HttpNotFound();
            }
            return View(tour_loaichiphii);
        }

        // POST: tour_loaichiphii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_loaichiphii tour_loaichiphii = db.tour_loaichiphii.Find(id);
            db.tour_loaichiphii.Remove(tour_loaichiphii);
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
