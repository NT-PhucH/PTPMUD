using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucThemNCC : UserControl
    {
        private readonly NhaCungCap_BLL _nccBLL = new NhaCungCap_BLL();

        public ucThemNCC()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            btnThem.Click += BtnThem_Click;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Nhà Cung Cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            NhaCungCap_DTO nccMoi = new NhaCungCap_DTO
            {
                TenNCC = txtTenNCC.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim()
            };

            var (ok, msg) = _nccBLL.Them(nccMoi);

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
            txtTenNCC.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtTenNCC.Focus();
        }
    }
}