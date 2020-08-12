namespace MutiShopDataContext.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Kho> Khoes { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<NhapKho> NhapKhoes { get; set; }
        public virtual DbSet<QuanLiKho> QuanLiKhoes { get; set; }
        public virtual DbSet<QuanTri> QuanTris { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<XuatKho> XuatKhoes { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.DanhMuc)
                .HasForeignKey(e => e.IdDanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DatHang>()
                .Property(e => e.MaDat)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.HoVaTen)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.IdKhachHang);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DatHangs)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.NguoiDat);

            modelBuilder.Entity<Kho>()
                .HasMany(e => e.QuanLiKhoes)
                .WithOptional(e => e.Kho)
                .HasForeignKey(e => e.IdKho);

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSanPham)
                .HasForeignKey(e => e.IdLoaiSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuanLiKho>()
                .HasMany(e => e.NhapKhoes)
                .WithOptional(e => e.QuanLiKho)
                .HasForeignKey(e => e.IdQuanLiKho);

            modelBuilder.Entity<QuanLiKho>()
                .HasMany(e => e.XuatKhoes)
                .WithOptional(e => e.QuanLiKho)
                .HasForeignKey(e => e.IdQuanLiKho);

            modelBuilder.Entity<QuanTri>()
                .Property(e => e.HoVaTen)
                .IsFixedLength();

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.NhapKhoes)
                .WithOptional(e => e.QuanTri)
                .HasForeignKey(e => e.NguoiNhap);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.QuanLiKhoes)
                .WithOptional(e => e.QuanTri)
                .HasForeignKey(e => e.NguoiSua);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.QuanLiKhoes1)
                .WithOptional(e => e.QuanTri1)
                .HasForeignKey(e => e.NguoiThem);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.PhanQuyens)
                .WithOptional(e => e.QuanTri)
                .HasForeignKey(e => e.IdQuanTri);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.QuanTri)
                .HasForeignKey(e => e.NguoiSua);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.SanPhams1)
                .WithOptional(e => e.QuanTri1)
                .HasForeignKey(e => e.NguoiThem);

            modelBuilder.Entity<QuanTri>()
                .HasMany(e => e.XuatKhoes)
                .WithOptional(e => e.QuanTri)
                .HasForeignKey(e => e.NguoiXuatKho);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.PhanQuyens)
                .WithOptional(e => e.Quyen)
                .HasForeignKey(e => e.IdQuyen);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.IdSanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.DatHangs)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.IdSanPham);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.QuanLiKhoes)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.IdSanPham);
        }
    }
}
