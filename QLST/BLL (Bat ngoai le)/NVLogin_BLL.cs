using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class NVLogin_BLL
    {
        private NVLogin_DAL nhanVienDAL = new NVLogin_DAL();

        public QLNV_DTO Login(string username, string password, out string message)
        {
            message = string.Empty; //[cite: 3]

            // 1. Kiểm tra đầu vào rỗng
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) //[cite: 3]
            {
                message = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu."; //[cite: 3]
                return null; //[cite: 3]
            }

            try
            {
                // 2. Lấy thông tin user từ DB CHỈ DỰA VÀO USERNAME
                QLNV_DTO nv = nhanVienDAL.GetNhanVienByUsername(username);

                // 3. Kiểm tra User có tồn tại không
                if (nv == null)
                {
                    message = "Tên đăng nhập hoặc mật khẩu không đúng."; // Giữ nguyên thông báo chung chung để bảo mật[cite: 3]
                    return null;
                }

                // ==========================================
                // 4. KIỂM TRA MẬT KHẨU BẰNG BCRYPT
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, nv.Password);

                if (!isPasswordValid)
                {
                    message = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return null;
                }

                // 5. Kiểm tra trạng thái tài khoản
                if (!nv.TrangThai) //[cite: 3]
                {
                    message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên."; //[cite: 3]
                    return null; //[cite: 3]
                }

                // Đăng nhập thành công
                return nv; //[cite: 3]
            }
            catch (Exception ex) //[cite: 3]
            {
                message = "Lỗi chi tiết: " + ex.Message; //[cite: 3]
                return null; //[cite: 3]
            }
        }
    }
}
