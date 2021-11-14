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
            ViewBag.Inner = new ThongKeLuongsController().getNKSLKTest();
            return View();
        }
        public List<TKLuongSanPhamModel> getNKSLKTest()
        {
            var rs = db.Database.SqlQuery<TKLuongSanPhamModel>("with Cau12(SoCongNhan, idCongViec, idCa, Ngay, tongTG) " +
                                             "as(select Count(DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca) as SoCongNhan, " +
                                             "DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay," +
                                             "Sum(dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) as tongTG " +
                                             "from DanhMucCongNhanThucHienKhoan, SanLuongKhoanTheoCa, NhatKySanLuongKhoan" +
                                             "where DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id" +
                                             "group by DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca, DanhMucCongNhanThucHienKhoan.id_cong_viec, SanLuongKhoanTheoCa.id_ca, NhatKySanLuongKhoan.ngay)" +
                                             "select CongNhan.id, CongNhan.ten, SUM(((DanhMucCongNhanThucHienKhoan.san_luong_thuc_te * DanhMucCongViec.don_gia) * dbo.SoGioLam(DanhMucCongNhanThucHienKhoan.thoi_gian_den, DanhMucCongNhanThucHienKhoan.thoi_gian_ve)) / Cau12.tongTG) as luongSanPham" +
                                             "from CongNhan, DanhMucCongNhanThucHienKhoan, CongViec, DanhMucCongViec, NhatKySanLuongKhoan, SanLuongKhoanTheoCa, Cau12" +
                                             "where CongNhan.id = DanhMucCongNhanThucHienKhoan.id_cong_nhan" +
                                             "and DanhMucCongNhanThucHienKhoan.id_cong_viec = CongViec.id" +
                                             "and CongViec.id_danh_muc_cong_viec = DanhMucCongViec.id" +
                                             "and DanhMucCongNhanThucHienKhoan.id_san_luong_khoan_theo_ca = SanLuongKhoanTheoCa.id" +
                                             "and SanLuongKhoanTheoCa.id_nkslk = NhatKySanLuongKhoan.id" +
                                             "and Cau12.idCongViec = CongViec.id" +
                                             "and Cau12.Ngay = NhatKySanLuongKhoan.ngay" +
                                             //"and NhatKySanLuongKhoan.Ngay between dbo.firstday('" + fisrtDay + "') and dbo.lastday('" + lastDay + "')" +
                                             "Group by CongNhan.id, CongNhan.ten").ToList();
            return rs;
        }   
    }
}