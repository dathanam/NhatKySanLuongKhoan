using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NKSLK_CS;
using PagedList;

namespace NKSLK_CS.Controllers
{
    public class CongNhansController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: CongNhans
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var CongNhan = db.CongNhan.Where(x => x.id != 0);
            ViewBag.phongban = new CongNhansController().Chucvu();
            ViewBag.phuong = new CongNhansController().getPhuong();
            return View(CongNhan.OrderByDescending(x => x.id).ToPagedList(page, pageSize));
        }

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
                             san_luong_thuc_te = DMCN.san_luong_thuc_te
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
            DanhMucCongNhanThucHienKhoan NK = db.DanhMucCongNhanThucHienKhoan.Where(x => x.id_cong_nhan == id).FirstOrDefault();
            if(NK != null)
            {
                TempData["msg"] = "<script>alert('Cong nhan da co NKSLK. Khong the xoa!');</script>";
            }   
            else
            {
                CongNhan congNhan = db.CongNhan.Find(id);
                db.CongNhan.Remove(congNhan);
                db.SaveChanges();
            }    

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
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,ten,ngay_sinh,gioi_tinh,chuc_vu,que_quan,luong_hop_dong,luong_bao_hiem,id_phong_ban,id_phuong")] CongNhan congNhan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(congNhan).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    ViewBag.id_phong_ban = new SelectList(db.PhongBan, "id", "ten", congNhan.id_phong_ban);
        //    ViewBag.id_phuong = new SelectList(db.Phuong, "id", "ten", congNhan.id_phuong);
        //    return RedirectToAction("Index");
        //}
        public ActionResult Edit(CongNhan congNhan)
        {
            var result = db.CongNhan.SingleOrDefault(b => b.id == congNhan.id);

            if (result != null)
            {
                result.ten = congNhan.ten;
                result.ngay_sinh = congNhan.ngay_sinh;
                result.chuc_vu = congNhan.chuc_vu;
                result.luong_hop_dong = congNhan.luong_hop_dong;
                result.luong_bao_hiem = congNhan.luong_bao_hiem;
                //    result.id_phong_ban = congNhan.id_phong_ban;
                //    result.id_phuong = congNhan.id_phuong;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string searchString, int category)
        {
            List<CongNhan> congNhan = new List<CongNhan>();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (category == 1)
                {
                    congNhan = db.CongNhan.Where(x => x.ten.Contains(searchString)).ToList();
                    ViewBag.phongban = new CongNhansController().Chucvu();
                    ViewBag.phuong = new CongNhansController().getPhuong();
                }
                else if (category == 2)
                {
                    congNhan = db.CongNhan.SqlQuery("select * from CongNhan where DATEDIFF(year,CongNhan.ngay_sinh,GETDATE()) =" + searchString).ToList();
                    ViewBag.phongban = new CongNhansController().Chucvu();
                    ViewBag.phuong = new CongNhansController().getPhuong();
                }
                else if (category == 3)
                {
                    congNhan = db.CongNhan.SqlQuery("select * from CongNhan, PhongBan where CongNhan.id_phong_ban = PhongBan.id and PhongBan.ten like N'%" + searchString + "%'").ToList();
                    ViewBag.phongban = new CongNhansController().Chucvu();
                    ViewBag.phuong = new CongNhansController().getPhuong();
                }
                else if (category == 4)
                {
                    congNhan = db.CongNhan.SqlQuery("select * from CongNhan, Phuong where CongNhan.id_phuong = Phuong.id and Phuong.ten like N'%" + searchString + "%'").ToList();
                    ViewBag.phongban = new CongNhansController().Chucvu();
                    ViewBag.phuong = new CongNhansController().getPhuong();
                }
            }
            else
            {
                congNhan = db.CongNhan.Where(x => x.id != 0).ToList();
                ViewBag.phongban = new CongNhansController().Chucvu();
                ViewBag.phuong = new CongNhansController().getPhuong();
            }
            return View("Index", congNhan);
        }

    }
}