namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XuatKho")]
    public partial class XuatKho
    {
        public int Id { get; set; }

        public int? IdQuanLiKho { get; set; }

        public int? NguoiXuatKho { get; set; }

        public DateTime? NgayXuat { get; set; }

        public int? SoLuong { get; set; }

        public virtual QuanLiKho QuanLiKho { get; set; }

        public virtual QuanTri QuanTri { get; set; }
    }
}
