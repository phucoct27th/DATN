namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            BinhLuans = new HashSet<BinhLuan>();
            DatHangs = new HashSet<DatHang>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string HoVaTen { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Không được để trống!")]
        public string TenDangNhap { get; set; }

        [StringLength(30)]
        [Required]
        public string MatKhau { get; set; }

        [StringLength(15)]
        [Required]
        public string SoDienThoai { get; set; }

        [StringLength(30)]
        [Required]
        public string Email { get; set; }

        [StringLength(200)]
        [Required]
        public string DiaChi { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }
    }
}
