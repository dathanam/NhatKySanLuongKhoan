using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NKSLK_CS.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CaLamViec> CaLamViecs { get; set; }
        public virtual DbSet<CongNhan> CongNhans { get; set; }
        public virtual DbSet<CongViec> CongViecs { get; set; }
        public virtual DbSet<DanhMucCongNhanThucHienKhoan> DanhMucCongNhanThucHienKhoans { get; set; }
        public virtual DbSet<DanhMucCongViec> DanhMucCongViecs { get; set; }
        public virtual DbSet<NhatKySanLuongKhoan> NhatKySanLuongKhoans { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<Phuong> Phuongs { get; set; }
        public virtual DbSet<Quan> Quans { get; set; }
        public virtual DbSet<SanLuongKhoanTheoCa> SanLuongKhoanTheoCas { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<taikhoan> taikhoans { get; set; }
        public virtual DbSet<ThanhPho> ThanhPhoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CaLamViec>()
                .Property(e => e.ten)
                .IsUnicode(false);

            modelBuilder.Entity<CaLamViec>()
                .Property(e => e.thoi_gian_bat_dau)
                .HasPrecision(2);

            modelBuilder.Entity<CaLamViec>()
                .Property(e => e.thoi_gian_ket_thuc)
                .HasPrecision(2);

            modelBuilder.Entity<CaLamViec>()
                .HasMany(e => e.SanLuongKhoanTheoCas)
                .WithOptional(e => e.CaLamViec)
                .HasForeignKey(e => e.id_ca);

            modelBuilder.Entity<CongNhan>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoans)
                .WithOptional(e => e.CongNhan)
                .HasForeignKey(e => e.id_cong_nhan);

            modelBuilder.Entity<CongNhan>()
                .HasMany(e => e.taikhoans)
                .WithRequired(e => e.CongNhan)
                .HasForeignKey(e => e.id_cong_nhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CongViec>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoans)
                .WithOptional(e => e.CongViec)
                .HasForeignKey(e => e.id_cong_viec);

            modelBuilder.Entity<DanhMucCongNhanThucHienKhoan>()
                .Property(e => e.thoi_gian_den)
                .HasPrecision(2);

            modelBuilder.Entity<DanhMucCongNhanThucHienKhoan>()
                .Property(e => e.thoi_gian_ve)
                .HasPrecision(2);

            modelBuilder.Entity<DanhMucCongViec>()
                .HasMany(e => e.CongViecs)
                .WithOptional(e => e.DanhMucCongViec)
                .HasForeignKey(e => e.id_danh_muc_cong_viec);

            modelBuilder.Entity<NhatKySanLuongKhoan>()
                .HasMany(e => e.SanLuongKhoanTheoCas)
                .WithOptional(e => e.NhatKySanLuongKhoan)
                .HasForeignKey(e => e.id_nkslk);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.CongNhans)
                .WithOptional(e => e.PhongBan)
                .HasForeignKey(e => e.id_phong_ban);

            modelBuilder.Entity<Phuong>()
                .HasMany(e => e.CongNhans)
                .WithOptional(e => e.Phuong)
                .HasForeignKey(e => e.id_phuong);

            modelBuilder.Entity<Quan>()
                .HasMany(e => e.Phuongs)
                .WithOptional(e => e.Quan)
                .HasForeignKey(e => e.id_quan);

            modelBuilder.Entity<SanLuongKhoanTheoCa>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoans)
                .WithOptional(e => e.SanLuongKhoanTheoCa)
                .HasForeignKey(e => e.id_san_luong_khoan_theo_ca);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CongViecs)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.id_sanpham);

            modelBuilder.Entity<ThanhPho>()
                .HasMany(e => e.Quans)
                .WithOptional(e => e.ThanhPho)
                .HasForeignKey(e => e.id_thanh_pho);
        }
    }
}
