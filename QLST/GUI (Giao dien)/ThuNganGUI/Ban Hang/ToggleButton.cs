using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLST
{
    public partial class ToggleButton : CheckBox
    {
        // Các thuộc tính để bạn dễ dàng chỉnh màu trong Properties của VS
        public Color OnBackendColor { get; set; } = Color.FromArgb(0, 123, 255); // Màu xanh giống ảnh
        public Color OffBackendColor { get; set; } = Color.Gray;
        public Color ToggleColor { get; set; } = Color.White;

        public ToggleButton()
        {
            this.Size = new Size(60, 30); // Kích thước mặc định
        }

        // Hàm lấy đường dẫn bo tròn
        private GraphicsPath GetFigurePath()
        {
            int arcSize = this.Height - 1;
            Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle rightArc = new Rectangle(this.Width - arcSize - 1, 0, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();
            return path;
        }

        // Vẽ lại giao diện nút
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // ĐÃ SỬA DÒNG NÀY: Gọi hàm GetRealBackColor() để fix triệt để lỗi viền đen
            pevent.Graphics.Clear(GetRealBackColor());

            if (this.Checked) // Trạng thái ON
            {
                // Vẽ nền xanh
                using (SolidBrush brush = new SolidBrush(OnBackendColor))
                {
                    pevent.Graphics.FillPath(brush, GetFigurePath());
                }
                // Vẽ nút tròn màu trắng bên phải
                using (SolidBrush brush = new SolidBrush(ToggleColor))
                {
                    pevent.Graphics.FillEllipse(brush, this.Width - this.Height + 2, 2, toggleSize, toggleSize);
                }
            }
            else // Trạng thái OFF
            {
                // Vẽ nền xám
                using (SolidBrush brush = new SolidBrush(OffBackendColor))
                {
                    pevent.Graphics.FillPath(brush, GetFigurePath());
                }
                // Vẽ nút tròn màu trắng bên trái
                using (SolidBrush brush = new SolidBrush(ToggleColor))
                {
                    pevent.Graphics.FillEllipse(brush, 2, 2, toggleSize, toggleSize);
                }
            }
        }

        private Color GetRealBackColor()
        {
            Control parent = this.Parent;
            while (parent != null && parent.BackColor == Color.Transparent)
            {
                parent = parent.Parent;
            }
            return parent != null ? parent.BackColor : Color.White;
        }
    }
}