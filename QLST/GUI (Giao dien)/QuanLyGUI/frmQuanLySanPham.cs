// ===================================================
// File: frmQuanLySanPham.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
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
        private readonly SanPham_BLL _bll = new SanPham_BLL();

        private int _selectedID = -1;
        private string _selectedImagePath = "";

        private List<SanPham_DTO> _currentList = new List<SanPham_DTO>();
        private DataGridView dgvSanPham;
        private TextBox txtTimKiem;
        private ComboBox cboLocLoai;
        private TextBox txtMaVach, txtTenSP, txtGia;
        private ComboBox cboLoai;
        private PictureBox picAnh;
        private Button btnChonAnh, btnThem, btnSua, btnTrangThai, btnLamMoi;
        private Button btnThemLoai;
        private Label lblTitle;
        private Panel panelLeft, panelRight, panelTop;

        public frmQuanLySanPham()
        {
            InitializeComponent(); // Gọi cái này trước để lừa Designer
            BuildUI();             // Gọi code vẽ UI xịn của mình sau
            LoadLoai();
            LoadData();
        }

        // ══════════════════════════════════════════════════════════════════════
        // KHỞI TẠO GIAO DIỆN
        // ══════════════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            this.Text = "Quản lý sản phẩm";
            this.Size = new Size(1180, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f);
            this.BackColor = Color.FromArgb(245, 247, 250);

            // ── PANEL TOP ────────────────────────────────────
            panelTop = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.FromArgb(30, 40, 60), Padding = new Padding(10, 10, 10, 0) };

            lblTitle = new Label { Text = "🛒  QUẢN LÝ SẢN PHẨM", ForeColor = Color.White, Font = new Font("Segoe UI", 13f, FontStyle.Bold), Location = new Point(15, 13), AutoSize = true };
            var lblSearch = new Label { Text = "🔍 Tìm kiếm:", ForeColor = Color.White, Location = new Point(310, 17), AutoSize = true };
            txtTimKiem = new TextBox { Location = new Point(400, 14), Width = 250, Height = 28 };
            txtTimKiem.TextChanged += (s, e) => LoadData();

            var lblLoc = new Label { Text = "Lọc loại:", ForeColor = Color.White, Location = new Point(680, 17), AutoSize = true };
            cboLocLoai = new ComboBox { Location = new Point(750, 14), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLocLoai.SelectedIndexChanged += (s, e) => LoadData();

            panelTop.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtTimKiem, lblLoc, cboLocLoai });

            // ── PANEL LEFT (DataGridView) ─────────────────────────────────────
            panelLeft = new Panel { Location = new Point(0, 55), Width = 730, Height = this.ClientSize.Height - 55, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom };

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
                RowTemplate = { Height = 32 }
            };
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvSanPham.EnableHeadersVisualStyles = false;
            dgvSanPham.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
            dgvSanPham.SelectionChanged += DgvSanPham_SelectionChanged;

            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", Visible = false });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã vạch", FillWeight = 15 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên SP", FillWeight = 30 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 18 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGia", HeaderText = "Giá bán", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTon", HeaderText = "Tồn", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTrangThai", HeaderText = "Trạng thái", FillWeight = 14, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            panelLeft.Controls.Add(dgvSanPham);

            // ── PANEL RIGHT (Form nhập liệu) ──────────────────────────────────
            panelRight = new Panel
            {
                Location = new Point(735, 55),
                Width = this.ClientSize.Width - 735,
                Height = this.ClientSize.Height - 55,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            int y = 20, labelW = 80, inputX = 100, inputW = 210;

            panelRight.Controls.Add(new Label { Text = "THÔNG TIN SẢN PHẨM", Location = new Point(15, y), AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 60) });

            y += 40;
            picAnh = new PictureBox { Location = new Point(inputX, y), Size = new Size(160, 160), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(245, 247, 250) };
            panelRight.Controls.Add(picAnh);

            y += 170;
            btnChonAnh = CreateButton("📷 Chọn ảnh", inputX, y, 160, Color.FromArgb(100, 120, 200));
            btnChonAnh.Click += BtnChonAnh_Click;
            panelRight.Controls.Add(btnChonAnh);

            y += 50;
            panelRight.Controls.Add(MakeLabel("Mã vạch:", 15, y, labelW));
            txtMaVach = MakeTextBox(inputX, y, inputW + 95); panelRight.Controls.Add(txtMaVach);

            y += 40;
            panelRight.Controls.Add(MakeLabel("Tên SP: *", 15, y, labelW));
            txtTenSP = MakeTextBox(inputX, y, inputW + 95); panelRight.Controls.Add(txtTenSP);

            y += 40;
            panelRight.Controls.Add(MakeLabel("Giá bán: *", 15, y, labelW));
            txtGia = MakeTextBox(inputX, y, inputW + 95); panelRight.Controls.Add(txtGia);

            y += 40;
            panelRight.Controls.Add(MakeLabel("Loại SP: *", 15, y, labelW));
            cboLoai = new ComboBox { Location = new Point(inputX, y), Width = inputW, DropDownStyle = ComboBoxStyle.DropDownList };
            panelRight.Controls.Add(cboLoai);
            btnThemLoai = CreateButton("➕ Loại mới", inputX + inputW + 5, y, 90, Color.FromArgb(80, 160, 100));
            btnThemLoai.Height = 26; btnThemLoai.Click += BtnThemLoai_Click;
            panelRight.Controls.Add(btnThemLoai);

            // ── CÁC NÚT HÀNH ĐỘNG ─────────────────────────────────────────────
            y += 65;
            btnThem = CreateButton("➕ THÊM", 15, y, 100, Color.FromArgb(34, 139, 34));
            btnThem.Click += BtnThem_Click; panelRight.Controls.Add(btnThem);

            btnSua = CreateButton("✏ SỬA", 125, y, 100, Color.FromArgb(30, 100, 200));
            btnSua.Click += BtnSua_Click; panelRight.Controls.Add(btnSua);

            btnTrangThai = CreateButton("⏸ NGỪNG BÁN", 235, y, 160, Color.FromArgb(200, 50, 50));
            btnTrangThai.Click += BtnTrangThai_Click; panelRight.Controls.Add(btnTrangThai);

            y += 45;
            btnLamMoi = CreateButton("🔄 Làm mới Form", 15, y, 380, Color.FromArgb(130, 130, 130));
            btnLamMoi.Click += (s, e) => ClearForm(); panelRight.Controls.Add(btnLamMoi);

            this.Controls.AddRange(new Control[] { panelTop, panelLeft, panelRight });
            this.Resize += (s, e) =>
            {
                panelLeft.Height = this.ClientSize.Height - 55;
                panelRight.Left = 735;
                panelRight.Width = this.ClientSize.Width - 735;
                panelRight.Height = this.ClientSize.Height - 55;
            };
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmQuanLySanPham
            // 
            this.ClientSize = new System.Drawing.Size(1091, 511);
            this.Name = "frmQuanLySanPham";
            this.ResumeLayout(false);

        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD DỮ LIỆU
        // ══════════════════════════════════════════════════════════════════════
        private void LoadLoai()
        {
            var loaiList = _bll.GetAllLoai();

            // Cập nhật an toàn chống crash theo logic cũ
            cboLoai.Items.Clear();
            cboLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Chọn loại --" });
            foreach (var l in loaiList) cboLoai.Items.Add(l);
            cboLoai.DisplayMember = "TenLoai"; cboLoai.ValueMember = "LoaiSanPhamID";
            cboLoai.SelectedIndex = 0;

            cboLocLoai.Items.Clear();
            cboLocLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Tất cả --" });
            foreach (var l in loaiList) cboLocLoai.Items.Add(l);
            cboLocLoai.DisplayMember = "TenLoai"; cboLocLoai.ValueMember = "LoaiSanPhamID";
            cboLocLoai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            dgvSanPham.SelectionChanged -= DgvSanPham_SelectionChanged;
            string kw = txtTimKiem?.Text ?? "";
            int loaiID = 0;
            if (cboLocLoai?.SelectedItem is LoaiSanPham_DTO selected) loaiID = selected.LoaiSanPhamID;

            // LƯU KẾT QUẢ TỪ DATABASE VÀO CACHE TRONG RAM (Chỉ gọi DB 1 lần)
            _currentList = !string.IsNullOrWhiteSpace(kw) ? _bll.Search(kw) : _bll.GetByLoai(loaiID);

            dgvSanPham.Rows.Clear();
            foreach (var sp in _currentList)
            {
                string strTrangThai = sp.TrangThai ? "✅ Đang bán" : "❌ Ngừng bán";
                int idx = dgvSanPham.Rows.Add(sp.SanPhamID, sp.MaVach, sp.TenSP, sp.TenLoai, string.Format("{0:N0} đ", sp.GiaBanHienTai), sp.TonKhoTong, strTrangThai);

                if (!sp.TrangThai)
                {
                    dgvSanPham.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvSanPham.Rows[idx].DefaultCellStyle.Font = new Font(dgvSanPham.Font, FontStyle.Strikeout);
                }
                else if (sp.TonKhoTong <= 5) dgvSanPham.Rows[idx].DefaultCellStyle.ForeColor = Color.Red;
            }
            ClearForm();
            dgvSanPham.SelectionChanged += DgvSanPham_SelectionChanged;
        }

        // ══════════════════════════════════════════════════════════════════════
        // TÌNH NĂNG MỚI: TỰ ĐỘNG CHUYỂN ĐẾN SẢN PHẨM TRÙNG MÃ VẠCH
        // ══════════════════════════════════════════════════════════════════════
        private void FocusRowByMaVach(string maVach)
        {
            // 1. Reset bộ lọc để đảm bảo sản phẩm đó chắc chắn hiển thị trên lưới
            txtTimKiem.Text = "";
            if (cboLocLoai.Items.Count > 0) cboLocLoai.SelectedIndex = 0;

            // 2. Quét lưới để bôi đen đúng dòng đang bị trùng
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                if (row.Cells["colMa"].Value?.ToString() == maVach)
                {
                    row.Selected = true;
                    // Lệnh này bắt màn hình cuộn xuống (scroll) thẳng vị trí sản phẩm đó
                    dgvSanPham.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        // SỰ KIỆN NÚT BẤM VÀ GRID
        // ══════════════════════════════════════════════════════════════════════
        private void DgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0) return;
            var row = dgvSanPham.SelectedRows[0];
            _selectedID = Convert.ToInt32(row.Cells["colID"].Value);

            // CÁCH MỚI: Tìm thẳng trong biến Cache (tốc độ ánh sáng, KHÔNG GỌI DATABASE)
            var sp = _currentList.Find(x => x.SanPhamID == _selectedID);
            if (sp == null) return;

            // Gán dữ liệu cực sạch (không cần Replace chuỗi phèn nữa)
            txtMaVach.Text = sp.MaVach;
            txtTenSP.Text = sp.TenSP;
            txtGia.Text = sp.GiaBanHienTai.ToString();

            // KHÓA MÃ VẠCH LẠI KHI ĐANG SỬA
            txtMaVach.ReadOnly = true;
            txtMaVach.BackColor = Color.FromArgb(245, 245, 245);

            foreach (LoaiSanPham_DTO item in cboLoai.Items)
            {
                if (item.LoaiSanPhamID == sp.LoaiSanPhamID) { cboLoai.SelectedItem = item; break; }
            }

            if (sp.TrangThai)
            {
                btnTrangThai.Text = "⏸ NGỪNG BÁN";
                btnTrangThai.BackColor = Color.FromArgb(200, 50, 50);
            }
            else
            {
                btnTrangThai.Text = "▶ BÁN LẠI";
                btnTrangThai.BackColor = Color.FromArgb(34, 139, 34);
            }

            if (!string.IsNullOrEmpty(sp.HinhAnh) && File.Exists(sp.HinhAnh))
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

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var sp = BuildDTO();
            if (sp == null) return;

            var (ok, msg) = _bll.ThemSanPham(sp);

            if (!ok)
            {
                MessageBox.Show(msg, "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // NẾU LỖI LÀ TRÙNG MÃ VẠCH -> NHẢY THẲNG ĐẾN SP BỊ TRÙNG
                if (msg.Contains("tồn tại")) FocusRowByMaVach(sp.MaVach);
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!"); return; }
            var sp = BuildDTO();
            if (sp == null) return;
            sp.SanPhamID = _selectedID;

            var (ok, msg) = _bll.SuaSanPham(sp);

            if (!ok)
            {
                MessageBox.Show(msg, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // NẾU LỖI LÀ TRÙNG MÃ VẠCH -> NHẢY THẲNG ĐẾN SP BỊ TRÙNG
                if (msg.Contains("tồn tại")) FocusRowByMaVach(sp.MaVach);
            }
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void BtnTrangThai_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm cần thao tác!"); return; }

            string question = btnTrangThai.Text == "⏸ NGỪNG BÁN" ? "Xác nhận NGỪNG BÁN sản phẩm này?" : "Xác nhận MỞ BÁN LẠI sản phẩm này?";
            if (MessageBox.Show(question, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            var (ok, msg) = _bll.ThayDoiTrangThai(_selectedID);
            if (ok) { LoadData(); }
            else { MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void BtnThemLoai_Click(object sender, EventArgs e)
        {
            string tenLoai = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên loại sản phẩm mới:", "Thêm loại", "");
            if (string.IsNullOrWhiteSpace(tenLoai)) return;

            var (ok, msg) = _bll.ThemLoai(tenLoai);
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (ok) LoadLoai();
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog() { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp", Title = "Chọn ảnh sản phẩm" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = dlg.FileName;
                    picAnh.Image = Image.FromFile(_selectedImagePath);
                }
            }
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
            _selectedID = -1; _selectedImagePath = "";
            txtMaVach.Text = ""; txtTenSP.Text = ""; txtGia.Text = "";

            if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;

            picAnh.Image = null;
            dgvSanPham.ClearSelection();
            btnTrangThai.Text = "⏸ NGỪNG BÁN"; btnTrangThai.BackColor = Color.FromArgb(200, 50, 50);

            // MỞ KHÓA MÃ VẠCH CHO PHÉP NHẬP ĐỂ THÊM MỚI
            txtMaVach.ReadOnly = false;
            txtMaVach.BackColor = Color.White;
        }

        private Label MakeLabel(string text, int x, int y, int w) => new Label { Text = text, Location = new Point(x, y + 3), Width = w, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50) };
        private TextBox MakeTextBox(int x, int y, int w) => new TextBox { Location = new Point(x, y), Width = w };
        private Button CreateButton(string text, int x, int y, int w, Color backColor) => new Button { Text = text, Location = new Point(x, y), Width = w, Height = 32, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };
    }
}