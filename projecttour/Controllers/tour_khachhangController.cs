﻿using System;
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
    public class tour_khachhangController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_khachhang
        public ActionResult Index()
        {
            return View(db.tour_khachhang.ToList());
        }

        // GET: tour_khachhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_khachhang tour_khachhang = db.tour_khachhang.Find(id);
            if (tour_khachhang == null)
            {
                return HttpNotFound();
            }
            return View(tour_khachhang);
        }

        // GET: tour_khachhang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tour_khachhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kh_id,kh_ten,kh_sdt,kh_ngaysinh,kh_email,kh_cmnd")] tour_khachhang tour_khachhang)
        {
            if (ModelState.IsValid)
            {
                db.tour_khachhang.Add(tour_khachhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour_khachhang);
        }

        // GET: tour_khachhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_khachhang tour_khachhang = db.tour_khachhang.Find(id);
            if (tour_khachhang == null)
            {
                return HttpNotFound();
            }
            return View(tour_khachhang);
        }

        // POST: tour_khachhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kh_id,kh_ten,kh_sdt,kh_ngaysinh,kh_email,kh_cmnd")] tour_khachhang tour_khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour_khachhang);
        }

        // GET: tour_khachhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_khachhang tour_khachhang = db.tour_khachhang.Find(id);
            if (tour_khachhang == null)
            {
                return HttpNotFound();
            }
            return View(tour_khachhang);
        }

        // POST: tour_khachhang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_khachhang tour_khachhang = db.tour_khachhang.Find(id);
            db.tour_khachhang.Remove(tour_khachhang);
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
