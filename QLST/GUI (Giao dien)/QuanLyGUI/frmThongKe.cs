// ===================================================
// File: frmThongKe.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmThongKe : Form
    {
        private readonly ThongKe_BLL _bll = new ThongKe_BLL();

        private TabControl tabMain;

        // Tab 1 - Tổng quan
        private Label lblDTHomNay, lblDTTuan, lblDTThang, lblHDHomNay, lblSoSP, lblSoKH;

        // Tab 2 - Doanh thu
        private DateTimePicker dtpFrom, dtpTo;
        private RadioButton rdoNgay, rdoThang;
        private NumericUpDown nudNam;
        private DataGridView dgvDoanhThu;
        private Panel panelChart;
        private List<ThongKeDoanhThu_DTO> _chartData = new List<ThongKeDoanhThu_DTO>();

        // Tab 3 - Top sản phẩm
        private DateTimePicker dtpFrom3, dtpTo3;
        private NumericUpDown nudTop;
        private DataGridView dgvTopSP;

        public frmThongKe()
        {
            BuildUI();
            LoadTongQuan();
        }

        private void BuildUI()
        {
            Text = "Thống kê & Báo cáo";
            Size = new Size(1150, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label
            {
                Text = "📊  THỐNG KÊ & BÁO CÁO",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Location = new Point(15, 12),
                AutoSize = true
            });

            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Padding = new Point(12, 6)
            };
            tabMain.TabPages.Add(BuildTabTongQuan());
            tabMain.TabPages.Add(BuildTabDoanhThu());
            tabMain.TabPages.Add(BuildTabTopSP());

            Controls.Add(tabMain);
            Controls.Add(header);
        }

        // ── TAB 1: TỔNG QUAN ──────────────────────────────────────────────────
        private TabPage BuildTabTongQuan()
        {
            var tab = new TabPage("  📋 Tổng quan  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            var btnRefresh = new Button
            {
                Text = "🔄 Cập nhật",
                Location = new Point(15, 15),
                Width = 120,
                Height = 32,
                BackColor = Color.FromArgb(30, 100, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnRefresh.Click += (s, e) => LoadTongQuan();
            tab.Controls.Add(btnRefresh);

            // 6 card thống kê
            var cards = new (string title, string color, string field)[] {
                ("💰 Doanh thu hôm nay", "#2ecc71", "homNay"),
                ("📅 Doanh thu tuần",    "#3498db", "tuan"),
                ("📆 Doanh thu tháng",   "#9b59b6", "thang"),
                ("🧾 Hóa đơn hôm nay",  "#e67e22", "hd"),
                ("📦 Sản phẩm",          "#1abc9c", "sp"),
                ("👥 Khách hàng",        "#e74c3c", "kh")
            };

            int col = 0, row2 = 0;
            foreach (var c in cards)
            {
                var pCard = new Panel
                {
                    Location = new Point(15 + col * 185, 60 + row2 * 140),
                    Size = new Size(175, 120),
                    BackColor = ColorTranslator.FromHtml(c.color)
                };
                var lTitle = new Label
                {
                    Text = c.title,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                var lVal = new Label
                {
                    Text = "...",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                    Location = new Point(10, 45),
                    AutoSize = true,
                    Name = "lbl_" + c.field
                };
                pCard.Controls.Add(lTitle);
                pCard.Controls.Add(lVal);
                tab.Controls.Add(pCard);

                // Lưu reference
                switch (c.field)
                {
                    case "homNay": lblDTHomNay = lVal; break;
                    case "tuan": lblDTTuan = lVal; break;
                    case "thang": lblDTThang = lVal; break;
                    case "hd": lblHDHomNay = lVal; break;
                    case "sp": lblSoSP = lVal; break;
                    case "kh": lblSoKH = lVal; break;
                }

                col++;
                if (col >= 3) { col = 0; row2++; }
            }
            return tab;
        }

        // ── TAB 2: DOANH THU ──────────────────────────────────────────────────
        private TabPage BuildTabDoanhThu()
        {
            var tab = new TabPage("  📈 Doanh thu  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            // Filter panel
            var pFilter = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(10, 8, 10, 0) };

            rdoNgay = new RadioButton { Text = "Theo ngày", Location = new Point(10, 15), Checked = true, AutoSize = true };
            rdoThang = new RadioButton { Text = "Theo tháng", Location = new Point(100, 15), AutoSize = true };
            rdoNgay.CheckedChanged += (s, e) => ToggleFilterMode();
            rdoThang.CheckedChanged += (s, e) => ToggleFilterMode();
            pFilter.Controls.AddRange(new Control[] { rdoNgay, rdoThang });

            pFilter.Controls.Add(new Label { Text = "Từ:", Location = new Point(210, 15), AutoSize = true });
            dtpFrom = new DateTimePicker { Location = new Point(235, 12), Width = 120, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-30) };
            pFilter.Controls.Add(dtpFrom);

            pFilter.Controls.Add(new Label { Text = "Đến:", Location = new Point(370, 15), AutoSize = true });
            dtpTo = new DateTimePicker { Location = new Point(398, 12), Width = 120, Format = DateTimePickerFormat.Short };
            pFilter.Controls.Add(dtpTo);

            pFilter.Controls.Add(new Label { Text = "Năm:", Location = new Point(210, 15), AutoSize = true, Name = "lblNam", Visible = false });
            nudNam = new NumericUpDown { Location = new Point(245, 12), Width = 80, Minimum = 2020, Maximum = 2099, Value = DateTime.Today.Year, Visible = false };
            pFilter.Controls.Add(nudNam);

            var btnXem = new Button
            {
                Text = "📊 Xem",
                Location = new Point(540, 10),
                Width = 90,
                Height = 30,
                BackColor = Color.FromArgb(30, 100, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnXem.Click += BtnXemDoanhThu_Click;
            pFilter.Controls.Add(btnXem);

            // Chart panel (vẽ bằng GDI+)
            panelChart = new Panel
            {
                Dock = DockStyle.Top,
                Height = 260,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelChart.Paint += PanelChart_Paint;

            // DataGridView
            dgvDoanhThu = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 34,
                RowTemplate = { Height = 28 }
            };
            StyleDgv(dgvDoanhThu);
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNgay", HeaderText = "Ngày / Tháng", FillWeight = 25 });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHD", HeaderText = "Số hóa đơn", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDT", HeaderText = "Doanh thu", FillWeight = 30, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTB", HeaderText = "TB/hóa đơn", FillWeight = 25, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });

            tab.Controls.Add(dgvDoanhThu);
            tab.Controls.Add(panelChart);
            tab.Controls.Add(pFilter);
            return tab;
        }

        // ── TAB 3: TOP SẢN PHẨM ───────────────────────────────────────────────
        private TabPage BuildTabTopSP()
        {
            var tab = new TabPage("  🏆 Top sản phẩm  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            var pFilter = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(10, 8, 10, 0) };
            pFilter.Controls.Add(new Label { Text = "Từ:", Location = new Point(10, 15), AutoSize = true });
            dtpFrom3 = new DateTimePicker { Location = new Point(35, 12), Width = 120, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-30) };
            pFilter.Controls.Add(dtpFrom3);
            pFilter.Controls.Add(new Label { Text = "Đến:", Location = new Point(170, 15), AutoSize = true });
            dtpTo3 = new DateTimePicker { Location = new Point(198, 12), Width = 120, Format = DateTimePickerFormat.Short };
            pFilter.Controls.Add(dtpTo3);
            pFilter.Controls.Add(new Label { Text = "Top:", Location = new Point(335, 15), AutoSize = true });
            nudTop = new NumericUpDown { Location = new Point(362, 12), Width = 60, Minimum = 5, Maximum = 50, Value = 10 };
            pFilter.Controls.Add(nudTop);

            var btnXem = new Button
            {
                Text = "📊 Xem",
                Location = new Point(440, 10),
                Width = 90,
                Height = 30,
                BackColor = Color.FromArgb(30, 100, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnXem.Click += BtnXemTopSP_Click;
            pFilter.Controls.Add(btnXem);

            dgvTopSP = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 34,
                RowTemplate = { Height = 28 }
            };
            StyleDgv(dgvTopSP);
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSTT", HeaderText = "#", FillWeight = 5, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên sản phẩm", FillWeight = 30 });
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLoai", HeaderText = "Loại", FillWeight = 15 });
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSL", HeaderText = "Số lượng bán", FillWeight = 13, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDT", HeaderText = "Doanh thu", FillWeight = 18, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvTopSP.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLN", HeaderText = "Lợi nhuận ước", FillWeight = 19, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });

            tab.Controls.Add(dgvTopSP);
            tab.Controls.Add(pFilter);
            return tab;
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD DỮ LIỆU
        // ══════════════════════════════════════════════════════════════════════
        private void LoadTongQuan()
        {
            var tq = _bll.GetTongQuan();
            lblDTHomNay.Text = $"{tq.DoanhThuHomNay:N0}đ";
            lblDTTuan.Text = $"{tq.DoanhThuTuan:N0}đ";
            lblDTThang.Text = $"{tq.DoanhThuThang:N0}đ";
            lblHDHomNay.Text = $"{tq.SoHoaDonHomNay}";
            lblSoSP.Text = $"{tq.SoSanPham}";
            lblSoKH.Text = $"{tq.SoKhachHang}";
        }

        private void BtnXemDoanhThu_Click(object sender, EventArgs e)
        {
            List<ThongKeDoanhThu_DTO> list;
            if (rdoNgay.Checked)
                list = _bll.GetDoanhThuTheoNgay(dtpFrom.Value, dtpTo.Value);
            else
                list = _bll.GetDoanhThuTheoThang((int)nudNam.Value);

            _chartData = list;
            panelChart.Invalidate(); // Vẽ lại chart

            dgvDoanhThu.Rows.Clear();
            foreach (var d in list)
            {
                long tb = d.SoHoaDon > 0 ? d.DoanhThu / d.SoHoaDon : 0;
                dgvDoanhThu.Rows.Add(d.Ngay, d.SoHoaDon,
                    string.Format("{0:N0} đ", d.DoanhThu),
                    string.Format("{0:N0} đ", tb));
            }
        }

        private void BtnXemTopSP_Click(object sender, EventArgs e)
        {
            var list = _bll.GetTopSanPham(dtpFrom3.Value, dtpTo3.Value, (int)nudTop.Value);
            dgvTopSP.Rows.Clear();
            int stt = 1;
            foreach (var sp in list)
            {
                int idx = dgvTopSP.Rows.Add(stt++, sp.TenSP, sp.TenLoai, sp.SoLuongBan,
                    string.Format("{0:N0} đ", sp.DoanhThu),
                    string.Format("{0:N0} đ", sp.LoiNhuan));
                // Tô màu top 3
                if (stt <= 4)
                    dgvTopSP.Rows[idx].DefaultCellStyle.BackColor =
                        stt == 2 ? Color.FromArgb(255, 215, 0) :    // vàng
                        stt == 3 ? Color.FromArgb(192, 192, 192) :  // bạc
                                   Color.FromArgb(205, 127, 50);     // đồng
            }
        }

        // ── VẼ CHART GDI+ ─────────────────────────────────────────────────────
        private void PanelChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            if (_chartData == null || _chartData.Count == 0)
            {
                g.DrawString("Chưa có dữ liệu — nhấn 'Xem' để tải",
                    new Font("Segoe UI", 11f), Brushes.Gray, 20, 100);
                return;
            }

            int pad = 55, padR = 20, padT = 20, padB = 35;
            int w = panelChart.Width - pad - padR;
            int h = panelChart.Height - padT - padB;

            long maxVal = 1;
            foreach (var d in _chartData) if (d.DoanhThu > maxVal) maxVal = d.DoanhThu;

            // Vẽ lưới ngang
            var penGrid = new Pen(Color.FromArgb(220, 220, 220));
            for (int i = 0; i <= 4; i++)
            {
                int y2 = padT + (int)(h * i / 4.0);
                g.DrawLine(penGrid, pad, y2, pad + w, y2);
                long val = maxVal - maxVal * i / 4;
                g.DrawString($"{val / 1000}K", new Font("Segoe UI", 7.5f), Brushes.Gray, 0, y2 - 7);
            }

            // Vẽ cột
            int n = _chartData.Count;
            float barW = (float)w / n * 0.6f;
            float gap = (float)w / n;
            var brushBar = new SolidBrush(Color.FromArgb(30, 100, 200));

            for (int i = 0; i < n; i++)
            {
                float barH = (float)(_chartData[i].DoanhThu * h / maxVal);
                float x2 = pad + i * gap + (gap - barW) / 2;
                float y2 = padT + h - barH;
                g.FillRectangle(brushBar, x2, y2, barW, barH);

                // Label ngày
                g.DrawString(_chartData[i].Ngay,
                    new Font("Segoe UI", 7f), Brushes.DimGray,
                    x2 - 3, padT + h + 3);
            }

            // Viền trục
            var penAxis = new Pen(Color.FromArgb(150, 150, 150));
            g.DrawLine(penAxis, pad, padT, pad, padT + h);
            g.DrawLine(penAxis, pad, padT + h, pad + w, padT + h);
        }

        private void ToggleFilterMode()
        {
            bool isNgay = rdoNgay.Checked;
            dtpFrom.Visible = isNgay; dtpTo.Visible = isNgay;
            nudNam.Visible = !isNgay;
            // Label "Từ:" / "Đến:" / "Năm:"
            foreach (Control c in dtpFrom.Parent.Controls)
            {
                if (c is Label lb)
                {
                    if (lb.Text == "Từ:" || lb.Text == "Đến:") lb.Visible = isNgay;
                    if (lb.Name == "lblNam") lb.Visible = !isNgay;
                }
            }
        }

        private void StyleDgv(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
        }
    }
}