// ===================================================
// File: KhachHang_DTO.cs
// Đặt vào: DTO (Type OTP) > QuanLyDTO
// ===================================================
using System;

namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    public class KhachHang_DTO
    {
        public int KhachHangID { get; set; }
        public string SDT { get; set; }
        public string TenKH { get; set; }
        public int DiemTichLuy { get; set; }

        // Thống kê
        public int TongHoaDon { get; set; }
        public long TongChiTieu { get; set; }
    }

    public class LichSuTichDiem_DTO
    {
        public int HoaDonID { get; set; }
        public string MaHD { get; set; }
        public DateTime ThoiGian { get; set; }
        public long TongTien { get; set; }
        public int DiemCong { get; set; }
    }
}