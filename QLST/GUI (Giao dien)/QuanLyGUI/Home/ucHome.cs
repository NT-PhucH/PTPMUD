using QLST.BLL__Bat_ngoai_le_;
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
    public partial class ucHome : UserControl
    {
        public ucHome()
        {
            InitializeComponent();

            // --- BẬT TÍNH NĂNG DOUBLEBUFFERED ĐỂ GIẢM NHẤP NHÁY ---
            // Cách 1: Sử dụng Reflection để bật thuộc tính DoubleBuffered ẩn
            typeof(UserControl).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, this, new object[] { true });

            // Hoặc cách 2 đơn giản hơn (nếu bạn kế thừa trực tiếp từ Control):
            // this.DoubleBuffered = true;
        }

        // Sự kiện chạy khi giao diện ucHome được nạp lên màn hình
        private void ucHome_Load(object sender, EventArgs e)
        {
            // Gọi hàm nạp dữ liệu ngay khi vừa mở màn hình lên
            LoadDashboardData();
        }

        /// <summary>
        /// Hàm chuyên trách lấy dữ liệu và hiển thị lên giao diện
        /// </summary>
        // Đừng quên using thư viện BLL ở đầu file nhé:
        // using QLST.BLL__Bat_ngoai_le_;

        private void LoadDashboardData()
        {
            // 1. GỌI TẦNG BLL LẤY DỮ LIỆU THẬT
            ThongKe_BLL thongKeBLL = new ThongKe_BLL();

            double doanhThuHomQua = thongKeBLL.LayDoanhThuNgay(DateTime.Today.AddDays(-1));
            double doanhThuHomNay = thongKeBLL.LayDoanhThuNgay(DateTime.Today);

            // Kiểm tra nếu hôm qua bằng 0 để tránh lỗi chia cho 0
            if (doanhThuHomQua == 0)
            {
                lblDoanhThuHomNay.Text = doanhThuHomNay.ToString("#,##0") + " VNĐ (Hôm qua 0đ)";
                lblDoanhThuHomNay.ForeColor = Color.Black;
            }
            else
            {
                // Tính toán phần trăm chênh lệch
                double phanTramBienDong = ((doanhThuHomNay - doanhThuHomQua) / doanhThuHomQua) * 100;
                string chuoiDoanhThu = doanhThuHomNay.ToString("#,##0") + " VNĐ";

                // XỬ LÝ LOGIC GHÉP CHUỖI VÀ ĐỔI MÀU
                if (doanhThuHomNay > doanhThuHomQua)
                {
                    lblDoanhThuHomNay.Text = chuoiDoanhThu + " (+" + phanTramBienDong.ToString("0.0") + "%)";
                    lblDoanhThuHomNay.ForeColor = Color.FromArgb(0, 182, 122);
                }
                else if (doanhThuHomNay < doanhThuHomQua)
                {
                    lblDoanhThuHomNay.Text = chuoiDoanhThu + " (" + phanTramBienDong.ToString("0.0") + "%)";
                    lblDoanhThuHomNay.ForeColor = Color.FromArgb(231, 76, 60);
                }
                else
                {
                    lblDoanhThuHomNay.Text = chuoiDoanhThu + " (0.0%)";
                    lblDoanhThuHomNay.ForeColor = Color.FromArgb(51, 51, 51);
                }
            }

            // =======================================================
            // XỬ LÝ VẼ BIỂU ĐỒ ĐỘNG CUỘN 7 NGÀY GẦN NHẤT + NGÀY MAI
            // =======================================================
            chartDoanhThu.Series[0].Points.Clear();
            chartDoanhThu.Series[0].Name = "Doanh thu";
            chartDoanhThu.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

            string[] danhSachThu = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };

            // Chạy từ 6 ngày trước (i = 6) đến ngày mai (i = -1)
            for (int i = 6; i >= -1; i--)
            {
                DateTime ngayHienTai = DateTime.Today.AddDays(-i);
                int indexThu = (int)ngayHienTai.DayOfWeek;
                string tenThu = danhSachThu[indexThu];

                if (i == -1) // Ngày mai
                {
                    int pointIndex = chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenThu, System.DBNull.Value);
                    chartDoanhThu.Series["Doanh thu"].Points[pointIndex].IsEmpty = true;
                }
                else if (i == 0) // Hôm nay
                {
                    // Dùng luôn biến đã lấy ở trên cho đỡ phải gọi SQL 2 lần
                    chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenThu + " (Nay)", doanhThuHomNay);
                }
                else // Quá khứ
                {
                    // Gọi Database lấy số của từng ngày trong quá khứ
                    double doanhThuNgayCu = thongKeBLL.LayDoanhThuNgay(ngayHienTai);
                    chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenThu, doanhThuNgayCu);
                }
            }
        }

        // Hàm tự động cập nhật lại vùng bo tròn khi Panel thay đổi kích thước theo màn hình
        private void Panel_Resize(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            int radius = 15;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius - 1, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius - 1, panel.Height - radius - 1, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius - 1, radius, radius, 90, 90);
            path.CloseAllFigures();

            // Cập nhật lại Region mới khít với kích thước vừa co giãn
            panel.Region = new Region(path);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chartDoanhThu_Click(object sender, EventArgs e)
        {

        }

        private void lblDoanhThuHomNay_Click(object sender, EventArgs e)
        {

        }
    }
    
}
