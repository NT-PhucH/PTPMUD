// ===================================================
// File: Kho_BLL.cs
// Đặt vào: BLL (Bat ngoai le) > QuanLyBLL
// ===================================================
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class Kho_BLL
    {
        private readonly Kho_DAL _dal = new Kho_DAL();

        // ── PHIẾU NHẬP ────────────────────────────────────────────────────────
        public List<PhieuNhap_DTO> GetAllPhieuNhap() => _dal.GetAllPhieuNhap();

        public List<PhieuNhap_DTO> GetPhieuNhapByDate(DateTime from, DateTime to)
        {
            if (from > to) return new List<PhieuNhap_DTO>();
            return _dal.GetPhieuNhapByDate(from, to);
        }

        public List<ChiTietPhieuNhap_DTO> GetChiTietPhieuNhap(int phieuNhapID)
            => _dal.GetChiTietPhieuNhap(phieuNhapID);

        public (bool ok, string msg) TaoPhieuNhap(PhieuNhap_DTO phieu, List<ChiTietPhieuNhap_DTO> chiTiet)
        {
            if (phieu.NhaCungCapID <= 0)
                return (false, "Vui lòng chọn nhà cung cấp!");
            if (chiTiet == null || chiTiet.Count == 0)
                return (false, "Chưa có sản phẩm nào trong phiếu nhập!");
            foreach (var ct in chiTiet)
            {
                if (ct.SoLuongNhap <= 0)
                    return (false, $"Số lượng nhập của '{ct.TenSP}' phải lớn hơn 0!");
                if (ct.GiaNhap <= 0)
                    return (false, $"Giá nhập của '{ct.TenSP}' phải lớn hơn 0!");
            }
            phieu.MaPN = _dal.SinhMaPhieuNhap();
            bool ok = _dal.TaoPhieuNhap(phieu, chiTiet);
            return ok ? (true, "Nhập kho thành công!") : (false, "Nhập kho thất bại, thử lại!");
        }

        // ── PHIẾU XUẤT ────────────────────────────────────────────────────────
        public List<PhieuXuat_DTO> GetAllPhieuXuat() => _dal.GetAllPhieuXuat();

        public List<ChiTietPhieuXuat_DTO> GetChiTietPhieuXuat(int phieuXuatID)
            => _dal.GetChiTietPhieuXuat(phieuXuatID);

        public (bool ok, string msg) TaoPhieuXuat(PhieuXuat_DTO phieu, List<ChiTietPhieuXuat_DTO> chiTiet)
        {
            if (string.IsNullOrWhiteSpace(phieu.LyDo))
                return (false, "Vui lòng chọn lý do xuất kho!");
            if (chiTiet == null || chiTiet.Count == 0)
                return (false, "Chưa có sản phẩm nào trong phiếu xuất!");
            foreach (var ct in chiTiet)
            {
                if (ct.SoLuongXuat <= 0)
                    return (false, $"Số lượng xuất của '{ct.TenSP}' phải lớn hơn 0!");
            }
            phieu.MaPX = _dal.SinhMaPhieuXuat();
            return _dal.TaoPhieuXuat(phieu, chiTiet);
        }

        // ── CẢNH BÁO ──────────────────────────────────────────────────────────
        public List<CanhBaoKho_DTO> GetHangSapHet(int nguong = 10) => _dal.GetHangSapHet(nguong);
        public List<CanhBaoKho_DTO> GetHangSapHetHan(int soNgay = 30) => _dal.GetHangSapHetHan(soNgay);
    }
}