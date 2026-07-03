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
        public QLNV_DTO GetNhanVienByUsername(string username)
        {
            QLNV_DTO nv = null;

            // 2. Sửa câu lệnh SQL: BỎ "AND Password = @Password" ở mệnh đề WHERE
            string query = "SELECT NhanVienID, MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai " +
                           "FROM NhanVien " +
                           "WHERE Username = @Username";

            // 3. Sửa Parameter: Chỉ truyền @Username
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter ("@Username", username)
            };

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters); //[cite: 4]

            if (data != null && data.Rows.Count > 0) //[cite: 4]
            {
                DataRow row = data.Rows[0]; //[cite: 4]

                nv = new QLNV_DTO() //[cite: 4]
                {
                    NhanVienID = Convert.ToInt32(row["NhanVienID"]), //[cite: 4]
                    MaNV = row["MaNV"].ToString(), //[cite: 4]
                    TenNV = row["TenNV"].ToString(), //[cite: 4]
                    Username = row["Username"].ToString(), //[cite: 4]
                    Password = row["Password"].ToString(), // Chứa chuỗi Hash đọc từ DB[cite: 4]
                    Role = Convert.ToInt32(row["Role"]), //[cite: 4]
                    SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null, //[cite: 4]
                    CaLamViec = row["CaLamViec"] != DBNull.Value ? row["CaLamViec"].ToString() : null, //[cite: 4]
                    TrangThai = row["TrangThai"] != DBNull.Value ? Convert.ToBoolean(row["TrangThai"]) : false, //[cite: 4]
                };
            }
            return nv;
        }
    }
}
