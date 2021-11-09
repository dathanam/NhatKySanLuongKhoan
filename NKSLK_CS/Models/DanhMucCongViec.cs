namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucCongViec")]
    public partial class DanhMucCongViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucCongViec()
        {
            CongViecs = new HashSet<CongViec>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        public double dinh_muc_khoan { get; set; }

        [Required]
        [StringLength(50)]
        public string don_vi_khoan { get; set; }

        public double he_so_khoan { get; set; }

        public double dinh_muc_lao_dong { get; set; }

        public double? don_gia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongViec> CongViecs { get; set; }
    }
}
