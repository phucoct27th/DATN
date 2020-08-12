namespace MutiShopDataContext.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanQuyen")]
    public partial class PhanQuyen
    {
        public int Id { get; set; }

        public int? IdQuyen { get; set; }

        public int? IdQuanTri { get; set; }

        public virtual QuanTri QuanTri { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
