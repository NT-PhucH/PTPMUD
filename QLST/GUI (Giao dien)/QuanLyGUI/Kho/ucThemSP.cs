using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Để dùng Interaction.InputBox (Cần Add Reference Microsoft.VisualBasic)

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucThemSP : UserControl
    {
        private readonly SanPham_BLL _spBLL = new SanPham_BLL();
        private string _duongDanAnh = string.Empty;

        public ucThemSP()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            this.Load += UcThemSP_Load;
            btnChonAnh.Click += BtnChonAnh_Click;
            btnThemLoai.Click += BtnThemLoai_Click;
            btnLuu.Click += BtnLuu_Click;
        }

        private void UcThemSP_Load(object sender, EventArgs e)
        {
            LoadLoaiSanPham();
        }

        private void LoadLoaiSanPham()
        {
            var dsLoai = _spBLL.GetAllLoai();
            cmbLoaiSP.DataSource = dsLoai;
            cmbLoaiSP.DisplayMember = "TenLoai";
            cmbLoaiSP.ValueMember = "LoaiSanPhamID";
            cmbLoaiSP.SelectedIndex = -1;
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Chọn ảnh sản phẩm";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _duongDanAnh = ofd.FileName;
                    picHinhAnh.Image = Image.FromFile(_duongDanAnh);
                }
            }
        }

        private void BtnThemLoai_Click(object sender, EventArgs e)
        {
            string tenLoaiMoi = Interaction.InputBox("Nhập tên loại sản phẩm mới:", "Thêm loại sản phẩm", "");
            if (!string.IsNullOrWhiteSpace(tenLoaiMoi))
            {
                var (ok, msg) = _spBLL.ThemLoai(tenLoaiMoi);
                if (ok)
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLoaiSanPham(); // Tải lại danh sách
                    cmbLoaiSP.Text = tenLoaiMoi; // Chọn loại vừa thêm
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cmbLoaiSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoaiSP.Focus();
                return;
            }

            if (!int.TryParse(txtGiaBan.Text.Trim(), out int giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            SanPham_DTO spMoi = new SanPham_DTO
            {
                MaVach = txtMaVach.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                GiaBanHienTai = giaBan,
                LoaiSanPhamID = (int)cmbLoaiSP.SelectedValue,
                TonKhoTong = 0, // Mặc định theo giao diện
                HinhAnh = _duongDanAnh
            };

            var (ok, msg) = _spBLL.ThemSanPham(spMoi);

            if (ok)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtMaVach.Clear();
            txtTenSP.Clear();
            txtGiaBan.Clear();
            cmbLoaiSP.SelectedIndex = -1;
            _duongDanAnh = string.Empty;
            if (picHinhAnh.Image != null)
            {
                picHinhAnh.Image.Dispose();
                picHinhAnh.Image = null;
            }
            txtMaVach.Focus();
        }
    }
}