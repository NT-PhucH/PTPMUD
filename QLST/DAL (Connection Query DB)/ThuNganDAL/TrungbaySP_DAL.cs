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
    public class TrungbaySP_DAL
    {
        public List<SanPhamDTO> GetAllSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            DataTable data =
                DataProvider.Instance.ExecuteQuery(
                @"
        SELECT 
            sp.MaVach, 
            sp.TenSP, 
            sp.GiaBanHienTai, 
            sp.MaLoai,
            sp.TonKhoTong,
            sp.HinhAnh,
            lsp.TenLoai
        FROM SanPham sp
        INNER JOIN LoaiSanPham lsp ON sp.MaLoai = lsp.MaLoai");

            foreach (DataRow row in data.Rows)
            {
                SanPhamDTO sanPham = new SanPhamDTO()
                {
                    MaSanPham = row["MaVach"].ToString(),
                    TenSanPham = row["TenSP"].ToString(),
                    DonGia = Convert.ToInt32(row["GiaBanHienTai"]),
                    MaLoai = row["MaLoai"].ToString(),
                    TenLoai = row["TenLoai"].ToString(),
                    soLuongTonKho = Convert.ToInt32(row["TonKhoTong"]),
                    HinhAnh = row["HinhAnh"].ToString()
                };
                list.Add(sanPham);
            }
            return list;
        }
    }
}
