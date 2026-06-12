// ===================================================
// File: ThongKe_BLL.cs
// Đặt vào: BLL (Bat ngoai le) > QuanLyBLL
// ===================================================
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class ThongKe_BLL
    {
        private readonly ThongKe_DAL _dal = new ThongKe_DAL();

        public ThongKeTongQuan_DTO GetTongQuan() => _dal.GetTongQuan();

        public List<ThongKeDoanhThu_DTO> GetDoanhThuTheoNgay(DateTime from, DateTime to)
        {
            if (from > to) return new List<ThongKeDoanhThu_DTO>();
            return _dal.GetDoanhThuTheoNgay(from, to);
        }

        public List<ThongKeDoanhThu_DTO> GetDoanhThuTheoThang(int nam)
            => _dal.GetDoanhThuTheoThang(nam);

        public List<ThongKeSanPham_DTO> GetTopSanPham(DateTime from, DateTime to, int top = 10)
        {
            if (from > to) return new List<ThongKeSanPham_DTO>();
            return _dal.GetTopSanPham(from, to, top);
        }
    }
}