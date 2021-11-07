namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public partial class DetailCongNhanModel
    {
        public string tenCN { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string TenCa { get; set; }
        public TimeSpan thoi_gian_den { get; set; }
        public TimeSpan thoi_gian_ve { get; set; }
        public string cong_viec { get; set; }
        public int san_luong_thuc_te { get; set; }

    }
}