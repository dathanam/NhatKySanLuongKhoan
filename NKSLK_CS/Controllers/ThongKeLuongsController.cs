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
    public class ThongKeLuongsController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();
        // GET: ThongKeLuongTheoTuan
        public ActionResult Index()
        {
            ViewBag.Inner = new ThongKeLuongsController().ThongKeLuong();
            return View();
        }
        public List<TKLuongSanPhamModel> ThongKeLuong()
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) as " +
                "( " +
                    "select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                    "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay, " +
                    "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                    "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan " +
                    "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                    "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id " +
                    "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay" +
                 ") " +
                "select CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> ThongKeLuongTheoTen(string searchString)
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) as " +
                "( " +
                    "select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                    "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay, " +
                    "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                    "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan " +
                    "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                    "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id " +
                    "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay" +
                 ") " +
                "select CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and CongNhan.ten like N'%" + searchString + "%'" +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> ThongKeLuongTheoTuan(string searchString)
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) as " +
                "( " +
                    "select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                    "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay, " +
                    "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                    "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan " +
                    "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                    "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id " +
                    "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay" +
                 ") " +
                "select CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and NhatKySanLuongKhoan.Ngay between dbo.firstday('" + searchString + "') and dbo.lastday('" + searchString + "') " +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> ThongKeLuongTheoThang(string searchString)
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) as " +
                "( " +
                    "select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                    "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay, " +
                    "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                    "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan " +
                    "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                    "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id " +
                    "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay" +
                 ") " +
                "select CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and month(Cau12.Ngay) = " + searchString +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> DetailLuong(int? id)
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) as " +
                "( " +
                    "select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                    "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay, " +
                    "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                    "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan " +
                    "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                    "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id " +
                    "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay" +
                 ") " +
                "select CongNhan.id, CongNhan.ten, NhatKySanLuongKhoan.ngay as NgayLamViec, ((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and CongNhan.id = " + id).ToList();
            return rs;
        }

        public ActionResult Details(int? id)
        {
            ViewBag.Inner = new ThongKeLuongsController().DetailLuong(id);
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchString, int category)
        {
            List<TKLuongSanPhamModel> TKLuong = new List<TKLuongSanPhamModel>();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (category == 1)
                {
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuongTheoTuan(searchString);
                }
                else if (category == 2)
                {
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuongTheoThang(searchString);
                }
                else if (category == 3)
                {
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuongTheoTen(searchString);
                }
            }
            else
            {
                ViewBag.Inner = new ThongKeLuongsController().ThongKeLuong();
            }
            return View("Index", TKLuong);
        }
    }
}