
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class NhaCungCap_BLL
    {
        private readonly NhaCungCap_DAL _dal = new NhaCungCap_DAL();

        // ── LẤY TẤT CẢ VÀ TÌM KIẾM  ───────────────────────────
        public List<NhaCungCap_DTO> GetAll() => _dal.GetAll();

        public List<NhaCungCap_DTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return _dal.GetAll();
            return _dal.Search(keyword.Trim());
        }

        // ── THÊM  ─────────
        public (bool ok, string msg) Them(NhaCungCap_DTO ncc)
        {
            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                return (false, "Vui lòng nhập tên nhà cung cấp!");

            return _dal.Insert(ncc)
                ? (true, "Thêm nhà cung cấp thành công!")
                : (false, "Thêm thất bại!");
        }

        // ── SỬA  ───────────────────────────
        public (bool ok, string msg) Sua(NhaCungCap_DTO ncc)
        {
            if (ncc.NhaCungCapID <= 0)
                return (false, "Không xác định được ID nhà cung cấp!");

            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                return (false, "Vui lòng nhập tên nhà cung cấp!");

            return _dal.Update(ncc)
                ? (true, "Cập nhật thành công!")
                : (false, "Cập nhật thất bại!");
        }

        // ── THAY ĐỔI TRẠNG THÁI  ─────────
        public (bool ok, string msg) ThayDoiTrangThai(int id)
        {
            if (id <= 0)
                return (false, "Không xác định được ID!");

            return _dal.ToggleTrangThai(id)
                ? (true, "Đã thay đổi trạng thái nhà cung cấp!")
                : (false, "Lỗi hệ thống: Không thể thay đổi trạng thái!");
        }


        // ── KIỂM TRA NHÀ CUNG CẤP CÒN GIAO DỊCH KHÔNG ─────────
        public bool KiemTraNhaCungCapKhaDung(int nccId)
        {
            if (nccId <= 0) return false;
            return _dal.KiemTraTrangThaiGiaoDich(nccId);
        }
    }
}