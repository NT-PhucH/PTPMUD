using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;

namespace QLST
{
    public partial class ucThongKe : UserControl
    {
        // 1. Khai báo tầng BLL để lấy dữ liệu thật
        private readonly ThongKe_BLL _bll = new ThongKe_BLL();

        // 2. Đổi List tạm thành List DTO thật để lưu dữ liệu xuất file Word
        private List<ThongKeSanPham_DTO> _fullTopProducts = new List<ThongKeSanPham_DTO>();
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
            SetupBieuDoTopSanPham();
            cboThoiGian.SelectedIndex = 0; // Mặc định chọn "Tùy chọn" -> Sẽ tự trigger NapDuLieuDashboard() qua sự kiện cboThoiGian_SelectedIndexChanged
        }

        private void UcThongKe_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_isFirstLoad)
            {
                NapDuLieuDashboard();
            }
            _isFirstLoad = false;
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

            // Đã ẩn series Lợi nhuận trên biểu đồ vì cần query phức tạp theo ngày, 
            // hiện tại BLL chỉ hỗ trợ tính Doanh Thu theo ngày trên chart.
            chartDoanhThu.Series.Add(seriesDT);
        }

        private void SetupBieuDoTopSanPham()
        {
            // Thiết lập các cột cho DataGridView Top Sản Phẩm
            dgvTopProduct.Columns.Clear();
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

            dgvTopProduct.BorderStyle = BorderStyle.None;
            dgvTopProduct.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvTopProduct.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTopProduct.BackgroundColor = Color.White;
            dgvTopProduct.EnableHeadersVisualStyles = false;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvTopProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvTopProduct.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvTopProduct.DefaultCellStyle.SelectionForeColor = Color.Black;

            foreach (DataGridViewColumn col in dgvTopProduct.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            dgvTopProduct.SelectionChanged += (s, e) => dgvTopProduct.ClearSelection();
        }

        private void NapDuLieuDashboard()
        {
            DateTime from = dtTuNgay.Value.Date;
            DateTime to = dtDenNgay.Value.Date;

            // 1. NẠP DỮ LIỆU TỪ BLL
            var dsDoanhThu = _bll.GetDoanhThuTheoNgay(from, to);
            // Lấy top 20 sản phẩm để hiển thị lưới, lấy 10000 để tính tổng lợi nhuận toàn cục
            _fullTopProducts = _bll.GetTopSanPham(from, to, 20);
            var dsAllSpForProfit = _bll.GetTopSanPham(from, to, 10000);

            // 2. TÍNH TOÁN CÁC THẺ KPI TRÊN CÙNG
            long tongDoanhThu = 0;
            int tongDonHang = 0;
            long tongLoiNhuan = 0;

            foreach (var item in dsDoanhThu)
            {
                tongDoanhThu += item.DoanhThu;
                tongDonHang += item.SoHoaDon;
            }

            foreach (var sp in dsAllSpForProfit)
            {
                tongLoiNhuan += sp.LoiNhuan;
            }

            long tbDonHang = tongDonHang > 0 ? tongDoanhThu / tongDonHang : 0;

            lblDoanhThu.Text = $"{tongDoanhThu:N0} VNĐ";
            lblLoiNhuan.Text = $"{tongLoiNhuan:N0} VNĐ";
            lblTongDonHang.Text = $"{tongDonHang:N0} Hóa đơn";
            lblGiaTriTrungBinh.Text = $"{tbDonHang:N0} VNĐ";

            // 3. VẼ LẠI BIỂU ĐỒ CHART THỰC TẾ
            chartDoanhThu.Series["Doanh thu"].Points.Clear();
            foreach (var item in dsDoanhThu)
            {
                // Ngay của DTO hiện trả về dạng "dd/MM"
                chartDoanhThu.Series["Doanh thu"].Points.AddXY(item.Ngay, item.DoanhThu);
            }

            // 4. ĐỔ DỮ LIỆU THỰC VÀO GRID TOP SẢN PHẨM (Giữ nguyên logic giới hạn 12 dòng hiển thị đẹp)
            dgvTopProduct.Rows.Clear();
            int limit = 12;

            if (_fullTopProducts.Count > limit)
            {
                // Lấy 9 sản phẩm đầu
                for (int i = 0; i < 9; i++)
                {
                    var sp = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), sp.TenSP, sp.SoLuongBan.ToString("N0"), string.Format("{0:N0} đ", sp.DoanhThu));
                }

                // Dòng ba chấm
                int idx = dgvTopProduct.Rows.Add("...", "...", "...", "...");
                dgvTopProduct.Rows[idx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTopProduct.Rows[idx].DefaultCellStyle.ForeColor = Color.Gray;

                // Lấy 3 sản phẩm cuối
                for (int i = _fullTopProducts.Count - 3; i < _fullTopProducts.Count; i++)
                {
                    var sp = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), sp.TenSP, sp.SoLuongBan.ToString("N0"), string.Format("{0:N0} đ", sp.DoanhThu));
                }
            }
            else
            {
                // Ít hơn limit thì hiển thị hết
                for (int i = 0; i < _fullTopProducts.Count; i++)
                {
                    var sp = _fullTopProducts[i];
                    dgvTopProduct.Rows.Add((i + 1).ToString(), sp.TenSP, sp.SoLuongBan.ToString("N0"), string.Format("{0:N0} đ", sp.DoanhThu));
                }
            }
        }

        private void cboThoiGian_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            switch (cboThoiGian.SelectedIndex)
            {
                case 0:
                    break; // Tùy chỉnh (Không tự đổi ngày)
                case 1: // Tuần này
                    int offset = today.DayOfWeek - DayOfWeek.Monday;
                    if (offset < 0) offset += 7;
                    dtTuNgay.Value = today.AddDays(-offset);
                    dtDenNgay.Value = today;
                    break;
                case 2: // Tháng này
                    dtTuNgay.Value = new DateTime(today.Year, today.Month, 1);
                    dtDenNgay.Value = today;
                    break;
                case 3: // Năm nay
                    dtTuNgay.Value = new DateTime(today.Year, 1, 1);
                    dtDenNgay.Value = today;
                    break;
            }
            // Gọi lọc dữ liệu luôn khi chọn Combo
            NapDuLieuDashboard();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            NapDuLieuDashboard();
        }

        // =========================================================================
        // PHẦN XUẤT FILE GIỮ NGUYÊN (Chỉ map lại Data thật cho Word)
        // =========================================================================

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
                        System.Text.StringBuilder html = new System.Text.StringBuilder();
                        html.Append("<html><head><meta charset='utf-8'></head><body>");
                        html.Append("<h2 style='text-align:center; font-family: Arial;'>CHI TIẾT TOP SẢN PHẨM BÁN CHẠY</h2>");
                        html.Append("<p style='text-align:center; font-family: Arial;'>Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</p>");

                        html.Append("<table border='1' style='width:100%; border-collapse:collapse; font-family: Arial;'>");
                        html.Append("<tr style='background-color:#f2f2f2;'><th>STT</th><th>Tên Sản Phẩm</th><th>Số Lượng Bán</th><th>Doanh Thu</th><th>Lợi Nhuận</th></tr>");

                        int stt = 1;
                        foreach (var item in _fullTopProducts) // Dùng list thật
                        {
                            html.Append($"<tr><td style='text-align:center;'>{stt++}</td>");
                            html.Append($"<td>{item.TenSP}</td>");
                            html.Append($"<td style='text-align:center;'>{item.SoLuongBan:N0}</td>");
                            html.Append($"<td style='text-align:right;'>{item.DoanhThu:N0} đ</td>");
                            html.Append($"<td style='text-align:right;'>{item.LoiNhuan:N0} đ</td></tr>");
                        }

                        html.Append("</table></body></html>");
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

        private void dgvTopProduct_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.None) dgv.ClearSelection();
        }
    }
}