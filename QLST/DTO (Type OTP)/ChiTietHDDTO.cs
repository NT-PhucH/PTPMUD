using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_
{
    public class ChiTietHDDTO
    {
        // Thuộc tính từ bảng SanPham & ChiTietHoaDon
        public string MaVach { get; set; }

        // Cần thêm TenSP để khi khôi phục đơn, UI biết tên SP để hiển thị
        public string TenSP { get; set; }

        public int SoLuongMua { get; set; }
        public decimal DongGiaBan { get; set; }

        // Field tính toán, không cần set
        public decimal ThanhTien
        {
            get { return SoLuongMua * DongGiaBan; }
        }
    }
}
