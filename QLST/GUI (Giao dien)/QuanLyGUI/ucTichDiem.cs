// ===================================================
// File: ucTichDiem.cs
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucTichDiem : UserControl
    {
        private readonly KhachHang_BLL _bll = new KhachHang_BLL();
        private int _selectedID = -1;
        private int _selectedDiem = 0;
        private bool _dangLoad = false;

        public ucTichDiem()
        {
            InitializeComponent();
            CustomInit();
            LoadData();
        }

        private void CustomInit()
        {
            // Cấu hình cột dgvKH
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", FillWeight = 5 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSDT", HeaderText = "SĐT", FillWeight = 16 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên KH", FillWeight = 25 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiem", HeaderText = "⭐ Điểm", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHD", HeaderText = "Số HĐ", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTong", HeaderText = "Tổng chi tiêu", FillWeight = 22, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHang", HeaderText = "Hạng", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            // Cấu hình cột dgvLichSu
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaHD", HeaderText = "Mã HĐ", FillWeight = 20 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTGian", HeaderText = "Thời gian", FillWeight = 28 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTien", HeaderText = "Tổng tiền", FillWeight = 28, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiemCong", HeaderText = "+Điểm", FillWeight = 14, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(34, 139, 34) } });
        }

        private void LoadData()
        {
            _dangLoad = true;
            dgvKH.Rows.Clear();
            var list = string.IsNullOrWhiteSpace(txtTimKiem.Text)
                ? _bll.GetAll() : _bll.Search(txtTimKiem.Text);

            int tongDiem = 0;
            foreach (var kh in list)
            {
                tongDiem += kh.DiemTichLuy;
                string hang = kh.DiemTichLuy >= 1000 ? "💎 VIP"
                            : kh.DiemTichLuy >= 500 ? "🥇 Vàng"
                            : kh.DiemTichLuy >= 100 ? "🏅 Bạc"
                            : "Thường";
                int idx = dgvKH.Rows.Add(kh.KhachHangID, kh.SDT, kh.TenKH,
                    kh.DiemTichLuy, kh.TongHoaDon,
                    string.Format("{0:N0} đ", kh.TongChiTieu), hang);

                if (kh.DiemTichLuy >= 1000) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(230, 210, 255);
                else if (kh.DiemTichLuy >= 500) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
                else if (kh.DiemTichLuy >= 100) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            }

            _dangLoad = false;
        }

        private void dgvKH_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangLoad || dgvKH.SelectedRows.Count == 0) return;
            var row = dgvKH.SelectedRows[0];
            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);
            _selectedDiem = Convert.ToInt32(row.Cells["colDiem"].Value);
            txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtTenKH.Text = row.Cells["colTen"].Value?.ToString();
            LoadLichSu();
        }

        private void LoadLichSu()
        {
            dgvLichSu.Rows.Clear();
            if (_selectedID <= 0) return;
            foreach (var ls in _bll.GetLichSu(_selectedID))
                dgvLichSu.Rows.Add(ls.MaHD, ls.ThoiGian.ToString("dd/MM/yyyy HH:mm"),
                    string.Format("{0:N0} đ", ls.TongTien), $"+{ls.DiemCong}");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn khách hàng cần sửa!"); return; }
            var (ok, msg) = _bll.Sua(new KhachHang_DTO { KhachHangID = _selectedID, SDT = txtSDT.Text.Trim(), TenKH = txtTenKH.Text.Trim() });
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            _selectedID = -1; _selectedDiem = 0;
            txtSDT.Text = txtTenKH.Text = "";
            dgvLichSu.Rows.Clear();
            dgvKH.ClearSelection();
        }


    }
}