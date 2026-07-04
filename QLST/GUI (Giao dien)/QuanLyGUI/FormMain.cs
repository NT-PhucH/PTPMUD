using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_;
using QLST.GUI__Giao_dien_.QuanLyGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormMain : Form
    {
        // Tạo các biến vùng chứa để lưu trữ giao diện (Cache)
        private ucHome _ucHome;
        private ucQuanLyNhanVien _ucUser;

        // Thêm mới các biến cho các tab khác
        private ucTichDiem _ucTichDiem;
        private ucThongKe _ucThongKe;
        private ucSettings _ucSettings;
        private ucLichSuDon _ucLichSuDon;
        private ucQLSP _ucQLSP;
        private FormThuNgan _frmThuNgan;
        private ucNhapHang _ucNhapHang;
        private ucXuatKho _ucXuatKho;
        private ucLichSu _ucLichSu;
        private ucCanhBao _ucCanhBao;

        // 1. Tạo biến để hứng dữ liệu người dùng đang đăng nhập
        private QLNV_DTO _nhanVienHienTai;

        // Hàm mặc định (Giữ lại để bản vẽ Designer của Visual Studio không bị lỗi)
        public FormMain()
        {
            InitializeComponent();
        }

        // 2. Thêm một hàm khởi tạo MỚI chuyên dùng để nhận dữ liệu từ FormLogin
        public FormMain(QLNV_DTO user)
        {
            InitializeComponent();
            _nhanVienHienTai = user; // Cất dữ liệu người dùng vào biến để lát nữa dùng (VD: hiển thị tên)
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Khởi tạo sẵn các UserControl ngay khi load Form
            _ucHome = new ucHome { Dock = DockStyle.Fill };
            _ucUser = new ucQuanLyNhanVien { Dock = DockStyle.Fill };

            // Tự động kích hoạt tab Home đầu tiên
            MenuButton_Click(btnHome, e);

            // (Tùy chọn) Bạn có thể dùng biến _nhanVienHienTai để hiển thị tên lên giao diện ở đây
            // Ví dụ: lblTenNhanVien.Text = "Xin chào: " + _nhanVienHienTai.TenNV;
        }

        // 2. Các hàm bổ trợ (Helper Methods)
        private void ResetButtonColor()
        {
            // Reset cho panel menu chính
            foreach (Control btn in panelMenu.Controls)
            {
                if (btn is Button)
                {
                    btn.BackColor = Color.FromArgb(51, 51, 76);
                    btn.ForeColor = Color.Gainsboro;
                }
            }
            // Reset cho panel Kho SubMenu
            foreach (Control btn in panelKhoSubMenu.Controls)
            {
                if (btn is Button)
                {
                    btn.BackColor = Color.FromArgb(51, 51, 76);
                    btn.ForeColor = Color.White; // Trả về màu trắng nguyên bản của Submenu
                }
            }
        }

        // 3. Sự kiện Click chung cho Menu
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            // --- 1. XỬ LÝ RIÊNG CHO NÚT KHO ---
            if (clickedButton == btnKho)
            {
                // Đảo trạng thái đóng/mở của panel submenu
                panelKhoSubMenu.Visible = !panelKhoSubMenu.Visible;
                btnKho.Invalidate();

                ResetButtonColor();
                btnKho.BackColor = Color.FromArgb(33, 157, 212);
                btnKho.ForeColor = Color.Gainsboro;

                if (panelKhoSubMenu.Visible)
                {
                    // Kiểm tra xem UC hiện tại trên màn hình có thuộc nhóm Kho không
                    bool isCurrentlyInKho = panelContent.Controls.Count > 0 &&
                        (panelContent.Controls[0] is ucNhapHang ||
                         panelContent.Controls[0] is ucXuatKho ||
                         panelContent.Controls[0] is ucLichSu ||
                         panelContent.Controls[0] is ucCanhBao);

                    if (!isCurrentlyInKho)
                    {
                        // Nếu đang ở tab khác (User, Home,...) chuyển sang Kho -> Mặc định load Tab Nhập Hàng
                        MenuButton_Click(btnNhapHang, e);
                    }
                    else
                    {
                        // Nếu đang ở sẵn trong Kho, chỉ mở panel ra và tô lại màu cho tab đang đứng
                        var currentControl = panelContent.Controls[0];
                        if (currentControl is ucNhapHang) { btnNhapHang.BackColor = Color.FromArgb(43, 43, 66); btnNhapHang.ForeColor = Color.FromArgb(33, 157, 212); }
                        else if (currentControl is ucXuatKho) { btnXuatKho.BackColor = Color.FromArgb(43, 43, 66); btnXuatKho.ForeColor = Color.FromArgb(33, 157, 212); }
                        else if (currentControl is ucLichSu) { btnLichSu.BackColor = Color.FromArgb(43, 43, 66); btnLichSu.ForeColor = Color.FromArgb(33, 157, 212); }
                        else if (currentControl is ucCanhBao) { btnCanhBao.BackColor = Color.FromArgb(43, 43, 66); btnCanhBao.ForeColor = Color.FromArgb(33, 157, 212); }
                    }
                }
                // Lệnh return này rất quan trọng để dừng hàm, ngăn chặn lệnh panelContent.Controls.Clear() chạy
                // Giúp giữ nguyên UserControl hiện tại khi chỉ đóng/mở nút Kho
                return;
            }

            // --- 2. XỬ LÝ CHO CÁC NÚT CÒN LẠI ---
            ResetButtonColor();

            if (panelKhoSubMenu.Controls.Contains(clickedButton))
            {
                btnKho.BackColor = Color.FromArgb(33, 157, 212);
                btnKho.ForeColor = Color.Gainsboro;

                clickedButton.BackColor = Color.FromArgb(43, 43, 66);
                clickedButton.ForeColor = Color.FromArgb(33, 157, 212);
            }
            else
            {
                clickedButton.BackColor = Color.FromArgb(33, 157, 212);
                clickedButton.ForeColor = Color.Gainsboro;

                // Tự động thu gọn menu Kho nếu bấm sang một Menu chính khác
                panelKhoSubMenu.Visible = false;
            }

            // Hiển thị UserControl / Form tương ứng
            panelContent.Controls.Clear();

            if (clickedButton == btnHome)
            {
                panelContent.Controls.Add(_ucHome); // Vẫn dùng bình thường [2]
            }
            else if (clickedButton == btnUser)
            {
                // Dùng biến cache _ucUser thay vì gọi new
                if (_ucUser == null) _ucUser = new ucQuanLyNhanVien { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucUser);
                _ucUser.Show();
            }
            else if (clickedButton == btnTichDiem)
            {
                if (_ucTichDiem == null) _ucTichDiem = new ucTichDiem { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucTichDiem);
                _ucTichDiem.Show();
            }
            else if (clickedButton == btnThongKe)
            {
                if (_ucThongKe == null) _ucThongKe = new ucThongKe { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucThongKe);
            }
            else if (clickedButton == btnSettings)
            {
                if (_ucSettings == null) _ucSettings = new ucSettings { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucSettings);
                _ucSettings.Show();
            }
            else if (clickedButton == btnLSHD)
            {
                if (_ucLichSuDon == null) _ucLichSuDon = new ucLichSuDon { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucLichSuDon);
                _ucLichSuDon.Show();
            }
            else if (clickedButton == btnQLSP)
            {
                if (_ucQLSP == null) _ucQLSP = new ucQLSP { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucQLSP);
            }
            else if (clickedButton == btnQLNCC)
            {
                /* if (_frmNCC == null) _frmNCC = new frmQuanLyNCC { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                 panelContent.Controls.Add(_frmNCC);
                 _frmNCC.Show();*/
                ucQuanLyNCC quanLyNCC = new ucQuanLyNCC { Dock = DockStyle.Fill };
                panelContent.Controls.Add(quanLyNCC);
                quanLyNCC.Show();
            }
            else if (clickedButton == btnShopping)
            {
                if (_frmThuNgan == null) _frmThuNgan = new FormThuNgan { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(_frmThuNgan);
                _frmThuNgan.Show();
            }
            // Các nút con của Kho
            else if (clickedButton == btnNhapHang)
            {
                if (_ucNhapHang == null) _ucNhapHang = new ucNhapHang { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucNhapHang);
            }
            else if (clickedButton == btnXuatKho)
            {
                if (_ucXuatKho == null) _ucXuatKho = new ucXuatKho { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucXuatKho);
            }
            else if (clickedButton == btnLichSu)
            {
                if (_ucLichSu == null) _ucLichSu = new ucLichSu { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucLichSu);
            }
            else if (clickedButton == btnCanhBao)
            {
                if (_ucCanhBao == null) _ucCanhBao = new ucCanhBao { Dock = DockStyle.Fill };
                panelContent.Controls.Add(_ucCanhBao);
            }
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // 1. Chạy một luồng ứng dụng mới độc lập bắt đầu từ Form Đăng nhập
                // Thay "FormDangNhap" bằng đúng tên Class Form đăng nhập của dự án của bạn
                System.Threading.Thread t = new System.Threading.Thread(() => Application.Run(new FormLogin()));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();

                // 2. Đóng và hủy hoàn toàn Form hiện tại cùng tất cả tài nguyên đi kèm
                this.Close();
            }
        }

        private void btnKho_Paint(object sender, PaintEventArgs e)
        {
            // Lấy ảnh mũi tên tương ứng với trạng thái đóng/mở của subpanel
            Image arrow = panelKhoSubMenu.Visible ? Properties.Resources.arrow_up : Properties.Resources.arrow_down;

            if (arrow != null)
            {
                // Tính toán vị trí: cách lề phải 15 pixel, căn giữa theo chiều dọc
                int x = btnKho.Width - arrow.Width - 4;
                int y = (btnKho.Height - arrow.Height) / 2;

                // Vẽ mũi tên lên mặt nút
                e.Graphics.DrawImage(arrow, x, y, arrow.Width, arrow.Height);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Xóa bỏ các hàm Paint trống nếu không dùng để code gọn gàng hơn
    }
}