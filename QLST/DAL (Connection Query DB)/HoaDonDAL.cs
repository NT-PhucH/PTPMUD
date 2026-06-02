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
    public class HoaDonDAL
    {
        public bool LuuHoaDonChinhThuc(HoaDonDTO donHang, decimal tienKhachDua, decimal tienThua)
        {
            // Sử dụng StringBuilder để nối chuỗi lệnh SQL một cách tối ưu
            StringBuilder query = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            // Bắt đầu khối Transaction của SQL Server
            query.AppendLine("BEGIN TRY");
            query.AppendLine("    BEGIN TRAN;");

            // 1. Gọi hàm sinh mã tự động và gán vào biến @NewMaHD
            query.AppendLine("    DECLARE @NewMaHD VARCHAR(15) = dbo.fn_SinhMaHoaDon();");

            // 2. Insert Hóa Đơn sử dụng biến @NewMaHD
            query.AppendLine(@"    INSERT INTO HoaDon (MaHD, MaNV, SDT_KhachHang, ThoiGianTao, TongTienCung, TienKhachDua, TienThua) 
                                   VALUES (@NewMaHD, @MaNV, @SDT, GETDATE(), @TongTien, @TienDua, @TienThua);");

            // Truyền tham số cho Hóa đơn (Xử lý null nếu không có SĐT khách hàng)
            parameters.Add(new SqlParameter("@MaNV", string.IsNullOrEmpty(donHang.MaNV) ? (object)DBNull.Value : donHang.MaNV));
            parameters.Add(new SqlParameter("@SDT", string.IsNullOrEmpty(donHang.SDT_KhachHang) ? (object)DBNull.Value : donHang.SDT_KhachHang));
            parameters.Add(new SqlParameter("@TongTien", donHang.TongTienCung));
            parameters.Add(new SqlParameter("@TienDua", tienKhachDua));
            parameters.Add(new SqlParameter("@TienThua", tienThua));

            // 3. Vòng lặp Insert Chi Tiết Hóa Đơn
            // Vì DataProvider dùng mảng Parameter, ta phải tạo tên parameter động (VD: @MaVach0, @MaVach1...)
            for (int i = 0; i < donHang.DanhSachSanPham.Count; i++)
            {
                var sp = donHang.DanhSachSanPham[i];

                query.AppendLine($@"    INSERT INTO ChiTietHoaDon (MaHD, MaVach, SoLuongMua, DongGiaBan, ThanhTien) 
                                        VALUES (@NewMaHD, @MaVach{i}, @SoLuong{i}, @DongGia{i}, @ThanhTien{i});");

                // Thêm tham số động vào List
                parameters.Add(new SqlParameter($"@MaVach{i}", sp.MaVach));
                parameters.Add(new SqlParameter($"@SoLuong{i}", sp.SoLuongMua));
                parameters.Add(new SqlParameter($"@DongGia{i}", sp.DongGiaBan));
                parameters.Add(new SqlParameter($"@ThanhTien{i}", sp.ThanhTien));
            }

            // 4. Kết thúc thành công thì Commit, lỗi thì nhảy xuống Catch Rollback
            query.AppendLine("    COMMIT TRAN;");
            query.AppendLine("END TRY");
            query.AppendLine("BEGIN CATCH");
            query.AppendLine("    ROLLBACK TRAN;");
            query.AppendLine("    THROW;"); //Quăng lỗi ngược lại cho C# bắt
            query.AppendLine("END CATCH");

            try
            {
                // Gọi duy nhất 1 lần ExecuteNonQuery của DataProvider
                int rowsAffected = DataProvider.Instance.ExecuteNonQuery(query.ToString(), parameters.ToArray());

                // Mặc dù query phức tạp, nhưng SQL sẽ trả về tổng số dòng bị ảnh hưởng > 0 nếu thành công
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi ex.Message ra file hoặc Console nếu cần
                return false;
            }
        }
    }
}
