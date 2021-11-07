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

        // GET: CongNhans/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CongNhan congNhan = db.CongNhan.Find(id);
        //    if (congNhan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(congNhan);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CongNhan congNhan)
        {
            if (ModelState.IsValid)
            {
                db.CongNhan.Add(congNhan);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(congNhan);
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


        public ActionResult Details(int? id)
        {
            ViewBag.Inner = new CongNhansController().getNKSLKTest(id);
            return View();
        }
        public List<DetailCongNhanModel> getNKSLKTest(int? id)
        {
            var inner = (from CN in db.CongNhan
                         join DMCN in db.DanhMucCongNhanThucHienKhoan on CN.id equals DMCN.id_cong_nhan
                         join CV in db.CongViec on DMCN.id_cong_viec equals CV.id
                         join DMCV in db.DanhMucCongViec on CV.id_danh_muc_cong_viec equals DMCV.id
                         join SLTC in db.SanLuongKhoanTheoCa on DMCN.id_san_luong_khoan_theo_ca equals SLTC.id
                         join CLV in db.CaLamViec on SLTC.id_ca equals CLV.id
                         join NKSLK in db.NhatKySanLuongKhoan on SLTC.id_nkslk equals NKSLK.id
                         where CN.id == id
                         select new DetailCongNhanModel
                         {
                             tenCN = CN.ten,
                             NgayLamViec = NKSLK.ngay,
                             TenCa = CLV.ten,
                             thoi_gian_den = DMCN.thoi_gian_den,
                             thoi_gian_ve = DMCN.thoi_gian_ve,
                             cong_viec = DMCV.ten,
                             san_luong_thuc_te = CV.san_luong_thuc_te
                         }).ToList();
            return inner;
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