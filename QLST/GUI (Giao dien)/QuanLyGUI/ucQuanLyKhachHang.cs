using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucQuanLyKhachHang : UserControl
    {
        private readonly KhachHang_BLL _bll = new KhachHang_BLL();
        private int _selectedID = -1;
        private bool _dangLoad = false;

        public ucQuanLyKhachHang()
        {
            InitializeComponent();
            LoadData();
        }

        // ✅ 1. Hiển thị danh sách khách hàng
        private void LoadData()
        {
            _dangLoad = true;
            dgvKH.Rows.Clear();

            var list = string.IsNullOrWhiteSpace(txtTimKiem.Text)
                ? _bll.GetAll()
                : _bll.Search(txtTimKiem.Text);

            foreach (var kh in list)
            {
                string hang = kh.DiemTichLuy >= 1000 ? "💎 VIP"
                            : kh.DiemTichLuy >= 500 ? "🥇 Vàng"
                            : kh.DiemTichLuy >= 100 ? "🏅 Bạc"
                            : "Thường";

                int idx = dgvKH.Rows.Add(kh.KhachHangID, kh.SDT, kh.TenKH,
                    kh.DiemTichLuy, kh.TongHoaDon,
                    string.Format("{0:N0} đ", kh.TongChiTieu), hang);

                // Tô màu theo hạng
                if (kh.DiemTichLuy >= 1000) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(230, 210, 255);
                else if (kh.DiemTichLuy >= 500) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
                else if (kh.DiemTichLuy >= 100) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            }

            _dangLoad = false;
        }

        // ✅ 2. Tìm kiếm theo tên/SĐT
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        // ✅ 3. Chọn khách hàng
        private void dgvKH_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangLoad || dgvKH.SelectedRows.Count == 0) return;

            var row = dgvKH.SelectedRows[0]; // DataGridViewRow
            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);

            txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtTenKH.Text = row.Cells["colTen"].Value?.ToString();
        }

        // ✅ 4. Sửa thông tin khách hàng
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var khDto = new KhachHang_DTO
            {
                KhachHangID = _selectedID,
                SDT = txtSDT.Text.Trim(),
                TenKH = txtTenKH.Text.Trim()
            };

            var (ok, msg) = _bll.Sua(khDto);

            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (ok)
            {
                ClearForm();
                LoadData();
            }
        }

        // ✅ 5. Làm mới form
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedID = -1;
            txtSDT.Text = "";
            txtTenKH.Text = "";
            txtTimKiem.Text = "";
            dgvKH.ClearSelection();
        }
    }
}