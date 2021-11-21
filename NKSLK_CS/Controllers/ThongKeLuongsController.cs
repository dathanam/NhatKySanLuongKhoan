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
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> ThongKeLuongMax()
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
                "select Top 1 CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
                "Group by CongNhan.id, CongNhan.ten " +
                "Order by luongSanPham DESC").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> ThongKeLuongMin()
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
                "select Top 1 CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
                "Group by CongNhan.id, CongNhan.ten " +
                "Order by luongSanPham ASC").ToList();
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
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
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
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
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
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
                "and month(NhatKySanLuongKhoan.Ngay) = " + searchString +
                "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }

        public List<TKLuongSanPhamModel> DetailLuongTheoThang(int? id, int month1)
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
                "select CongNhan.id, CongNhan.ten, NhatKySanLuongKhoan.ngay as NgayLamViec, SanLuongKhoanTheoCa.id_ca as CaLamViec, DanhMucCongViec.ten as TenCV, " +
                " ((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve) / Cau12.tongTG) as luongSanPham " +
                "from CongNhan, CaLamViec, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12 " +
                "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan " +
                "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id " +
                "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id " +
                "and SanLuongKhoanTheoCa.id_ca = CaLamViec.id " +
                "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id " +
                "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id and Cau12.idCongViec = CongViec.id " +
                "and Cau12.Ngay = NhatKySanLuongKhoan.ngay " +
                "and Cau12.idCa = CaLamViec.id " +
                "and CongNhan.id = " + id +
                " and month(NhatKySanLuongKhoan.ngay) = " + month1).ToList();
            return rs;
        }

        public ActionResult Details(int? id)
        {
            int month1;
            month1 = Convert.ToInt32(TempData["thang"]);
            ViewBag.Inner = new ThongKeLuongsController().DetailLuongTheoThang(id, month1);
            return View();
        }


        [HttpPost]
        public ActionResult Search(string searchString, int category)
        {
            TempData["thang"] = searchString;
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
                if (category == 4)
                {
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuongMax();
                }
                else if (category == 5)
                {
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuongMin();
                }
                else
                    ViewBag.Inner = new ThongKeLuongsController().ThongKeLuong();
            }
            return View("Index", TKLuong);
        }
    }
}