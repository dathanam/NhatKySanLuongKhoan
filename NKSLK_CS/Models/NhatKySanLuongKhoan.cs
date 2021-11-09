namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhatKySanLuongKhoan")]
    public partial class NhatKySanLuongKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhatKySanLuongKhoan()
        {
            SanLuongKhoanTheoCas = new HashSet<SanLuongKhoanTheoCa>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanLuongKhoanTheoCa> SanLuongKhoanTheoCas { get; set; }
    }
}
