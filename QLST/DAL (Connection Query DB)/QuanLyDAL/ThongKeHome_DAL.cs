using QLST.DAL__Connection_Query_DB_.Query_DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_
{
    public class ThongKeHome_DAL
    {
        // Hàm tính tổng tiền hóa đơn theo 1 ngày truyền vào
        public double GetDoanhThuTheoNgay(DateTime ngay)
        {
            double doanhThu = 0;
            // Dùng hàm ISNULL để nếu ngày đó không bán được đơn nào thì trả về 0 thay vì lỗi Null
            string query = "SELECT ISNULL(SUM(TongTienCung), 0) FROM HoaDon WHERE CAST(ThoiGianTao AS DATE) = @Ngay";

            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Lấy phần ngày (bỏ qua giờ phút giây) để so sánh chuẩn xác
                    cmd.Parameters.AddWithValue("@Ngay", ngay.Date);

                    // Thực thi và ép kiểu kết quả về double
                    doanhThu = Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
            return doanhThu;
        }
        // Đếm số lượng hóa đơn trong ngày
        public int GetSoLuongHoaDonTheoNgay(DateTime ngay)
        {
            int soLuong = 0;
            string query = "SELECT COUNT(HoaDonID) FROM HoaDon WHERE CAST(ThoiGianTao AS DATE) = @Ngay";
            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
                    soLuong = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return soLuong;
        }
        // 1. Hàng sắp hết tồn (Dựa vào NguongHetHang trong ThamSoHeThong)
        public List<string> GetHangSapHetTon()
        {
            List<string> list = new List<string>();

            // Lấy NguongHetHang trực tiếp từ DB để so sánh
            string query = @"
        SELECT TenSP, TonKhoTong 
        FROM SanPham 
        WHERE TonKhoTong <= (SELECT TOP 1 NguongHetHang FROM ThamSoHeThong)
        ORDER BY TonKhoTong ASC";

            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add($"[Còn {reader["TonKhoTong"]}] - {reader["TenSP"]}");
                    }
                }
            }
            return list;
        }

        // 2. Hàng sắp hết hạn (Dựa vào NguongHetHan trong ThamSoHeThong)
        public List<string> GetHangSapHetHan()
        {
            List<string> list = new List<string>();

            // Lấy NguongHetHan trực tiếp từ DB để cộng ngày, 
            // và thêm điều kiện ct.SoLuongTonCuaLo > 0 để tránh báo ảo các lô đã bán hết
            string query = @"
        SELECT sp.TenSP, ct.HSD 
        FROM ChiTietPhieuNhap_LoHang ct
        INNER JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
        WHERE ct.HSD <= DATEADD(day, (SELECT TOP 1 NguongHetHan FROM ThamSoHeThong), GETDATE()) 
          AND ct.HSD IS NOT NULL
          AND ct.SoLuongTonCuaLoNay > 0 
        ORDER BY ct.HSD ASC";

            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime hsd = Convert.ToDateTime(reader["HSD"]);
                        list.Add($"[{hsd:dd/MM/yyyy}] - {reader["TenSP"]}");
                    }
                }
            }
            return list;
        }
    }

}