using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class NhaCungCap_DAL
    {
        // ── LẤY TẤT CẢ ───────────────────────────────────────────────────────
        public List<NhaCungCap_DTO> GetAll()
        {
            var list = new List<NhaCungCap_DTO>();
            string sql = @"
                SELECT ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC, 
                       ncc.SoDienThoai, ncc.DiaChi, ncc.TrangThai,
                       COUNT(pn.PhieuNhapID)       AS TongPhieuNhap,
                       ISNULL(SUM(pn.TongTienThanhToan), 0) AS TongTienNhap
                FROM NhaCungCap ncc
                LEFT JOIN PhieuNhap pn ON ncc.NhaCungCapID = pn.NhaCungCapID
                GROUP BY ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC,
                         ncc.SoDienThoai, ncc.DiaChi, ncc.TrangThai
                ORDER BY ncc.TenNCC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── TÌM KIẾM ─────────────────────────────────────────────────────────
        public List<NhaCungCap_DTO> Search(string keyword)
        {
            var list = new List<NhaCungCap_DTO>();
            string sql = @"
                SELECT ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC, 
                       ncc.SoDienThoai, ncc.DiaChi, ncc.TrangThai,
                       COUNT(pn.PhieuNhapID)       AS TongPhieuNhap,
                       ISNULL(SUM(pn.TongTienThanhToan), 0) AS TongTienNhap
                FROM NhaCungCap ncc
                LEFT JOIN PhieuNhap pn ON ncc.NhaCungCapID = pn.NhaCungCapID
                WHERE ncc.TenNCC LIKE N'%' + @kw + '%'
                   OR ncc.MaNCC  LIKE '%' + @kw + '%'
                   OR ncc.SoDienThoai LIKE '%' + @kw + '%'
                GROUP BY ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC, 
                         ncc.SoDienThoai, ncc.DiaChi, ncc.TrangThai
                ORDER BY ncc.TenNCC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@kw", keyword)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── THÊM (TỰ SINH MÃ BẰNG SQL) ───────────────────────────────────────
        public bool Insert(NhaCungCap_DTO ncc)
        {
            string sql = @"
                DECLARE @NextID INT = ISNULL((SELECT MAX(NhaCungCapID) FROM NhaCungCap), 0) + 1;
                DECLARE @NewMaNCC VARCHAR(20) = 'NCC' + RIGHT('0000' + CAST(@NextID AS VARCHAR(4)), 4);

                INSERT INTO NhaCungCap (MaNCC, TenNCC, SoDienThoai, DiaChi)
                VALUES (@NewMaNCC, @Ten, @SDT, @DC)";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@Ten", ncc.TenNCC),
                new SqlParameter("@SDT", (object)ncc.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@DC",  (object)ncc.DiaChi      ?? DBNull.Value)
            });

            return rows > 0;
        }

        // ── SỬA ────────────────────────────────────────────────────
        public bool Update(NhaCungCap_DTO ncc)
        {
            string sql = @"
                UPDATE NhaCungCap
                SET TenNCC = @Ten, SoDienThoai = @SDT, DiaChi = @DC
                WHERE NhaCungCapID = @ID";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@Ten", ncc.TenNCC),
                new SqlParameter("@SDT", (object)ncc.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@DC",  (object)ncc.DiaChi      ?? DBNull.Value),
                new SqlParameter("@ID",  ncc.NhaCungCapID)
            });
            return rows > 0;
        }

        // ── BẬT / TẮT TRẠNG THÁI ───────────────────────────
        public bool ToggleTrangThai(int id)
        {
            string sql = @"
                UPDATE NhaCungCap
                SET TrangThai = CASE WHEN TrangThai = 1 THEN 0 ELSE 1 END
                WHERE NhaCungCapID = @ID";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", id)
            });
            return rows > 0;
        }

        // ── HELPER ───────────────────────────────────────────────────────────
        private NhaCungCap_DTO MapRow(DataRow row) => new NhaCungCap_DTO
        {
            NhaCungCapID = Convert.ToInt32(row["NhaCungCapID"]),
            MaNCC = row["MaNCC"].ToString(),
            TenNCC = row["TenNCC"].ToString(),
            SoDienThoai = row["SoDienThoai"] == DBNull.Value ? "" : row["SoDienThoai"].ToString(),
            DiaChi = row["DiaChi"] == DBNull.Value ? "" : row["DiaChi"].ToString(),
            TongPhieuNhap = Convert.ToInt32(row["TongPhieuNhap"]),
            TongTienNhap = Convert.ToInt64(row["TongTienNhap"]),
            TrangThai = Convert.ToBoolean(row["TrangThai"])
        };
    }
}