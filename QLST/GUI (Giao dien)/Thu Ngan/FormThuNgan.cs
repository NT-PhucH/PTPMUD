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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        // ========================================================
        // 1. CÁC BIẾN TOÀN CỤC PHỤC VỤ PHÂN TRANG
        // ========================================================
        private int currentPage = 1;
        private int pageSize = 18; // Hiển thị 18 sản phẩm 1 trang (3 cột x 6 hàng)
        private int totalPages = 1;
        private List<SanPhamDTO> dsspToanBo = new List<SanPhamDTO>(); // Chứa toàn bộ dữ liệu kéo từ DB

        public FormThuNgan()
        {
            InitializeComponent();
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            chuyenTrang1.BamNutTrai += ChuyenTrang1_BamNutTrai;
            chuyenTrang1.BamNutPhai += ChuyenTrang1_BamNutPhai;
        }

        private void panelAccount_MouseEnter(object sender, EventArgs e)
        {
            cmsAccount.Show(panelAccount, new Point(0, panelAccount.Height));
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            HĐ formHoaDon = new HĐ();
            formHoaDon.StartPosition = FormStartPosition.CenterParent;
            formHoaDon.ShowDialog();
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

        private void ThemMonHang()
        {
            KhungMonHang cardMoi = new KhungMonHang();
            cardMoi.Width = flowLayoutPanel1.ClientSize.Width - cardMoi.Margin.Left - cardMoi.Margin.Right;
            flowLayoutPanel1.Controls.Add(cardMoi);
        }

        private void ThemMonHangVaoDanhSach(string maSP, string tenSP, decimal donGia)
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is KhungMonHang card && card.MaSP == maSP)
                {
                    card.TangSoLuong();
                    return;
                }
            }

            KhungMonHang cardMoi = new KhungMonHang();
            cardMoi.CapNhatThongTin(maSP, tenSP, donGia);

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
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maCanTim = txtTimKiem.Text;
                ThemMonHangVaoDanhSach(maCanTim, "Tên Sản Phẩm Quét Được", 150000);
                txtTimKiem.Clear();
            }
        }

        private void SanPhamBenPhai_Click(object sender, EventArgs e)
        {
            ThemMonHangVaoDanhSach("SP002", "Sản Phẩm Chọn Bằng Chuột", 200000);
        }

        // ========================================================
        // 2. LOGIC LẤY DỮ LIỆU & PHÂN TRANG (MỚI THÊM)
        // ========================================================

        // Hàm này CHỈ GỌI 1 LẦN khi load Form để kéo dữ liệu từ SQL lên RAM
        private void LoadDuLieuBanDau()
        {
            SanPhamBLL spBLL = new SanPhamBLL();
            dsspToanBo = spBLL.GetSanPham(); // Lấy tất cả sản phẩm

            // Tính toán tổng số trang
            totalPages = (int)Math.Ceiling((double)dsspToanBo.Count / pageSize);
            if (totalPages == 0) totalPages = 1;

            currentPage = 1;
            HienThiDanhSachSanPham(); // Gọi hàm vẽ màn hình
        }

        // Hàm này dùng để vẽ lại danh sách của Trang Hiện Tại
        private void HienThiDanhSachSanPham()
        {
            // Bật tắt chống giật màn hình
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.Controls.Clear();

            int soCot = 3;
            int soHang = 6;
            int khoangCach = 10;

            int chieuRongThe = (flowLayoutPanel2.ClientSize.Width - (khoangCach * (soCot + 1))) / soCot;
            int chieuCaoThe = (flowLayoutPanel2.ClientSize.Height - (khoangCach * (soHang + 1))) / soHang;

            // THUẬT TOÁN PHÂN TRANG: Cắt đúng dữ liệu của trang hiện tại
            var danhSachTrangHienTai = dsspToanBo.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            foreach (SanPhamDTO sp in danhSachTrangHienTai)
            {
                ProductCard card = new QLST.GUI__Giao_dien_.ProductCard();

                card.ProductName = sp.TenSanPham;
                card.ProductPrice = sp.DonGia.ToString("N0");
                LoadProductImage(sp.HinhAnh, card);

                card.Width = chieuRongThe;
                card.Height = chieuCaoThe;
                card.Margin = new Padding(khoangCach / 2);

                card.OnSelectProduct += Card_OnSelectProduct;

                flowLayoutPanel2.Controls.Add(card);
            }

            flowLayoutPanel2.ResumeLayout();

            
            chuyenTrang1.CapNhatThongTinTrang(currentPage, totalPages);
        }

        // ========================================================
        // 3. SỰ KIỆN NÚT CHUYỂN TRANG
        // (Bạn nhớ qua màn hình Designer, click đúp vào 2 nút Mũi Tên và trỏ vào 2 hàm này nhé)
        // ========================================================

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

        // ========================================================
        // 4. CÁC SỰ KIỆN FORM CÒN LẠI
        // ========================================================

        private void Card_OnSelectProduct(object sender, EventArgs e)
        {
            QLST.GUI__Giao_dien_.ProductCard clickedCard = sender as QLST.GUI__Giao_dien_.ProductCard;
            if (clickedCard != null)
            {
                decimal giaTien = 0;
                decimal.TryParse(clickedCard.ProductPrice.Replace(",", ""), out giaTien);
                ThemMonHangVaoDanhSach(clickedCard.ProductName, clickedCard.ProductName, giaTien);
            }
        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {
            // Thay vì gọi HienThiDanhSachSanPham, ta gọi hàm lấy dữ liệu 1 lần
            LoadDuLieuBanDau();
        }

        private void FormThuNgan_SizeChanged(object sender, EventArgs e)
        {
            if (dsspToanBo.Count > 0) // Tránh lỗi chia cho 0 khi Form vừa khởi tạo chưa kịp có dữ liệu
            {
                HienThiDanhSachSanPham(); // Resize thì chỉ việc vẽ lại trang hiện tại cho mượt
            }
        }

        private void LoadProductImage(string imageNameFromDatabase, ProductCard Product)
        {
            string projectFolder = Application.StartupPath;// lưu ảnh sản phẩm folder cạnh file .exe chinh pa: "...\PTPMUD\QLST\bin\Debug\Images"
            string imageFolder = Path.Combine(projectFolder, "Images");
            string fullImagePath = Path.Combine(imageFolder, imageNameFromDatabase);

            if (File.Exists(fullImagePath))
            {
                Product.ProductImage = Image.FromFile(fullImagePath);
            }
            else
            {
                string defaultImagePath = Path.Combine(imageFolder, "NoImage.png");
                if (File.Exists(defaultImagePath))
                {
                    Product.ProductImage = Image.FromFile(defaultImagePath);
                }
                else
                {
                    Product.ProductImage = null;
                }
            }
        }

        // Các hàm rỗng không dùng đến (Có thể giữ lại để không bị lỗi file Designer)
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }

    }
}