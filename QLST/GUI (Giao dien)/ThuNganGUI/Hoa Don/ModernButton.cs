using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLST
{
    public class ModernButton : Button
    {
        private int borderRadius = 12;

        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        public ModernButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 45);
            this.BackColor = Color.FromArgb(0, 85, 204);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Resize += (sender, e) => this.Invalidate();
        }

        // HÀM MỚI: Dò tìm màu nền thực sự phía sau nút
        private Color GetRealBackColor()
        {
            Control parent = this.Parent;
            while (parent != null && parent.BackColor == Color.Transparent)
            {
                parent = parent.Parent;
            }
            return parent != null ? parent.BackColor : Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // FIX LỖI GÓC ĐEN: Sử dụng màu nền thực tế tìm được
            e.Graphics.Clear(GetRealBackColor());

            // Vẽ nền bo tròn
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                path.AddArc(this.Width - borderRadius - 1, 0, borderRadius, borderRadius, 270, 90);
                path.AddArc(this.Width - borderRadius - 1, this.Height - borderRadius - 1, borderRadius, borderRadius, 0, 90);
                path.AddArc(0, this.Height - borderRadius - 1, borderRadius, borderRadius, 90, 90);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            // Vẽ chữ căn giữa
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, flags);
        }
    }
}