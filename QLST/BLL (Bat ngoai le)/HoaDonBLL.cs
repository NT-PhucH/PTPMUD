using QLST.DAL__Connection_Query_DB_;
using QLST.DTO__Type_OTP_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_
{
    public class HoaDonBLL
    {
        private HoaDonDAL hoaDonDAL = new HoaDonDAL();
        private DonTamDAL donTamDAL = new DonTamDAL(); // Khởi tạo thêm DAL xử lý bảng tạm

        // 1. Lấy số lượng đơn đang chờ (để hiển thị lên chuông vàng)
        public int SoLuongDonTam
        {
            get
            {
                // Đếm số dòng trả về từ Database
                DataTable dt = donTamDAL.LayDanhSachDonTam();
                return dt.Rows.Count;
            }
        }

        // 2. Lấy danh sách để đổ lên Dropdown menu của chuông vàng
        public List<HoaDonDTO> LayDanhSachDonTam()
        {
            List<HoaDonDTO> listDonTam = new List<HoaDonDTO>();
            DataTable dt = donTamDAL.LayDanhSachDonTam();

            // Lặp qua từng dòng của DataTable để ép kiểu sang DTO
            foreach (DataRow row in dt.Rows)
            {
                HoaDonDTO don = new HoaDonDTO();
                don.HoldId = row["HoldId"].ToString();
                don.ThoiGianLuuTam = Convert.ToDateTime(row["ThoiGianLuuTam"]);

                // Nếu DTO của bạn có field TongTienCung thì map luôn ở đây để hiển thị
                if (row.Table.Columns.Contains("TongTienCung") && row["TongTienCung"] != DBNull.Value)
                {
                    don.TongTienCung = Convert.ToDecimal(row["TongTienCung"]);
                }

                listDonTam.Add(don);
            }
            return listDonTam;
        }

        // 3. Đẩy 1 đơn vào Database (Lưu xuống bảng HoaDonTam)
        public bool LuuTamDonHang(HoaDonDTO donHang)
        {
            // Kiểm tra an toàn trước khi lưu
            if (string.IsNullOrEmpty(donHang.HoldId))
            {
                donHang.HoldId = Guid.NewGuid().ToString(); // Đảm bảo luôn có ID
            }
            donHang.ThoiGianLuuTam = DateTime.Now;

            return donTamDAL.LuuDonTamXuongDB(donHang);
        }

        // 4. Lấy đơn ra khỏi Database (Khôi phục) và XÓA nó khỏi bảng tạm
        public HoaDonDTO KhoiPhucDonHang(string holdId)
        {
            HoaDonDTO donKhoiPhuc = new HoaDonDTO();
            donKhoiPhuc.HoldId = holdId;

            // Chú ý: Cần đổi tên List<ChiTietHoaDonDTO> cho khớp với thuộc tính trong HoaDonDTO của bạn
            donKhoiPhuc.DanhSachSanPham = new List<ChiTietHDDTO>();

            // Gọi DAL lấy chi tiết các mặt hàng của đơn này
            DataTable dtChiTiet = donTamDAL.LayChiTietDonTam(holdId);

            foreach (DataRow row in dtChiTiet.Rows)
            {
                ChiTietHDDTO ct = new ChiTietHDDTO();
                ct.MaVach = row["MaVach"].ToString();
                ct.SoLuongMua = Convert.ToInt32(row["SoLuongMua"]);
                ct.DongGiaBan = Convert.ToDecimal(row["DongGiaBan"]);

                // Thuộc tính mở rộng để giao diện biết Tên SP mà hiển thị
                if (row.Table.Columns.Contains("TenSP"))
                {
                    ct.TenSP = row["TenSP"].ToString();
                }

                donKhoiPhuc.DanhSachSanPham.Add(ct);
            }

            // Nếu rút dữ liệu thành công, ta xóa đơn tạm này khỏi Database để tránh rác
            if (donKhoiPhuc.DanhSachSanPham.Count > 0)
            {
                donTamDAL.XoaDonTam(holdId);
            }

            return donKhoiPhuc;
        }

        // 5. Chuyển đơn hàng xuống DAL để lưu bảng HoaDon chính thức khi thanh toán xong
        public bool ThanhToanDonHang(HoaDonDTO donHang, decimal tienKhachDua, decimal tienThua)
        {
            return hoaDonDAL.LuuHoaDonChinhThuc(donHang, tienKhachDua, tienThua);
        }
    }
}
