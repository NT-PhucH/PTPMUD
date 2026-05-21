// Nằm trong Project DAL, file DbConnection.cs
using System.Data.SqlClient;

namespace DAL
{
    public class DbConnection
    {
        // Đổi "TEN_MAY_TINH" thành tên server SQL của bạn
        public static string chuoiKetNoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySieuThi;Integrated Security=True;Trust Server Certificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(chuoiKetNoi);
        }
    }
}