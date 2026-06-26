// ===================================================
// File: Kho_BLL.cs
// Đặt vào: BLL > QuanLyBLL
// ===================================================
using QLST.BLL__Bat_ngoai_le_.Core; // Để gọi Setting_BLL
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class Kho_BLL
    {
        private readonly Kho_DAL _dal = new Kho_DAL();
        private readonly Setting_BLL _settingBll = new Setting_BLL();

        // ── PHIẾU NHẬP ────────────────────────────────────────────────────
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
            if (phieu.NhaCungCapID <= 0) return (false, "Vui lòng chọn nhà cung cấp!");
            if (chiTiet == null || chiTiet.Count == 0) return (false, "Chưa có sản phẩm nào trong phiếu nhập!");

            foreach (var ct in chiTiet)
            {
                if (ct.SoLuongNhap <= 0) return (false, $"Số lượng nhập của '{ct.TenSP}' phải lớn hơn 0!");
                if (ct.GiaNhap <= 0) return (false, $"Giá nhập của '{ct.TenSP}' phải lớn hơn 0!");
                if (ct.HSD.HasValue && ct.HSD.Value.Date < DateTime.Today)
                    return (false, $"Hạn sử dụng của '{ct.TenSP}' đã qua ({ct.HSD.Value:dd/MM/yyyy})!");
            }
            phieu.MaPN = _dal.SinhMaPhieuNhap();
            try
            {
                _dal.TaoPhieuNhap(phieu, chiTiet);
                return (true, "Nhập kho thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi nhập kho: " + ex.Message);
            }
        }

        // ── PHIẾU XUẤT ────────────────────────────────────────────────────
        public List<PhieuXuat_DTO> GetAllPhieuXuat() => _dal.GetAllPhieuXuat();

        public List<ChiTietPhieuXuat_DTO> GetChiTietPhieuXuat(int phieuXuatID)
            => _dal.GetChiTietPhieuXuat(phieuXuatID);

        public (bool ok, string msg) TaoPhieuXuat(PhieuXuat_DTO phieu, List<ChiTietPhieuXuat_DTO> chiTiet)
        {
            if (string.IsNullOrWhiteSpace(phieu.LyDo)) return (false, "Vui lòng chọn lý do xuất kho!");
            if (chiTiet == null || chiTiet.Count == 0) return (false, "Chưa có sản phẩm nào trong phiếu xuất!");

            foreach (var ct in chiTiet)
            {
                if (ct.SoLuongXuat <= 0) return (false, $"Số lượng xuất của '{ct.TenSP}' phải lớn hơn 0!");
            }
            phieu.MaPX = _dal.SinhMaPhieuXuat();
            try
            {
                _dal.TaoPhieuXuat(phieu, chiTiet);
                return (true, "Xuất kho thành công!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // ── CẢNH BÁO ──────────────────────────────────────────────────────
        public List<CanhBaoKho_DTO> GetHangSapHet()
        {
            int nguong = _settingBll.GetCauHinh()?.NguongHetHang ?? 10;
            return _dal.GetHangSapHet(nguong);
        }

        public List<CanhBaoKho_DTO> GetHangSapHetHan()
        {
            int soNgay = _settingBll.GetCauHinh()?.NguongHetHan ?? 30;
            return _dal.GetHangSapHetHan(soNgay);
        }
    }
}