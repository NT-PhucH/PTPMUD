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
        public FormMain()
        {
            InitializeComponent();
        }

        // 2. Các hàm bổ trợ (Helper Methods) dùng chung cho các sự kiện
        private void ResetButtonColor()
        {
            // Duyệt qua tất cả các Control nằm bên trong panelMenu
            foreach (Control btn in panelMenu.Controls)
            {
                // Kiểm tra nếu Control đó đúng là một Nút bấm (Button)
                if (btn is Button)
                {
                    // Trả về màu nền tối mặc định của Menu 
                    btn.BackColor = Color.FromArgb(51, 51, 76);

                    // Trả về màu chữ mặc định 
                    btn.ForeColor = Color.Gainsboro;
                }
            }
        }

        // 3. Nơi viết các sự kiện (Events) tương tác của người dùng


        // 2. HÀM TỐI ƯU: Gộp tất cả các sự kiện Click của Menu làm một
        private void MenuButton_Click(object sender, EventArgs e)
        {
            // Ép kiểu 'sender' về thành một Button để máy hiểu nút nào vừa được bấm
            Button clickedButton = (Button)sender;

            // Bước 1: Đưa tất cả các nút về màu tối mặc định
            ResetButtonColor();

            // Bước 2: Làm sáng ĐÚNG cái nút vừa được click dựa vào biến 'clickedButton'
            clickedButton.BackColor = Color.FromArgb(33, 157, 212); // Màu nền xanh sáng
            clickedButton.ForeColor = Color.Gainsboro;              // Màu chữ

            panelContent.Controls.Clear(); // Xóa màn hình cũ đang hiển thị trong vùng trống

            if (clickedButton == btnHome)
            {
                // Khởi tạo và nạp giao diện ucHome vào vùng trống
                ucHome homeGiaoDien = new ucHome();
                homeGiaoDien.Dock = DockStyle.Fill;
                panelContent.Controls.Add(homeGiaoDien);
            }
            else if (clickedButton == btnShopping)
            {
                // Sau này khi bạn tạo ucShopping thì mở đoạn này ra dùng:
                // ucShopping shoppingGiaoDien = new ucShopping();
                // shoppingGiaoDien.Dock = DockStyle.Fill;
                // panelContent.Controls.Add(shoppingGiaoDien);
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Chỉ cần duy nhất 1 dòng này!
            // Hàm này sẽ tự kích hoạt làm sáng btnHome và tự nạp luôn ucHome một lần duy nhất.
            MenuButton_Click(btnHome, e);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            // Để trống hoặc xóa nếu không dùng
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }



}