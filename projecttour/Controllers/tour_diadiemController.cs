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
    public class tour_diadiemController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_diadiem
        public ActionResult Index()
        {
            return View(db.tour_diadiem.ToList());
        }

        // GET: tour_diadiem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_diadiem tour_diadiem = db.tour_diadiem.Find(id);
            if (tour_diadiem == null)
            {
                return HttpNotFound();
            }
            return View(tour_diadiem);
        }

        // GET: tour_diadiem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tour_diadiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dd_thanhpho,dd_ten,dd_mota")] tour_diadiem tour_diadiem)
        {
            if (ModelState.IsValid)
            {
                int generateId = 0;
                if (db.tour_diadiem.ToList().Count > 0) generateId = (from id in db.tour_diadiem select id).Max(e => e.dd_id);
                tour_diadiem.dd_id = generateId + 1;
                db.tour_diadiem.Add(tour_diadiem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour_diadiem);
        }

        // GET: tour_diadiem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_diadiem tour_diadiem = db.tour_diadiem.Find(id);
            if (tour_diadiem == null)
            {
                return HttpNotFound();
            }
            return View(tour_diadiem);
        }

        // POST: tour_diadiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dd_id,dd_thanhpho,dd_ten,dd_mota")] tour_diadiem tour_diadiem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_diadiem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour_diadiem);
        }

        // GET: tour_diadiem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_diadiem tour_diadiem = db.tour_diadiem.Find(id);
            if (tour_diadiem == null)
            {
                return HttpNotFound();
            }
            return View(tour_diadiem);
        }

        // POST: tour_diadiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_diadiem tour_diadiem = db.tour_diadiem.Find(id);
            db.tour_diadiem.Remove(tour_diadiem);
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
