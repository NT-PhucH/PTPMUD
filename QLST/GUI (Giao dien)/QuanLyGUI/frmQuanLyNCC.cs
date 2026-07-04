using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmQuanLyNCC : Form
    {
        private readonly NhaCungCap_BLL _bll = new NhaCungCap_BLL();
        private int _selectedID = -1; // Biến lưu giữ ID ngầm

        private DataGridView dgvNCC;
        private TextBox txtTimKiem, txtMaNCC, txtTenNCC, txtSDT, txtDiaChi;
        private Button btnSua, btnLamMoi; // Đã bỏ btnThem và btnTrangThai
        private Label lblTongNCC, lblTongTien;

        public frmQuanLyNCC()
        {
            BuildUI();
            LoadData();
        }

        private void BuildUI()
        {
            Text = "Quản lý nhà cung cấp";
            Size = new Size(1150, 660);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            // 1. HEADER
            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label
            {
                Text = "🏭  QUẢN LÝ NHÀ CUNG CẤP",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Location = new Point(15, 12),
                AutoSize = true
            });

            // 2. THANH TÌM KIẾM
            var pSearch = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.White, Padding = new Padding(10, 8, 10, 0) };
            pSearch.Controls.Add(new Label { Text = "🔍 Tìm kiếm:", Location = new Point(10, 12), AutoSize = true });
            txtTimKiem = new TextBox { Location = new Point(100, 9), Width = 300 };
            txtTimKiem.TextChanged += (s, e) => LoadData();
            pSearch.Controls.Add(txtTimKiem);

            lblTongNCC = new Label { Location = new Point(430, 12), AutoSize = true, ForeColor = Color.FromArgb(30, 100, 200), Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            lblTongTien = new Label { Location = new Point(580, 12), AutoSize = true, ForeColor = Color.FromArgb(34, 139, 34), Font = new Font("Segoe UI", 9.5f, FontStyle.Bold) };
            pSearch.Controls.Add(lblTongNCC);
            pSearch.Controls.Add(lblTongTien);

            // 3. PANEL TRÁI (CHỨA DATAGRIDVIEW)
            var pLeft = new Panel { Location = new Point(0, 95), Width = 730, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom, Height = 565 };

            dgvNCC = new DataGridView
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
                RowTemplate = { Height = 35 } // Tăng chiều cao dòng một chút để nút bấm nhìn đẹp hơn
            };
            dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvNCC.EnableHeadersVisualStyles = false;

            // Đăng ký sự kiện
            dgvNCC.SelectionChanged += DgvNCC_SelectionChanged;
            dgvNCC.CellClick += DgvNCC_CellClick; // Sự kiện bấm nút Trạng thái

            // ── ĐỊNH NGHĨA CỘT DATAGRIDVIEW ──
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", Visible = false });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMa", HeaderText = "Mã NCC", FillWeight = 12 });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên NCC", FillWeight = 25 });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSDT", HeaderText = "SĐT", FillWeight = 13 });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDC", HeaderText = "Địa chỉ", FillWeight = 20 });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSoPN", HeaderText = "Số PN", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNCC.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTong", HeaderText = "Tổng tiền", FillWeight = 15, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });

            // CỘT NÚT ĐỘNG: TRẠNG THÁI
            DataGridViewButtonColumn btnToggle = new DataGridViewButtonColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                FillWeight = 18,
                FlatStyle = FlatStyle.Flat
            };
            dgvNCC.Columns.Add(btnToggle);

            pLeft.Controls.Add(dgvNCC);

            // 4. PANEL PHẢI (CHỨA FORM THÔNG TIN)
            var pRight = new Panel
            {
                Location = new Point(735, 95),
                Width = 400,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                Padding = new Padding(15),
                Height = 565
            };

            int y = 15;
            pRight.Controls.Add(MakeBold("THÔNG TIN NHÀ CUNG CẤP", 15, y));

            y += 35;
            pRight.Controls.Add(MakeLabel("Mã NCC:", 15, y));
            txtMaNCC = MakeTextBox(15, y + 22, 340);
            txtMaNCC.ReadOnly = true;
            txtMaNCC.BackColor = Color.FromArgb(245, 245, 245);
            pRight.Controls.Add(new Label { Text = "* Cập nhật không thay đổi được mã", Location = new Point(15, y + 45), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic) });
            pRight.Controls.Add(txtMaNCC);

            y += 60;
            pRight.Controls.Add(MakeLabel("Tên NCC: *", 15, y));
            txtTenNCC = MakeTextBox(15, y + 22, 340); pRight.Controls.Add(txtTenNCC);

            y += 60;
            pRight.Controls.Add(MakeLabel("Số điện thoại:", 15, y));
            txtSDT = MakeTextBox(15, y + 22, 340); pRight.Controls.Add(txtSDT);

            y += 60;
            pRight.Controls.Add(MakeLabel("Địa chỉ:", 15, y));
            txtDiaChi = new TextBox { Location = new Point(15, y + 22), Width = 340, Height = 70, Multiline = true, ScrollBars = ScrollBars.Vertical };
            pRight.Controls.Add(txtDiaChi);

            y += 110;

            // --- THIẾT KẾ LẠI NÚT SỬA VÀ NÚT LÀM MỚI ---
            // Nút sửa làm bự hơn, màu cam nổi bật
            btnSua = MakeBtn("✏ CẬP NHẬT (SỬA)", 15, y, 200, Color.FromArgb(255, 152, 0));

            // Nút làm mới xếp cạnh nút sửa
            btnLamMoi = MakeBtn("🔄 Làm mới", 225, y, 130, Color.FromArgb(130, 130, 130));

            btnSua.Click += BtnSua_Click;
            btnLamMoi.Click += (s, e) => ClearForm();
            pRight.Controls.AddRange(new Control[] { btnSua, btnLamMoi });

            // ADD VÀO FORM
            Controls.AddRange(new Control[] { pLeft, pRight, pSearch, header });
            Resize += (s, e) => {
                pLeft.Height = ClientSize.Height - 95;
                pRight.Height = ClientSize.Height - 95;
                pRight.Left = ClientSize.Width - 415;
                pLeft.Width = ClientSize.Width - 430;
            };
        }

        private void LoadData()
        {
            dgvNCC.SelectionChanged -= DgvNCC_SelectionChanged;
            dgvNCC.Rows.Clear();

            var list = string.IsNullOrWhiteSpace(txtTimKiem.Text)
                ? _bll.GetAll() : _bll.Search(txtTimKiem.Text);

            long tongTien = 0;
            foreach (var n in list)
            {
                tongTien += n.TongTienNhap;

                int idx = dgvNCC.Rows.Add(
                    n.NhaCungCapID, n.MaNCC, n.TenNCC, n.SoDienThoai,
                    n.DiaChi, n.TongPhieuNhap, string.Format("{0:N0} đ", n.TongTienNhap)
                );

                // --- ĐỔ TRẠNG THÁI VÀ MÀU SẮC LÊN NÚT BẤM TRONG GRID ---
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
                    dgvNCC.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(250, 220, 220); // Dòng bị khóa thì tô đỏ nhạt
                }
            }

            lblTongNCC.Text = $"Tổng NCC: {list.Count}";
            lblTongTien.Text = $"Tổng tiền nhập: {tongTien:N0} đ";

            dgvNCC.ClearSelection();
            ClearForm();
            dgvNCC.SelectionChanged += DgvNCC_SelectionChanged;
        }

        // --- SỰ KIỆN KHI BẤM VÀO BẤT KỲ Ô NÀO TRÊN GRID ---
        private void DgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // NẾU CLICK ĐÚNG VÀO CỘT NÚT BẤM "TRẠNG THÁI"
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
                        LoadData(); // Cập nhật lại màu sắc và trạng thái trên lưới
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

        // --- CÁC HÀM TIỆN ÍCH TẠO CONTROL ---
        private Label MakeLabel(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y), AutoSize = true };
        private Label MakeBold(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 60) };
        private TextBox MakeTextBox(int x, int y, int w) => new TextBox { Location = new Point(x, y), Width = w };
        private Button MakeBtn(string t, int x, int y, int w, Color c) =>
            new Button { Text = t, Location = new Point(x, y), Width = w, Height = 40, BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Cursor = Cursors.Hand };

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmQuanLyNCC";
            this.Load += new System.EventHandler(this.frmQuanLyNCC_Load);
            this.ResumeLayout(false);
        }

        private void frmQuanLyNCC_Load(object sender, EventArgs e) { }
    }
}