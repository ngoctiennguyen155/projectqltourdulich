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
        public class thongke
        {
            public int idtour { get; set; }
            public string tour { get; set; }
            public int tongdoandi { get; set; }
            public int tongdanhthu { get; set; }
            public int tongchiphi { get; set; }
            public int lai { get; set; }
            public int sokhach { get; set; }
            public int giatour { get; set; }
        }
        public ActionResult Index()
        {
            var tour_nguoidi = db.tour_nguoidi.Include(t => t.tour_doan);
            List<thongke> listtk = new List<thongke>();
            List<int> ttt = new List<int>();
            var tourlist = db.tours.ToList();
            tourlist.ForEach(e =>
            {
                thongke tk = new thongke();
                tk.idtour = e.tour_id;
                tk.tour = e.tour_ten;
                
                tk.tongdoandi = db.tour_doan.Count(element => element.tour_id==e.tour_id );

                int tdt = 0;
                int tcp = 0;
                // tong chi phi = doan gia * so nguoi di
                db.tour_doan.Where(ee => ee.tour_id == e.tour_id).ToList().ForEach(z =>
                 {
                     // tong nguoi di * gia -> doanh thu
                     string giadoan = db.tour_gia.First(zz => zz.gia_id == z.id_gia_tour).gia_sotien.ToString();
                     int nguoidi = db.tour_nguoidi.Where(zz => zz.doan_id == z.doan_id).Count();
                     
                     tdt += nguoidi * int.Parse(giadoan);

                     //all chi phi total
                     db.tour_chiphi.Where(zz => zz.doan_id == z.doan_id).ToList().ForEach(zzz =>
                     {
                         string ztamz = db.tour_chiphi.First(zzzz => zzzz.chiphi_id == zzz.chiphi_id).chiphi_total.ToString();
                         tcp += int.Parse(ztamz);
                     });
                     
                 });
                tk.tongchiphi = tcp;
                tk.tongdanhthu = tdt;
                
                listtk.Add(tk);
                //
            });
            ViewBag.listCost = listtk.ToList();
            return View(listtk.ToList());
        }


        // GET: tour_nguoidi/Edit/5
        public ActionResult Chitiet(int? id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            tour_nguoidi tour_nguoidi = db.tour_nguoidi.Find(id);
           /* if (tour_nguoidi == null)
            {
                return HttpNotFound();
            }*/
            List<thongke> listtk = new List<thongke>();
            
            ViewBag.listTour = db.tours.ToList();
            db.tour_doan.Where(ee => ee.tour_id == id).ToList().ForEach(z =>
            {
                // tong nguoi di * gia -> doanh thu
                thongke tKe = new thongke();
                tKe.idtour = z.doan_id;
                tKe.tour = z.doan_name;
                string giadoan = db.tour_gia.First(zz => zz.gia_id == z.id_gia_tour).gia_sotien.ToString();
                int nguoidi = db.tour_nguoidi.Where(zz => zz.doan_id == z.doan_id).Count();
                tKe.sokhach = nguoidi;
                tKe.giatour = int.Parse(giadoan);
                tKe.tongdanhthu = nguoidi * int.Parse(giadoan);
                //all chi phi total
                int tcp = 0;
                db.tour_chiphi.Where(zz => zz.doan_id == z.doan_id).ToList().ForEach(zzz =>
                {
                    string ztamz = db.tour_chiphi.First(zzzz => zzzz.chiphi_id == zzz.chiphi_id).chiphi_total.ToString();
                    tcp += int.Parse(ztamz);
                });
                tKe.tongchiphi = tcp;
                listtk.Add(tKe);
            });
            return View(listtk.ToList());
        }
        [HttpPost]
        public ActionResult Chitiet()
        {
            string id = HttpContext.Request.Form["selectedValue"];
            return Redirect("https://localhost:44341/tour_nguoidi/Chitiet/" + id);
        }

    }
}
