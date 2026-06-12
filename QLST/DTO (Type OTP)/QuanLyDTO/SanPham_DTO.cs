// ===================================================
// File: SanPham_DTO.cs
// Đặt vào: DTO (Type OTP) > QuanLyDTO
// ===================================================
using System;

namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    public class SanPham_DTO
    {
        public int SanPhamID { get; set; }
        public string MaVach { get; set; }
        public string TenSP { get; set; }
        public int GiaBanHienTai { get; set; }
        public int LoaiSanPhamID { get; set; }
        public string TenLoai { get; set; }
        public int TonKhoTong { get; set; }
        public string HinhAnh { get; set; }
    }

    public class LoaiSanPham_DTO
    {
        public int LoaiSanPhamID { get; set; }
        public string TenLoai { get; set; }
    }
}