using System.Data.SqlClient;

namespace DAL
{
    public class DBConnect
    {
        // Thay bằng Tên máy tính\Tên Instance SQL Server của bạn
        public static string chuoiKetNoi = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySieuThi;Integrated Security=True";
        protected SqlConnection conn;

        public DBConnect()
        {
            conn = new SqlConnection(chuoiKetNoi);
        }
    }
}