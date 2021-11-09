namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanLuongKhoanTheoCa")]
    public partial class SanLuongKhoanTheoCa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanLuongKhoanTheoCa()
        {
            DanhMucCongNhanThucHienKhoans = new HashSet<DanhMucCongNhanThucHienKhoan>();
        }

        public int id { get; set; }

        public int? id_ca { get; set; }

        public int? id_nkslk { get; set; }

        public virtual CaLamViec CaLamViec { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMucCongNhanThucHienKhoan> DanhMucCongNhanThucHienKhoans { get; set; }

        public virtual NhatKySanLuongKhoan NhatKySanLuongKhoan { get; set; }
    }
}
