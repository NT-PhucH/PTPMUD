using System;

namespace QLST.DAL__Connection_Query_DB_.Query_DB
{
    public class DB_Connection
    {
        // Khai báo static để các class khác gọi thẳng mà không cần new DB_Connection()
        // Đổi chuỗi kết nối ở đây thì TOÀN BỘ dự án sẽ tự động cập nhật theo
        public static string GetConnectionString()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySieuThiv2;Integrated Security=True";
        }
    }
}