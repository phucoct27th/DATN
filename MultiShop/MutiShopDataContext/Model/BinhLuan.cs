
namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        public int Id { get; set; }

        public int? IdSanPham { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public int? IdKhachHang { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
