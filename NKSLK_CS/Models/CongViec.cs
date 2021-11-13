namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongViec")]
    public partial class CongViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongViec()
        {
            DanhMucCongNhanThucHienKhoan = new HashSet<DanhMucCongNhanThucHienKhoan>();
        }

        public int id { get; set; }

        public int so_lo { get; set; }

        public int? id_sanpham { get; set; }

        public int? id_danh_muc_cong_viec { get; set; }

        public virtual DanhMucCongViec DanhMucCongViec { get; set; }

        public virtual SanPham SanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMucCongNhanThucHienKhoan> DanhMucCongNhanThucHienKhoan { get; set; }
    }
}
