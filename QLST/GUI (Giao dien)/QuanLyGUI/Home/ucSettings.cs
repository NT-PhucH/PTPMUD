using QLST.DAL__Connection_Query_DB_.Core;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucSettings : UserControl
    {
        private readonly ThamSo_DAL _dal = new ThamSo_DAL();
        private ThamSoHeThong_DTO _ts;

        private static ThamSoHeThong_DTO _cache;
        public static ThamSoHeThong_DTO LayCauHinh()
        {
            if (_cache == null) _cache = new ThamSo_DAL().Get();
            return _cache;
        }

        public ucSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                _ts = _dal.Get();
                if (_ts != null)
                {
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
                }
                panelPreview?.Invalidate();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải cài đặt: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCH.Text))
            { MessageBox.Show("Tên cửa hàng không được để trống!"); return; }

            if (_ts == null) _ts = new ThamSoHeThong_DTO();

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

            bool ok = _dal.Update(_ts);
            if (ok) _cache = _ts;

            panelPreview?.Invalidate();
            MessageBox.Show(ok ? "✅ Lưu thành công!" : "⚠️ Lưu thất bại!",
                "Thông báo", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            panelPreview?.Invalidate();
        }

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

            void Ctr(string t, Font f)
            {
                var s = g.MeasureString(t, f);
                g.DrawString(t, f, Brushes.Black, px + (pw - s.Width) / 2, py);
                py += (int)s.Height + 1;
            }
            void Lft(string t, Font f)
            {
                g.DrawString(t, f, Brushes.Black, px, py);
                py += (int)g.MeasureString("A", f).Height + 2;
            }
            void Rgt(string left, string right, Font f)
            {
                g.DrawString(left, f, Brushes.Black, px, py);
                var rs = g.MeasureString(right, f);
                g.DrawString(right, f, Brushes.Black, px + pw - rs.Width, py);
                py += (int)g.MeasureString("A", f).Height + 2;
            }
            void Eq() { Lft(new string('=', 40), fNormal); }
            void Dsh() { Lft(new string('-', 40), fNormal); }

            string tenCH = txtTenCH?.Text ?? "TÊN CỬA HÀNG";
            string diaChi = txtDiaChi?.Text ?? "";
            string hotShip = txtHotlineShip?.Text ?? "";
            string vat = nudVAT?.Value.ToString() ?? "0";
            string footer = txtFooter?.Text ?? "";

            Ctr(tenCH.ToUpper(), fTitle);
            Ctr("HÓA ĐƠN BÁN HÀNG", fBold);
            Eq();
            Lft($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm} - Số HĐ: HD------", fNormal);
            foreach (var l in WrapText($"Đ/c: {diaChi}", fNormal, g, pw))
            { g.DrawString(l, fNormal, Brushes.Black, px, py); py += 13; }
            if (!string.IsNullOrEmpty(hotShip)) Lft($"Hotline ship hàng: {hotShip}", fNormal);
            Lft("Thu Ngân: [Tên thu ngân]", fNormal);
            Ctr("********BẢN CHÍNH********", fBold);
            Eq();

            Lft("Tên Hàng               SL  Đơn giá T.Tiền", fBold);
            Dsh();
            Lft("SP000001 - Mì tôm xào khô Goreng", fNormal);
            Lft("vị đặc biệt 85g T40 - (gói)", fNormal);
            Rgt("", "5  5,556  27,778", fNormal);
            Dsh();
            Lft("083651 - Xúc xích TT Ponnie vị heo", fNormal);
            Lft("20 gói*5 cây*19gr T20 - (GOI)", fNormal);
            Rgt("", "1  9,259   9,259", fNormal);
            Eq();

            Rgt("Tổng số lượng:", "6", fNormal);
            if (vat != "0") Rgt($"VAT {vat}%:", "2,963", fNormal);
            Rgt("Tổng tiền (Đã bao gồm VAT):", "40,000", fBold);
            Rgt("Tiền khách trả:", "40,000", fNormal);
            Rgt("Tiền trả lại KH (VND):", "460,000", fNormal);
            Rgt("+ Tiền mặt", "500,000", fNormal);
            Eq();

            Lft("Tên khách: Khách lẻ", fNormal);
            Lft("Mã KH:", fNormal);
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

                void C(string t, Font f)
                {
                    var sz = g.MeasureString(t, f);
                    g.DrawString(t, f, Brushes.Black, lx + (lw - sz.Width) / 2, ly);
                    ly += (int)sz.Height + 1;
                }
                void L(string t, Font f)
                {
                    g.DrawString(t, f, Brushes.Black, lx, ly);
                    ly += (int)g.MeasureString("A", f).Height + 1;
                }
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
                foreach (var line in (txtFooter.Text ?? "").Split('\n'))
                    C(line.Trim(), fS);
            };

            var ppd = new PrintPreviewDialog { Document = pd, Width = 420, Height = 750 };
            ppd.ShowDialog();
        }

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
    }
}
