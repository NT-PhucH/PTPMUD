using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class NhanVienBLL
    {
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        public NhanVienDTO Login(string username, string password, out string message)
        {
            message = string.Empty;
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                message = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return null;
            }
            try
            {
                NhanVienDTO nv = nhanVienDAL.GetNhanVienByLogin(username, password);
                if (nv == null)
                {
                    message = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return null;
                }
                if (!nv.TrangThai)
                {
                    message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên.";
                    return null;
                }
                // Đăng nhập thành công
                return nv;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                message = "Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại sau.";
                return null;
            }
        }
    }
}
