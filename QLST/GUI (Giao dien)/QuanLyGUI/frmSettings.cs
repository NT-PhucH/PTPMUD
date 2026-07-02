// ===================================================
// File: frmSettings.cs (Phiên bản Chuẩn 3-Tier + VietQR)
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using QLST.BLL__Bat_ngoai_le_.Core; // Bổ sung thư viện gọi BLL
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmSettings : Form
    {
        // 1. CHỈ KHỞI TẠO BLL, KHÔNG KHỞI TẠO DAL TẠI ĐÂY
        private readonly Setting_BLL _bll = new Setting_BLL();
        private Setting_DTO _ts;

        // Controls Tab 1
        private TextBox txtTenCH, txtDiaChi, txtSDT, txtHotlineShip, txtEmail;

        // Controls Tab 2 (Thêm 3 Control cho VietQR)
        private NumericUpDown nudVAT, nudDiem, nudHetHang, nudHetHan;
        private ComboBox cbNganHang;
        private TextBox txtSoTaiKhoan, txtTenTaiKhoan;

        // Controls Tab 3
        private TextBox txtFooter;
        private Panel panelPreview;

        // Ghi chú: Đã xóa _cache tĩnh tại form vì Setting_BLL đã tự động quản lý

        public frmSettings()
        {
            BuildUI();
            LoadSettings();
        }

        // ══════════════════════════════════════════════════════════════════════
        // BUILD UI
        // ══════════════════════════════════════════════════════════════════════
        private void BuildUI()
        {
            Text = "Cài đặt hệ thống";
            Size = new Size(950, 700); // Tăng chút chiều cao để chứa VietQR
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label { Text = "⚙️  CÀI ĐẶT HỆ THỐNG", ForeColor = Color.White, Font = new Font("Segoe UI", 13f, FontStyle.Bold), Location = new Point(15, 13), AutoSize = true });

            // Tab
            var tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10f, FontStyle.Bold), Padding = new Point(14, 6) };
            tab.TabPages.Add(BuildTabCuaHang());
            tab.TabPages.Add(BuildTabTaiChinh());
            tab.TabPages.Add(BuildTabHoaDon());

            // Bottom
            var pBot = new Panel { Dock = DockStyle.Bottom, Height = 55, BackColor = Color.White };
            var btnLuu = MakeBtn("💾 Lưu cài đặt", 15, 11, 155, Color.FromArgb(34, 139, 34));
            btnLuu.Click += BtnLuu_Click;

            var btnReset = MakeBtn("🔄 Tải lại", 180, 11, 100, Color.FromArgb(100, 100, 100));
            btnReset.Click += (s, e) => LoadSettings();

            pBot.Controls.AddRange(new Control[] { btnLuu, btnReset });

            Controls.Add(tab);
            Controls.Add(pBot);
            Controls.Add(header);
        }

        private TabPage BuildTabCuaHang()
        {
            var tab = new TabPage("  🏪 Cửa hàng  ");
            tab.BackColor = Color.White;
            int y = 20;

            tab.Controls.Add(MakeBold("Thông tin cửa hàng", 20, y));

            y += 40;
            tab.Controls.Add(MakeLabel("Tên cửa hàng: *", 20, y));
            txtTenCH = MakeTxt(20, y + 22, 870); tab.Controls.Add(txtTenCH);

            y += 65;
            tab.Controls.Add(MakeLabel("Địa chỉ:", 20, y));
            txtDiaChi = MakeTxt(20, y + 22, 870); tab.Controls.Add(txtDiaChi);

            y += 65;
            tab.Controls.Add(MakeLabel("Số điện thoại chính:", 20, y));
            txtSDT = MakeTxt(20, y + 22, 350); tab.Controls.Add(txtSDT);

            tab.Controls.Add(MakeLabel("Hotline ship hàng:", 390, y));
            txtHotlineShip = MakeTxt(390, y + 22, 350); tab.Controls.Add(txtHotlineShip);

            y += 65;
            tab.Controls.Add(MakeLabel("Email:", 20, y));
            txtEmail = MakeTxt(20, y + 22, 400); tab.Controls.Add(txtEmail);

            return tab;
        }

        private TabPage BuildTabTaiChinh()
        {
            var tab = new TabPage("  💰 Tài chính & Kho  ");
            tab.BackColor = Color.White;
            int y = 20;

            // VAT
            tab.Controls.Add(MakeBold("Thuế VAT", 20, y));
            y += 38;
            tab.Controls.Add(MakeLabel("VAT (%):", 20, y));
            nudVAT = MakeNud(120, y, 0, 100, 8);
            tab.Controls.Add(nudVAT);
            tab.Controls.Add(MakeNote("Nhập 0 = không tính VAT. Ví dụ: 8 = 8%", 200, y + 3));

            // Tích điểm
            y += 55;
            tab.Controls.Add(MakeBold("Tích điểm khách hàng", 20, y));
            y += 38;
            tab.Controls.Add(MakeLabel("Cứ 10.000đ được:", 20, y));
            nudDiem = MakeNud(160, y, 0, 100, 1);
            tab.Controls.Add(nudDiem);
            tab.Controls.Add(MakeLabel("điểm", 220, y + 3));
            tab.Controls.Add(MakeNote("Ví dụ: 1 = 10.000đ → 1 điểm  |  2 = 10.000đ → 2 điểm", 260, y + 3));

            // Cảnh báo kho
            y += 55;
            tab.Controls.Add(MakeBold("Cảnh báo kho hàng", 20, y));
            y += 38;
            tab.Controls.Add(MakeLabel("Cảnh báo hàng sắp hết khi tồn kho ≤", 20, y));
            nudHetHang = MakeNud(265, y, 1, 9999, 10);
            tab.Controls.Add(nudHetHang);
            tab.Controls.Add(MakeLabel("sản phẩm", 325, y + 3));

            y += 48;
            tab.Controls.Add(MakeLabel("Cảnh báo hàng sắp hết hạn khi còn ≤", 20, y));
            nudHetHan = MakeNud(265, y, 1, 365, 30);
            tab.Controls.Add(nudHetHan);
            tab.Controls.Add(MakeLabel("ngày", 325, y + 3));

            // ====== BỔ SUNG GIAO DIỆN VIETQR ======
            y += 55;
            tab.Controls.Add(MakeBold("Thông tin nhận chuyển khoản (VietQR)", 20, y));
            y += 38;

            tab.Controls.Add(MakeLabel("Ngân hàng:", 20, y));
            cbNganHang = new ComboBox { Location = new Point(120, y), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f) };
            cbNganHang.Items.AddRange(new string[] { "Vietcombank", "MBBank", "Techcombank", "TPBank" });
            tab.Controls.Add(cbNganHang);

            tab.Controls.Add(MakeLabel("Số TK:", 280, y));
            txtSoTaiKhoan = MakeTxt(330, y - 2, 180);
            tab.Controls.Add(txtSoTaiKhoan);

            tab.Controls.Add(MakeLabel("Tên TK:", 530, y));
            txtTenTaiKhoan = MakeTxt(590, y - 2, 200);
            tab.Controls.Add(txtTenTaiKhoan);
            // ======================================

            // Ghi chú liên kết
            y += 60;
            var pNote = new Panel { Location = new Point(20, y), Width = 870, Height = 85, BackColor = Color.FromArgb(235, 245, 255), BorderStyle = BorderStyle.FixedSingle };
            pNote.Controls.Add(new Label
            {
                Text = "ℹ️  Các tham số này được sử dụng bởi:\n" +
                       "• VAT (%)          →  FormThuNgan  khi tính tổng tiền hóa đơn\n" +
                       "• Điểm / 10.000đ  →  frmQuanLyKhachHang  khi hiển thị lịch sử tích điểm\n" +
                       "• Ngưỡng hết hàng / hết hạn  →  frmQuanLyKho tab ⚠️ Cảnh báo\n" +
                       "• VietQR  →  FormThuNgan tự động tạo mã quét",
                Location = new Point(10, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(30, 60, 120)
            });
            tab.Controls.Add(pNote);

            return tab;
        }

        private TabPage BuildTabHoaDon()
        {
            var tab = new TabPage("  🖨️ Hóa đơn  ");
            tab.BackColor = Color.FromArgb(245, 247, 250);

            var pLeft = new Panel { Location = new Point(5, 5), Width = 370, BackColor = Color.White, Padding = new Padding(15) };
            pLeft.Height = 535;
            int y = 15;
            pLeft.Controls.Add(MakeBold("Nội dung chân hóa đơn:", 15, y));

            y += 30;
            txtFooter = new TextBox { Location = new Point(15, y), Width = 330, Height = 230, Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Courier New", 8.5f) };
            pLeft.Controls.Add(txtFooter);

            y += 245;
            var btnCapNhat = MakeBtn("🔄 Cập nhật preview", 15, y, 210, Color.FromArgb(100, 120, 180));
            btnCapNhat.Click += (s, e) => panelPreview?.Invalidate();
            pLeft.Controls.Add(btnCapNhat);

            y += 48;
            var btnIn = MakeBtn("🖨️ In thử", 15, y, 150, Color.FromArgb(30, 40, 60));
            btnIn.Click += BtnInThu_Click;
            pLeft.Controls.Add(btnIn);

            y += 48;
            pLeft.Controls.Add(new Label { Text = "* Khổ giấy: 80mm (K80)\n* Font in nhiệt: Courier New 8pt", Location = new Point(15, y), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8f, FontStyle.Italic) });

            var pRight = new Panel { Location = new Point(385, 5), BackColor = Color.FromArgb(180, 180, 180) };
            pRight.Width = 530; pRight.Height = 535;

            pRight.Controls.Add(new Label { Text = "|←————————— 80mm ————————→|", Location = new Point(5, 510), AutoSize = true, ForeColor = Color.DimGray, Font = new Font("Courier New", 7.5f) });

            var scroll = new Panel { Location = new Point(10, 8), Width = 320, Height = 498, AutoScroll = true, BackColor = Color.FromArgb(180, 180, 180) };
            panelPreview = new Panel { Width = 302, Height = 750, BackColor = Color.White };
            panelPreview.Paint += PanelPreview_Paint;
            scroll.Controls.Add(panelPreview);
            pRight.Controls.Add(scroll);

            tab.Controls.Add(pLeft);
            tab.Controls.Add(pRight);
            tab.Resize += (s, e) => {
                pLeft.Height = tab.Height - 10;
                pRight.Height = tab.Height - 10;
                scroll.Height = tab.Height - 25;
            };
            return tab;
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD / LƯU QUA BLL
        // ══════════════════════════════════════════════════════════════════════
        private void LoadSettings()
        {
            try
            {
                // 2. Tải cấu hình thông qua BLL để có cache chung toàn phần mềm
                _ts = _bll.GetCauHinh();

                txtTenCH.Text = _ts.TenCuaHang;
                txtDiaChi.Text = _ts.DiaChi;
                txtSDT.Text = _ts.SoDienThoai;
                txtHotlineShip.Text = _ts.HotlineShip;
                txtEmail.Text = _ts.Email;

                nudVAT.Value = _ts.VAT;
                nudDiem.Value = _ts.DiemPer10K;
                nudHetHang.Value = _ts.NguongHetHang;
                nudHetHan.Value = _ts.NguongHetHan;
                txtFooter.Text = _ts.FooterHoaDon;

                // Load dữ liệu VietQR
                cbNganHang.Text = string.IsNullOrEmpty(_ts.NganHang) ? "Vietcombank" : _ts.NganHang;
                txtSoTaiKhoan.Text = _ts.SoTaiKhoan;
                txtTenTaiKhoan.Text = _ts.TenTaiKhoan;

                panelPreview?.Invalidate();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải cài đặt: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // Gán giá trị vào đối tượng
            _ts.TenCuaHang = txtTenCH.Text.Trim();
            _ts.DiaChi = txtDiaChi.Text.Trim();
            _ts.SoDienThoai = txtSDT.Text.Trim();
            _ts.HotlineShip = txtHotlineShip.Text.Trim();
            _ts.Email = txtEmail.Text.Trim();
            _ts.VAT = (int)nudVAT.Value;
            _ts.DiemPer10K = (int)nudDiem.Value;
            _ts.NguongHetHang = (int)nudHetHang.Value;
            _ts.NguongHetHan = (int)nudHetHan.Value;
            _ts.FooterHoaDon = txtFooter.Text;

            // Gán dữ liệu VietQR
            _ts.NganHang = cbNganHang.Text;
            _ts.SoTaiKhoan = txtSoTaiKhoan.Text.Trim();
            _ts.TenTaiKhoan = txtTenTaiKhoan.Text.Trim();

            // 3. Đẩy toàn bộ kiểm tra và thao tác cho tầng BLL
            var (ok, msg) = _bll.CapNhat(_ts);

            panelPreview?.Invalidate();
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        // ══════════════════════════════════════════════════════════════════════
        // PREVIEW HÓA ĐƠN 80MM & IN THỬ (GIỮ NGUYÊN)
        // ══════════════════════════════════════════════════════════════════════
        private void PanelPreview_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            var fTitle = new Font("Courier New", 9f, FontStyle.Bold);
            var fBold = new Font("Courier New", 8f, FontStyle.Bold);
            var fNormal = new Font("Courier New", 7.5f);
            var fSmall = new Font("Courier New", 7f, FontStyle.Italic);

            int pw = panelPreview.Width - 10;
            int px = 5;
            int py = 8;

            void Ctr(string t, Font f) { var s = g.MeasureString(t, f); g.DrawString(t, f, Brushes.Black, px + (pw - s.Width) / 2, py); py += (int)s.Height + 1; }
            void Lft(string t, Font f) { g.DrawString(t, f, Brushes.Black, px, py); py += (int)g.MeasureString("A", f).Height + 2; }
            void Rgt(string left, string right, Font f) { g.DrawString(left, f, Brushes.Black, px, py); var rs = g.MeasureString(right, f); g.DrawString(right, f, Brushes.Black, px + pw - rs.Width, py); py += (int)g.MeasureString("A", f).Height + 2; }
            void Eq() { Lft(new string('=', 40), fNormal); }
            void Dsh() { Lft(new string('-', 40), fNormal); }

            string tenCH = txtTenCH?.Text ?? "TÊN CỬA HÀNG";
            string diaChi = txtDiaChi?.Text ?? "";
            string sdt = txtSDT?.Text ?? "";
            string hotShip = txtHotlineShip?.Text ?? "";
            string vat = nudVAT?.Value.ToString() ?? "0";
            string footer = txtFooter?.Text ?? "";

            Ctr(tenCH.ToUpper(), fTitle);
            Ctr("HÓA ĐƠN BÁN HÀNG", fBold);
            Eq();
            Lft($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm} - Số HĐ: HD------", fNormal);
            foreach (var l in WrapText($"Đ/c: {diaChi}", fNormal, g, pw)) { g.DrawString(l, fNormal, Brushes.Black, px, py); py += 13; }
            if (!string.IsNullOrEmpty(hotShip)) Lft($"Hotline ship hàng: {hotShip}", fNormal);
            Lft("Thu Ngân: [Tên thu ngân]", fNormal);
            Ctr("********BẢN CHÍNH********", fBold);
            Eq();

            Lft("Tên Hàng               SL  Đơn giá T.Tiền", fBold);
            Dsh();
            Lft("SP000001 - Mì tôm xào khô Goreng", fNormal);
            Lft("vị đặc biệt 85g T40 - (gói)", fNormal);
            Rgt("", "5  5,556  27,778", fNormal);
            Eq();

            Rgt("Tổng số lượng:", "5", fNormal);
            if (vat != "0") Rgt($"VAT {vat}%:", "2,222", fNormal);
            Rgt("Tổng tiền (Đã bao gồm VAT):", "30,000", fBold);
            Eq();

            Lft("Tên khách: Khách lẻ", fNormal);
            Lft("Tổng điểm tích lũy: 0", fNormal);
            Dsh();

            py += 4;
            foreach (var line in footer.Split('\n'))
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) { py += 6; continue; }
                foreach (var wl in WrapText(trimmed, fSmall, g, pw))
                {
                    var sz = g.MeasureString(wl, fSmall);
                    g.DrawString(wl, fSmall, Brushes.Black, px + (pw - sz.Width) / 2, py);
                    py += 12;
                }
            }
            py += 10;
            panelPreview.Height = Math.Max(py + 20, 600);
        }

        private void BtnInThu_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = new PaperSize("K80", 315, 1100);
            pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);

            pd.PrintPage += (ps, pe) => {
                var g = pe.Graphics;
                var fT = new Font("Courier New", 9f, FontStyle.Bold);
                var fB = new Font("Courier New", 8f, FontStyle.Bold);
                var fN = new Font("Courier New", 7.5f);
                var fS = new Font("Courier New", 7f, FontStyle.Italic);

                int lx = pe.MarginBounds.Left;
                int ly = pe.MarginBounds.Top;
                int lw = pe.MarginBounds.Width;

                void C(string t, Font f) { var sz = g.MeasureString(t, f); g.DrawString(t, f, Brushes.Black, lx + (lw - sz.Width) / 2, ly); ly += (int)sz.Height + 1; }
                void L(string t, Font f) { g.DrawString(t, f, Brushes.Black, lx, ly); ly += (int)g.MeasureString("A", f).Height + 1; }
                void Eq2() { L(new string('=', 42), fN); }

                C((_ts?.TenCuaHang ?? "").ToUpper(), fT);
                C("HÓA ĐƠN BÁN HÀNG", fB);
                Eq2();
                L($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fN);
                L($"Đ/c: {_ts?.DiaChi}", fN);
                L($"Hotline ship: {_ts?.HotlineShip}", fN);
                L("Thu Ngân: [Tên thu ngân]", fN);
                Eq2();
                L("(Đây là bản in thử - nội dung SP sẽ do FormThuNgan điền)", fN);
                Eq2();
                foreach (var line in (txtFooter.Text ?? "").Split('\n')) C(line.Trim(), fS);
            };

            var ppd = new PrintPreviewDialog { Document = pd, Width = 420, Height = 750 };
            ppd.ShowDialog();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private List<string> WrapText(string text, Font f, Graphics g, int maxW)
        {
            var result = new List<string>();
            var words = text.Split(' ');
            string line = "";
            foreach (var wd in words)
            {
                string test = string.IsNullOrEmpty(line) ? wd : line + " " + wd;
                if (g.MeasureString(test, f).Width > maxW) { result.Add(line); line = wd; }
                else line = test;
            }
            if (!string.IsNullOrEmpty(line)) result.Add(line);
            return result;
        }

        private Label MakeLabel(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y), AutoSize = true };
        private Label MakeBold(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 60) };
        private Label MakeNote(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic) };
        private TextBox MakeTxt(int x, int y, int w) => new TextBox { Location = new Point(x, y), Width = w };

        private NumericUpDown MakeNud(int x, int y, int min, int max, int val) => new NumericUpDown
        {
            Location = new Point(x, y),
            Width = 70,
            Minimum = min,
            Maximum = max,
            Value = val,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold)
        };

        private Button MakeBtn(string t, int x, int y, int w, Color c) => new Button
        {
            Text = t,
            Location = new Point(x, y),
            Width = w,
            Height = 34,
            BackColor = c,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
    }
}