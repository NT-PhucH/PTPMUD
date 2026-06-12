using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_;
using QLST.GUI__Giao_dien_.Home;
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
        private User _ucUser;

        // 1. Tạo biến để hứng dữ liệu người dùng đang đăng nhập
        private NhanVienDTO _nhanVienHienTai;

        // Hàm mặc định (Giữ lại để bản vẽ Designer của Visual Studio không bị lỗi)
        public FormMain()
        {
            InitializeComponent();
        }

        // 2. Thêm một hàm khởi tạo MỚI chuyên dùng để nhận dữ liệu từ FormLogin
        public FormMain(NhanVienDTO user)
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
            foreach (Control btn in panelMenu.Controls)
            {
                if (btn is Button)
                {
                    btn.BackColor = Color.FromArgb(51, 51, 76);
                    btn.ForeColor = Color.Gainsboro;
                }
            }
        }

        // 3. Sự kiện Click chung cho Menu
        private void MenuButton_Click(object sender, EventArgs e)
        {
            // Ép kiểu an toàn bằng từ khóa 'as'
            Button clickedButton = sender as Button;
            if (clickedButton == null) return; // Nếu không phải Button thì thoát để tránh lỗi

            // Bước 1: Đổi màu nút
            ResetButtonColor();
            clickedButton.BackColor = Color.FromArgb(33, 157, 212);
            clickedButton.ForeColor = Color.Gainsboro;

            // Bước 2: Hiển thị UserControl tương ứng
            panelContent.Controls.Clear(); // Xóa control hiện tại trên panel

            if (clickedButton == btnHome)
            {
                panelContent.Controls.Add(_ucHome);
            }
            else if (clickedButton == btnUser)
            {
                panelContent.Controls.Add(_ucUser);
            }
            /*else if (clickedButton == btnKho)
            {
                ucKho giaoDienKho = new ucKho { Dock = DockStyle.Fill };
                panelContent.Controls.Clear();
                panelContent.Controls.Add(giaoDienKho);
            }*/
            else if (clickedButton == btnSettings)
            {
                // Tạo form Thu Ngân
                FormThuNgan frmThuNgan = new FormThuNgan();

                // TUYỆT CHIÊU: Lột bỏ vỏ bọc cửa sổ để biến Form thành UserControl
                frmThuNgan.TopLevel = false;
                frmThuNgan.FormBorderStyle = FormBorderStyle.None; // Bỏ viền và nút X đỏ
                frmThuNgan.Dock = DockStyle.Fill;                  // Phóng to lắp đầy panel

                // Nhét vào panel và ép nó hiển thị ra
                panelContent.Controls.Add(frmThuNgan);
                frmThuNgan.Show();
            }
            /*else if (clickedButton == btnKho) // đổi tên button cho đúng
            {
                frmQuanLySanPham frmSP = new frmQuanLySanPham();
                frmSP.TopLevel = false;
                frmSP.FormBorderStyle = FormBorderStyle.None;
                frmSP.Dock = DockStyle.Fill;
                panelContent.Controls.Add(frmSP);
                frmSP.Show();
            }*/
            else if (clickedButton == btnKho) // đổi tên button cho đúng
            {
                
                frmQuanLyKho frmKHo = new frmQuanLyKho();
                frmKHo.TopLevel = false;
                frmKHo.FormBorderStyle = FormBorderStyle.None;
                frmKHo.Dock = DockStyle.Fill;
                panelContent.Controls.Add(frmKHo);
                frmKHo.Show();
            }
            else if (clickedButton == btn1)
            {
                frmQuanLyNCC frmNCC = new frmQuanLyNCC();
                frmNCC.TopLevel = false;
                frmNCC.FormBorderStyle = FormBorderStyle.None;
                frmNCC.Dock = DockStyle.Fill;
                panelContent.Controls.Add(frmNCC);
                frmNCC.Show();

            }
            else if (clickedButton == btn2)
            {
                frmThongKe frmNV = new frmThongKe();
                frmNV.TopLevel = false;
                frmNV.FormBorderStyle = FormBorderStyle.None;
                frmNV.Dock = DockStyle.Fill;
                panelContent.Controls.Add(frmNV);
                frmNV.Show();
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

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        // Xóa bỏ các hàm Paint trống nếu không dùng để code gọn gàng hơn
    }
}