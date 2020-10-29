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
    public class tour_nguoidiController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_nguoidi
        public ActionResult Index()
        {
            var tour_nguoidi = db.tour_nguoidi.Include(t => t.tour_doan);
            return View(tour_nguoidi.ToList());
        }

        // GET: tour_nguoidi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nguoidi tour_nguoidi = db.tour_nguoidi.Find(id);
            if (tour_nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(tour_nguoidi);
        }

        // GET: tour_nguoidi/Create
        public ActionResult Create()
        {
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name");
            return View();
        }

        // POST: tour_nguoidi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nguoidi_id,doan_id,nguoidi_dsnhanvien,nguoidi_dskhachhang")] tour_nguoidi tour_nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.tour_nguoidi.Add(tour_nguoidi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_nguoidi.doan_id);
            return View(tour_nguoidi);
        }

        // GET: tour_nguoidi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nguoidi tour_nguoidi = db.tour_nguoidi.Find(id);
            if (tour_nguoidi == null)
            {
                return HttpNotFound();
            }
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_nguoidi.doan_id);
            return View(tour_nguoidi);
        }

        // POST: tour_nguoidi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nguoidi_id,doan_id,nguoidi_dsnhanvien,nguoidi_dskhachhang")] tour_nguoidi tour_nguoidi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_nguoidi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_nguoidi.doan_id);
            return View(tour_nguoidi);
        }

        // GET: tour_nguoidi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nguoidi tour_nguoidi = db.tour_nguoidi.Find(id);
            if (tour_nguoidi == null)
            {
                return HttpNotFound();
            }
            return View(tour_nguoidi);
        }

        // POST: tour_nguoidi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_nguoidi tour_nguoidi = db.tour_nguoidi.Find(id);
            db.tour_nguoidi.Remove(tour_nguoidi);
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
