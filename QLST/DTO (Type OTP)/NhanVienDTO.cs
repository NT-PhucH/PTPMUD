using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string SoDienThoai { get; set; }
        public string CaLamViec { get; set; }
        public bool TrangThai { get; set; }
        public string DuongDanAnh { get; set; }
    }
}
