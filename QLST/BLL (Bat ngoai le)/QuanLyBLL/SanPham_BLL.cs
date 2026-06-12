// ===================================================
// File: SanPham_BLL.cs
// Đặt vào: BLL (Bat ngoai le) > QuanLyBLL
// ===================================================
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class SanPham_BLL
    {
        private readonly SanPham_DAL _dal = new SanPham_DAL();

        // ── LẤY DANH SÁCH ─────────────────────────────────────────────────────
        public List<SanPham_DTO> GetAll() => _dal.GetAll();

        public List<SanPham_DTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _dal.GetAll();
            return _dal.Search(keyword.Trim());
        }

        public List<SanPham_DTO> GetByLoai(int loaiID)
        {
            if (loaiID <= 0) return _dal.GetAll();
            return _dal.GetByLoai(loaiID);
        }

        public List<LoaiSanPham_DTO> GetAllLoai() => _dal.GetAllLoai();

        // ── THÊM ──────────────────────────────────────────────────────────────
        public (bool ok, string msg) ThemSanPham(SanPham_DTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaVach))
                return (false, "Vui lòng nhập mã vạch!");
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                return (false, "Vui lòng nhập tên sản phẩm!");
            if (sp.GiaBanHienTai <= 0)
                return (false, "Giá bán phải lớn hơn 0!");
            if (sp.LoaiSanPhamID <= 0)
                return (false, "Vui lòng chọn loại sản phẩm!");
            if (_dal.IsMaVachExists(sp.MaVach))
                return (false, "Mã vạch đã tồn tại!");

            bool result = _dal.Insert(sp);
            return result ? (true, "Thêm sản phẩm thành công!") : (false, "Thêm thất bại, thử lại!");
        }

        // ── SỬA ───────────────────────────────────────────────────────────────
        public (bool ok, string msg) SuaSanPham(SanPham_DTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaVach))
                return (false, "Vui lòng nhập mã vạch!");
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                return (false, "Vui lòng nhập tên sản phẩm!");
            if (sp.GiaBanHienTai <= 0)
                return (false, "Giá bán phải lớn hơn 0!");
            if (sp.LoaiSanPhamID <= 0)
                return (false, "Vui lòng chọn loại sản phẩm!");
            if (_dal.IsMaVachExists(sp.MaVach, sp.SanPhamID))
                return (false, "Mã vạch đã tồn tại ở sản phẩm khác!");

            bool result = _dal.Update(sp);
            return result ? (true, "Cập nhật thành công!") : (false, "Cập nhật thất bại, thử lại!");
        }

        // ── XÓA ───────────────────────────────────────────────────────────────
        public (bool ok, string msg) XoaSanPham(int sanPhamID)
        {
            bool result = _dal.Delete(sanPhamID);
            return result ? (true, "Đã xóa sản phẩm!") : (false, "Xóa thất bại, thử lại!");
        }

        // ── THÊM LOẠI ─────────────────────────────────────────────────────────
        public (bool ok, string msg) ThemLoai(string tenLoai)
        {
            if (string.IsNullOrWhiteSpace(tenLoai))
                return (false, "Vui lòng nhập tên loại!");
            bool result = _dal.InsertLoai(tenLoai.Trim());
            return result ? (true, "Đã thêm loại!") : (false, "Thêm loại thất bại!");
        }
    }
}