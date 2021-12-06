namespace NKSLK_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongNhan")]
    public partial class CongNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongNhan()
        {
            DanhMucCongNhanThucHienKhoan = new HashSet<DanhMucCongNhanThucHienKhoan>();
            TaiKhoan = new HashSet<TaiKhoan>();
        }


        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngay_sinh { get; set; }

        [StringLength(10)]
        public string gioi_tinh { get; set; }

        [Required]
        [StringLength(100)]
        public string chuc_vu { get; set; }

        [StringLength(200)]
        public string que_quan { get; set; }

        public double luong_hop_dong { get; set; }

        public double luong_bao_hiem { get; set; }

        public int? id_phong_ban { get; set; }

        public int? id_phuong { get; set; }

        public virtual PhongBan PhongBan { get; set; }

        public virtual Phuong Phuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMucCongNhanThucHienKhoan> DanhMucCongNhanThucHienKhoan { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoan { get; set; }
    }
}
