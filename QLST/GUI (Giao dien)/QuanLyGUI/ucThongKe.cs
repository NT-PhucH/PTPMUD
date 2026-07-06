using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            dgvTopProduct.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold);
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
            menu.Font = new System.Drawing.Font("Segoe UI", 10F);

            menu.Items.Add("📊 Xuất ra file Excel (.xlsx)", null, (s, ev) => ThucHienXuatFile("Excel"));

            menu.Show(btnXuatData, new Point(0, -menu.PreferredSize.Height - 10));
        }

        private void ThucHienXuatFile(string loaiFile)
        {
            // Kiểm tra có dữ liệu để xuất hay không
            if (_fullTopProducts == null || _fullTopProducts.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = $"Chọn nơi lưu file {loaiFile}";
                sfd.FileName = "BaoCaoThongKe_" + DateTime.Now.ToString("yyyyMMdd_HHmm");

                if (loaiFile == "Excel")
                    sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                else
                    sfd.Filter = "PDF Document (*.pdf)|*.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (loaiFile == "Excel")
                        {
                            XuatExcel(sfd.FileName);
                        }
                        else if (loaiFile == "PDF")
                        {
                            XuatPDF(sfd.FileName);
                        }

                        MessageBox.Show($"Đã trích xuất dữ liệu ra file {loaiFile} thành công!\n\nĐường dẫn: {sfd.FileName}",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xuất file:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                // =========================================================================
                // SHEET 1: BÁO CÁO TỔNG QUAN VÀ TOP SẢN PHẨM
                // =========================================================================
                var wsSP = workbook.Worksheets.Add("Top Sản Phẩm");

                // Ghi rõ thời gian theo format: Từ ngày dd tháng MM năm yyyy
                string thoiGianBaoCao = $"Thời gian: Từ ngày {dtTuNgay.Value:dd} tháng {dtTuNgay.Value:MM} năm {dtTuNgay.Value:yyyy} " +
                                        $"đến ngày {dtDenNgay.Value:dd} tháng {dtDenNgay.Value:MM} năm {dtDenNgay.Value:yyyy}";

                // 1. TẠO TIÊU ĐỀ & THỜI GIAN
                wsSP.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ TỔNG QUAN VÀ SẢN PHẨM";
                var titleRange = wsSP.Range(1, 1, 1, 7);
                titleRange.Merge();
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.FontSize = 15;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wsSP.Row(1).Height = 25;

                wsSP.Cell(2, 1).Value = thoiGianBaoCao;
                wsSP.Range(2, 1, 2, 7).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsSP.Range(2, 1, 2, 7).Style.Font.Italic = true;

                // 2. XUẤT CÁC CHỈ SỐ KPI TỔNG QUAN
                wsSP.Cell(4, 2).Value = "Doanh Thu:";
                wsSP.Cell(4, 2).Style.Font.Bold = true;
                wsSP.Cell(4, 3).Value = lblDoanhThu.Text;
                wsSP.Cell(4, 3).Style.Font.FontColor = XLColor.Green;

                wsSP.Cell(4, 5).Value = "Tổng Hóa Đơn:";
                wsSP.Cell(4, 5).Style.Font.Bold = true;
                wsSP.Cell(4, 6).Value = lblTongDonHang.Text;
                wsSP.Cell(4, 6).Style.Font.FontColor = XLColor.Blue;

                wsSP.Cell(5, 2).Value = "Lợi Nhuận Gộp:";
                wsSP.Cell(5, 2).Style.Font.Bold = true;
                wsSP.Cell(5, 3).Value = lblLoiNhuan.Text;
                wsSP.Cell(5, 3).Style.Font.FontColor = XLColor.DarkOrange;

                wsSP.Cell(5, 5).Value = "Trung Bình/Đơn:";
                wsSP.Cell(5, 5).Style.Font.Bold = true;
                wsSP.Cell(5, 6).Value = lblGiaTriTrungBinh.Text;
                wsSP.Cell(5, 6).Style.Font.FontColor = XLColor.Purple;

                // 3. TẠO HEADER CHO BẢNG SẢN PHẨM
                int headerRow = 8;
                string[] headers = { "STT", "Mã SP", "Tên Sản Phẩm", "Phân Loại", "Số Lượng Bán", "Doanh Thu (VNĐ)", "Lợi Nhuận (VNĐ)" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = wsSP.Cell(headerRow, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // 4. ĐỔ DỮ LIỆU TỪ LIST TOP SẢN PHẨM
                int currentRow = headerRow + 1;
                int stt = 1;
                foreach (var item in _fullTopProducts)
                {
                    wsSP.Cell(currentRow, 1).Value = stt++;
                    wsSP.Cell(currentRow, 2).Value = item.SanPhamID;
                    wsSP.Cell(currentRow, 3).Value = item.TenSP;
                    wsSP.Cell(currentRow, 4).Value = item.TenLoai;
                    wsSP.Cell(currentRow, 5).Value = item.SoLuongBan;
                    wsSP.Cell(currentRow, 6).Value = item.DoanhThu;
                    wsSP.Cell(currentRow, 7).Value = item.LoiNhuan;

                    wsSP.Range(currentRow, 1, currentRow, 7).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    wsSP.Range(currentRow, 1, currentRow, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    currentRow++;
                }
                wsSP.Column(5).Style.NumberFormat.Format = "#,##0";
                wsSP.Column(6).Style.NumberFormat.Format = "#,##0";
                wsSP.Column(7).Style.NumberFormat.Format = "#,##0";
                wsSP.Columns().AdjustToContents();

                // =========================================================================
                // SHEET 2: CHI TIẾT DOANH THU
                // =========================================================================
                var wsDT = workbook.Worksheets.Add("Doanh Thu");

                var dsDoanhThu = _bll.GetDoanhThuTheoNgay(dtTuNgay.Value.Date, dtDenNgay.Value.Date);

                wsDT.Cell(1, 1).Value = "THỐNG KÊ CHI TIẾT DOANH THU THEO NGÀY";
                var titleDTRange = wsDT.Range(1, 1, 1, 4);
                titleDTRange.Merge();
                titleDTRange.Style.Font.Bold = true;
                titleDTRange.Style.Font.FontSize = 15;
                titleDTRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleDTRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wsDT.Row(1).Height = 25;

                wsDT.Cell(2, 1).Value = thoiGianBaoCao;
                wsDT.Range(2, 1, 2, 4).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsDT.Range(2, 1, 2, 4).Style.Font.Italic = true;

                int headerDTRow = 4;
                string[] headersDT = { "STT", "Ngày", "Số Hóa Đơn", "Doanh Thu (VNĐ)" };
                for (int i = 0; i < headersDT.Length; i++)
                {
                    var cell = wsDT.Cell(headerDTRow, i + 1);
                    cell.Value = headersDT[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                int currentDTRow = headerDTRow + 1;
                int sttDT = 1;
                long sumHoaDon = 0;
                long sumDoanhThu = 0;

                foreach (var item in dsDoanhThu)
                {
                    wsDT.Cell(currentDTRow, 1).Value = sttDT++;
                    wsDT.Cell(currentDTRow, 2).Value = item.Ngay;
                    wsDT.Cell(currentDTRow, 3).Value = item.SoHoaDon;
                    wsDT.Cell(currentDTRow, 4).Value = item.DoanhThu;

                    wsDT.Range(currentDTRow, 1, currentDTRow, 4).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    wsDT.Range(currentDTRow, 1, currentDTRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    sumHoaDon += item.SoHoaDon;
                    sumDoanhThu += item.DoanhThu;
                    currentDTRow++;
                }

                // Dòng tổng cộng
                wsDT.Cell(currentDTRow, 1).Value = "TỔNG CỘNG";
                wsDT.Range(currentDTRow, 1, currentDTRow, 2).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsDT.Range(currentDTRow, 1, currentDTRow, 2).Style.Font.Bold = true;

                wsDT.Cell(currentDTRow, 3).Value = sumHoaDon;
                wsDT.Cell(currentDTRow, 3).Style.Font.Bold = true;

                wsDT.Cell(currentDTRow, 4).Value = sumDoanhThu;
                wsDT.Cell(currentDTRow, 4).Style.Font.Bold = true;

                wsDT.Range(currentDTRow, 1, currentDTRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wsDT.Column(3).Style.NumberFormat.Format = "#,##0";
                wsDT.Column(4).Style.NumberFormat.Format = "#,##0";
                wsDT.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }
        }

        private void XuatPDF(string filePath)
        {
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontSubtitle = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontItalic = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);
            iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontKPI = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                string thoiGianBaoCao = $"Thời gian: Từ ngày {dtTuNgay.Value:dd} tháng {dtTuNgay.Value:MM} năm {dtTuNgay.Value:yyyy} " +
                                        $"đến ngày {dtDenNgay.Value:dd} tháng {dtDenNgay.Value:MM} năm {dtDenNgay.Value:yyyy}";

                // =========================================================================
                // PHẦN 1: BÁO CÁO TỔNG QUAN & TOP SẢN PHẨM (TRANG 1)
                // =========================================================================
                Paragraph title = new Paragraph("BÁO CÁO THỐNG KÊ TỔNG QUAN VÀ SẢN PHẨM", fontTitle)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 5f
                };
                pdfDoc.Add(title);

                Paragraph dateRange = new Paragraph(thoiGianBaoCao, fontItalic)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateRange);

                // IN CÁC CHỈ SỐ KPI
                PdfPTable kpiTable = new PdfPTable(2);
                kpiTable.WidthPercentage = 90;
                kpiTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                kpiTable.SpacingAfter = 20f;

                PdfPCell cellDT = new PdfPCell(new Phrase("Doanh Thu: " + lblDoanhThu.Text, fontKPI)) { Border = 0, PaddingBottom = 10f };
                PdfPCell cellHD = new PdfPCell(new Phrase("Tổng Hóa Đơn: " + lblTongDonHang.Text, fontKPI)) { Border = 0, PaddingBottom = 10f };
                kpiTable.AddCell(cellDT);
                kpiTable.AddCell(cellHD);

                PdfPCell cellLN = new PdfPCell(new Phrase("Lợi Nhuận Gộp: " + lblLoiNhuan.Text, fontKPI)) { Border = 0 };
                PdfPCell cellTB = new PdfPCell(new Phrase("Trung Bình/Đơn: " + lblGiaTriTrungBinh.Text, fontKPI)) { Border = 0 };
                kpiTable.AddCell(cellLN);
                kpiTable.AddCell(cellTB);

                pdfDoc.Add(kpiTable);

                // IN BẢNG TOP SẢN PHẨM 
                PdfPTable tableSP = new PdfPTable(6);
                tableSP.WidthPercentage = 100;
                float[] widthsSP = new float[] { 5f, 35f, 15f, 10f, 17.5f, 17.5f };
                tableSP.SetWidths(widthsSP);
                tableSP.SpacingAfter = 30f;

                string[] headersSP = { "STT", "Tên Sản Phẩm", "Phân Loại", "Đã bán", "Doanh Thu (VNĐ)", "Lợi Nhuận (VNĐ)" };
                foreach (string header in headersSP)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, fontHeader))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        BackgroundColor = new BaseColor(230, 230, 230),
                        Padding = 6f
                    };
                    tableSP.AddCell(cell);
                }

                int stt = 1;
                foreach (var item in _fullTopProducts)
                {
                    tableSP.AddCell(new PdfPCell(new Phrase(stt++.ToString(), fontNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
                    tableSP.AddCell(new PdfPCell(new Phrase(item.TenSP, fontNormal)) { Padding = 5f });
                    tableSP.AddCell(new PdfPCell(new Phrase(item.TenLoai, fontNormal)) { Padding = 5f });
                    tableSP.AddCell(new PdfPCell(new Phrase(item.SoLuongBan.ToString("N0"), fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });
                    tableSP.AddCell(new PdfPCell(new Phrase(item.DoanhThu.ToString("N0"), fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });
                    tableSP.AddCell(new PdfPCell(new Phrase(item.LoiNhuan.ToString("N0"), fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });
                }
                pdfDoc.Add(tableSP);

                // =========================================================================
                // PHẦN 2: CHI TIẾT DOANH THU THEO NGÀY (SANG TRANG MỚI TÁCH BIỆT)
                // =========================================================================
                pdfDoc.NewPage(); // Lệnh này giúp ngắt trang sang trang tiếp theo

                Paragraph titleDT = new Paragraph("CHI TIẾT DOANH THU THEO NGÀY", fontSubtitle)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15f
                };
                pdfDoc.Add(titleDT);

                Paragraph dateRangeDT = new Paragraph(thoiGianBaoCao, fontItalic)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateRangeDT); // Thêm lại dòng thời gian cho trang 2 đỡ trống

                var dsDoanhThu = _bll.GetDoanhThuTheoNgay(dtTuNgay.Value.Date, dtDenNgay.Value.Date);

                PdfPTable tableDT = new PdfPTable(4);
                tableDT.WidthPercentage = 80;
                tableDT.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widthsDT = new float[] { 10f, 30f, 20f, 40f };
                tableDT.SetWidths(widthsDT);

                string[] headersDT = { "STT", "Ngày", "Số Hóa Đơn", "Doanh Thu (VNĐ)" };
                foreach (string header in headersDT)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, fontHeader))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        BackgroundColor = new BaseColor(230, 230, 230),
                        Padding = 6f
                    };
                    tableDT.AddCell(cell);
                }

                int sttDT = 1;
                long sumHD = 0;
                long sumDT = 0;

                foreach (var item in dsDoanhThu)
                {
                    tableDT.AddCell(new PdfPCell(new Phrase(sttDT++.ToString(), fontNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
                    tableDT.AddCell(new PdfPCell(new Phrase(item.Ngay, fontNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
                    tableDT.AddCell(new PdfPCell(new Phrase(item.SoHoaDon.ToString("N0"), fontNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5f });
                    tableDT.AddCell(new PdfPCell(new Phrase(item.DoanhThu.ToString("N0"), fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });

                    sumHD += item.SoHoaDon;
                    sumDT += item.DoanhThu;
                }

                // Dòng tổng cộng cuối bảng PDF
                PdfPCell cellTong = new PdfPCell(new Phrase("TỔNG CỘNG", fontHeader))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 6f,
                    BackgroundColor = new BaseColor(245, 245, 245)
                };
                tableDT.AddCell(cellTong);
                tableDT.AddCell(new PdfPCell(new Phrase(sumHD.ToString("N0"), fontHeader)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 6f, BackgroundColor = new BaseColor(245, 245, 245) });
                tableDT.AddCell(new PdfPCell(new Phrase(sumDT.ToString("N0"), fontHeader)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 6f, BackgroundColor = new BaseColor(245, 245, 245) });

                pdfDoc.Add(tableDT);
                pdfDoc.Close();
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