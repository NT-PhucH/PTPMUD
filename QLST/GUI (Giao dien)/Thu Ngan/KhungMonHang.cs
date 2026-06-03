using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_
{
    public partial class KhungMonHang : UserControl
    {
        private decimal donGiaGoc = 0;

        // Thêm event để báo cáo thay đổi lên FormThuNgan
        public event EventHandler DuLieuThayDoi;

        // Thêm 2 thuộc tính để FormThuNgan có thể lấy số liệu
        public int SoLuong => int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0;
        public decimal ThanhTien => donGiaGoc * SoLuong;

        public KhungMonHang()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e) { }

        private void TinhThanhTien()
        {
            if (int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                lblThanhTien.Text = (donGiaGoc * soLuong).ToString("N0");
            }
            else
            {
                lblThanhTien.Text = "0";
            }
            // Kích hoạt sự kiện mỗi khi tiền hoặc số lượng thay đổi
            DuLieuThayDoi?.Invoke(this, EventArgs.Empty);
        }

        public void CapNhatThongTin(string maSP, string tenSP, decimal donGia)
        {
            lblMaSP.Text = maSP;
            lblTenSP.Text = tenSP;
            donGiaGoc = donGia;
            lblDonGia.Text = donGia.ToString("N0");
            TinhThanhTien();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                txtSoLuong.Text = (soLuong + 1).ToString();
                TinhThanhTien();
            }
        }

        public void GanSTT(int stt)
        {
            lblSTT.Text = stt.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                Control parent = this.Parent;
                parent.Controls.Remove(this);

                // Gọi event báo cho FormThuNgan biết đã xóa sản phẩm để tính lại tổng
                DuLieuThayDoi?.Invoke(this, EventArgs.Empty);

                this.Dispose();

                int stt = parent.Controls.Count;
                foreach (Control ctrl in parent.Controls)
                {
                    if (ctrl is KhungMonHang card)
                    {
                        card.GanSTT(stt);
                        stt--;
                    }
                }
            }
        }

        public string MaSP
        {
            get { return lblMaSP.Text; }
        }

        public void TangSoLuong()
        {
            if (int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                txtSoLuong.Text = (soLuong + 1).ToString();
                TinhThanhTien();
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }
    }
}