namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatHang")]
    public partial class DatHang
    {
        [Key]
        [StringLength(200)]
        public string MaDat { get; set; }

        public int? NguoiDat { get; set; }

        public int? IdSanPham { get; set; }

        public int? TrangThai { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(200)]
        public string DiaChiGiaoHang { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
