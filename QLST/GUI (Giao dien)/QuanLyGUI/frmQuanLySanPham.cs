// ===================================================
// File: frmQuanLySanPham.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI  (hoặc tương đương)
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmQuanLySanPham : Form
    {
        // ── BLL ───────────────────────────────────────────────────────────────
        private readonly SanPham_BLL _bll = new SanPham_BLL();

        // ── TRẠNG THÁI ────────────────────────────────────────────────────────
        private int _selectedID = -1;
        private string _selectedImagePath = "";

        // ── CONTROLS ──────────────────────────────────────────────────────────
        private DataGridView dgvSanPham;
        private TextBox txtTimKiem;
        private ComboBox cboLocLoai;
        private TextBox txtMaVach, txtTenSP, txtGia;
        private ComboBox cboLoai;
        private PictureBox picAnh;
        private Button btnChonAnh, btnThem, btnSua, btnXoa, btnLamMoi;
        private Button btnThemLoai;
        private Label lblTitle;
        private Panel panelLeft, panelRight, panelTop, panelBottom;

        public frmQuanLySanPham()
        {
            InitializeComponent();
            LoadLoai();
            LoadData();
        }

        // ══════════════════════════════════════════════════════════════════════
        // KHỞI TẠO GIAO DIỆN
        // ══════════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.Text = "Quản lý sản phẩm";
            this.Size = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f);
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ── PANEL TOP (thanh tìm kiếm) ────────────────────────────────────
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.FromArgb(30, 40, 60),
                Padding = new Padding(10, 10, 10, 0)
            };

            lblTitle = new Label
            {
                Text = "🛒  QUẢN LÝ SẢN PHẨM",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Location = new Point(15, 13),
                AutoSize = true
            };

            var lblSearch = new Label { Text = "Tìm:", ForeColor = Color.White, Location = new Point(280, 17), AutoSize = true };

            txtTimKiem = new TextBox { Location = new Point(315, 14), Width = 220, Height = 28 };
            txtTimKiem.TextChanged += (s, e) => LoadData();

            var lblLoc = new Label { Text = "Lọc loại:", ForeColor = Color.White, Location = new Point(550, 17), AutoSize = true };

            cboLocLoai = new ComboBox
            {
                Location = new Point(615, 14),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboLocLoai.SelectedIndexChanged += (s, e) => LoadData();

            panelTop.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtTimKiem, lblLoc, cboLocLoai });

            // ── PANEL LEFT (DataGridView) ─────────────────────────────────────
            panelLeft = new Panel
            {
                Location = new Point(0, 55),
                Width = 650,
                Height = this.ClientSize.Height - 55,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };

            dgvSanPham = new DataGridView
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
                RowTemplate = { Height = 30 }
            };
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvSanPham.EnableHeadersVisualStyles = false;
            dgvSanPham.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
            dgvSanPham.SelectionChanged += DgvSanPham_SelectionChanged;

            // Thêm cột thủ công (không bind trực tiếp để kiểm soát hiển thị)
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", Width = 45, FillWeight = 5 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã vạch", FillWeight = 15 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên sản phẩm", FillWeight = 35 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 20 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGia", HeaderText = "Giá bán", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTon", HeaderText = "Tồn kho", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            panelLeft.Controls.Add(dgvSanPham);

            // ── PANEL RIGHT (Form nhập liệu) ──────────────────────────────────
            panelRight = new Panel
            {
                Location = new Point(655, 55),
                Width = this.ClientSize.Width - 655,
                Height = this.ClientSize.Height - 55,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            int y = 15;
            int labelW = 90;
            int inputX = 110;
            int inputW = 200;

            // Hình ảnh
            picAnh = new PictureBox
            {
                Location = new Point(inputX, y),
                Size = new Size(180, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 247, 250)
            };
            panelRight.Controls.Add(picAnh);

            y += 160;
            btnChonAnh = CreateButton("📷 Chọn ảnh", inputX, y, 180, Color.FromArgb(100, 120, 200));
            btnChonAnh.Click += BtnChonAnh_Click;
            panelRight.Controls.Add(btnChonAnh);

            y += 45;
            panelRight.Controls.Add(MakeLabel("Mã vạch:", 15, y, labelW));
            txtMaVach = MakeTextBox(inputX, y, inputW);
            panelRight.Controls.Add(txtMaVach);

            y += 38;
            panelRight.Controls.Add(MakeLabel("Tên SP:", 15, y, labelW));
            txtTenSP = MakeTextBox(inputX, y, inputW);
            panelRight.Controls.Add(txtTenSP);

            y += 38;
            panelRight.Controls.Add(MakeLabel("Giá bán:", 15, y, labelW));
            txtGia = MakeTextBox(inputX, y, inputW);
            panelRight.Controls.Add(txtGia);

            y += 38;
            panelRight.Controls.Add(MakeLabel("Loại SP:", 15, y, labelW));
            cboLoai = new ComboBox
            {
                Location = new Point(inputX, y),
                Width = inputW,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelRight.Controls.Add(cboLoai);

            btnThemLoai = CreateButton("+ Thêm loại", inputX + inputW + 5, y, 90, Color.FromArgb(80, 160, 100));
            btnThemLoai.Height = 26;
            btnThemLoai.Click += BtnThemLoai_Click;
            panelRight.Controls.Add(btnThemLoai);

            // ── Các nút hành động ─────────────────────────────────────────────
            y += 55;
            btnThem = CreateButton("➕ THÊM", 15, y, 95, Color.FromArgb(34, 139, 34));
            btnThem.Click += BtnThem_Click;
            panelRight.Controls.Add(btnThem);

            btnSua = CreateButton("✏ SỬA", 120, y, 95, Color.FromArgb(30, 100, 200));
            btnSua.Click += BtnSua_Click;
            panelRight.Controls.Add(btnSua);

            btnXoa = CreateButton("🗑 XÓA", 225, y, 95, Color.FromArgb(200, 50, 50));
            btnXoa.Click += BtnXoa_Click;
            panelRight.Controls.Add(btnXoa);

            y += 45;
            btnLamMoi = CreateButton("🔄 Làm mới", 15, y, 305, Color.FromArgb(130, 130, 130));
            btnLamMoi.Click += (s, e) => ClearForm();
            panelRight.Controls.Add(btnLamMoi);

            // ── Gắn vào Form ──────────────────────────────────────────────────
            this.Controls.AddRange(new Control[] { panelTop, panelLeft, panelRight });
            this.Resize += (s, e) => {
                panelLeft.Height = this.ClientSize.Height - 55;
                panelRight.Left = 655;
                panelRight.Width = this.ClientSize.Width - 655;
                panelRight.Height = this.ClientSize.Height - 55;
            };
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD DỮ LIỆU
        // ══════════════════════════════════════════════════════════════════════
        private void LoadLoai()
        {
            var loaiList = _bll.GetAllLoai();

            // ComboBox lọc (thêm mục "Tất cả")
            cboLocLoai.Items.Clear();
            cboLocLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Tất cả --" });
            foreach (var l in loaiList) cboLocLoai.Items.Add(l);
            cboLocLoai.DisplayMember = "TenLoai";
            cboLocLoai.ValueMember = "LoaiSanPhamID";
            cboLocLoai.SelectedIndex = 0;

            // ComboBox form nhập
            cboLoai.Items.Clear();
            cboLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Chọn loại --" });
            foreach (var l in loaiList) cboLoai.Items.Add(l);
            cboLoai.DisplayMember = "TenLoai";
            cboLoai.ValueMember = "LoaiSanPhamID";
            cboLoai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string kw = txtTimKiem?.Text ?? "";
            int loaiID = 0;
            if (cboLocLoai?.SelectedItem is LoaiSanPham_DTO selected)
                loaiID = selected.LoaiSanPhamID;

            List<SanPham_DTO> list;
            if (!string.IsNullOrWhiteSpace(kw))
                list = _bll.Search(kw);
            else
                list = _bll.GetByLoai(loaiID);

            dgvSanPham.Rows.Clear();
            foreach (var sp in list)
            {
                var row = dgvSanPham.Rows.Add(
                    sp.SanPhamID,
                    sp.MaVach,
                    sp.TenSP,
                    sp.TenLoai,
                    string.Format("{0:N0} đ", sp.GiaBanHienTai),
                    sp.TonKhoTong
                );
                // Tô đỏ nếu tồn kho thấp
                if (sp.TonKhoTong <= 5)
                    dgvSanPham.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        // SỰ KIỆN
        // ══════════════════════════════════════════════════════════════════════
        private void DgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0) return;
            var row = dgvSanPham.SelectedRows[0];

            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);
            txtMaVach.Text = row.Cells["colMa"].Value?.ToString();
            txtTenSP.Text = row.Cells["colTen"].Value?.ToString();
            // Lấy giá thuần (bỏ định dạng " đ" và dấu phẩy)
            string giaRaw = row.Cells["colGia"].Value?.ToString()
                .Replace("đ", "").Replace(",", "").Replace(".", "").Trim();
            txtGia.Text = giaRaw;

            // Chọn đúng loại trong ComboBox
            string tenLoai = row.Cells["colLoai"].Value?.ToString();
            foreach (LoaiSanPham_DTO item in cboLoai.Items)
            {
                if (item.TenLoai == tenLoai) { cboLoai.SelectedItem = item; break; }
            }

            // Hiện ảnh (nếu có)
            var spList = _bll.GetAll();
            var sp = spList.Find(x => x.SanPhamID == _selectedID);
            if (sp != null && !string.IsNullOrEmpty(sp.HinhAnh) && File.Exists(sp.HinhAnh))
            {
                _selectedImagePath = sp.HinhAnh;
                picAnh.Image = Image.FromFile(sp.HinhAnh);
            }
            else
            {
                _selectedImagePath = "";
                picAnh.Image = null;
            }
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dlg.Title = "Chọn ảnh sản phẩm";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = dlg.FileName;
                    picAnh.Image = Image.FromFile(_selectedImagePath);
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var sp = BuildDTO();
            if (sp == null) return;

            var (ok, msg) = _bll.ThemSanPham(sp);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!"); return; }
            var sp = BuildDTO();
            if (sp == null) return;
            sp.SanPhamID = _selectedID;

            var (ok, msg) = _bll.SuaSanPham(sp);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!"); return; }

            var confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn xóa sản phẩm này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var (ok, msg) = _bll.XoaSanPham(_selectedID);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void BtnThemLoai_Click(object sender, EventArgs e)
        {
            string tenLoai = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập tên loại sản phẩm mới:", "Thêm loại", "");
            if (string.IsNullOrWhiteSpace(tenLoai)) return;

            var (ok, msg) = _bll.ThemLoai(tenLoai);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) LoadLoai();
        }

        // ══════════════════════════════════════════════════════════════════════
        // HELPER
        // ══════════════════════════════════════════════════════════════════════
        private SanPham_DTO BuildDTO()
        {
            if (!int.TryParse(txtGia.Text.Trim().Replace(",", "").Replace(".", ""), out int gia))
            {
                MessageBox.Show("Giá bán phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            int loaiID = 0;
            if (cboLoai.SelectedItem is LoaiSanPham_DTO l) loaiID = l.LoaiSanPhamID;

            return new SanPham_DTO
            {
                MaVach = txtMaVach.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                GiaBanHienTai = gia,
                LoaiSanPhamID = loaiID,
                HinhAnh = _selectedImagePath
            };
        }

        private void ClearForm()
        {
            _selectedID = -1;
            _selectedImagePath = "";
            txtMaVach.Text = "";
            txtTenSP.Text = "";
            txtGia.Text = "";
            cboLoai.SelectedIndex = 0;
            picAnh.Image = null;
            dgvSanPham.ClearSelection();
        }

        // ── Factory helpers cho control ───────────────────────────────────────
        private Label MakeLabel(string text, int x, int y, int w) =>
            new Label { Text = text, Location = new Point(x, y + 3), Width = w, Font = new Font("Segoe UI", 9.5f) };

        private TextBox MakeTextBox(int x, int y, int w) =>
            new TextBox { Location = new Point(x, y), Width = w };

        private Button CreateButton(string text, int x, int y, int w, Color backColor)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Width = w,
                Height = 32,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }
    }
}