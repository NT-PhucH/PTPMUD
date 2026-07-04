using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucNhapHang : UserControl
    {
        // Giữ nguyên 100% kết nối DB lúc đầu của bạn
        private readonly Kho_BLL _bll = new Kho_BLL();
        private readonly SanPham_BLL _spBll = new SanPham_BLL();
        private readonly NhaCungCap_BLL _nccBll = new NhaCungCap_BLL();
        private List<ChiTietPhieuNhap_DTO> _gioNhap = new List<ChiTietPhieuNhap_DTO>();

        public ucNhapHang()
        {
            InitializeComponent();
            WireEvents();

            // Đăng ký sự kiện Load thay vì gọi DB trực tiếp ở đây
            this.Load += UcNhapHang_Load;
        }

        private void UcNhapHang_Load(object sender, EventArgs e)
        {
            // Safeguard: Chỉ kết nối DB khi ứng dụng thực sự chạy (Runtime)
            // Ngăn Visual Studio tự động gọi DB khi đang xem tab [Design]
            if (!this.DesignMode && LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                LoadComboData();
            }
        }

        private void WireEvents()
        {
            btnThemNCC.Click += BtnThemNCC_Click;
            btnThemSPNhap.Click += BtnThemSP_Click;
            chkNSX.CheckedChanged += (s, e) => dtpNSX.Enabled = chkNSX.Checked;
            chkHSD.CheckedChanged += (s, e) => dtpHSD.Enabled = chkHSD.Checked;
            btnThemVaoGioNhap.Click += BtnThemVaoGioNhap_Click;
            btnXoaDongNhap.Click += BtnXoaDongNhap_Click;
            btnLuuPhieuNhap.Click += BtnLuuPhieuNhap_Click;
        }

        private void LoadComboData()
        {
            cboNCC.DataSource = _nccBll.GetAll();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "NhaCungCapID";
            RefreshComboSanPham();
        }

        private void RefreshComboSanPham()
        {
            var spList = _spBll.GetAll();
            cboSPNhap.DataSource = new List<SanPham_DTO>(spList);
            cboSPNhap.DisplayMember = "TenSP";
            cboSPNhap.ValueMember = "SanPhamID";
        }

        private void BtnThemNCC_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo UserControl của bạn
            ucThemNCC ucThem = new ucThemNCC();
            ucThem.Dock = DockStyle.Fill; // Cho tự động lấp đầy cửa sổ

            // 2. Tạo một Form trống để "chứa" (host) UserControl này
            using (Form popup = new Form())
            {
                popup.Text = "Thêm Nhà Cung Cấp Mới";
                // Kích thước form nên nhỉnh hơn UserControl một chút để bù trừ viền cửa sổ
                popup.Size = new System.Drawing.Size(366, 480);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.FormBorderStyle = FormBorderStyle.FixedDialog; // Form cứng, không cho thu phóng
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                // 3. Nhét UserControl vào Form
                popup.Controls.Add(ucThem);

                // 4. Mở Form lên dưới dạng Dialog (Chờ người dùng đóng lại thì code mới chạy tiếp)
                popup.ShowDialog();
            } // Lệnh using sẽ tự động dọn dẹp Form khỏi bộ nhớ sau khi đóng

            // 5. Ngay khi Form popup đóng, tiến hành load lại Combo và chọn item mới nhất
            LoadComboData();
            if (cboNCC.Items.Count > 0)
            {
                cboNCC.SelectedIndex = cboNCC.Items.Count - 1;
            }
        }

        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo UserControl của bạn
            ucThemSP ucThem = new ucThemSP();
            ucThem.Dock = DockStyle.Fill; // Cho tự động lấp đầy cửa sổ

            // 2. Tạo một Form trống để "chứa" (host) UserControl này
            using (Form popup = new Form())
            {
                popup.Text = "Thêm Sản Phẩm Mới";
                // Kích thước form nên nhỉnh hơn UserControl một chút để bù trừ viền cửa sổ
                popup.Size = new System.Drawing.Size(366, 500);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.FormBorderStyle = FormBorderStyle.FixedDialog; // Form cứng, không cho thu phóng
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                // 3. Nhét UserControl vào Form
                popup.Controls.Add(ucThem);

                // 4. Mở Form lên dưới dạng Dialog (Chờ người dùng đóng lại thì code mới chạy tiếp)
                popup.ShowDialog();
            } // Lệnh using sẽ tự động dọn dẹp Form khỏi bộ nhớ sau khi đóng

            // 5. Ngay khi Form popup đóng, tiến hành load lại Combo và chọn item mới nhất
            LoadComboData();
            if (cboNCC.Items.Count > 0)
            {
                cboNCC.SelectedIndex = cboNCC.Items.Count - 1;
            }
        }

        private void BtnThemVaoGioNhap_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra ComboBox Nhà cung cấp trước tiên
            if (cboNCC.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gọi BLL kiểm tra trạng thái giao dịch (Real-time DB check)
            var ncc = (NhaCungCap_DTO)cboNCC.SelectedItem;
            if (!_nccBll.KiemTraNhaCungCapKhaDung(ncc.NhaCungCapID))
            {
                MessageBox.Show($"Nhà cung cấp '{ncc.TenNCC}' hiện đã ngừng giao dịch.\nVui lòng chọn nhà cung cấp khác!", "Ngừng giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 3. Các bước kiểm tra dữ liệu giỏ hàng như cũ
            if (cboSPNhap.SelectedItem == null) { MessageBox.Show("Vui lòng chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!int.TryParse(txtSLNhap.Text.Trim(), out int sl) || sl <= 0) { MessageBox.Show("Số lượng nhập không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string giaRaw = txtGiaNhap.Text.Trim().Replace(",", "").Replace(".", "").Replace(" ", "");
            if (!long.TryParse(giaRaw, out long gia) || gia <= 0) { MessageBox.Show("Giá nhập không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (chkHSD.Checked && chkNSX.Checked && dtpNSX.Value >= dtpHSD.Value) { MessageBox.Show("NSX phải trước HSD!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var sp = (SanPham_DTO)cboSPNhap.SelectedItem;
            int existIdx = _gioNhap.FindIndex(x => x.SanPhamID == sp.SanPhamID);

            if (existIdx >= 0)
            {
                var ans = MessageBox.Show($"Sản phẩm '{sp.TenSP}' đã có.\nChọn Yes = Cộng dồn SL | Chọn No = Hủy thao tác", "Trùng lặp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.No) return;
                if (ans == DialogResult.Yes) { _gioNhap[existIdx].SoLuongNhap += sl; RefreshGioNhap(); ResetFormNhap(); return; }
            }

            _gioNhap.Add(new ChiTietPhieuNhap_DTO
            {
                SanPhamID = sp.SanPhamID,
                TenSP = sp.TenSP,
                MaVach = sp.MaVach,
                SoLuongNhap = sl,
                GiaNhap = (int)gia,
                NSX = chkNSX.Checked ? dtpNSX.Value.Date : (DateTime?)null,
                HSD = chkHSD.Checked ? dtpHSD.Value.Date : (DateTime?)null
            });

            RefreshGioNhap();
            ResetFormNhap();
        }

        private void BtnXoaDongNhap_Click(object sender, EventArgs e)
        {
            if (dgvGioNhap.SelectedRows.Count == 0) return;
            int idx = dgvGioNhap.SelectedRows[0].Index;
            _gioNhap.RemoveAt(idx);
            RefreshGioNhap();
        }

        private void BtnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedItem == null) { MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (_gioNhap.Count == 0) { MessageBox.Show("Giỏ hàng đang trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            long tong = 0;
            foreach (var ct in _gioNhap) tong += (long)ct.SoLuongNhap * ct.GiaNhap;

            var phieu = new PhieuNhap_DTO
            {
                NhaCungCapID = ((NhaCungCap_DTO)cboNCC.SelectedItem).NhaCungCapID,
                NhanVienID = 1, // Fix SessionManager.NhanVienDangNhap sau
                TongTienThanhToan = tong
            };

            var (ok, msg) = _bll.TaoPhieuNhap(phieu, _gioNhap);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (ok)
            {
                _gioNhap.Clear();
                RefreshGioNhap();
                RefreshComboSanPham();
            }
        }

        private void RefreshGioNhap()
        {
            dgvGioNhap.Rows.Clear();
            long tong = 0;
            foreach (var ct in _gioNhap)
            {
                tong += (long)ct.SoLuongNhap * ct.GiaNhap;
                dgvGioNhap.Rows.Add(
                    ct.TenSP,
                    ct.SoLuongNhap,
                    string.Format("{0:N0} đ", ct.GiaNhap),
                    ct.NSX.HasValue ? ct.NSX.Value.ToString("dd/MM/yyyy") : "—",
                    ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—"
                );
            }
            lblTongTienNhap.Text = $"Tổng tiền:  {tong:N0} đ";
        }

        private void ResetFormNhap()
        {
            txtSLNhap.Text = "1";
            txtGiaNhap.Text = "";
            chkNSX.Checked = false;
            chkHSD.Checked = false;
        }

    }
}