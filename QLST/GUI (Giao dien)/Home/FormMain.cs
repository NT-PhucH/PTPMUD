using QLST.GUI__Giao_dien_.Home;
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

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Khởi tạo sẵn các UserControl ngay khi load Form
            _ucHome = new ucHome { Dock = DockStyle.Fill };
            _ucUser = new User { Dock = DockStyle.Fill };

            // Tự động kích hoạt tab Home đầu tiên
            MenuButton_Click(btnHome, e);
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

        // Xóa bỏ các hàm Paint trống nếu không dùng để code gọn gàng hơn
    }
}