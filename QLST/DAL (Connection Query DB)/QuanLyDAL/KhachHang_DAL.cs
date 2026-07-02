// ===================================================
// File: KhachHang_DAL.cs
// Đặt vào: DAL (Connection Query DB) > QuanLyDAL
// ===================================================
using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class KhachHang_DAL
    {
        // ── LẤY TẤT CẢ (kèm thống kê) ────────────────────────────────────────
        public List<KhachHang_DTO> GetAll()
        {
            var list = new List<KhachHang_DTO>();
            string sql = @"
                SELECT kh.KhachHangID, kh.SDT, kh.TenKH, kh.DiemTichLuy,
                       COUNT(hd.HoaDonID)            AS TongHoaDon,
                       ISNULL(SUM(hd.TongTienCung),0) AS TongChiTieu
                FROM KhachHang kh
                LEFT JOIN HoaDon hd ON kh.KhachHangID = hd.KhachHangID
                GROUP BY kh.KhachHangID, kh.SDT, kh.TenKH, kh.DiemTichLuy
                ORDER BY kh.DiemTichLuy DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── TÌM KIẾM ─────────────────────────────────────────────────────────
        public List<KhachHang_DTO> Search(string keyword)
        {
            var list = new List<KhachHang_DTO>();
            string sql = @"
                SELECT kh.KhachHangID, kh.SDT, kh.TenKH, kh.DiemTichLuy,
                       COUNT(hd.HoaDonID)             AS TongHoaDon,
                       ISNULL(SUM(hd.TongTienCung),0) AS TongChiTieu
                FROM KhachHang kh
                LEFT JOIN HoaDon hd ON kh.KhachHangID = hd.KhachHangID
                WHERE kh.TenKH LIKE N'%' + @kw + '%'
                   OR kh.SDT   LIKE '%'  + @kw + '%'
                GROUP BY kh.KhachHangID, kh.SDT, kh.TenKH, kh.DiemTichLuy
                ORDER BY kh.DiemTichLuy DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@kw", keyword)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── LỊCH SỬ HÓA ĐƠN / TÍCH ĐIỂM ────────────────────────────────────
        public List<LichSuTichDiem_DTO> GetLichSu(int khachHangID, int diemPer10k)
        {
            var list = new List<LichSuTichDiem_DTO>();
            string sql = @"
                SELECT HoaDonID, MaHD, ThoiGianTao, TongTienCung
                FROM HoaDon
                WHERE KhachHangID = @ID
                ORDER BY ThoiGianTao DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", khachHangID)
            });
            foreach (DataRow row in dt.Rows)
            {
                long tong = Convert.ToInt64(row["TongTienCung"]);
                list.Add(new LichSuTichDiem_DTO
                {
                    HoaDonID = Convert.ToInt32(row["HoaDonID"]),
                    MaHD = row["MaHD"].ToString(),
                    ThoiGian = Convert.ToDateTime(row["ThoiGianTao"]),
                    TongTien = tong,
                    DiemCong = (int)(tong / 10000) * diemPer10k
                });
            }
            return list;
        }

        // ── THÊM ─────────────────────────────────────────────────────────────
        public bool Insert(KhachHang_DTO kh)
        {
            string sql = @"
                INSERT INTO KhachHang (SDT, TenKH, DiemTichLuy)
                VALUES (@SDT, @Ten, 0)";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@SDT", kh.SDT),
                new SqlParameter("@Ten", kh.TenKH)
            });
            return rows > 0;
        }

        // ── SỬA ──────────────────────────────────────────────────────────────
        public bool Update(KhachHang_DTO kh)
        {
            string sql = @"
                UPDATE KhachHang
                SET SDT = @SDT, TenKH = @Ten
                WHERE KhachHangID = @ID";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@SDT", kh.SDT),
                new SqlParameter("@Ten", kh.TenKH),
                new SqlParameter("@ID",  kh.KhachHangID)
            });
            return rows > 0;
        }


        // ── KIỂM TRA SĐT TRÙNG ───────────────────────────────────────────────
        public bool IsSDTExists(string sdt, int excludeID = 0)
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM KhachHang WHERE SDT = @SDT AND KhachHangID <> @ID",
                new SqlParameter[] {
                    new SqlParameter("@SDT", sdt),
                    new SqlParameter("@ID",  excludeID)
                });
            return Convert.ToInt32(result) > 0;
        }

        private KhachHang_DTO MapRow(DataRow row) => new KhachHang_DTO
        {
            KhachHangID = Convert.ToInt32(row["KhachHangID"]),
            SDT = row["SDT"].ToString(),
            TenKH = row["TenKH"].ToString(),
            DiemTichLuy = row["DiemTichLuy"] == DBNull.Value ? 0 : Convert.ToInt32(row["DiemTichLuy"]),
            TongHoaDon = Convert.ToInt32(row["TongHoaDon"]),
            TongChiTieu = Convert.ToInt64(row["TongChiTieu"])
        };
    }
}