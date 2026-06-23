// ===================================================
// File: HoaDonIn_DTO.cs
// Đặt vào: DTO (Type OTP)
// ===================================================
using System.Collections.Generic;

namespace QLST.DTO__Type_OTP_
{
    public class HoaDonIn_DTO
    {
        public Setting_DTO CauHinh { get; set; }

        // Thông tin chung
        public string MaHoaDon { get; set; } = "";
        public string TenThuNgan { get; set; } = "";
        public string MaThuNgan { get; set; } = "";
        public string TenKhach { get; set; } = "Khách lẻ";
        public string MaKhach { get; set; } = "";
        public int DiemTichLuy { get; set; } = 0;

        // Danh sách hàng hóa
        public List<ChiTietHoaDonIn_DTO> DanhSachSP { get; set; } = new List<ChiTietHoaDonIn_DTO>();

        // Tiền nong
        public long TongTienChua { get; set; } = 0;
        public long TienVAT { get; set; } = 0;
        public long TongTienSau { get; set; } = 0;
        public long TienKhachDua { get; set; } = 0;
        public long TienThua { get; set; } = 0;
        public string PhuongThucTT { get; set; } = "Tiền mặt";
        public bool AnTienThua { get; set; } = false;
    }

    public class ChiTietHoaDonIn_DTO
    {
        public int SanPhamID { get; set; }
        public string MaVach { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public long DonGia { get; set; }
        public long ThanhTien { get; set; }
    }
}