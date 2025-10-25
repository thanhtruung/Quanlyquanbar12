namespace Quanlyquanbar1.entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoUong")]
    public partial class DoUong
    {
        [Key]
        public int MaDoUong { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDoUong { get; set; }

        [StringLength(50)]
        public string LoaiDoUong { get; set; }

        public decimal? DonGia { get; set; }
    }
}
