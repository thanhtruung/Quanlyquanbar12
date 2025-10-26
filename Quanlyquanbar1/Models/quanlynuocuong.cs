using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Quanlyquanbar1.Models;

namespace Quanlyquanbar1.entities
{
    public partial class quanlynuocuong : DbContext
    {
        public quanlynuocuong()
            : base("name=quanlynuocuong")
        {
        }

        public virtual DbSet<DoUong> DoUong { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);
        }
    }
}
