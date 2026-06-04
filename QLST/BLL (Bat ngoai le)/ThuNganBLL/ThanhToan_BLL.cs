using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.ThuNganOTP;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class ThanhToan_BLL
    {
        private ThanhToan_DAL dal = new ThanhToan_DAL();

        public bool XuLyThanhToan(string maHD, int nhanVienID, long tongTien, string phuongThucTT, long tienKhachDua, long tienThua, List<ThanhToan_DTO> dsChiTiet, out string thongBao)
        {
            // Kiểm tra nghiệp vụ an toàn: Giỏ hàng trống không cho phép gửi xuống DB
            if (dsChiTiet == null || dsChiTiet.Count == 0)
            {
                thongBao = "Giỏ hàng rỗng, không thể xử lý thanh toán!";
                return false;
            }

            // Gọi xuống tầng dữ liệu xử lý tiếp
            return dal.ThucHienThanhToan(maHD, nhanVienID, tongTien, phuongThucTT, tienKhachDua, tienThua, dsChiTiet, out thongBao);
        }
    }
}