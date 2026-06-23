using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class QLHD_BLL
    {
        private readonly QLHD_DAL _dal = new QLHD_DAL();

        public List<HoaDon_DTO> Search(DateTime from, DateTime to, string phuongThuc, string keywordKH)
        {
            if (from > to) return new List<HoaDon_DTO>(); // Bắt lỗi logic ngày
            return _dal.Search(from, to, phuongThuc, keywordKH);
        }

        public List<ChiTietHoaDon_DTO> GetChiTiet(int hoaDonID)
        {
            return _dal.GetChiTiet(hoaDonID);
        }
    }
}
