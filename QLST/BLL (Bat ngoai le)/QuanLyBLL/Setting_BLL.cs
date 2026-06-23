// ===================================================
// File: Setting_BLL.cs
// Đặt vào: BLL > CoreBLL (hoặc QuanLyBLL)
// ===================================================
using QLST.DAL__Connection_Query_DB_.Core;
using QLST.DTO__Type_OTP_;

namespace QLST.BLL__Bat_ngoai_le_.Core
{
    public class Setting_BLL
    {
        private readonly Setting_DAL _dal = new Setting_DAL();

        // Dùng Static Cache ở đây để lưu cấu hình dùng chung cho toàn App
        private static Setting_DTO _cache;

        public Setting_DTO GetCauHinh()
        {
            if (_cache == null) _cache = _dal.Get();
            return _cache;
        }

        public (bool ok, string msg) CapNhat(Setting_DTO ts)
        {
            if (string.IsNullOrWhiteSpace(ts.TenCuaHang))
                return (false, "Tên cửa hàng không được để trống!");

            bool ok = _dal.Update(ts);

            // Nếu lưu DB thành công thì cập nhật lại luôn cái Cache
            if (ok) _cache = ts;

            return ok ? (true, "✅ Lưu cài đặt thành công!") : (false, "⚠️ Lưu thất bại do lỗi hệ thống!");
        }
    }
}