using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_.QuanLyGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucQuanLyNhanVien : UserControl
    {
        private QLNV_BLL _bll = new QLNV_BLL();
        private int _selectedID = 0;

        public ucQuanLyNhanVien()
        {
            // Bắt buộc gọi để vẽ giao diện từ file Designer
            InitializeComponent();

            CustomInitUI();
            cboLocRole.SelectedIndex = 0; // Mặc định chọn "Tất cả"

            // Chạy dữ liệu lần đầu luôn, không cần đợi ấn Lọc
            LoadData();
        }

        private void CustomInitUI()
        {
            // Chặn người dùng nhập Mã NV
            txtMaNV.ReadOnly = true;
            txtMaNV.Text = "(Tự động sinh)";

            // -----------------------------------------------------

            // Cấu hình các cột cơ bản
            dgvNV.Columns.Add("Ma", "Mã NV");
            dgvNV.Columns.Add("Ten", "Họ Tên");
            dgvNV.Columns.Add("User", "Username");
            dgvNV.Columns.Add("Role", "Vai trò");
            dgvNV.Columns.Add("Ca", "Ca làm");

            // CỘT NÚT ĐỘNG
            DataGridViewButtonColumn btnToggle = new DataGridViewButtonColumn
            {
                Name = "colToggle",
                HeaderText = "Trạng thái (Bật/Tắt)",
                Width = 140,
                FlatStyle = FlatStyle.Flat
            };
            dgvNV.Columns.Add(btnToggle);

            // Style màu sắc chuẩn
            dgvNV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgvNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvNV.EnableHeadersVisualStyles = false;
            dgvNV.RowTemplate.Height = 35; // Chỉnh cao lên chút cho nút dễ bấm
        }

        private void LoadData()
        {
            dgvNV.Rows.Clear();
            int roleFilter = cboLocRole.SelectedIndex;
            var list = _bll.Search(txtSearch.Text.Trim(), roleFilter, chkLocNghiViec.Checked);

            foreach (var nv in list)
            {
                string roleText = nv.Role == 1 ? "Admin" : nv.Role == 2 ? "Thu ngân" : nv.Role == 3 ? "Kho" : "Bảo vệ";

                int idx = dgvNV.Rows.Add(nv.MaNV, nv.TenNV, nv.Username, roleText, nv.CaLamViec);
                dgvNV.Rows[idx].Tag = nv;

                // XỬ LÝ NÚT TOGGLE ĐỘNG DỰA VÀO TRẠNG THÁI
                DataGridViewButtonCell btnCell = (DataGridViewButtonCell)dgvNV.Rows[idx].Cells["colToggle"];
                if (nv.TrangThai)
                {
                    btnCell.Value = "🟢 Đang hoạt động";
                    btnCell.Style.ForeColor = Color.DarkGreen;
                    btnCell.Style.SelectionForeColor = Color.DarkGreen;
                }
                else
                {
                    btnCell.Value = "🔴 Đã khóa";
                    btnCell.Style.ForeColor = Color.DarkRed;
                    btnCell.Style.SelectionForeColor = Color.DarkRed;
                    dgvNV.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(240, 220, 220); // Bôi nền hồng nhạt cho người đã khóa
                }
            }
            dgvNV.ClearSelection();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkShowPass.Checked;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Xác định trạng thái để lưu
            bool trangThaiLuu = true; // Mặc định thêm mới là Đang hoạt động
            if (_selectedID > 0 && dgvNV.CurrentRow != null)
            {
                // Nếu là Cập nhật, giữ nguyên trạng thái cũ của nhân viên đó
                trangThaiLuu = ((QLNV_DTO)dgvNV.CurrentRow.Tag).TrangThai;
            }

            var nv = new QLNV_DTO
            {
                NhanVienID = _selectedID,
                TenNV = txtTenNV.Text.Trim(),
                Username = txtUser.Text.Trim(),
                Password = txtPass.Text,
                Role = cboRole.SelectedIndex + 1,
                SoDienThoai = txtSDT.Text.Trim(),
                CaLamViec = cboCaLam.Text,
                TrangThai = trangThaiLuu
            };

            var (ok, msg) = _bll.Save(nv, _selectedID == 0);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (ok)
            {
                LoadData();
                btnClear_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _selectedID = 0;
            txtMaNV.Text = "(Tự động sinh)"; // Đổi lại thành gợi ý
            txtTenNV.Clear();
            txtUser.Clear();
            txtPass.Clear();
            txtSDT.Clear();
            cboRole.SelectedIndex = -1;
            cboCaLam.SelectedIndex = -1;
            dgvNV.ClearSelection();
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var nv = (QLNV_DTO)dgvNV.Rows[e.RowIndex].Tag;

            // NẾU CLICK VÀO CỘT TOGGLE (Cột Bật/Tắt)
            if (dgvNV.Columns[e.ColumnIndex].Name == "colToggle")
            {
                // Xác định hành động dựa trên trạng thái hiện tại
                string hanhDong = nv.TrangThai ? "KHÓA" : "MỞ KHÓA";
                string canhBao = nv.TrangThai ?
                                 $"Bạn có chắc muốn KHÓA tài khoản của '{nv.TenNV}'?" :
                                 $"Bạn có muốn MỞ KHÓA lại tài khoản cho '{nv.TenNV}'?";

                if (MessageBox.Show(canhBao, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Gọi BLL để đảo ngược trạng thái trong DB
                    _bll.ToggleTrangThai(nv.NhanVienID);

                    // Reload lại dữ liệu để Grid tự cập nhật màu và text
                    LoadData();
                }
                return; // Thoát ra, không đẩy dữ liệu lên Form bên trái
            }

            // Nếu click vào các cột khác thì Map data lên Form để Sửa (như cũ)
            _selectedID = nv.NhanVienID;
            txtMaNV.Text = nv.MaNV; // Khi bấm vào Sửa, load Mã NV thật lên
            txtTenNV.Text = nv.TenNV;
            txtUser.Text = nv.Username;
            txtPass.Text = nv.Password;
            cboRole.SelectedIndex = nv.Role > 0 ? nv.Role - 1 : -1;
            txtSDT.Text = nv.SoDienThoai;
            cboCaLam.Text = nv.CaLamViec;
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}