using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class QLNV_DAL
    {

        // 1. TÌM KIẾM & LỌC DỮ LIỆU
        public List<NhanVienDTO> Search(string keyword, int roleFilter, bool showInactive)
        {
            var list = new List<NhanVienDTO>();
            string sql = @"
                SELECT NhanVienID, MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai 
                FROM NhanVien 
                WHERE (MaNV LIKE '%' + @kw + '%' OR TenNV LIKE N'%' + @kw + '%' OR SoDienThoai LIKE '%' + @kw + '%')";

            if (roleFilter > 0) sql += " AND Role = @Role";
            if (!showInactive) sql += " AND TrangThai = 1"; // Chỉ hiện người đang làm

            var parameters = new List<SqlParameter> { new SqlParameter("@kw", keyword) };
            if (roleFilter > 0) parameters.Add(new SqlParameter("@Role", roleFilter));

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());
            foreach (DataRow row in dt.Rows) list.Add(MapRow(row));
            return list;
        }

        // 2. THÊM MỚI NHÂN VIÊN
        public bool Insert(NhanVienDTO nv)
        {
            string sql = @"INSERT INTO NhanVien (MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai) 
                           VALUES (@MaNV, @TenNV, @User, @Pass, @Role, @SDT, @Ca, @TrangThai)";
            return DataProvider.Instance.ExecuteNonQuery(sql, CreateParameters(nv).ToArray()) > 0;
        }

        // 3. CẬP NHẬT NHÂN VIÊN
        public bool Update(NhanVienDTO nv)
        {
            string sql = @"UPDATE NhanVien SET MaNV=@MaNV, TenNV=@TenNV, Username=@User, Password=@Pass, 
                           Role=@Role, SoDienThoai=@SDT, CaLamViec=@Ca, TrangThai=@TrangThai 
                           WHERE NhanVienID=@ID";
            var p = CreateParameters(nv);
            p.Add(new SqlParameter("@ID", nv.NhanVienID));
            return DataProvider.Instance.ExecuteNonQuery(sql, p.ToArray()) > 0;
        }


        // 5. UNIQUE CHECK (Kiểm tra trùng Mã hoặc Username)
        public bool IsMaNV_Or_Username_Exist(string maNV, string user, int excludeID = 0)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE (MaNV = @Ma OR Username = @User) AND NhanVienID <> @ID";
            object res = DataProvider.Instance.ExecuteScalar(sql, new SqlParameter[] {
                new SqlParameter("@Ma", maNV), new SqlParameter("@User", user), new SqlParameter("@ID", excludeID)
            });
            return Convert.ToInt32(res) > 0;
        }

        // 6. TỰ ĐỘNG TẠO MÃ
        public string GenerateMaNV()
        {
            object res = DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM NhanVien");
            return $"NV{Convert.ToInt32(res) + 1:D3}";
        }

        // HELPER
        private List<SqlParameter> CreateParameters(NhanVienDTO nv) => new List<SqlParameter> {
            new SqlParameter("@MaNV", nv.MaNV), new SqlParameter("@TenNV", nv.TenNV),
            new SqlParameter("@User", nv.Username), new SqlParameter("@Pass", nv.Password), // Cần băm MD5 ở BLL trước khi truyền vào đây
            new SqlParameter("@Role", nv.Role), new SqlParameter("@TrangThai", nv.TrangThai),
            new SqlParameter("@SDT", (object)nv.SoDienThoai ?? DBNull.Value),
            new SqlParameter("@Ca", (object)nv.CaLamViec ?? DBNull.Value)
        };

        private NhanVienDTO MapRow(DataRow row) => new NhanVienDTO
        {
            NhanVienID = Convert.ToInt32(row["NhanVienID"]),
            MaNV = row["MaNV"].ToString(),
            TenNV = row["TenNV"].ToString(),
            Username = row["Username"].ToString(),
            Password = row["Password"].ToString(),
            Role = Convert.ToInt32(row["Role"]),
            TrangThai = Convert.ToBoolean(row["TrangThai"]),
            SoDienThoai = row["SoDienThoai"]?.ToString(),
            CaLamViec = row["CaLamViec"]?.ToString()
        };

        public bool ToggleTrangThai(int id)
        {
            // Dùng phép toán ~ (NOT) để đảo giá trị bit: 1 thành 0, 0 thành 1
            string sql = "UPDATE NhanVien SET TrangThai = ~TrangThai WHERE NhanVienID = @ID";
            return DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
        new SqlParameter("@ID", id)
    }) > 0;
        }
    }
}
