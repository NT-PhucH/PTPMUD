using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;

namespace QLST.DAL__Connection_Query_DB_
{
    public class ThuNgan_DAL
    {
        public List<ThuNganSP_DTO> LayDanhSachSanPham()
        {
            List<ThuNganSP_DTO> list = new List<ThuNganSP_DTO>();
            string connString = DB_Connection.GetConnectionString();
            string query = "SELECT SanPhamID, MaVach, TenSP, GiaBanHienTai, HinhAnh, TonKhoTong, TrangThai FROM SanPham WHERE TrangThai = 1";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThuNganSP_DTO
                            {
                                SanPhamID = Convert.ToInt32(reader["SanPhamID"]),
                                MaVach = reader["MaVach"].ToString(),
                                TenSP = reader["TenSP"].ToString(),
                                GiaBanHienTai = Convert.ToInt32(reader["GiaBanHienTai"]),
                                HinhAnh = reader["HinhAnh"] != DBNull.Value ? reader["HinhAnh"].ToString() : null,
                                TonKhoTong = Convert.ToInt32(reader["TonKhoTong"]),
                                TrangThai = Convert.ToBoolean(reader["TrangThai"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public List<ThuNganSP_DTO> TimKiemSanPham(string keyword)
        {
            List<ThuNganSP_DTO> list = new List<ThuNganSP_DTO>();
            string connString = DB_Connection.GetConnectionString();
            string query = @"SELECT SanPhamID, MaVach, TenSP, GiaBanHienTai, HinhAnh, TonKhoTong, TrangThai 
                             FROM SanPham 
                             WHERE TrangThai = 1 AND (MaVach = @Keyword OR TenSP LIKE @LikeKeyword)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", keyword);
                    cmd.Parameters.AddWithValue("@LikeKeyword", "%" + keyword + "%");

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThuNganSP_DTO
                            {
                                SanPhamID = Convert.ToInt32(reader["SanPhamID"]),
                                MaVach = reader["MaVach"].ToString(),
                                TenSP = reader["TenSP"].ToString(),
                                GiaBanHienTai = Convert.ToInt32(reader["GiaBanHienTai"]),
                                HinhAnh = reader["HinhAnh"] != DBNull.Value ? reader["HinhAnh"].ToString() : null,
                                TonKhoTong = Convert.ToInt32(reader["TonKhoTong"]),
                                TrangThai = Convert.ToBoolean(reader["TrangThai"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}