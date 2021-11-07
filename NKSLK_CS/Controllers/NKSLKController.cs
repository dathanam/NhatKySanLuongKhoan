using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class NKSLKController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();
        // GET: NKSLK
        public ActionResult Index()
        {
            var NKSLK = db.Database.SqlQuery<NKSLKModel>("Select NhatKySanLuongKhoan.ngay, DanhMucCongViec.ten as congViec, SanPham.ten as sanPham, Count(DanhMucCongNhanThucHienKhoan.id_cong_nhan) as soLuongCongNhan, Sum(CongViec.san_luong_thuc_te) as sanLuong " +
                                                        "from NhatKySanLuongKhoan, DanhMucCongViec, SanPham, DanhMucCongNhanThucHienKhoan, CongViec, SanLuongKhoanTheoCa "+
                                                        "where NhatKySanLuongKhoan.id = SanLuongKhoanTheoCa.id_nkslk " +
                                                        "and SanLuongKhoanTheoCa.id = DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca "+
                                                        "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id "+
                                                        "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id "+
                                                        "and CongViec.id_sanpham = SanPham.id "+
                                                        "Group by NhatKySanLuongKhoan.ngay, DanhMucCongViec.ten, SanPham.ten").ToList();

            ViewBag.danhMucCongViec = new NKSLKController().getDanhMucCongViec();
            ViewBag.sanPham = new NKSLKController().getSanPham();
            ViewBag.congNhan = new NKSLKController().getCongNhan();

            return View(NKSLK);
        }

        public List<DanhMucCongViec> getDanhMucCongViec()
        {
            var model = db.DanhMucCongViec.ToList();
            return model;
        }
        public List<SanPham> getSanPham()
        {
            var model = db.SanPham.ToList();
            return model;
        }
        public List<CongNhan> getCongNhan()
        {
            var model = db.CongNhan.ToList();
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DateTime ngay, int ca, int congViec, int sanPham, int soLo, int congNhan)
        {
            NhatKySanLuongKhoan NK = new NhatKySanLuongKhoan();
            NK.ngay = ngay;
            db.NhatKySanLuongKhoan.Add(NK);
            db.SaveChanges();

            SanLuongKhoanTheoCa SLKTC = new SanLuongKhoanTheoCa();
            var MaxNkslk = db.NhatKySanLuongKhoan.Where(p => p.id > 0).Max(p => p.id);
            SLKTC.id_nkslk = MaxNkslk;
            SLKTC.id_ca = ca;
            db.NhatKySanLuongKhoan.Add(NK);
            db.SaveChanges();

            CongViec CV = new CongViec();
            CV.id_danh_muc_cong_viec = congViec;
            CV.id_sanpham = sanPham;
            CV.so_lo = soLo;
            CV.san_luong_thuc_te = 0;
            db.CongViec.Add(CV);
            db.SaveChanges();

            DanhMucCongNhanThucHienKhoan DMCN = new DanhMucCongNhanThucHienKhoan();
            DMCN.id_cong_nhan = congNhan;
            DMCN.id_san_luong_khoan_theo_ca = db.SanLuongKhoanTheoCa.Where(p => p.id > 0).Max(p => p.id);
            DMCN.id_cong_viec = db.CongViec.Where(p => p.id > 0).Max(p => p.id);
            db.DanhMucCongNhanThucHienKhoan.Add(DMCN);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}