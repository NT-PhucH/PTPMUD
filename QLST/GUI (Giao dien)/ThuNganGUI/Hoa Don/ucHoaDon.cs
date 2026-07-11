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
    public partial class ucHoaDon : UserControl
    {
        // ── CHỈ NHẬN ĐÚNG 1 KHAY DỮ LIỆU TỪ BLL TRUYỀN XUỐNG ──────────────────
        public HoaDonIn_DTO DuLieu { get; set; }

        public ucHoaDon()
        {
            InitializeComponent();
        }

        private void Scroll_Resize(object sender, EventArgs e)
        {
            int cx = (_scroll.ClientSize.Width - _paper.Width) / 2;
            _paper.Left = Math.Max(5, cx);
            CapNhat();
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

        /// <summary>
        /// Hàm tổng quản lý cấu trúc hóa đơn. 
        /// Muốn đổi thứ tự hiển thị, bạn chỉ cần thay đổi vị trí các dòng gọi hàm bên dưới.
        /// </summary>
        public int VeHoaDonChung(Graphics g, int startX, int startY, int width)
        {
            if (DuLieu == null) return startY;

            // Khởi tạo bộ ngữ cảnh vẽ (chứa tọa độ Y hiện tại, font chữ và các hàm helper)
            var ctx = new DrawingContext(g, startX, startY, width);

            // =================================================================
            // CÁC MODULE - BẠN CÓ THỂ KÉO/THAY ĐỔI THỨ TỰ CÁC DÒNG NÀY DỄ DÀNG:
            // =================================================================
            Vemodule_Header(ctx);               // Khối 1: Tên cửa hàng, tiêu đề, ngày tháng
            Vemodule_DanhSachSanPham(ctx);      // Khối 2: Bảng danh sách sản phẩm mua
            Vemodule_TongTien(ctx);             // Khối 3: Tổng số lượng, VAT, tổng tiền, tiền thừa
            Vemodule_ThongTinKhachHang(ctx);    // Khối 4: Tên khách hàng, điểm tích lũy
            Vemodule_Footer(ctx);               // Khối 5: Lời chào, chân trang hóa đơn
            // =================================================================

            ctx.Gap(15);
            return ctx.CurrentY; // Trả về chiều cao thực tế sau khi vẽ xong tất cả module
        }

        // ── 1. MODULE: TIÊU ĐỀ & THÔNG TIN CHUNG ─────────────────────────────
        private void Vemodule_Header(DrawingContext ctx)
        {
            ctx.Ctr((DuLieu.CauHinh?.TenCuaHang ?? "TÊN CỬA HÀNG").ToUpper(), ctx.fTitle);
            ctx.Ctr("HÓA ĐƠN BÁN HÀNG", ctx.fBold);
            ctx.Eq();

            ctx.Lft($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm} - Số HĐ: {(string.IsNullOrEmpty(DuLieu.MaHoaDon) ? "------" : DuLieu.MaHoaDon)}", ctx.fNormal);

            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.DiaChi))
            {
                foreach (var wl in WrapText($"Đ/c: {DuLieu.CauHinh.DiaChi}", ctx.fNormal, ctx.Graphics, ctx.Width))
                {
                    ctx.Graphics.DrawString(wl, ctx.fNormal, Brushes.Black, ctx.StartX, ctx.CurrentY);
                    ctx.CurrentY += 13;
                }
            }

            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.HotlineShip)) ctx.Lft($"Hotline ship hàng: {DuLieu.CauHinh.HotlineShip}", ctx.fNormal);
            if (!string.IsNullOrEmpty(DuLieu.TenThuNgan)) ctx.Lft($"Thu Ngân: {DuLieu.TenThuNgan} ({DuLieu.MaThuNgan})", ctx.fNormal);

            ctx.Gap(2);
            ctx.Ctr("******** HÓA ĐƠN ********", ctx.fBold);
            ctx.Eq();
        }

        // ── 2. MODULE: BẢNG CHI TIẾT SẢN PHẨM ────────────────────────────────
        private void Vemodule_DanhSachSanPham(DrawingContext ctx)
        {
            ctx.Graphics.DrawString("Tên Hàng", ctx.fBold, Brushes.Black, ctx.StartX, ctx.CurrentY);
            ctx.Graphics.DrawString("SL", ctx.fBold, Brushes.Black, ctx.StartX + 170, ctx.CurrentY);
            ctx.Graphics.DrawString("Đơn giá", ctx.fBold, Brushes.Black, ctx.StartX + 195, ctx.CurrentY);
            var thStr = ctx.Graphics.MeasureString("T.Tiền", ctx.fBold);
            ctx.Graphics.DrawString("T.Tiền", ctx.fBold, Brushes.Black, ctx.StartX + ctx.Width - thStr.Width, ctx.CurrentY);
            ctx.CurrentY += 14;

            if (DuLieu.DanhSachSP == null || DuLieu.DanhSachSP.Count == 0)
            {
                ctx.Ctr("(Chưa có sản phẩm)", ctx.fItalic);
            }
            else
            {
                foreach (var sp in DuLieu.DanhSachSP)
                {
                    var wrappedTen = WrapText(sp.TenSP, ctx.fNormal, ctx.Graphics, 165);
                    for (int i = 0; i < wrappedTen.Count; i++)
                    {
                        ctx.Graphics.DrawString(wrappedTen[i], ctx.fNormal, Brushes.Black, ctx.StartX, ctx.CurrentY);
                        ctx.CurrentY += 13;
                    }

                    ctx.Graphics.DrawString(sp.SoLuong.ToString(), ctx.fNormal, Brushes.Black, ctx.StartX + 170, ctx.CurrentY);
                    ctx.Graphics.DrawString($"{sp.DonGia:N0}", ctx.fNormal, Brushes.Black, ctx.StartX + 190, ctx.CurrentY);
                    string tienStr = $"{sp.ThanhTien:N0}";
                    var tsz = ctx.Graphics.MeasureString(tienStr, ctx.fNormal);
                    ctx.Graphics.DrawString(tienStr, ctx.fNormal, Brushes.Black, ctx.StartX + ctx.Width - tsz.Width, ctx.CurrentY);
                    ctx.CurrentY += 14;
                }
            }
            ctx.Eq();
        }

        // ── 3. MODULE: KHỐI TÍNH TOÁN TỔNG TIỀN ────────────────────────────────
        private void Vemodule_TongTien(DrawingContext ctx)
        {
            int tongSL = 0;
            if (DuLieu.DanhSachSP != null) foreach (var sp in DuLieu.DanhSachSP) tongSL += sp.SoLuong;

            ctx.Row("Tổng số lượng:", tongSL.ToString(), ctx.fNormal);
            if ((DuLieu.CauHinh?.VAT ?? 0) > 0) ctx.Row($"VAT {DuLieu.CauHinh.VAT}%:", $"{DuLieu.TienVAT:N0}", ctx.fNormal);

            ctx.Row("Tổng tiền (Đã bao gồm VAT):", $"{DuLieu.TongTienSau:N0}", ctx.fBold);
            ctx.Row($"+ {DuLieu.PhuongThucTT}:", $"{DuLieu.TienKhachDua:N0}", ctx.fNormal);

            if (!DuLieu.AnTienThua) ctx.Row("Tiền trả lại KH (VND):", $"{DuLieu.TienThua:N0}", ctx.fNormal);
            ctx.Eq();
        }

        // ── 4. MODULE: THÔNG TIN KHÁCH HÀNG THÂN THIẾT ────────────────────────
        private void Vemodule_ThongTinKhachHang(DrawingContext ctx)
        {
            ctx.Lft($"Tên khách: {DuLieu.TenKhach}", ctx.fNormal);
            ctx.Lft($"Mã KH: {DuLieu.MaKhach}", ctx.fNormal);
            ctx.Lft($"Tổng điểm tích lũy: {DuLieu.DiemTichLuy}", ctx.fNormal);
            ctx.Dsh();
            ctx.Gap(4);
        }

        // ── 5. MODULE: CHÂN HÓA ĐƠN VÀ LỜI CHÀO ───────────────────────────────
        private void Vemodule_Footer(DrawingContext ctx)
        {
            if (!string.IsNullOrEmpty(DuLieu.CauHinh?.FooterHoaDon))
            {
                foreach (var line in DuLieu.CauHinh.FooterHoaDon.Split('\n'))
                {
                    string t = line.Trim();
                    if (string.IsNullOrEmpty(t)) { ctx.Gap(5); continue; }
                    foreach (var wl in WrapText(t, ctx.fItalic, ctx.Graphics, ctx.Width)) ctx.Ctr(wl, ctx.fItalic);
                }
            }
        }

        // ── CÁC HÀM TIỆN ÍCH HỆ THỐNG ─────────────────────────────────────────
        private void Paper_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            VeHoaDonChung(e.Graphics, 5, 8, _paper.Width - 10);
        }

        private List<string> WrapText(string text, Font f, Graphics g, int maxW)
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
            var pd = new PrintDocument();//thư viện có sẵn
            int chieuCaoThucTe = 0;
            using (Bitmap nhap = new Bitmap(1, 1))
            using (Graphics gNhap = Graphics.FromImage(nhap))
            {
                chieuCaoThucTe = VeHoaDonChung(gNhap, 8, 8, 315 - 16);//hàm vẽ hóa đơn
            }

            pd.DefaultPageSettings.PaperSize = new PaperSize("K80", 315, chieuCaoThucTe + 20);
            pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);
            pd.PrintPage += (s, pe) => VeHoaDonChung(pe.Graphics, pe.MarginBounds.Left, pe.MarginBounds.Top, pe.MarginBounds.Width);

            var ppd = new PrintPreviewDialog { Document = pd, Width = 450, Height = 800, Text = "Preview hóa đơn - Nhấn Print để in" };
            ppd.ShowDialog();
        }

        // ── LỚP NGỮ CẢNH VẼ TRỢ GIÚP (ĐÓNG GÓI BIẾN CHẠY ĐỂ TRÁNH XUNG ĐỘT) ────
        public class DrawingContext
        {
            public Graphics Graphics { get; set; }
            public int StartX { get; set; }
            public int CurrentY { get; set; }
            public int Width { get; set; }

            // Quản lý tập trung Font chữ toàn hóa đơn tại đây
            public Font fTitle = new Font("Courier New", 9f, FontStyle.Bold);
            public Font fBold = new Font("Courier New", 8f, FontStyle.Bold);
            public Font fNormal = new Font("Courier New", 7.5f);
            public Font fSmall = new Font("Courier New", 7f);
            public Font fItalic = new Font("Courier New", 7f, FontStyle.Italic);

            public DrawingContext(Graphics g, int x, int y, int w)
            {
                Graphics = g; StartX = x; CurrentY = y; Width = w;
            }

            public void Ctr(string t, Font f) { var sz = Graphics.MeasureString(t, f); Graphics.DrawString(t, f, Brushes.Black, StartX + (Width - sz.Width) / 2, CurrentY); CurrentY += (int)sz.Height + 1; }
            public void Lft(string t, Font f) { Graphics.DrawString(t, f, Brushes.Black, StartX, CurrentY); CurrentY += (int)Graphics.MeasureString("A", f).Height + 2; }
            public void Row(string left, string right, Font f) { Graphics.DrawString(left, f, Brushes.Black, StartX, CurrentY); var rs = Graphics.MeasureString(right, f); Graphics.DrawString(right, f, Brushes.Black, StartX + Width - rs.Width, CurrentY); CurrentY += (int)Graphics.MeasureString("A", f).Height + 2; }
            public void Eq() { Lft(new string('=', 40), fNormal); }
            public void Dsh() { Lft(new string('-', 40), fNormal); }
            public void Gap(int h = 4) { CurrentY += h; }
        }
    }
}