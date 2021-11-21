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
    public class DanhMucCongViecsController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: DanhMucCongViecs
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var DanhMucCongViec = db.DanhMucCongViec.Where(x => x.id != 0);
            return View(DanhMucCongViec.OrderByDescending(x => x.id).ToPagedList(page, pageSize));
        }

        // GET: DanhMucCongViecs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMucCongViec model)
        {
            db.DanhMucCongViec.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: DanhMucCongViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
            if (danhMucCongViec == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", danhMucCongViec);
        }

        // POST: DanhMucCongViecs/Edit/5
        [HttpPost]
        public ActionResult Edit(DanhMucCongViec danhMucCongViec)
        {
            var result = db.DanhMucCongViec.SingleOrDefault(b => b.id == danhMucCongViec.id);

            if (result != null)
            {
                result.ten = danhMucCongViec.ten;
                result.dinh_muc_khoan = danhMucCongViec.dinh_muc_khoan;
                result.don_vi_khoan = danhMucCongViec.don_vi_khoan;
                result.he_so_khoan = danhMucCongViec.he_so_khoan;
                result.dinh_muc_lao_dong = danhMucCongViec.dinh_muc_lao_dong;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
            if (danhMucCongViec == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", danhMucCongViec);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DanhMucCongNhanThucHienKhoan NK = db.DanhMucCongNhanThucHienKhoan.Where(x => x.id_cong_nhan == id).FirstOrDefault();
            if (NK != null)
            {
                TempData["msg"] = "<script>alert('Danh muc cong viec da co trong NKSLK. Khong the xoa!');</script>";
            }
            else
            {
                DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
                db.DanhMucCongViec.Remove(danhMucCongViec);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public List<SLKCV_Max> NKSKLMax()
        {
            var rs = db.Database.SqlQuery<SLKCV_Max>("With CV(id ,SoLuong) as " +
                                                            "(select DanhMucCongNhanThucHienKhoan.id_cong_viec, COUNT(DanhMucCongNhanThucHienKhoan.id_cong_viec) from DanhMucCongNhanThucHienKhoan " +
                                                            "Group by DanhMucCongNhanThucHienKhoan.id_cong_viec) " +
                                                            "select DanhMucCongViec.ten, DanhMucCongViec.dinh_muc_khoan, DanhMucCongViec.don_vi_khoan, DanhMucCongViec.he_so_khoan, DanhMucCongViec.dinh_muc_lao_dong, DanhMucCongViec.don_gia " +
                                                            "from DanhMucCongViec, CV " +
                                                            "where DanhMucCongViec.id = CV.id " +
                                                            "and CV.SoLuong = (select MAX(CV.SoLuong) from CV)").ToList();
            return rs;
        }

        [HttpPost]
        public ActionResult Search(string searchString, int category, int page = 1, int pageSize = 5)
        {
            List<DanhMucCongViec> danhMucCongViec = new List<DanhMucCongViec>();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (category == 1)
                {
                    danhMucCongViec = db.DanhMucCongViec.Where(x => x.ten.Contains(searchString)).ToList();
                }
                else if(category == 2)
                {
                    danhMucCongViec = db.DanhMucCongViec.SqlQuery("select * from DanhMucCongViec where DanhMucCongViec.don_gia >=" + searchString).ToList();
                }
                else if(category == 3)
                {
                    danhMucCongViec = db.DanhMucCongViec.SqlQuery("select * from DanhMucCongViec where DanhMucCongViec.don_gia <=" + searchString).ToList();
                }
            }
            else
            {
                if (category == 4)
                {
                    danhMucCongViec = db.DanhMucCongViec.SqlQuery("select * from DanhMucCongViec where DanhMucCongViec.don_gia = (select max(don_gia) from DanhMucCongViec)").ToList();
                }
                else if (category == 5)
                {
                    danhMucCongViec = db.DanhMucCongViec.SqlQuery("select * from DanhMucCongViec where DanhMucCongViec.don_gia = (select min(don_gia) from DanhMucCongViec)").ToList();
                }
                else if (category == 6)
                {
                    ViewBag.Inner = new DanhMucCongViecsController().NKSKLMax();
                    return View("NKSLK_Max");
                }
                else
                {
                    danhMucCongViec = db.DanhMucCongViec.Where(x => x.id != 0).ToList();
                }

            }
            return View("Index", danhMucCongViec.OrderByDescending(x => x.id).ToPagedList(page, pageSize));
        }
    }
}
