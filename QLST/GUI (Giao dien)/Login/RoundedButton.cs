using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLST
{
    public class RoundedButton : Button
    {
        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.FromArgb(25, 25, 25);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.Parent != null)
            {
                e.Graphics.Clear(this.Parent.BackColor);
            }
            else
            {
                e.Graphics.Clear(Color.White);
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = this.Height - 1;
                path.AddArc(0, 0, radius, radius, 90, 180);
                path.AddArc(this.Width - radius - 1, 0, radius, radius, 270, 180);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, flags);
        }
    }
}