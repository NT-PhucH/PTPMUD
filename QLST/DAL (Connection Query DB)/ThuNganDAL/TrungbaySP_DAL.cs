using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLST.DAL__Connection_Query_DB_
{
    public class TrungBaySP_DAL
    {
        public List<TrungBaySP_DTO> GetAllSanPham()
        {
            List<TrungBaySP_DTO> list = new List<TrungBaySP_DTO>();

            DataTable data =
                DataProvider.Instance.ExecuteQuery(
                "SELECT SanPhamID, MaVach, TenSP, GiaBanHienTai, LoaiSanPhamID, TonKhoTong, HinhAnh  FROM SanPham");

            foreach (DataRow row in data.Rows)
            {
                TrungBaySP_DTO sanPham = new TrungBaySP_DTO()
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    MaSanPham = row["MaVach"].ToString(),
                    TenSanPham = row["TenSP"].ToString(),
                    DonGia = Convert.ToInt32(row["GiaBanHienTai"]),
                    MaLoai = row["LoaiSanPhamID"].ToString(),
                    soLuongTonKho = Convert.ToInt32(row["TonKhoTong"]),
                    HinhAnh = row["HinhAnh"].ToString()
                };
                list.Add(sanPham);
            }
            return list;
        }
    }
}