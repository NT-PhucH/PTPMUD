// ===================================================
// File: Setting_BLL.cs
// ===================================================
using QLST.DAL__Connection_Query_DB_.Core;
using QLST.DTO__Type_OTP_;

namespace QLST.BLL__Bat_ngoai_le_.Core
{
    public class Setting_BLL
    {
        private readonly Setting_DAL _dal = new Setting_DAL();
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

            // BỔ SUNG: Kiểm tra dữ liệu cấu hình VietQR hợp lệ
            if (!string.IsNullOrWhiteSpace(ts.SoTaiKhoan))
            {
                if (string.IsNullOrWhiteSpace(ts.NganHang))
                    return (false, "Vui lòng chọn ngân hàng khi đã nhập số tài khoản!");
                if (string.IsNullOrWhiteSpace(ts.TenTaiKhoan))
                    return (false, "Vui lòng nhập Tên tài khoản để hiển thị mã QR chính xác!");
            }

            bool ok = _dal.Update(ts);

            // Nếu lưu DB thành công thì cập nhật lại luôn cái Cache
            if (ok) _cache = ts;

            return ok ? (true, "✅ Lưu cài đặt thành công!") : (false, "⚠️ Lưu thất bại do lỗi hệ thống!");
        }
    }
}