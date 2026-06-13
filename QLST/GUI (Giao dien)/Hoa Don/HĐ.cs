using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLST
{
    public partial class HĐ : Form
    {
        public decimal TongTienCanThu { get; set; } // Nhận từ FormThuNgan truyền sang
        private bool isFormatting = false; // Cờ hiệu để tránh lỗi lặp vô hạn khi format text

        public HĐ()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Load += HĐ_Load;

            // Sự kiện đóng Form
            picClose.Click += PicClose_Click;
            picAnHD.Click += PicAnHD_Click;
            btnKhongInHD.Click += BtnKhongInHD_Click;
            btnInHD.Click += BtnHoanTat_Click;

            txtGiamGia.TextChanged += O_NhapTien_TextChanged;
            txtVAT.TextChanged += O_NhapTien_TextChanged;
            txtThuKhac.TextChanged += O_NhapTien_TextChanged;
            txtKhachDua.TextChanged += O_NhapTien_TextChanged;

            // Cập nhật tên txtSDT ở đây
            txtSDT.TextChanged += O_NhapSDT_TextChanged;

            // THÊM DÒNG NÀY: Sự kiện gạt nút tích điểm
            toggleButton3.CheckedChanged += ToggleButton3_CheckedChanged;
        }

        private void HĐ_Load(object sender, EventArgs e)
        {
            // Nạp dữ liệu ban đầu lên UI
            lblTongTienHang.Text = TongTienCanThu.ToString("N0");
            label14.Text = "0";

            // THÊM 2 DÒNG NÀY: Ẩn phần quy đổi và tắt nút gạt tích điểm lúc ban đầu
            panel13.Visible = false;
            toggleButton3.Checked = false;

            // Tính toán ngay lần đầu mở form
            TinhToanHoaDon();
        }

        // ==========================================
        // KHU VỰC 1: HÀM XỬ LÝ LOGIC TÍNH TOÁN
        // ==========================================
        private void TinhToanHoaDon()
        {
            // 1. Lấy dữ liệu từ các ô nhập (nếu để trống thì tính là 0)
            decimal giamGia = ParseMoney(txtGiamGia.Text);
            decimal vat = ParseMoney(txtVAT.Text);
            decimal thuKhac = ParseMoney(txtThuKhac.Text);
            decimal tienKhachDua = ParseMoney(txtKhachDua.Text);

            // 2. Tính khách cần trả
            decimal khachCanTra = TongTienCanThu - giamGia + vat + thuKhac;
            if (khachCanTra < 0) khachCanTra = 0;

            // 3. Đẩy kết quả lên UI
            label10.Text = khachCanTra.ToString("N0");
            label16.Text = khachCanTra.ToString("N0");
            label17.Text = khachCanTra.ToString("N0");

            // ==========================================
            // LOGIC MỚI: TÍNH ĐIỂM QUY ĐỔI (Dựa trên Tổng tiền hàng ban đầu)
            // ==========================================
            // Lấy TongTienCanThu chia cho 1000 thay vì khachCanTra
            int diemQuyDoi = (int)(TongTienCanThu / 1000);

            // Hiển thị lên Label
            lblQuyDoiDiem.Text = "+" + diemQuyDoi.ToString("N0") + " điểm";


            // ==========================================
            // KIỂM TRA ĐỔI MÀU CHỮ TIỀN KHÁCH ĐƯA
            // ==========================================
            if (tienKhachDua > 0 && tienKhachDua < khachCanTra)
            {
                txtKhachDua.ForeColor = Color.Red; // Thiếu tiền -> Chữ đỏ
            }
            else
            {
                txtKhachDua.ForeColor = Color.Black; // Đủ tiền hoặc trống -> Chữ đen
            }

            // 4. Tính tiền thừa
            decimal tienThua = tienKhachDua - khachCanTra;
            if (tienKhachDua > 0 && tienThua >= 0)
            {
                lblTienThua.Text = tienThua.ToString("N0");
            }
            else
            {
                lblTienThua.Text = "0";
            }
        }

        // Hàm hỗ trợ: Chuyển đổi "1,000,000" về số thực tế 1000000 để máy tính hiểu
        private decimal ParseMoney(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            // Xóa dấu phẩy để parse
            string cleanInput = input.Replace(",", "").Replace(".", "");
            if (decimal.TryParse(cleanInput, out decimal result))
                return result;
            return 0;
        }

        // ==========================================
        // KHU VỰC 2: RÀNG BUỘC KÝ TỰ VÀ ĐỊNH DẠNG
        // ==========================================
        private void O_NhapTien_TextChanged(object sender, EventArgs e)
        {
            if (isFormatting) return;

            UnderlineTextBox txt = sender as UnderlineTextBox;
            if (txt == null) return;

            isFormatting = true;

            // Lưu lại vị trí con trỏ hiện tại và độ dài văn bản cũ
            int cursorPosition = txt.SelectionStart;
            int oldLength = txt.Text.Length;

            // Lọc lấy số
            string digitsOnly = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (decimal.TryParse(digitsOnly, out decimal value) && value > 0)
            {
                txt.Text = value.ToString("N0"); // Tự động thêm dấu phẩy
            }
            else
            {
                txt.Text = "";
            }

            // Tính toán lại vị trí con trỏ chuột cho mượt mà
            int newLength = txt.Text.Length;
            cursorPosition += (newLength - oldLength); // Bù trừ số dấu phẩy được sinh ra
            if (cursorPosition < 0) cursorPosition = 0;

            // Trả con trỏ về đúng vị trí
            txt.SelectionStart = cursorPosition;

            isFormatting = false;

            // Tự động tính toán lại Form
            TinhToanHoaDon();
        }
        private void O_NhapSDT_TextChanged(object sender, EventArgs e)
        {
            UnderlineTextBox txt = sender as UnderlineTextBox;
            if (txt != null)
            {
                // Lọc ký tự chỉ giữ lại số
                string digitsOnly = new string(txt.Text.Where(char.IsDigit).ToArray());
                if (txt.Text != digitsOnly)
                {
                    txt.Text = digitsOnly;
                }

                // LOGIC MỚI: Nếu ô SĐT bị xóa trống mà nút tích điểm đang bật, thì tự động tắt nút đi
                if (string.IsNullOrWhiteSpace(txt.Text) && toggleButton3.Checked)
                {
                    toggleButton3.Checked = false;
                }
            }
        }

        private void underlineTextBox5_Load(object sender, EventArgs e)
        {
            // Phương thức trống do Designer sinh ra, cứ giữ nguyên
        }

        // ==========================================
        // KHU VỰC 3: CÁC NÚT ĐIỀU HƯỚNG
        // ==========================================
        private void PicClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnKhongInHD_Click(object sender, EventArgs e)
        {
            decimal khachCanTra = ParseMoney(label17.Text);
            decimal tienKhachDua = ParseMoney(txtKhachDua.Text);

            if (tienKhachDua < khachCanTra)
            {
                // Chỉ nháy con trỏ lại vào ô nhập và không làm gì cả (vì chữ đã đỏ rồi)
                txtKhachDua.Focus();
                return;
            }

            // Nếu đủ tiền thì mới đóng form và báo thành công
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            // Vẫn phải chặn không cho thanh toán nếu chữ đang màu đỏ (tiền đang thiếu)
            decimal khachCanTra = ParseMoney(label17.Text);
            decimal tienKhachDua = ParseMoney(txtKhachDua.Text);

            if (tienKhachDua < khachCanTra)
            {
                // Chỉ nháy con trỏ lại vào ô nhập và không làm gì cả (vì chữ đã đỏ rồi)
                txtKhachDua.Focus();
                return;
            }

            // Nếu đủ tiền thì mới đóng form và báo thành công
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PicAnHD_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
        private void ToggleButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu người dùng gạt nút sang BẬT (Màu xanh)
            if (toggleButton3.Checked)
            {
                // Kiểm tra xem ô SĐT có trống không
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    // Nếu trống -> Báo lỗi, ép nút gạt quay về TẮT và ẩn Panel
                    MessageBox.Show("Vui lòng nhập Số điện thoại của khách hàng trước khi bật Tích điểm!", "Yêu cầu nhập SĐT", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    toggleButton3.Checked = false; // Trả về màu xám
                    panel13.Visible = false; // Tiếp tục ẩn
                    txtSDT.Focus(); // Nháy chuột luôn vào ô SĐT cho người dùng nhập
                }
                else
                {
                    // Đã có SĐT -> Cho phép bật và hiện Panel quy đổi điểm
                    panel13.Visible = true;

                    // (Mai sau bạn có CSDL thì sẽ gọi hàm kiểm tra SĐT ở cơ sở dữ liệu tại vị trí này)
                }
            }
            // Nếu người dùng gạt nút sang TẮT (Màu xám)
            else
            {
                panel13.Visible = false; // Ẩn Panel đi
            }
        }

        
    }
}