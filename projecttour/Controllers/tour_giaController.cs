using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projecttour.Models;

namespace projecttour.Controllers
{
    public class tour_giaController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tour_gia
        public ActionResult Index(string tour)
        {
            ViewBag.tours = from t in db.tours select t;
            ViewBag.Costs = "";
            ViewBag.choose = 0;
            if (!String.IsNullOrEmpty(tour))
            {
                int id = Int32.Parse(tour);
                ViewBag.choose = id;
                ViewBag.Costs = from c in db.tour_gia
                                where c.tour_id == id
                                select c;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Find()
        {
            string id = HttpContext.Request.Form["selectedValue"];
            if (id == "0") return RedirectToAction("index");
            return RedirectToAction("index", new {tour = id });
        }

        public ActionResult Create()
        {
            ViewBag.tours = from t in db.tours select t;
            return View();
        }














        // GET: tour_gia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            return View(tour_gia);
        }

        

        // POST: tour_gia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "gia_sotien,gia_tungay,gia_denngay,tour_id")] tour_gia tour_gia)
        {
            int generateID = 0;
            if(db.tour_gia.ToList().Count>0) generateID = (from id in db.tour_gia select id).Max(e => e.gia_id);
            tour_gia.gia_id = generateID + 1;
            if (ModelState.IsValid)
            {
                db.tour_gia.Add(tour_gia);
                db.SaveChanges();
                return RedirectToAction("index", new { tour = tour_gia.tour_id });
            }

            return RedirectToAction("index", new { tour = tour_gia.tour_id });
        }

        // GET: tour_gia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            ViewBag.tour = db.tour_gia.Find(id);
            /*ViewBag.tour.gia_tungay =  ViewBag.tour.gia_tungay.ToString("dd/MM/yyyy");
            ViewBag.tour.gia_denngay = ViewBag.tour.gia_denngay.ToString("dd/MM/yyyy");*/
            ViewBag.tours = from t in db.tours select t;
            return View();
        }

        // POST: tour_gia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "gia_id,gia_sotien,gia_tungay,gia_denngay,tour_id")] tour_gia tour_gia)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(tour_gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", new { tour = tour_gia.tour_id });
            }
            return RedirectToAction("index", new { tour = tour_gia.tour_id });
        }

        // GET: tour_gia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            return View(tour_gia);
        }

        // POST: tour_gia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_gia tour_gia = db.tour_gia.Find(id);
            db.tour_gia.Remove(tour_gia);
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
