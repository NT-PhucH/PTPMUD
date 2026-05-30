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
    public class NhanVienDAL
    {
        public NhanVienDTO GetNhanVienByLogin(string username, string password)
        {
            NhanVienDTO nv = null;

            string query = "SELECT MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai, DuongDanAnh " +
                           "FROM NhanVien " +
                           "WHERE Username = @Username AND Password = @Password";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter ("username", username),
                new SqlParameter ("password", password)
            };

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters);

            if (data != null && data.Rows.Count > 0) { 
                DataRow row = data.Rows[0];

                nv = new NhanVienDTO()
                {
                    MaNV = row["MaNV"].ToString(),
                    TenNV = row["TenNV"].ToString(),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Role = Convert.ToInt32(row["Role"]),

                    // Xử lý các trường có thể null dưới DB (DBNull.Value)
                    SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                    CaLamViec = row["CaLamViec"] != DBNull.Value ? row["CaLamViec"].ToString() : null,
                    TrangThai = row["TrangThai"] != DBNull.Value ? Convert.ToBoolean(row["TrangThai"]) : false,
                    DuongDanAnh = row["DuongDanAnh"] != DBNull.Value ? row["DuongDanAnh"].ToString() : null
                };
            }
            // Trả về DTO (nếu sai tài khoản/mật khẩu thì data.Rows.Count = 0, nv sẽ bằng null)
            return nv;
        }
    }
}
