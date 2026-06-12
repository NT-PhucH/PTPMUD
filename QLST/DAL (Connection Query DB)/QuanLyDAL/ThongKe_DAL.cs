// ===================================================
// File: ThongKe_DAL.cs
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
    public class ThongKe_DAL
    {
        // ── TỔNG QUAN ─────────────────────────────────────────────────────────
        public ThongKeTongQuan_DTO GetTongQuan()
        {
            string sql = @"
                SELECT
                    ISNULL((SELECT SUM(TongTienCung) FROM HoaDon WHERE CAST(ThoiGianTao AS DATE) = CAST(GETDATE() AS DATE)), 0) AS DTHomNay,
                    ISNULL((SELECT SUM(TongTienCung) FROM HoaDon WHERE ThoiGianTao >= DATEADD(DAY,-7,GETDATE())), 0)            AS DTTuan,
                    ISNULL((SELECT SUM(TongTienCung) FROM HoaDon WHERE MONTH(ThoiGianTao)=MONTH(GETDATE()) AND YEAR(ThoiGianTao)=YEAR(GETDATE())), 0) AS DTThang,
                    ISNULL((SELECT COUNT(*) FROM HoaDon WHERE CAST(ThoiGianTao AS DATE) = CAST(GETDATE() AS DATE)), 0)          AS HDHomNay,
                    ISNULL((SELECT COUNT(*) FROM SanPham), 0)    AS SoSP,
                    ISNULL((SELECT COUNT(*) FROM KhachHang), 0)  AS SoKH";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            if (dt.Rows.Count == 0) return new ThongKeTongQuan_DTO();
            var row = dt.Rows[0];
            return new ThongKeTongQuan_DTO
            {
                DoanhThuHomNay = Convert.ToInt64(row["DTHomNay"]),
                DoanhThuTuan = Convert.ToInt64(row["DTTuan"]),
                DoanhThuThang = Convert.ToInt64(row["DTThang"]),
                SoHoaDonHomNay = Convert.ToInt32(row["HDHomNay"]),
                SoSanPham = Convert.ToInt32(row["SoSP"]),
                SoKhachHang = Convert.ToInt32(row["SoKH"])
            };
        }

        // ── DOANH THU THEO NGÀY ───────────────────────────────────────────────
        public List<ThongKeDoanhThu_DTO> GetDoanhThuTheoNgay(DateTime from, DateTime to)
        {
            var list = new List<ThongKeDoanhThu_DTO>();
            string sql = @"
                SELECT CAST(ThoiGianTao AS DATE) AS Ngay,
                       SUM(TongTienCung) AS DoanhThu,
                       COUNT(*)          AS SoHoaDon
                FROM HoaDon
                WHERE CAST(ThoiGianTao AS DATE) BETWEEN @From AND @To
                GROUP BY CAST(ThoiGianTao AS DATE)
                ORDER BY Ngay";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@From", from.Date),
                new SqlParameter("@To",   to.Date)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new ThongKeDoanhThu_DTO
                {
                    Ngay = Convert.ToDateTime(row["Ngay"]).ToString("dd/MM"),
                    DoanhThu = Convert.ToInt64(row["DoanhThu"]),
                    SoHoaDon = Convert.ToInt32(row["SoHoaDon"])
                });
            return list;
        }

        // ── DOANH THU THEO THÁNG ──────────────────────────────────────────────
        public List<ThongKeDoanhThu_DTO> GetDoanhThuTheoThang(int nam)
        {
            var list = new List<ThongKeDoanhThu_DTO>();
            string sql = @"
                SELECT MONTH(ThoiGianTao) AS Thang,
                       SUM(TongTienCung)  AS DoanhThu,
                       COUNT(*)           AS SoHoaDon
                FROM HoaDon
                WHERE YEAR(ThoiGianTao) = @Nam
                GROUP BY MONTH(ThoiGianTao)
                ORDER BY Thang";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@Nam", nam)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new ThongKeDoanhThu_DTO
                {
                    Ngay = $"T{row["Thang"]}",
                    DoanhThu = Convert.ToInt64(row["DoanhThu"]),
                    SoHoaDon = Convert.ToInt32(row["SoHoaDon"])
                });
            return list;
        }

        // ── TOP SẢN PHẨM BÁN CHẠY ────────────────────────────────────────────
        public List<ThongKeSanPham_DTO> GetTopSanPham(DateTime from, DateTime to, int top = 10)
        {
            var list = new List<ThongKeSanPham_DTO>();
            string sql = @"
                SELECT TOP (@Top)
                    sp.SanPhamID, sp.TenSP, lsp.TenLoai,
                    SUM(ct.SoLuongMua)  AS SoLuongBan,
                    SUM(ct.ThanhTien)   AS DoanhThu,
                    ISNULL((
                        SELECT TOP 1 ctn.GiaNhap
                        FROM ChiTietPhieuNhap_LoHang ctn
                        WHERE ctn.SanPhamID = sp.SanPhamID
                        ORDER BY ctn.MaChiTietPN DESC
                    ), 0) AS GiaNhapTB
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.HoaDonID = hd.HoaDonID
                JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                WHERE CAST(hd.ThoiGianTao AS DATE) BETWEEN @From AND @To
                GROUP BY sp.SanPhamID, sp.TenSP, lsp.TenLoai
                ORDER BY SoLuongBan DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@Top",  top),
                new SqlParameter("@From", from.Date),
                new SqlParameter("@To",   to.Date)
            });
            foreach (DataRow row in dt.Rows)
            {
                long dt2 = Convert.ToInt64(row["DoanhThu"]);
                long sl = Convert.ToInt64(row["SoLuongBan"]);
                long giaNhap = Convert.ToInt64(row["GiaNhapTB"]);
                list.Add(new ThongKeSanPham_DTO
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    TenSP = row["TenSP"].ToString(),
                    TenLoai = row["TenLoai"] == DBNull.Value ? "" : row["TenLoai"].ToString(),
                    SoLuongBan = (int)sl,
                    DoanhThu = dt2,
                    GiaNhapTB = giaNhap,
                    LoiNhuan = dt2 - (giaNhap * sl)
                });
            }
            return list;
        }
    }
}