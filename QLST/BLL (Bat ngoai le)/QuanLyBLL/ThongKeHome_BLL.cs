using QLST.DAL__Connection_Query_DB_;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class ThongKeHome_BLL
    {
        private ThongKeHome_DAL dal = new ThongKeHome_DAL();

        public double LayDoanhThuNgay(DateTime ngay)
        {
            return dal.GetDoanhThuTheoNgay(ngay);
        }
        public int LaySoLuongHoaDonNgay(DateTime ngay)
        {
            return dal.GetSoLuongHoaDonTheoNgay(ngay);
        }
        public List<string> LayDanhSachSapHetTon() => dal.GetHangSapHetTon();
        public List<string> LayDanhSachSapHetHan() => dal.GetHangSapHetHan();
    }
}