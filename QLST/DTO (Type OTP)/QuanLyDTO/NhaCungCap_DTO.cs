// ===================================================
// File: NhaCungCap_DTO.cs
// Đặt vào: DTO (Type OTP) > QuanLyDTO
// ===================================================
namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    public class NhaCungCap_DTO
    {
        public int NhaCungCapID { get; set; }
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        // Thống kê nhanh (dùng trong danh sách)
        public int TongPhieuNhap { get; set; }
        public long TongTienNhap { get; set; }
    }
}