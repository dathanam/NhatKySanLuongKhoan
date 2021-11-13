namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucCongNhanThucHienKhoan")]
    public partial class DanhMucCongNhanThucHienKhoan
    {
        public int id { get; set; }

        public TimeSpan thoi_gian_den { get; set; }

        public TimeSpan thoi_gian_ve { get; set; }

        public int? id_cong_nhan { get; set; }

        public int? id_san_luong_khoan_theo_ca { get; set; }

        public int? id_cong_viec { get; set; }

        public int san_luong_thuc_te { get; set; }

        public virtual CongNhan CongNhan { get; set; }

        public virtual CongViec CongViec { get; set; }

        public virtual SanLuongKhoanTheoCa SanLuongKhoanTheoCa { get; set; }
    }
}
