namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class NKSLKDetail
    {
        public string tenCongNhan { get; set; }

        public TimeSpan thoi_gian_den { get; set; }

        public TimeSpan thoi_gian_ve { get; set; }

        public int sanLuong { get; set; }

    }
}
