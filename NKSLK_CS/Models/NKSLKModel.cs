namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class NKSLKModel
    {
        public DateTime ngay { get; set; }
        public string congViec { get; set; }
        public string sanPham { get; set; }
        public int soLuongCongNhan { get; set; }
        public int sanLuong { get; set; }
    }
}
