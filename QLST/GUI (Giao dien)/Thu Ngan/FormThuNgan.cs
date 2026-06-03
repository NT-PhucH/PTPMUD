using QLST.BLL__Bat_ngoai_le_;
using QLST.DTO__Type_OTP_;
using QLST.GUI__Giao_dien_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        #region 1. HẰNG SỐ & BIẾN TOÀN CỤC

        // Cấu hình phân trang sản phẩm
        private int currentPage = 1;
        private readonly int pageSize = 18; // 3 cột x 6 hàng
        private int totalPages = 1;
        private List<SanPhamDTO> dsspToanBo = new List<SanPhamDTO>();

        // Cấu hình giao diện lưới sản phẩm
        private readonly int SO_COT = 3;
        private readonly int SO_HANG = 6;
        private readonly int KHOANG_CACH = 10;

        // Thành phần UI động cho Dropdown Thông báo (Hóa đơn tạm)
        private Panel pnlDropdownThongBao;
        private FlowLayoutPanel flpDanhSachThongBao;
        private readonly List<HoaDonTam> danhSachHoaDonTam = new List<HoaDonTam>();

        // Lớp cấu trúc dữ liệu lưu trữ hóa đơn tạm thời
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

            chuyenTrang1.BamNutTrai += ChuyenTrang1_BamNutTrai;
            chuyenTrang1.BamNutPhai += ChuyenTrang1_BamNutPhai;
        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {
            // 1. Giữ nguyên các hàm nạp dữ liệu cũ của bạn
            LoadDuLieuBanDau();
            KhoiTaoGiaoDienThongBao();

            // 2. XÓA BỎ đoạn code tạo nút Đăng xuất bằng tay trước đó (vì Designer đã có sẵn nút "đăngXuấtToolStripMenuItem")
            // Bạn chỉ cần gán sự kiện Click trực tiếp cho nút đã thiết kế ngoài giao diện:
            đăngXuấtToolStripMenuItem.Click -= MenuDangXuat_Click;
            đăngXuấtToolStripMenuItem.Click += MenuDangXuat_Click;

            // 3. Nếu bạn muốn viết logic cho nút "Tích điểm" khi ấn vào, hãy gán luôn tại đây:
            // tíchĐiểmToolStripMenuItem.Click += (s, ev) => { MessageBox.Show("Chức năng tích điểm"); };
        }


        private void FormThuNgan_SizeChanged(object sender, EventArgs e)
        {
            // Tránh chia cho 0 hoặc tính toán khi chưa có dữ liệu sản phẩm
            if (dsspToanBo != null && dsspToanBo.Count > 0)
            {
                HienThiDanhSachSanPham();
            }
        }

        #endregion

        #region 3. LOGIC XỬ LÝ HÓA ĐƠN TẠM (DROPDOWN THÔNG BÁO)

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

            // Cấu hình nút chuông thông báo (pictureBox3)
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

                // Hiệu ứng Hover chuột
                pnlItem.MouseEnter += (s, e) => pnlItem.BackColor = Color.FromArgb(60, 60, 60);
                pnlItem.MouseLeave += (s, e) => pnlItem.BackColor = Color.Transparent;

                // Sự kiện click khôi phục hóa đơn
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
                // Định vị hộp thông báo hiển thị ngay dưới nút chuông
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
            TinhTongDonHang(); // Tính toán lại tiền ngay khi khôi phục đơn
        }

        #endregion

        #region 4. LOGIC GIỎ HÀNG (BÊN TRÁI MÀN HÌNH)

        public void TinhTongDonHang()
        {
            int tongSanPham = flowLayoutPanel1.Controls.Count;
            int tongSoLuong = 0;
            decimal tongTienHang = 0;
            decimal giamGia = 0;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card)
                {
                    tongSoLuong += card.SoLuong;
                    tongTienHang += card.ThanhTien;
                }
            }

            decimal thanhTien = tongTienHang - giamGia;

            // Cập nhật thông tin lên giao diện dựa theo ID các control label
            label6.Text = tongSanPham.ToString();
            label7.Text = tongSoLuong.ToString();
            label8.Text = giamGia.ToString("N0");
            label9.Text = thanhTien.ToString("N0");
        }

        private void KhungMonHang_DuLieuThayDoi(object sender, EventArgs e)
        {
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

        private void ThemMonHangVaoDanhSach(string maSP, string tenSP, decimal donGia)
        {
            // Nếu sản phẩm đã tồn tại, tăng số lượng lên 1
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card && card.MaSP == maSP)
                {
                    card.TangSoLuong();
                    return;
                }
            }

            // Tạo thẻ món hàng mới nếu chưa có trong giỏ
            KhungMonHang cardMoi = new KhungMonHang();
            cardMoi.DuLieuThayDoi += KhungMonHang_DuLieuThayDoi;
            cardMoi.CapNhatThongTin(maSP, tenSP, donGia);
            cardMoi.Width = flowLayoutPanel1.ClientSize.Width - cardMoi.Margin.Left - cardMoi.Margin.Right - 5;

            flowLayoutPanel1.Controls.Add(cardMoi);
            flowLayoutPanel1.Controls.SetChildIndex(cardMoi, 0); // Đẩy món mới quét lên trên cùng

            // Đánh lại số thứ tự (STT) cho các mặt hàng
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

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maCanTim = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(maCanTim))
                {
                    ThemMonHangVaoDanhSach(maCanTim, "Tên Sản Phẩm Quét Được", 150000);
                    txtTimKiem.Clear();
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0) 
    {
                MessageBox.Show("Không có sản phẩm nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy số tiền hiện tại trên giao diện giỏ hàng
            decimal.TryParse(label9.Text.Replace(",", ""), out decimal tongTien);

    HĐ formHoaDon = new HĐ(); 
    formHoaDon.TongTienCanThu = tongTien; // <--- Truyền tiền cực kỳ khoa học qua thuộc tính vừa tạo
            formHoaDon.StartPosition = FormStartPosition.CenterParent; 

    DialogResult ketQua = formHoaDon.ShowDialog(); 

    if (ketQua == DialogResult.Retry)
            {
                LuuTamHoaDonHienTai(); 
    }
            else if (ketQua == DialogResult.OK)
            {
                // Thanh toán hoàn tất -> Xóa sạch giỏ hàng hiện tại để tiếp tục làm việc
                flowLayoutPanel1.Controls.Clear(); 
        TinhTongDonHang(); 
        MessageBox.Show("Giao dịch thanh toán hoàn tất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region 5. LOGIC HIỂN THỊ & PHÂN TRANG SẢN PHẨM (BÊN PHẢI MÀN HÌNH)

        private void LoadDuLieuBanDau()
        {
            SanPhamBLL spBLL = new SanPhamBLL();
            dsspToanBo = spBLL.GetSanPham();

            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;

            currentPage = 1;
            HienThiDanhSachSanPham();
        }

        private void HienThiDanhSachSanPham()
        {
            flowLayoutPanel2.SuspendLayout();

            // Giải phóng ảnh cũ của các Card trước khi clear để tránh rò rỉ bộ nhớ (Memory Leak)
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

            // Thuật toán lấy sản phẩm phân trang dựa theo LINQ Skip - Take
            var danhSachTrangHienTai = dsspToanBo.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            foreach (SanPhamDTO sp in danhSachTrangHienTai)
            {
                ProductCard card = new ProductCard
                {
                    ProductName = sp.TenSanPham,
                    ProductPrice = sp.DonGia.ToString("N0"),
                    Width = chieuRongThe,
                    Height = chieuCaoThe,
                    Margin = new Padding(KHOANG_CACH / 2)
                };

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
                decimal giaTien = 0;
                decimal.TryParse(clickedCard.ProductPrice.Replace(",", ""), out giaTien);
                ThemMonHangVaoDanhSach(clickedCard.ProductName, clickedCard.ProductName, giaTien);
            }
        }

        private void LoadProductImage(string imageNameFromDatabase, ProductCard productCard)
        {
            string imageFolder = Path.Combine(Application.StartupPath, "Images");
            string fullImagePath = Path.Combine(imageFolder, imageNameFromDatabase ?? "");
            string defaultImagePath = Path.Combine(imageFolder, "NoImage.png");

            string pathToLoad = File.Exists(fullImagePath) ? fullImagePath : (File.Exists(defaultImagePath) ? defaultImagePath : null);

            if (pathToLoad != null)
            {
                try
                {
                    // Tối ưu đọc file qua MemoryStream để không bị lock (khóa) file ảnh gốc ngoài ổ đĩa
                    using (FileStream fs = new FileStream(pathToLoad, FileMode.Open, FileAccess.Read))
                    {
                        productCard.ProductImage = Image.FromStream(fs);
                    }
                }
                catch
                {
                    productCard.ProductImage = null;
                }
            }
            else
            {
                productCard.ProductImage = null;
            }
        }

        #endregion

        #region 6. CÁC SỰ KIỆN KHÁC

        private void panelAccount_MouseEnter(object sender, EventArgs e)
        {
            cmsAccount.Show(panelAccount, new Point(0, panelAccount.Height));
        }
        private void MenuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // 1. Chạy một luồng ứng dụng mới độc lập bắt đầu từ Form Đăng nhập
                // Thay "FormDangNhap" bằng đúng tên Class Form đăng nhập của dự án của bạn
                System.Threading.Thread t = new System.Threading.Thread(() => Application.Run(new FormLogin()));
                t.SetApartmentState(System.Threading.ApartmentState.STA);
                t.Start();

                // 2. Đóng và hủy hoàn toàn Form hiện tại cùng tất cả tài nguyên đi kèm
                this.Close();
            }
        }

        #endregion
    }
}