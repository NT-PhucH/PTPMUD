using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QLST
{
    public class UnderlineTextBox : UserControl
    {
        private TextBox txt;
        private Panel pnlLine;

        [Category("Appearance")]
        [Description("Màu sắc của đường gạch dưới.")]
        [Browsable(true)]
        public Color LineColor
        {
            get { return pnlLine.BackColor; }
            set { pnlLine.BackColor = value; }
        }

        [Category("Appearance")]
        [Description("Căn lề cho chữ (Trái, Phải, Giữa).")]
        [Browsable(true)]
        public HorizontalAlignment TextAlign
        {
            get { return txt.TextAlign; }
            set { txt.TextAlign = value; }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        // THÊM MỚI: Thuộc tính này giúp quản lý vị trí con trỏ chuột nhấp nháy
        [Browsable(false)]
        public int SelectionStart
        {
            get { return txt.SelectionStart; }
            set { txt.SelectionStart = value; }
        }

        public UnderlineTextBox()
        {
            txt = new TextBox();
            txt.BorderStyle = BorderStyle.None;
            txt.Dock = DockStyle.Top;
            txt.TextAlign = HorizontalAlignment.Right;

            // QUAN TRỌNG NHẤT: Báo cho Form biết mỗi khi người dùng gõ phím
            txt.TextChanged += (s, e) => this.OnTextChanged(e);

            pnlLine = new Panel();
            pnlLine.Height = 2;
            pnlLine.Dock = DockStyle.Bottom;
            pnlLine.BackColor = Color.Black;

            this.Controls.Add(pnlLine);
            this.Controls.Add(txt);

            this.BackColor = Color.White;
            txt.BackColor = this.BackColor;
            this.Height = txt.Height + pnlLine.Height + 3;
        }

        public void Clear()
        {
            txt.Clear();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            txt.BackColor = this.BackColor;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = txt.Height + pnlLine.Height + 3;
        }
        // Bắt buộc TextBox bên trong phải đổi màu theo UserControl
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                txt.ForeColor = value;
            }
        }
    }
}