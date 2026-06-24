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
        // Thêm class tạm này ở trong hoặc ngoài class ucThongKe đều được
        public class TopProductItem
        {
            public string Ten { get; set; }
            public string SoLuong { get; set; }
            public string DoanhThu { get; set; }
        }

        // Biến toàn cục lưu toàn bộ dữ liệu để xuất file Word
        private List<TopProductItem> _fullTopProducts = new List<TopProductItem>();
        private bool _isFirstLoad = true;

        public ucThongKe()
        {
            InitializeComponent();

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
            SetupBieuDoTopSanPham(); // Cấu hình cột và viền cho bảng Top SP
            NapDuLieuDashboard();
            cboThoiGian.SelectedIndex = 0; // Mặc định chọn "Tùy chọn"

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

            Series seriesDT = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = Color.FromArgb(0, 122, 204),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8
            };

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

        private void SetupBieuDoTopSanPham()
        {
            // Thiết lập các cột cho DataGridView Top Sản Phẩm
            dgvTopProduct.Columns.Add("colSTT", "STT");
            dgvTopProduct.Columns["colSTT"].Width = 40;
            dgvTopProduct.Columns["colSTT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTopProduct.Columns["colSTT"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProduct.Columns.Add("colTenSP", "Tên SP");
            dgvTopProduct.Columns["colTenSP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvTopProduct.Columns.Add("colSoLuong", "Đã bán");
            dgvTopProduct.Columns["colSoLuong"].Width = 65;
            dgvTopProduct.Columns["colSoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTopProduct.Columns["colSoLuong"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvTopProduct.Columns.Add("colDoanhThu", "Doanh thu");
            dgvTopProduct.Columns["colDoanhThu"].Width = 95;
            dgvTopProduct.Columns["colDoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTopProduct.Columns["colDoanhThu"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Xóa toàn bộ viền lưới để giao diện phẳng và đẹp mắt
            dgvTopProduct.BorderStyle = BorderStyle.None;
            dgvTopProduct.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvTopProduct.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTopProduct.BackgroundColor = Color.White;

            // Custom màu sắc Header và các dòng được chọn
            dgvTopProduct.EnableHeadersVisualStyles = false;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            // Xóa màu bôi xanh rực rỡ, đổi sang nền xám nhạt nhẹ nhàng khi click
            dgvTopProduct.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvTopProduct.DefaultCellStyle.SelectionForeColor = Color.Black;

            foreach (DataGridViewColumn col in dgvTopProduct.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Hủy bỏ trạng thái bôi xanh ngay khi người dùng click vào bất kỳ dòng/ô nào
            dgvTopProduct.SelectionChanged += (s, e) => dgvTopProduct.ClearSelection();
        }

        private void NapDuLieuDashboard()
        {
            lblDoanhThu.Text = "345,600,000 VNĐ";
            lblLoiNhuan.Text = "120,500,000 VNĐ";
            lblTongDonHang.Text = "1,250 hóa đơn";
            lblGiaTriTrungBinh.Text = "276,480 VNĐ";

            chartDoanhThu.Series["Doanh thu"].Points.Clear();
            chartDoanhThu.Series["Lợi nhuận gộp"].Points.Clear();

            string[] days = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
            decimal[] dtValues = { 45M, 55M, 48M, 65M, 70M, 85M, 92M };
            decimal[] lnValues = { 15M, 18M, 16M, 22M, 24M, 30M, 33M };

            for (int i = 0; i < days.Length; i++)
            {
                chartDoanhThu.Series["Doanh thu"].Points.AddXY(days[i], dtValues[i]);
                chartDoanhThu.Series["Lợi nhuận gộp"].Points.AddXY(days[i], lnValues[i]);
            }

            // Nạp dữ liệu giả vào bảng Top Sản Phẩm Bán Chạy
            // 1. Tạo danh sách dữ liệu ĐẦY ĐỦ (Giả lập lấy từ Database)
            _fullTopProducts.Clear();
            for (int i = 1; i <= 20; i++) // Giả sử có 20 sản phẩm
            {
                _fullTopProducts.Add(new TopProductItem { Ten = "Sản phẩm thứ " + i, SoLuong = (300 - i * 5).ToString(), DoanhThu = (15000000 - i * 100000).ToString("N0") + "đ" });
            }

            // 2. Logic giới hạn hiển thị trên DataGridView
            dgvTopProduct.Rows.Clear();
            int limit = 12; // Tổng số dòng tối đa cho phép hiển thị để không bị tràn khung

            if (_fullTopProducts.Count > limit)
            {
                // Lấy 9 sản phẩm đầu tiên
                for (int i = 0; i < 9; i++)
                {
                    var item = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), item.Ten, item.SoLuong, item.DoanhThu);
                }

                // Dòng chứa dấu ba chấm (...)
                int idx = dgvTopProduct.Rows.Add("...", "...", "...", "...");
                dgvTopProduct.Rows[idx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTopProduct.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;

                // Lấy 3 sản phẩm cuối cùng
                for (int i = _fullTopProducts.Count - 3; i < _fullTopProducts.Count; i++)
                {
                    var item = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), item.Ten, item.SoLuong, item.DoanhThu);
                }
            }
            else
            {
                // Nếu ít hơn giới hạn thì hiển thị bình thường
                for (int i = 0; i < _fullTopProducts.Count; i++)
                {
                    var item = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), item.Ten, item.SoLuong, item.DoanhThu);
                }
            }
        }

        private void cboThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            switch (cboThoiGian.SelectedIndex)
            {
                case 0:
                    int offset = today.DayOfWeek - DayOfWeek.Monday;
                    if (offset < 0) offset += 7;
                    dtTuNgay.Value = today.AddDays(-offset);
                    dtDenNgay.Value = today;
                    break;
                case 1:
                    dtTuNgay.Value = new DateTime(today.Year, today.Month, 1);
                    dtDenNgay.Value = today;
                    break;
                case 2:
                    dtTuNgay.Value = new DateTime(today.Year, 1, 1);
                    dtDenNgay.Value = today;
                    break;
                case 3:
                    break;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            NapDuLieuDashboard();
        }

        private void btnXuatData_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 10F);

            menu.Items.Add("📊 Xuất ra file Excel (.xlsx)", null, (s, ev) => ThucHienXuatFile("Excel"));
            menu.Items.Add("📕 Xuất ra file PDF (.pdf)", null, (s, ev) => ThucHienXuatFile("PDF"));

            menu.Show(btnXuatData, new Point(0, -menu.PreferredSize.Height - 10));
        }

        private void ThucHienXuatFile(string loaiFile)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = $"Chọn nơi lưu file {loaiFile}";
                sfd.FileName = "ThongKe_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

                if (loaiFile == "Excel")
                    sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                else
                    sfd.Filter = "PDF Document (*.pdf)|*.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Đã trích xuất dữ liệu ra file {loaiFile} thành công!\n\nĐường dẫn: {sfd.FileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
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

        // Bỏ bôi xanh khi nhấp chuột ra ngoài bảng
        private void dgvTopProduct_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None) dgv.ClearSelection();
        }

        

        private void btnCTTopSp_Click(object sender, EventArgs e)
        {
            if (_fullTopProducts == null || _fullTopProducts.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu chi tiết Top Sản Phẩm";
                sfd.Filter = "Word Document (*.doc)|*.doc";
                sfd.FileName = "ChiTietTopSanPham_" + DateTime.Now.ToString("ddMMyyyy") + ".doc";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Dùng StringBuilder tạo cấu trúc bảng để Word tự convert
                        System.Text.StringBuilder html = new System.Text.StringBuilder();
                        html.Append("<html><head><meta charset='utf-8'></head><body>");
                        html.Append("<h2 style='text-align:center; font-family: Arial;'>CHI TIẾT TOP SẢN PHẨM BÁN CHẠY</h2>");
                        html.Append("<p style='text-align:center; font-family: Arial;'>Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</p>");

                        html.Append("<table border='1' style='width:100%; border-collapse:collapse; font-family: Arial;'>");
                        html.Append("<tr style='background-color:#f2f2f2;'><th>STT</th><th>Tên Sản Phẩm</th><th>Số Lượng Bán</th><th>Doanh Thu</th></tr>");

                        // Lặp qua toàn bộ danh sách (chứ không phải danh sách đã bị cắt trên Grid)
                        int stt = 1;
                        foreach (var item in _fullTopProducts)
                        {
                            html.Append($"<tr><td style='text-align:center;'>{stt++}</td>");
                            html.Append($"<td>{item.Ten}</td>");
                            html.Append($"<td style='text-align:center;'>{item.SoLuong}</td>");
                            html.Append($"<td style='text-align:right;'>{item.DoanhThu}</td></tr>");
                        }

                        html.Append("</table></body></html>");

                        // Ghi ra file
                        System.IO.File.WriteAllText(sfd.FileName, html.ToString());
                        MessageBox.Show("Đã xuất file Word thành công!\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
    }
}