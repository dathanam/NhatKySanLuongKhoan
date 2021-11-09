namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaLamViec")]
    public partial class CaLamViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaLamViec()
        {
            SanLuongKhoanTheoCas = new HashSet<SanLuongKhoanTheoCa>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string ten { get; set; }

        public TimeSpan thoi_gian_bat_dau { get; set; }

        public TimeSpan thoi_gian_ket_thuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanLuongKhoanTheoCa> SanLuongKhoanTheoCas { get; set; }
    }
}
