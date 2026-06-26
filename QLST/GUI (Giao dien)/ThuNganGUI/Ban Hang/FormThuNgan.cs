using QLST.BLL__Bat_ngoai_le_;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.ThuNganOTP;
using QLST.GUI__Giao_dien_;
using QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        #region 1. HẰNG SỐ & BIẾN TOÀN CỤC

        private int currentPage = 1;
        private readonly int pageSize = 18; // 3 cột x 6 hàng
        private int totalPages = 1;

        // Đã thay thế sang DTO chuẩn mới
        private List<ThuNganSP_DTO> dsspToanBo = new List<ThuNganSP_DTO>();

        private readonly int SO_COT = 3;
        private readonly int SO_HANG = 6;
        private readonly int KHOANG_CACH = 10;
        private Dictionary<string, int> dictTonKho = new Dictionary<string, int>();

        private Panel pnlDropdownThongBao;
        private FlowLayoutPanel flpDanhSachThongBao;
        private readonly List<HoaDonTam> danhSachHoaDonTam = new List<HoaDonTam>();

        public class HoaDonTam
        {
            public string MaHoaDon { get; set; }
            public DateTime ThoiGianLuu { get; set; }
            public List<Control> DanhSachKhungMonHang { get; set; } = new List<Control>();
        }

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

            // BỔ SUNG: Sự kiện lọc dữ liệu gợi ý realtime mỗi khi gõ chữ
            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            this.txtTimKiem.KeyDown += txtTimKiem_KeyDown;

            chuyenTrang1.BamNutTrai += ChuyenTrang1_BamNutTrai;
            chuyenTrang1.BamNutPhai += ChuyenTrang1_BamNutPhai;
        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {
            LoadDuLieuBanDau();
            KhoiTaoGiaoDienThongBao();

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

        #endregion

        #region 3. LOGIC XỬ LÝ HÓA ĐƠN TẠM

        private void KhoiTaoGiaoDienThongBao()
        {
            pnlDropdownThongBao = new Panel
            {
                Size = new Size(350, 400),
                BackColor = Color.FromArgb(40, 40, 40),
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblHeader = new Label
            {
                Text = "Hóa đơn chờ thanh toán",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            pnlDropdownThongBao.Controls.Add(lblHeader);

            flpDanhSachThongBao = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.FromArgb(40, 40, 40)
            };
            pnlDropdownThongBao.Controls.Add(flpDanhSachThongBao);
            flpDanhSachThongBao.BringToFront();

            this.Controls.Add(pnlDropdownThongBao);
            pnlDropdownThongBao.BringToFront();

            pictureBox3.Cursor = Cursors.Hand;
        }

        private void CapNhatGiaoDienThongBao()
        {
            flpDanhSachThongBao.SuspendLayout();
            flpDanhSachThongBao.Controls.Clear();

            if (danhSachHoaDonTam.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Không có đơn hàng chờ.",
                    ForeColor = Color.LightGray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                flpDanhSachThongBao.Controls.Add(lblEmpty);
                flpDanhSachThongBao.ResumeLayout();
                return;
            }

            foreach (var hd in danhSachHoaDonTam.OrderByDescending(x => x.ThoiGianLuu))
            {
                Panel pnlItem = new Panel
                {
                    Width = flpDanhSachThongBao.Width - 25,
                    Height = 70,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = hd
                };

                PictureBox picIcon = new PictureBox
                {
                    Image = global::QLST.Properties.Resources.shopping_cart__1_,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(40, 40),
                    Location = new Point(10, 15)
                };

                Label lblTitle = new Label
                {
                    Text = $"Đơn hàng tạm: {hd.MaHoaDon}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(60, 10),
                    AutoSize = true
                };

                Label lblTime = new Label
                {
                    Text = $"Đã lưu lúc {hd.ThoiGianLuu:HH:mm:ss} - {hd.DanhSachKhungMonHang.Count} SP",
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.Gray,
                    Location = new Point(60, 35),
                    AutoSize = true
                };

                pnlItem.Controls.Add(picIcon);
                pnlItem.Controls.Add(lblTitle);
                pnlItem.Controls.Add(lblTime);

                pnlItem.MouseEnter += (s, e) => pnlItem.BackColor = Color.FromArgb(60, 60, 60);
                pnlItem.MouseLeave += (s, e) => pnlItem.BackColor = Color.Transparent;

                pnlItem.Click += (s, e) => KhoiPhucHoaDon(hd);
                foreach (Control c in pnlItem.Controls)
                {
                    c.Click += (s, e) => KhoiPhucHoaDon(hd);
                }

                flpDanhSachThongBao.Controls.Add(pnlItem);
            }

            flpDanhSachThongBao.ResumeLayout();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            if (!pnlDropdownThongBao.Visible)
            {
                Point locationOnForm = pictureBox3.FindForm().PointToClient(pictureBox3.Parent.PointToScreen(pictureBox3.Location));
                pnlDropdownThongBao.Location = new Point(locationOnForm.X - pnlDropdownThongBao.Width + pictureBox3.Width, locationOnForm.Y + pictureBox3.Height + 5);

                CapNhatGiaoDienThongBao();
                pnlDropdownThongBao.Visible = true;
                pnlDropdownThongBao.BringToFront();
            }
            else
            {
                pnlDropdownThongBao.Visible = false;
            }
        }

        private void LuuTamHoaDonHienTai()
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào để lưu tạm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HoaDonTam hd = new HoaDonTam
            {
                MaHoaDon = "HD_" + DateTime.Now.ToString("HHmmss"),
                ThoiGianLuu = DateTime.Now
            };

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                hd.DanhSachKhungMonHang.Add(ctrl);
            }

            flowLayoutPanel1.Controls.Clear();
            danhSachHoaDonTam.Add(hd);

            MessageBox.Show($"Đã lưu tạm đơn hàng: {hd.MaHoaDon}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void KhoiPhucHoaDon(HoaDonTam hd)
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
                    flowLayoutPanel1.Controls.Clear();
                }
            }

            foreach (Control ctrl in hd.DanhSachKhungMonHang)
            {
                flowLayoutPanel1.Controls.Add(ctrl);
            }

            danhSachHoaDonTam.Remove(hd);
            pnlDropdownThongBao.Visible = false;
            TinhTongDonHang();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- CHỐT CHẶN BẮT LỖI TRƯỚC KHI MỞ FORM THANH TOÁN ---
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    if (card.SoLuong <= 0)
                    {
                        MessageBox.Show($"Sản phẩm '{card.TenSP}' đang có số lượng không hợp lệ ({card.SoLuong}).\nVui lòng nhập số lớn hơn 0!", "Lỗi số lượng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Khóa luồng, cấm mở giao diện thanh toán
                    }

                    if (dictTonKho.ContainsKey(card.MaSP) && card.SoLuong > dictTonKho[card.MaSP])
                    {
                        MessageBox.Show($"Không thể thanh toán!\nSản phẩm '{card.TenSP}' vượt quá tồn kho.\n(Tồn thực tế: {dictTonKho[card.MaSP]} - Số lượng đang nhập: {card.SoLuong}).", "Thiếu hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Khóa luồng
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
                flowLayoutPanel1.Controls.Clear();
                TinhTongDonHang();
                LoadDuLieuBanDau();
            }
        }
        #endregion

        #region 4. LOGIC GIỎ HÀNG

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

            label6.Text = tongSanPham.ToString();
            label7.Text = tongSoLuong.ToString();
            label8.Text = "0";
            label9.Text = tongTienHang.ToString("N0");
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
            // BẮT LỖI 1: Kho hết sạch hàng thì cấm thêm
            if (tonKhoTong <= 0)
            {
                MessageBox.Show($"Sản phẩm '{tenSP}' đã hết hàng trong kho!", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ghi chép tồn kho vào từ điển để kiểm tra lúc gõ tay
            if (!dictTonKho.ContainsKey(maSP))
            {
                dictTonKho.Add(maSP, tonKhoTong);
            }

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card && card.MaSP == maSP)
                {
                    // BẮT LỖI 2: Nếu cộng thêm 1 mà vượt tồn kho thì chặn lại
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

            int sttMoi = flowLayoutPanel1.Controls.Count;
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang c)
                {
                    c.GanSTT(sttMoi);
                    sttMoi--;
                }
            }

            TinhTongDonHang();
        }
        // TÍNH NĂNG GỢI Ý: Tìm kiếm ngay khi gõ
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDuLieuBanDau(); // Xóa trắng thì trả lại toàn bộ SP
                return;
            }

            ThuNgan_BLL thuNganBLL = new ThuNgan_BLL();
            dsspToanBo = thuNganBLL.TimKiemSanPham(keyword);

            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;
            currentPage = 1;
            HienThiDanhSachSanPham();
        }

        // TÍNH NĂNG QUÉT MÃ: Chỉ bắt Enter nếu khớp mã vạch thì đẩy luôn vào giỏ
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maCanTim = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(maCanTim)) return;

                var spQuetChuan = dsspToanBo.Find(x => x.MaVach == maCanTim);
                if (spQuetChuan != null)
                {
                    ThemMonHangVaoDanhSach(spQuetChuan.MaVach, spQuetChuan.TenSP, spQuetChuan.GiaBanHienTai, spQuetChuan.SanPhamID, spQuetChuan.TonKhoTong);
                    txtTimKiem.Clear();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã vạch này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region 5. LOGIC HIỂN THỊ & PHÂN TRANG SẢN PHẨM

        private void LoadDuLieuBanDau()
        {
            ThuNgan_BLL spBLL = new ThuNgan_BLL();
            dsspToanBo = spBLL.LayDanhSachTrungBay(); // Form chỉ việc gọi BLL cung cấp dữ liệu

            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;

            currentPage = 1;
            HienThiDanhSachSanPham();
        }

        private void HienThiDanhSachSanPham()
        {
            flowLayoutPanel2.SuspendLayout();

            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                if (ctrl is ProductCard card && card.ProductImage != null)
                {
                    card.ProductImage.Dispose();
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

        // Logic load ảnh thuần túy của giao diện, đã loại bỏ block catch thừa
        private void LoadProductImage(string imageNameFromDatabase, ProductCard productCard)
        {
            string imageFolder = Path.Combine(Application.StartupPath, "Images");
            string fullImagePath = Path.Combine(imageFolder, imageNameFromDatabase ?? "");
            string defaultImagePath = Path.Combine(imageFolder, "NoImage.png");

            string pathToLoad = File.Exists(fullImagePath) ? fullImagePath : (File.Exists(defaultImagePath) ? defaultImagePath : null);

            if (pathToLoad != null)
            {
                using (FileStream fs = new FileStream(pathToLoad, FileMode.Open, FileAccess.Read))
                {
                    productCard.ProductImage = Image.FromStream(fs);
                }
            }
            else
            {
                productCard.ProductImage = null;
            }
        }

        #endregion

        #region 6. CÁC SỰ KIỆN KHÁC

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