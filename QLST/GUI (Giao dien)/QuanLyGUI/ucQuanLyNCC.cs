using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucQuanLyNCC : UserControl
    {
        private readonly NhaCungCap_BLL _bll = new NhaCungCap_BLL();
        private int _selectedID = -1;

        public ucQuanLyNCC()
        {
            InitializeComponent();
            RegisterEvents();
            LoadData();
        }

        private void RegisterEvents()
        {
            // Bỏ sự kiện TextChanged để chỉ lọc khi ấn nút
            btnLoc.Click += (s, e) => LoadData();

            dgvNCC.SelectionChanged += DgvNCC_SelectionChanged;
            dgvNCC.CellClick += DgvNCC_CellClick;
            btnSua.Click += BtnSua_Click;
            btnLamMoi.Click += (s, e) => ClearForm();
        }

        private void LoadData()
        {
            dgvNCC.SelectionChanged -= DgvNCC_SelectionChanged;
            dgvNCC.Rows.Clear();

            // 1. Lấy danh sách từ BLL
            var list = string.IsNullOrWhiteSpace(txtTimKiem.Text)
                ? _bll.GetAll() : _bll.Search(txtTimKiem.Text);

            // 2. Logic ẩn NCC đã ngừng giao dịch nếu không tích checkbox
            if (!chkHienNgungGD.Checked)
            {
                list = list.Where(n => n.TrangThai == true).ToList();
            }

            long tongTien = 0;
            foreach (var n in list)
            {
                tongTien += n.TongTienNhap;

                int idx = dgvNCC.Rows.Add(
                    n.NhaCungCapID, n.MaNCC, n.TenNCC, n.SoDienThoai,
                    n.DiaChi, n.TongPhieuNhap, string.Format("{0:N0} đ", n.TongTienNhap)
                );

                DataGridViewButtonCell btnCell = (DataGridViewButtonCell)dgvNCC.Rows[idx].Cells["colTrangThai"];
                if (n.TrangThai)
                {
                    btnCell.Value = "🟢 Hoạt động";
                    btnCell.Style.ForeColor = Color.DarkGreen;
                    btnCell.Style.SelectionForeColor = Color.DarkGreen;
                    dgvNCC.Rows[idx].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    btnCell.Value = "🔴 Ngừng GD";
                    btnCell.Style.ForeColor = Color.DarkRed;
                    btnCell.Style.SelectionForeColor = Color.DarkRed;
                    dgvNCC.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(250, 220, 220);
                }
            }

            lblTongNCC.Text = $"Tổng NCC: {list.Count}";
            lblTongTien.Text = $"Tổng tiền nhập: {tongTien:N0} đ";

            dgvNCC.ClearSelection();
            ClearForm();
            dgvNCC.SelectionChanged += DgvNCC_SelectionChanged;
        }

        private void DgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvNCC.Columns[e.ColumnIndex].Name == "colTrangThai")
            {
                int id = Convert.ToInt32(dgvNCC.Rows[e.RowIndex].Cells["colID"].Value);
                string tenNCC = dgvNCC.Rows[e.RowIndex].Cells["colTen"].Value.ToString();
                string trangThaiText = dgvNCC.Rows[e.RowIndex].Cells["colTrangThai"].Value.ToString();

                bool dangHoatDong = trangThaiText.Contains("Hoạt động");

                string hanhDong = dangHoatDong ? "NGỪNG GIAO DỊCH" : "MỞ HOẠT ĐỘNG LẠI";
                string canhBao = dangHoatDong ?
                                 $"Bạn có chắc muốn NGỪNG GIAO DỊCH với nhà cung cấp '{tenNCC}'?" :
                                 $"Bạn có muốn MỞ LẠI GIAO DỊCH với nhà cung cấp '{tenNCC}'?";

                if (MessageBox.Show(canhBao, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var (ok, msg) = _bll.ThayDoiTrangThai(id);
                    if (ok)
                    {
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DgvNCC_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNCC.SelectedRows.Count == 0) return;
            var row = dgvNCC.SelectedRows[0];

            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);
            txtMaNCC.Text = row.Cells["colMa"].Value?.ToString();
            txtTenNCC.Text = row.Cells["colTen"].Value?.ToString();
            txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtDiaChi.Text = row.Cells["colDC"].Value?.ToString();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn NCC cần sửa trên bảng điều khiển trước!"); return; }
            var dto = BuildDTO();
            dto.NhaCungCapID = _selectedID;

            var (ok, msg) = _bll.Sua(dto);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { LoadData(); }
        }

        private NhaCungCap_DTO BuildDTO() => new NhaCungCap_DTO
        {
            TenNCC = txtTenNCC.Text.Trim(),
            SoDienThoai = txtSDT.Text.Trim(),
            DiaChi = txtDiaChi.Text.Trim()
        };

        private void ClearForm()
        {
            _selectedID = -1;
            txtMaNCC.Text = txtTenNCC.Text = txtSDT.Text = txtDiaChi.Text = "";
            dgvNCC.ClearSelection();
        }
    }
}