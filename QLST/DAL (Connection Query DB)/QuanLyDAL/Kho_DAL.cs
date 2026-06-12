// ===================================================
// File: Kho_DAL.cs
// Đặt vào: DAL (Connection Query DB) > QuanLyDAL
// ===================================================
using QLST.DAL__Connection_Query_DB_.Query_DB;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLST.DAL__Connection_Query_DB_.QuanLyDAL
{
    public class Kho_DAL
    {
        // ════════════════════════════════════════════════════════════════════
        // PHIẾU NHẬP
        // ════════════════════════════════════════════════════════════════════

        public List<PhieuNhap_DTO> GetAllPhieuNhap()
        {
            var list = new List<PhieuNhap_DTO>();
            string sql = @"
                SELECT pn.PhieuNhapID, pn.MaPN, pn.NhaCungCapID, ncc.TenNCC,
                       pn.NhanVienID, nv.TenNV, pn.NgayLap, pn.TongTienThanhToan
                FROM PhieuNhap pn
                LEFT JOIN NhaCungCap ncc ON pn.NhaCungCapID = ncc.NhaCungCapID
                LEFT JOIN NhanVien   nv  ON pn.NhanVienID   = nv.NhanVienID
                ORDER BY pn.NgayLap DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(MapPhieuNhap(row));
            return list;
        }

        public List<PhieuNhap_DTO> GetPhieuNhapByDate(DateTime from, DateTime to)
        {
            var list = new List<PhieuNhap_DTO>();
            string sql = @"
                SELECT pn.PhieuNhapID, pn.MaPN, pn.NhaCungCapID, ncc.TenNCC,
                       pn.NhanVienID, nv.TenNV, pn.NgayLap, pn.TongTienThanhToan
                FROM PhieuNhap pn
                LEFT JOIN NhaCungCap ncc ON pn.NhaCungCapID = ncc.NhaCungCapID
                LEFT JOIN NhanVien   nv  ON pn.NhanVienID   = nv.NhanVienID
                WHERE CAST(pn.NgayLap AS DATE) BETWEEN @From AND @To
                ORDER BY pn.NgayLap DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@From", from.Date),
                new SqlParameter("@To",   to.Date)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(MapPhieuNhap(row));
            return list;
        }

        public List<ChiTietPhieuNhap_DTO> GetChiTietPhieuNhap(int phieuNhapID)
        {
            var list = new List<ChiTietPhieuNhap_DTO>();
            string sql = @"
                SELECT ct.MaChiTietPN, ct.PhieuNhapID, ct.SanPhamID,
                       sp.TenSP, sp.MaVach,
                       ct.SoLuongNhap, ct.GiaNhap, ct.NSX, ct.HSD, ct.SoLuongTonCuaLoNay
                FROM ChiTietPhieuNhap_LoHang ct
                JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
                WHERE ct.PhieuNhapID = @ID";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", phieuNhapID)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new ChiTietPhieuNhap_DTO
                {
                    MaChiTietPN = Convert.ToInt32(row["MaChiTietPN"]),
                    PhieuNhapID = Convert.ToInt32(row["PhieuNhapID"]),
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    TenSP = row["TenSP"].ToString(),
                    MaVach = row["MaVach"].ToString(),
                    SoLuongNhap = Convert.ToInt32(row["SoLuongNhap"]),
                    GiaNhap = Convert.ToInt32(row["GiaNhap"]),
                    NSX = row["NSX"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NSX"]),
                    HSD = row["HSD"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["HSD"]),
                    SoLuongTonKho = Convert.ToInt32(row["SoLuongTonCuaLoNay"])
                });
            return list;
        }

        // Tạo phiếu nhập + cập nhật tồn kho (transaction)
        public bool TaoPhieuNhap(PhieuNhap_DTO phieu, List<ChiTietPhieuNhap_DTO> chiTiet)
        {
            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Insert PhieuNhap
                    string sqlPN = @"
                        INSERT INTO PhieuNhap (MaPN, NhaCungCapID, NhanVienID, NgayLap, TongTienThanhToan)
                        VALUES (@MaPN, @NccID, @NvID, GETDATE(), @Tong);
                        SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdPN = new SqlCommand(sqlPN, conn, trans);
                    cmdPN.Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@MaPN",  phieu.MaPN),
                        new SqlParameter("@NccID", phieu.NhaCungCapID),
                        new SqlParameter("@NvID",  phieu.NhanVienID),
                        new SqlParameter("@Tong",  phieu.TongTienThanhToan)
                    });
                    int newID = Convert.ToInt32(cmdPN.ExecuteScalar());

                    // 2. Insert từng ChiTiet + cập nhật TonKhoTong
                    foreach (var ct in chiTiet)
                    {
                        string sqlCT = @"
                            INSERT INTO ChiTietPhieuNhap_LoHang
                                (PhieuNhapID, SanPhamID, SoLuongNhap, GiaNhap, NSX, HSD, SoLuongTonCuaLoNay)
                            VALUES (@PN, @SP, @SL, @Gia, @NSX, @HSD, @SLTon)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("@PN",    newID),
                            new SqlParameter("@SP",    ct.SanPhamID),
                            new SqlParameter("@SL",    ct.SoLuongNhap),
                            new SqlParameter("@Gia",   ct.GiaNhap),
                            new SqlParameter("@NSX",   (object)ct.NSX ?? DBNull.Value),
                            new SqlParameter("@HSD",   (object)ct.HSD ?? DBNull.Value),
                            new SqlParameter("@SLTon", ct.SoLuongNhap)
                        });
                        cmdCT.ExecuteNonQuery();

                        // Cộng tồn kho
                        string sqlTon = "UPDATE SanPham SET TonKhoTong = TonKhoTong + @SL WHERE SanPhamID = @SP";
                        SqlCommand cmdTon = new SqlCommand(sqlTon, conn, trans);
                        cmdTon.Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("@SL", ct.SoLuongNhap),
                            new SqlParameter("@SP", ct.SanPhamID)
                        });
                        cmdTon.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // PHIẾU XUẤT
        // ════════════════════════════════════════════════════════════════════

        public List<PhieuXuat_DTO> GetAllPhieuXuat()
        {
            var list = new List<PhieuXuat_DTO>();
            string sql = @"
                SELECT px.PhieuXuatID, px.MaPX, px.NhanVienID, nv.TenNV,
                       px.NgayXuat, px.LyDo, px.GhiChu
                FROM PhieuXuat px
                LEFT JOIN NhanVien nv ON px.NhanVienID = nv.NhanVienID
                ORDER BY px.NgayXuat DESC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(new PhieuXuat_DTO
                {
                    PhieuXuatID = Convert.ToInt32(row["PhieuXuatID"]),
                    MaPX = row["MaPX"].ToString(),
                    NhanVienID = row["NhanVienID"] == DBNull.Value ? 0 : Convert.ToInt32(row["NhanVienID"]),
                    TenNV = row["TenNV"] == DBNull.Value ? "" : row["TenNV"].ToString(),
                    NgayXuat = Convert.ToDateTime(row["NgayXuat"]),
                    LyDo = row["LyDo"].ToString(),
                    GhiChu = row["GhiChu"] == DBNull.Value ? "" : row["GhiChu"].ToString()
                });
            return list;
        }

        public List<ChiTietPhieuXuat_DTO> GetChiTietPhieuXuat(int phieuXuatID)
        {
            var list = new List<ChiTietPhieuXuat_DTO>();
            string sql = @"
                SELECT ct.MaCTPX, ct.PhieuXuatID, ct.SanPhamID,
                       sp.TenSP, sp.MaVach, ct.SoLuongXuat, sp.TonKhoTong, ct.GhiChu
                FROM ChiTietPhieuXuat ct
                JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
                WHERE ct.PhieuXuatID = @ID";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@ID", phieuXuatID)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new ChiTietPhieuXuat_DTO
                {
                    MaCTPX = Convert.ToInt32(row["MaCTPX"]),
                    PhieuXuatID = Convert.ToInt32(row["PhieuXuatID"]),
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    TenSP = row["TenSP"].ToString(),
                    MaVach = row["MaVach"].ToString(),
                    SoLuongXuat = Convert.ToInt32(row["SoLuongXuat"]),
                    TonKhoHienTai = Convert.ToInt32(row["TonKhoTong"]),
                    GhiChu = row["GhiChu"] == DBNull.Value ? "" : row["GhiChu"].ToString()
                });
            return list;
        }

        // Tạo phiếu xuất + trừ tồn kho (transaction)
        public (bool ok, string msg) TaoPhieuXuat(PhieuXuat_DTO phieu, List<ChiTietPhieuXuat_DTO> chiTiet)
        {
            using (SqlConnection conn = new SqlConnection(DB_Connection.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // Kiểm tra tồn kho trước
                    foreach (var ct in chiTiet)
                    {
                        string sqlCheck = "SELECT TonKhoTong FROM SanPham WHERE SanPhamID = @SP";
                        SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans);
                        cmdCheck.Parameters.Add(new SqlParameter("@SP", ct.SanPhamID));
                        int ton = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        if (ct.SoLuongXuat > ton)
                        {
                            trans.Rollback();
                            return (false, $"Sản phẩm '{ct.TenSP}' chỉ còn {ton} trong kho!");
                        }
                    }

                    // Insert PhieuXuat
                    string sqlPX = @"
                        INSERT INTO PhieuXuat (MaPX, NhanVienID, NgayXuat, LyDo, GhiChu)
                        VALUES (@MaPX, @NvID, GETDATE(), @LyDo, @GhiChu);
                        SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdPX = new SqlCommand(sqlPX, conn, trans);
                    cmdPX.Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@MaPX",   phieu.MaPX),
                        new SqlParameter("@NvID",   phieu.NhanVienID > 0 ? (object)phieu.NhanVienID : DBNull.Value),
                        new SqlParameter("@LyDo",   phieu.LyDo),
                        new SqlParameter("@GhiChu", (object)phieu.GhiChu ?? DBNull.Value)
                    });
                    int newID = Convert.ToInt32(cmdPX.ExecuteScalar());

                    // Insert chi tiết + trừ tồn
                    foreach (var ct in chiTiet)
                    {
                        string sqlCT = @"
                            INSERT INTO ChiTietPhieuXuat (PhieuXuatID, SanPhamID, SoLuongXuat, GhiChu)
                            VALUES (@PX, @SP, @SL, @GhiChu)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("@PX",     newID),
                            new SqlParameter("@SP",     ct.SanPhamID),
                            new SqlParameter("@SL",     ct.SoLuongXuat),
                            new SqlParameter("@GhiChu", (object)ct.GhiChu ?? DBNull.Value)
                        });
                        cmdCT.ExecuteNonQuery();

                        // Trừ tồn kho
                        string sqlTon = "UPDATE SanPham SET TonKhoTong = TonKhoTong - @SL WHERE SanPhamID = @SP";
                        SqlCommand cmdTon = new SqlCommand(sqlTon, conn, trans);
                        cmdTon.Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("@SL", ct.SoLuongXuat),
                            new SqlParameter("@SP", ct.SanPhamID)
                        });
                        cmdTon.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return (true, "Xuất kho thành công!");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return (false, "Lỗi: " + ex.Message);
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════
        // CẢNH BÁO KHO
        // ════════════════════════════════════════════════════════════════════

        // Hàng sắp hết tồn (mặc định <= 10)
        public List<CanhBaoKho_DTO> GetHangSapHet(int nguong = 10)
        {
            var list = new List<CanhBaoKho_DTO>();
            string sql = @"
                SELECT sp.SanPhamID, sp.MaVach, sp.TenSP, sp.TonKhoTong,
                       lsp.TenLoai, NULL AS HSD
                FROM SanPham sp
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                WHERE sp.TonKhoTong <= @Nguong
                ORDER BY sp.TonKhoTong ASC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@Nguong", nguong)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new CanhBaoKho_DTO
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    MaVach = row["MaVach"].ToString(),
                    TenSP = row["TenSP"].ToString(),
                    TonKhoTong = Convert.ToInt32(row["TonKhoTong"]),
                    TenLoai = row["TenLoai"] == DBNull.Value ? "" : row["TenLoai"].ToString(),
                    LoaiCanhBao = "SapHet"
                });
            return list;
        }

        // Hàng sắp hết hạn (trong vòng 30 ngày tới)
        public List<CanhBaoKho_DTO> GetHangSapHetHan(int soNgay = 30)
        {
            var list = new List<CanhBaoKho_DTO>();
            string sql = @"
                SELECT DISTINCT sp.SanPhamID, sp.MaVach, sp.TenSP, sp.TonKhoTong,
                       lsp.TenLoai, MIN(ct.HSD) AS HSD
                FROM ChiTietPhieuNhap_LoHang ct
                JOIN SanPham sp ON ct.SanPhamID = sp.SanPhamID
                LEFT JOIN LoaiSanPham lsp ON sp.LoaiSanPhamID = lsp.LoaiSanPhamID
                WHERE ct.HSD IS NOT NULL
                  AND ct.HSD <= DATEADD(DAY, @SoNgay, GETDATE())
                  AND ct.HSD >= GETDATE()
                  AND ct.SoLuongTonCuaLoNay > 0
                GROUP BY sp.SanPhamID, sp.MaVach, sp.TenSP, sp.TonKhoTong, lsp.TenLoai
                ORDER BY HSD ASC";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new SqlParameter[] {
                new SqlParameter("@SoNgay", soNgay)
            });
            foreach (DataRow row in dt.Rows)
                list.Add(new CanhBaoKho_DTO
                {
                    SanPhamID = Convert.ToInt32(row["SanPhamID"]),
                    MaVach = row["MaVach"].ToString(),
                    TenSP = row["TenSP"].ToString(),
                    TonKhoTong = Convert.ToInt32(row["TonKhoTong"]),
                    HSD = row["HSD"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["HSD"]),
                    TenLoai = row["TenLoai"] == DBNull.Value ? "" : row["TenLoai"].ToString(),
                    LoaiCanhBao = "HetHan"
                });
            return list;
        }

        // Sinh mã phiếu tự động
        public string SinhMaPhieuNhap()
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM PhieuNhap");
            int count = Convert.ToInt32(result) + 1;
            return $"PN{DateTime.Now:yyyyMMdd}{count:D4}";
        }

        public string SinhMaPhieuXuat()
        {
            object result = DataProvider.Instance.ExecuteScalar(
                "SELECT COUNT(*) FROM PhieuXuat");
            int count = Convert.ToInt32(result) + 1;
            return $"PX{DateTime.Now:yyyyMMdd}{count:D4}";
        }

        // ── HELPER ────────────────────────────────────────────────────────────
        private PhieuNhap_DTO MapPhieuNhap(DataRow row) => new PhieuNhap_DTO
        {
            PhieuNhapID = Convert.ToInt32(row["PhieuNhapID"]),
            MaPN = row["MaPN"].ToString(),
            NhaCungCapID = row["NhaCungCapID"] == DBNull.Value ? 0 : Convert.ToInt32(row["NhaCungCapID"]),
            TenNCC = row["TenNCC"] == DBNull.Value ? "" : row["TenNCC"].ToString(),
            NhanVienID = row["NhanVienID"] == DBNull.Value ? 0 : Convert.ToInt32(row["NhanVienID"]),
            TenNV = row["TenNV"] == DBNull.Value ? "" : row["TenNV"].ToString(),
            NgayLap = Convert.ToDateTime(row["NgayLap"]),
            TongTienThanhToan = Convert.ToInt64(row["TongTienThanhToan"])
        };
    }
}