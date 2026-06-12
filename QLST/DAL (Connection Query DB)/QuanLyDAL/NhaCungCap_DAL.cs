// ===================================================
// File: NhaCungCap_DAL.cs
// Đặt vào: DAL (Connection Query DB) > QuanLyDAL
// XÓA file NhaCungCap_Stub.cs sau khi thêm file này!
// ===================================================
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
        // ── LẤY TẤT CẢ (kèm thống kê số phiếu + tổng tiền) ──────────────────
        public List<NhaCungCap_DTO> GetAll()
        {
            var list = new List<NhaCungCap_DTO>();
            string sql = @"
                SELECT ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC,
                       ncc.SoDienThoai, ncc.DiaChi,
                       COUNT(pn.PhieuNhapID)       AS TongPhieuNhap,
                       ISNULL(SUM(pn.TongTienThanhToan), 0) AS TongTienNhap
                FROM NhaCungCap ncc
                LEFT JOIN PhieuNhap pn ON ncc.NhaCungCapID = pn.NhaCungCapID
                GROUP BY ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC,
                         ncc.SoDienThoai, ncc.DiaChi
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
                       ncc.SoDienThoai, ncc.DiaChi,
                       COUNT(pn.PhieuNhapID)       AS TongPhieuNhap,
                       ISNULL(SUM(pn.TongTienThanhToan), 0) AS TongTienNhap
                FROM NhaCungCap ncc
                LEFT JOIN PhieuNhap pn ON ncc.NhaCungCapID = pn.NhaCungCapID
                WHERE ncc.TenNCC LIKE N'%' + @kw + '%'
                   OR ncc.MaNCC  LIKE '%' + @kw + '%'
                   OR ncc.SoDienThoai LIKE '%' + @kw + '%'
                GROUP BY ncc.NhaCungCapID, ncc.MaNCC, ncc.TenNCC,
                         ncc.SoDienThoai, ncc.DiaChi
                ORDER BY ncc.TenNCC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@kw", keyword)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── THÊM ─────────────────────────────────────────────────────────────
        public bool Insert(NhaCungCap_DTO ncc)
        {
            string sql = @"
                INSERT INTO NhaCungCap (MaNCC, TenNCC, SoDienThoai, DiaChi)
                VALUES (@Ma, @Ten, @SDT, @DC)";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@Ma",  ncc.MaNCC),
                new SqlParameter("@Ten", ncc.TenNCC),
                new SqlParameter("@SDT", (object)ncc.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@DC",  (object)ncc.DiaChi      ?? DBNull.Value)
            });
            return rows > 0;
        }

        // ── SỬA ──────────────────────────────────────────────────────────────
        public bool Update(NhaCungCap_DTO ncc)
        {
            string sql = @"
                UPDATE NhaCungCap
                SET MaNCC = @Ma, TenNCC = @Ten, SoDienThoai = @SDT, DiaChi = @DC
                WHERE NhaCungCapID = @ID";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@Ma",  ncc.MaNCC),
                new SqlParameter("@Ten", ncc.TenNCC),
                new SqlParameter("@SDT", (object)ncc.SoDienThoai ?? DBNull.Value),
                new SqlParameter("@DC",  (object)ncc.DiaChi      ?? DBNull.Value),
                new SqlParameter("@ID",  ncc.NhaCungCapID)
            });
            return rows > 0;
        }

        // ── XÓA ──────────────────────────────────────────────────────────────
        public bool Delete(int id)
        {
            // Kiểm tra còn phiếu nhập không
            object count = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM PhieuNhap WHERE NhaCungCapID = @ID",
                new SqlParameter[] { new SqlParameter("@ID", id) });
            if (Convert.ToInt32(count) > 0) return false;

            int rows = DataProvider.Instance.ExecuteNonQuery(
                "DELETE FROM NhaCungCap WHERE NhaCungCapID = @ID",
                new SqlParameter[] { new SqlParameter("@ID", id) });
            return rows > 0;
        }

        // ── KIỂM TRA MÃ TRÙNG ────────────────────────────────────────────────
        public bool IsMaExists(string maNCC, int excludeID = 0)
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @Ma AND NhaCungCapID <> @ID",
                new SqlParameter[] {
                    new SqlParameter("@Ma",  maNCC),
                    new SqlParameter("@ID",  excludeID)
                });
            return Convert.ToInt32(result) > 0;
        }

        // ── SINH MÃ TỰ ĐỘNG ──────────────────────────────────────────────────
        public string SinhMaNCC()
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM NhaCungCap");
            return $"NCC{Convert.ToInt32(result) + 1:D4}";
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
            TongTienNhap = Convert.ToInt64(row["TongTienNhap"])
        };
    }
}