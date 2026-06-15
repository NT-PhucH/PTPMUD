// ===================================================
// File: KhachHang_BLL.cs
// Đặt vào: BLL (Bat ngoai le) > QuanLyBLL
// ===================================================
using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class KhachHang_BLL
    {
        private readonly KhachHang_DAL _dal = new KhachHang_DAL();

        // Quy tắc tích điểm: cứ 10.000đ = 1 điểm (lấy từ ThamSoHeThong nếu muốn)
        public int DiemPer10k { get; set; } = 1;

        public List<KhachHang_DTO> GetAll() => _dal.GetAll();
        public List<KhachHang_DTO> Search(string kw)
            => string.IsNullOrWhiteSpace(kw) ? _dal.GetAll() : _dal.Search(kw.Trim());

        public List<LichSuTichDiem_DTO> GetLichSu(int khachHangID)
            => _dal.GetLichSu(khachHangID, DiemPer10k);

        public (bool ok, string msg) Them(KhachHang_DTO kh)
        {
            if (string.IsNullOrWhiteSpace(kh.SDT))
                return (false, "Vui lòng nhập số điện thoại!");
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                return (false, "Vui lòng nhập tên khách hàng!");
            if (_dal.IsSDTExists(kh.SDT))
                return (false, "Số điện thoại đã tồn tại!");
            return _dal.Insert(kh)
                ? (true, "Thêm khách hàng thành công!")
                : (false, "Thêm thất bại, thử lại!");
        }

        public (bool ok, string msg) Sua(KhachHang_DTO kh)
        {
            if (string.IsNullOrWhiteSpace(kh.SDT))
                return (false, "Vui lòng nhập số điện thoại!");
            if (string.IsNullOrWhiteSpace(kh.TenKH))
                return (false, "Vui lòng nhập tên khách hàng!");
            if (_dal.IsSDTExists(kh.SDT, kh.KhachHangID))
                return (false, "Số điện thoại đã tồn tại ở khách hàng khác!");
            return _dal.Update(kh)
                ? (true, "Cập nhật thành công!")
                : (false, "Cập nhật thất bại!");
        }

        public (bool ok, string msg) Xoa(int id)
        {
            return _dal.Delete(id)
                ? (true, "Đã xóa khách hàng!")
                : (false, "Không thể xóa! Khách hàng này có lịch sử mua hàng.");
        }

        public (bool ok, string msg) CapNhatDiem(int id, int diemMoi)
        {
            if (diemMoi < 0) return (false, "Điểm không thể âm!");
            return _dal.CapNhatDiem(id, diemMoi)
                ? (true, "Cập nhật điểm thành công!")
                : (false, "Cập nhật điểm thất bại!");
        }
    }
}