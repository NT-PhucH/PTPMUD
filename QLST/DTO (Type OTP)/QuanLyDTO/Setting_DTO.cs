
namespace QLST.DTO__Type_OTP_
{
    public class Setting_DTO
    {
        public int ID { get; set; }
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string HotlineShip { get; set; }
        public string Email { get; set; }
        public int VAT { get; set; }
        public int DiemPer10K { get; set; }
        public int NguongHetHang { get; set; }
        public int NguongHetHan { get; set; }
        public string FooterHoaDon { get; set; }

        public string NganHang { get; set; } // Lưu mã BIN hoặc Tên viết tắt (VD: vcb, mbbank)
        public string SoTaiKhoan { get; set; }
        public string TenTaiKhoan { get; set; }
    }
}