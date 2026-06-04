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
        public bool ThucHienThanhToan(string maHD, int nhanVienID, long tongTienCung, string phuongThucTT, long tienKhachDua, long tienThua, List<ThanhToan_DTO> dsChiTiet, out string message)
        {
            string connString = DB_Connection.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // Khởi tạo Transaction để khóa luồng ghi dữ liệu an toàn
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. CHÈN DỮ LIỆU VÀO BẢNG HOADON & LẤY LẠI HOADONID VỪA TỰ TĂNG
                        string sqlHoaDon = @"INSERT INTO HoaDon (MaHD, NhanVienID, ThoiGianTao, TongTienCung, PhuongThucThanhToan, TienKhachDua, TienThua) 
                                             OUTPUT INSERTED.HoaDonID 
                                             VALUES (@MaHD, @NhanVienID, GETDATE(), @TongTienCung, @PhuongThuc, @KhachDua, @TienThua)";

                        int hoaDonID;
                        using (SqlCommand cmdHD = new SqlCommand(sqlHoaDon, conn, trans))
                        {
                            cmdHD.Parameters.AddWithValue("@MaHD", maHD);
                            cmdHD.Parameters.AddWithValue("@NhanVienID", nhanVienID);
                            cmdHD.Parameters.AddWithValue("@TongTienCung", tongTienCung);
                            cmdHD.Parameters.AddWithValue("@PhuongThuc", phuongThucTT ?? (object)DBNull.Value);
                            cmdHD.Parameters.AddWithValue("@KhachDua", tienKhachDua);
                            cmdHD.Parameters.AddWithValue("@TienThua", tienThua);

                            hoaDonID = (int)cmdHD.ExecuteScalar(); // Thực thi và trả về giá trị OUTPUT
                        }

                        // 2. VÒNG LẶP XỬ LÝ CHI TIẾT HÓA ĐƠN VÀ TRỪ TỒN KHO SẢN PHẨM
                        string sqlChiTiet = @"INSERT INTO ChiTietHoaDon (HoaDonID, SanPhamID, SoLuongMua, DonGiaBan, ThanhTien) 
                                              VALUES (@HoaDonID, @SanPhamID, @SoLuongMua, @DonGiaBan, @ThanhTien)";

                        string sqlTruKho = @"UPDATE SanPham SET TonKhoTong = ISNULL(TonKhoTong, 0) - @SoLuongMua 
                                             WHERE SanPhamID = @SanPhamID";

                        foreach (var item in dsChiTiet)
                        {
                            // 2.1. Ghi dữ liệu vào bảng ChiTietHoaDon
                            using (SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@HoaDonID", hoaDonID);
                                cmdCT.Parameters.AddWithValue("@SanPhamID", item.SanPhamID);
                                cmdCT.Parameters.AddWithValue("@SoLuongMua", item.SoLuongMua);
                                cmdCT.Parameters.AddWithValue("@DonGiaBan", item.DonGiaBan);
                                cmdCT.Parameters.AddWithValue("@ThanhTien", item.ThanhTien);
                                cmdCT.ExecuteNonQuery();
                            }

                            // 2.2. Cập nhật trừ số lượng tồn kho trong bảng SanPham
                            using (SqlCommand cmdKho = new SqlCommand(sqlTruKho, conn, trans))
                            {
                                cmdKho.Parameters.AddWithValue("@SanPhamID", item.SanPhamID);
                                cmdKho.Parameters.AddWithValue("@SoLuongMua", item.SoLuongMua);
                                cmdKho.ExecuteNonQuery();
                            }
                        }

                        // Nếu chạy mượt hết đến đây thì chốt lưu dữ liệu chính thức xuống ổ đĩa
                        trans.Commit();
                        message = "Thanh toán đơn hàng hoàn tất!";
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Nếu dính bất kỳ lỗi nào (rớt mạng, sai kiểu dữ liệu...) lập tức quay xe hủy toàn bộ lệnh trước đó
                        trans.Rollback();
                        message = "Lỗi xử lý cơ sở dữ liệu: " + ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}