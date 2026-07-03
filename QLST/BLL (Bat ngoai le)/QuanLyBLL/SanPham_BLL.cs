using QLST.DAL__Connection_Query_DB_.QuanLyDAL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System.Collections.Generic;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class SanPham_BLL
    {
        private readonly SanPham_DAL _dal = new SanPham_DAL();
        private readonly ImageService _imageService = new ImageService();

        public List<SanPham_DTO> GetAll() => _dal.GetAll();

        public List<SanPham_DTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return _dal.GetAll();
            return _dal.Search(keyword.Trim());
        }

        public List<SanPham_DTO> GetByLoai(int loaiID)
        {
            if (loaiID <= 0) return _dal.GetAll();
            return _dal.GetByLoai(loaiID);
        }

        public List<LoaiSanPham_DTO> GetAllLoai() => _dal.GetAllLoai();

        public (bool ok, string msg) ThemSanPham(SanPham_DTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaVach)) return (false, "Vui lòng nhập mã vạch!");
            if (string.IsNullOrWhiteSpace(sp.TenSP)) return (false, "Vui lòng nhập tên sản phẩm!");
            if (sp.GiaBanHienTai <= 0) return (false, "Giá bán phải lớn hơn 0!");
            if (sp.LoaiSanPhamID <= 0) return (false, "Vui lòng chọn loại sản phẩm!");
            if (_dal.IsMaVachExists(sp.MaVach)) return (false, "Mã vạch đã tồn tại!");

            var processResult = _imageService.ProcessImage(sp.HinhAnh, sp.MaVach);
            if (!string.IsNullOrEmpty(processResult.errorMessage))
            {
                return (false, processResult.errorMessage);
            }
            sp.HinhAnh = processResult.fileName;

            bool result = _dal.Insert(sp);
            return result ? (true, "Thêm sản phẩm thành công!") : (false, "Thêm thất bại, thử lại!");
        }

        public (bool ok, string msg) ThayDoiTrangThai(int sanPhamID)
        {
            if (sanPhamID <= 0) return (false, "Không xác định được ID sản phẩm!");

            bool result = _dal.ToggleTrangThai(sanPhamID);
            return result ? (true, "Đã cập nhật trạng thái sản phẩm!") : (false, "Cập nhật thất bại, thử lại!");
        }

        public (bool ok, string msg) ThemLoai(string tenLoai)
        {
            if (string.IsNullOrWhiteSpace(tenLoai)) return (false, "Vui lòng nhập tên loại!");
            bool result = _dal.InsertLoai(tenLoai.Trim());
            return result ? (true, "Đã thêm loại!") : (false, "Thêm loại thất bại!");
        }

        public (bool ok, string msg) SuaSanPham(SanPham_DTO sp, string oldImageName = "")
        {
            if (sp.SanPhamID <= 0) return (false, "Không xác định được ID sản phẩm!");
            if (string.IsNullOrWhiteSpace(sp.TenSP)) return (false, "Vui lòng nhập tên sản phẩm!");
            if (sp.GiaBanHienTai <= 0) return (false, "Giá bán phải lớn hơn 0!");
            if (sp.LoaiSanPhamID <= 0) return (false, "Vui lòng chọn loại sản phẩm!");

            var processResult = _imageService.ProcessImage(sp.HinhAnh, sp.MaVach);
            if (!string.IsNullOrEmpty(processResult.errorMessage))
            {
                return (false, processResult.errorMessage);
            }
            sp.HinhAnh = processResult.fileName;

            bool result = _dal.Update(sp);

            if (result)
            {
                if (!string.IsNullOrEmpty(oldImageName) && sp.HinhAnh != oldImageName)
                {
                    _imageService.DeleteOldImage(oldImageName);
                }
                return (true, "Cập nhật thành công!");
            }

            return (false, "Cập nhật thất bại, thử lại!");
        }
    }
}