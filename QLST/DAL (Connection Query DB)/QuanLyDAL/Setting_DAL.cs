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
            // Bổ sung 3 cột VietQR vào câu lệnh UPDATE
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
                    FooterHoaDon  = @Footer,
                    NganHang      = @NganHang,
                    SoTaiKhoan    = @SoTaiKhoan,
                    TenTaiKhoan   = @TenTaiKhoan
                WHERE ID = @ID";

            // Bổ sung thêm Parameter cho 3 trường mới
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
                new SqlParameter("@NganHang",   ts.NganHang      ?? ""),
                new SqlParameter("@SoTaiKhoan", ts.SoTaiKhoan    ?? ""),
                new SqlParameter("@TenTaiKhoan",ts.TenTaiKhoan   ?? ""),
                new SqlParameter("@ID",         ts.ID)
            });
            return rows > 0;
        }

        // Bổ sung đọc dữ liệu 3 cột mới từ DataTable
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
            FooterHoaDon = row["FooterHoaDon"] == DBNull.Value ? "" : row["FooterHoaDon"].ToString(),
            NganHang = row.Table.Columns.Contains("NganHang") && row["NganHang"] != DBNull.Value ? row["NganHang"].ToString() : "",
            SoTaiKhoan = row.Table.Columns.Contains("SoTaiKhoan") && row["SoTaiKhoan"] != DBNull.Value ? row["SoTaiKhoan"].ToString() : "",
            TenTaiKhoan = row.Table.Columns.Contains("TenTaiKhoan") && row["TenTaiKhoan"] != DBNull.Value ? row["TenTaiKhoan"].ToString() : ""
        };
    }
}