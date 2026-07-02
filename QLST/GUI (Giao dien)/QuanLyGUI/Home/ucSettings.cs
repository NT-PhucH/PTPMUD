// ===================================================
// File: ucSettings.cs
// Đặt vào: GUI > QuanLyGUI
// ===================================================
using QLST.BLL__Bat_ngoai_le_.Core;
using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucSettings : UserControl
    {
        private readonly Setting_BLL _bll = new Setting_BLL();
        private Setting_DTO _ts;

        public ucSettings()
        {
            InitializeComponent();
            LoadSettings();
            panelPreviewScroll.Resize += PanelPreviewScroll_Resize;
        }

        private void LoadSettings()
        {
            try
            {
                // Lấy từ Cache của BLL cho nhanh
                _ts = _bll.GetCauHinh();
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

                    // Gán dữ liệu VietQR từ DB lên ComboBox/TextBox
                    string nganHang = _ts.NganHang;

                    if (string.IsNullOrWhiteSpace(nganHang) ||
                        !cbNganHang.Items.Contains(nganHang))
                    {
                        cbNganHang.SelectedIndex = 0; // Vietcombank
                    }
                    else
                    {
                        cbNganHang.SelectedItem = nganHang;
                    }
                    txtSoTaiKhoan.Text = _ts.SoTaiKhoan;
                    txtTenTaiKhoan.Text = _ts.TenTaiKhoan;
                }
                else
                {
                    // Đề phòng lỗi NullReferenceException
                    _ts = new Setting_DTO();
                }
                panelPreview?.Invalidate(); // Báo hiệu vẽ lại Preview
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải cài đặt: " + ex.Message); }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // VALIDATION BƯỚC 1: Ngăn lưu thông tin trống
            if (string.IsNullOrWhiteSpace(txtTenCH.Text))
            {
                MessageBox.Show("Tên cửa hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_ts == null) _ts = new Setting_DTO();

            // Lấy dữ liệu từ UI ném vào DTO
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

            // Lấy dữ liệu VietQR ném vào DTO
            _ts.NganHang = cbNganHang.SelectedItem?.ToString() ?? "Vietcombank";
            _ts.SoTaiKhoan = txtSoTaiKhoan.Text.Trim();
            _ts.TenTaiKhoan = txtTenTaiKhoan.Text.Trim();

            // Khóa nút lưu trong quá trình chờ để chống Click đúp 2 lần (lỗi memory/DB leak)
            btnLuu.Enabled = false;

            try
            {
                // Đẩy xuống BLL xử lý
                var (ok, msg) = _bll.CapNhat(_ts);
                panelPreview?.Invalidate();
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ngoại lệ khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLuu.Enabled = true; // Lưu xong thì mở khóa
            }
        }

        private void BtnReset_Click(object sender, EventArgs e) => LoadSettings();

        private void BtnCapNhat_Click(object sender, EventArgs e) => panelPreview?.Invalidate();

        // ── HÀM TẠO DỮ LIỆU HÓA ĐƠN MẪU DÙNG CHUNG CHO PREVIEW VÀ IN THỬ ──────────────
        private HoaDonIn_DTO TaoDuLieuHoaDonMau()
        {
            var duLieu = new HoaDonIn_DTO();

            // Lấy trực tiếp dữ liệu đang nhập trên màn hình để preview (không cần lưu)
            duLieu.CauHinh = new Setting_DTO
            {
                TenCuaHang = txtTenCH.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                HotlineShip = txtHotlineShip.Text.Trim(),
                VAT = (int)nudVAT.Value,
                FooterHoaDon = txtFooter.Text
            };

            // Fake chứng từ & Sản phẩm
            duLieu.MaHoaDon = "HD-TEST-123";
            duLieu.TenThuNgan = "Nguyễn Văn Admin";
            duLieu.TenKhach = "Khách lẻ thử nghiệm";
            duLieu.PhuongThucTT = "Tiền Mặt";

            duLieu.DanhSachSP = new List<ChiTietHoaDonIn_DTO>
            {
                new ChiTietHoaDonIn_DTO { TenSP = "Sản Phẩm Test Demo Số 1 (gói)", SoLuong = 5, DonGia = 5556, ThanhTien = 27780 },
                new ChiTietHoaDonIn_DTO { TenSP = "Sản Phẩm Test Demo Số 2 (hộp)", SoLuong = 1, DonGia = 9259, ThanhTien = 9259 }
            };

            duLieu.TongTienChua = 37039;
            duLieu.TienVAT = (long)(duLieu.TongTienChua * duLieu.CauHinh.VAT / 100.0);
            duLieu.TongTienSau = duLieu.TongTienChua + duLieu.TienVAT;
            duLieu.TienKhachDua = 500000;
            duLieu.TienThua = duLieu.TienKhachDua - duLieu.TongTienSau;

            return duLieu;
        }

        // ── SỰ KIỆN VẼ LÊN PANEL PREVIEW ────────────────────────────────────
        private void PanelPreview_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            var hdMau = new ucHoaDon();
            hdMau.DuLieu = TaoDuLieuHoaDonMau(); // Nạp DTO vào khay

            int margin = 5;
            int chieuCao = hdMau.VeHoaDonChung(e.Graphics, margin, 8, panelPreview.Width - (margin * 2));

            panelPreview.Height = Math.Max(chieuCao + 20, 600);
        }

        // ── SỰ KIỆN IN THỬ RA MÁY IN KHÔNG BỊ TRẮNG GIẤY ────────────────────
        private void BtnInThu_Click(object sender, EventArgs e)
        {
            var hdMau = new ucHoaDon();
            hdMau.DuLieu = TaoDuLieuHoaDonMau();
            int chieuCaoThucTe = 0;

            using (Bitmap nhap = new Bitmap(315, 1500))
            {
                using (Graphics gNhap = Graphics.FromImage(nhap))
                {
                    chieuCaoThucTe = hdMau.VeHoaDonChung(gNhap, 8, 8, 315 - 16);
                }
            }

            var pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = new PaperSize("K80", 315, chieuCaoThucTe + 20);
            pd.DefaultPageSettings.Margins = new Margins(8, 8, 8, 8);

            pd.PrintPage += (ps, pe) => {
                hdMau.VeHoaDonChung(pe.Graphics, pe.MarginBounds.Left, pe.MarginBounds.Top, pe.MarginBounds.Width);
            };

            var ppd = new PrintPreviewDialog { Document = pd, Width = 450, Height = 800, Text = "In thử hóa đơn từ Cài Đặt" };
            ppd.ShowDialog();
        }

        private void PanelPreviewScroll_Resize(object sender, EventArgs e)
        {
            int xPosition = (panelPreviewScroll.ClientSize.Width - panelPreview.Width) / 2;
            if (xPosition < 0) xPosition = 0;
            panelPreview.Left = xPosition;
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}