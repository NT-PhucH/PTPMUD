// ===================================================
// File: frmQuanLyKho.Designer.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class frmQuanLyKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // ═══════════════════════════════════════════════════════════════════
        // CONTROLS – khai báo field
        // ═══════════════════════════════════════════════════════════════════
        private TabControl tabMain;

        // ── Tab Nhập ──
        private Panel pnlNhapLeft;
        private ComboBox cboNCC;
        private Button btnThemNCC;
        private ComboBox cboSPNhap;
        private Button btnThemSPNhap;
        private TextBox txtSLNhap, txtGiaNhap;
        private CheckBox chkNSX, chkHSD;
        private DateTimePicker dtpNSX, dtpHSD;
        private DataGridView dgvGioNhap;
        private Label lblTongTienNhap;

        // ── Tab Xuất ──
        private Panel pnlXuatLeft;
        private ComboBox cboLyDo, cboSPXuat;
        private Button btnThemSPXuat;
        private TextBox txtSLXuat, txtGhiChuXuat, txtGhiChuPhieuXuat;
        private DataGridView dgvGioXuat;

        // ── Tab Lịch sử ──
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cboLoaiPhieu;
        private DataGridView dgvLichSuAll, dgvChiTietLichSu;
        private Label lblChiTietTitle;

        // ── Tab Cảnh báo ──
        private DataGridView dgvSapHet, dgvSapHetHan;
        private Label lblNguongInfo, lblHanInfo; // LƯU Ý: Thêm Label để gán Text động

        // ═══════════════════════════════════════════════════════════════════
        // InitializeComponent
        // ═══════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Quản lý kho hàng";
            this.Size = new Size(1280, 760);
            this.MinimumSize = new Size(1024, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f);
            this.BackColor = ColorPalette.Background;

            var sidebar = BuildSidebar();

            tabMain = new TabControl
            {
                Appearance = TabAppearance.FlatButtons,
                DrawMode = TabDrawMode.OwnerDrawFixed,
                ItemSize = new Size(0, 1),
                Dock = DockStyle.Fill,
                Padding = new Point(0, 0)
            };

            tabMain.TabPages.Add(BuildTabNhap());
            tabMain.TabPages.Add(BuildTabXuat());
            tabMain.TabPages.Add(BuildTabLichSu());
            tabMain.TabPages.Add(BuildTabCanhBao());

            var contentWrapper = new Panel { Dock = DockStyle.Fill };
            contentWrapper.Controls.Add(tabMain);

            var masterLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            masterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            masterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            masterLayout.Controls.Add(sidebar, 0, 0);
            masterLayout.Controls.Add(contentWrapper, 1, 0);

            this.Controls.Add(masterLayout);
            this.ResumeLayout(false);
        }

        // ═══════════════════════════════════════════════════════════════════
        // SIDEBAR
        // ═══════════════════════════════════════════════════════════════════
        private Panel BuildSidebar()
        {
            var sidebar = new Panel { Dock = DockStyle.Fill, BackColor = ColorPalette.Sidebar };

            var logoPanel = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = ColorPalette.SidebarDark };
            logoPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // SỬA C# 7.3: Thay vì 'using var' thì dùng block 'using() { }'
                using (var icon = new Font("Segoe UI Emoji", 22f))
                {
                    e.Graphics.DrawString("📦", icon, Brushes.White, 16, 14);
                }
                using (var titleFont = new Font("Segoe UI", 11f, FontStyle.Bold))
                {
                    e.Graphics.DrawString("QUẢN LÝ KHO", titleFont, Brushes.White, 58, 18);
                }
                using (var subFont = new Font("Segoe UI", 7.5f))
                {
                    e.Graphics.DrawString("Warehouse Management", subFont, new SolidBrush(Color.FromArgb(160, 180, 220)), 58, 42);
                }
            };

            var navNhap = BuildNavButton("📥", "Nhập hàng", 0, true);
            var navXuat = BuildNavButton("📤", "Xuất kho", 1, false);
            var navLich = BuildNavButton("📋", "Lịch sử", 2, false);
            var navCanh = BuildNavButton("⚠️", "Cảnh báo", 3, false);

            navNhap.Click += (s, e) => ActivateTab(0, navNhap, navXuat, navLich, navCanh);
            navXuat.Click += (s, e) => ActivateTab(1, navNhap, navXuat, navLich, navCanh);
            navLich.Click += (s, e) => ActivateTab(2, navNhap, navXuat, navLich, navCanh);
            navCanh.Click += (s, e) => ActivateTab(3, navNhap, navXuat, navLich, navCanh);

            _navButtons = new[] { navNhap, navXuat, navLich, navCanh };

            var navFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = ColorPalette.Sidebar
            };
            navFlow.Controls.AddRange(new Control[] { navNhap, navXuat, navLich, navCanh });

            var footer = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = ColorPalette.SidebarDark };
            footer.Paint += (s, e) =>
            {
                using (var f = new Font("Segoe UI", 7.5f))
                {
                    e.Graphics.DrawString("QLST v2.0 • Kho hàng", f, new SolidBrush(Color.FromArgb(120, 150, 190)), 12, 12);
                }
            };

            sidebar.Controls.Add(navFlow);
            sidebar.Controls.Add(logoPanel);
            sidebar.Controls.Add(footer);

            return sidebar;
        }

        private Button BuildNavButton(string icon, string label, int index, bool active)
        {
            var btn = new Button
            {
                Text = $"  {icon}  {label}",
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 200,
                Height = 52,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Tag = index
            };
            btn.FlatAppearance.BorderSize = 0;
            SetNavStyle(btn, active);
            return btn;
        }

        private void SetNavStyle(Button btn, bool active)
        {
            if (active)
            {
                btn.BackColor = ColorPalette.NavActive; btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            }
            else
            {
                btn.BackColor = ColorPalette.Sidebar; btn.ForeColor = Color.FromArgb(180, 200, 235);
                btn.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        // TAB 1: NHẬP HÀNG
        // ═══════════════════════════════════════════════════════════════════
        private TabPage BuildTabNhap()
        {
            var tab = new TabPage(); tab.BackColor = ColorPalette.Background;

            pnlNhapLeft = BuildCard(12, 12, 360, -1, anchorAll: true);
            int y = 16;
            AddSectionTitle(pnlNhapLeft, "Tạo phiếu nhập kho", ref y);

            AddFieldLabel(pnlNhapLeft, "Nhà cung cấp *", y);
            var rowNCC = new Panel { Location = new Point(16, y + 20), Width = 328, Height = 34 };
            cboNCC = MakeCombo(0, 0, 288);
            btnThemNCC = MakeIconButton("＋", 294, 0, 34, ColorPalette.Success);
            rowNCC.Controls.Add(cboNCC); rowNCC.Controls.Add(btnThemNCC);
            pnlNhapLeft.Controls.Add(rowNCC); y += 66;

            AddFieldLabel(pnlNhapLeft, "Sản phẩm *", y);
            var rowSP = new Panel { Location = new Point(16, y + 20), Width = 328, Height = 34 };
            cboSPNhap = MakeCombo(0, 0, 288);
            btnThemSPNhap = MakeIconButton("＋", 294, 0, 34, ColorPalette.Primary);
            rowSP.Controls.Add(cboSPNhap); rowSP.Controls.Add(btnThemSPNhap);
            pnlNhapLeft.Controls.Add(rowSP); y += 66;

            AddFieldLabel(pnlNhapLeft, "Số lượng nhập *", y);
            txtSLNhap = MakeTextBox(16, y + 20, 150); txtSLNhap.Text = "1";
            pnlNhapLeft.Controls.Add(txtSLNhap);

            AddFieldLabel(pnlNhapLeft, "Giá nhập (đ) *", y, xOffset: 182);
            txtGiaNhap = MakeTextBox(182, y + 20, 162);
            pnlNhapLeft.Controls.Add(txtGiaNhap); y += 66;

            AddSeparator(pnlNhapLeft, y); y += 16;
            chkNSX = MakeCheckBox("Ngày sản xuất (NSX)", 16, y);
            chkNSX.CheckedChanged += (s, e) => dtpNSX.Enabled = chkNSX.Checked;
            pnlNhapLeft.Controls.Add(chkNSX);
            dtpNSX = MakeDatePicker(16, y + 24, 152, false);
            pnlNhapLeft.Controls.Add(dtpNSX);

            chkHSD = MakeCheckBox("Hạn sử dụng (HSD)", 182, y);
            chkHSD.CheckedChanged += (s, e) => dtpHSD.Enabled = chkHSD.Checked;
            pnlNhapLeft.Controls.Add(chkHSD);
            dtpHSD = MakeDatePicker(182, y + 24, 162, false);
            pnlNhapLeft.Controls.Add(dtpHSD); y += 72;

            var btnThemGio = MakeStyledButton("➕  Thêm vào phiếu", 16, y, 152, ColorPalette.Success, 36);
            btnThemGio.Name = "btnThemVaoGioNhap";
            pnlNhapLeft.Controls.Add(btnThemGio);

            var btnXoaDong = MakeStyledButton("🗑  Xóa dòng chọn", 182, y, 162, ColorPalette.Danger, 36);
            btnXoaDong.Name = "btnXoaDongNhap";
            pnlNhapLeft.Controls.Add(btnXoaDong); y += 52;

            AddSeparator(pnlNhapLeft, y); y += 16;
            lblTongTienNhap = new Label { Text = "Tổng tiền:  0 đ", Location = new Point(16, y), AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = ColorPalette.Primary };
            pnlNhapLeft.Controls.Add(lblTongTienNhap); y += 36;

            var btnLuu = MakeStyledButton("💾   LƯU PHIẾU NHẬP", 16, y, 328, ColorPalette.Sidebar, 42);
            btnLuu.Name = "btnLuuPhieuNhap";
            pnlNhapLeft.Controls.Add(btnLuu);

            var pRight = BuildCard(384, 12, -1, -1, anchorAll: true, anchorRight: true);
            AddSectionTitle(pRight, "Danh sách sản phẩm trong phiếu (Giỏ hàng)", offsetY: 8);
            dgvGioNhap = BuildDgv(new[] {
                ("Tên sản phẩm", 38, DataGridViewContentAlignment.MiddleLeft),
                ("SL", 10, DataGridViewContentAlignment.MiddleCenter),
                ("Giá nhập", 18, DataGridViewContentAlignment.MiddleRight),
                ("NSX", 17, DataGridViewContentAlignment.MiddleCenter),
                ("HSD", 17, DataGridViewContentAlignment.MiddleCenter)
            }, 16, 48, -1, -1, anchorAll: true);
            pRight.Controls.Add(dgvGioNhap);

            tab.Controls.Add(pnlNhapLeft); tab.Controls.Add(pRight);

            tab.Resize += (s, e) => {
                pnlNhapLeft.Height = tab.Height - 24;
                pRight.Left = 384; pRight.Width = tab.Width - 396; pRight.Height = tab.Height - 24;
                dgvGioNhap.Width = pRight.Width - 32; dgvGioNhap.Height = pRight.Height - 70;
            };

            return tab;
        }

        // ═══════════════════════════════════════════════════════════════════
        // TAB 2: XUẤT KHO
        // ═══════════════════════════════════════════════════════════════════
        private TabPage BuildTabXuat()
        {
            var tab = new TabPage(); tab.BackColor = ColorPalette.Background;

            pnlXuatLeft = BuildCard(12, 12, 360, -1, anchorAll: true);
            int y = 16;
            AddSectionTitle(pnlXuatLeft, "Tạo phiếu xuất kho", ref y);

            AddFieldLabel(pnlXuatLeft, "Lý do xuất *", y);
            cboLyDo = MakeCombo(16, y + 20, 328);
            cboLyDo.Items.AddRange(new object[] { "Hàng hỏng / Vỡ", "Mất mát / Thất lạc", "Hết hạn sử dụng", "Xuất nội bộ", "Khác" });
            cboLyDo.SelectedIndex = 0; pnlXuatLeft.Controls.Add(cboLyDo); y += 66;

            AddFieldLabel(pnlXuatLeft, "Sản phẩm *", y);
            var rowSP = new Panel { Location = new Point(16, y + 20), Width = 328, Height = 34 };
            cboSPXuat = MakeCombo(0, 0, 288);
            btnThemSPXuat = MakeIconButton("＋", 294, 0, 34, ColorPalette.Primary);
            rowSP.Controls.Add(cboSPXuat); rowSP.Controls.Add(btnThemSPXuat);
            pnlXuatLeft.Controls.Add(rowSP); y += 66;

            AddFieldLabel(pnlXuatLeft, "Số lượng xuất *", y);
            txtSLXuat = MakeTextBox(16, y + 20, 328); txtSLXuat.Text = "1";
            pnlXuatLeft.Controls.Add(txtSLXuat); y += 66;

            AddFieldLabel(pnlXuatLeft, "Ghi chú dòng", y);
            txtGhiChuXuat = MakeTextBox(16, y + 20, 328); pnlXuatLeft.Controls.Add(txtGhiChuXuat); y += 66;

            var btnThemGio = MakeStyledButton("➕  Thêm vào phiếu", 16, y, 152, ColorPalette.Success, 36);
            btnThemGio.Name = "btnThemVaoGioXuat"; pnlXuatLeft.Controls.Add(btnThemGio);

            var btnXoa = MakeStyledButton("🗑  Xóa dòng chọn", 182, y, 162, ColorPalette.Danger, 36);
            btnXoa.Name = "btnXoaDongXuat"; pnlXuatLeft.Controls.Add(btnXoa); y += 52;

            AddSeparator(pnlXuatLeft, y); y += 16;

            AddFieldLabel(pnlXuatLeft, "Ghi chú phiếu", y);
            txtGhiChuPhieuXuat = MakeTextBox(16, y + 20, 328); txtGhiChuPhieuXuat.Height = 60; txtGhiChuPhieuXuat.Multiline = true;
            pnlXuatLeft.Controls.Add(txtGhiChuPhieuXuat); y += 90;

            var btnLuu = MakeStyledButton("💾   LƯU PHIẾU XUẤT", 16, y, 328, Color.FromArgb(150, 35, 35), 42);
            btnLuu.Name = "btnLuuPhieuXuat";
            pnlXuatLeft.Controls.Add(btnLuu);

            var pRight = BuildCard(384, 12, -1, -1, anchorAll: true, anchorRight: true);
            AddSectionTitle(pRight, "Danh sách xuất (Giỏ hàng)", offsetY: 8);
            dgvGioXuat = BuildDgv(new[] {
                ("Tên sản phẩm", 45, DataGridViewContentAlignment.MiddleLeft),
                ("SL xuất", 15, DataGridViewContentAlignment.MiddleCenter),
                ("Tồn kho hiện tại", 20, DataGridViewContentAlignment.MiddleCenter),
                ("Ghi chú", 20, DataGridViewContentAlignment.MiddleLeft)
            }, 16, 48, -1, -1, anchorAll: true);
            pRight.Controls.Add(dgvGioXuat);

            tab.Controls.Add(pnlXuatLeft); tab.Controls.Add(pRight);

            tab.Resize += (s, e) => {
                pnlXuatLeft.Height = tab.Height - 24;
                pRight.Left = 384; pRight.Width = tab.Width - 396; pRight.Height = tab.Height - 24;
                dgvGioXuat.Width = pRight.Width - 32; dgvGioXuat.Height = pRight.Height - 70;
            };

            return tab;
        }

        // ═══════════════════════════════════════════════════════════════════
        // TAB 3: LỊCH SỬ TỔNG HỢP
        // ═══════════════════════════════════════════════════════════════════
        private TabPage BuildTabLichSu()
        {
            var tab = new TabPage(); tab.BackColor = ColorPalette.Background;

            var pFilter = new Panel { Location = new Point(12, 12), Height = 50, BackColor = Color.White, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            StyleCard(pFilter);

            pFilter.Controls.Add(MakeSmallLabel("Từ ngày:", 12, 16));
            dtpFrom = new DateTimePicker { Location = new Point(72, 12), Width = 120, Format = DateTimePickerFormat.Short, Value = System.DateTime.Today.AddMonths(-1) };
            pFilter.Controls.Add(dtpFrom);

            pFilter.Controls.Add(MakeSmallLabel("Đến ngày:", 208, 16));
            dtpTo = new DateTimePicker { Location = new Point(278, 12), Width = 120, Format = DateTimePickerFormat.Short };
            pFilter.Controls.Add(dtpTo);

            pFilter.Controls.Add(MakeSmallLabel("Loại:", 414, 16));
            cboLoaiPhieu = new ComboBox { Location = new Point(446, 12), Width = 110, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiPhieu.Items.AddRange(new object[] { "Tất cả", "Phiếu nhập", "Phiếu xuất" });
            cboLoaiPhieu.SelectedIndex = 0; pFilter.Controls.Add(cboLoaiPhieu);

            var btnLoc = MakeStyledButton("🔍  Tìm kiếm", 572, 10, 110, ColorPalette.Primary, 30);
            btnLoc.Name = "btnLocLichSu"; pFilter.Controls.Add(btnLoc);

            var btnRefresh = MakeStyledButton("↺", 690, 10, 34, Color.FromArgb(100, 100, 120), 30);
            btnRefresh.Name = "btnRefreshLichSu"; btnRefresh.Font = new Font("Segoe UI", 12f);
            pFilter.Controls.Add(btnRefresh);

            var pListCard = BuildCard(12, 74, -1, -1, anchorAll: true);
            AddSectionTitle(pListCard, "Danh sách phiếu", offsetY: 8);
            dgvLichSuAll = BuildDgv(new[] {
                ("Loại", 8, DataGridViewContentAlignment.MiddleCenter),
                ("Mã phiếu", 14, DataGridViewContentAlignment.MiddleLeft),
                ("Đối tác / Lý do", 28, DataGridViewContentAlignment.MiddleLeft),
                ("Nhân viên", 18, DataGridViewContentAlignment.MiddleLeft),
                ("Ngày", 14, DataGridViewContentAlignment.MiddleCenter),
                ("Giá trị", 18, DataGridViewContentAlignment.MiddleRight)
            }, 16, 46, -1, -1, anchorAll: true);
            pListCard.Controls.Add(dgvLichSuAll);

            var pDetailCard = BuildCard(12, -1, -1, 220, anchorAll: true, anchorBottom: true);
            lblChiTietTitle = new Label { Text = "Chi tiết phiếu — chọn một phiếu ở trên để xem", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = ColorPalette.Primary };
            pDetailCard.Controls.Add(lblChiTietTitle);

            dgvChiTietLichSu = BuildDgv(new[] {
                ("Tên sản phẩm", 38, DataGridViewContentAlignment.MiddleLeft),
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Số lượng", 13, DataGridViewContentAlignment.MiddleCenter),
                ("Đơn giá", 17, DataGridViewContentAlignment.MiddleRight),
                ("NSX", 8, DataGridViewContentAlignment.MiddleCenter),
                ("HSD", 8, DataGridViewContentAlignment.MiddleCenter)
            }, 16, 38, -1, 152, anchorAll: true);
            pDetailCard.Controls.Add(dgvChiTietLichSu);

            tab.Controls.Add(pFilter); tab.Controls.Add(pListCard); tab.Controls.Add(pDetailCard);

            tab.Resize += (s, e) => {
                pFilter.Width = tab.Width - 24; pListCard.Width = tab.Width - 24;
                pListCard.Height = tab.Height - 24 - 50 - 12 - 220 - 8;
                dgvLichSuAll.Width = pListCard.Width - 32; dgvLichSuAll.Height = pListCard.Height - 60;
                pDetailCard.Top = tab.Height - 24 - 220; pDetailCard.Width = tab.Width - 24;
                dgvChiTietLichSu.Width = pDetailCard.Width - 32;
            };

            return tab;
        }

        // ═══════════════════════════════════════════════════════════════════
        // TAB 4: CẢNH BÁO
        // ═══════════════════════════════════════════════════════════════════
        private TabPage BuildTabCanhBao()
        {
            var tab = new TabPage(); tab.BackColor = ColorPalette.Background;

            var pHet = BuildCard(12, 12, -1, -1, anchorAll: true);
            var titleHet = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(230, 50, 50) };
            titleHet.Paint += (s, e) => {
                using (var f = new Font("Segoe UI", 10f, FontStyle.Bold)) { e.Graphics.DrawString("🔴  HÀNG SẮP HẾT TỒN KHO", f, Brushes.White, 16, 14); }
            };

            var pNguongInfo = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.FromArgb(255, 240, 240) };
            lblNguongInfo = new Label { Text = "Ngưỡng cảnh báo lấy từ cài đặt hệ thống.", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic), ForeColor = Color.FromArgb(150, 30, 30) };
            pNguongInfo.Controls.Add(lblNguongInfo);
            var btnReloadHet = MakeStyledButton("↺ Tải lại", 0, 6, 90, Color.FromArgb(200, 60, 60), 26);
            btnReloadHet.Name = "btnReloadSapHet"; btnReloadHet.Dock = DockStyle.Right;
            pNguongInfo.Controls.Add(btnReloadHet);

            dgvSapHet = BuildDgvFlat(new[] {
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Tên sản phẩm", 42, DataGridViewContentAlignment.MiddleLeft),
                ("Loại hàng", 22, DataGridViewContentAlignment.MiddleLeft),
                ("Tồn kho", 20, DataGridViewContentAlignment.MiddleCenter)
            });
            dgvSapHet.Dock = DockStyle.Fill;
            pHet.Controls.Add(dgvSapHet); pHet.Controls.Add(pNguongInfo); pHet.Controls.Add(titleHet);

            var pHan = BuildCard(12, -1, -1, -1, anchorAll: true, anchorBottom: true);
            var titleHan = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(200, 140, 0) };
            titleHan.Paint += (s, e) => {
                using (var f = new Font("Segoe UI", 10f, FontStyle.Bold)) { e.Graphics.DrawString("🟡  HÀNG SẮP HẾT HẠN SỬ DỤNG", f, Brushes.White, 16, 14); }
            };

            var pHanInfo = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.FromArgb(255, 248, 220) };
            lblHanInfo = new Label { Text = "Số ngày cảnh báo lấy từ cài đặt hệ thống.", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic), ForeColor = Color.FromArgb(130, 90, 0) };
            pHanInfo.Controls.Add(lblHanInfo);
            var btnReloadHan = MakeStyledButton("↺ Tải lại", 0, 6, 90, Color.FromArgb(180, 120, 0), 26);
            btnReloadHan.Name = "btnReloadSapHan"; btnReloadHan.Dock = DockStyle.Right;
            pHanInfo.Controls.Add(btnReloadHan);

            dgvSapHetHan = BuildDgvFlat(new[] {
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Tên sản phẩm", 38, DataGridViewContentAlignment.MiddleLeft),
                ("Loại hàng", 22, DataGridViewContentAlignment.MiddleLeft),
                ("Hạn sử dụng", 14, DataGridViewContentAlignment.MiddleCenter),
                ("Còn lại", 10, DataGridViewContentAlignment.MiddleCenter)
            });
            dgvSapHetHan.Dock = DockStyle.Fill;
            pHan.Controls.Add(dgvSapHetHan); pHan.Controls.Add(pHanInfo); pHan.Controls.Add(titleHan);

            tab.Controls.Add(pHet); tab.Controls.Add(pHan);

            tab.Resize += (s, e) => {
                int halfH = (tab.Height - 36) / 2;
                pHet.Location = new Point(12, 12); pHet.Width = tab.Width - 24; pHet.Height = halfH;
                pHan.Location = new Point(12, 12 + halfH + 8); pHan.Width = tab.Width - 24; pHan.Height = halfH;
            };

            return tab;
        }

        // ═══════════════════════════════════════════════════════════════════
        // UI HELPERS (Colors, Buttons, DataGridViews)
        // ═══════════════════════════════════════════════════════════════════
        private static class ColorPalette
        {
            public static Color Background = Color.FromArgb(242, 245, 250);
            public static Color Sidebar = Color.FromArgb(28, 38, 58);
            public static Color SidebarDark = Color.FromArgb(18, 26, 42);
            public static Color NavActive = Color.FromArgb(52, 100, 220);
            public static Color Primary = Color.FromArgb(40, 90, 200);
            public static Color Success = Color.FromArgb(26, 140, 80);
            public static Color Danger = Color.FromArgb(195, 50, 50);
            public static Color CardBg = Color.White;
            public static Color HeaderBg = Color.FromArgb(28, 38, 58);
            public static Color HeaderFg = Color.White;
            public static Color Border = Color.FromArgb(220, 225, 235);
            public static Color AltRow = Color.FromArgb(244, 247, 255);
        }

        private Panel BuildCard(int x, int y, int w, int h, bool anchorAll = false, bool anchorRight = false, bool anchorBottom = false)
        {
            var p = new Panel { BackColor = ColorPalette.CardBg }; StyleCard(p);
            if (x >= 0) p.Location = new Point(x, y >= 0 ? y : 0);
            if (w >= 0) p.Width = w; if (h >= 0) p.Height = h;
            if (anchorAll && !anchorRight && !anchorBottom) p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            else if (anchorAll && anchorRight && !anchorBottom) p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            else if (anchorAll && anchorBottom) p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            return p;
        }
        private void StyleCard(Panel p) { p.Padding = new Padding(0); }
        private void AddSectionTitle(Panel parent, string text, ref int y, int x = 16)
        {
            parent.Controls.Add(new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = ColorPalette.Sidebar });
            parent.Controls.Add(new Panel { Location = new Point(x, y + 26), Width = parent.Width - x * 2, Height = 2, BackColor = ColorPalette.Border });
            y += 40;
        }
        private void AddSectionTitle(Panel parent, string text, int offsetY = 0) { int y = offsetY; AddSectionTitle(parent, text, ref y); }
        private void AddFieldLabel(Panel parent, string text, int y, int xOffset = 16) => parent.Controls.Add(new Label { Text = text, Location = new Point(xOffset, y), AutoSize = true, Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(90, 100, 120) });
        private void AddSeparator(Panel parent, int y) => parent.Controls.Add(new Panel { Location = new Point(16, y), Width = parent.Width - 32, Height = 1, BackColor = ColorPalette.Border });
        private TextBox MakeTextBox(int x, int y, int w) => new TextBox { Location = new Point(x, y), Width = w, Font = new Font("Segoe UI", 9.5f), BorderStyle = BorderStyle.FixedSingle };
        private ComboBox MakeCombo(int x, int y, int w) => new ComboBox { Location = new Point(x, y), Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f), Height = 34 };
        private CheckBox MakeCheckBox(string text, int x, int y) => new CheckBox { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(60, 80, 120) };
        private DateTimePicker MakeDatePicker(int x, int y, int w, bool enabled) => new DateTimePicker { Location = new Point(x, y), Width = w, Format = DateTimePickerFormat.Short, Enabled = enabled };
        private Button MakeStyledButton(string text, int x, int y, int w, Color color, int h = 32) => new Button { Text = text, Location = new Point(x, y), Width = w, Height = h, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold), Cursor = Cursors.Hand };
        private Button MakeIconButton(string icon, int x, int y, int size, Color color) { var btn = new Button { Text = icon, Location = new Point(x, y), Width = size, Height = size, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11f, FontStyle.Bold), Cursor = Cursors.Hand }; btn.FlatAppearance.BorderSize = 0; return btn; }
        private Label MakeSmallLabel(string text, int x, int y) => new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(80, 80, 100) };

        private DataGridView BuildDgv((string header, int weight, DataGridViewContentAlignment align)[] cols, int x, int y, int w, int h, bool anchorAll = false)
        {
            var dgv = new DataGridView { Location = new Point(x, y), BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 36, RowTemplate = { Height = 30 }, GridColor = ColorPalette.Border, CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal };
            if (w >= 0) dgv.Width = w; if (h >= 0) dgv.Height = h;
            if (anchorAll) dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StyleDgv(dgv);
            foreach (var (header, weight, align) in cols) dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = header, FillWeight = weight, DefaultCellStyle = { Alignment = align } });
            return dgv;
        }

        private DataGridView BuildDgvFlat((string header, int weight, DataGridViewContentAlignment align)[] cols) { var dgv = BuildDgv(cols, 0, 0, 0, 0); dgv.Anchor = AnchorStyles.None; return dgv; }

        private void StyleDgv(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorPalette.HeaderBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorPalette.HeaderFg;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorPalette.AltRow;
            dgv.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgv.DefaultCellStyle.SelectionBackColor = ColorPalette.Primary;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        }
    }
}