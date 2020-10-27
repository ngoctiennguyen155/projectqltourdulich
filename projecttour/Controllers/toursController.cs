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
    public class toursController : Controller
    {
        private tour_dulichEntities db = new tour_dulichEntities();

        // GET: tours
        public ActionResult Index()
        {
            var tours = db.tours.Include(t => t.tour_gia).Include(t => t.tour_loai);
            return View(tours.ToList());
        }

        // GET: tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: tours/Create
        public ActionResult Create()
        {
            ViewBag.gia_id = new SelectList(db.tour_gia, "gia_id", "gia_id");
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten");
            return View();
        }

        // POST: tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tour_id,tour_ten,tour_mota,loai_id,gia_id")] tour tour)
        {
            if (ModelState.IsValid)
            {
                db.tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gia_id = new SelectList(db.tour_gia, "gia_id", "gia_id", tour.gia_id);
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten", tour.loai_id);
            return View(tour);
        }

        // GET: tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.gia_id = new SelectList(db.tour_gia, "gia_id", "gia_id", tour.gia_id);
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten", tour.loai_id);
            return View(tour);
        }

        // POST: tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tour_id,tour_ten,tour_mota,loai_id,gia_id")] tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gia_id = new SelectList(db.tour_gia, "gia_id", "gia_id", tour.gia_id);
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten", tour.loai_id);
            return View(tour);
        }

        // GET: tours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour tour = db.tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour tour = db.tours.Find(id);
            db.tours.Remove(tour);
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
