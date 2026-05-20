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
    public partial class HĐ : Form
    {
        public HĐ()
        {
            InitializeComponent();
        }

        private void HĐ_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Báo cho FormThuNgan biết là hành động này bị hủy
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Báo cho FormThuNgan biết là hành động này bị hủy
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Báo cho FormThuNgan biết là hành động này bị hủy
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Báo cho FormThuNgan biết là hành động này bị hủy
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // 2. Hiển thị ảnh QR lên
            pictureQR.Visible = true;
        }

        private void pictureQR_Click(object sender, EventArgs e)
        {

        }

        private void radioTienMat_CheckedChanged(object sender, EventArgs e)
        {
            pictureQR.Visible = false;
        }
    }
}
