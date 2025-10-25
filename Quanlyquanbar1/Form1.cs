using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlyquanbar1
{
    public partial class Form1 : Form
    {
        // Tạo 3 bảng dữ liệu tạm
        private DataTable doUongTable;
        private DataTable nhanVienTable;
        private DataTable khachHangTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo dữ liệu mẫu
            KhoiTaoBangDoUong();
            KhoiTaoBangNhanVien();
            KhoiTaoBangKhachHang();
        }

        // ========================= KHỞI TẠO BẢNG =========================
        private void KhoiTaoBangDoUong()
        {
            doUongTable = new DataTable();
            doUongTable.Columns.Add("ID", typeof(int));
            doUongTable.Columns.Add("TenDoUong", typeof(string));
            doUongTable.Columns.Add("LoaiDoUong", typeof(string));
            doUongTable.Columns.Add("DonGia", typeof(decimal));

            // Dữ liệu mẫu
            doUongTable.Rows.Add(1, "Cà phê sữa", "Cà phê", 25000);
            doUongTable.Rows.Add(2, "Trà đào", "Trà", 30000);
            doUongTable.Rows.Add(3, "Bia Tiger", "Bia", 35000);

            dgvDoUong.DataSource = doUongTable;
            dgvDoUong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KhoiTaoBangNhanVien()
        {
            nhanVienTable = new DataTable();
            nhanVienTable.Columns.Add("ID", typeof(int));
            nhanVienTable.Columns.Add("TenNV", typeof(string));
            nhanVienTable.Columns.Add("ChucVu", typeof(string));
            nhanVienTable.Columns.Add("Luong", typeof(decimal));

            nhanVienTable.Rows.Add(1, "Nguyễn Văn A", "Phục vụ", 5000000);
            nhanVienTable.Rows.Add(2, "Trần Thị B", "Thu ngân", 6000000);

            dgvNhanVien.DataSource = nhanVienTable;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void KhoiTaoBangKhachHang()
        {
            khachHangTable = new DataTable();
            khachHangTable.Columns.Add("ID", typeof(int));
            khachHangTable.Columns.Add("TenKH", typeof(string));
            khachHangTable.Columns.Add("SDT", typeof(string));
            khachHangTable.Columns.Add("GhiChu", typeof(string));

            khachHangTable.Rows.Add(1, "Lê Minh", "0905123456", "Khách quen");
            khachHangTable.Rows.Add(2, "Phạm Hương", "0987456123", "VIP");

            dgvKhach.DataSource = khachHangTable;
            dgvKhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ========================= ĐỒ UỐNG =========================
        private void btnThemDU_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtGiaDU.Text, out decimal gia) || string.IsNullOrWhiteSpace(txtTenDU.Text) || string.IsNullOrWhiteSpace(cboLoaiDU.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ thông tin hợp lệ!");
                return;
            }

            int newId = doUongTable.Rows.Count > 0 ? (int)doUongTable.Compute("MAX(ID)", "") + 1 : 1;
            doUongTable.Rows.Add(newId, txtTenDU.Text.Trim(), cboLoaiDU.Text.Trim(), gia);
            ClearDoUongInputs();
        }

        private void btnSuaDU_Click(object sender, EventArgs e)
        {
            if (dgvDoUong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đồ uống cần sửa!");
                return;
            }

            DataRow row = ((DataRowView)dgvDoUong.CurrentRow.DataBoundItem).Row;
            row["TenDoUong"] = txtTenDU.Text.Trim();
            row["LoaiDoUong"] = cboLoaiDU.Text.Trim();
            row["DonGia"] = decimal.Parse(txtGiaDU.Text);
        }

        private void btnXoaDU_Click(object sender, EventArgs e)
        {
            if (dgvDoUong.CurrentRow == null) return;
            dgvDoUong.Rows.RemoveAt(dgvDoUong.CurrentRow.Index);
        }

        private void btnLamMoiDU_Click(object sender, EventArgs e)
        {
            ClearDoUongInputs();
        }

        private void ClearDoUongInputs()
        {
            txtTenDU.Clear();
            txtGiaDU.Clear();
            cboLoaiDU.SelectedIndex = -1;
        }

        // ========================= NHÂN VIÊN =========================
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtLuong.Text, out decimal luong) || string.IsNullOrWhiteSpace(txtTenNV.Text) || string.IsNullOrWhiteSpace(txtChucVu.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đúng thông tin nhân viên!");
                return;
            }

            int newId = nhanVienTable.Rows.Count > 0 ? (int)nhanVienTable.Compute("MAX(ID)", "") + 1 : 1;
            nhanVienTable.Rows.Add(newId, txtTenNV.Text.Trim(), txtChucVu.Text.Trim(), luong);
            ClearNhanVienInputs();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;
            DataRow row = ((DataRowView)dgvNhanVien.CurrentRow.DataBoundItem).Row;
            row["TenNV"] = txtTenNV.Text.Trim();
            row["ChucVu"] = txtChucVu.Text.Trim();
            row["Luong"] = decimal.Parse(txtLuong.Text);
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;
            dgvNhanVien.Rows.RemoveAt(dgvNhanVien.CurrentRow.Index);
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            ClearNhanVienInputs();
        }

        private void ClearNhanVienInputs()
        {
            txtTenNV.Clear();
            txtChucVu.Clear();
            txtLuong.Clear();
        }

        // ========================= KHÁCH HÀNG =========================
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên và số điện thoại!");
                return;
            }

            int newId = khachHangTable.Rows.Count > 0 ? (int)khachHangTable.Compute("MAX(ID)", "") + 1 : 1;
            khachHangTable.Rows.Add(newId, txtTenKH.Text.Trim(), txtSDT.Text.Trim(), txtGhiChu.Text.Trim());
            ClearKhachInputs();
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (dgvKhach.CurrentRow == null) return;
            DataRow row = ((DataRowView)dgvKhach.CurrentRow.DataBoundItem).Row;
            row["TenKH"] = txtTenKH.Text.Trim();
            row["SDT"] = txtSDT.Text.Trim();
            row["GhiChu"] = txtGhiChu.Text.Trim();
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (dgvKhach.CurrentRow == null) return;
            dgvKhach.Rows.RemoveAt(dgvKhach.CurrentRow.Index);
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            ClearKhachInputs();
        }

        private void ClearKhachInputs()
        {
            txtTenKH.Clear();
            txtSDT.Clear();
            txtGhiChu.Clear();
        }
    }
}
