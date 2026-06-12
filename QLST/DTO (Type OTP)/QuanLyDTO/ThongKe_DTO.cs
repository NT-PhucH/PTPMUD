// ===================================================
// File: ThongKe_DTO.cs
// Đặt vào: DTO (Type OTP) > QuanLyDTO
// ===================================================
using System;

namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    public class ThongKeDoanhThu_DTO
    {
        public string Ngay { get; set; }
        public long DoanhThu { get; set; }
        public int SoHoaDon { get; set; }
    }

    public class ThongKeSanPham_DTO
    {
        public int SanPhamID { get; set; }
        public string TenSP { get; set; }
        public string TenLoai { get; set; }
        public int SoLuongBan { get; set; }
        public long DoanhThu { get; set; }
        public long GiaNhapTB { get; set; }
        public long LoiNhuan { get; set; }
    }

    public class ThongKeTongQuan_DTO
    {
        public long DoanhThuHomNay { get; set; }
        public long DoanhThuTuan { get; set; }
        public long DoanhThuThang { get; set; }
        public int SoHoaDonHomNay { get; set; }
        public int SoSanPham { get; set; }
        public int SoKhachHang { get; set; }
        public long LoiNhuanThang { get; set; }
    }
}