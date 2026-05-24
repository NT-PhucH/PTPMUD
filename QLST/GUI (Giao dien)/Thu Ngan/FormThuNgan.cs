using QLST.GUI__Giao_dien_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        public FormThuNgan()
        {
            InitializeComponent();
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            ThemMonHangVaoDanhSach("SP38473663252", "Thùng 48 hộp Milo 180ml", 20000000);
            ThemMonHangVaoDanhSach("8934868100904", "Dầu Gội DOVE phục hồi hư tổn 640g", 13900000);
        }

        

        private void panelAccount_MouseEnter(object sender, EventArgs e)
        {
            cmsAccount.Show(panelAccount, new Point(0, panelAccount.Height));
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Khởi tạo đối tượng FormHoaDon
            HĐ formHoaDon = new HĐ();

            // Đặt vị trí xuất hiện của FormHoaDon là ở giữa Form cha (FormThuNgan)
            formHoaDon.StartPosition = FormStartPosition.CenterParent;

            // Hiển thị FormHoaDon dưới dạng Modal Dialog (nằm đè lên và khóa Form dưới)
            formHoaDon.ShowDialog();
        }

        // Bắt sự kiện khi FlowLayoutPanel thay đổi kích thước (ví dụ khi bạn phóng to/thu nhỏ toàn màn hình)
        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.SuspendLayout();
            foreach (Control card in flowLayoutPanel1.Controls)
            {
                // Liên tục cập nhật lại chiều ngang mỗi khi Form bị kéo thả hoặc phóng to
                card.Width = flowLayoutPanel1.ClientSize.Width - card.Margin.Left - card.Margin.Right - 2;
            }
            flowLayoutPanel1.ResumeLayout();
        }

        // Hàm này dùng để thêm 1 món hàng mới vào danh sách (Không có tham số)
        private void ThemMonHang()
        {
            KhungMonHang cardMoi = new KhungMonHang();
            cardMoi.Width = flowLayoutPanel1.ClientSize.Width - cardMoi.Margin.Left - cardMoi.Margin.Right;
            flowLayoutPanel1.Controls.Add(cardMoi);
        }

        // Hàm thêm món hàng có đầy đủ tham số
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

        // ---------------------------------------------------------
        // SỰ KIỆN 1: KHI QUÉT MÃ VẠCH (Ở thanh TextBox tìm kiếm)
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maCanTim = txtTimKiem.Text;

                // (Tại đây: Bạn sẽ gọi Database để tìm Tên và Giá của sản phẩm có mã này)
                // Dưới đây là code giả lập gọi hàm thêm sản phẩm:
                ThemMonHangVaoDanhSach(maCanTim, "Tên Sản Phẩm Quét Được", 150000);

                // Quét xong thì xóa trắng ô nhập để quét món tiếp theo
                txtTimKiem.Clear();
            }
        }

        // ---------------------------------------------------------
        // SỰ KIỆN 2: KHI CLICK CHỌN SẢN PHẨM TỪ BẢNG BÊN PHẢI
        private void SanPhamBenPhai_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ sản phẩm được click và gọi hàm thêm
            ThemMonHangVaoDanhSach("SP002", "Sản Phẩm Chọn Bằng Chuột", 200000);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // 2. File FormThuNgan.cs (Bổ sung vào khu vực code hiện tại)
        private void HienThiDanhSachSanPham()
        {
            flowLayoutPanel2.Controls.Clear();

            int soCot = 3;
            int soHang = 6;
            int khoangCach = 10;

            int chieuRongThe = (flowLayoutPanel2.ClientSize.Width - (khoangCach * (soCot + 1))) / soCot;
            int chieuCaoThe = (flowLayoutPanel2.ClientSize.Height - (khoangCach * (soHang + 1))) / soHang;

            for (int i = 0; i < soCot * soHang; i++)
            {
                QLST.GUI__Giao_dien_.ProductCard card = new QLST.GUI__Giao_dien_.ProductCard();

                card.ProductName = "Tên sản phẩm " + (i + 1);
                card.ProductPrice = "100,000";

                card.Width = chieuRongThe;
                card.Height = chieuCaoThe;
                card.Margin = new Padding(khoangCach / 2);

                card.OnSelectProduct += Card_OnSelectProduct;

                flowLayoutPanel2.Controls.Add(card);
            }
        }

        private void Card_OnSelectProduct(object sender, EventArgs e)
        {
            QLST.GUI__Giao_dien_.ProductCard clickedCard = sender as QLST.GUI__Giao_dien_.ProductCard;
            if (clickedCard != null)
            {
                decimal giaTien = 0;
                decimal.TryParse(clickedCard.ProductPrice.Replace(",", ""), out giaTien);

                // Thay "MÃ_TẠM" bằng tên sản phẩm hoặc mã thực tế để phân biệt
                ThemMonHangVaoDanhSach(clickedCard.ProductName, clickedCard.ProductName, giaTien);
            }
        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {
            HienThiDanhSachSanPham();
        }

        private void FormThuNgan_SizeChanged(object sender, EventArgs e)
        {
            HienThiDanhSachSanPham();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
    }
}