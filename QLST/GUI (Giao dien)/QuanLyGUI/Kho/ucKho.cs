using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_
{
    // Bắt buộc kế thừa từ UserControl
    public class ucKho : UserControl
    {
        // 1. Khai báo các thành phần giao diện
        private Panel pnlTop;
        private Label lblTitle;
        private TextBox txtTimKiem;
        private Label lblTimKiem;

        private Panel pnlBottom;
        private Button btnNhapKho;
        private Button btnKiemKe;

        private DataGridView dgvKho;

        // Dữ liệu kho
        private List<TrungBaySP_DTO> dsKho = new List<TrungBaySP_DTO>();

        public ucKho()
        {
            // Gọi hàm tự động vẽ giao diện
            VeGiaoDienBangCode();

            // Gán sự kiện khi load xong
            this.Load += UcKho_Load;
        }

        // --- HÀM TỰ ĐỘNG SINH GIAO DIỆN KHÔNG CẦN KÉO THẢ ---
        private void VeGiaoDienBangCode()
        {
            this.Size = new Size(900, 600);
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // --- PANEL TOP (Chứa Tiêu đề và Tìm kiếm) ---
            pnlTop = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };

            lblTitle = new Label
            {
                Text = "📦 QUẢN LÝ TỒN KHO",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 76),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            lblTimKiem = new Label { Text = "Tìm kiếm:", AutoSize = true, Location = new Point(450, 25) };
            txtTimKiem = new TextBox { Width = 300, Location = new Point(530, 22), Font = new Font("Segoe UI", 11F) };
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged; // Gán sự kiện tìm kiếm

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblTimKiem);
            pnlTop.Controls.Add(txtTimKiem);

            // --- PANEL BOTTOM (Chứa Nút bấm thao tác) ---
            pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White };

            btnNhapKho = new Button
            {
                Text = "➕ Nhập Kho",
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 40),
                Location = new Point(20, 10),
                Cursor = Cursors.Hand
            };
            btnNhapKho.FlatAppearance.BorderSize = 0;

            btnKiemKe = new Button
            {
                Text = "📋 Kiểm Kê",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 40),
                Location = new Point(150, 10),
                Cursor = Cursors.Hand
            };
            btnKiemKe.FlatAppearance.BorderSize = 0;

            // Gán thử sự kiện click cho nút
            btnNhapKho.Click += (s, e) => MessageBox.Show("Chức năng Nhập kho đang phát triển!", "Thông báo");

            pnlBottom.Controls.Add(btnNhapKho);
            pnlBottom.Controls.Add(btnKiemKe);

            // --- DATAGRIDVIEW (Lưới dữ liệu ở giữa) ---
            dgvKho = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Trang trí lưới cho đẹp
            dgvKho.EnableHeadersVisualStyles = false;
            dgvKho.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvKho.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKho.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvKho.ColumnHeadersHeight = 40;
            dgvKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            // --- LẮP RÁP VÀO USERCONTROL ---
            this.Controls.Add(dgvKho);    // Phải Add cái Fill trước
            this.Controls.Add(pnlBottom); // Add cái Bottom
            this.Controls.Add(pnlTop);    // Add cái Top
        }

        // --- LOGIC XỬ LÝ DỮ LIỆU ---
        private void UcKho_Load(object sender, EventArgs e)
        {
            try
            {
                // Gọi tới hàm lấy dữ liệu bạn đã viết sẵn ở DAL
                TrungBaySP_DAL spDAL = new TrungBaySP_DAL();
                dsKho = spDAL.GetAllSanPham();

                // Đổ vào DataGridView
                dgvKho.DataSource = dsKho;

                // Tùy chỉnh cột hiển thị
                if (dgvKho.Columns["SanPhamID"] != null) dgvKho.Columns["SanPhamID"].Visible = false;
                if (dgvKho.Columns["TenLoai"] != null) dgvKho.Columns["TenLoai"].Visible = false;
                if (dgvKho.Columns["HinhAnh"] != null) dgvKho.Columns["HinhAnh"].Visible = false;

                if (dgvKho.Columns["MaSanPham"] != null) dgvKho.Columns["MaSanPham"].HeaderText = "Mã Vạch";
                if (dgvKho.Columns["TenSanPham"] != null) dgvKho.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                if (dgvKho.Columns["DonGia"] != null)
                {
                    dgvKho.Columns["DonGia"].HeaderText = "Đơn Giá";
                    dgvKho.Columns["DonGia"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ
                }
                if (dgvKho.Columns["soLuongTonKho"] != null) dgvKho.Columns["soLuongTonKho"].HeaderText = "Tồn Kho";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu kho: " + ex.Message);
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lọc dữ liệu Real-time
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dgvKho.DataSource = dsKho;
            }
            else
            {
                var dsLoc = dsKho.Where(sp =>
                    (sp.TenSanPham != null && sp.TenSanPham.ToLower().Contains(tuKhoa)) ||
                    (sp.MaSanPham != null && sp.MaSanPham.ToLower().Contains(tuKhoa))
                ).ToList();

                dgvKho.DataSource = dsLoc;
            }
        }
    }
}