namespace MutiShopDataContext.Model
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
            BinhLuans = new HashSet<BinhLuan>();
            DatHangs = new HashSet<DatHang>();
            QuanLiKhoes = new HashSet<QuanLiKho>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSanPham { get; set; }

        public int IdDanhMuc { get; set; }

        public int IdLoaiSanPham { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string MauSac { get; set; }

        [Required]
        [StringLength(50)]
        public string Size { get; set; }

        [Column(TypeName = "ntext")]
        public string Anh { get; set; }

        [Column(TypeName = "ntext")]
        public string Mota { get; set; }

        public bool TrangThai { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NoiDung { get; set; }

        public DateTime? NgayThem { get; set; }

        public int? NguoiThem { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? NguoiSua { get; set; }

        public int? Gia { get; set; }
        public int? LuotXem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanLiKho> QuanLiKhoes { get; set; }

        public virtual QuanTri QuanTri { get; set; }

        public virtual QuanTri QuanTri1 { get; set; }
    }
}
