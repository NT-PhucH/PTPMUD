using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_
{
    public class ThuNgan_DAL
    {
        public List<ThuNganSP_DTO> LayDanhSachSanPham()
        {
            List<ThuNganSP_DTO> list = new List<ThuNganSP_DTO>();
            string query = "SELECT SanPhamID, MaVach, TenSP, GiaBanHienTai, HinhAnh, TonKhoTong, TrangThai FROM SanPham WHERE TrangThai = 1";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new ThuNganSP_DTO
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    MaVach = row["MaVach"].ToString(),
                    TenSP = row["TenSP"].ToString(),
                    GiaBanHienTai = Convert.ToInt32(row["GiaBanHienTai"]),
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    TonKhoTong = Convert.ToInt32(row["TonKhoTong"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                });
            }

            return list;
        }


        public List<LoaiSanPham_DTO> LayDanhSachLoaiSanPham()
        {
            List<LoaiSanPham_DTO> list = new List<LoaiSanPham_DTO>();
            string query = "SELECT LoaiSanPhamID, TenLoai, TrangThai FROM LoaiSanPham WHERE TrangThai = 1";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new LoaiSanPham_DTO
                {
                    LoaiSanPhamID = Convert.ToInt32(row["LoaiSanPhamID"]),
                    TenLoai = row["TenLoai"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                });
            }

            return list;
        }

        // 2. Tìm kiếm sản phẩm kết hợp Keyword và Mã Loại (kiểu int)
        public List<ThuNganSP_DTO> TimKiemSanPhamKemLoai(string keyword, int loaiSanPhamID)
        {
            List<ThuNganSP_DTO> list = new List<ThuNganSP_DTO>();
            string query = @"SELECT SanPhamID, MaVach, TenSP, GiaBanHienTai, HinhAnh, TonKhoTong, TrangThai 
                             FROM SanPham 
                             WHERE TrangThai = 1";

            List<SqlParameter> paramList = new List<SqlParameter>();

            // loaiSanPhamID > 0 nghĩa là người dùng chọn 1 loại cụ thể (không phải 'Tất cả')
            if (loaiSanPhamID > 0)
            {
                query += " AND LoaiSanPhamID = @LoaiSanPhamID";
                paramList.Add(new SqlParameter("@LoaiSanPhamID", loaiSanPhamID));
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += " AND (MaVach = @Keyword OR TenSP LIKE @LikeKeyword)";
                paramList.Add(new SqlParameter("@Keyword", keyword.Trim()));
                paramList.Add(new SqlParameter("@LikeKeyword", "%" + keyword.Trim() + "%"));
            }

            // Nếu paramList có phần tử thì gán sang mảng, ngược lại truyền null
            SqlParameter[] parameters = paramList.Count > 0 ? paramList.ToArray() : null;

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new ThuNganSP_DTO
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    MaVach = row["MaVach"].ToString(),
                    TenSP = row["TenSP"].ToString(),
                    GiaBanHienTai = Convert.ToInt32(row["GiaBanHienTai"]),
                    HinhAnh = row["HinhAnh"] != DBNull.Value ? row["HinhAnh"].ToString() : null,
                    TonKhoTong = Convert.ToInt32(row["TonKhoTong"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                });
            }

            return list;
        }
    }
}