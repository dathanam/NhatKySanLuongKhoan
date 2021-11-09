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
            var NKSLK = db.Database.SqlQuery<NKSLKModel>("Select NhatKySanLuongKhoan.ngay,SanLuongKhoanTheoCa.id_ca as ca, DanhMucCongViec.ten as congViec, SanPham.ten as sanPham, DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca as idSanLuongKhoan, DanhMucCongNhanThucHienKhoan.id_cong_viec as idCongViec, Count(DanhMucCongNhanThucHienKhoan.id_cong_nhan) as soLuongCongNhan, Sum(CongViec.san_luong_thuc_te) as sanLuong " +
                                                        "from NhatKySanLuongKhoan, DanhMucCongViec, SanPham, DanhMucCongNhanThucHienKhoan, CongViec, SanLuongKhoanTheoCa "+
                                                        "where NhatKySanLuongKhoan.id = SanLuongKhoanTheoCa.id_nkslk " +
                                                        "and SanLuongKhoanTheoCa.id = DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca "+
                                                        "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id "+
                                                        "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id "+
                                                        "and CongViec.id_sanpham = SanPham.id "+
                                                        "Group by NhatKySanLuongKhoan.ngay, DanhMucCongViec.ten, SanPham.ten, DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca").ToList();

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
        public ActionResult Create(DateTime ngayTao, int ca, int congViec, int sanPham, int soLo, int congNhan)
        {
            NhatKySanLuongKhoan NK = new NhatKySanLuongKhoan();
            NK.ngay = ngayTao;
            db.NhatKySanLuongKhoan.Add(NK);
            db.SaveChanges();

            SanLuongKhoanTheoCa SLKTC = new SanLuongKhoanTheoCa();
            var MaxNkslk = db.NhatKySanLuongKhoan.Where(p => p.id > 0).Max(p => p.id);
            SLKTC.id_nkslk = MaxNkslk;
            SLKTC.id_ca = ca;
            db.SanLuongKhoanTheoCa.Add(SLKTC);
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

        public ActionResult AddCN(int idCV, int idSLK)
        {
            ViewBag.idCV = idCV;
            ViewBag.idSLK = idSLK;
            ViewBag.congNhan = new NKSLKController().getCongNhan();
            return PartialView("PartialAddCN");
        }
        [HttpPost]
        public ActionResult AddCN(int idCV, int idSLK, int idCongNhan)
        {
            var congViec = db.CongViec.Where(x => x.id == idCV).FirstOrDefault();
            CongViec CV = new CongViec();
            CV.id_danh_muc_cong_viec = congViec.id_danh_muc_cong_viec;
            CV.id_sanpham = congViec.id_sanpham;
            CV.so_lo = congViec.so_lo;
            CV.san_luong_thuc_te = 0;
            db.CongViec.Add(CV);
            db.SaveChanges();

            DanhMucCongNhanThucHienKhoan DMCN = new DanhMucCongNhanThucHienKhoan();
            DMCN.id_cong_nhan = idCongNhan; 
            DMCN.id_cong_viec = db.CongViec.Where(p => p.id > 0).Max(p => p.id);
            DMCN.id_san_luong_khoan_theo_ca = idSLK;
            db.DanhMucCongNhanThucHienKhoan.Add(DMCN);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detail(DateTime NKSLK, String DMCV, string SP, int ca)
        {
            var detail = (from CN in db.CongNhan
                         join DMCN in db.DanhMucCongNhanThucHienKhoan on CN.id equals DMCN.id_cong_nhan
                         join CV in db.CongViec on DMCN.id_cong_viec equals CV.id
                         join DMCVV in db.DanhMucCongViec on CV.id_danh_muc_cong_viec equals DMCVV.id
                         join SPP in db.SanPham on CV.id_sanpham equals SPP.id
                         join SLK in db.SanLuongKhoanTheoCa on DMCN.id_san_luong_khoan_theo_ca equals SLK.id
                         join NKSL in db.NhatKySanLuongKhoan on SLK.id_nkslk equals NKSL.id
                         where NKSL.ngay == NKSLK
                         where DMCVV.ten == DMCV
                         where SPP.ten == SP
                         where SLK.id_ca == ca

                         select new NKSLKDetail
                         {
                             tenCongNhan = CN.ten,
                             thoi_gian_den = DMCN.thoi_gian_den,
                             thoi_gian_ve = DMCN.thoi_gian_ve,
                             sanLuong = CV.san_luong_thuc_te
                         }).ToList();
            return PartialView("PartialDetail", detail);
        }
        public ActionResult Delete(DateTime NKSLK, String DMCV, string SP, int ca, int idCongViec, int idSanLuongKhoan)
        {
            ViewBag.NKSLK = NKSLK;
            ViewBag.DMCV = DMCV;
            ViewBag.SP = SP;
            ViewBag.ca = ca;
            ViewBag.idCongViec = idCongViec;
            ViewBag.idSanLuongKhoan = idSanLuongKhoan;

            return PartialView("PartialDelete");  
        }

        [HttpPost]
        public ActionResult Delete(int idCongViec, int idSanLuongKhoan)
        {
            //var DMCNTHK = db.DanhMucCongNhanThucHienKhoan.SqlQuery("select id from DanhMucCongNhanThucHienKhoan where id_cong_viec = "+idCongViec+ " and id_san_luong_khoan_theo_ca = "+idSanLuongKhoan).ToList();
            return PartialView("Index");
        }
    }
}