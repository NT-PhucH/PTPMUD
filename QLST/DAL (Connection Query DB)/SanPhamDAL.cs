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
    public class SanPhamDAL
    {
        public List<SanPhamDTO> GetAllSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            DataTable data =
                DataProvider.Instance.ExecuteQuery(
                "SELECT MaVach, TenSP, GiaBanHienTai, MaLoai, TonKhoTong, HinhAnh  FROM SanPham");

            foreach (DataRow row in data.Rows)
            {
                SanPhamDTO sanPham = new SanPhamDTO()
                {
                    MaSanPham = row["MaVach"].ToString(),
                    TenSanPham = row["TenSP"].ToString(),
                    DonGia = Convert.ToInt32(row["GiaBanHienTai"]),
                    MaLoai = row["MaLoai"].ToString(),
                    soLuongTonKho = Convert.ToInt32(row["TonKhoTong"]),
                    HinhAnh = row["HinhAnh"].ToString()
                };
                list.Add(sanPham);
            }
            return list;
        }
    }
}
