using QLST.DAL__Connection_Query_DB_.Query_DB; // Để gọi DB_Connection lấy chuỗi kết nối chung
using QLST.DTO__Type_OTP_;
using QLST.DTO__Type_OTP_.ThuNganOTP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_
{
    public class ThanhToan_DAL
    {
        // Sửa lại danh sách tham số (thêm int? khachHangID, int diemCong)
        public bool ThucHienThanhToan(string maHD, int nhanVienID, int? khachHangID, int diemCong, long tongTienCung, string phuongThucTT, long tienKhachDua, long tienThua, List<ThanhToan_DTO> dsChiTiet, out string message)
        {
            string connString = DB_Connection.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. CẬP NHẬT INSERT HOADON (Thêm cột KhachHangID)
                        string sqlHoaDon = @"INSERT INTO HoaDon (MaHD, NhanVienID, KhachHangID, ThoiGianTao, TongTienCung, PhuongThucThanhToan, TienKhachDua, TienThua) 
                                     OUTPUT INSERTED.HoaDonID 
                                     VALUES (@MaHD, @NhanVienID, @KhachHangID, GETDATE(), @TongTienCung, @PhuongThuc, @KhachDua, @TienThua)";

                        int hoaDonID;
                        using (SqlCommand cmdHD = new SqlCommand(sqlHoaDon, conn, trans))
                        {
                            cmdHD.Parameters.AddWithValue("@MaHD", maHD);
                            cmdHD.Parameters.AddWithValue("@NhanVienID", nhanVienID);
                            cmdHD.Parameters.AddWithValue("@KhachHangID", khachHangID ?? (object)DBNull.Value); // Nếu null thì lưu NULL
                            cmdHD.Parameters.AddWithValue("@TongTienCung", tongTienCung);
                            cmdHD.Parameters.AddWithValue("@PhuongThuc", phuongThucTT ?? (object)DBNull.Value);
                            cmdHD.Parameters.AddWithValue("@KhachDua", tienKhachDua);
                            cmdHD.Parameters.AddWithValue("@TienThua", tienThua);

                            hoaDonID = (int)cmdHD.ExecuteScalar();
                        }

                        // (Phần 2: INSERT ChiTietHoaDon VÀ UPDATE SanPham GIỮ NGUYÊN NHƯ CŨ)
                        string sqlChiTiet = @"INSERT INTO ChiTietHoaDon (HoaDonID, SanPhamID, SoLuongMua, DonGiaBan, ThanhTien) VALUES (@HoaDonID, @SanPhamID, @SoLuongMua, @DonGiaBan, @ThanhTien)";
                        string sqlTruKho = @"UPDATE SanPham SET TonKhoTong = ISNULL(TonKhoTong, 0) - @SoLuongMua WHERE SanPhamID = @SanPhamID";

                        foreach (var item in dsChiTiet)
                        {
                            using (SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn, trans)) { /* code cũ của bạn */ }
                            using (SqlCommand cmdKho = new SqlCommand(sqlTruKho, conn, trans)) { /* code cũ của bạn */ }
                        }

                        // 3. TÍCH ĐIỂM CHO KHÁCH HÀNG (Nếu có chọn khách)
                        if (khachHangID.HasValue && diemCong > 0)
                        {
                            // Update trực tiếp bảng KhachHang, không cần JOIN
                            string sqlTichDiem = "UPDATE KhachHang SET DiemTichLuy = ISNULL(DiemTichLuy, 0) + @DiemCong WHERE KhachHangID = @KhachHangID";
                            using (SqlCommand cmdKH = new SqlCommand(sqlTichDiem, conn, trans))
                            {
                                cmdKH.Parameters.AddWithValue("@DiemCong", diemCong);
                                cmdKH.Parameters.AddWithValue("@KhachHangID", khachHangID.Value);
                                cmdKH.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        message = "Thanh toán đơn hàng hoàn tất!";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        message = "Lỗi xử lý cơ sở dữ liệu: " + ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}