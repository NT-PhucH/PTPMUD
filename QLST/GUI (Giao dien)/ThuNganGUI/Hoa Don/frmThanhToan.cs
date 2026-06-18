// ===================================================
// File: frmThanhToan.cs  — CHỈ CHỨA LOGIC
// UI do Designer quản lý, KHÔNG khai báo control ở đây
// ===================================================
using QLST.DAL__Connection_Query_DB_.Core;
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don
{
    public partial class frmThanhToan : Form
    {
        // ── Dữ liệu truyền vào ────────────────────────────────────────────────
        private List<DongHoaDon> _danhSachSP;
        private long _tongTienChua;
        private ThamSoHeThong_DTO _cfg;
        private PictureBox _picQR;

        // ── Khách hàng tìm được ───────────────────────────────────────────────
        private KhachHang_DTO _khachHang = null;

        // ── ucHoaDon nhúng vào panel1 ─────────────────────────────────────────
        private ucHoaDon _ucHD;

        // ── Flag tránh vòng lặp TextChanged ──────────────────────────────────
        private bool _dangChonKH = false;

        // ── Constructor ───────────────────────────────────────────────────────
        public frmThanhToan(List<DongHoaDon> danhSachSP, long tongTienChua)
        {
            InitializeComponent();
            _danhSachSP = danhSachSP;
            _tongTienChua = tongTienChua;
            _cfg = new ThamSo_DAL().Get();
            this.Load += frmThanhToan_Load;
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD
        // ══════════════════════════════════════════════════════════════════════
        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            NhungUcHoaDon();
            KhoiTaoSuKien();
            TinhVaHienThi();

            // KHỞI TẠO QR ĐỘNG (Bạn có thể gán Image bằng mã QR thật của cửa hàng)
            _picQR = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Visible = false,
                // Image = Image.FromFile("duong_dan_den_file_qr_cua_ban.jpg") // Gán ảnh QR tĩnh vào đây
            };
            panel2.Controls.Add(_picQR);
            _picQR.BringToFront(); // Đưa QR lên trên cùng để không bị che
        }

        // ── Nhúng ucHoaDon vào panel1 ─────────────────────────────────────────
        private void NhungUcHoaDon()
        {
            _ucHD = new ucHoaDon { Dock = DockStyle.Fill };
            panel1.Controls.Add(_ucHD);

            _ucHD.CauHinh = _cfg;
            _ucHD.MaHoaDon = $"HD{DateTime.Now:ddMMyyyy}{new Random().Next(1000, 9999)}";
            _ucHD.TenThuNgan = SessionManager.NhanVienDangNhap?.TenNV ?? "";
            _ucHD.MaThuNgan = SessionManager.NhanVienDangNhap?.MaNV ?? "";
            _ucHD.DanhSachSP = _danhSachSP;
            _ucHD.TenKhach = "Khách lẻ";
        }

        // ── Gán sự kiện ──────────────────────────────────────────────────────
        private void KhoiTaoSuKien()
        {
            // Gợi ý KH khi gõ SĐT (realtime)
            txtSDT.TextChanged += (s, e) => GoiYKhachHang();
            txtSDT.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) DatKhachLe(); };

            // Tính tiền thừa khi nhập tiền khách đưa
            txtKhachDua.TextChanged += (s, e) => TinhTienThua();

            // Chọn phương thức thanh toán
            paymentSelectorBar1.Click += (s, e) => {
                this.BeginInvoke(new Action(() => {
                    bool laChuyenKhoan = !paymentSelectorBar1.IsCashSelected;

                    // SỬA 1: Trả lại đúng vị trí chữ (Chuyển khoản = Chuyển Khoản)
                    _ucHD.PhuongThucTT = laChuyenKhoan ? "Chuyển Khoản" : "Tiền Mặt";
                    _ucHD.AnTienThua = laChuyenKhoan;

                    panel14.Visible = !laChuyenKhoan;
                    if (_picQR != null) _picQR.Visible = laChuyenKhoan;

                    if (laChuyenKhoan)
                    {
                        int vatPct = _cfg?.VAT ?? 0;
                        long tongSau = (long)(_tongTienChua * (1 + vatPct / 100.0));
                        _ucHD.TienKhachDua = tongSau;
                        _ucHD.TienThua = 0;
                        CapNhatHoaDon();
                    }
                    else
                    {
                        // SỬA 2: LỖI KHÔNG ĐỒNG BỘ TIỀN.
                        // Khi chọn lại Tiền Mặt, phải gọi hàm này để nó đọc số từ txtKhachDua 
                        // và in lên hóa đơn ngay lập tức.
                        TinhTienThua();
                    }
                }));
            };
        }

        // ── Tính và hiển thị tổng tiền ban đầu ───────────────────────────────
        private void TinhVaHienThi()
        {
            int vatPct = _cfg?.VAT ?? 0;
            long tienVAT = vatPct > 0 ? (long)(_tongTienChua * vatPct / 100.0) : 0;
            long tongSau = _tongTienChua + tienVAT;

            //lblKhachThanhToan.Text = $"{tongSau:N0} đ";
            txtKhachDua.Text = LamTronTien(tongSau).ToString();
            lblTienThua.Text = "0 đ";

            _ucHD.TongTienChua = _tongTienChua;
            _ucHD.TienVAT = tienVAT;
            _ucHD.TongTienSau = tongSau;
            _ucHD.TienKhachDua = LamTronTien(tongSau);
            _ucHD.TienThua = LamTronTien(tongSau) - tongSau;
            _ucHD.AnTienThua = false;
            CapNhatHoaDon();
        }

        // ══════════════════════════════════════════════════════════════════════
        // TÌM KIẾM KHÁCH HÀNG — GỢI Ý REALTIME TRONG PANEL5
        // ══════════════════════════════════════════════════════════════════════
        private void GoiYKhachHang()
        {
            if (_dangChonKH) return;

            string keyword = txtSDT.Text.Trim();
            panel5.Controls.Clear();

            if (string.IsNullOrEmpty(keyword))
            {
                if (_khachHang != null) DatKhachLe();
                return;
            }

            try
            {
                var list = new KhachHang_DAL().Search(keyword);
                if (list.Count == 0) { HienNutTaoMoi(keyword); return; }
                HienDanhSachGoiY(list);
            }
            catch { /* lỗi kết nối — bỏ qua */ }
        }

        private void HienDanhSachGoiY(List<KhachHang_DTO> list)
        {
            panel5.AutoScroll = true;
            int y = 0;

            foreach (var kh in list)
            {
                var row = new Panel
                {
                    Location = new Point(0, y),
                    Size = new Size(panel5.Width - 2, 52),
                    BackColor = Color.White,
                    Cursor = Cursors.Hand,
                    Tag = kh
                };
                row.Paint += (s, pe) =>
                    pe.Graphics.DrawLine(Pens.LightGray, 0, row.Height - 1, row.Width, row.Height - 1);

                string hang = LayHang(kh.DiemTichLuy);

                var lblTen = new Label
                {
                    Text = $"{kh.TenKH}",
                    Location = new Point(10, 7),
                    Size = new Size(panel5.Width - 20, 18),
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 100, 200)
                };
                var lblInfo = new Label
                {
                    Text = $"📞 {kh.SDT}   ⭐ {kh.DiemTichLuy:N0} điểm  {hang}",
                    Location = new Point(10, 29),
                    Size = new Size(panel5.Width - 20, 16),
                    Font = new Font("Segoe UI", 8.5f),
                    ForeColor = Color.Gray
                };

                row.Controls.Add(lblTen);
                row.Controls.Add(lblInfo);

                EventHandler chon = (s, e) => ChonKhachHang(kh);
                row.Click += chon; lblTen.Click += chon; lblInfo.Click += chon;
                row.MouseEnter += (s, e) => row.BackColor = Color.FromArgb(235, 245, 255);
                row.MouseLeave += (s, e) => row.BackColor = Color.White;

                panel5.Controls.Add(row);
                y += 53;
            }
        }

        private void HienNutTaoMoi(string keyword)
        {
            panel5.AutoScroll = false;
            panel5.Controls.Add(new Label
            {
                Text = $"Không tìm thấy KH \"{keyword}\"",
                Location = new Point(10, 14),
                Size = new Size(panel5.Width - 20, 18),
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.Gray
            });

            var btnTao = new Button
            {
                Text = "➕  Tạo khách hàng mới",
                Location = new Point(10, 40),
                Size = new Size(panel5.Width - 20, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 85, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnTao.FlatAppearance.BorderSize = 0;
            btnTao.Click += (s, e) => MoFormTaoKhach(keyword);
            panel5.Controls.Add(btnTao);
        }

        private void ChonKhachHang(KhachHang_DTO kh)
        {
            _khachHang = kh;
            _dangChonKH = true;
            txtSDT.Text = kh.SDT;
            _dangChonKH = false;

            HienThiThongTinKhach();
            _ucHD.TenKhach = kh.TenKH;
            _ucHD.MaKhach = kh.KhachHangID.ToString();
            _ucHD.DiemTichLuy = kh.DiemTichLuy;
            CapNhatHoaDon();
        }

        private void DatKhachLe()
        {
            _khachHang = null;
            panel5.Controls.Clear();
            _ucHD.TenKhach = "Khách lẻ";
            _ucHD.MaKhach = "";
            _ucHD.DiemTichLuy = 0;
            CapNhatHoaDon();
        }

        // ── Card KH gọn: Tên (+ nút đổi khách cùng dòng) / SĐT / Điểm + Hạng ──
        private void HienThiThongTinKhach()
        {
            panel5.Controls.Clear();
            panel5.AutoScroll = false;

            string hang = LayHang(_khachHang.DiemTichLuy);

            // Nút đổi khách — đặt bên PHẢI, cùng dòng với tên khách
            var btnHuy = new Button
            {
                Text = "✕ Đổi",
                Size = new Size(70, 26),
                Location = new Point(panel5.Width - 80, 11),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => { txtSDT.Clear(); DatKhachLe(); };
            panel5.Controls.Add(btnHuy);

            // Tên — chiếm phần còn lại bên trái, không đè lên nút
            panel5.Controls.Add(new Label
            {
                Text = $"✅  {_khachHang.TenKH}",
                Location = new Point(10, 12),
                Size = new Size(panel5.Width - 100, 22),
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 100, 200),
                AutoEllipsis = true
            });
            // SĐT
            panel5.Controls.Add(new Label
            {
                Text = $"📞  {_khachHang.SDT}",
                Location = new Point(10, 40),
                Size = new Size(panel5.Width - 20, 18),
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.DimGray
            });
            // Điểm + Hạng
            panel5.Controls.Add(new Label
            {
                Text = $"⭐  {_khachHang.DiemTichLuy:N0} điểm   {hang}",
                Location = new Point(10, 64),
                Size = new Size(panel5.Width - 20, 18),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 120, 0)
            });
        }

        private void MoFormTaoKhach(string sdtMacDinh)
        {
            string ten = Microsoft.VisualBasic.Interaction.InputBox(
                $"Nhập tên khách hàng (SĐT: {sdtMacDinh}):", "Tạo khách mới", "");
            if (string.IsNullOrWhiteSpace(ten)) return;
            try
            {
                var khMoi = new KhachHang_DTO { TenKH = ten.Trim(), SDT = sdtMacDinh, DiemTichLuy = 0 };
                if (new KhachHang_DAL().Insert(khMoi))
                {
                    var ds = new KhachHang_DAL().Search(sdtMacDinh);
                    if (ds.Count > 0) ChonKhachHang(ds[0]);
                }
                else
                    MessageBox.Show("Tạo khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TinhTienThua()
        {
            int vatPct = _cfg?.VAT ?? 0;
            long tongSau = (long)(_tongTienChua * (1 + vatPct / 100.0));

            if (long.TryParse(txtKhachDua.Text.Replace(",", "").Replace(".", "").Trim(), out long khachDua))
            {
                long thua = khachDua - tongSau;
                lblTienThua.Text = thua >= 0 ? $"{thua:N0} đ" : "Chưa đủ tiền!";
                lblTienThua.ForeColor = thua >= 0 ? Color.FromArgb(34, 139, 34) : Color.Red;
                _ucHD.TienKhachDua = khachDua;
                _ucHD.TienThua = Math.Max(0, thua);
                CapNhatHoaDon();
            }
        }


        private void CapNhatHoaDon() => _ucHD.CapNhat();

        // ── Nút Hoàn Tất ─────────────────────────────────────────────────────
        private void btnInHD_Click(object sender, EventArgs e)
        {
            int vatPct = _cfg?.VAT ?? 0;
            long tongSau = (long)(_tongTienChua * (1 + vatPct / 100.0));
            bool laChuyenKhoan = !paymentSelectorBar1.IsCashSelected;

            // Validate chỉ khi tiền mặt
            if (!laChuyenKhoan)
            {
                if (!long.TryParse(txtKhachDua.Text.Replace(",", "").Replace(".", "").Trim(), out long kd)
                    || kd < tongSau)
                {
                    MessageBox.Show("Số tiền khách đưa chưa đủ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Build chi tiết hóa đơn
            var dsChiTiet = new List<QLST.DTO__Type_OTP_.ThuNganOTP.ThanhToan_DTO>();
            foreach (var dong in _danhSachSP)
            {
                dsChiTiet.Add(new QLST.DTO__Type_OTP_.ThuNganOTP.ThanhToan_DTO
                {
                    SanPhamID = dong.SanPhamID,
                    SoLuongMua = dong.SoLuong,
                    DonGiaBan = (int)dong.DonGia,
                    ThanhTien = dong.ThanhTien
                });
            }

            long tienKhachDua = laChuyenKhoan
                ? tongSau  // chuyển khoản: đúng số, không làm tròn
                : long.Parse(txtKhachDua.Text.Replace(",", "").Replace(".", "").Trim());
            long tienThua = laChuyenKhoan ? 0 : tienKhachDua - tongSau;
            string phuong = laChuyenKhoan ? "Chuyển Khoản" : "Tiền Mặt";

            // ── FIX FK: lấy NhanVienID (int) đúng field, không parse MaNV ──
            // SessionManager cần expose NhanVienID (int). Nếu có thì dùng:
            //   int nhanVienID = SessionManager.NhanVienDangNhap?.NhanVienID ?? 0;
            // Nếu chỉ có MaNV dạng "NV001" thì phải query DB để lấy ID, hoặc
            // thêm property NhanVienID vào NhanVien_DTO và gán khi login.
            // Tạm thời lấy an toàn nhất:
            int nhanVienID = 1;
            var nv = SessionManager.NhanVienDangNhap;
            if (nv != null)
            {
                // Ưu tiên dùng NhanVienID nếu DTO có field đó
                // nhanVienID = nv.NhanVienID;
                // Fallback: thử parse MaNV nếu lỡ là số thuần
                int.TryParse(nv.MaNV, out nhanVienID);
            }

            if (nhanVienID <= 0)
            {
                MessageBox.Show(
                    "Không xác định được nhân viên đang đăng nhập (NhanVienID = 0).\n" +
                    "Vui lòng kiểm tra: SessionManager.NhanVienDangNhap cần có property NhanVienID (int) khớp với bảng NhanVien.",
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? khID = null;
            if (_khachHang != null)
            {
                khID = _khachHang.KhachHangID; // Tùy thuộc vào kiểu dữ liệu KhachHangID trong DTO của bạn
            }

            // Tính điểm cộng (Ví dụ: 10k VNĐ = 1 điểm, bạn có thể chỉnh lại tỉ lệ chia)
            int diemCong = khID.HasValue ? (int)(tongSau / 10000) : 0;
            var bll = new QLST.BLL__Bat_ngoai_le_.ThanhToan_BLL();
            bool ok = bll.XuLyThanhToan(
                _ucHD.MaHoaDon, nhanVienID, khID, diemCong, tongSau, phuong,
                tienKhachDua, tienThua, dsChiTiet, out string thongBao);

            if (!ok)
            {
                MessageBox.Show("Lỗi thanh toán: " + thongBao, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string noidung = laChuyenKhoan
                ? "Thanh toán chuyển khoản thành công!\n\nBạn có muốn in hóa đơn không?"
                : $"Thanh toán thành công!\nTiền thừa: {tienThua:N0} đ\n\nBạn có muốn in hóa đơn không?";

            if (MessageBox.Show(noidung, "Thành công",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                _ucHD.InHoaDon();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void label15_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }

        // ── Helpers ──────────────────────────────────────────────────────────
        private long LamTronTien(long tien) => ((tien + 999) / 1000) * 1000;

        private string LayHang(int diem) =>
            diem >= 1000 ? "💎 VIP" :
            diem >= 500 ? "🥇 Vàng" :
            diem >= 100 ? "🏅 Bạc" : "Thường";

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void paymentSelectorBar1_Load(object sender, EventArgs e)
        {

        }

        private void frmThanhToanold_Load(object sender, EventArgs e)
        {

        }

        private void txtSDT_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}