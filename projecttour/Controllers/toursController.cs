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
        private tour_dulichEntities1 db = new tour_dulichEntities1();

        // GET: tours
        public ActionResult Index()
        {
            var tours = db.tours.Include(t => t.tour_loai);
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
            // load for dropdown dia diem
            ViewBag.diadiem = db.tour_diadiem
                                .Select(c => new SelectListItem { Value = c.dd_id.ToString(), Text = c.dd_ten + "-" + c.dd_thanhpho })
                                .ToList();
            // load data db.chitiet
            ViewBag.cityoftour = (from ct in db.tour_chitiet
                                 join dd in db.tour_diadiem
                                 on ct.dd_id equals dd.dd_id
                                 where ct.tour_id == id
                                 select new SelectListItem { Value = dd.dd_ten, Text = dd.dd_thanhpho }).ToList() ;

            ViewBag.Costs = from c in db.tour_gia
                            where c.tour_id == id
                            select c;

            return View(tour);
        }
        [HttpPost]
        public ActionResult Details(ICollection<string> listPlaces)
        {
            string[] array = new string[listPlaces.Count];
            listPlaces.CopyTo(array, 0);
            TempData["idplace"] = "<script>alert('"+ array[0] + "');</script>";
            for (int i = 1; i < array.Length; i++)
            {
                tour_chitiet oneTour_chitiet = new tour_chitiet();
                int generateIdTour_chitiet = (from id in db.tour_chitiet select id).Max(e => e.ct_id);
                oneTour_chitiet.ct_id = generateIdTour_chitiet + 1;
                oneTour_chitiet.tour_id = int.Parse(array[0]);
                oneTour_chitiet.dd_id = int.Parse(array[i]);
                //thutu?
                oneTour_chitiet.ct_thutu = 1;
                db.tour_chitiet.Add(oneTour_chitiet);
                db.SaveChanges();
            }

            return RedirectToAction("Details");
        }
        // GET: tours/Create
        public ActionResult Create()
        {
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten");
            ViewBag.diadiem = db.tour_diadiem
                                .Select(c => new SelectListItem { Value = c.dd_id.ToString(), Text = c.dd_ten + "-" + c.dd_thanhpho })
                                .ToList();
            ViewBag.block = new List<SelectListItem>(){ new SelectListItem() {Text="0", Value="0"},new SelectListItem() { Text="1", Value="1"} };
            return View();
        }

        // POST: tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tour_ten,tour_mota,loai_id,block")] tour tour, ICollection<string> listPlaces)
        {


            int generateID = 0;
            if(db.tours.ToList().Count > 0)
                generateID = (from id in db.tours select id).Max(e => e.tour_id);
            tour.tour_id = generateID + 1;
            if (ModelState.IsValid)
            {
                db.tours.Add(tour);
                db.SaveChanges();
                /*start add cost for tour */
                /*end*/

                /**/
                string[] array = new string[listPlaces.Count];
                listPlaces.CopyTo(array, 0);
                TempData["idplace"] = "<script>alert('" + array[0] + "');</script>";
                for (int i = 1; i < array.Length; i++)
                {
                    tour_chitiet oneTour_chitiet = new tour_chitiet();
                    int generateIdTour_chitiet = (from id in db.tour_chitiet select id).Max(e => e.ct_id);
                    oneTour_chitiet.ct_id = generateIdTour_chitiet + 1;
                    oneTour_chitiet.tour_id = tour.tour_id;
                    oneTour_chitiet.dd_id = int.Parse(array[i]);
                    //thutu?
                    oneTour_chitiet.ct_thutu = 1;
                    db.tour_chitiet.Add(oneTour_chitiet);
                    db.SaveChanges();
                }
                /**/
                return RedirectToAction("Index");
            }

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
            ViewBag.loai_id = new SelectList(db.tour_loai, "loai_id", "loai_ten", tour.loai_id);
            return View(tour);
        }

        // POST: tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tour_id,tour_ten,tour_mota,loai_id,block")] tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
