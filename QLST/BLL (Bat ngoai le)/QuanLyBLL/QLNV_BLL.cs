using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class QLNV_BLL
    {
        private readonly QLNV_DAL _dal = new QLNV_DAL();

        public List<QLNV_DTO> Search(string keyword, int roleFilter, bool showInactive)
            => _dal.Search(keyword, roleFilter, showInactive);

        public string SinhMaTuDong() => _dal.GenerateMaNV();

        public (bool ok, string msg) Save(QLNV_DTO nv, bool isAdd)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(nv.TenNV) || string.IsNullOrWhiteSpace(nv.Username) || string.IsNullOrWhiteSpace(nv.Password))
                return (false, "Vui lòng nhập đầy đủ: Tên, Username và Password!");

            if (nv.Role <= 0) return (false, "Vui lòng chọn Vai trò (Role) cho nhân viên!");

            // 2. Phân tách báo lỗi trùng lặp rõ ràng
            if (isAdd)
            {
                if (string.IsNullOrWhiteSpace(nv.MaNV)) return (false, "Không xác định được Mã nhân viên!");
                if (_dal.IsMaNVExist(nv.MaNV)) return (false, "Mã Nhân viên này đã tồn tại!");
            }

            if (_dal.IsUsernameExist(nv.Username, isAdd ? 0 : nv.NhanVienID))
                return (false, "Tên đăng nhập (Username) này đã có người sử dụng. Vui lòng chọn tên khác!");

            // 3. Thực thi
            bool success = isAdd ? _dal.Insert(nv) : _dal.Update(nv);
            return success ? (true, "Lưu thông tin nhân viên thành công!") : (false, "Lỗi cơ sở dữ liệu!");
        }

        public (bool ok, string msg) ToggleTrangThai(int id)
        {
            if (id <= 0) return (false, "Không xác định được ID nhân viên!");
            bool success = _dal.ToggleTrangThai(id);
            return success ? (true, "Đã cập nhật trạng thái nhân viên!") : (false, "Lỗi khi cập nhật trạng thái!");
        }
    }
}