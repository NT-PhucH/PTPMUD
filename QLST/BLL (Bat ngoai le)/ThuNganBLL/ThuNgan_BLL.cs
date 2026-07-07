using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;

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


        public List<LoaiSanPham_DTO> LayDanhSachLoaiSanPham()
        {
            try
            {
                var list = dal.LayDanhSachLoaiSanPham();

                // Chèn mục "Tất cả" với ID = 0 vào đầu danh sách
                list.Insert(0, new LoaiSanPham_DTO
                {
                    LoaiSanPhamID = 0,
                    TenLoai = "Tất cả sản phẩm",
                    TrangThai = true
                });

                return list;
            }
            catch (Exception)
            {
                return new List<LoaiSanPham_DTO>();
            }
        }

        public List<ThuNganSP_DTO> TimKiemSanPhamKemLoai(string keyword, int loaiSanPhamID)
        {
            try
            {
                // Tối ưu hóa: Nếu không có từ khóa và đang chọn "Tất cả" (ID = 0)
                if (string.IsNullOrWhiteSpace(keyword) && loaiSanPhamID <= 0)
                {
                    return dal.LayDanhSachSanPham();
                }

                return dal.TimKiemSanPhamKemLoai(keyword, loaiSanPhamID);
            }
            catch (Exception)
            {
                return new List<ThuNganSP_DTO>();
            }
        }
    }
}