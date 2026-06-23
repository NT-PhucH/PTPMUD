using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLST
{
    public partial class ucThongKe : UserControl
    {
        private bool _isFirstLoad = true;

        public ucThongKe()
        {
            InitializeComponent();

            // Kích hoạt tính năng DoubleBuffered cho các Panel để tối ưu hóa việc vẽ lại giao diện khi co giãn màn hình
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            typeof(TableLayoutPanel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, tlpMain, new object[] { true });

            this.Load += UcThongKe_Load;
            this.VisibleChanged += UcThongKe_VisibleChanged;
        }

        private void UcThongKe_Load(object sender, EventArgs e)
        {
            KhoiTaoBieuDoMacDinh();
            NapDuLieuDashboard();
            _isFirstLoad = false;
        }

        private void UcThongKe_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_isFirstLoad)
            {
                NapDuLieuDashboard();
            }
        }

        private void KhoiTaoBieuDoMacDinh()
        {
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Series Doanh Thu
            Series seriesDT = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = Color.FromArgb(0, 122, 204),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            // Series Lợi Nhuận
            Series seriesLN = new Series("Lợi nhuận gộp")
            {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = Color.FromArgb(243, 156, 18),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

            chartDoanhThu.Series.Add(seriesDT);
            chartDoanhThu.Series.Add(seriesLN);
        }

        private void NapDuLieuDashboard()
        {
            // Thử nghiệm dữ liệu mẫu trực quan hóa tỷ lệ phần trăm
            lblDoanhThu.Text = "345,600,000 VNĐ";
            lblLoiNhuan.Text = "120,500,000 VNĐ";
            lblTongDonHang.Text = "1,250 hóa đơn";
            lblGiaTriTrungBinh.Text = "276,480 VNĐ";

            // Vẽ dữ liệu lên biểu đồ (Ví dụ tuần qua)
            chartDoanhThu.Series["Doanh thu"].Points.Clear();
            chartDoanhThu.Series["Lợi nhuận gộp"].Points.Clear();

            string[] days = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
            decimal[] dtValues = { 45M, 55M, 48M, 65M, 70M, 85M, 92M }; // Đơn vị Triệu
            decimal[] lnValues = { 15M, 18M, 16M, 22M, 24M, 30M, 33M };

            for (int i = 0; i < days.Length; i++)
            {
                chartDoanhThu.Series["Doanh thu"].Points.AddXY(days[i], dtValues[i]);
                chartDoanhThu.Series["Lợi nhuận gộp"].Points.AddXY(days[i], lnValues[i]);
            }

            // Đổ dữ liệu mẫu vào DataGridView Giao dịch gần nhất
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDon", typeof(string));
            dt.Columns.Add("ThoiGian", typeof(string));
            dt.Columns.Add("ThuNgan", typeof(string));
            dt.Columns.Add("TongTien", typeof(string));
            dt.Columns.Add("TrangThai", typeof(string));

            dt.Rows.Add("HD00991", "14:35", "Nguyễn Văn A", "125,000đ", "Hoàn thành");
            dt.Rows.Add("HD00990", "14:32", "Trần Thị B", "1,450,000đ", "Hoàn thành");
            dt.Rows.Add("HD00989", "14:28", "Nguyễn Văn A", "320,000đ", "Hủy/Trả hàng");

            dgvGiaoDich.DataSource = dt;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            // Thực hiện xử lý lọc dữ liệu tại đây dựa vào dtTừNgày, dtĐếnNgày, cboThuNgan, cboCaLamViec
            NapDuLieuDashboard();
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            // Tự động bo góc các thẻ KPI giống ucHome
            Panel panel = (Panel)sender;
            int radius = 12;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius - 1, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius - 1, panel.Height - radius - 1, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius - 1, radius, radius, 90, 90);
            path.CloseAllFigures();
            panel.Region = new Region(path);
        }
    }
}