using NKSLK_CS.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NKSLK_CS
{
    public partial class NKSLK_Context : DbContext
    {
        public NKSLK_Context()
            : base("name=NKSLK_Context")
        {
        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<CaLamViec> CaLamViec { get; set; }
        public virtual DbSet<CongNhan> CongNhan { get; set; }
        public virtual DbSet<CongViec> CongViec { get; set; }
        public virtual DbSet<DanhMucCongNhanThucHienKhoan> DanhMucCongNhanThucHienKhoan { get; set; }
        public virtual DbSet<DanhMucCongViec> DanhMucCongViec { get; set; }
        public virtual DbSet<NhatKySanLuongKhoan> NhatKySanLuongKhoan { get; set; }
        public virtual DbSet<PhongBan> PhongBan { get; set; }
        public virtual DbSet<Phuong> Phuong { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<SanLuongKhoanTheoCa> SanLuongKhoanTheoCa { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<ThanhPho> ThanhPho { get; set; }

        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }

        public virtual DbSet <nhom> nhom { get; set; }

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
                .HasMany(e => e.SanLuongKhoanTheoCa)
                .WithOptional(e => e.CaLamViec)
                .HasForeignKey(e => e.id_ca);

            modelBuilder.Entity<CongNhan>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoan)
                .WithOptional(e => e.CongNhan)
                .HasForeignKey(e => e.id_cong_nhan);

            modelBuilder.Entity<CongNhan>()
                .HasMany(e => e.TaiKhoan)
                .WithOptional(e => e.CongNhan)
                .HasForeignKey(e => e.id_cong_nhan);


            modelBuilder.Entity<CongViec>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoan)
                .WithOptional(e => e.CongViec)
                .HasForeignKey(e => e.id_cong_viec);

            modelBuilder.Entity<DanhMucCongNhanThucHienKhoan>()
                .Property(e => e.thoi_gian_den)
                .HasPrecision(2);

            modelBuilder.Entity<DanhMucCongNhanThucHienKhoan>()
                .Property(e => e.thoi_gian_ve)
                .HasPrecision(2);

            modelBuilder.Entity<DanhMucCongViec>()
                .HasMany(e => e.CongViec)
                .WithOptional(e => e.DanhMucCongViec)
                .HasForeignKey(e => e.id_danh_muc_cong_viec);

            modelBuilder.Entity<NhatKySanLuongKhoan>()
                .HasMany(e => e.SanLuongKhoanTheoCa)
                .WithOptional(e => e.NhatKySanLuongKhoan)
                .HasForeignKey(e => e.id_nkslk);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.CongNhan)
                .WithOptional(e => e.PhongBan)
                .HasForeignKey(e => e.id_phong_ban);

            modelBuilder.Entity<Phuong>()
                .HasMany(e => e.CongNhan)
                .WithOptional(e => e.Phuong)
                .HasForeignKey(e => e.id_phuong);

            modelBuilder.Entity<Quan>()
                .HasMany(e => e.Phuong)
                .WithOptional(e => e.Quan)
                .HasForeignKey(e => e.id_quan);

            modelBuilder.Entity<SanLuongKhoanTheoCa>()
                .HasMany(e => e.DanhMucCongNhanThucHienKhoan)
                .WithOptional(e => e.SanLuongKhoanTheoCa)
                .HasForeignKey(e => e.id_san_luong_khoan_theo_ca);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CongViec)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.id_sanpham);

            modelBuilder.Entity<ThanhPho>()
                .HasMany(e => e.Quan)
                .WithOptional(e => e.ThanhPho)
                .HasForeignKey(e => e.id_thanh_pho);
            modelBuilder.Entity<nhom>()
                .HasMany(e => e.TaiKhoan)
                .WithOptional(e => e.nhom)
                .HasForeignKey(e => e.id_nhom);

        }
    }
}
