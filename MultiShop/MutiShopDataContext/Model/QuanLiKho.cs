namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuanLiKho")]
    public partial class QuanLiKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuanLiKho()
        {
            NhapKhoes = new HashSet<NhapKho>();
            XuatKhoes = new HashSet<XuatKho>();
        }

        public int Id { get; set; }

        public int? IdKho { get; set; }

        public int? IdSanPham { get; set; }

        public int? NguoiNhap { get; set; }

        public DateTime? NgayNhap { get; set; }

        public int? SoLuong { get; set; }

        public int? NguoiSua { get; set; }

        public DateTime? NgaySua { get; set; }

        public int? NguoiThem { get; set; }

        public DateTime? NgayThem { get; set; }

        public virtual Kho Kho { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhapKho> NhapKhoes { get; set; }

        public virtual QuanTri QuanTri { get; set; }

        public virtual QuanTri QuanTri1 { get; set; }

        public virtual SanPham SanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XuatKho> XuatKhoes { get; set; }
    }
}
