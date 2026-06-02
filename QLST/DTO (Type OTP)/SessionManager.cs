using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_
{
    public static class SessionManager
    {
        // Lưu toàn bộ object nhân viên để tiện lấy Mã, Tên, hoặc Phân quyền (Role)
        public static NhanVienDTO NhanVienDangNhap { get; set; }

        // Hàm kiểm tra xem đã đăng nhập chưa
        public static bool IsLoggedIn()
        {
            return NhanVienDangNhap != null;
        }

        // Hàm xóa dữ liệu khi Đăng xuất
        public static void DangXuat()
        {
            NhanVienDangNhap = null;
        }
    }
}
