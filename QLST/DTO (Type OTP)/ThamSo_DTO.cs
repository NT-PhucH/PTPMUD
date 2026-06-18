// ===================================================
// File: ThamSo_DTO.cs
// Đặt vào: DTO (Type OTP) — đặt thẳng vào folder DTO, 
//          không cần subfolder vì dùng chung toàn app
// ===================================================
namespace QLST.DTO__Type_OTP_
{
    public class ThamSoHeThong_DTO
    {
        public int ID { get; set; }
        public string TenCuaHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string HotlineShip { get; set; }
        public string Email { get; set; }
        public int VAT { get; set; }   // %
        public int DiemPer10K { get; set; }   // điểm / 10.000đ
        public int NguongHetHang { get; set; }   // tồn kho <= x
        public int NguongHetHan { get; set; }   // ngày HSD <= x
        public string FooterHoaDon { get; set; }
    }
}