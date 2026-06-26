using System;
using System.Collections.Generic;
using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class ThuNgan_BLL
    {
        private ThuNgan_DAL dal = new ThuNgan_DAL();

        public List<ThuNganSP_DTO> LayDanhSachTrungBay()
        {
            try
            {
                return dal.LayDanhSachSanPham();
            }
            catch (Exception)
            {
                // Bắt lỗi ngầm để bảo vệ Form UI, trả về danh sách rỗng nếu CSDL lỗi
                return new List<ThuNganSP_DTO>();
            }
        }

        public List<ThuNganSP_DTO> TimKiemSanPham(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return dal.LayDanhSachSanPham();
                }
                return dal.TimKiemSanPham(keyword.Trim());
            }
            catch (Exception)
            {
                // Bắt lỗi ngầm để bảo vệ Form UI
                return new List<ThuNganSP_DTO>();
            }
        }
    }
}