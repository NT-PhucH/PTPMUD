// ===================================================
// File: ucHoaDon.cs
// Đặt vào: GUI > ThuNganGUI > HoaDon
// UserControl vẽ hóa đơn 80mm, tái sử dụng được
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
        // ── Dữ liệu được set từ bên ngoài ────────────────────────────────────
        public ThamSoHeThong_DTO CauHinh { get; set; }
        public string MaHoaDon { get; set; } = "";
        public string TenThuNgan { get; set; } = "";
        public string MaThuNgan { get; set; } = "";
        public string TenKhach { get; set; } = "Khách lẻ";
        public string MaKhach { get; set; } = "";
        public int DiemTichLuy { get; set; } = 0;
        public List<DongHoaDon> DanhSachSP { get; set; } = new List<DongHoaDon>();
        public long TongTienChua { get; set; } = 0; // trước VAT
        public long TienVAT { get; set; } = 0;
        public long TongTienSau { get; set; } = 0; // sau VAT
        public long TienKhachDua { get; set; } = 0;
        public long TienThua { get; set; } = 0;
        public string PhuongThucTT { get; set; } = "Tiền mặt";
        public bool AnTienThua { get; set; } = false; // true khi chuyển khoản — ẩn dòng tiền trả lại trên hóa đơn

        // Panel cuộn chứa preview
        private Panel _scroll;
        private Panel _paper;

        public ucHoaDon()
        {
            BackColor = Color.FromArgb(200, 200, 200);
            BuildUI();
        }

        private void BuildUI()
        {
            _scroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(200, 200, 200)
            };

            _paper = new Panel
            {
                Width = 302,   // 80mm @ 96dpi
                BackColor = Color.White,
                Location = new Point(5, 5)
            };
            _paper.Paint += Paper_Paint;

            _scroll.Controls.Add(_paper);
            Controls.Add(_scroll);

            // Center paper khi resize + tính lại chiều cao đúng
            _scroll.Resize += (s, e) => {
                int cx = (_scroll.ClientSize.Width - _paper.Width) / 2;
                _paper.Left = Math.Max(5, cx);
                CapNhat(); // [FIX] đảm bảo AutoScrollMinSize luôn khớp khi panel cha đổi kích thước
            };
        }

        // Gọi hàm này sau khi set các property để vẽ lại
        public void CapNhat()
        {
            // [FIX] Tính trước chiều cao thật của hóa đơn TRƯỚC khi Paint,
            // để _scroll.AutoScrollMinSize được set đúng ngay, không bị "đông cứng"
            // ở kích thước cũ khiến phần dưới hóa đơn bị cắt mất khi nội dung dài ra.
            int chieuCaoMoi;
            using (Bitmap nhap = new Bitmap(1, 1))
            using (Graphics gNhap = Graphics.FromImage(nhap))
            {
                chieuCaoMoi = VeHoaDonChung(gNhap, 5, 8, _paper.Width - 10);
            }

            _paper.Height = Math.Max(chieuCaoMoi + 20, _scroll.ClientSize.Height);

            // Đảm bảo panel cuộn nhận đúng kích thước nội dung
            _scroll.AutoScrollMinSize = new Size(_paper.Width + 10, _paper.Height + 10);

            _paper.Invalidate();
        }

        // ══════════════════════════════════════════════════════════════════════
        // VẼ HÓA ĐƠN 80MM
        // ══════════════════════════════════════════════════════════════════════
        // ══════════════════════════════════════════════════════════════════════
        // HÀM VẼ DÙNG CHUNG (Cho cả Preview màn hình và Máy in)
        // ══════════════════════════════════════════════════════════════════════
        private int VeHoaDonChung(Graphics g, int startX, int startY, int width)
        {
            int px = startX;
            int py = startY;
            int pw = width;

            var fTitle = new Font("Courier New", 9f, FontStyle.Bold);
            var fBold = new Font("Courier New", 8f, FontStyle.Bold);
            var fNormal = new Font("Courier New", 7.5f);
            var fSmall = new Font("Courier New", 7f);
            var fItalic = new Font("Courier New", 7f, FontStyle.Italic);

            // Helpers
            void Ctr(string t, Font f, Brush br = null)
            {
                br = br ?? Brushes.Black;
                var sz = g.MeasureString(t, f);
                g.DrawString(t, f, br, px + (pw - sz.Width) / 2, py);
                py += (int)sz.Height + 1;
            }
            void Lft(string t, Font f)
            {
                g.DrawString(t, f, Brushes.Black, px, py);
                py += (int)g.MeasureString("A", f).Height + 2;
            }
            void Row(string left, string right, Font f)
            {
                g.DrawString(left, f, Brushes.Black, px, py);
                var rs = g.MeasureString(right, f);
                g.DrawString(right, f, Brushes.Black, px + pw - rs.Width, py);
                py += (int)g.MeasureString("A", f).Height + 2;
            }
            void Eq() { Lft(new string('=', 40), fNormal); }
            void Dsh() { Lft(new string('-', 40), fNormal); }
            void Gap(int h = 4) { py += h; }

            string tenCH = CauHinh?.TenCuaHang ?? "TÊN CỬA HÀNG";
            string diaChi = CauHinh?.DiaChi ?? "";
            string hotShip = CauHinh?.HotlineShip ?? "";
            int vatPct = CauHinh?.VAT ?? 0;
            string footer = CauHinh?.FooterHoaDon ?? "";

            // ── HEADER ────────────────────────────────────────────────────────
            Ctr(tenCH.ToUpper(), fTitle);
            Ctr("HÓA ĐƠN BÁN HÀNG", fBold);
            Eq();

            string ngay = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string maHD = string.IsNullOrEmpty(MaHoaDon) ? "------" : MaHoaDon;
            Lft($"Ngày: {ngay} - Số HĐ: {maHD}", fNormal);

            if (!string.IsNullOrEmpty(diaChi))
                foreach (var wl in Wrap($"Đ/c: {diaChi}", fNormal, g, pw))
                { g.DrawString(wl, fNormal, Brushes.Black, px, py); py += 13; }

            if (!string.IsNullOrEmpty(hotShip))
                Lft($"Hotline ship hàng: {hotShip}", fNormal);

            if (!string.IsNullOrEmpty(TenThuNgan))
                Lft($"Thu Ngân: {TenThuNgan} ({MaThuNgan})", fNormal);

            Gap(2);
            Ctr("******** HÓA ĐƠN ********", fBold);
            Eq();

            // ── BẢNG SẢN PHẨM ────────────────────────────────────────────────
            g.DrawString("Tên Hàng", fBold, Brushes.Black, px, py);
            g.DrawString("SL", fBold, Brushes.Black, px + 170, py);
            g.DrawString("Đơn giá", fBold, Brushes.Black, px + 195, py);
            var thStr = g.MeasureString("T.Tiền", fBold);
            g.DrawString("T.Tiền", fBold, Brushes.Black, px + pw - thStr.Width, py);
            py += 14;


            if (DanhSachSP == null || DanhSachSP.Count == 0)
            {
                Ctr("(Chưa có sản phẩm)", fItalic);
            }
            else
            {
                foreach (var sp in DanhSachSP)
                {
                    string tenDong = sp.TenSP;
                    var wrappedTen = Wrap(tenDong, fNormal, g, 165);
                    for (int i = 0; i < wrappedTen.Count; i++)
                    {
                        g.DrawString(wrappedTen[i], fNormal, Brushes.Black, px, py);
                        py += 13;
                    }

                    string slStr = sp.SoLuong.ToString();
                    string giaStr = $"{sp.DonGia:N0}";
                    string tienStr = $"{sp.ThanhTien:N0}";

                    g.DrawString(slStr, fNormal, Brushes.Black, px + 170, py);
                    g.DrawString(giaStr, fNormal, Brushes.Black, px + 190, py);
                    var tsz = g.MeasureString(tienStr, fNormal);
                    g.DrawString(tienStr, fNormal, Brushes.Black, px + pw - tsz.Width, py);
                    py += 14;

                }
            }
            Eq();

            // ── TỔNG TIỀN ─────────────────────────────────────────────────────
            int tongSL = 0;
            if (DanhSachSP != null) foreach (var sp in DanhSachSP) tongSL += sp.SoLuong;
            Row("Tổng số lượng:", tongSL.ToString(), fNormal);

            if (vatPct > 0)
                Row($"VAT {vatPct}%:", $"{TienVAT:N0}", fNormal);

            Row("Tổng tiền (Đã bao gồm VAT):", $"{TongTienSau:N0}", fBold);
            Row($"+ {PhuongThucTT}:", $"{TienKhachDua:N0}", fNormal);
            if (!AnTienThua) // ẩn dòng tiền trả lại khi thanh toán chuyển khoản
            {
                Row("Tiền trả lại KH (VND):", $"{TienThua:N0}", fNormal);
            }
            Eq();

            // ── KHÁCH HÀNG ────────────────────────────────────────────────────
            Lft($"Tên khách: {TenKhach}", fNormal);
            Lft($"Mã KH: {MaKhach}", fNormal);
            Lft($"Tổng điểm tích lũy: {DiemTichLuy}", fNormal);
            Dsh();
            Gap(4);

            // ── FOOTER ────────────────────────────────────────────────────────
            if (!string.IsNullOrEmpty(footer))
            {
                foreach (var line in footer.Split('\n'))
                {
                    string t = line.Trim();
                    if (string.IsNullOrEmpty(t)) { Gap(5); continue; }
                    foreach (var wl in Wrap(t, fItalic, g, pw))
                        Ctr(wl, fItalic);
                }
            }

            Gap(15);
            return py; // Trả về tọa độ Y cuối cùng (chính là chiều cao toàn bộ hóa đơn)
        }

        // ══════════════════════════════════════════════════════════════════════
        // VẼ LÊN PREVIEW GIAO DIỆN MÀN HÌNH (GỌI HÀM CHUNG)
        // ══════════════════════════════════════════════════════════════════════
        private void Paper_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            // [FIX] Không set _paper.Height ở đây nữa — đã được tính & gán
            // chính xác trong CapNhat() trước khi Invalidate(), tránh việc
            // resize control ngay trong sự kiện Paint của chính nó (gây
            // AutoScrollMinSize không cập nhật kịp -> hóa đơn bị cắt cụt).
            VeHoaDonChung(e.Graphics, 5, 8, _paper.Width - 10);
        }

        // ── Wrap text ─────────────────────────────────────────────────────────
        private List<string> Wrap(string text, Font f, Graphics g, int maxW)
        {
            var result = new List<string>();
            var words = text.Split(' ');
            string line = "";
            foreach (var w in words)
            {
                string test = string.IsNullOrEmpty(line) ? w : line + " " + w;
                if (g.MeasureString(test, f).Width > maxW) { result.Add(line); line = w; }
                else line = test;
            }
            if (!string.IsNullOrEmpty(line)) result.Add(line);
            if (result.Count == 0) result.Add("");
            return result;
        }

        // ══════════════════════════════════════════════════════════════════════
        // IN HÓA ĐƠN THẬT RA MÁY IN (GỌI HÀM CHUNG & CẮT GIẤY VỪA ĐỦ)
        // ══════════════════════════════════════════════════════════════════════
        public void InHoaDon()
        {
            var pd = new PrintDocument();

            // 1. Tuyệt chiêu: Mượn 1 Graphics giả để vẽ nháp nhằm lấy chính xác chiều cao
            int chieuCaoThucTe = 0;
            using (Bitmap nhap = new Bitmap(1, 1))
            {
                using (Graphics gNhap = Graphics.FromImage(nhap))
                {
                    // Giả lập lề ngang 315 điểm ảnh trừ đi lề
                    chieuCaoThucTe = VeHoaDonChung(gNhap, 8, 8, 315 - 16);
                }
            }

            // 2. Set PaperSize với chiều cao vừa tính được (loại bỏ hoàn toàn khoảng trắng)
            pd.DefaultPageSettings.PaperSize = new PaperSize("K80", 315, chieuCaoThucTe + 20);
            pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);
            pd.PrintPage += PrintPage;

            var ppd = new PrintPreviewDialog
            {
                Document = pd,
                Width = 450,
                Height = 800,
                Text = "Preview hóa đơn - Nhấn Print để in"
            };
            ppd.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs pe)
        {
            // Tái sử dụng 100% hàm vẽ chung cho bản in thật
            VeHoaDonChung(pe.Graphics, pe.MarginBounds.Left, pe.MarginBounds.Top, pe.MarginBounds.Width);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucHoaDon
            // 
            this.Name = "ucHoaDon";
            this.Load += new System.EventHandler(this.ucHoaDon_Load);
            this.ResumeLayout(false);

        }

        private void ucHoaDon_Load(object sender, EventArgs e)
        {

        }
    }

    // ── DTO dòng sản phẩm trong hóa đơn ─────────────────────────────────────
    public class DongHoaDon
    {
        public int SanPhamID { get; set; }
        public string MaVach { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public long DonGia { get; set; }
        public long ThanhTien { get; set; }
    }
}