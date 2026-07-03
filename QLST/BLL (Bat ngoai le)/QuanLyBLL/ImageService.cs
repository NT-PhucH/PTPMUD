using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLST.BLL__Bat_ngoai_le_.QuanLyBLL
{
    public class ImageService
    {
        private const string IMAGE_FOLDER = @"..\..\Resources";
        private const string PRODUCT_FOLDER = "Anh_SP";
        private readonly string _imgDir;

        public ImageService()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            _imgDir = Path.GetFullPath(Path.Combine(baseDir, IMAGE_FOLDER, PRODUCT_FOLDER));

            if (!Directory.Exists(_imgDir))
            {
                Directory.CreateDirectory(_imgDir);
            }
        }

        public (string fileName, string errorMessage) ProcessImage(string sourcePath, string maVach)
        {
            if (string.IsNullOrWhiteSpace(sourcePath)) return (string.Empty, string.Empty);
            if (!Path.IsPathRooted(sourcePath)) return (sourcePath, string.Empty);
            if (!File.Exists(sourcePath)) return (string.Empty, "File ảnh gốc không tồn tại.");

            var validationMsg = ValidateImageExtension(sourcePath);
            if (!string.IsNullOrEmpty(validationMsg)) return (string.Empty, validationMsg);

            try
            {
                string ext = Path.GetExtension(sourcePath).ToLower();
                string newFileName = GenerateFileName(maVach, ext);
                string destPath = Path.Combine(_imgDir, newFileName);

                File.Copy(sourcePath, destPath, true);
                return (newFileName, string.Empty);
            }
            catch (Exception ex)
            {
                return (string.Empty, $"Lỗi khi lưu/sao chép ảnh: {ex.Message}");
            }
        }

        private string ValidateImageExtension(string sourcePath)
        {
            string ext = Path.GetExtension(sourcePath).ToLower();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
            {
                return "Định dạng file không hợp lệ! (Chỉ hỗ trợ .jpg, .jpeg, .png)";
            }
            return string.Empty;
        }

        private string GenerateFileName(string maVach, string ext)
        {
            return $"{maVach}_{Guid.NewGuid().ToString("N").Substring(0, 6)}{ext}";
        }

        public void DeleteOldImage(string oldImageName)
        {
            if (string.IsNullOrEmpty(oldImageName)) return;

            string oldFilePath = Path.Combine(_imgDir, oldImageName);
            if (File.Exists(oldFilePath))
            {
                try
                {
                    File.Delete(oldFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Không thể xóa ảnh cũ: {ex.Message}");
                }
            }
        }
    }
}
