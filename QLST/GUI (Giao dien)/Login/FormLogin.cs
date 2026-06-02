using QLST.BLL__Bat_ngoai_le_;
using QLST.DTO__Type_OTP_;
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
        private Timer timerMatKhau;
        private int labelMatKhauY_Up;
        private int labelMatKhauY_Down;
        public FormLogin()
        {
            InitializeComponent();

            // Khởi tạo timer độc lập cho mật khẩu
            timerMatKhau = new Timer(this.components);
            timerMatKhau.Interval = 10;

            // Tự động tính toán vị trí dựa vào txtMatKhau
            labelMatKhauY_Down = txtMatKhau.Top + 4;
            labelMatKhauY_Up = txtMatKhau.Top - lblMatKhau.Height;

            // Thiết lập vị trí ban đầu
            lblMatKhau.Top = labelMatKhauY_Down;
            lblMatKhau.Left = txtMatKhau.Left + 2;
            lblMatKhau.BringToFront();

            // Gán các sự kiện cho ô mật khẩu
            lblMatKhau.Click += (s, e) => txtMatKhau.Focus();
            txtMatKhau.Enter += TxtMatKhau_Enter;
            txtMatKhau.Leave += TxtMatKhau_Leave;
            timerMatKhau.Tick += TimerMatKhau_Tick;

            // (Giữ nguyên các thiết lập sự kiện của email phía dưới...)
            lblEmail.Click += (s, e) => txtEmail.Focus();
            txtEmail.Enter += TxtEmail_Enter;
            txtEmail.Leave += TxtEmail_Leave;
            timerEmail.Tick += TimerEmail_Tick;
            this.Load += FormLogin_Load;

            panel2.Paint += Panel_Paint;
            panel3.Paint += Panel_Paint;
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            // 1. Chỉnh Padding(Trái, Trên, Phải, Dưới)
            // Đẩy TextBox lùi vào 5px ở lề trái và nhường 2px ở lề dưới cho đường gạch ngang
            panel2.Padding = new Padding(0, 0, 0, 2);
            panel3.Padding = new Padding(0, 0, 35, 2);

            // 2. Cấu hình vị trí cho Email
            labelY_Up = txtEmail.Top - 20;
            labelY_Down = txtEmail.Top ;
            lblEmail.Top = labelY_Down;
            lblEmail.Left = txtEmail.Left; // Bằng đúng vị trí TextBox (không cộng thêm 5 nữa)
            lblEmail.BringToFront();
            lblEmail.BackColor = Color.White;

            // 3. Cấu hình vị trí cho Mật khẩu
            labelMatKhauY_Up = txtMatKhau.Top - 20;
            labelMatKhauY_Down = txtMatKhau.Top ;
            lblMatKhau.Top = labelMatKhauY_Down;
            lblMatKhau.Left = txtMatKhau.Left; // Bằng đúng vị trí TextBox
            lblMatKhau.BringToFront();
            lblMatKhau.BackColor = Color.White;

            picShowHide.Left = panel3.Width - picShowHide.Width - 5;
            picShowHide.Image = Properties.Resources.hide;

            // Căn chỉnh tọa độ dòng: Cho con mắt nằm chính giữa theo chiều dọc của ô txtMatKhau
            picShowHide.Top = txtMatKhau.Top + (txtMatKhau.Height - picShowHide.Height) / 2-5;
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
        // Khi người dùng click vào ô Mật khẩu
        private void TxtMatKhau_Enter(object sender, EventArgs e)
        {
            timerMatKhau.Start();
        }

        // Khi người dùng click ra ngoài ô Mật khẩu
        private void TxtMatKhau_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                timerMatKhau.Start();
            }
        }

        // Xử lý chuyển động hiệu ứng nổi cho ô Mật khẩu
        private void TimerMatKhau_Tick(object sender, EventArgs e)
        {
            // Nếu đang chọn ô mật khẩu hoặc ô đã nhập chữ -> chữ bay lên và thu nhỏ
            if (txtMatKhau.Focused || txtMatKhau.Text.Length > 0)
            {
                if (lblMatKhau.Top > labelMatKhauY_Up)
                {
                    lblMatKhau.Top -= speed; // Di chuyển lên với tốc độ speed = 3
                    lblMatKhau.Font = new Font(lblMatKhau.Font.FontFamily, 8, FontStyle.Regular);
                    lblMatKhau.ForeColor = Color.DimGray;
                }
                else
                {
                    timerMatKhau.Stop();
                }
            }
            // Nếu ô trống và không được chọn -> chữ trượt xuống vị trí cũ
            else
            {
                if (lblMatKhau.Top < labelMatKhauY_Down)
                {
                    lblMatKhau.Top += speed; // Di chuyển xuống
                    lblMatKhau.Font = new Font(lblMatKhau.Font.FontFamily, 10, FontStyle.Regular);
                    lblMatKhau.ForeColor = Color.Black;
                }
                else
                {
                    timerMatKhau.Stop();
                }
            }
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                // Tạo bút vẽ màu xám (Color.DarkGray hoặc Color.Silver tùy bạn chọn độ đậm nhạt)
                // Độ dày nét vẽ là 1.5f hoặc 2 để đường nét rõ ràng giống ảnh mẫu
                using (Pen pen = new Pen(Color.Silver, 1.5f))
                {
                    // Vẽ một đường thẳng từ góc dưới bên trái sang góc dưới bên phải của Panel
                    e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
                }
            }
        }
        private void picShowHide_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái ẩn/hiện mật khẩu
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;

            // Thay đổi ảnh tương ứng với trạng thái
            if (txtMatKhau.UseSystemPasswordChar)
            {
                // Khi mật khẩu bị ẩn (hiện dấu chấm) -> Hiển thị icon mắt nhắm
                picShowHide.Image = Properties.Resources.hide;
            }
            else
            {
                // Khi mật khẩu hiển thị chữ -> Hiển thị icon mắt mở
                picShowHide.Image = Properties.Resources.show;
            }
        }
        private NVLogin_BLL nhanVienBLL = new NVLogin_BLL();

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtMatKhau.Text.Trim();
            string message;

            NhanVienDTO loggedInUser = nhanVienBLL.Login(username, password, out message);

            if (loggedInUser != null)
            {
                // Thành công, có thể hiển thị lời chào dựa trên tên
                MessageBox.Show($"Xin chào {loggedInUser.TenNV}!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Lưu thông tin vào SessionManager toàn cục
                SessionManager.NhanVienDangNhap = loggedInUser;

                // Mở Form Main và truyền DTO sang để phân quyền
                FormMain mainForm = new FormMain(loggedInUser); 
                this.Hide(); // Ẩn Form Login khi mở Form Main
                mainForm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                // Thất bại, in ra câu thông báo lỗi từ BLL
                MessageBox.Show(message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
