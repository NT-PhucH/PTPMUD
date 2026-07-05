using QLST.BLL__Bat_ngoai_le_;
using QLST.DTO__Type_OTP_;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormLogin : Form
    {
        #region 1. Khai báo biến và Khởi tạo Form
        // Các biến hỗ trợ di chuyển form và tạo hiệu ứng animation
        private bool dragging = false;
        private Point dragStartPoint = new Point(0, 0);
        private int labelY_Up;
        private int labelY_Down;
        private int speed = 3;
        private int labelMatKhauY_Up;
        private int labelMatKhauY_Down;
        private NVLogin_BLL nhanVienBLL = new NVLogin_BLL();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Cấu hình vị trí cho Email
            labelY_Up = txtEmail.Top - 20;
            labelY_Down = txtEmail.Top;
            lblEmail.Top = labelY_Down;
            lblEmail.Left = txtEmail.Left;
            lblEmail.BringToFront();

            // Cấu hình vị trí cho Mật khẩu
            labelMatKhauY_Up = txtMatKhau.Top - 20;
            labelMatKhauY_Down = txtMatKhau.Top;
            lblMatKhau.Top = labelMatKhauY_Down;
            lblMatKhau.Left = txtMatKhau.Left;
            lblMatKhau.BringToFront();

            picShowHide.Left = panel3.Width - picShowHide.Width - 5;
            picShowHide.Top = txtMatKhau.Top + (txtMatKhau.Height - picShowHide.Height) / 2 - 5;

            picShowHide.Image = Properties.Resources.hide;

        }
        #endregion

        #region 2. Điều khiển Form (Thu nhỏ, Đóng, Di chuyển)
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStartPoint = new Point(e.X, e.Y);
        }

        private void panelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPoint = PointToScreen(e.Location);
                this.Location = new Point(currentScreenPoint.X - dragStartPoint.X, currentScreenPoint.Y - dragStartPoint.Y);
            }
        }

        private void panelTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        #region 3. Hiệu ứng UI - Input Email
        private void lblEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            timerEmail.Start();
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                timerEmail.Start();
            }
        }

        private void TimerEmail_Tick(object sender, EventArgs e)
        {
            if (txtEmail.Focused || txtEmail.Text.Length > 0)
            {
                if (lblEmail.Top > labelY_Up)
                {
                    lblEmail.Top -= speed;
                    lblEmail.Font = new Font(lblEmail.Font.FontFamily, 8);
                }
                else
                {
                    timerEmail.Stop();
                }
            }
            else
            {
                if (lblEmail.Top < labelY_Down)
                {
                    lblEmail.Top += speed;
                    lblEmail.Font = new Font(lblEmail.Font.FontFamily, 10);
                }
                else
                {
                    timerEmail.Stop();
                }
            }
        }
        #endregion

        #region 4. Hiệu ứng UI - Input Mật khẩu
        private void lblMatKhau_Click(object sender, EventArgs e)
        {
            txtMatKhau.Focus();
        }

        private void TxtMatKhau_Enter(object sender, EventArgs e)
        {
            timerMatKhau.Start();
        }

        private void TxtMatKhau_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                timerMatKhau.Start();
            }
        }

        private void TimerMatKhau_Tick(object sender, EventArgs e)
        {
            if (txtMatKhau.Focused || txtMatKhau.Text.Length > 0)
            {
                if (lblMatKhau.Top > labelMatKhauY_Up)
                {
                    lblMatKhau.Top -= speed;
                    lblMatKhau.Font = new Font(lblMatKhau.Font.FontFamily, 8, FontStyle.Regular);
                    lblMatKhau.ForeColor = Color.DimGray;
                }
                else
                {
                    timerMatKhau.Stop();
                }
            }
            else
            {
                if (lblMatKhau.Top < labelMatKhauY_Down)
                {
                    lblMatKhau.Top += speed;
                    lblMatKhau.Font = new Font(lblMatKhau.Font.FontFamily, 10, FontStyle.Regular);
                    lblMatKhau.ForeColor = Color.Black;
                }
                else
                {
                    timerMatKhau.Stop();
                }
            }
        }
        #endregion

        #region 5. Hiệu ứng UI - Vẽ thành phần phụ trợ (Line, Ẩn/Hiện mật khẩu)
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (Pen pen = new Pen(Color.Silver, 1.5f))
                {
                    e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
                }
            }
        }

        private void picShowHide_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !txtMatKhau.UseSystemPasswordChar;
            picShowHide.Image = txtMatKhau.UseSystemPasswordChar ? Properties.Resources.hide : Properties.Resources.show;
        }
        #endregion

        #region 6. Chức năng Nghiệp vụ chính (Đăng nhập & Thoát)
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtMatKhau.Text.Trim();
            string message;

            QLNV_DTO loggedInUser = nhanVienBLL.Login(username, password, out message);

            if (loggedInUser != null)
            {
                MessageBox.Show($"Xin chào {loggedInUser.TenNV}!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SessionManager.NhanVienDangNhap = loggedInUser;

                this.Hide();

                if (loggedInUser.Role == 1)
                {
                    FormMain mainForm = new FormMain(loggedInUser);
                    mainForm.ShowDialog();
                }
                else
                {
                    FormThuNgan thuNganForm = new FormThuNgan();
                    thuNganForm.ShowDialog();
                }

                this.Visible = true;
            }
            else
            {
                MessageBox.Show(message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}