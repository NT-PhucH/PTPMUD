// ===================================================
// File: SanPham_DAL.cs
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
    public class SanPham_DAL
    {
        // ── LẤY TẤT CẢ SẢN PHẨM (kèm tên loại) ──────────────────────────────
        public List<SanPham_DTO> GetAll()
        {
            var list = new List<SanPham_DTO>();
            string sql = @"
                SELECT sp.SanPhamID, sp.MaVach, sp.TenSP, sp.GiaBanHienTai,
                       sp.LoaiSanPhamID, lsp.TenLoai, sp.TonKhoTong, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                ORDER BY sp.TenSP";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── TÌM KIẾM THEO TÊN HOẶC MÃ VẠCH ──────────────────────────────────
        public List<SanPham_DTO> Search(string keyword)
        {
            var list = new List<SanPham_DTO>();
            string sql = @"
                SELECT sp.SanPhamID, sp.MaVach, sp.TenSP, sp.GiaBanHienTai,
                       sp.LoaiSanPhamID, lsp.TenLoai, sp.TonKhoTong, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                WHERE sp.TenSP LIKE N'%' + @kw + '%'
                   OR sp.MaVach LIKE '%' + @kw + '%'
                ORDER BY sp.TenSP";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@kw", keyword)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── LẤY THEO LOẠI ─────────────────────────────────────────────────────
        public List<SanPham_DTO> GetByLoai(int loaiID)
        {
            var list = new List<SanPham_DTO>();
            string sql = @"
                SELECT sp.SanPhamID, sp.MaVach, sp.TenSP, sp.GiaBanHienTai,
                       sp.LoaiSanPhamID, lsp.TenLoai, sp.TonKhoTong, sp.HinhAnh
                FROM SanPham sp
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                WHERE sp.LoaiSanPhamID = @loaiID
                ORDER BY sp.TenSP";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@loaiID", loaiID)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        // ── THÊM SẢN PHẨM ─────────────────────────────────────────────────────
        public bool Insert(SanPham_DTO sp)
        {
            string sql = @"
                INSERT INTO SanPham (MaVach, TenSP, GiaBanHienTai, LoaiSanPhamID, TonKhoTong, HinhAnh)
                VALUES (@MaVach, @TenSP, @Gia, @LoaiID, @Ton, @Anh)";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@MaVach", sp.MaVach),
                new SqlParameter("@TenSP",  sp.TenSP),
                new SqlParameter("@Gia",    sp.GiaBanHienTai),
                new SqlParameter("@LoaiID", sp.LoaiSanPhamID),
                new SqlParameter("@Ton",    sp.TonKhoTong),
                new SqlParameter("@Anh",    sp.HinhAnh ?? "")
            });
            return rows > 0;
        }

        // ── SỬA SẢN PHẨM ──────────────────────────────────────────────────────
        public bool Update(SanPham_DTO sp)
        {
            string sql = @"
                UPDATE SanPham
                SET MaVach = @MaVach, TenSP = @TenSP, GiaBanHienTai = @Gia,
                    LoaiSanPhamID = @LoaiID, HinhAnh = @Anh
                WHERE SanPhamID = @ID";

            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@MaVach", sp.MaVach),
                new SqlParameter("@TenSP",  sp.TenSP),
                new SqlParameter("@Gia",    sp.GiaBanHienTai),
                new SqlParameter("@LoaiID", sp.LoaiSanPhamID),
                new SqlParameter("@Anh",    sp.HinhAnh ?? ""),
                new SqlParameter("@ID",     sp.SanPhamID)
            });
            return rows > 0;
        }

        // ── XÓA SẢN PHẨM ──────────────────────────────────────────────────────
        public bool Delete(int sanPhamID)
        {
            string sql = "DELETE FROM SanPham WHERE SanPhamID = @ID";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", sanPhamID)
            });
            return rows > 0;
        }

        // ── KIỂM TRA MÃ VẠCH TRÙNG ────────────────────────────────────────────
        public bool IsMaVachExists(string maVach, int excludeID = 0)
        {
            string sql = "SELECT COUNT(*) FROM SanPham WHERE MaVach = @MaVach AND SanPhamID <> @ID";
            object result = DataProvider.Instance.ExecuteScalar(sql, new SqlParameter[] {
                new SqlParameter("@MaVach", maVach),
                new SqlParameter("@ID",     excludeID)
            });
            return Convert.ToInt32(result) > 0;
        }

        // ── LẤY TẤT CẢ LOẠI SẢN PHẨM ─────────────────────────────────────────
        public List<LoaiSanPham_DTO> GetAllLoai()
        {
            var list = new List<LoaiSanPham_DTO>();
            DataTable dt = DataProvider.Instance.ExecuteQuery(
                "SELECT LoaiSanPhamID, TenLoai FROM LoaiSanPham ORDER BY TenLoai");
            foreach (DataRow row in dt.Rows)
                list.Add(new LoaiSanPham_DTO
                {
                    LoaiSanPhamID = Convert.ToInt32(row["LoaiSanPhamID"]),
                    TenLoai = row["TenLoai"].ToString()
                });
            return list;
        }

        // ── THÊM LOẠI SẢN PHẨM ────────────────────────────────────────────────
        public bool InsertLoai(string tenLoai)
        {
            string sql = "INSERT INTO LoaiSanPham (TenLoai) VALUES (@TenLoai)";
            int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                new SqlParameter("@TenLoai", tenLoai)
            });
            return rows > 0;
        }

        // ── HELPER ────────────────────────────────────────────────────────────
        private SanPham_DTO MapRow(DataRow row)
        {
            return new SanPham_DTO
            {
                SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                MaVach = row["MaVach"].ToString(),
                TenSP = row["TenSP"].ToString(),
                GiaBanHienTai = Convert.ToInt32(row["GiaBanHienTai"]),
                LoaiSanPhamID = row["LoaiSanPhamID"] == DBNull.Value ? 0 : Convert.ToInt32(row["LoaiSanPhamID"]),
                TenLoai = row["TenLoai"] == DBNull.Value ? "" : row["TenLoai"].ToString(),
                TonKhoTong = row["TonKhoTong"] == DBNull.Value ? 0 : Convert.ToInt32(row["TonKhoTong"]),
                HinhAnh = row["HinhAnh"] == DBNull.Value ? "" : row["HinhAnh"].ToString()
            };
        }
    }
}