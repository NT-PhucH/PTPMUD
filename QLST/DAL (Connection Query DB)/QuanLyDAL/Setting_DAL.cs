
using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_.Core
{
    public class Setting_DAL
    {
        
        public Setting_DTO Get()
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery(
                "SELECT TOP 1 * FROM ThamSoHeThong ORDER BY ID");

            if (dt.Rows.Count == 0) return new Setting_DTO();
            return MapRow(dt.Rows[0]);
        }

        public bool Update(Setting_DTO ts)
        {
            string sql = @"
                UPDATE ThamSoHeThong SET
                    TenCuaHang    = @TenCH,
                    DiaChi        = @DC,
                    SoDienThoai   = @SDT,
                    HotlineShip   = @HotShip,
                    Email         = @Email,
                    VAT           = @VAT,
                    DiemPer10K    = @Diem,
                    NguongHetHang = @NguongHang,
                    NguongHetHan  = @NguongHan,
                    FooterHoaDon  = @Footer
                WHERE ID = @ID";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@TenCH",      ts.TenCuaHang),
                new SqlParameter("@DC",         ts.DiaChi        ?? ""),
                new SqlParameter("@SDT",        ts.SoDienThoai   ?? ""),
                new SqlParameter("@HotShip",    ts.HotlineShip   ?? ""),
                new SqlParameter("@Email",      ts.Email         ?? ""),
                new SqlParameter("@VAT",        ts.VAT),
                new SqlParameter("@Diem",       ts.DiemPer10K),
                new SqlParameter("@NguongHang", ts.NguongHetHang),
                new SqlParameter("@NguongHan",  ts.NguongHetHan),
                new SqlParameter("@Footer",     ts.FooterHoaDon  ?? ""),
                new SqlParameter("@ID",         ts.ID)
            });
            return rows > 0;
        }

        private Setting_DTO MapRow(DataRow row) => new Setting_DTO
        {
            ID = Convert.ToInt32(row["ID"]),
            TenCuaHang = row["TenCuaHang"].ToString(),
            DiaChi = row["DiaChi"] == DBNull.Value ? "" : row["DiaChi"].ToString(),
            SoDienThoai = row["SoDienThoai"] == DBNull.Value ? "" : row["SoDienThoai"].ToString(),
            HotlineShip = row["HotlineShip"] == DBNull.Value ? "" : row["HotlineShip"].ToString(),
            Email = row["Email"] == DBNull.Value ? "" : row["Email"].ToString(),
            VAT = Convert.ToInt32(row["VAT"]),
            DiemPer10K = Convert.ToInt32(row["DiemPer10K"]),
            NguongHetHang = Convert.ToInt32(row["NguongHetHang"]),
            NguongHetHan = Convert.ToInt32(row["NguongHetHan"]),
            FooterHoaDon = row["FooterHoaDon"] == DBNull.Value ? "" : row["FooterHoaDon"].ToString()
        };
    }
}