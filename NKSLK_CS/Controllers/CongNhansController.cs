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
    public class CongNhansController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: CongNhans
        public ActionResult Index()
        {
            var CongNhan = db.CongNhan.Where(x => x.id != 0);
            ViewBag.phongban = new CongNhansController().Chucvu();
            ViewBag.phuong = new CongNhansController().getPhuong();
            return View(CongNhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CongNhan model)
        {
            db.CongNhan.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<PhongBan> Chucvu()
        {
            var model = db.PhongBan.ToList();
            return model;
        }

        public List<Phuong> getPhuong()
        {
            var model = db.Phuong.ToList();
            return model;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNhan congNhan = db.CongNhan.Find(id);
            if (congNhan == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", congNhan);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CongNhan congNhan = db.CongNhan.Find(id);
            db.CongNhan.Remove(congNhan);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNhan congNhan = db.CongNhan.Find(id);
            if (congNhan == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", congNhan);
        }

        [HttpPost]
        public ActionResult Edit(CongNhan congNhan)
        {
            var result = db.CongNhan.SingleOrDefault(b => b.id == congNhan.id);

            if (result != null)
            {
                result.ten = congNhan.ten;
                result.ngay_sinh = congNhan.ngay_sinh;
                result.PhongBan.ten = congNhan.PhongBan.ten;
                result.chuc_vu = congNhan.chuc_vu;
                result.luong_hop_dong = congNhan.luong_hop_dong;
                result.luong_bao_hiem = congNhan.luong_bao_hiem;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}