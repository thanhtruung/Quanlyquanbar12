using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlyquanbar1
{
    public partial class Form1 : Form
    {
        // 3 bảng dữ liệu
        private DataTable doUongTable;
        private DataTable nhanVienTable;
        private DataTable khachHangTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KhoiTaoBangDoUong();
            KhoiTaoBangNhanVien();
            KhoiTaoBangKhachHang();

            // Gán sự kiện chọn dòng cho 3 bảng
            dgvDoUong.CellClick += dgvDoUong_CellClick;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            dgvKhach.CellClick += dgvKhach_CellClick;
        }

        // ========================= KHỞI TẠO BẢNG =========================
        private void KhoiTaoBangDoUong()
        {
            doUongTable = new DataTable();
            doUongTable.Columns.Add("ID", typeof(int));
            doUongTable.Columns.Add("TenDoUong", typeof(string));
            doUongTable.Columns.Add("LoaiDoUong", typeof(string));
            doUongTable.Columns.Add("DonGia", typeof(decimal));

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
            if (!decimal.TryParse(txtGiaDU.Text, out decimal gia) ||
                string.IsNullOrWhiteSpace(txtTenDU.Text) ||
                string.IsNullOrWhiteSpace(cboLoaiDU.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ thông tin hợp lệ!");
                return;
            }

            int newId = doUongTable.Rows.Count > 0 ? (int)doUongTable.Compute("MAX(ID)", "") + 1 : 1;
            doUongTable.Rows.Add(newId, txtTenDU.Text.Trim(), cboLoaiDU.Text.Trim(), gia);

            ClearDoUongInputs();
            dgvDoUong.ClearSelection();
        }

        private void btnSuaDU_Click(object sender, EventArgs e)
        {
            if (dgvDoUong.CurrentRow == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn đồ uống cần sửa!");
                return;
            }

            if (!decimal.TryParse(txtGiaDU.Text, out decimal gia))
            {
                MessageBox.Show("⚠️ Đơn giá không hợp lệ!");
                return;
            }

            // Lấy dòng được chọn
            int index = dgvDoUong.CurrentRow.Index;
            if (index >= 0 && index < doUongTable.Rows.Count)
            {
                doUongTable.Rows[index]["TenDoUong"] = txtTenDU.Text.Trim();
                doUongTable.Rows[index]["LoaiDoUong"] = cboLoaiDU.Text.Trim();
                doUongTable.Rows[index]["DonGia"] = gia;

                // Cập nhật lại giao diện
                dgvDoUong.Refresh();
                MessageBox.Show("✅ Sửa đồ uống thành công!");
            }
        }


        private void btnXoaDU_Click(object sender, EventArgs e)
        {
            if (dgvDoUong.CurrentRow == null) return;
            dgvDoUong.Rows.RemoveAt(dgvDoUong.CurrentRow.Index);
            ClearDoUongInputs();
        }

        private void btnLamMoiDU_Click(object sender, EventArgs e)
        {
            ClearDoUongInputs();
            dgvDoUong.ClearSelection();
        }

        private void dgvDoUong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvDoUong.Rows[e.RowIndex].IsNewRow) return;
            var bound = dgvDoUong.Rows[e.RowIndex].DataBoundItem;
            if (bound == null) return;

            DataRow row = ((DataRowView)bound).Row;
            txtTenDU.Text = row["TenDoUong"].ToString();
            cboLoaiDU.Text = row["LoaiDoUong"].ToString();
            txtGiaDU.Text = row["DonGia"].ToString();
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
            if (!decimal.TryParse(txtLuong.Text, out decimal luong) ||
                string.IsNullOrWhiteSpace(txtTenNV.Text) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đúng thông tin nhân viên!");
                return;
            }

            int newId = nhanVienTable.Rows.Count > 0 ? (int)nhanVienTable.Compute("MAX(ID)", "") + 1 : 1;
            nhanVienTable.Rows.Add(newId, txtTenNV.Text.Trim(), txtChucVu.Text.Trim(), luong);
            ClearNhanVienInputs();
            dgvNhanVien.ClearSelection();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            if (!decimal.TryParse(txtLuong.Text, out decimal luong))
            {
                MessageBox.Show("⚠️ Lương không hợp lệ!");
                return;
            }

            int index = dgvNhanVien.CurrentRow.Index;
            if (index >= 0 && index < nhanVienTable.Rows.Count)
            {
                nhanVienTable.Rows[index]["TenNV"] = txtTenNV.Text.Trim();
                nhanVienTable.Rows[index]["ChucVu"] = txtChucVu.Text.Trim();
                nhanVienTable.Rows[index]["Luong"] = luong;

                dgvNhanVien.Refresh();
                MessageBox.Show("✅ Sửa nhân viên thành công!");
            }
        }


        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;
            dgvNhanVien.Rows.RemoveAt(dgvNhanVien.CurrentRow.Index);
            ClearNhanVienInputs();
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            ClearNhanVienInputs();
            dgvNhanVien.ClearSelection();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvNhanVien.Rows[e.RowIndex].IsNewRow) return;
            var bound = dgvNhanVien.Rows[e.RowIndex].DataBoundItem;
            if (bound == null) return;

            DataRow row = ((DataRowView)bound).Row;
            txtTenNV.Text = row["TenNV"].ToString();
            txtChucVu.Text = row["ChucVu"].ToString();
            txtLuong.Text = row["Luong"].ToString();
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
            dgvKhach.ClearSelection();
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (dgvKhach.CurrentRow == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            int index = dgvKhach.CurrentRow.Index;
            if (index >= 0 && index < khachHangTable.Rows.Count)
            {
                khachHangTable.Rows[index]["TenKH"] = txtTenKH.Text.Trim();
                khachHangTable.Rows[index]["SDT"] = txtSDT.Text.Trim();
                khachHangTable.Rows[index]["GhiChu"] = txtGhiChu.Text.Trim();

                dgvKhach.Refresh();
                MessageBox.Show("✅ Sửa khách hàng thành công!");
            }
        }


        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (dgvKhach.CurrentRow == null) return;
            dgvKhach.Rows.RemoveAt(dgvKhach.CurrentRow.Index);
            ClearKhachInputs();
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            ClearKhachInputs();
            dgvKhach.ClearSelection();
        }

        private void dgvKhach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvKhach.Rows[e.RowIndex].IsNewRow) return;
            var bound = dgvKhach.Rows[e.RowIndex].DataBoundItem;
            if (bound == null) return;

            DataRow row = ((DataRowView)bound).Row;
            txtTenKH.Text = row["TenKH"].ToString();
            txtSDT.Text = row["SDT"].ToString();
            txtGhiChu.Text = row["GhiChu"].ToString();
        }

        private void ClearKhachInputs()
        {
            txtTenKH.Clear();
            txtSDT.Clear();
            txtGhiChu.Clear();
        }
    }
}
