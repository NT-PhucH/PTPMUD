using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.Thu_Ngan
{
    public partial class ChuyenTrang : UserControl
    {
        public event EventHandler BamNutTrai;
        public event EventHandler BamNutPhai;

        public ChuyenTrang()
        {
            InitializeComponent();
            // ÉP LIÊN KẾT SỰ KIỆN: Đảm bảo khi bấm nút vật lý, hàm code sẽ chạy
            // Bạn thay 'btnTrai' và 'btnPhai' thành đúng ID nút bấm của bạn trong Designer nếu có khác nhé
            this.btnTrai.Click += new System.EventHandler(this.btnTrai_Click);
            this.btnPhai.Click += new System.EventHandler(this.btnPhai_Click);
        }

        private void btnTrai_Click(object sender, EventArgs e)
        {
            // Bắn tín hiệu lên cho Form cha biết là nút Trái vừa bị bấm
            BamNutTrai?.Invoke(this, e);
        }

        // 3. Sự kiện khi click nút Mũi Tên Phải (bên trong UserControl)
        private void btnPhai_Click(object sender, EventArgs e)
        {
            // Bắn tín hiệu lên cho Form cha biết là nút Phải vừa bị bấm
            BamNutPhai?.Invoke(this, e);
        }

        // 4. Hàm để Form cha ra lệnh cập nhật chữ ở Label
        public void CapNhatThongTinTrang(int trangHienTai, int tongSoTrang)
        {
            pageNumber.Text = $"{trangHienTai} / {tongSoTrang}";
        }

        private void btnMuiTenPhai_Click(object sender, EventArgs e)
        {

        }
    }
}
