using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class TrungBaySP_BLL
    {
        private TrungBaySP_DAL dssp = new TrungBaySP_DAL();

        public List<TrungBaySP_DTO> GetSanPham()
        {
            return dssp.GetAllSanPham();
        }
    }
}