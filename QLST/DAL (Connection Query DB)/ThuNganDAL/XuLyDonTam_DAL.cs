using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DAL__Connection_Query_DB_
{
    public class DonTamDAL
    {
        // 1. LƯU TẠM ĐƠN HÀNG XUỐNG DB
        public bool LuuDonTamXuongDB(HoaDonDTO donHang)
        {
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            query.AppendLine("BEGIN TRY BEGIN TRAN;");

            // Insert vào bảng HoaDonTam
            query.AppendLine(@"INSERT INTO HoaDonTam (HoldId, ThoiGianLuuTam, MaNV, SDT_KhachHang, TongTienCung) 
                               VALUES (@HoldId, @ThoiGian, @MaNV, @SDT, @TongTien);");

            parameters.Add(new SqlParameter("@HoldId", donHang.HoldId));
            parameters.Add(new SqlParameter("@ThoiGian", donHang.ThoiGianLuuTam));
            parameters.Add(new SqlParameter("@MaNV", string.IsNullOrEmpty(donHang.MaNV) ? (object)DBNull.Value : donHang.MaNV));
            parameters.Add(new SqlParameter("@SDT", string.IsNullOrEmpty(donHang.SDT_KhachHang) ? (object)DBNull.Value : donHang.SDT_KhachHang));
            parameters.Add(new SqlParameter("@TongTien", donHang.TongTienCung));

            // Insert danh sách sản phẩm vào bảng ChiTietHoaDonTam
            for (int i = 0; i < donHang.DanhSachSanPham.Count; i++)
            {
                var sp = donHang.DanhSachSanPham[i];
                query.AppendLine($@"INSERT INTO ChiTietHoaDonTam (HoldId, MaVach, SoLuongMua, DongGiaBan, ThanhTien) 
                                    VALUES (@HoldId, @MaVach{i}, @SoLuong{i}, @DongGia{i}, @ThanhTien{i});");

                parameters.Add(new SqlParameter($"@MaVach{i}", sp.MaVach));
                parameters.Add(new SqlParameter($"@SoLuong{i}", sp.SoLuongMua));
                parameters.Add(new SqlParameter($"@DongGia{i}", sp.DongGiaBan));
                parameters.Add(new SqlParameter($"@ThanhTien{i}", sp.ThanhTien));
            }

            query.AppendLine("COMMIT TRAN; END TRY BEGIN CATCH ROLLBACK TRAN; THROW; END CATCH");

            try
            {
                return DataProvider.Instance.ExecuteNonQuery(query.ToString(), parameters.ToArray()) > 0;
            }
            catch
            {
                return false;
            }
        }

        // 2. LẤY DANH SÁCH CÁC ĐƠN ĐANG TREO (Để hiển thị lên Chuông vàng)
        public DataTable LayDanhSachDonTam()
        {
            string query = "SELECT HoldId, ThoiGianLuuTam, TongTienCung FROM HoaDonTam ORDER BY ThoiGianLuuTam DESC";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 3. LẤY CHI TIẾT SẢN PHẨM CỦA 1 ĐƠN (Khi ấn khôi phục)
        // Lưu ý: Kết hợp JOIN với bảng Sản Phẩm thật để lấy được Tên SP hiển thị lên UI
        public DataTable LayChiTietDonTam(string holdId)
        {
            string query = @"
                SELECT ct.MaVach, sp.TenSP, ct.SoLuongMua, ct.DongGiaBan, ct.ThanhTien 
                FROM ChiTietHoaDonTam ct
                JOIN SanPham sp ON ct.MaVach = sp.MaVach
                WHERE ct.HoldId = @HoldId";

            SqlParameter[] param = { new SqlParameter("@HoldId", holdId) };
            return DataProvider.Instance.ExecuteQuery(query, param);
        }

        // 4. XÓA ĐƠN TẠM KHI ĐÃ KHÔI PHỤC THÀNH CÔNG
        public bool XoaDonTam(string holdId)
        {
            // Nhờ có ON DELETE CASCADE ở duới database, ta chỉ cần xóa HoaDonTam, ChiTietHoaDonTam sẽ tự bốc hơi
            string query = "DELETE FROM HoaDonTam WHERE HoldId = @HoldId";
            SqlParameter[] param = { new SqlParameter("@HoldId", holdId) };
            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }
    }
}
