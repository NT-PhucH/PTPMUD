// ===================================================
// File: frmQuanLyKho.cs (Logic)
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.BLL__Bat_ngoai_le_.Core;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class frmQuanLyKho : Form
    {
        // ── TIER FIX: CHỈ ĐƯỢC GỌI BLL ────────────────────────────────────
        private readonly Kho_BLL _bll = new Kho_BLL();
        private readonly SanPham_BLL _spBll = new SanPham_BLL();
        private readonly NhaCungCap_BLL _nccBll = new NhaCungCap_BLL();
        private readonly Setting_BLL _settingBll = new Setting_BLL();

        private bool _dangLoad = false;
        private List<ChiTietPhieuNhap_DTO> _gioNhap = new List<ChiTietPhieuNhap_DTO>();
        private List<ChiTietPhieuXuat_DTO> _gioXuat = new List<ChiTietPhieuXuat_DTO>();
        private Button[] _navButtons;

        private int _nguongHetHang = 10;
        private int _nguongHetHan = 30;

        public frmQuanLyKho()
        {
            InitializeComponent();
            WireEvents();
            LoadSettings();
            LoadComboData();
            LoadLichSuAll();
            LoadCanhBao();
        }

        private void WireEvents()
        {
            FindBtn("btnThemVaoGioNhap").Click += BtnThemVaoGioNhap_Click;
            FindBtn("btnXoaDongNhap").Click += (s, e) => XoaDongGio(dgvGioNhap, _gioNhap, RefreshGioNhap);
            FindBtn("btnLuuPhieuNhap").Click += BtnLuuPhieuNhap_Click;
            btnThemNCC.Click += BtnThemNCC_Click;
            btnThemSPNhap.Click += BtnThemSP_Click;

            FindBtn("btnThemVaoGioXuat").Click += BtnThemVaoGioXuat_Click;
            FindBtn("btnXoaDongXuat").Click += (s, e) => XoaDongGio(dgvGioXuat, _gioXuat, RefreshGioXuat);
            FindBtn("btnLuuPhieuXuat").Click += BtnLuuPhieuXuat_Click;
            btnThemSPXuat.Click += BtnThemSP_Click;

            FindBtn("btnLocLichSu").Click += (s, e) => LoadLichSuAll();
            FindBtn("btnRefreshLichSu").Click += (s, e) => LoadLichSuAll();
            dgvLichSuAll.SelectionChanged += DgvLichSuAll_SelectionChanged;

            FindBtn("btnReloadSapHet").Click += (s, e) => LoadCanhBaoSapHet();
            FindBtn("btnReloadSapHan").Click += (s, e) => LoadCanhBaoSapHetHan();

            dgvSapHet.CellFormatting += DgvSapHet_CellFormatting;
            dgvSapHetHan.CellFormatting += DgvSapHetHan_CellFormatting;
        }

        private Button FindBtn(string name)
        {
            return FindControlByName(this, name) as Button ?? throw new InvalidOperationException($"Button '{name}' not found.");
        }

        private Control FindControlByName(Control root, string name)
        {
            foreach (Control c in root.Controls)
            {
                if (c.Name == name) return c;
                var found = FindControlByName(c, name);
                if (found != null) return found;
            }
            return null;
        }

        private void ActivateTab(int index, params Button[] allBtns)
        {
            tabMain.SelectedIndex = index;
            foreach (var b in allBtns) SetNavStyle(b, (int)b.Tag == index);
        }

        private void LoadSettings()
        {
            var ts = _settingBll.GetCauHinh();
            if (ts != null)
            {
                _nguongHetHang = ts.NguongHetHang;
                _nguongHetHan = ts.NguongHetHan;
            }
            // Update UI Labels 
            lblNguongInfo.Text = $"🔴 Ngưỡng hệ thống: Báo động khi tồn kho dưới {_nguongHetHang} sản phẩm.";
            lblHanInfo.Text = $"🟡 Ngưỡng hệ thống: Báo động khi hạn sử dụng còn dưới {_nguongHetHan} ngày.";
        }

        private void LoadComboData()
        {
            var nccList = _nccBll.GetAll();
            cboNCC.DataSource = nccList;
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

            cboSPXuat.DataSource = new List<SanPham_DTO>(spList);
            cboSPXuat.DisplayMember = "TenSP";
            cboSPXuat.ValueMember = "SanPhamID";
        }

        // ── KẾT NỐI VỚI CÁC FORM QUẢN LÝ KHÁC ──
        private void BtnThemNCC_Click(object sender, EventArgs e)
        {
            using (var frm = new frmQuanLyNCC())
            {
                frm.ShowDialog();
                cboNCC.DataSource = _nccBll.GetAll();
                if (cboNCC.Items.Count > 0) cboNCC.SelectedIndex = cboNCC.Items.Count - 1;
            }
        }

        private void BtnThemSP_Click(object sender, EventArgs e)
        {
            using (var frm = new frmQuanLySanPham())
            {
                frm.ShowDialog();
                RefreshComboSanPham();
                if (cboSPNhap.Items.Count > 0) cboSPNhap.SelectedIndex = cboSPNhap.Items.Count - 1;
                if (cboSPXuat.Items.Count > 0) cboSPXuat.SelectedIndex = cboSPXuat.Items.Count - 1;
            }
        }

        // ── LOGIC NHẬP KHO ──
        private void BtnThemVaoGioNhap_Click(object sender, EventArgs e)
        {
            if (cboSPNhap.SelectedItem == null) { ShowWarn("Vui lòng chọn sản phẩm!"); return; }
            if (!int.TryParse(txtSLNhap.Text.Trim(), out int sl) || sl <= 0) { ShowWarn("Số lượng nhập không hợp lệ!"); return; }
            string giaRaw = txtGiaNhap.Text.Trim().Replace(",", "").Replace(".", "").Replace(" ", "");
            if (!long.TryParse(giaRaw, out long gia) || gia <= 0) { ShowWarn("Giá nhập không hợp lệ!"); return; }
            if (chkHSD.Checked && chkNSX.Checked && dtpNSX.Value >= dtpHSD.Value) { ShowWarn("NSX phải trước HSD!"); return; }

            var sp = (SanPham_DTO)cboSPNhap.SelectedItem;
            int existIdx = _gioNhap.FindIndex(x => x.SanPhamID == sp.SanPhamID);

            if (existIdx >= 0)
            {
                var ans = MessageBox.Show($"Sản phẩm '{sp.TenSP}' đã có.\nChọn Yes = Cộng dồn SL | Chọn No = Thêm dòng mới", "Trùng lặp", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (ans == DialogResult.Cancel) return;
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

            RefreshGioNhap(); ResetFormNhap();
        }

        private void BtnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedItem == null) { ShowWarn("Vui lòng chọn nhà cung cấp!"); return; }
            if (_gioNhap.Count == 0) { ShowWarn("Giỏ hàng đang trống!"); return; }

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

            if (ok) { _gioNhap.Clear(); RefreshGioNhap(); LoadLichSuAll(); LoadCanhBao(); RefreshComboSanPham(); }
        }

        // ── LOGIC XUẤT KHO ──
        private void BtnThemVaoGioXuat_Click(object sender, EventArgs e)
        {
            if (cboSPXuat.SelectedItem == null) { ShowWarn("Vui lòng chọn sản phẩm!"); return; }
            if (!int.TryParse(txtSLXuat.Text.Trim(), out int sl) || sl <= 0) { ShowWarn("Số lượng xuất không hợp lệ!"); return; }

            var sp = (SanPham_DTO)cboSPXuat.SelectedItem;
            if (sl > sp.TonKhoTong) { ShowWarn($"Số lượng xuất ({sl}) vượt tồn kho ({sp.TonKhoTong})!"); return; }

            _gioXuat.Add(new ChiTietPhieuXuat_DTO
            {
                SanPhamID = sp.SanPhamID,
                TenSP = sp.TenSP,
                MaVach = sp.MaVach,
                SoLuongXuat = sl,
                TonKhoHienTai = sp.TonKhoTong,
                GhiChu = txtGhiChuXuat.Text.Trim()
            });

            RefreshGioXuat(); txtSLXuat.Text = "1"; txtGhiChuXuat.Text = "";
        }

        private void BtnLuuPhieuXuat_Click(object sender, EventArgs e)
        {
            if (_gioXuat.Count == 0) { ShowWarn("Giỏ hàng xuất đang trống!"); return; }

            var phieu = new PhieuXuat_DTO
            {
                LyDo = cboLyDo.SelectedItem?.ToString() ?? "",
                NhanVienID = 1, // Fix SessionManager sau
                GhiChu = txtGhiChuPhieuXuat.Text.Trim()
            };

            var (ok, msg) = _bll.TaoPhieuXuat(phieu, _gioXuat);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (ok) { _gioXuat.Clear(); RefreshGioXuat(); LoadLichSuAll(); LoadCanhBao(); RefreshComboSanPham(); }
        }

        private void XoaDongGio<T>(DataGridView dgv, List<T> gio, Action refresh)
        {
            if (dgv.SelectedRows.Count == 0) return;
            int idx = dgv.SelectedRows[0].Index;
            gio.RemoveAt(idx); refresh();
        }

        // ── LOGIC LỊCH SỬ ──
        private void LoadLichSuAll()
        {
            _dangLoad = true;
            dgvLichSuAll.Rows.Clear(); dgvChiTietLichSu.Rows.Clear();
            lblChiTietTitle.Text = "Chi tiết phiếu — chọn một phiếu ở trên để xem";

            bool showNhap = cboLoaiPhieu.SelectedIndex == 0 || cboLoaiPhieu.SelectedIndex == 1;
            bool showXuat = cboLoaiPhieu.SelectedIndex == 0 || cboLoaiPhieu.SelectedIndex == 2;

            if (showNhap)
            {
                var dsNhap = _bll.GetPhieuNhapByDate(dtpFrom.Value, dtpTo.Value);
                foreach (var p in dsNhap)
                {
                    int idx = dgvLichSuAll.Rows.Add("📥 Nhập", p.MaPN, p.TenNCC, p.TenNV, p.NgayLap.ToString("dd/MM/yyyy HH:mm"), string.Format("{0:N0} đ", p.TongTienThanhToan));
                    dgvLichSuAll.Rows[idx].Tag = ("N", p.PhieuNhapID);
                    dgvLichSuAll.Rows[idx].DefaultCellStyle.ForeColor = Color.FromArgb(0, 110, 60);
                }
            }

            if (showXuat)
            {
                var dsXuat = _bll.GetAllPhieuXuat();
                foreach (var p in dsXuat)
                {
                    if (p.NgayXuat.Date < dtpFrom.Value.Date || p.NgayXuat.Date > dtpTo.Value.Date) continue;
                    int idx = dgvLichSuAll.Rows.Add("📤 Xuất", p.MaPX, p.LyDo, p.TenNV, p.NgayXuat.ToString("dd/MM/yyyy HH:mm"), "—");
                    dgvLichSuAll.Rows[idx].Tag = ("X", p.PhieuXuatID);
                    dgvLichSuAll.Rows[idx].DefaultCellStyle.ForeColor = Color.FromArgb(160, 40, 0);
                }
            }
            _dangLoad = false;
        }

        private void DgvLichSuAll_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangLoad || dgvLichSuAll.SelectedRows.Count == 0 || dgvLichSuAll.SelectedRows[0].Tag == null) return;

            var (loai, id) = ((string, int))dgvLichSuAll.SelectedRows[0].Tag;
            dgvChiTietLichSu.Rows.Clear();

            if (loai == "N")
            {
                EnsureChiTietColumnsNhap();
                lblChiTietTitle.Text = $"Chi tiết Phiếu Nhập — {dgvLichSuAll.SelectedRows[0].Cells[1].Value}";
                foreach (var ct in _bll.GetChiTietPhieuNhap(id))
                    dgvChiTietLichSu.Rows.Add(ct.TenSP, ct.MaVach, ct.SoLuongNhap, string.Format("{0:N0} đ", ct.GiaNhap),
                        ct.NSX.HasValue ? ct.NSX.Value.ToString("dd/MM/yyyy") : "—", ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—");
            }
            else
            {
                EnsureChiTietColumnsXuat();
                lblChiTietTitle.Text = $"Chi tiết Phiếu Xuất — {dgvLichSuAll.SelectedRows[0].Cells[1].Value}";
                foreach (var ct in _bll.GetChiTietPhieuXuat(id))
                    dgvChiTietLichSu.Rows.Add(ct.TenSP, ct.MaVach, ct.SoLuongXuat, ct.GhiChu);
            }
        }

        private string _chiTietMode = "";
        private void EnsureChiTietColumnsNhap()
        {
            if (_chiTietMode == "N") return; dgvChiTietLichSu.Columns.Clear();
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sản phẩm", FillWeight = 34 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã vạch", FillWeight = 16 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giá nhập", FillWeight = 16, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "NSX", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "HSD", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            _chiTietMode = "N";
        }
        private void EnsureChiTietColumnsXuat()
        {
            if (_chiTietMode == "X") return; dgvChiTietLichSu.Columns.Clear();
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sản phẩm", FillWeight = 45 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã vạch", FillWeight = 18 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL xuất", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ghi chú", FillWeight = 25 });
            _chiTietMode = "X";
        }

        // ── LOGIC CẢNH BÁO ──
        private void LoadCanhBao() { LoadCanhBaoSapHet(); LoadCanhBaoSapHetHan(); }

        private void LoadCanhBaoSapHet()
        {
            dgvSapHet.Rows.Clear();
            foreach (var c in _bll.GetHangSapHet()) dgvSapHet.Rows.Add(c.MaVach, c.TenSP, c.TenLoai, c.TonKhoTong);
        }

        private void LoadCanhBaoSapHetHan()
        {
            dgvSapHetHan.Rows.Clear();
            foreach (var c in _bll.GetHangSapHetHan())
                dgvSapHetHan.Rows.Add(c.MaVach, c.TenSP, c.TenLoai, c.HSD.HasValue ? c.HSD.Value.ToString("dd/MM/yyyy") : "—", c.HSD.HasValue ? $"{(c.HSD.Value - DateTime.Today).Days} ngày" : "—");
        }

        private void DgvSapHet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int ton = dgvSapHet.Rows[e.RowIndex].Cells[3].Value is int v ? v : 0;
            if (ton == 0) dgvSapHet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
            else if (ton <= _nguongHetHang / 2) dgvSapHet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 210);
        }

        private void DgvSapHetHan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var s = dgvSapHetHan.Rows[e.RowIndex].Cells[4].Value as string;
            if (s != null && s.EndsWith(" ngày") && int.TryParse(s.Replace(" ngày", ""), out int days))
            {
                if (days <= 7) dgvSapHetHan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 180);
                else if (days <= 14) dgvSapHetHan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 210);
            }
        }

        private void RefreshGioNhap()
        {
            dgvGioNhap.Rows.Clear(); long tong = 0;
            foreach (var ct in _gioNhap)
            {
                tong += (long)ct.SoLuongNhap * ct.GiaNhap;
                dgvGioNhap.Rows.Add(ct.TenSP, ct.SoLuongNhap, string.Format("{0:N0} đ", ct.GiaNhap), ct.NSX.HasValue ? ct.NSX.Value.ToString("dd/MM/yyyy") : "—", ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—");
            }
            lblTongTienNhap.Text = $"Tổng tiền:  {tong:N0} đ";
        }

        private void RefreshGioXuat()
        {
            dgvGioXuat.Rows.Clear();
            foreach (var ct in _gioXuat) dgvGioXuat.Rows.Add(ct.TenSP, ct.SoLuongXuat, ct.TonKhoHienTai, ct.GhiChu);
        }

        private void ResetFormNhap() { txtSLNhap.Text = "1"; txtGiaNhap.Text = ""; chkNSX.Checked = false; chkHSD.Checked = false; }
        private void ShowWarn(string msg) => MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}