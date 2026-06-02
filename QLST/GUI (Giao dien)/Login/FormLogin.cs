using QLST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Hãy chắc chắn đã thêm thư viện này để dùng SqlConnection
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

        // Khai báo chuỗi kết nối Database (Thay đổi thông tin Data Source và Initial Catalog cho đúng cấu hình của bạn)
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLySieuThiv2;Integrated Security=True";

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

            // Thiết lập sự kiện của email
            lblEmail.Click += (s, e) => txtEmail.Focus();
            txtEmail.Enter += TxtEmail_Enter;
            txtEmail.Leave += TxtEmail_Leave;
            timerEmail.Tick += TimerEmail_Tick;
            this.Load += FormLogin_Load;

            panel2.Paint += Panel_Paint;
            panel3.Paint += Panel_Paint;

            // Bắt buộc phải đăng ký sự kiện Click cho nút đăng nhập nếu trong Designer chưa gán
            btnLogIn.Click += btnLogIn_Click;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            // 1. Kiểm tra dữ liệu đầu vào xem đã nhập chưa
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Khối using kết nối CSDL phải nằm TRONG phương thức btnLogIn_Click
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Truy vấn lấy Role và TrangThai của tài khoản khớp Username và Password
                    string query = "SELECT Role, TrangThai FROM NhanVien WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Nếu tìm thấy tài khoản hợp lệ
                            {
                                // Kiểm tra trạng thái tài khoản (TrangThai = true/1 mới cho phép đăng nhập)
                                bool trangThai = Convert.ToBoolean(reader["TrangThai"]);
                                if (!trangThai)
                                {
                                    MessageBox.Show("Tài khoản của bạn đã bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }

                                // Lấy giá trị Role từ database
                                int role = Convert.ToInt32(reader["Role"]);

                                // 3. Rẽ nhánh xử lý chuyển giao diện dựa theo Role
                                this.Hide(); // Ẩn Form Đăng nhập hiện tại đi

                                if (role == 1)
                                {
                                    // Tạo và hiển thị FormMain (Dành cho Quản lý / Kế toán)
                                    FormMain frmMain = new FormMain();
                                    frmMain.ShowDialog();
                                }
                                else if (role > 1)
                                {
                                    // Tạo và hiển thị FormThuNgan (Thu ngân, Kho, Bảo vệ...)
                                    FormThuNgan frmThuNgan = new FormThuNgan();
                                    frmThuNgan.ShowDialog();
                                }

                                this.Close(); // Đóng hoàn toàn Form đăng nhập sau khi Form chức năng tắt
                            }
                            else
                            {
                                // Không tìm thấy dòng dữ liệu nào khớp
                                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } // Kết thúc hàm btnLogIn_Click đúng vị trí

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // 1. Chỉnh Padding(Trái, Trên, Phải, Dưới)
            panel2.Padding = new Padding(0, 0, 0, 2);
            panel3.Padding = new Padding(0, 0, 35, 2);

            // 2. Cấu hình vị trí cho Email
            labelY_Up = txtEmail.Top - 20;
            labelY_Down = txtEmail.Top;
            lblEmail.Top = labelY_Down;
            lblEmail.Left = txtEmail.Left;
            lblEmail.BringToFront();
            lblEmail.BackColor = Color.White;

            // 3. Cấu hình vị trí cho Mật khẩu
            labelMatKhauY_Up = txtMatKhau.Top - 20;
            labelMatKhauY_Down = txtMatKhau.Top;
            lblMatKhau.Top = labelMatKhauY_Down;
            lblMatKhau.Left = txtMatKhau.Left;
            lblMatKhau.BringToFront();
            lblMatKhau.BackColor = Color.White;

            picShowHide.Left = panel3.Width - picShowHide.Width - 5;
            picShowHide.Image = Properties.Resources.hide;

            // Căn chỉnh tọa độ dòng
            picShowHide.Top = txtMatKhau.Top + (txtMatKhau.Height - picShowHide.Height) / 2 - 5;
        }

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

            if (txtMatKhau.UseSystemPasswordChar)
            {
                picShowHide.Image = Properties.Resources.hide;
            }
            else
            {
                picShowHide.Image = Properties.Resources.show;
            }
        }
    }
}