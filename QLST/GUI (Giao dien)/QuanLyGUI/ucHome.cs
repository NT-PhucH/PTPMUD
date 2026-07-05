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
        // THÊM BIẾN FLAG NÀY: Để tránh việc dữ liệu bị gọi load trùng 2 lần khi khởi động
        private bool _isFirstLoad = true;

        public ucHome()
        {
            InitializeComponent();

            // --- BẬT TÍNH NĂNG DOUBLEBUFFERED ĐỂ GIẢM NHẤP NHÁY ---
            typeof(UserControl).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, this, new object[] { true });

            // ĐĂNG KÝ CÁC SỰ KIỆN NGAY TẠI ĐÂY (An toàn tuyệt đối, không chạm vào Designer)
            this.Load += UcHome_Load;
            this.VisibleChanged += UcHome_VisibleChanged;
        }

        // 1. CHẠY KHI MỞ APP LÊN LẦN ĐẦU TIÊN
        private void UcHome_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
            _isFirstLoad = false; // Đánh dấu đã chạy xong lần đầu
        }

        // 2. CHẠY KHI CHUYỂN QUA LẠI GIỮA CÁC TAB
        private void UcHome_VisibleChanged(object sender, EventArgs e)
        {
            // Chỉ chạy khi tab được hiện lên VÀ không phải là lần load đầu tiên (để tránh trùng lặp)
            if (this.Visible && !_isFirstLoad)
            {
                LoadDashboardData();
            }
        }

        /// <summary>
        /// Hàm chuyên trách lấy dữ liệu và hiển thị lên giao diện
        /// </summary>
        // Đừng quên using thư viện BLL ở đầu file nhé:
        // using QLST.BLL__Bat_ngoai_le_;

        private void LoadDashboardData()
        {
            // 1. GỌI TẦNG BLL LẤY DỮ LIỆU THẬT
            ThongKeHome_BLL thongKeBLL = new ThongKeHome_BLL();

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
            // CHÈN ĐOẠN CODE MỚI NÀY VÀO ĐÂY ĐỂ ĐỔI CHỮ VÀ HIỂN THỊ SỐ
            // =======================================================
            // 1. Đổi tiêu đề từ "Tổng đơn hàng:" thành "Tổng hóa đơn hôm nay:" tại lúc chạy
            label2.Text = "Tổng hóa đơn hôm nay:";

            // 2. Gọi tầng BLL lấy tổng số hóa đơn thực tế trong ngày hôm nay từ database
            int soLuongHoaDonNay = thongKeBLL.LaySoLuongHoaDonNgay(DateTime.Today);

            // 3. Hiển thị con số đó lên label hiển thị giá trị (lblTongDonHang)
            lblTongDonHang.Text = soLuongHoaDonNay.ToString("N0") + " hóa đơn";
            lblTongDonHang.ForeColor = Color.FromArgb(0, 122, 204); // Đổi sang màu xanh dương cho đẹp
            lblTongDonHang.Font = new Font("Segoe UI", 12f, FontStyle.Bold); // Định dạng chữ to rõ ràng

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
            // CẬP NHẬT CẢNH BÁO KHO
            lbHangHetTon.Items.Clear();
            List<string> sapHetTon = thongKeBLL.LayDanhSachSapHetTon();
            if (sapHetTon.Count > 0)
            {
                foreach (var item in sapHetTon) lbHangHetTon.Items.Add(item);
            }
            else
            {
                lbHangHetTon.Items.Add("✓ Kho hàng ổn định, không có SP sắp hết.");
            }

            lbHangHetHan.Items.Clear();
            List<string> sapHetHan = thongKeBLL.LayDanhSachSapHetHan();
            if (sapHetHan.Count > 0)
            {
                foreach (var item in sapHetHan) lbHangHetHan.Items.Add(item);
            }
            else
            {
                lbHangHetHan.Items.Add("✓ Không có SP sắp hết hạn.");
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
        
    }
    
}
