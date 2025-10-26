using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Quanlyquanbar1.Models
{
    public partial class QuanLyQuanBarContext : DbContext
    {
        public QuanLyQuanBarContext()
            : base("name=QuanLyQuanBarContext")
        {
        }

        public virtual DbSet<DoUong> DoUongs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
