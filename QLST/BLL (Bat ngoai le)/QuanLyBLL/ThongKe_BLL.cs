using QLST.DAL__Connection_Query_DB_;
using System;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class ThongKe_BLL
    {
        private ThongKe_DAL dal = new ThongKe_DAL();

        public double LayDoanhThuNgay(DateTime ngay)
        {
            return dal.GetDoanhThuTheoNgay(ngay);
        }
    }
}