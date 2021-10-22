namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CongViec = new HashSet<CongViec>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        public int? so_dang_ky { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngay_dang_ky { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngay_san_xuat { get; set; }

        [Column(TypeName = "date")]
        public DateTime han_su_dung { get; set; }

        [StringLength(200)]
        public string quy_cach { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongViec> CongViec { get; set; }
    }
}
