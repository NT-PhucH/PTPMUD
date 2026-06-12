// ===================================================
// File: Kho_DTO.cs
// Đặt vào: DTO (Type OTP) > QuanLyDTO
// ===================================================
using System;

namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    // ── PHIẾU NHẬP ────────────────────────────────────────────────────────────
    public class PhieuNhap_DTO
    {
        public int PhieuNhapID { get; set; }
        public string MaPN { get; set; }
        public int NhaCungCapID { get; set; }
        public string TenNCC { get; set; }
        public int NhanVienID { get; set; }
        public string TenNV { get; set; }
        public DateTime NgayLap { get; set; }
        public long TongTienThanhToan { get; set; }
    }

    public class ChiTietPhieuNhap_DTO
    {
        public int MaChiTietPN { get; set; }
        public int PhieuNhapID { get; set; }
        public int SanPhamID { get; set; }
        public string TenSP { get; set; }
        public string MaVach { get; set; }
        public int SoLuongNhap { get; set; }
        public int GiaNhap { get; set; }
        public DateTime? NSX { get; set; }
        public DateTime? HSD { get; set; }
        public int SoLuongTonKho { get; set; }
    }

    // ── PHIẾU XUẤT ────────────────────────────────────────────────────────────
    public class PhieuXuat_DTO
    {
        public int PhieuXuatID { get; set; }
        public string MaPX { get; set; }
        public int NhanVienID { get; set; }
        public string TenNV { get; set; }
        public DateTime NgayXuat { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
    }

    public class ChiTietPhieuXuat_DTO
    {
        public int MaCTPX { get; set; }
        public int PhieuXuatID { get; set; }
        public int SanPhamID { get; set; }
        public string TenSP { get; set; }
        public string MaVach { get; set; }
        public int SoLuongXuat { get; set; }
        public int TonKhoHienTai { get; set; }
        public string GhiChu { get; set; }
    }

    // ── CẢNH BÁO KHO ──────────────────────────────────────────────────────────
    public class CanhBaoKho_DTO
    {
        public int SanPhamID { get; set; }
        public string MaVach { get; set; }
        public string TenSP { get; set; }
        public int TonKhoTong { get; set; }
        public DateTime? HSD { get; set; }
        public string TenLoai { get; set; }
        public string LoaiCanhBao { get; set; } // "SapHet" | "HetHan"
    }
}