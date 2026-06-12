// ===================================================
// File: NhaCungCap_BLL.cs
// Đặt vào: BLL (Bat ngoai le) > QuanLyBLL
// ===================================================
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class NhaCungCap_BLL
    {
        private readonly NhaCungCap_DAL _dal = new NhaCungCap_DAL();

        public List<NhaCungCap_DTO> GetAll() => _dal.GetAll();

        public List<NhaCungCap_DTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return _dal.GetAll();
            return _dal.Search(keyword.Trim());
        }

        public (bool ok, string msg) Them(NhaCungCap_DTO ncc)
        {
            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                return (false, "Vui lòng nhập tên nhà cung cấp!");
            if (_dal.IsMaExists(ncc.MaNCC))
                return (false, "Mã NCC đã tồn tại!");
            ncc.MaNCC = string.IsNullOrWhiteSpace(ncc.MaNCC) ? _dal.SinhMaNCC() : ncc.MaNCC.Trim();
            return _dal.Insert(ncc) ? (true, "Thêm nhà cung cấp thành công!") : (false, "Thêm thất bại!");
        }

        public (bool ok, string msg) Sua(NhaCungCap_DTO ncc)
        {
            if (string.IsNullOrWhiteSpace(ncc.TenNCC))
                return (false, "Vui lòng nhập tên nhà cung cấp!");
            if (_dal.IsMaExists(ncc.MaNCC, ncc.NhaCungCapID))
                return (false, "Mã NCC đã tồn tại ở nhà cung cấp khác!");
            return _dal.Update(ncc) ? (true, "Cập nhật thành công!") : (false, "Cập nhật thất bại!");
        }

        public (bool ok, string msg) Xoa(int id)
        {
            bool ok = _dal.Delete(id);
            return ok ? (true, "Đã xóa nhà cung cấp!")
                      : (false, "Không thể xóa! NCC này còn liên kết với phiếu nhập hàng.");
        }
    }
}