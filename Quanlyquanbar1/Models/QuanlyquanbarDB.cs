using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Quanlyquanbar1.Models
{
    public partial class QuanlyquanbarDB : DbContext
    {
        public QuanlyquanbarDB()
            : base("name=QuanlyquanbarDB")
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
