using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class SanPhamBLL
    {
        private SanPhamDAL dssp = new SanPhamDAL();

        public List<SanPhamDTO> GetSanPham()
        {
            return dssp.GetAllSanPham();
        }
    }
}
