using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLST.DTO__Type_OTP_.ThuNganOTP;

namespace QLST.GUI__Giao_dien_.ThuNganGUI.Ban_Hang
{
    public partial class ucThongBaoDonTam : UserControl
    {
        // Sự kiện gửi dữ liệu ra ngoài Form chính khi click khôi phục
        public event EventHandler<HoaDonTam_DTO> OnKhoiPhucHoaDon;

        public ucThongBaoDonTam()
        {
            InitializeComponent();
        }

        public void CapNhatGiaoDien(List<HoaDonTam_DTO> danhSachHoaDonTam)
        {
            flpDanhSachThongBao.SuspendLayout();
            flpDanhSachThongBao.Controls.Clear();

            if (danhSachHoaDonTam == null || danhSachHoaDonTam.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Không có đơn hàng chờ.",
                    ForeColor = Color.LightGray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                flpDanhSachThongBao.Controls.Add(lblEmpty);
                flpDanhSachThongBao.ResumeLayout();
                return;
            }

            foreach (var hd in danhSachHoaDonTam.OrderByDescending(x => x.ThoiGianLuu))
            {
                Panel pnlItem = new Panel
                {
                    Width = flpDanhSachThongBao.Width - 25,
                    Height = 70,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = hd
                };

                PictureBox picIcon = new PictureBox
                {
                    Image = global::QLST.Properties.Resources.shopping_cart__1_,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(40, 40),
                    Location = new Point(10, 15)
                };

                Label lblTitle = new Label
                {
                    Text = $"Đơn hàng tạm: {hd.MaHoaDon}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(60, 10),
                    AutoSize = true
                };

                Label lblTime = new Label
                {
                    Text = $"Lưu lúc {hd.ThoiGianLuu:HH:mm:ss} - {hd.DanhSachChiTiet.Sum(x => x.SoLuong)} SP",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.Gray,
                    Location = new Point(60, 35),
                    AutoSize = true
                };

                pnlItem.Controls.Add(picIcon);
                pnlItem.Controls.Add(lblTitle);
                pnlItem.Controls.Add(lblTime);

                pnlItem.MouseEnter += (s, e) => pnlItem.BackColor = Color.FromArgb(60, 60, 60);
                pnlItem.MouseLeave += (s, e) => pnlItem.BackColor = Color.Transparent;

                EventHandler clickEvent = (s, e) =>
                {
                    OnKhoiPhucHoaDon?.Invoke(this, hd);
                };

                pnlItem.Click += clickEvent;
                foreach (Control c in pnlItem.Controls)
                {
                    c.Click += clickEvent;
                }

                flpDanhSachThongBao.Controls.Add(pnlItem);
            }

            flpDanhSachThongBao.ResumeLayout();
        }
    }
}