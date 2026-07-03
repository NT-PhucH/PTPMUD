// File: DTO/ThuNganDTO/HoaDonTam_DTO.cs
using System;
using System.Collections.Generic;

namespace QLST.DTO__Type_OTP_.ThuNganOTP
{
    public class HoaDonTam_DTO
    {
        public string MaHoaDon { get; set; }
        public DateTime ThoiGianLuu { get; set; }

        // Thay vì lưu List<Control>, ta lưu List chi tiết sản phẩm
        public List<ChiTietHoaDonIn_DTO> DanhSachChiTiet { get; set; }

        public HoaDonTam_DTO()
        {
            DanhSachChiTiet = new List<ChiTietHoaDonIn_DTO>();
        }
    }
}