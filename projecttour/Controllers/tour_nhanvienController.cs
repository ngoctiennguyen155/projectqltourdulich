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
    public class tour_nhanvienController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_nhanvien
        public ActionResult Index()
        {
            return View(db.tour_nhanvien.ToList());
        }

        // GET: tour_nhanvien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nhanvien tour_nhanvien = db.tour_nhanvien.Find(id);
            if (tour_nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(tour_nhanvien);
        }

        // GET: tour_nhanvien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tour_nhanvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nv_ten,nv_sdt,nv_ngaysinh,nv_email,nv_nhiemvu")] tour_nhanvien tour_nhanvien)
        {
            if (ModelState.IsValid)
            {
                int generateId = 0;
                if (db.tour_nhanvien.ToList().Count > 0) generateId = (from id in db.tour_nhanvien select id).Max(e => e.nv_id);
                tour_nhanvien.nv_id = generateId + 1;
                db.tour_nhanvien.Add(tour_nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour_nhanvien);
        }

        // GET: tour_nhanvien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nhanvien tour_nhanvien = db.tour_nhanvien.Find(id);
            if (tour_nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(tour_nhanvien);
        }

        // POST: tour_nhanvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nv_id,nv_ten,nv_sdt,nv_ngaysinh,nv_email,nv_nhiemvu")] tour_nhanvien tour_nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_nhanvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour_nhanvien);
        }

        // GET: tour_nhanvien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_nhanvien tour_nhanvien = db.tour_nhanvien.Find(id);
            if (tour_nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(tour_nhanvien);
        }

        // POST: tour_nhanvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_nhanvien tour_nhanvien = db.tour_nhanvien.Find(id);
            db.tour_nhanvien.Remove(tour_nhanvien);
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
