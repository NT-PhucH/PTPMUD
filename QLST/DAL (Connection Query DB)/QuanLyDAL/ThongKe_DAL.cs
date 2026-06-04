using QLST.DAL__Connection_Query_DB_.Query_DB;
using System;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_
{
    public class ThongKe_DAL
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
    }
}