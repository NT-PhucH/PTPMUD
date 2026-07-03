using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class QLNV_DAL
    {
        // 1. TÌM KIẾM & LỌC DỮ LIỆU
        public List<QLNV_DTO> Search(string keyword, int roleFilter, bool showInactive)
        {
            var list = new List<QLNV_DTO>();
            string sql = @"
                SELECT NhanVienID, MaNV, TenNV, Username, Role, SoDienThoai, CaLamViec, TrangThai 
                FROM NhanVien 
                WHERE (MaNV LIKE '%' + @kw + '%' OR TenNV LIKE N'%' + @kw + '%' OR SoDienThoai LIKE '%' + @kw + '%')";

            if (roleFilter > 0) sql += " AND Role = @Role";
            if (!showInactive) sql += " AND TrangThai = 1";

            var parameters = new List<SqlParameter> { new SqlParameter("@kw", keyword) };
            if (roleFilter > 0) parameters.Add(new SqlParameter("@Role", roleFilter));

            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, parameters.ToArray());
            foreach (DataRow row in dt.Rows) list.Add(MapRow(row));
            return list;
        }

        // 2. THÊM MỚI NHÂN VIÊN (TỰ SINH MÃ BẰNG C#)
        public bool Insert(QLNV_DTO nv)
        {
            // 1. Lấy ID lớn nhất và tự sinh MaNV mới (Logic từ GenerateMaNV đưa vào đây)
            object res = DataProvider.Instance.ExecuteScalar("SELECT MAX(NhanVienID) FROM NhanVien");
            int maxId = (res == DBNull.Value || res == null) ? 0 : Convert.ToInt32(res);
            string newMaNV = $"NV{(maxId + 1):D3}"; // Format thành NV001, NV002...

            // 2. Câu lệnh INSERT
            string sql = @"INSERT INTO NhanVien (MaNV, TenNV, Username, Password, Role, SoDienThoai, CaLamViec, TrangThai) 
                   VALUES (@MaNV, @TenNV, @User, @Pass, @Role, @SDT, @Ca, @TrangThai)";

            // 3. Tạo tham số và gán mã NV tự sinh vào
            var p = CreateParameters(nv);

            // Tìm và cập nhật lại tham số @MaNV trong danh sách (hoặc add mới nếu hàm CreateParameters chưa add)
            var maNVParam = p.Find(x => x.ParameterName == "@MaNV");
            if (maNVParam != null)
            {
                maNVParam.Value = newMaNV;
            }
            else
            {
                p.Add(new SqlParameter("@MaNV", newMaNV));
            }

            return DataProvider.Instance.ExecuteNonQuery(sql, p.ToArray()) > 0;
        }

        // 3. CẬP NHẬT NHÂN VIÊN (Đã bỏ MaNV khỏi lệnh Update)
        public bool Update(QLNV_DTO nv)
        {
            string sql = @"UPDATE NhanVien 
                           SET TenNV=@TenNV, Username=@User, Password=@Pass, 
                               Role=@Role, SoDienThoai=@SDT, CaLamViec=@Ca 
                           WHERE NhanVienID=@ID";

            var p = CreateParameters(nv);
            p.Add(new SqlParameter("@ID", nv.NhanVienID));
            return DataProvider.Instance.ExecuteNonQuery(sql, p.ToArray()) > 0;
        }


        // 5. KIỂM TRA TRÙNG TÀI KHOẢN (Dùng cho cả Thêm và Sửa)
        public bool IsUsernameExist(string user, int excludeID = 0)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE Username = @User AND NhanVienID <> @ID";
            object res = DataProvider.Instance.ExecuteScalar(sql, new SqlParameter[] {
                new SqlParameter("@User", user), new SqlParameter("@ID", excludeID)
            });
            return Convert.ToInt32(res) > 0;
        }


        // 7. BẬT TẮT TRẠNG THÁI
        public bool ToggleTrangThai(int id)
        {
            string sql = "UPDATE NhanVien SET TrangThai = CASE WHEN TrangThai = 1 THEN 0 ELSE 1 END WHERE NhanVienID = @ID";
            return DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@ID", id) }) > 0;
        }

        // HELPER
        private List<SqlParameter> CreateParameters(QLNV_DTO nv) => new List<SqlParameter> {
            new SqlParameter("@MaNV", nv.MaNV ?? ""),
            new SqlParameter("@TenNV", nv.TenNV),
            new SqlParameter("@User", nv.Username),
            new SqlParameter("@Pass", nv.Password),
            new SqlParameter("@Role", nv.Role),
            new SqlParameter("@TrangThai", nv.TrangThai),
            new SqlParameter("@SDT", (object)nv.SoDienThoai ?? DBNull.Value),
            new SqlParameter("@Ca", (object)nv.CaLamViec ?? DBNull.Value)
        };

        private QLNV_DTO MapRow(DataRow row) => new QLNV_DTO
        {
            NhanVienID = Convert.ToInt32(row["NhanVienID"]),
            MaNV = row["MaNV"].ToString(),
            TenNV = row["TenNV"].ToString(),
            Username = row["Username"].ToString(),
            Role = Convert.ToInt32(row["Role"]),
            TrangThai = Convert.ToBoolean(row["TrangThai"]),
            SoDienThoai = row["SoDienThoai"]?.ToString(),
            CaLamViec = row["CaLamViec"]?.ToString()
        };
    }
}