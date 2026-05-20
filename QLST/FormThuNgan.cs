using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST
{
    public partial class FormThuNgan : Form
    {
        public FormThuNgan()
        {
            InitializeComponent();
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FormThuNgan_Load(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void panelAccount_MouseEnter(object sender, EventArgs e)
        {
            cmsAccount.Show(panelAccount, new Point(0, panelAccount.Height));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Khởi tạo đối tượng FormHoaDon
            HĐ formHoaDon = new HĐ();

            // Đặt vị trí xuất hiện của FormHoaDon là ở giữa Form cha (FormThuNgan)
            formHoaDon.StartPosition = FormStartPosition.CenterParent;

            // Hiển thị FormHoaDon dưới dạng Modal Dialog (nằm đè lên và khóa Form dưới)
            formHoaDon.ShowDialog();
        }
    }
}
