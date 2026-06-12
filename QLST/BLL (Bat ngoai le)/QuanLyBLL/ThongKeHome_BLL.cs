using QLST.DAL__Connection_Query_DB_;
using System;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class ThongKeHome_BLL
    {
        private ThongKeHome_DAL dal = new ThongKeHome_DAL();

        public double LayDoanhThuNgay(DateTime ngay)
        {
            return dal.GetDoanhThuTheoNgay(ngay);
        }
    }
}