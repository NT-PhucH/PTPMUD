using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_
{
    public class TrungBaySP_DTO
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }
            public string TenSanPham { get; set; }

            public int DonGia { get; set; }

            public string MaLoai { get; set; }
            public string TenLoai { get; set; }

            public int soLuongTonKho { get; set; }

            public string HinhAnh { get; set; }
    }
}
