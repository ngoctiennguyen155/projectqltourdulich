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
    public class tour_chiphiController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_chiphi
        public ActionResult Index()
        {
            var tour_chiphi = db.tour_chiphi.Include(t => t.tour_doan);
            return View(tour_chiphi.ToList());
        }

        // GET: tour_chiphi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_chiphi tour_chiphi = db.tour_chiphi.Find(id);
            if (tour_chiphi == null)
            {
                return HttpNotFound();
            }
            return View(tour_chiphi);
        }

        // GET: tour_chiphi/Create
        public ActionResult Create()
        {
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name");
            return View();
        }

        // POST: tour_chiphi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chiphi_id,doan_id,chiphi_total,chiphi_chitiet")] tour_chiphi tour_chiphi)
        {
            if (ModelState.IsValid)
            {
                db.tour_chiphi.Add(tour_chiphi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_chiphi.doan_id);
            return View(tour_chiphi);
        }

        // GET: tour_chiphi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_chiphi tour_chiphi = db.tour_chiphi.Find(id);
            if (tour_chiphi == null)
            {
                return HttpNotFound();
            }
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_chiphi.doan_id);
            return View(tour_chiphi);
        }

        // POST: tour_chiphi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chiphi_id,doan_id,chiphi_total,chiphi_chitiet")] tour_chiphi tour_chiphi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_chiphi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doan_id = new SelectList(db.tour_doan, "doan_id", "doan_name", tour_chiphi.doan_id);
            return View(tour_chiphi);
        }

        // GET: tour_chiphi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_chiphi tour_chiphi = db.tour_chiphi.Find(id);
            if (tour_chiphi == null)
            {
                return HttpNotFound();
            }
            return View(tour_chiphi);
        }

        // POST: tour_chiphi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_chiphi tour_chiphi = db.tour_chiphi.Find(id);
            db.tour_chiphi.Remove(tour_chiphi);
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
