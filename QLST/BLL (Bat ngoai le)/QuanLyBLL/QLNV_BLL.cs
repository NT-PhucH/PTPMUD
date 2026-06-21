using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class QLNV_BLL
    {
        private readonly QLNV_DAL _dal = new QLNV_DAL();

        public List<NhanVienDTO> Search(string keyword, int roleFilter, bool showInactive)
            => _dal.Search(keyword, roleFilter, showInactive);

        public string SinhMaTuDong() => _dal.GenerateMaNV();

        public (bool ok, string msg) Save(NhanVienDTO nv, bool isAdd)
        {
            // 1. Validation Null
            if (string.IsNullOrWhiteSpace(nv.MaNV) || string.IsNullOrWhiteSpace(nv.TenNV) ||
                string.IsNullOrWhiteSpace(nv.Username) || string.IsNullOrWhiteSpace(nv.Password))
                return (false, "Vui lòng nhập đầy đủ: Mã, Tên, Username và Password!");

            if (nv.Role <= 0) return (false, "Vui lòng chọn Vai trò (Role) cho nhân viên!");

            // 2. Validation Unique
            if (_dal.IsMaNV_Or_Username_Exist(nv.MaNV, nv.Username, isAdd ? 0 : nv.NhanVienID))
                return (false, "Mã Nhân viên hoặc Username đã tồn tại trong hệ thống!");

            // 3. Thực thi
            bool success = isAdd ? _dal.Insert(nv) : _dal.Update(nv);
            return success ? (true, "Lưu thông tin thành công!") : (false, "Lỗi cơ sở dữ liệu!");
        }

        

        public bool ToggleTrangThai(int id)
        {
            return _dal.ToggleTrangThai(id);
        }
    }
}
