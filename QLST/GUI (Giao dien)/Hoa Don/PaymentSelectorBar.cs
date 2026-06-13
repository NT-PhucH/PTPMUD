using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLST
{
    public partial class PaymentSelectorBar : UserControl
    {
        private bool isCashSelected = true;

        // Màu sắc chuẩn từ ảnh của bạn
        private Color selectedColor = Color.FromArgb(28, 93, 233); // Xanh đậm
        private Color unselectedBarColor = Color.FromArgb(242, 242, 242); // Xám nhạt
        private Color selectedTextColor = Color.White;
        private Color unselectedTextColor = Color.DimGray;
        private Font mainFont = new Font("Segoe UI", 12f, FontStyle.Bold);

        public PaymentSelectorBar()
        {
            InitializeComponent();

            // Xóa bỏ các control cũ (TableLayout, Button) do Designer sinh ra để ta tự vẽ
            this.Controls.Clear();

            this.DoubleBuffered = true; // Rất quan trọng: Chống giật/nháy hình khi vẽ
            this.Size = new Size(350, 50);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
        }

        // --- Gửi dữ liệu ra Form chính ---
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCashSelected => isCashSelected;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTransferSelected => !isCashSelected;

        // --- XỬ LÝ CLICK CHUỘT ---
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            // Kích vào nửa trái là Tiền mặt, nửa phải là Chuyển khoản
            if (e.X < this.Width / 2)
                isCashSelected = true;
            else
                isCashSelected = false;

            this.Invalidate(); // Yêu cầu Form vẽ lại màu sắc ngay lập tức
        }

        // --- TỰ VẼ GIAO DIỆN SIÊU MƯỢT ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Bật làm mịn viền (Khử răng cưa tuyệt đối)
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int radius = 15; // Độ bo tròn góc
            Rectangle rectFull = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            // 1. Vẽ nền Xám nhạt cho toàn bộ thanh
            using (GraphicsPath pathFull = GetRoundedPath(rectFull, radius))
            using (SolidBrush brushBg = new SolidBrush(unselectedBarColor))
            {
                g.FillPath(brushBg, pathFull);
            }

            // 2. Chia đôi khu vực
            int halfWidth = this.Width / 2;
            Rectangle rectCash = new Rectangle(0, 0, halfWidth, this.Height - 1);
            Rectangle rectTransfer = new Rectangle(halfWidth, 0, halfWidth - 1, this.Height - 1);

            // 3. Vẽ nền Xanh cho nút đang được chọn
            using (SolidBrush brushSel = new SolidBrush(selectedColor))
            {
                if (isCashSelected)
                {
                    using (GraphicsPath pathCash = GetRoundedPath(rectCash, radius))
                        g.FillPath(brushSel, pathCash);
                }
                else
                {
                    using (GraphicsPath pathTransfer = GetRoundedPath(rectTransfer, radius))
                        g.FillPath(brushSel, pathTransfer);
                }
            }

            // 4. Vẽ Nội dung (Icon, Chữ, Dấu tích)
            DrawButtonContent(g, "Tiền mặt", rectCash, isCashSelected, true);
            DrawButtonContent(g, "Chuyển khoản", rectTransfer, !isCashSelected, false);
        }

        // --- HÀM VẼ CHI TIẾT BÊN TRONG MỖI NÚT ---
        private void DrawButtonContent(Graphics g, string text, Rectangle bounds, bool isSelected, bool isCashIcon)
        {
            Color textColor = isSelected ? selectedTextColor : unselectedTextColor;

            // Căn giữa nội dung
            SizeF textSize = g.MeasureString(text, mainFont);
            int iconWidth = 24;
            int spacing = 8;
            int totalWidth = iconWidth + spacing + (int)textSize.Width;

            int startX = bounds.X + (bounds.Width - totalWidth) / 2;
            int startY = bounds.Y + (bounds.Height - 24) / 2; // 24 là chiều cao icon

            // Vẽ Icon tiền/điện thoại
            DrawIcon(g, startX, startY, textColor, isCashIcon);

            // Vẽ Chữ
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                g.DrawString(text, mainFont, textBrush, startX + iconWidth + spacing, bounds.Y + (bounds.Height - textSize.Height) / 2);
            }

            // Vẽ dấu tích ở góc trên bên phải (Giống y hệt ảnh 2)
            if (isSelected)
            {
                int checkRadius = 8;
                int checkX = bounds.Right - 24; // Cách lề phải
                int checkY = bounds.Top + 6;    // Cách lề trên

                using (Pen checkPen = new Pen(Color.White, 1.5f))
                {
                    // Vẽ vòng tròn viền trắng
                    g.DrawEllipse(checkPen, checkX, checkY, checkRadius * 2, checkRadius * 2);

                    // Vẽ 2 nét của dấu tích
                    checkPen.Width = 2f;
                    g.DrawLines(checkPen, new Point[] {
                        new Point(checkX + 4, checkY + 8),
                        new Point(checkX + 7, checkY + 11),
                        new Point(checkX + 12, checkY + 5)
                    });
                }
            }
        }

        // --- HÀM VẼ ICON CHUẨN ---
        private void DrawIcon(Graphics g, int x, int y, Color color, bool isCash)
        {
            using (Pen p = new Pen(color, 1.5f))
            {
                if (isCash) // Icon Tiền
                {
                    g.DrawRoundedRectangleCustom(p, x + 2, y + 6, 20, 12, 3);
                    g.DrawEllipse(p, x + 9, y + 9, 6, 6);
                }
                else // Icon Chuyển khoản (Điện thoại)
                {
                    g.DrawRoundedRectangleCustom(p, x + 8, y + 2, 10, 20, 2);
                    g.DrawEllipse(p, x + 12, y + 18, 2, 2);
                    g.DrawArc(p, x + 2, y + 4, 8, 8, 200, 140);
                    g.DrawArc(p, x + 5, y + 7, 4, 4, 200, 140);
                }
            }
        }

        // --- HÀM TẠO KHUNG BO TRÒN ---
        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // Helper vẽ góc bo tròn cho Icon
    public static class GraphicsExtensionsHelper
    {
        public static void DrawRoundedRectangleCustom(this Graphics g, Pen p, int x, int y, int width, int height, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(x, y, radius, radius, 180, 90);
                path.AddArc(x + width - radius, y, radius, radius, 270, 90);
                path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
                path.AddArc(x, y + height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                g.DrawPath(p, path);
            }
        }
    }
}