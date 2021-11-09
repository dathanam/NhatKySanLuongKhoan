namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phuong")]
    public partial class Phuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phuong()
        {
            CongNhans = new HashSet<CongNhan>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string ten { get; set; }

        public int? id_quan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongNhan> CongNhans { get; set; }

        public virtual Quan Quan { get; set; }
    }
}
