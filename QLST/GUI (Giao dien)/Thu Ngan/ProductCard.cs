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
    public partial class ProductCard : UserControl
    {
        public event EventHandler OnSelectProduct;

        public ProductCard()
        {
            InitializeComponent();
        }

        public string ProductName
        {
            get { return lblProductName.Text; }
            set { lblProductName.Text = value; }
        }

        public string ProductPrice
        {
            get { return lblProductPrice.Text; }
            set { lblProductPrice.Text = value; }
        }

        public Image ProductImage
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        private void tlpSP_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                foreach (Control ctrl in this.Parent.Controls)
                {
                    if (ctrl is ProductCard card)
                    {
                        card.BackColor = Color.White;
                    }
                }
            }
            this.BackColor = Color.Blue;
        }

        private void tlpSP_DoubleClick(object sender, EventArgs e)
        {
            OnSelectProduct?.Invoke(this, EventArgs.Empty);
        }
    }
}