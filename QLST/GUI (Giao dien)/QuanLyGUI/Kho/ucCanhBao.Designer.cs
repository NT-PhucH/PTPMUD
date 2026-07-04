// ===================================================
// 1. File giao diện: ucCanhBao.Designer.cs
// ===================================================
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucCanhBao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Panel pHet;
        private Panel pNguongInfo;
        private Label lblNguongInfo;
        private Button btnReloadSapHet;
        private DataGridView dgvSapHet;

        private Panel pHan;
        private Panel pHanInfo;
        private Label lblHanInfo;
        private Button btnReloadSapHan;
        private DataGridView dgvSapHetHan;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(242, 245, 250);
            this.Font = new Font("Segoe UI", 9.5f);
            this.Size = new Size(1000, 700);

            // --- Panel Hàng sắp hết tồn kho ---
            pHet = BuildCard(12, 12, this.Width - 24, (this.Height - 36) / 2);

            var titleHet = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(230, 50, 50) };
            titleHet.Paint += (s, e) =>
            {
                using (var f = new Font("Segoe UI", 10f, FontStyle.Bold)) { e.Graphics.DrawString("🔴  HÀNG SẮP HẾT TỒN KHO", f, Brushes.White, 16, 14); }
            };

            pNguongInfo = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.FromArgb(255, 240, 240) };
            lblNguongInfo = new Label { Text = "Ngưỡng cảnh báo lấy từ cài đặt hệ thống.", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic), ForeColor = Color.FromArgb(150, 30, 30) };
            pNguongInfo.Controls.Add(lblNguongInfo);

            btnReloadSapHet = MakeStyledButton("↺ Tải lại", 0, 6, 90, Color.FromArgb(200, 60, 60), 26);
            btnReloadSapHet.Dock = DockStyle.Right;
            pNguongInfo.Controls.Add(btnReloadSapHet);

            dgvSapHet = BuildDgvFlat(new[] {
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Tên sản phẩm", 42, DataGridViewContentAlignment.MiddleLeft),
                ("Loại hàng", 22, DataGridViewContentAlignment.MiddleLeft),
                ("Tồn kho", 20, DataGridViewContentAlignment.MiddleCenter)
            });
            dgvSapHet.Dock = DockStyle.Fill;
            pHet.Controls.Add(dgvSapHet); pHet.Controls.Add(pNguongInfo); pHet.Controls.Add(titleHet);

            // --- Panel Hàng sắp hết hạn sử dụng ---
            pHan = BuildCard(12, 12 + pHet.Height + 8, this.Width - 24, (this.Height - 36) / 2);

            var titleHan = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(200, 140, 0) };
            titleHan.Paint += (s, e) =>
            {
                using (var f = new Font("Segoe UI", 10f, FontStyle.Bold)) { e.Graphics.DrawString("🟡  HÀNG SẮP HẾT HẠN SỬ DỤNG", f, Brushes.White, 16, 14); }
            };

            pHanInfo = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.FromArgb(255, 248, 220) };
            lblHanInfo = new Label { Text = "Số ngày cảnh báo lấy từ cài đặt hệ thống.", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 8.5f, FontStyle.Italic), ForeColor = Color.FromArgb(130, 90, 0) };
            pHanInfo.Controls.Add(lblHanInfo);

            btnReloadSapHan = MakeStyledButton("↺ Tải lại", 0, 6, 90, Color.FromArgb(180, 120, 0), 26);
            btnReloadSapHan.Dock = DockStyle.Right;
            pHanInfo.Controls.Add(btnReloadSapHan);

            dgvSapHetHan = BuildDgvFlat(new[] {
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Tên sản phẩm", 38, DataGridViewContentAlignment.MiddleLeft),
                ("Loại hàng", 22, DataGridViewContentAlignment.MiddleLeft),
                ("Hạn sử dụng", 14, DataGridViewContentAlignment.MiddleCenter),
                ("Còn lại", 10, DataGridViewContentAlignment.MiddleCenter)
            });
            dgvSapHetHan.Dock = DockStyle.Fill;
            pHan.Controls.Add(dgvSapHetHan); pHan.Controls.Add(pHanInfo); pHan.Controls.Add(titleHan);

            this.Controls.Add(pHet);
            this.Controls.Add(pHan);

            this.Resize += UcCanhBao_Resize;

            this.ResumeLayout(false);
        }

        private void UcCanhBao_Resize(object sender, System.EventArgs e)
        {
            int halfH = (this.Height - 36) / 2;
            pHet.Location = new Point(12, 12);
            pHet.Width = this.Width - 24;
            pHet.Height = halfH;

            pHan.Location = new Point(12, 12 + halfH + 8);
            pHan.Width = this.Width - 24;
            pHan.Height = halfH;
        }

        // --- GIAO DIỆN HELPERS ---
        private Panel BuildCard(int x, int y, int w, int h)
        {
            var p = new Panel { BackColor = Color.White, Location = new Point(x, y), Width = w, Height = h, Padding = new Padding(0) };
            return p;
        }
        private Button MakeStyledButton(string text, int x, int y, int w, Color color, int h = 32) => new Button { Text = text, Location = new Point(x, y), Width = w, Height = h, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold), Cursor = Cursors.Hand };

        private DataGridView BuildDgvFlat((string header, int weight, DataGridViewContentAlignment align)[] cols)
        {
            var dgv = new DataGridView { BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 36, RowTemplate = { Height = 30 }, GridColor = Color.FromArgb(220, 225, 235), CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 38, 58);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 247, 255);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 90, 200);
            foreach (var (header, weight, align) in cols) dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = header, FillWeight = weight, DefaultCellStyle = { Alignment = align } });
            return dgv;
        }
    }
}