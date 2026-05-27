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
    public partial class FormLogin : Form
    {
        private bool dragging = false;
        private Point dragStartPoint = new Point(0, 0);
        private int labelY_Up;
        private int labelY_Down;
        private int speed = 3;
        public FormLogin()
        {
            InitializeComponent();

            // 1. Tự động tính toán vị trí dựa vào txtEmail
            // Vị trí lúc nằm TRONG textbox (Cộng thêm vài pixel để căn giữa dọc)
            labelY_Down = txtEmail.Top + 4;

            // Vị trí lúc bay LÊN TRÊN textbox (Trừ đi chiều cao của label để nó nằm ngay trên đầu)
            labelY_Up = txtEmail.Top - lblEmail.Height;

            // 2. Thiết lập vị trí ban đầu cho chữ nằm bên trong TextBox
            lblEmail.Top = labelY_Down;
            lblEmail.Left = txtEmail.Left + 2; // Thụt lề trái vào một chút cho đẹp

            // 3. Đảm bảo chữ luôn nổi lên trên TextBox
            lblEmail.BringToFront();

            // 4. Gán các sự kiện
            lblEmail.Click += (s, e) => txtEmail.Focus(); // Click vào chữ -> focus vào textbox
            txtEmail.Enter += TxtEmail_Enter;
            txtEmail.Leave += TxtEmail_Leave;
            timerEmail.Tick += TimerEmail_Tick;
            this.Load += FormLogin_Load;
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            // 1. Tính toán vị trí khi chạy LÊN (Vị trí hiện tại bạn đang để trên Form)
            // Tức là mép dưới của Label sẽ vừa chạm mép trên của TextBox
            labelY_Up = txtEmail.Top - lblEmail.Height;

            // 2. Tính toán vị trí ban đầu nằm TRONG TextBox
            // Cộng thêm khoảng 2-4 pixel để chữ căn giữa theo chiều dọc của TextBox
            labelY_Down = txtEmail.Top + 2;

            // 3. Ép Label về vị trí BAN ĐẦU (Nằm bên trong TextBox)
            lblEmail.Top = labelY_Down;

            // Căn lề trái cho Label thụt vào trong TextBox một chút cho đẹp
            lblEmail.Left = txtEmail.Left + 5;

            // 4. Đảm bảo Label luôn nổi lên trên cùng, không bị TextBox đè mất
            lblEmail.BringToFront();
            lblEmail.BackColor = Color.White; // Hoặc màu nền của Panel để không bị lộ viền
        }
        // Sự kiện click nút Thu nhỏ
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Sự kiện click nút Đóng
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Khi người dùng nhấn chuột xuống trên thanh tiêu đề
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStartPoint = new Point(e.X, e.Y); // Lưu vị trí bắt đầu
        }

        // Khi người dùng di chuyển chuột trên thanh tiêu đề
        private void panelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                // Tính toán khoảng cách chuột đã di chuyển
                Point currentScreenPoint = PointToScreen(e.Location);
                this.Location = new Point(currentScreenPoint.X - dragStartPoint.X, currentScreenPoint.Y - dragStartPoint.Y);
            }
        }

        // Khi người dùng thả chuột
        private void panelTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        // Đặt tọa độ Y cho Label
        



        // Khi người dùng click vào TextBox
        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            timerEmail.Start();
        }

        // Khi người dùng click ra ngoài TextBox
        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            // Chỉ chạy chữ về chỗ cũ nếu người dùng CHƯA nhập gì
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                timerEmail.Start();
            }
        }

        // Xử lý chuyển động (Animation)
        private void TimerEmail_Tick(object sender, EventArgs e)
        {
            // Nếu TextBox đang được chọn HOẶC đã có chữ bên trong -> Chữ phải ở trên cùng
            if (txtEmail.Focused || txtEmail.Text.Length > 0)
            {
                if (lblEmail.Top > labelY_Up)
                {
                    lblEmail.Top -= speed; // Di chuyển lên
                    lblEmail.Font = new Font(lblEmail.Font.FontFamily, 8); // Thu nhỏ chữ
                }
                else
                {
                    timerEmail.Stop(); // Dừng Timer khi đã đến đích
                }
            }
            // Nếu TextBox trống và không được chọn -> Chữ chạy xuống vị trí ban đầu
            else
            {
                if (lblEmail.Top < labelY_Down)
                {
                    lblEmail.Top += speed; // Di chuyển xuống
                    lblEmail.Font = new Font(lblEmail.Font.FontFamily, 10); // Phóng to chữ lại
                }
                else
                {
                    timerEmail.Stop();
                }
            }
        }
    }
}
