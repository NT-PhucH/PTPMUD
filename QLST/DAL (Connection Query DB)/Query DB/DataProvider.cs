using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DAL__Connection_Query_DB_.Query_DB
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if(instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }

        private DataProvider() { }

        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySieuThi;Integrated Security=True;";
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Tự động gán toàn bộ tham số nếu có
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(data); // Tự động chạy và đổ dữ liệu vào DataTable
                    }
                }
            } // Tự động đóng và trả Connection về Pool
            return data;
        }

        // --- 4. HÀM CHUNG CHO CÁC LỆNH INSERT, UPDATE, DELETE ---
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int data = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    data = cmd.ExecuteNonQuery(); // Trả về số dòng bị ảnh hưởng
                }
            }
            return data;
        }

        // --- 5. HÀM CHUNG LẤY 1 GIÁ TRỊ ĐƠN (Ví dụ: COUNT(*), SUM, Lấy Tồn Kho) ---
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object data = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    data = cmd.ExecuteScalar();
                }
            }
            return data;
        }
    }
}
