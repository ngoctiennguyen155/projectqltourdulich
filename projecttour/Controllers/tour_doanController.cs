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
    public class tour_doanController : Controller
    {
        private tour_dulichEntities1 db = new tour_dulichEntities1();
        // load địa điểm & chi phí
       
        // GET: tour_doan
        public ActionResult Index()
        {
            var tour_doan = db.tour_doan.Include(t => t.tour);
            List<string> tam = new List<string>();

            tour_doan.ToList().ForEach(e =>
            {
                int zz = (int)e.id_gia_tour;
                string z = (from vnd in db.tour_gia
                            where vnd.gia_id == zz
                            select vnd.gia_sotien).First().ToString();
                tam.Add(z);
            });
            ViewBag.CostList = tam;
            return View(tour_doan.ToList());
        }
        public ActionResult Chiphi(int? id)
        {
            // tour
            int tourid = db.tour_doan
                                .Where(c => c.doan_id == id)
                                .Select(c => c.tour_id).First();
            ViewBag.tour_id = db.tours
                                .Where(c => c.tour_id == tourid)
                                .Select(c => new SelectListItem { Value = c.tour_id.ToString(), Text = c.tour_ten})
                                .First();
            // doan
            ViewBag.doanname = db.tour_doan
                                .Where(c => c.doan_id == id)
                                .Select(c => c.doan_name).First();
            ViewBag.doanid = id;

            //ls chi phi
            ViewBag.Listchiphi = db.tour_loaichiphii
                                .Where(c => c.cp_id!=0)
                                .Select(c => new SelectListItem { Value = c.cp_id.ToString(), Text = c.cp_ten })
                                .ToList();
            var tour_Chiphis = db.tour_chiphi
                                .Where(c => c.doan_id == id)
                                .ToList();
            ViewBag.listchiphii = tour_Chiphis;
            List<string> tam = new List<string>();
            int result = 0;
            tour_Chiphis.ForEach(e =>
            {
                if (tour_Chiphis.Count > 0)
                {
                    int tt = int.Parse(e.chiphi_total.ToString());
                    result += tt;
                    int zz = (int)e.loaichiphi;
                    string z = (from vnd in db.tour_loaichiphii
                                where vnd.cp_id == zz
                                select vnd.cp_ten).First().ToString();
                    tam.Add(z);
                }
            });
            ViewBag.namechiphi = tam;
            ViewBag.total = result;


            return View();
        }
        [HttpPost]
        public ActionResult Chiphi()
        {
            int idTour = int.Parse(Request["tour_id"]);
            int idDoan =int.Parse(Request["doan_name"]);
            int loaichiphi=int.Parse(Request["lcp"]); ;
            string hoadon= Request["hoa_don"];
            string noidung = Request["noi_dung"];
            DateTime ngaylhd = DateTime.Now;
            int sotien = int.Parse(Request["sotien"]);

            tour_chiphi z = new tour_chiphi();
            int generateId = 0;
            if(db.tour_chiphi.ToList().Count>0) generateId = (from id in db.tour_chiphi select id).Max(e => e.chiphi_id);
            z.chiphi_id = generateId + 1;
            z.doan_id = idDoan;
            z.chiphi_total = sotien;
            z.loaichiphi = loaichiphi;
            z.hoadon = hoadon;
            z.noidung = noidung;
            z.chiphi_chitiet = "0,";
            z.ngaylap = ngaylhd;
            db.tour_chiphi.Add(z as tour_chiphi);
            db.SaveChanges();

            return Redirect("/tour_doan/Chiphi/" + idDoan);
        }
        // GET: tour_doan/Details/5
        public ActionResult Details(int? id)
        {
            //load data customers
            ViewBag.customers = db.tour_khachhang
                                .Select(c => new SelectListItem { Value = c.kh_id.ToString(), Text = c.kh_ten + "-" + c.kh_cmnd })
                                .ToList();
            //load data staffs
            ViewBag.staffs = db.tour_nhanvien
                                .Select(c => new SelectListItem { Value = c.nv_id.ToString(), Text = c.nv_ten + "-" + c.nv_nhiemvu })
                                .ToList();
           
           
            //start load staffs current
            string getstringstaff = (from vnd in db.tour_nguoidi
                             where vnd.doan_id == id
                             select vnd.nguoidi_dsnhanvien).First().ToString();
            string[] liststaffs = getstringstaff.Split(',');
            List<SelectListItem> listStaffCurrent = new List<SelectListItem>();
            for (int i = 0; i < liststaffs.Length - 1; i++)
            {
                int idtatm = int.Parse(liststaffs[i]);
                string name = (from vnd in db.tour_nhanvien
                               where vnd.nv_id == idtatm
                               select vnd.nv_ten).First().ToString();
                string cmnd = (from vnd in db.tour_nhanvien
                               where vnd.nv_id == idtatm
                               select vnd.nv_cmnd).First().ToString();

                listStaffCurrent.Add(new SelectListItem { Value = liststaffs[i], Text = name + "-" + cmnd });
            }
            ViewBag.listStaffCurrent = listStaffCurrent;

            ViewBag.idtourcurrent = id;
            //end
            //start load customers current
            
            List<SelectListItem> listcustomerCurrent = new List<SelectListItem>();
            if(db.tour_nguoidi.Where(e => e.doan_id == id && e.nguoidi_dsnhanvien == "0").ToList().Count>0)
            db.tour_nguoidi.Where(e => e.doan_id == id && e.nguoidi_dsnhanvien=="0").ToList().ForEach(ee =>
            {
                listcustomerCurrent.Add(new SelectListItem { Value = ee.nguoidi_dskhachhang, Text = db.tour_khachhang.Where(eee => eee.kh_id.ToString() == ee.nguoidi_dskhachhang).Select(eee => eee.kh_ten).First() + "-" + db.tour_khachhang.Where(eee => eee.kh_id.ToString() == ee.nguoidi_dskhachhang).Select(eee => eee.kh_cmnd).First() });
            });
            
            ViewBag.listcustomerCurrent = listcustomerCurrent;

            List<SelectListItem> liststaffCurrent = new List<SelectListItem>();
            if (db.tour_nguoidi.Where(e => e.doan_id == id && e.nguoidi_dsnhanvien == "0").ToList().Count > 0)
                db.tour_nguoidi.Where(e => e.doan_id == id && e.nguoidi_dskhachhang == "0").ToList().ForEach(ee =>
            {
                liststaffCurrent.Add(new SelectListItem { Value = ee.nguoidi_dsnhanvien, Text = db.tour_nhanvien.Where(eee => eee.nv_id.ToString() == ee.nguoidi_dsnhanvien).Select(eee => eee.nv_ten).First() + "-" + db.tour_nhanvien.Where(eee => eee.nv_id.ToString() == ee.nguoidi_dsnhanvien).Select(eee => eee.nv_nhiemvu).First() });
            });

            ViewBag.listcustomerCurrent = listcustomerCurrent;
            ViewBag.listStaffCurrent = liststaffCurrent;
            //end
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
        [HttpPost]
        public ActionResult Detail( ICollection<string> listCustommer, ICollection<string> listStaffs)
        {
            string[] array = new string[listCustommer.Count];
            listCustommer.CopyTo(array, 0);

            string[] array1 = new string[listStaffs.Count];
            listStaffs.CopyTo(array1, 0);
            TempData["idplace"] = "<script>alert('" + array[0] + "');</script>";

            int idcurrentdoan = int.Parse(Request["idcurrent"]);
            // delete all khach hang vs nhan vien
            db.tour_nguoidi.Where(e => e.doan_id == idcurrentdoan).ToList().ForEach(ee =>
             {
                 db.tour_nguoidi.Remove(db.tour_nguoidi.Where(eee => eee.nguoidi_id == ee.nguoidi_id).FirstOrDefault());
                 db.SaveChanges();
             });
            //
            
            for (int i = 1; i < array.Length; i++)
            {
                int generateId = 0;
                if (db.tour_nguoidi.ToList().Count > 0)
                {
                    generateId = (from id in db.tour_nguoidi select id).Max(e => e.nguoidi_id);
                }
                tour_nguoidi ndtam = new tour_nguoidi();
                ndtam.nguoidi_id = generateId + 1;
                ndtam.doan_id = idcurrentdoan;
                ndtam.nguoidi_dsnhanvien = "0";
                ndtam.nguoidi_dskhachhang = array[i];
                db.tour_nguoidi.Add(ndtam);
                db.SaveChanges();
            }
            for (int i = 1; i < array1.Length; i++)
            {
                int generateId = 0;
                if (db.tour_nguoidi.ToList().Count > 0)
                {
                    generateId = (from id in db.tour_nguoidi select id).Max(e => e.nguoidi_id);
                }
                tour_nguoidi ndtam = new tour_nguoidi();
                ndtam.nguoidi_id = generateId + 1;
                ndtam.doan_id = idcurrentdoan;
                ndtam.nguoidi_dsnhanvien = array1[i];
                ndtam.nguoidi_dskhachhang = "0";
                db.tour_nguoidi.Add(ndtam);
                db.SaveChanges();
            }
            return Redirect("https://localhost:44341/tour_doan/Details/" + idcurrentdoan);
        }
        // GET: tour_doan/Create
        public ActionResult Create()
        {
            ViewBag.CostList = from c in db.tour_gia select c;
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
            //Request["cost"] idtour
           
            int stsb = int.Parse(Request["costvalue"].ToString());
            int zzz = db.tour_gia.Where(a => a.gia_sotien == stsb && a.tour_id== tour_doan.tour_id)
                     .Select(a => a.gia_id)
                     .First();
            if (ModelState.IsValid)
            {
                int generateIdTour_doan = 0;
                if(db.tour_doan.ToList().Count>0)
                generateIdTour_doan = (from id in db.tour_doan select id).Max(e => e.doan_id);
                int iddoan = generateIdTour_doan + 1;
                tour_doan.doan_id = iddoan;
                tour_doan.id_gia_tour = zzz;
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
            int idTour =int.Parse((from vnd in db.tour_doan
                        where vnd.doan_id == id
                        select vnd.tour_id).First().ToString());
            ViewBag.selectedCost = (from vnd in db.tour_doan
                                    where vnd.doan_id == id
                                    select vnd.id_gia_tour).First().ToString();
            ViewBag.listCost = (from c in db.tour_gia
                                where c.tour_id == idTour
                                select new SelectListItem { Value = c.gia_id.ToString(), Text = c.gia_sotien.ToString()}).ToList();

            ViewBag.tour_id = new SelectList(db.tours, "tour_id", "tour_ten", tour_doan.tour_id);
            return View(tour_doan);
        }

        // POST: tour_doan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doan_id,doan_name,doan_ngaydi,doan_ngayve,doan_chitietchuongtrinh")] tour_doan tour_doan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_doan).State = EntityState.Modified;
                int idcostchange = int.Parse(Request["costchange"]);
                int idtour = int.Parse(Request["tour_idd"]);
                tour_doan.tour_id = idtour;
                tour_doan.id_gia_tour = idcostchange;
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
            tour_nguoidi tour_Nguoidi = db.tour_nguoidi.FirstOrDefault(e => e.doan_id == id);
            db.tour_nguoidi.Remove(tour_Nguoidi);

            tour_chiphi tour_Chiphi = db.tour_chiphi.FirstOrDefault(e => e.doan_id == id);
            db.tour_chiphi.Remove(tour_Chiphi);

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
