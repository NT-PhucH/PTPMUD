// ===================================================
// File: ucHoaDon.cs
// Đặt vào: GUI > ThuNganGUI > HoaDon
// ===================================================
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don
{
    public class ucHoaDon : UserControl
    {
        // ── CHỈ NHẬN ĐÚNG 1 KHAY DỮ LIỆU TỪ BLL TRUYỀN XUỐNG ──────────────────
        public HoaDonIn_DTO DuLieu { get; set; }

        private Panel _scroll;
        private Panel _paper;

        public ucHoaDon()
        {
            BackColor = Color.FromArgb(200, 200, 200);
            BuildUI();
        }

        private void BuildUI()
        {
            _scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.FromArgb(200, 200, 200) };
            _paper = new Panel { Width = 302, BackColor = Color.White, Location = new Point(5, 5) };

            _paper.Paint += Paper_Paint;
            _scroll.Controls.Add(_paper);
            Controls.Add(_scroll);

            _scroll.Resize += (s, e) => {
                int cx = (_scroll.ClientSize.Width - _paper.Width) / 2;
                _paper.Left = Math.Max(5, cx);
                CapNhat();
            };
        }

        public void CapNhat()
        {
            if (DuLieu == null) return;

            int chieuCaoMoi;
            using (Bitmap nhap = new Bitmap(1, 1))
            using (Graphics gNhap = Graphics.FromImage(nhap))
            {
                chieuCaoMoi = VeHoaDonChung(gNhap, 5, 8, _paper.Width - 10);
            }

            _paper.Height = Math.Max(chieuCaoMoi + 20, _scroll.ClientSize.Height);
            _scroll.AutoScrollMinSize = new Size(_paper.Width + 10, _paper.Height + 10);
            _paper.Invalidate();
        }

        public int VeHoaDonChung(Graphics g, int startX, int startY, int width)
        {
            if (DuLieu == null) return startY;

            int px = startX; int py = startY; int pw = width;

            var fTitle = new Font("Courier New", 9f, FontStyle.Bold);
            var fBold = new Font("Courier New", 8f, FontStyle.Bold);
            var fNormal = new Font("Courier New", 7.5f);
            var fSmall = new Font("Courier New", 7f);
            var fItalic = new Font("Courier New", 7f, FontStyle.Italic);

            void Ctr(string t, Font f) { var sz = g.MeasureString(t, f); g.DrawString(t, f, Brushes.Black, px + (pw - sz.Width) / 2, py); py += (int)sz.Height + 1; }
            void Lft(string t, Font f) { g.DrawString(t, f, Brushes.Black, px, py); py += (int)g.MeasureString("A", f).Height + 2; }
            void Row(string left, string right, Font f) { g.DrawString(left, f, Brushes.Black, px, py); var rs = g.MeasureString(right, f); g.DrawString(right, f, Brushes.Black, px + pw - rs.Width, py); py += (int)g.MeasureString("A", f).Height + 2; }
            void Eq() { Lft(new string('=', 40), fNormal); }
            void Dsh() { Lft(new string('-', 40), fNormal); }
            void Gap(int h = 4) { py += h; }

            // ── HEADER ────────────────────────────────────────────────────────
            Ctr((DuLieu.CauHinh?.TenCuaHang ?? "TÊN CỬA HÀNG").ToUpper(), fTitle);
            Ctr("HÓA ĐƠN BÁN HÀNG", fBold);
            Eq();

            Lft($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm} - Số HĐ: {(string.IsNullOrEmpty(DuLieu.MaHoaDon) ? "------" : DuLieu.MaHoaDon)}", fNormal);

            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.DiaChi))
                foreach (var wl in Wrap($"Đ/c: {DuLieu.CauHinh.DiaChi}", fNormal, g, pw))
                { g.DrawString(wl, fNormal, Brushes.Black, px, py); py += 13; }

            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.HotlineShip)) Lft($"Hotline ship hàng: {DuLieu.CauHinh.HotlineShip}", fNormal);
            if (!string.IsNullOrEmpty(DuLieu.TenThuNgan)) Lft($"Thu Ngân: {DuLieu.TenThuNgan} ({DuLieu.MaThuNgan})", fNormal);

            Gap(2); Ctr("******** HÓA ĐƠN ********", fBold); Eq();

            // ── BẢNG SẢN PHẨM ────────────────────────────────────────────────
            g.DrawString("Tên Hàng", fBold, Brushes.Black, px, py);
            g.DrawString("SL", fBold, Brushes.Black, px + 170, py);
            g.DrawString("Đơn giá", fBold, Brushes.Black, px + 195, py);
            var thStr = g.MeasureString("T.Tiền", fBold);
            g.DrawString("T.Tiền", fBold, Brushes.Black, px + pw - thStr.Width, py);
            py += 14;

            if (DuLieu.DanhSachSP == null || DuLieu.DanhSachSP.Count == 0)
            {
                Ctr("(Chưa có sản phẩm)", fItalic);
            }
            else
            {
                foreach (var sp in DuLieu.DanhSachSP)
                {
                    var wrappedTen = Wrap(sp.TenSP, fNormal, g, 165);
                    for (int i = 0; i < wrappedTen.Count; i++)
                    { g.DrawString(wrappedTen[i], fNormal, Brushes.Black, px, py); py += 13; }

                    g.DrawString(sp.SoLuong.ToString(), fNormal, Brushes.Black, px + 170, py);
                    g.DrawString($"{sp.DonGia:N0}", fNormal, Brushes.Black, px + 190, py);
                    string tienStr = $"{sp.ThanhTien:N0}";
                    var tsz = g.MeasureString(tienStr, fNormal);
                    g.DrawString(tienStr, fNormal, Brushes.Black, px + pw - tsz.Width, py);
                    py += 14;
                }
            }
            Eq();

            // ── TỔNG TIỀN ─────────────────────────────────────────────────────
            int tongSL = 0;
            if (DuLieu.DanhSachSP != null) foreach (var sp in DuLieu.DanhSachSP) tongSL += sp.SoLuong;

            Row("Tổng số lượng:", tongSL.ToString(), fNormal);
            if ((DuLieu.CauHinh?.VAT ?? 0) > 0) Row($"VAT {DuLieu.CauHinh.VAT}%:", $"{DuLieu.TienVAT:N0}", fNormal);

            Row("Tổng tiền (Đã bao gồm VAT):", $"{DuLieu.TongTienSau:N0}", fBold);
            Row($"+ {DuLieu.PhuongThucTT}:", $"{DuLieu.TienKhachDua:N0}", fNormal);

            if (!DuLieu.AnTienThua) Row("Tiền trả lại KH (VND):", $"{DuLieu.TienThua:N0}", fNormal);
            Eq();

            // ── KHÁCH HÀNG ────────────────────────────────────────────────────
            Lft($"Tên khách: {DuLieu.TenKhach}", fNormal);
            Lft($"Mã KH: {DuLieu.MaKhach}", fNormal);
            Lft($"Tổng điểm tích lũy: {DuLieu.DiemTichLuy}", fNormal);
            Dsh(); Gap(4);

            // ── FOOTER ────────────────────────────────────────────────────────
            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.FooterHoaDon))
            {
                foreach (var line in DuLieu.CauHinh.FooterHoaDon.Split('\n'))
                {
                    string t = line.Trim();
                    if (string.IsNullOrEmpty(t)) { Gap(5); continue; }
                    foreach (var wl in Wrap(t, fItalic, g, pw)) Ctr(wl, fItalic);
                }
            }
            Gap(15); return py;
        }

        private void Paper_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            VeHoaDonChung(e.Graphics, 5, 8, _paper.Width - 10);
        }

        private List<string> Wrap(string text, Font f, Graphics g, int maxW)
        {
            var result = new List<string>();
            var words = text.Split(' '); string line = "";
            foreach (var w in words)
            {
                string test = string.IsNullOrEmpty(line) ? w : line + " " + w;
                if (g.MeasureString(test, f).Width > maxW) { result.Add(line); line = w; } else line = test;
            }
            if (!string.IsNullOrEmpty(line)) result.Add(line);
            if (result.Count == 0) result.Add("");
            return result;
        }

        public void InHoaDon()
        {
            var pd = new PrintDocument();
            int chieuCaoThucTe = 0;
            using (Bitmap nhap = new Bitmap(1, 1))
            using (Graphics gNhap = Graphics.FromImage(nhap))
            {
                chieuCaoThucTe = VeHoaDonChung(gNhap, 8, 8, 315 - 16);
            }

            pd.DefaultPageSettings.PaperSize = new PaperSize("K80", 315, chieuCaoThucTe + 20);
            pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);
            pd.PrintPage += (s, pe) => VeHoaDonChung(pe.Graphics, pe.MarginBounds.Left, pe.MarginBounds.Top, pe.MarginBounds.Width);

            var ppd = new PrintPreviewDialog { Document = pd, Width = 450, Height = 800, Text = "Preview hóa đơn - Nhấn Print để in" };
            ppd.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ucHoaDon";
            this.ResumeLayout(false);
        }
    }
}