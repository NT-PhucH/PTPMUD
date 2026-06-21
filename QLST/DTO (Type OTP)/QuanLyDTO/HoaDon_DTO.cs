using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_.QuanLyDTO
{
    public class HoaDon_DTO
    {
        public int HoaDonID { get; set; }
        public string MaHD { get; set; }
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public string SDT { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public long TongTienCung { get; set; }
        public string PhuongThucThanhToan { get; set; }
    }

    public class ChiTietHoaDon_DTO
    {
        public int MaCTHD { get; set; }
        public string TenSP { get; set; }
        public int SoLuongMua { get; set; }
        public long DonGiaBan { get; set; }
        public long ThanhTien { get; set; }
    }
}
