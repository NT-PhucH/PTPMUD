using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucXuatKho : UserControl
    {
        private readonly Kho_BLL _bll = new Kho_BLL();
        private readonly SanPham_BLL _spBll = new SanPham_BLL();
        private List<ChiTietPhieuXuat_DTO> _gioXuat = new List<ChiTietPhieuXuat_DTO>();

        public ucXuatKho()
        {
            InitializeComponent();
            WireEvents();

            // Đăng ký sự kiện Load để gọi dữ liệu, tránh lỗi Design Time
            this.Load += UcXuatKho_Load;
        }

        private void UcXuatKho_Load(object sender, EventArgs e)
        {
            // Safeguard: Chỉ kết nối DB khi ứng dụng thực sự chạy
            if (!this.DesignMode && LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                LoadComboData();
            }
        }

        private void WireEvents()
        {
            btnThemSPXuat.Click += BtnThemSP_Click;
            btnThemVaoGioXuat.Click += BtnThemVaoGioXuat_Click;
            btnXoaDongXuat.Click += BtnXoaDongXuat_Click;
            btnLuuPhieuXuat.Click += BtnLuuPhieuXuat_Click;
        }

        private void LoadComboData()
        {
            RefreshComboSanPham();
        }

        private void RefreshComboSanPham()
        {
            var spList = _spBll.GetAll();
            cboSPXuat.DataSource = new List<SanPham_DTO>(spList);
            cboSPXuat.DisplayMember = "TenSP";
            cboSPXuat.ValueMember = "SanPhamID";
        }

        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo UserControl ucThemSP
            ucThemSP ucThem = new ucThemSP();
            ucThem.Dock = DockStyle.Fill;

            // 2. Tạo một Form popup để chứa UserControl
            using (Form popup = new Form())
            {
                popup.Text = "Thêm Sản Phẩm Mới";
                popup.Size = new System.Drawing.Size(366, 500);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                // 3. Đưa UserControl vào Form
                popup.Controls.Add(ucThem);

                // 4. Mở Form dưới dạng Dialog
                popup.ShowDialog();
            }

            // 5. Load lại dữ liệu và chọn sản phẩm mới nhất
            RefreshComboSanPham();
            if (cboSPXuat.Items.Count > 0)
            {
                cboSPXuat.SelectedIndex = cboSPXuat.Items.Count - 1;
            }
        }

        private void BtnThemVaoGioXuat_Click(object sender, EventArgs e)
        {
            if (cboSPXuat.SelectedItem == null) { MessageBox.Show("Vui lòng chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!int.TryParse(txtSLXuat.Text.Trim(), out int sl) || sl <= 0) { MessageBox.Show("Số lượng xuất không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var sp = (SanPham_DTO)cboSPXuat.SelectedItem;
            if (sl > sp.TonKhoTong) { MessageBox.Show($"Số lượng xuất ({sl}) vượt tồn kho ({sp.TonKhoTong})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            _gioXuat.Add(new ChiTietPhieuXuat_DTO
            {
                SanPhamID = sp.SanPhamID,
                TenSP = sp.TenSP,
                MaVach = sp.MaVach,
                SoLuongXuat = sl,
                TonKhoHienTai = sp.TonKhoTong,
                GhiChu = txtGhiChuXuat.Text.Trim()
            });

            RefreshGioXuat();
            txtSLXuat.Text = "1";
            txtGhiChuXuat.Text = "";
        }

        private void BtnXoaDongXuat_Click(object sender, EventArgs e)
        {
            if (dgvGioXuat.SelectedRows.Count == 0) return;
            int idx = dgvGioXuat.SelectedRows[0].Index;
            _gioXuat.RemoveAt(idx);
            RefreshGioXuat();
        }

        private void BtnLuuPhieuXuat_Click(object sender, EventArgs e)
        {
            if (_gioXuat.Count == 0) { MessageBox.Show("Giỏ hàng xuất đang trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var phieu = new PhieuXuat_DTO
            {
                LyDo = cboLyDo.SelectedItem?.ToString() ?? "",
                NhanVienID = 1, // Fix SessionManager sau
                GhiChu = txtGhiChuPhieuXuat.Text.Trim()
            };

            var (ok, msg) = _bll.TaoPhieuXuat(phieu, _gioXuat);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (ok)
            {
                _gioXuat.Clear();
                RefreshGioXuat();
                RefreshComboSanPham();
            }
        }

        private void RefreshGioXuat()
        {
            dgvGioXuat.Rows.Clear();
            foreach (var ct in _gioXuat)
            {
                dgvGioXuat.Rows.Add(ct.TenSP, ct.SoLuongXuat, ct.TonKhoHienTai, ct.GhiChu);
            }
        }

    }
}