using QLST.BLL__Bat_ngoai_le_;
using QLST.BLL__Bat_ngoai_le_.Core;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.ThuNganOTP;
using QLST.GUI__Giao_dien_;
using QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don;
using QLST.GUI__Giao_dien_.ThuNganGUI.Ban_Hang;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QLST.DTO__Type_OTP_.QuanLyDTO;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        private readonly ThuNgan_BLL _thuNganBLL = new ThuNgan_BLL();
        private readonly Setting_BLL _settingBLL = new Setting_BLL();

        #region 1. HẰNG SỐ & BIẾN TOÀN CỤC

        private int currentPage = 1;
        private readonly int pageSize = 18;
        private int totalPages = 1;

        private List<ThuNganSP_DTO> dsspToanBo = new List<ThuNganSP_DTO>();

        private readonly int SO_COT = 3;
        private readonly int SO_HANG = 6;
        private readonly int KHOANG_CACH = 10;
        private Dictionary<string, int> dictTonKho = new Dictionary<string, int>();

        private ucThongBaoDonTam ucThongBao;
        private readonly List<HoaDonTam_DTO> danhSachHoaDonTam = new List<HoaDonTam_DTO>();

        private bool isInitializingComboBox = true;

        // Giữ nguyên đường dẫn hệ thống
        private readonly string _productImagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources", "Anh_SP");

        // Cache cấu hình VAT và Timer cho kỹ thuật Debounce
        private decimal _phanTramVAT = 0;
        private Timer _searchTimer = new Timer { Interval = 300 };

        #endregion

        #region 2. KHỞI TẠO FORM & SỰ KIỆN HỆ THỐNG

        public FormThuNgan()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.flowLayoutPanel1.SizeChanged += flowLayoutPanel1_SizeChanged;
            this.SizeChanged += FormThuNgan_SizeChanged;
            this.Load += FormThuNgan_Load;

            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            if (this.cboLoaiSanPham != null)
            {
                this.cboLoaiSanPham.SelectedIndexChanged += cboLoaiSanPham_SelectedIndexChanged;
            }

            chuyenTrang1.BamNutTrai += ChuyenTrang1_BamNutTrai;
            chuyenTrang1.BamNutPhai += ChuyenTrang1_BamNutPhai;
        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {
            // Cache phần trăm VAT từ đầu
            var setting = _settingBLL.GetCauHinh();
            if (setting != null)
            {
                _phanTramVAT = (decimal)setting.VAT / 100m;
            }

            // Gắn sự kiện cho Timer tìm kiếm
            _searchTimer.Tick += SearchTimer_Tick;

            LoadDanhSachLoaiSanPham();
            LoadDuLieuBanDau();
            KhoiTaoGiaoDienThongBao();
            KhoiTaoAutoComplete();

            đăngXuấtToolStripMenuItem.Click -= MenuDangXuat_Click;
            đăngXuấtToolStripMenuItem.Click += MenuDangXuat_Click;
        }

        private void FormThuNgan_SizeChanged(object sender, EventArgs e)
        {
            if (dsspToanBo != null && dsspToanBo.Count > 0)
            {
                HienThiDanhSachSanPham();
            }
        }

        private void LoadDanhSachLoaiSanPham()
        {
            if (this.cboLoaiSanPham == null) return;

            isInitializingComboBox = true;
            var danhSachLoai = _thuNganBLL.LayDanhSachLoaiSanPham();

            cboLoaiSanPham.DataSource = danhSachLoai;
            cboLoaiSanPham.DisplayMember = "TenLoai";
            cboLoaiSanPham.ValueMember = "LoaiSanPhamID";
            isInitializingComboBox = false;
        }

        #endregion

        #region 3. LOGIC XỬ LÝ HÓA ĐƠN TẠM

        private void KhoiTaoGiaoDienThongBao()
        {
            ucThongBao = new ucThongBaoDonTam
            {
                Visible = false
            };
            ucThongBao.OnKhoiPhucHoaDon += UcThongBao_OnKhoiPhucHoaDon;
            this.Controls.Add(ucThongBao);
            ucThongBao.BringToFront();
            pictureBox3.Cursor = Cursors.Hand;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            if (!ucThongBao.Visible)
            {
                Point locationOnForm = pictureBox3.FindForm().PointToClient(pictureBox3.Parent.PointToScreen(pictureBox3.Location));
                ucThongBao.Location = new Point(locationOnForm.X - ucThongBao.Width + pictureBox3.Width, locationOnForm.Y + pictureBox3.Height + 5);

                ucThongBao.CapNhatGiaoDien(danhSachHoaDonTam);
                ucThongBao.Visible = true;
                ucThongBao.BringToFront();
            }
            else
            {
                ucThongBao.Visible = false;
            }
        }

        private void LuuTamHoaDonHienTai()
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào để lưu tạm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HoaDonTam_DTO hd = new HoaDonTam_DTO
            {
                MaHoaDon = "HD_" + DateTime.Now.ToString("HHmmss"),
                ThoiGianLuu = DateTime.Now
            };

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    // Ngăn lỗi chia cho 0
                    if (card.SoLuong <= 0)
                    {
                        MessageBox.Show($"Sản phẩm '{card.TenSP}' có số lượng không hợp lệ. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    hd.DanhSachChiTiet.Add(new ChiTietHoaDonIn_DTO
                    {
                        MaVach = card.MaSP,
                        TenSP = card.TenSP,
                        SoLuong = card.SoLuong,
                        SanPhamID = card.SanPhamID,
                        DonGia = (long)(card.ThanhTien / card.SoLuong),
                        ThanhTien = (long)card.ThanhTien
                    });
                }
            }

            ClearGioHang();

            danhSachHoaDonTam.Add(hd);
            MessageBox.Show($"Đã lưu tạm đơn hàng: {hd.MaHoaDon}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TinhTongDonHang();
        }

        private void UcThongBao_OnKhoiPhucHoaDon(object sender, HoaDonTam_DTO hd)
        {
            KhoiPhucHoaDon(hd);
        }

        private void KhoiPhucHoaDon(HoaDonTam_DTO hd)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                var confirm = MessageBox.Show("Hóa đơn hiện tại chưa thanh toán. Bạn có muốn lưu tạm hóa đơn này trước khi khôi phục đơn cũ không?", "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (confirm == DialogResult.Cancel) return;
                if (confirm == DialogResult.Yes)
                {
                    LuuTamHoaDonHienTai();
                }
                else
                {
                    ClearGioHang();
                }
            }

            foreach (var item in hd.DanhSachChiTiet)
            {
                KhungMonHang cardMoi = new KhungMonHang();
                cardMoi.DuLieuThayDoi += KhungMonHang_DuLieuThayDoi;
                cardMoi.CapNhatThongTin(item.MaVach, item.TenSP, item.DonGia);

                cardMoi.SanPhamID = item.SanPhamID;
                cardMoi.SoLuong = item.SoLuong;

                cardMoi.Width = flowLayoutPanel1.ClientSize.Width - cardMoi.Margin.Left - cardMoi.Margin.Right - 5;
                flowLayoutPanel1.Controls.Add(cardMoi);
                flowLayoutPanel1.Controls.SetChildIndex(cardMoi, 0);
            }

            CapNhatSTT();

            danhSachHoaDonTam.Remove(hd);
            ucThongBao.Visible = false;
            TinhTongDonHang();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    if (card.SoLuong <= 0)
                    {
                        MessageBox.Show($"Sản phẩm '{card.TenSP}' đang có số lượng không hợp lệ ({card.SoLuong}).\nVui lòng nhập số lớn hơn 0!", "Lỗi số lượng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (dictTonKho.ContainsKey(card.MaSP) && card.SoLuong > dictTonKho[card.MaSP])
                    {
                        MessageBox.Show($"Không thể thanh toán!\nSản phẩm '{card.TenSP}' vượt quá tồn kho.\n(Tồn thực tế: {dictTonKho[card.MaSP]} - Số lượng đang nhập: {card.SoLuong}).", "Thiếu hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            var danhSachSP = new List<ChiTietHoaDonIn_DTO>();
            long tongTienChua = 0;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    long donGia = (long)(card.ThanhTien / card.SoLuong);
                    long thanhTien = (long)card.ThanhTien;
                    danhSachSP.Add(new ChiTietHoaDonIn_DTO
                    {
                        MaVach = card.MaSP,
                        TenSP = card.TenSP,
                        SoLuong = card.SoLuong,
                        DonGia = donGia,
                        ThanhTien = thanhTien,
                        SanPhamID = card.SanPhamID
                    });
                    tongTienChua += thanhTien;
                }
            }

            var formThanhToan = new frmThanhToan(danhSachSP, tongTienChua);
            formThanhToan.StartPosition = FormStartPosition.CenterParent;
            DialogResult ketQua = formThanhToan.ShowDialog();

            if (ketQua == DialogResult.Retry)
            {
                LuuTamHoaDonHienTai();
            }
            else if (ketQua == DialogResult.OK)
            {
                ClearGioHang();
                dictTonKho.Clear(); // Refresh cache tồn kho sau khi đã thanh toán thành công
                TinhTongDonHang();

                txtTimKiem.Clear();
                if (cboLoaiSanPham != null) cboLoaiSanPham.SelectedIndex = 0;
                LoadDuLieuBanDau();
            }
        }
        #endregion

        #region 4. LOGIC GIỎ HÀNG

        // Hàm hỗ trợ dọn dẹp giỏ hàng
        private void ClearGioHang()
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();
        }

        // Hàm hỗ trợ cập nhật lại STT cho các khung món hàng
        private void CapNhatSTT()
        {
            int sttMoi = flowLayoutPanel1.Controls.Count;
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang c)
                {
                    c.GanSTT(sttMoi);
                    sttMoi--;
                }
            }
        }

        public void TinhTongDonHang()
        {
            int tongSanPham = flowLayoutPanel1.Controls.Count;
            int tongSoLuong = 0;
            decimal tongTienHang = 0;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    tongSoLuong += card.SoLuong;
                    tongTienHang += card.ThanhTien;
                }
            }

            // Dùng _phanTramVAT đã cache
            decimal tienVAT = tongTienHang * _phanTramVAT;
            decimal tongThanhTien = tongTienHang + tienVAT;

            lblTongSanPham.Text = tongSanPham.ToString();
            lblTongSoLuong.Text = tongSoLuong.ToString();
            lblVAT.Text = tienVAT.ToString("N0");
            lblTongThanhTien.Text = tongThanhTien.ToString("N0");
        }

        private void KhungMonHang_DuLieuThayDoi(object sender, EventArgs e)
        {
            if (sender is KhungMonHang card)
            {
                if (card.SoLuong <= 0)
                {
                    MessageBox.Show($"Số lượng của '{card.TenSP}' phải lớn hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dictTonKho.ContainsKey(card.MaSP) && card.SoLuong > dictTonKho[card.MaSP])
                {
                    MessageBox.Show($"Bạn vừa nhập {card.SoLuong} cái.\nNhưng '{card.TenSP}' chỉ còn {dictTonKho[card.MaSP]} cái trong kho!", "Lỗi tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            TinhTongDonHang();
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.SuspendLayout();
            foreach (Control card in flowLayoutPanel1.Controls)
            {
                card.Width = flowLayoutPanel1.ClientSize.Width - card.Margin.Left - card.Margin.Right - 2;
            }
            flowLayoutPanel1.ResumeLayout();
        }


        private void ThemMonHangVaoDanhSach(string maSP, string tenSP, decimal donGia, int sanPhamID, int tonKhoTong)
        {
            if (tonKhoTong <= 0)
            {
                MessageBox.Show($"Sản phẩm '{tenSP}' đã hết hàng trong kho!", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dictTonKho.ContainsKey(maSP))
            {
                dictTonKho.Add(maSP, tonKhoTong);
            }

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card && card.MaSP == maSP)
                {
                    if (card.SoLuong >= tonKhoTong)
                    {
                        MessageBox.Show($"Sản phẩm '{tenSP}' chỉ còn tối đa {tonKhoTong} cái trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    card.TangSoLuong();
                    return;
                }
            }

            KhungMonHang cardMoi = new KhungMonHang();
            cardMoi.DuLieuThayDoi += KhungMonHang_DuLieuThayDoi;
            cardMoi.CapNhatThongTin(maSP, tenSP, donGia);

            cardMoi.SanPhamID = sanPhamID;
            cardMoi.Width = flowLayoutPanel1.ClientSize.Width - cardMoi.Margin.Left - cardMoi.Margin.Right - 5;

            flowLayoutPanel1.Controls.Add(cardMoi);
            flowLayoutPanel1.Controls.SetChildIndex(cardMoi, 0);

            CapNhatSTT();
            TinhTongDonHang();
        }

        // --- XỬ LÝ LỌC SẢN PHẨM ---
        private void ThucHienLocSanPham()
        {
            string keyword = txtTimKiem.Text.Trim();
            int maLoai = 0;

            if (this.cboLoaiSanPham != null && this.cboLoaiSanPham.SelectedValue != null)
            {
                if (int.TryParse(this.cboLoaiSanPham.SelectedValue.ToString(), out int parsedID))
                {
                    maLoai = parsedID;
                }
            }

            dsspToanBo = _thuNganBLL.TimKiemSanPhamKemLoai(keyword, maLoai);

            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;
            currentPage = 1;
            HienThiDanhSachSanPham();
        }

        private void KhoiTaoAutoComplete()
        {
            AutoCompleteStringCollection dataGoiY = new AutoCompleteStringCollection();

            var danhSachTatCaSP = _thuNganBLL.TimKiemSanPhamKemLoai("", 0);

            foreach (var sp in danhSachTatCaSP)
            {
                dataGoiY.Add(sp.TenSP);
            }

            txtTimKiem.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiem.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTimKiem.AutoCompleteCustomSource = dataGoiY;
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            ThucHienLocSanPham();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Reset timer (Debounce)
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void cboLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializingComboBox) return;
            ThucHienLocSanPham();
        }

        #endregion

        #region 5. LOGIC HIỂN THỊ & PHÂN TRANG SẢN PHẨM

        private void LoadDuLieuBanDau()
        {
            dsspToanBo = _thuNganBLL.LayDanhSachTrungBay();

            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;

            currentPage = 1;
            HienThiDanhSachSanPham();
        }

        private void HienThiDanhSachSanPham()
        {
            flowLayoutPanel2.SuspendLayout();

            // Xử lý Memory Leak: Dispose hoàn toàn ProductCard
            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                if (ctrl is ProductCard card)
                {
                    if (card.ProductImage != null) card.ProductImage.Dispose();
                    card.Dispose();
                }
            }
            flowLayoutPanel2.Controls.Clear();

            int chieuRongThe = (flowLayoutPanel2.ClientSize.Width - (KHOANG_CACH * (SO_COT + 1))) / SO_COT;
            int chieuCaoThe = (flowLayoutPanel2.ClientSize.Height - (KHOANG_CACH * (SO_HANG + 1))) / SO_HANG;

            var danhSachTrangHienTai = dsspToanBo.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            foreach (ThuNganSP_DTO sp in danhSachTrangHienTai)
            {
                ProductCard card = new ProductCard
                {
                    ProductName = sp.TenSP,
                    ProductPrice = sp.GiaBanHienTai.ToString("N0"),
                    Width = chieuRongThe,
                    Height = chieuCaoThe,
                    Margin = new Padding(KHOANG_CACH / 2)
                };
                card.Tag = sp;

                LoadProductImage(sp.HinhAnh, card);
                card.OnSelectProduct += Card_OnSelectProduct;

                flowLayoutPanel2.Controls.Add(card);
            }

            flowLayoutPanel2.ResumeLayout();
            chuyenTrang1.CapNhatThongTinTrang(currentPage, totalPages);
        }

        private void ChuyenTrang1_BamNutTrai(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                HienThiDanhSachSanPham();
            }
        }

        private void ChuyenTrang1_BamNutPhai(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                HienThiDanhSachSanPham();
            }
        }

        private void Card_OnSelectProduct(object sender, EventArgs e)
        {
            if (sender is ProductCard clickedCard)
            {
                ThuNganSP_DTO spThucTe = clickedCard.Tag as ThuNganSP_DTO;
                if (spThucTe != null)
                {
                    ThemMonHangVaoDanhSach(
                        spThucTe.MaVach,
                        spThucTe.TenSP,
                        spThucTe.GiaBanHienTai,
                        spThucTe.SanPhamID,
                        spThucTe.TonKhoTong
                    );
                }
            }
        }

        // --- HÀM TẢI ẢNH AN TOÀN TỪ ucQLSP ---
        private Image LoadImageNoLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var img = Image.FromStream(fs))
                {
                    return new Bitmap(img);
                }
            }
        }

        private void LoadProductImage(string imageNameFromDatabase, ProductCard productCard)
        {
            string fullImagePath = string.IsNullOrEmpty(imageNameFromDatabase) ? string.Empty : Path.Combine(_productImagesPath, imageNameFromDatabase);
            string defaultImagePath = Path.Combine(_productImagesPath, "NoImage.png");

            if (File.Exists(fullImagePath))
            {
                productCard.ProductImage = LoadImageNoLock(fullImagePath);
            }
            else if (File.Exists(defaultImagePath))
            {
                productCard.ProductImage = LoadImageNoLock(defaultImagePath);
            }
            else
            {
                productCard.ProductImage = null;
            }
        }

        #endregion

        #region 6. CÁC SỰ KIỆN KHÁC

        // Giữ nguyên thiết lập luồng theo yêu cầu
        private void MenuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                System.Threading.Thread t = new System.Threading.Thread(() => Application.Run(new FormLogin()));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();

                this.Close();
            }
        }

        #endregion
    }
}