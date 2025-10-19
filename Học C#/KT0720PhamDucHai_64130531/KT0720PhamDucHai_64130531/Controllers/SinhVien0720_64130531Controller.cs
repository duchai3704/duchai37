using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT0720PhamDucHai_64130531.Models;

namespace KT0720PhamDucHai_64130531.Controllers
{
    public class SinhVien0720_64130531Controller : Controller
    {
        private KT0720_64130531Entities db = new KT0720_64130531Entities();

        public ActionResult TimKiem_64130531(string maSV = "", string hoTen = "", string maLop = "")
        {
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");

            var sinhViens = db.SinhViens.SqlQuery(
                "EXEC SinhVien_TimKiem @MaSV, @HoTen, @MaLop",
                new SqlParameter("MaSV", string.IsNullOrEmpty(maSV) ? (object)DBNull.Value : maSV),
                new SqlParameter("HoTen", string.IsNullOrEmpty(hoTen) ? (object)DBNull.Value : hoTen),
                new SqlParameter("MaLop", string.IsNullOrEmpty(maLop) ? (object)DBNull.Value : maLop)
            ).ToList();

            if (sinhViens.Count() == 0)
                ViewBag.TB = "Không có thông tin cần tìm";

            return View(sinhViens);
        }

        // GET: SinhVien0720_64130531
        public ActionResult Index()
        {
            var sinhViens = db.SinhViens.Include(s => s.LOP);
            return View(sinhViens.ToList());
        }

        

        // GET: SinhVien0720_64130531/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        string LayMaSV()
        {
            var maMax = db.SinhViens.ToList().Select(n => n.MaSV).Max();
            int maSV = int.Parse(maMax.Substring(4)) + 1;
            string SV = maSV.ToString("D4");
            return "6413" + SV;
        }

        // GET: SinhVien0720_64130531/Create
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhVien0720_64130531/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SinhVien sinhVien)
        {

            var imgSV = Request.Files["AnhSV"];
            string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
            var path = Server.MapPath("/Images/" + postedFileName);
            imgSV.SaveAs(path);
            if (ModelState.IsValid)
            {
                sinhVien.MaSV = LayMaSV();
                sinhVien.AnhSV = postedFileName;
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhVien0720_64130531/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // POST: SinhVien0720_64130531/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SinhVien sinhVien)
        {
            var imgSV = Request.Files["AnhSV"];
            try
            {
                string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
                var path = Server.MapPath("~/Images/" + postedFileName);
                imgSV.SaveAs(path);
            }
            catch { }
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }

        // GET: SinhVien0720_64130531/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhVien0720_64130531/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SinhVien sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
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
