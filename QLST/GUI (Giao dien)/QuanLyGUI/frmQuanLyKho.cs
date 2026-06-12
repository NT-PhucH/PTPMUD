// ===================================================
// File: frmQuanLyKho.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmQuanLyKho : Form
    {
        private bool _dangLoad = false;
        private readonly Kho_BLL _bll = new Kho_BLL();
        private readonly SanPham_DAL _spDal = new SanPham_DAL();
        private readonly NhaCungCap_DAL _nccDal = new NhaCungCap_DAL();

        // Giỏ hàng tạm cho phiếu nhập / xuất đang tạo
        private List<ChiTietPhieuNhap_DTO> _gioNhap = new List<ChiTietPhieuNhap_DTO>();
        private List<ChiTietPhieuXuat_DTO> _gioXuat = new List<ChiTietPhieuXuat_DTO>();

        // ── CONTROLS ──────────────────────────────────────────────────────────
        private TabControl tabMain;

        // Tab 1 - Nhập hàng
        private ComboBox cboNCC, cboSPNhap;
        private TextBox txtSLNhap, txtGiaNhap, txtGhiChuNhap;
        private DateTimePicker dtpNSX, dtpHSD;
        private CheckBox chkNSX, chkHSD;
        private DataGridView dgvGioNhap, dgvLichSuNhap, dgvChiTietNhap;
        private Label lblTongTienNhap;

        // Tab 2 - Xuất kho
        private ComboBox cboLyDo, cboSPXuat;
        private TextBox txtSLXuat, txtGhiChuXuat, txtGhiChuPhieuXuat;
        private DataGridView dgvGioXuat, dgvLichSuXuat, dgvChiTietXuat;

        // Tab 3 - Lịch sử (xem chung)
        private DateTimePicker dtpFrom, dtpTo;
        private DataGridView dgvLichSuAll;

        // Tab 4 - Cảnh báo
        private DataGridView dgvSapHet, dgvSapHetHan;
        private NumericUpDown nudNguong, nudSoNgay;

        public frmQuanLyKho()
        {
            BuildUI();
            LoadComboData();
            LoadLichSuNhap();
            LoadLichSuXuat();
            LoadCanhBao();
        }

        // ══════════════════════════════════════════════════════════════════════
        // BUILD UI
        // ══════════════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            Text = "Quản lý kho hàng";
            Size = new Size(1150, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label
            {
                Text = "📦  QUẢN LÝ KHO HÀNG",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Location = new Point(15, 12),
                AutoSize = true
            });

            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Padding = new Point(12, 6)
            };

            tabMain.TabPages.Add(BuildTabNhap());
            tabMain.TabPages.Add(BuildTabXuat());
            tabMain.TabPages.Add(BuildTabLichSu());
            tabMain.TabPages.Add(BuildTabCanhBao());

            Controls.Add(tabMain);
            Controls.Add(header);
        }

        // ── TAB 1: NHẬP HÀNG ──────────────────────────────────────────────────
        private TabPage BuildTabNhap()
        {
            var tab = new TabPage("  📥 Nhập hàng  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            // Panel trái: form nhập
            var pLeft = new Panel { Location = new Point(5, 5), Width = 370, Height = 640, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom, BackColor = Color.White, Padding = new Padding(12) };

            int y = 12;
            pLeft.Controls.Add(MakeLabel("Nhà cung cấp: *", 12, y));
            cboNCC = MakeCombo(12, y + 22, 346); pLeft.Controls.Add(cboNCC);

            y += 65;
            pLeft.Controls.Add(MakeLabel("Sản phẩm: *", 12, y));
            cboSPNhap = MakeCombo(12, y + 22, 346); pLeft.Controls.Add(cboSPNhap);

            y += 65;
            pLeft.Controls.Add(MakeLabel("Số lượng nhập: *", 12, y));
            txtSLNhap = MakeTextBox(12, y + 22, 160); txtSLNhap.Text = "1"; pLeft.Controls.Add(txtSLNhap);

            pLeft.Controls.Add(MakeLabel("Giá nhập (đ): *", 185, y));
            txtGiaNhap = MakeTextBox(185, y + 22, 173); pLeft.Controls.Add(txtGiaNhap);

            y += 65;
            chkNSX = new CheckBox { Text = "Ngày sản xuất:", Location = new Point(12, y), AutoSize = true };
            chkNSX.CheckedChanged += (s, e) => dtpNSX.Enabled = chkNSX.Checked;
            pLeft.Controls.Add(chkNSX);
            dtpNSX = new DateTimePicker { Location = new Point(12, y + 22), Width = 160, Format = DateTimePickerFormat.Short, Enabled = false };
            pLeft.Controls.Add(dtpNSX);

            chkHSD = new CheckBox { Text = "Hạn sử dụng:", Location = new Point(185, y), AutoSize = true };
            chkHSD.CheckedChanged += (s, e) => dtpHSD.Enabled = chkHSD.Checked;
            pLeft.Controls.Add(chkHSD);
            dtpHSD = new DateTimePicker { Location = new Point(185, y + 22), Width = 173, Format = DateTimePickerFormat.Short, Enabled = false };
            pLeft.Controls.Add(dtpHSD);

            y += 65;
            pLeft.Controls.Add(MakeLabel("Ghi chú dòng:", 12, y));
            txtGhiChuNhap = MakeTextBox(12, y + 22, 346); pLeft.Controls.Add(txtGhiChuNhap);

            y += 55;
            var btnThemVaoGio = MakeButton("➕ Thêm vào phiếu", 12, y, 160, Color.FromArgb(34, 139, 34));
            btnThemVaoGio.Click += BtnThemVaoGioNhap_Click;
            pLeft.Controls.Add(btnThemVaoGio);

            var btnXoaDong = MakeButton("🗑 Xóa dòng", 185, y, 173, Color.FromArgb(180, 60, 60));
            btnXoaDong.Click += (s, e) => {
                if (dgvGioNhap.SelectedRows.Count > 0)
                {
                    _gioNhap.RemoveAt(dgvGioNhap.SelectedRows[0].Index);
                    RefreshGioNhap();
                }
            };
            pLeft.Controls.Add(btnXoaDong);

            y += 45;
            lblTongTienNhap = new Label { Text = "Tổng tiền: 0 đ", Location = new Point(12, y), AutoSize = true, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 100, 200) };
            pLeft.Controls.Add(lblTongTienNhap);

            y += 35;
            var btnLuuPhieu = MakeButton("💾 LƯU PHIẾU NHẬP", 12, y, 346, Color.FromArgb(30, 40, 60));
            btnLuuPhieu.Height = 38;
            btnLuuPhieu.Click += BtnLuuPhieuNhap_Click;
            pLeft.Controls.Add(btnLuuPhieu);

            // Panel phải: giỏ + lịch sử
            var pRight = new Panel { Location = new Point(380, 5), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom };
            pRight.Width = 750; pRight.Height = 640;

            pRight.Controls.Add(MakeBoldLabel("Sản phẩm trong phiếu:", 0, 0));
            dgvGioNhap = MakeDgv(0, 22, 750, 200);
            dgvGioNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên SP", FillWeight = 40 });
            dgvGioNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSL", HeaderText = "SL nhập", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvGioNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGia", HeaderText = "Giá nhập", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvGioNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHSD", HeaderText = "HSD", FillWeight = 25 });
            pRight.Controls.Add(dgvGioNhap);

            pRight.Controls.Add(MakeBoldLabel("Lịch sử phiếu nhập:", 0, 235));
            dgvLichSuNhap = MakeDgv(0, 257, 750, 200);
            dgvLichSuNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã phiếu", FillWeight = 20 });
            dgvLichSuNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNCC", HeaderText = "Nhà cung cấp", FillWeight = 30 });
            dgvLichSuNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày lập", FillWeight = 20 });
            dgvLichSuNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTong", HeaderText = "Tổng tiền", FillWeight = 30, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvLichSuNhap.SelectionChanged += (s, e) => LoadChiTietPhieuNhap();
            pRight.Controls.Add(dgvLichSuNhap);

            pRight.Controls.Add(MakeBoldLabel("Chi tiết phiếu đã chọn:", 0, 470));
            dgvChiTietNhap = MakeDgv(0, 492, 750, 150);
            dgvChiTietNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen2", HeaderText = "Tên SP", FillWeight = 35 });
            dgvChiTietNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSL2", HeaderText = "SL nhập", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGia2", HeaderText = "Giá nhập", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvChiTietNhap.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHSD2", HeaderText = "HSD", FillWeight = 30 });
            pRight.Controls.Add(dgvChiTietNhap);

            tab.Controls.Add(pLeft);
            tab.Controls.Add(pRight);
            tab.Resize += (s, e) => {
                pLeft.Height = tab.Height - 10;
                pRight.Width = tab.Width - 385;
                pRight.Height = tab.Height - 10;
                foreach (DataGridView d in new[] { dgvGioNhap, dgvLichSuNhap, dgvChiTietNhap })
                    d.Width = pRight.Width;
            };
            return tab;
        }

        // ── TAB 2: XUẤT KHO ───────────────────────────────────────────────────
        private TabPage BuildTabXuat()
        {
            var tab = new TabPage("  📤 Xuất kho  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            var pLeft = new Panel { Location = new Point(5, 5), Width = 370, BackColor = Color.White, Padding = new Padding(12) };
            pLeft.Height = 640;

            int y = 12;
            pLeft.Controls.Add(MakeLabel("Lý do xuất: *", 12, y));
            cboLyDo = MakeCombo(12, y + 22, 346);
            cboLyDo.Items.AddRange(new object[] { "Hàng hỏng", "Mất mát", "Hết hạn", "Xuất nội bộ", "Khác" });
            cboLyDo.SelectedIndex = 0;
            pLeft.Controls.Add(cboLyDo);

            y += 65;
            pLeft.Controls.Add(MakeLabel("Sản phẩm: *", 12, y));
            cboSPXuat = MakeCombo(12, y + 22, 346); pLeft.Controls.Add(cboSPXuat);

            y += 65;
            pLeft.Controls.Add(MakeLabel("Số lượng xuất: *", 12, y));
            txtSLXuat = MakeTextBox(12, y + 22, 160); txtSLXuat.Text = "1"; pLeft.Controls.Add(txtSLXuat);

            y += 55;
            pLeft.Controls.Add(MakeLabel("Ghi chú dòng:", 12, y));
            txtGhiChuXuat = MakeTextBox(12, y + 22, 346); pLeft.Controls.Add(txtGhiChuXuat);

            y += 55;
            var btnThemXuat = MakeButton("➕ Thêm vào phiếu", 12, y, 160, Color.FromArgb(34, 139, 34));
            btnThemXuat.Click += BtnThemVaoGioXuat_Click;
            pLeft.Controls.Add(btnThemXuat);

            var btnXoaXuat = MakeButton("🗑 Xóa dòng", 185, y, 173, Color.FromArgb(180, 60, 60));
            btnXoaXuat.Click += (s, e) => {
                if (dgvGioXuat.SelectedRows.Count > 0)
                {
                    _gioXuat.RemoveAt(dgvGioXuat.SelectedRows[0].Index);
                    RefreshGioXuat();
                }
            };
            pLeft.Controls.Add(btnXoaXuat);

            y += 55;
            pLeft.Controls.Add(MakeLabel("Ghi chú phiếu:", 12, y));
            txtGhiChuPhieuXuat = MakeTextBox(12, y + 22, 346); pLeft.Controls.Add(txtGhiChuPhieuXuat);

            y += 55;
            var btnLuuXuat = MakeButton("💾 LƯU PHIẾU XUẤT", 12, y, 346, Color.FromArgb(180, 60, 60));
            btnLuuXuat.Height = 38;
            btnLuuXuat.Click += BtnLuuPhieuXuat_Click;
            pLeft.Controls.Add(btnLuuXuat);

            var pRight = new Panel { Location = new Point(380, 5), Width = 750, Height = 640, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom };

            pRight.Controls.Add(MakeBoldLabel("Sản phẩm trong phiếu:", 0, 0));
            dgvGioXuat = MakeDgv(0, 22, 750, 200);
            dgvGioXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên SP", FillWeight = 45 });
            dgvGioXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSL", HeaderText = "SL xuất", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvGioXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTon", HeaderText = "Tồn kho", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvGioXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGhiChu", HeaderText = "Ghi chú", FillWeight = 20 });
            pRight.Controls.Add(dgvGioXuat);

            pRight.Controls.Add(MakeBoldLabel("Lịch sử phiếu xuất:", 0, 235));
            dgvLichSuXuat = MakeDgv(0, 257, 750, 200);
            dgvLichSuXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã phiếu", FillWeight = 20 });
            dgvLichSuXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLyDo", HeaderText = "Lý do", FillWeight = 30 });
            dgvLichSuXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNV", HeaderText = "Nhân viên", FillWeight = 25 });
            dgvLichSuXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày xuất", FillWeight = 25 });
            dgvLichSuXuat.SelectionChanged += (s, e) => LoadChiTietPhieuXuat();
            pRight.Controls.Add(dgvLichSuXuat);

            pRight.Controls.Add(MakeBoldLabel("Chi tiết phiếu đã chọn:", 0, 470));
            dgvChiTietXuat = MakeDgv(0, 492, 750, 150);
            dgvChiTietXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen2", HeaderText = "Tên SP", FillWeight = 45 });
            dgvChiTietXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSL2", HeaderText = "SL xuất", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvChiTietXuat.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGhiChu2", HeaderText = "Ghi chú", FillWeight = 35 });
            pRight.Controls.Add(dgvChiTietXuat);

            tab.Controls.Add(pLeft);
            tab.Controls.Add(pRight);
            tab.Resize += (s, e) => {
                pLeft.Height = tab.Height - 10;
                pRight.Width = tab.Width - 385;
                pRight.Height = tab.Height - 10;
                foreach (DataGridView d in new[] { dgvGioXuat, dgvLichSuXuat, dgvChiTietXuat })
                    d.Width = pRight.Width;
            };
            return tab;
        }

        // ── TAB 3: LỊCH SỬ ────────────────────────────────────────────────────
        private TabPage BuildTabLichSu()
        {
            var tab = new TabPage("  📋 Lịch sử  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            var pFilter = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(10, 8, 10, 0) };
            pFilter.Controls.Add(new Label { Text = "Từ ngày:", Location = new Point(10, 12), AutoSize = true });
            dtpFrom = new DateTimePicker { Location = new Point(75, 8), Width = 130, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(-1) };
            pFilter.Controls.Add(dtpFrom);
            pFilter.Controls.Add(new Label { Text = "Đến ngày:", Location = new Point(220, 12), AutoSize = true });
            dtpTo = new DateTimePicker { Location = new Point(290, 8), Width = 130, Format = DateTimePickerFormat.Short };
            pFilter.Controls.Add(dtpTo);
            var btnLoc = MakeButton("🔍 Lọc", 435, 8, 80, Color.FromArgb(30, 100, 200));
            btnLoc.Height = 28;
            btnLoc.Click += (s, e) => LoadLichSuAll();
            pFilter.Controls.Add(btnLoc);

            dgvLichSuAll = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 36,
                RowTemplate = { Height = 28 }
            };
            StyleDgv(dgvLichSuAll);
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 10 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã phiếu", FillWeight = 18 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDoiTac", HeaderText = "NCC / Lý do", FillWeight = 28 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày", FillWeight = 18 });
            dgvLichSuAll.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGiaTri", HeaderText = "Giá trị", FillWeight = 26, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });

            tab.Controls.Add(dgvLichSuAll);
            tab.Controls.Add(pFilter);
            return tab;
        }

        // ── TAB 4: CẢNH BÁO ───────────────────────────────────────────────────
        private TabPage BuildTabCanhBao()
        {
            var tab = new TabPage("  ⚠️ Cảnh báo  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            // Sắp hết tồn
            var pTop = new Panel { Dock = DockStyle.Top, Height = 300, Padding = new Padding(5) };

            var pNguong = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.White };
            pNguong.Controls.Add(new Label { Text = "🔴 Hàng sắp hết tồn — ngưỡng cảnh báo:", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) });
            nudNguong = new NumericUpDown { Location = new Point(280, 7), Width = 60, Minimum = 1, Maximum = 100, Value = 10 };
            pNguong.Controls.Add(nudNguong);
            var btnRefHet = MakeButton("Cập nhật", 355, 7, 80, Color.FromArgb(200, 80, 40));
            btnRefHet.Height = 26;
            btnRefHet.Click += (s, e) => LoadCanhBaoSapHet();
            pNguong.Controls.Add(btnRefHet);

            dgvSapHet = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 32, RowTemplate = { Height = 26 } };
            StyleDgv(dgvSapHet);
            dgvSapHet.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã vạch", FillWeight = 18 });
            dgvSapHet.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên sản phẩm", FillWeight = 40 });
            dgvSapHet.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 22 });
            dgvSapHet.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTon", HeaderText = "Tồn kho", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            pTop.Controls.Add(dgvSapHet);
            pTop.Controls.Add(pNguong);

            // Sắp hết hạn
            var pBot = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };

            var pSoNgay = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.White };
            pSoNgay.Controls.Add(new Label { Text = "🟡 Hàng sắp hết hạn — trong vòng:", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) });
            nudSoNgay = new NumericUpDown { Location = new Point(240, 7), Width = 60, Minimum = 1, Maximum = 365, Value = 30 };
            pSoNgay.Controls.Add(nudSoNgay);
            pSoNgay.Controls.Add(new Label { Text = "ngày", Location = new Point(308, 10), AutoSize = true });
            var btnRefHan = MakeButton("Cập nhật", 355, 7, 80, Color.FromArgb(180, 140, 0));
            btnRefHan.Height = 26;
            btnRefHan.Click += (s, e) => LoadCanhBaoSapHetHan();
            pSoNgay.Controls.Add(btnRefHan);

            dgvSapHetHan = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 32, RowTemplate = { Height = 26 } };
            StyleDgv(dgvSapHetHan);
            dgvSapHetHan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã vạch", FillWeight = 18 });
            dgvSapHetHan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên sản phẩm", FillWeight = 35 });
            dgvSapHetHan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 20 });
            dgvSapHetHan.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHSD", HeaderText = "Hạn sử dụng", FillWeight = 27 });

            pBot.Controls.Add(dgvSapHetHan);
            pBot.Controls.Add(pSoNgay);

            tab.Controls.Add(pBot);
            tab.Controls.Add(pTop);
            return tab;
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD DỮ LIỆU
        // ══════════════════════════════════════════════════════════════════════
        private void LoadComboData()
        {
            // Sản phẩm
            var spList = _spDal.GetAll();
            cboSPNhap.DataSource = new List<SanPham_DTO>(spList);
            cboSPNhap.DisplayMember = "TenSP"; cboSPNhap.ValueMember = "SanPhamID";
            cboSPXuat.DataSource = new List<SanPham_DTO>(spList);
            cboSPXuat.DisplayMember = "TenSP"; cboSPXuat.ValueMember = "SanPhamID";

            // Nhà cung cấp
            var nccList = _nccDal.GetAll();
            cboNCC.DataSource = nccList;
            cboNCC.DisplayMember = "TenNCC"; cboNCC.ValueMember = "NhaCungCapID";
        }

        private void LoadLichSuNhap()
        {
            _dangLoad = true;
            dgvLichSuNhap.Rows.Clear();
            foreach (var p in _bll.GetAllPhieuNhap())
                dgvLichSuNhap.Rows.Add(p.MaPN, p.TenNCC,
                    p.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                    string.Format("{0:N0} đ", p.TongTienThanhToan));
            _dangLoad = false;
        }

        private void LoadLichSuXuat()
        {
            _dangLoad = true;
            dgvLichSuXuat.Rows.Clear();
            foreach (var p in _bll.GetAllPhieuXuat())
                dgvLichSuXuat.Rows.Add(p.MaPX, p.LyDo, p.TenNV,
                    p.NgayXuat.ToString("dd/MM/yyyy HH:mm"));
            _dangLoad = false;
        }

        private void LoadChiTietPhieuNhap()
        {
            dgvChiTietNhap.Rows.Clear();
            if (_dangLoad) return;
            // Lấy index tương ứng trong list
            var list = _bll.GetAllPhieuNhap();
            if (dgvLichSuNhap.SelectedRows[0].Index >= list.Count) return;
            int id = list[dgvLichSuNhap.SelectedRows[0].Index].PhieuNhapID;
            foreach (var ct in _bll.GetChiTietPhieuNhap(id))
                dgvChiTietNhap.Rows.Add(ct.TenSP, ct.SoLuongNhap,
                    string.Format("{0:N0} đ", ct.GiaNhap),
                    ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—");
        }

        private void LoadChiTietPhieuXuat()
        {
            dgvChiTietXuat.Rows.Clear();
            if (_dangLoad) return;
            var list = _bll.GetAllPhieuXuat();
            if (dgvLichSuXuat.SelectedRows[0].Index >= list.Count) return;
            int id = list[dgvLichSuXuat.SelectedRows[0].Index].PhieuXuatID;
            foreach (var ct in _bll.GetChiTietPhieuXuat(id))
                dgvChiTietXuat.Rows.Add(ct.TenSP, ct.SoLuongXuat, ct.GhiChu);
        }

        private void LoadLichSuAll()
        {
            dgvLichSuAll.Rows.Clear();
            foreach (var p in _bll.GetPhieuNhapByDate(dtpFrom.Value, dtpTo.Value))
            {
                int idx = dgvLichSuAll.Rows.Add("📥 Nhập", p.MaPN, p.TenNCC,
                    p.NgayLap.ToString("dd/MM/yyyy"), string.Format("{0:N0} đ", p.TongTienThanhToan));
                dgvLichSuAll.Rows[idx].DefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 0);
            }
            foreach (var p in _bll.GetAllPhieuXuat())
            {
                if (p.NgayXuat.Date < dtpFrom.Value.Date || p.NgayXuat.Date > dtpTo.Value.Date) continue;
                int idx = dgvLichSuAll.Rows.Add("📤 Xuất", p.MaPX, p.LyDo,
                    p.NgayXuat.ToString("dd/MM/yyyy"), "—");
                dgvLichSuAll.Rows[idx].DefaultCellStyle.ForeColor = Color.FromArgb(160, 40, 0);
            }
        }

        private void LoadCanhBao()
        {
            LoadCanhBaoSapHet();
            LoadCanhBaoSapHetHan();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmQuanLyKho
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmQuanLyKho";
            this.Load += new System.EventHandler(this.frmQuanLyKho_Load);
            this.ResumeLayout(false);

        }

        private void frmQuanLyKho_Load(object sender, EventArgs e)
        {

        }

        private void LoadCanhBaoSapHet()
        {
            dgvSapHet.Rows.Clear();
            foreach (var c in _bll.GetHangSapHet((int)nudNguong.Value))
            {
                int idx = dgvSapHet.Rows.Add(c.MaVach, c.TenSP, c.TenLoai, c.TonKhoTong);
                if (c.TonKhoTong == 0)
                    dgvSapHet.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
            }
        }

        private void LoadCanhBaoSapHetHan()
        {
            dgvSapHetHan.Rows.Clear();
            foreach (var c in _bll.GetHangSapHetHan((int)nudSoNgay.Value))
            {
                string hsd = c.HSD.HasValue ? c.HSD.Value.ToString("dd/MM/yyyy") : "—";
                int idx = dgvSapHetHan.Rows.Add(c.MaVach, c.TenSP, c.TenLoai, hsd);
                if (c.HSD.HasValue && (c.HSD.Value - DateTime.Today).TotalDays <= 7)
                    dgvSapHetHan.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 180);
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        // SỰ KIỆN
        // ══════════════════════════════════════════════════════════════════════
        private void BtnThemVaoGioNhap_Click(object sender, EventArgs e)
        {
            if (cboSPNhap.SelectedItem == null) return;
            if (!int.TryParse(txtSLNhap.Text.Trim(), out int sl) || sl <= 0)
            { MessageBox.Show("Số lượng không hợp lệ!"); return; }
            if (!int.TryParse(txtGiaNhap.Text.Trim().Replace(",", "").Replace(".", ""), out int gia) || gia <= 0)
            { MessageBox.Show("Giá nhập không hợp lệ!"); return; }

            var sp = (SanPham_DTO)cboSPNhap.SelectedItem;
            _gioNhap.Add(new ChiTietPhieuNhap_DTO
            {
                SanPhamID = sp.SanPhamID,
                TenSP = sp.TenSP,
                SoLuongNhap = sl,
                GiaNhap = gia,
                NSX = chkNSX.Checked ? dtpNSX.Value : (DateTime?)null,
                HSD = chkHSD.Checked ? dtpHSD.Value : (DateTime?)null,
            });
            RefreshGioNhap();
            txtSLNhap.Text = "1"; txtGiaNhap.Text = ""; txtGhiChuNhap.Text = "";
        }

        private void BtnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedItem == null) { MessageBox.Show("Chọn nhà cung cấp!"); return; }
            var ncc = (NhaCungCap_DTO)cboNCC.SelectedItem;

            long tong = 0;
            foreach (var ct in _gioNhap) tong += (long)ct.SoLuongNhap * ct.GiaNhap;

            var phieu = new PhieuNhap_DTO
            {
                NhaCungCapID = ncc.NhaCungCapID,
                NhanVienID = SessionManager.NhanVienDangNhap?.NhanVienID ?? 0,
                TongTienThanhToan = tong
            };

            var (ok, msg) = _bll.TaoPhieuNhap(phieu, _gioNhap);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { _gioNhap.Clear(); RefreshGioNhap(); LoadLichSuNhap(); }
        }

        private void BtnThemVaoGioXuat_Click(object sender, EventArgs e)
        {
            if (cboSPXuat.SelectedItem == null) return;
            if (!int.TryParse(txtSLXuat.Text.Trim(), out int sl) || sl <= 0)
            { MessageBox.Show("Số lượng không hợp lệ!"); return; }

            var sp = (SanPham_DTO)cboSPXuat.SelectedItem;
            _gioXuat.Add(new ChiTietPhieuXuat_DTO
            {
                SanPhamID = sp.SanPhamID,
                TenSP = sp.TenSP,
                SoLuongXuat = sl,
                TonKhoHienTai = sp.TonKhoTong,
                GhiChu = txtGhiChuXuat.Text.Trim()
            });
            RefreshGioXuat();
            txtSLXuat.Text = "1"; txtGhiChuXuat.Text = "";
        }

        private void BtnLuuPhieuXuat_Click(object sender, EventArgs e)
        {
            var phieu = new PhieuXuat_DTO
            {
                LyDo = cboLyDo.SelectedItem?.ToString() ?? "",
                NhanVienID = SessionManager.NhanVienDangNhap?.NhanVienID ?? 0,
                GhiChu = txtGhiChuPhieuXuat.Text.Trim()
            };

            var (ok, msg) = _bll.TaoPhieuXuat(phieu, _gioXuat);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { _gioXuat.Clear(); RefreshGioXuat(); LoadLichSuXuat(); LoadCanhBaoSapHet(); }
        }

        // ══════════════════════════════════════════════════════════════════════
        // HELPER
        // ══════════════════════════════════════════════════════════════════════
        private void RefreshGioNhap()
        {
            dgvGioNhap.Rows.Clear();
            long tong = 0;
            foreach (var ct in _gioNhap)
            {
                tong += (long)ct.SoLuongNhap * ct.GiaNhap;
                dgvGioNhap.Rows.Add(ct.TenSP, ct.SoLuongNhap,
                    string.Format("{0:N0} đ", ct.GiaNhap),
                    ct.HSD.HasValue ? ct.HSD.Value.ToString("dd/MM/yyyy") : "—");
            }
            lblTongTienNhap.Text = $"Tổng tiền: {tong:N0} đ";
        }

        private void RefreshGioXuat()
        {
            dgvGioXuat.Rows.Clear();
            foreach (var ct in _gioXuat)
                dgvGioXuat.Rows.Add(ct.TenSP, ct.SoLuongXuat, ct.TonKhoHienTai, ct.GhiChu);
        }

        private Label MakeLabel(string text, int x, int y) =>
            new Label { Text = text, Location = new Point(x, y), AutoSize = true };

        private Label MakeBoldLabel(string text, int x, int y) =>
            new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };

        private TextBox MakeTextBox(int x, int y, int w) =>
            new TextBox { Location = new Point(x, y), Width = w };

        private ComboBox MakeCombo(int x, int y, int w) =>
            new ComboBox { Location = new Point(x, y), Width = w, DropDownStyle = ComboBoxStyle.DropDownList };

        private Button MakeButton(string text, int x, int y, int w, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Width = w,
                Height = 32,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private DataGridView MakeDgv(int x, int y, int w, int h)
        {
            var dgv = new DataGridView
            {
                Location = new Point(x, y),
                Width = w,
                Height = h,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 32,
                RowTemplate = { Height = 26 }
            };
            StyleDgv(dgv);
            return dgv;
        }

        private void StyleDgv(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
        }
    }
}