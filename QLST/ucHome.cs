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
        private void LoadDashboardData()
        {
            // =======================================================
            // GIAI ĐOẠN GIẢ LẬP (Mai sau các số này sẽ lấy từ SQL)
            // =======================================================
            double doanhThuHomQua = 4000000;
            double doanhThuHomNay = 5450000;

            // Kiểm tra nếu hôm qua bằng 0 để tránh lỗi chia cho 0
            if (doanhThuHomQua == 0)
            {
                lblDoanhThuHomNay.Text = doanhThuHomNay.ToString("#,##0") + " đ (Chưa có đối sánh)";
                lblDoanhThuHomNay.ForeColor = Color.Black; // Hoặc màu mặc định của bạn
                return;
            }

            // Tính toán phần trăm chênh lệch
            double phanTramBienDong = ((doanhThuHomNay - doanhThuHomQua) / doanhThuHomQua) * 100;

            // Chuẩn bị chuỗi định dạng tiền tệ cho doanh thu hôm nay
            string chuoiDoanhThu = doanhThuHomNay.ToString("#,##0") + " VNĐ";

            // =======================================================
            // XỬ LÝ LOGIC GHÉP CHUỖI VÀ ĐỔI MÀU (1 LABEL DUY NHẤT)
            // =======================================================

            // Trường hợp 1: Doanh thu TĂNG
            if (doanhThuHomNay > doanhThuHomQua)
            {
                // Ghép chuỗi thành dạng: 5,450,000 VNĐ (+36.3%)
                lblDoanhThuHomNay.Text = chuoiDoanhThu + " (+" + phanTramBienDong.ToString("0.0") + "%)";

                // Đổi toàn bộ chữ sang màu xanh sáng
                lblDoanhThuHomNay.ForeColor = Color.FromArgb(0, 182, 122);
            }
            // Trường hợp 2: Doanh thu GIẢM
            else if (doanhThuHomNay < doanhThuHomQua)
            {
                // Vì phanTramBienDong đã mang dấu âm sẵn (ví dụ: -15.5), nên khi ghép chuỗi sẽ thành: 3,000,000 VNĐ (-25.0%)
                lblDoanhThuHomNay.Text = chuoiDoanhThu + " (" + phanTramBienDong.ToString("0.0") + "%)";

                // Đổi toàn bộ chữ sang màu đỏ
                lblDoanhThuHomNay.ForeColor = Color.FromArgb(231, 76, 60);
            }
            // Trường hợp 3: BẰNG NHAU
            else
            {
                lblDoanhThuHomNay.Text = chuoiDoanhThu + " (0.0%)";
                lblDoanhThuHomNay.ForeColor = Color.FromArgb(51, 51, 51); // Màu tối mặc định
            }

            // =======================================================
            // XỬ LÝ VẼ BIỂU ĐỒ ĐỘNG CUỘN 7 NGÀY GẦN NHẤT + NGÀY MAI
            // =======================================================

            // 1. Xóa sạch dữ liệu và các mốc cũ trên biểu đồ
            chartDoanhThu.Series[0].Points.Clear();
            chartDoanhThu.Series[0].Name = "Doanh thu";
            chartDoanhThu.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

            // Mảng định nghĩa tên các thứ bằng tiếng Việt
            string[] danhSachThu = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };

            // 2. Vòng lặp: chạy từ 6 ngày trước (i = 6) đến ngày mai (i = -1)
            for (int i = 6; i >= -1; i--)
            {
                // Lấy mốc ngày dựa vào biến i (i = -1 sẽ là Ngày mai)
                DateTime ngayHienTai = DateTime.Today.AddDays(-i);

                // Tìm xem ngày đó là thứ mấy trong tuần
                int indexThu = (int)ngayHienTai.DayOfWeek;
                string tenThu = danhSachThu[indexThu];

                // TRƯỜNG HỢP LÀ NGÀY MAI (i = -1)
                if (i == -1)
                {
                    // 3. THAY ĐỔI TẠI ĐÂY: Thêm điểm dữ liệu nhưng giá trị Y là rỗng (DBNull.Value)
                    // Cách này giúp trục X vẫn hiện tên thứ (Ví dụ: T4) nhưng đường đồ thị dừng lại ở hôm nay
                    int pointIndex = chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenThu, System.DBNull.Value);

                    // Đánh dấu điểm này là điểm trống để chart không cố vẽ nối vào
                    chartDoanhThu.Series["Doanh thu"].Points[pointIndex].IsEmpty = true;
                }
                // TRƯỜNG HỢP LÀ HÔM NAY (i = 0)
                else if (i == 0)
                {
                    double doanhThuGiaLap = 5450000; // Lấy đúng doanh thu hôm nay
                    string tenHienThi = tenThu + " (Hôm nay)";

                    chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenHienThi, doanhThuGiaLap);
                }
                // TRƯỜNG HỢP CÁC NGÀY TRONG QUÁ KHỨ (i từ 1 đến 6)
                else
                {
                    // Các ngày trước đó cho ngẫu nhiên từ 10tr đến 15tr để đồ thị uốn lượn
                    double doanhThuGiaLap = new Random(indexThu).Next(10, 16) * 1000000;

                    chartDoanhThu.Series["Doanh thu"].Points.AddXY(tenThu, doanhThuGiaLap);
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
    }
    
}
