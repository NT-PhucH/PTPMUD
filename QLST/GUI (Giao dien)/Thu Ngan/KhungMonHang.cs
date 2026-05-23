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
    }
}
