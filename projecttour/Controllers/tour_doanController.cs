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
    public class tour_doanController : Controller
    {
        private tour_dulichEntities db = new tour_dulichEntities();

        // GET: tour_doan
        public ActionResult Index()
        {
            var tour_doan = db.tour_doan.Include(t => t.tour);
            return View(tour_doan.ToList());
        }

        // GET: tour_doan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_doan tour_doan = db.tour_doan.Find(id);
            if (tour_doan == null)
            {
                return HttpNotFound();
            }
            return View(tour_doan);
        }

        // GET: tour_doan/Create
        public ActionResult Create()
        {
            ViewBag.tour_id = new SelectList(db.tours, "tour_id", "tour_ten");
            return View();
        }

        // POST: tour_doan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doan_id,tour_id,doan_name,doan_ngaydi,doan_ngayve,doan_chitietchuongtrinh")] tour_doan tour_doan)
        {
            if (ModelState.IsValid)
            {
                db.tour_doan.Add(tour_doan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tour_id = new SelectList(db.tours, "tour_id", "tour_ten", tour_doan.tour_id);
            return View(tour_doan);
        }

        // GET: tour_doan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_doan tour_doan = db.tour_doan.Find(id);
            if (tour_doan == null)
            {
                return HttpNotFound();
            }
            ViewBag.tour_id = new SelectList(db.tours, "tour_id", "tour_ten", tour_doan.tour_id);
            return View(tour_doan);
        }

        // POST: tour_doan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doan_id,tour_id,doan_name,doan_ngaydi,doan_ngayve,doan_chitietchuongtrinh")] tour_doan tour_doan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_doan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tour_id = new SelectList(db.tours, "tour_id", "tour_ten", tour_doan.tour_id);
            return View(tour_doan);
        }

        // GET: tour_doan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_doan tour_doan = db.tour_doan.Find(id);
            if (tour_doan == null)
            {
                return HttpNotFound();
            }
            return View(tour_doan);
        }

        // POST: tour_doan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_doan tour_doan = db.tour_doan.Find(id);
            db.tour_doan.Remove(tour_doan);
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
