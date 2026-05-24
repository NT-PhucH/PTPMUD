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
        public KhungMonHang()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        public void CapNhatThongTin(string maSP, string tenSP, decimal donGia)
        {
            // Giả sử bạn đã đặt tên các Label trong KhungMonHang tương ứng như sau:
            lblMaSP.Text = maSP;
            lblTenSP.Text = tenSP;
            lblDonGia.Text = donGia.ToString("N0"); // Format số tiền có dấu phẩy
            lblThanhTien.Text = donGia.ToString("N0");
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                txtSoLuong.Text = (soLuong + 1).ToString();
            }
        }
        // 1. Trong file KhungMonHang.cs, thêm hàm gán Số thứ tự:
        public void GanSTT(int stt)
        {
            lblSTT.Text = stt.ToString(); // Thay "lblSTT" bằng tên Label chứa số thứ tự của bạn
        }

        // 2. Cập nhật lại sự kiện nút Xóa (trong KhungMonHang.cs) để tự động đánh lại số cho chuẩn khi có thẻ bị xóa:
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                Control parent = this.Parent;
                parent.Controls.Remove(this);
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
            }
        }


    }
}
