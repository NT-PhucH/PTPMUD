using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST
{
    public partial class HĐ : Form
    {
        #region 1. THUỘC TÍNH (PROPERTIES) TRUYỀN DỮ LIỆU

        #region 1. THUỘC TÍNH (PROPERTIES) TRUYỀN DỮ LIỆU

        // Thuộc tính để nhận tổng tiền cần thu từ FormThuNgan truyền sang
        public decimal TongTienCanThu { get; set; }

        // 👉 BỔ SUNG 3 TÚI CHỨA DỮ LIỆU ĐỂ TRẢ VỀ CHO FORM THU NGÂN
        public string PhuongThucThanhToan { get; private set; }
        public long TienKhachDua { get; private set; }
        public long TienThua { get; private set; }

        #endregion

        #endregion

        #region 2. KHỞI TẠO FORM & SỰ KIỆN HỆ THỐNG

        public HĐ()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Load += HĐ_Load;

            // Đăng ký sự kiện thay đổi phương thức thanh toán
            radioTienMat.CheckedChanged += RadioTienMat_CheckedChanged;
            radioChuyenKhoan.CheckedChanged += RadioChuyenKhoan_CheckedChanged;

            // Đăng ký sự kiện nhập tiền khách đưa
            textKhachDua.TextChanged += TextKhachDua_TextChanged;

            // Đăng ký sự kiện click cho các nút chức năng
            picClose.Click += PicClose_Click;
            picAnHD.Click += PicAnHD_Click;
            button1.Click += BtnKhongInHD_Click;
            button2.Click += BtnHoanTat_Click;
        }

        private void HĐ_Load(object sender, EventArgs e)
        {
            // Hiển thị số tiền cần thu lên giao diện khi nạp Form
            lblTongThu.Text = TongTienCanThu.ToString("N0");
            lblTienThua.Text = "0";

            // Mặc định chọn phương thức Tiền mặt khi mở Form
            radioTienMat.Checked = true;
            pictureQR.Visible = false;
        }

        #endregion

        #region 3. LOGIC XỬ LÝ TÍNH TOÁN TIỀN MẶT & TIỀN THỪA

        private void TextKhachDua_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textKhachDua.Text))
            {
                lblTienThua.Text = "0";
                return;
            }

            // Bỏ các dấu phân cách nghìn nếu có để tính toán chính xác
            string cleanInput = textKhachDua.Text.Replace(",", "").Replace(".", "");

            if (decimal.TryParse(cleanInput, out decimal tienKhachDua))
            {
                // Tự động định dạng lại chuỗi tiền tệ khi người dùng đang nhập cho dễ nhìn (Ví dụ: 100,000)
                textKhachDua.TextChanged -= TextKhachDua_TextChanged;
                textKhachDua.Text = tienKhachDua.ToString("N0");
                textKhachDua.SelectionStart = textKhachDua.Text.Length; // Đẩy con trỏ chuột về cuối
                textKhachDua.TextChanged += TextKhachDua_TextChanged;

                // Tính tiền thừa trả khách
                decimal tienThua = tienKhachDua - TongTienCanThu;

                if (tienThua >= 0)
                {
                    lblTienThua.Text = tienThua.ToString("N0");
                    lblTienThua.ForeColor = Color.Green; // Tiền hợp lệ hiện màu xanh
                }
                else
                {
                    lblTienThua.Text = $"Thiếu: {Math.Abs(tienThua):N0}";
                    lblTienThua.ForeColor = Color.Red; // Thiếu tiền hiện màu đỏ
                }
            }
            else
            {
                lblTienThua.Text = "Không hợp lệ";
                lblTienThua.ForeColor = Color.Red;
            }
        }

        private void RadioTienMat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTienMat.Checked)
            {
                // Hiện các control nhập tiền mặt, ẩn ảnh QR code đi
                pictureQR.Visible = false; 
                txtKhachDua.Visible = true;
                textKhachDua.Visible = true;
                vnd1.Visible = true;
                txtTienThua.Visible = true;
                lblTienThua.Visible = true;
                vnd2.Visible = true;
            }
        }

        private void RadioChuyenKhoan_CheckedChanged(object sender, EventArgs e)
        {
            if (radioChuyenKhoan.Checked) 
            {
                // Hiện ảnh QR thanh toán, ẩn toàn bộ phần tính tiền thừa đi cho gọn giao diện
                pictureQR.Visible = true;
                txtKhachDua.Visible = false;
                textKhachDua.Visible = false;
                vnd1.Visible = false;
                txtTienThua.Visible = false;
                lblTienThua.Visible = false;
                vnd2.Visible = false;
            }
        }

        #endregion

        #region 4. SỰ KIỆN ĐIỀU HƯỚNG & ĐÓNG FORM (BUTTONS & PICTUREBOXES)

        // Bấm nút X hoặc nút "Không in HĐ" -> Hủy giao dịch thanh toán
        private void PicClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }

        private void BtnKhongInHD_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }

        // Bấm nút "Hoàn tất" -> Xác nhận thanh toán thành công thành công
        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            // 1. LẤY PHƯƠNG THỨC THANH TOÁN
            if (radioChuyenKhoan.Checked)
            {
                PhuongThucThanhToan = "Chuyển khoản";
                TienKhachDua = (long)TongTienCanThu; // Chuyển khoản thì coi như đưa vừa đủ
                TienThua = 0;
            }
            else
            {
                PhuongThucThanhToan = "Tiền mặt";

                // Kiểm tra tiền khách đưa
                string cleanInput = textKhachDua.Text.Replace(",", "").Replace(".", "");
                if (!decimal.TryParse(cleanInput, out decimal tienKhachDua) || tienKhachDua < TongTienCanThu)
                {
                    MessageBox.Show("Số tiền khách đưa chưa đủ hoặc không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng lại không cho thanh toán
                }

                // Lưu thông tin tiền bạc lại
                TienKhachDua = (long)tienKhachDua;
                TienThua = (long)(tienKhachDua - TongTienCanThu);
            }

            this.DialogResult = DialogResult.OK; // Trả về trạng thái hoàn thành giao dịch thành công
            this.Close();
        }

        // Bấm nút Ẩn Hóa Đơn -> Đưa đơn vào khay hệ thống chờ (Dropdown thông báo)
        private void PicAnHD_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry; // Trả về Retry để báo FormThuNgan chuyển vào bộ nhớ tạm
            this.Close(); 
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}