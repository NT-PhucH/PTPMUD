// ===================================================
// File: frmQuanLyKhachHang.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmQuanLyKhachHang : Form
    {
        private readonly KhachHang_BLL _bll = new KhachHang_BLL();
        private int _selectedID = -1;
        private int _selectedDiem = 0;
        private bool _dangLoad = false;

        private DataGridView dgvKH, dgvLichSu;
        private TextBox txtTimKiem, txtSDT, txtTenKH;
        private Label lblTongKH, lblTongDiem, lblDiemHienTai;
        private NumericUpDown nudDiem;
        private Button btnSua, btnLamMoi;
        private Panel panelStats;

        public frmQuanLyKhachHang()
        {
            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            Text = "Quản lý khách hàng & Tích điểm";
            Size = new Size(1150, 680);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label
            {
                Text = "👥  QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Location = new Point(15, 13),
                AutoSize = true
            });

            // Stats bar
            panelStats = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(44, 62, 80), Padding = new Padding(15, 10, 0, 0) };
            lblTongKH = new Label { Text = "Tổng KH: 0", ForeColor = Color.White, Location = new Point(15, 14), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            lblTongDiem = new Label { Text = "Tổng điểm: 0", ForeColor = Color.FromArgb(46, 204, 113), Location = new Point(160, 14), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            var lblRule = new Label { Text = "⭐ Quy tắc: 10.000đ = 1 điểm", ForeColor = Color.FromArgb(241, 196, 15), Location = new Point(320, 14), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };
            panelStats.Controls.AddRange(new Control[] { lblTongKH, lblTongDiem, lblRule });

            // Search bar
            var pSearch = new Panel { Dock = DockStyle.Top, Height = 44, BackColor = Color.White, Padding = new Padding(10, 8, 10, 0) };
            pSearch.Controls.Add(new Label { Text = "🔍", Location = new Point(10, 12), AutoSize = true });
            txtTimKiem = new TextBox { Location = new Point(35, 9), Width = 280 };
            txtTimKiem.TextChanged += (s, e) => LoadData();
            pSearch.Controls.Add(txtTimKiem);

            // Panel trái: danh sách KH + lịch sử
            var pLeft = new Panel { Location = new Point(0, 144), Width = 680, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom };
            pLeft.Height = 536;

            // DGV khách hàng
            dgvKH = MakeDgv(0, 0, 680, 300);
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", FillWeight = 5 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSDT", HeaderText = "SĐT", FillWeight = 16 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên KH", FillWeight = 25 });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiem", HeaderText = "⭐ Điểm", FillWeight = 12, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHD", HeaderText = "Số HĐ", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTong", HeaderText = "Tổng chi tiêu", FillWeight = 22, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvKH.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHang", HeaderText = "Hạng", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKH.SelectionChanged += DgvKH_SelectionChanged;
            pLeft.Controls.Add(dgvKH);

            pLeft.Controls.Add(new Label { Text = "📋 Lịch sử mua hàng:", Location = new Point(5, 308), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) });
            dgvLichSu = MakeDgv(0, 328, 680, 200);
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaHD", HeaderText = "Mã HĐ", FillWeight = 20 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTGian", HeaderText = "Thời gian", FillWeight = 28 });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTien", HeaderText = "Tổng tiền", FillWeight = 28, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiemCong", HeaderText = "+Điểm", FillWeight = 14, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(34, 139, 34) } });
            pLeft.Controls.Add(dgvLichSu);

            // Panel phải: form nhập + quản lý điểm
            var pRight = new Panel
            {
                Location = new Point(685, 144),
                Width = 450,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            pRight.Height = 536;

            int y = 15;
            pRight.Controls.Add(MakeBold("THÔNG TIN KHÁCH HÀNG", 15, y));

            y += 35;
            pRight.Controls.Add(MakeLabel("Số điện thoại: *", 15, y));
            txtSDT = MakeTextBox(15, y + 22, 390); pRight.Controls.Add(txtSDT);

            y += 60;
            pRight.Controls.Add(MakeLabel("Tên khách hàng: *", 15, y));
            txtTenKH = MakeTextBox(15, y + 22, 390); pRight.Controls.Add(txtTenKH);

            y += 60;
            btnSua = MakeBtn("✏ SỬA", 145, y, 120, Color.FromArgb(30, 100, 200));
            btnSua.Click += BtnSua_Click;
            pRight.Controls.AddRange(new Control[] { btnSua});

            y += 45;
            btnLamMoi = MakeBtn("🔄 Làm mới", 15, y, 390, Color.FromArgb(130, 130, 130));
            btnLamMoi.Click += (s, e) => ClearForm();
            pRight.Controls.Add(btnLamMoi);

            // Phần quản lý điểm
            y += 55;
            var sep = new Panel { Location = new Point(15, y), Width = 390, Height = 2, BackColor = Color.FromArgb(220, 220, 220) };
            pRight.Controls.Add(sep);

            y += 15;
            pRight.Controls.Add(MakeBold("QUẢN LÝ ĐIỂM TÍCH LŨY", 15, y));

            y += 30;
            lblDiemHienTai = new Label { Text = "Điểm hiện tại: —", Location = new Point(15, y), AutoSize = true, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 100, 200) };
            pRight.Controls.Add(lblDiemHienTai);

            y += 30;
            pRight.Controls.Add(MakeLabel("Điểm mới:", 15, y));
            nudDiem = new NumericUpDown { Location = new Point(15, y + 22), Width = 140, Minimum = 0, Maximum = 999999, Font = new Font("Segoe UI", 11f) };
            pRight.Controls.Add(nudDiem);


            y += 70;
            // Bảng quy đổi hạng
            var lblHang = new Label
            {
                Text = "🏅 Bạc: ≥100đ  |  🥇 Vàng: ≥500đ  |  💎 VIP: ≥1000đ",
                Location = new Point(15, y),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            };
            pRight.Controls.Add(lblHang);

            Controls.AddRange(new Control[] { pLeft, pRight, pSearch, panelStats, header });
            Resize += (s, e) => {
                pLeft.Height = ClientSize.Height - 144;
                pRight.Height = ClientSize.Height - 144;
                pRight.Left = ClientSize.Width - 460;
                pLeft.Width = ClientSize.Width - 475;
                dgvKH.Width = pLeft.Width;
                dgvLichSu.Width = pLeft.Width;
            };
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

                // Tô màu theo hạng
                if (kh.DiemTichLuy >= 1000) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(230, 210, 255);
                else if (kh.DiemTichLuy >= 500) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
                else if (kh.DiemTichLuy >= 100) dgvKH.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            }

            lblTongKH.Text = $"Tổng KH: {list.Count}";
            lblTongDiem.Text = $"Tổng điểm: {tongDiem:N0}";
            _dangLoad = false;
        }

        private void DgvKH_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangLoad || dgvKH.SelectedRows.Count == 0) return;
            var row = dgvKH.SelectedRows[0];
            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);
            _selectedDiem = Convert.ToInt32(row.Cells["colDiem"].Value);
            txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtTenKH.Text = row.Cells["colTen"].Value?.ToString();
            lblDiemHienTai.Text = $"Điểm hiện tại: {_selectedDiem:N0} điểm";
            nudDiem.Value = _selectedDiem;
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


        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn khách hàng cần sửa!"); return; }
            var (ok, msg) = _bll.Sua(new KhachHang_DTO { KhachHangID = _selectedID, SDT = txtSDT.Text.Trim(), TenKH = txtTenKH.Text.Trim() });
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { ClearForm(); LoadData(); }
        }



        private void ClearForm()
        {
            _selectedID = -1; _selectedDiem = 0;
            txtSDT.Text = txtTenKH.Text = "";
            lblDiemHienTai.Text = "Điểm hiện tại: —";
            nudDiem.Value = 0;
            dgvLichSu.Rows.Clear();
            dgvKH.ClearSelection();
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
                ColumnHeadersHeight = 34,
                RowTemplate = { Height = 28 }
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
            return dgv;
        }

        private Label MakeLabel(string t, int x, int y) =>
            new Label { Text = t, Location = new Point(x, y), AutoSize = true };
        private Label MakeBold(string t, int x, int y) =>
            new Label { Text = t, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 60) };
        private TextBox MakeTextBox(int x, int y, int w) =>
            new TextBox { Location = new Point(x, y), Width = w };
        private Button MakeBtn(string t, int x, int y, int w, Color c) =>
            new Button
            {
                Text = t,
                Location = new Point(x, y),
                Width = w,
                Height = 32,
                BackColor = c,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
    }
}