using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.DTO__Type_OTP_
{
    public class HoaDonDTO
    {
        // ID định danh đơn lưu tạm trên RAM (dùng Guid để không bao giờ trùng)
        public string HoldId { get; set; } = Guid.NewGuid().ToString();

        // Thời gian lúc ấn nút "Lưu tạm" - dùng để hiển thị lên Dropdown của cái chuông
        public DateTime ThoiGianLuuTam { get; set; }

        // Tên hiển thị trên Dropdown (VD: "Đơn lúc 14:30 - 3 SP")
        public string TenHienThi
        {
            get { return $"Đơn {ThoiGianLuuTam.ToString("HH:mm")} - {DanhSachSanPham.Count} SP"; }
        }

        // Thông tin liên quan đến bảng HoaDon
        public string MaNV { get; set; } // Nhân viên nào đang xử lý đơn này

        // SDT khách hàng (có thể null nếu khách vãng lai hoặc chưa kịp nhập thông tin khách)
        public string SDT_KhachHang { get; set; }

        // Chứa danh sách các mặt hàng khách đã quét
        public List<ChiTietHDDTO> DanhSachSanPham { get; set; } = new List<ChiTietHDDTO>();

        // Tổng tiền của đơn lưu tạm
        public decimal TongTienCung
        {
            get { return DanhSachSanPham.Sum(x => x.ThanhTien); }
            set { this.TongTienCung = value; }
            }
    }
}
