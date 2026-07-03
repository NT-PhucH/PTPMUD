using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_;
using QLST.GUI__Giao_dien_.Home;
using QLST.GUI__Giao_dien_.QuanLyGUI;
using QLST.GUI__Giao_dien_.QuanLyGUI.QL_User;
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
        private User _ucUser;

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
            _ucUser = new User { Dock = DockStyle.Fill };

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

            // Bước 1: Đổi màu nút
            ResetButtonColor();

            // Nếu click vào nút con của Kho, giữ lại màu cho cả nút cha (btnKho)
            if (panelKhoSubMenu.Controls.Contains(clickedButton))
            {
                btnKho.BackColor = Color.FromArgb(33, 157, 212);
                btnKho.ForeColor = Color.Gainsboro;

                // Nút con được chọn sẽ có màu highlight khác biệt một chút (hoặc giống tùy bạn)
                clickedButton.BackColor = Color.FromArgb(43, 43, 66);
                clickedButton.ForeColor = Color.FromArgb(33, 157, 212);
            }
            else
            {
                // Nếu click các nút khác thì highlight nút đó như bình thường
                clickedButton.BackColor = Color.FromArgb(33, 157, 212);
                clickedButton.ForeColor = Color.Gainsboro;
            }

            // Bước 2: Hiển thị UserControl / Form tương ứng
            // Bước 2: Hiển thị UserControl / Form tương ứng
            panelContent.Controls.Clear();

            if (clickedButton == btnHome)
            {
                panelContent.Controls.Add(_ucHome);
            }
            else if (clickedButton == btnUser)
            {
                frmQuanLyNhanVien frmNV = new frmQuanLyNhanVien { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmNV);
                frmNV.Show();
            }
            else if (clickedButton == btnTichDiem)
            {
                ucQuanLyKhachHang qlkh = new ucQuanLyKhachHang { Dock = DockStyle.Fill };
                panelContent.Controls.Add(qlkh);
                qlkh.Show();
            }
            else if (clickedButton == btnThongKe)
            {
                ucThongKe thongKe = new ucThongKe { Dock = DockStyle.Fill };
                panelContent.Controls.Add(thongKe);
                /*frmThongKe frmKH = new frmThongKe { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmKH);
                frmKH.Show();*/
            }
            else if (clickedButton == btnSettings)
            {
                ucSettings settings = new ucSettings { Dock = DockStyle.Fill };
                panelContent.Controls.Add(settings);
            }
            else if (clickedButton == btnLSHD)
            {
                /*frmQuanLyDonHang frmHD = new frmQuanLyDonHang { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmHD);
                frmHD.Show();*/
                ucLichSuDon lichSuDon = new ucLichSuDon { Dock = DockStyle.Fill };
                panelContent.Controls.Add(lichSuDon);
            }
            else if (clickedButton == btnQLSP)
            {
                /*frmQuanLySanPham frmSP = new frmQuanLySanPham { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmSP);
                frmSP.Show();*/
                ucQLSP settings = new ucQLSP { Dock = DockStyle.Fill };
                panelContent.Controls.Add(settings);
            }
            else if (clickedButton == btnQLNCC)
            {
                frmQuanLyNCC frmNCC = new frmQuanLyNCC { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmNCC);
                frmNCC.Show();
            }
            else if (clickedButton == btnKho)
            {
                frmQuanLyKho frmKHo = new frmQuanLyKho { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmKHo);
                frmKHo.Show();

                panelKhoSubMenu.Visible = !panelKhoSubMenu.Visible;
                btnKho.Invalidate();
                ResetButtonColor();
                btnKho.BackColor = Color.FromArgb(33, 157, 212);
                btnKho.ForeColor = Color.Gainsboro;
            }
            else if (clickedButton == btnShopping)
            {
                FormThuNgan frmThuNgan = new FormThuNgan { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
                panelContent.Controls.Add(frmThuNgan);
                frmThuNgan.Show();
            }
            // --- THÊM LOGIC CHO CÁC NÚT CON CỦA KHO Ở ĐÂY ---
            else if (clickedButton == btnNhapHang)
            {
                // Logic cho nút Nhập Hàng
            }
            else if (clickedButton == btnXuatKho)
            {
                // Logic cho nút Xuất Kho
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

        // Xóa bỏ các hàm Paint trống nếu không dùng để code gọn gàng hơn
    }
}