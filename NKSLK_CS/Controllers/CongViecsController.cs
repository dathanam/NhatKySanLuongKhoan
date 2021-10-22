using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NKSLK_CS;

namespace NKSLK_CS.Controllers
{
    public class CongViecsController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: CongViecs
        public ActionResult Index()
        {
            var congViec = db.CongViec.Include(c => c.DanhMucCongViec).Include(c => c.SanPham);
            return View(congViec.ToList());
        }

        // GET: CongViecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = db.CongViec.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            return View(congViec);
        }

        // GET: CongViecs/Create
        public ActionResult Create()
        {
            ViewBag.id_danh_muc_cong_viec = new SelectList(db.DanhMucCongViec, "id", "ten");
            ViewBag.id_sanpham = new SelectList(db.SanPham, "id", "ten");
            return View();
        }

        // POST: CongViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,san_luong_thuc_te,so_lo,id_sanpham,id_danh_muc_cong_viec")] CongViec congViec)
        {
            if (ModelState.IsValid)
            {
                db.CongViec.Add(congViec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_danh_muc_cong_viec = new SelectList(db.DanhMucCongViec, "id", "ten", congViec.id_danh_muc_cong_viec);
            ViewBag.id_sanpham = new SelectList(db.SanPham, "id", "ten", congViec.id_sanpham);
            return View(congViec);
        }

        // GET: CongViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = db.CongViec.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_danh_muc_cong_viec = new SelectList(db.DanhMucCongViec, "id", "ten", congViec.id_danh_muc_cong_viec);
            ViewBag.id_sanpham = new SelectList(db.SanPham, "id", "ten", congViec.id_sanpham);
            return View(congViec);
        }

        // POST: CongViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,san_luong_thuc_te,so_lo,id_sanpham,id_danh_muc_cong_viec")] CongViec congViec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congViec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_danh_muc_cong_viec = new SelectList(db.DanhMucCongViec, "id", "ten", congViec.id_danh_muc_cong_viec);
            ViewBag.id_sanpham = new SelectList(db.SanPham, "id", "ten", congViec.id_sanpham);
            return View(congViec);
        }

        // GET: CongViecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = db.CongViec.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            return View(congViec);
        }

        // POST: CongViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongViec congViec = db.CongViec.Find(id);
            db.CongViec.Remove(congViec);
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
