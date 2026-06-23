using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class QLHD_DAL
    {
        // ── 1. TÌM KIẾM ĐA ĐIỀU KIỆN ────────────────────────────────────
        public List<HoaDon_DTO> Search(DateTime tuNgay, DateTime denNgay, string phuongThuc, string keywordKH)
        {
            var list = new List<HoaDon_DTO>();
            string sql = @"
                SELECT hd.HoaDonID, hd.MaHD, hd.ThoiGianTao, hd.TongTienCung, hd.PhuongThucThanhToan,
                       nv.TenNV, kh.TenKH, kh.SDT
                FROM HoaDon hd
                LEFT JOIN NhanVien nv ON hd.NhanVienID = nv.NhanVienID
                LEFT JOIN KhachHang kh ON hd.KhachHangID = kh.KhachHangID
                WHERE CAST(hd.ThoiGianTao AS DATE) BETWEEN @TuNgay AND @DenNgay ";

            var parameters = new List<SqlParameter> {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date)
            };

            // Lọc theo phương thức thanh toán nếu không phải là "Tất cả"
            if (phuongThuc != "Tất cả")
            {
                sql += " AND hd.PhuongThucThanhToan = @PTTT ";
                parameters.Add(new SqlParameter("@PTTT", phuongThuc));
            }

            // Tìm kiếm theo tên hoặc SĐT khách hàng
            if (!string.IsNullOrWhiteSpace(keywordKH))
            {
                sql += " AND (kh.TenKH LIKE N'%' + @Kw + '%' OR kh.SDT LIKE '%' + @Kw + '%') ";
                parameters.Add(new SqlParameter("@Kw", keywordKH.Trim()));
            }

            sql += " ORDER BY hd.ThoiGianTao DESC";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new HoaDon_DTO
                {
                    HoaDonID = Convert.ToInt32(row["HoaDonID"]),
                    MaHD = row["MaHD"].ToString(),
                    ThoiGianTao = Convert.ToDateTime(row["ThoiGianTao"]),
                    TongTienCung = Convert.ToInt64(row["TongTienCung"]),
                    PhuongThucThanhToan = row["PhuongThucThanhToan"].ToString(),
                    TenNV = row["TenNV"] == DBNull.Value ? "" : row["TenNV"].ToString(),
                    TenKH = row["TenKH"] == DBNull.Value ? "Khách lẻ" : row["TenKH"].ToString(),
                    SDT = row["SDT"] == DBNull.Value ? "" : row["SDT"].ToString()
                });
            }
            return list;
        }

        // ── 2. LẤY CHI TIẾT HÓA ĐƠN ──────────────────────────────────────
        public List<ChiTietHoaDon_DTO> GetChiTiet(int hoaDonID)
        {
            var list = new List<ChiTietHoaDon_DTO>();
            string sql = @"
                SELECT ct.MaCTHD, sp.TenSP, ct.SoLuongMua, ct.DonGiaBan, ct.ThanhTien
                FROM ChiTietHoaDon ct
                JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
                WHERE ct.HoaDonID = @ID";

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", hoaDonID)
            });

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ChiTietHoaDon_DTO
                {
                    MaCTHD = Convert.ToInt32(row["MaCTHD"]),
                    TenSP = row["TenSP"].ToString(),
                    SoLuongMua = Convert.ToInt32(row["SoLuongMua"]),
                    DonGiaBan = Convert.ToInt64(row["DonGiaBan"]),
                    ThanhTien = Convert.ToInt64(row["ThanhTien"])
                });
            }
            return list;
        }
    }
}
