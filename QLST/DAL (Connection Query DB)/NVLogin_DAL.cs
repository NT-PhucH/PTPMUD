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
    public class NVLogin_DAL
    {
        public QLNV_DTO GetNhanVienByLogin(string username, string password)
        {
            QLNV_DTO nv = null;

            // SẠN 1: Bổ sung NhanVienID vào đầu câu SELECT
            string query = "SELECT NhanVienID, MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai " +
                           "FROM NhanVien " +
                           "WHERE Username = @Username AND Password = @Password";

            // Sửa Code Smell: Thêm dấu @ vào tên parameter
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter ("@Username", username),
                new SqlParameter ("@Password", password)
            };

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters);

            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];

                nv = new QLNV_DTO()
                {
                    // SẠN 1: Map NhanVienID (kiểu int) để hệ thống nhận diện đúng khóa chính
                    NhanVienID = Convert.ToInt32(row["NhanVienID"]),

                    MaNV = row["MaNV"].ToString(),
                    TenNV = row["TenNV"].ToString(),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Role = Convert.ToInt32(row["Role"]),

                    SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                    CaLamViec = row["CaLamViec"] != DBNull.Value ? row["CaLamViec"].ToString() : null,
                    TrangThai = row["TrangThai"] != DBNull.Value ? Convert.ToBoolean(row["TrangThai"]) : false,
                };
            }
            return nv;
        }
    }
}
