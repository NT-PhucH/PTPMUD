using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucLichSu : UserControl
    {
        private readonly Kho_BLL _bll = new Kho_BLL();
        private bool _dangLoad = false;
        private string _chiTietMode = "";

        public ucLichSu()
        {
            InitializeComponent();
            WireEvents();
            this.Load += UcLichSu_Load;
        }

        private void UcLichSu_Load(object sender, EventArgs e)
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                // Setup giá trị mặc định cho filter
                cboLoaiPhieu.SelectedIndex = 0;
                dtpFrom.Value = DateTime.Now.AddDays(-30); // Mặc định lấy 30 ngày gần nhất
                dtpTo.Value = DateTime.Now;

                InitDanhSachColumns(); // Khởi tạo cột cho bảng danh sách
                LoadLichSuAll();
            }
        }

        private void WireEvents()
        {
            btnLocLichSu.Click += (s, e) => LoadLichSuAll();
            btnRefreshLichSu.Click += (s, e) =>
            {
                dtpFrom.Value = DateTime.Now.AddDays(-30);
                dtpTo.Value = DateTime.Now;
                cboLoaiPhieu.SelectedIndex = 0;
                LoadLichSuAll();
            };
            dgvLichSuAll.SelectionChanged += DgvLichSuAll_SelectionChanged;
        }

        private void InitDanhSachColumns()
        {
            dgvLichSuAll.Columns.Clear();
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Loại", FillWeight = 15 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Phiếu", FillWeight = 15 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "NCC / Lý Do", FillWeight = 25 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nhân Viên", FillWeight = 20 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời Gian", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tổng Tiền", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        private void LoadLichSuAll()
        {
            _dangLoad = true;
            dgvLichSuAll.Rows.Clear();
            dgvChiTietLichSu.Rows.Clear();
            lblChiTietTitle.Text = "Chi tiết phiếu — chọn một phiếu ở bên trái để xem";

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

            if (dgvLichSuAll.Rows.Count > 0)
            {
                dgvLichSuAll.Rows[0].Selected = true;
                DgvLichSuAll_SelectionChanged(null, EventArgs.Empty);
            }
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
                {
                    dgvChiTietLichSu.Rows.Add(
                        ct.TenSP,
                        ct.MaVach,
                        ct.SoLuongNhap,
                        string.Format("{0:N0} đ", ct.GiaNhap),
                        ct.NSX.HasValue ? ct.NSX.Value.ToString("dd/MM/yyyy") : "—",
                        ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—"
                    );
                }
            }
            else
            {
                EnsureChiTietColumnsXuat();
                lblChiTietTitle.Text = $"Chi tiết Phiếu Xuất — {dgvLichSuAll.SelectedRows[0].Cells[1].Value}";
                foreach (var ct in _bll.GetChiTietPhieuXuat(id))
                {
                    dgvChiTietLichSu.Rows.Add(ct.TenSP, ct.MaVach, ct.SoLuongXuat, ct.GhiChu);
                }
            }
        }

        private void EnsureChiTietColumnsNhap()
        {
            if (_chiTietMode == "N") return;
            dgvChiTietLichSu.Columns.Clear();
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sản phẩm", FillWeight = 34 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã vạch", FillWeight = 16 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giá nhập", FillWeight = 16, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "NSX", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "HSD", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            _chiTietMode = "N";
        }

        private void EnsureChiTietColumnsXuat()
        {
            if (_chiTietMode == "X") return;
            dgvChiTietLichSu.Columns.Clear();
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sản phẩm", FillWeight = 45 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã vạch", FillWeight = 18 });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietLichSu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ghi chú", FillWeight = 25 });
            _chiTietMode = "X";
        }
    }
}