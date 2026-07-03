using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucXuatKho
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

        private Panel pnlXuatLeft;
        private ComboBox cboLyDo, cboSPXuat;
        private Button btnThemSPXuat;
        private TextBox txtSLXuat, txtGhiChuXuat, txtGhiChuPhieuXuat;
        private DataGridView dgvGioXuat;
        private Button btnThemVaoGioXuat;
        private Button btnXoaDongXuat;
        private Button btnLuuPhieuXuat;
        private Panel pRight;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(242, 245, 250);
            this.Font = new Font("Segoe UI", 9.5f);
            this.Size = new Size(1000, 700);

            // Left Panel
            pnlXuatLeft = BuildCard(12, 12, 360, this.Height - 24, anchorAll: true);
            int y = 16;
            AddSectionTitle(pnlXuatLeft, "Tạo phiếu xuất kho", ref y);

            AddFieldLabel(pnlXuatLeft, "Lý do xuất *", y);
            cboLyDo = MakeCombo(16, y + 20, 328);
            cboLyDo.Items.AddRange(new object[] { "Hàng hỏng / Vỡ", "Mất mát / Thất lạc", "Hết hạn sử dụng", "Xuất nội bộ", "Khác" });
            cboLyDo.SelectedIndex = 0;
            pnlXuatLeft.Controls.Add(cboLyDo); y += 66;

            AddFieldLabel(pnlXuatLeft, "Sản phẩm *", y);
            var rowSP = new Panel { Location = new Point(16, y + 20), Width = 328, Height = 34 };
            cboSPXuat = MakeCombo(0, 0, 288);
            btnThemSPXuat = MakeIconButton("＋", 294, 0, 34, Color.FromArgb(40, 90, 200));
            rowSP.Controls.Add(cboSPXuat); rowSP.Controls.Add(btnThemSPXuat);
            pnlXuatLeft.Controls.Add(rowSP); y += 66;

            AddFieldLabel(pnlXuatLeft, "Số lượng xuất *", y);
            txtSLXuat = MakeTextBox(16, y + 20, 328); txtSLXuat.Text = "1";
            pnlXuatLeft.Controls.Add(txtSLXuat); y += 66;

            AddFieldLabel(pnlXuatLeft, "Ghi chú dòng", y);
            txtGhiChuXuat = MakeTextBox(16, y + 20, 328);
            pnlXuatLeft.Controls.Add(txtGhiChuXuat); y += 66;

            btnThemVaoGioXuat = MakeStyledButton("➕  Thêm vào phiếu", 16, y, 152, Color.FromArgb(26, 140, 80), 36);
            pnlXuatLeft.Controls.Add(btnThemVaoGioXuat);

            btnXoaDongXuat = MakeStyledButton("🗑  Xóa dòng chọn", 182, y, 162, Color.FromArgb(195, 50, 50), 36);
            pnlXuatLeft.Controls.Add(btnXoaDongXuat); y += 52;

            AddSeparator(pnlXuatLeft, y); y += 16;

            AddFieldLabel(pnlXuatLeft, "Ghi chú phiếu", y);
            txtGhiChuPhieuXuat = MakeTextBox(16, y + 20, 328); txtGhiChuPhieuXuat.Height = 60; txtGhiChuPhieuXuat.Multiline = true;
            pnlXuatLeft.Controls.Add(txtGhiChuPhieuXuat); y += 90;

            btnLuuPhieuXuat = MakeStyledButton("💾   LƯU PHIẾU XUẤT", 16, y, 328, Color.FromArgb(150, 35, 35), 42);
            pnlXuatLeft.Controls.Add(btnLuuPhieuXuat);

            // Right Panel
            pRight = BuildCard(384, 12, this.Width - 396, this.Height - 24, anchorAll: true, anchorRight: true);
            AddSectionTitle(pRight, "Danh sách xuất (Giỏ hàng)", offsetY: 8);
            dgvGioXuat = BuildDgv(new[] {
                ("Tên sản phẩm", 45, DataGridViewContentAlignment.MiddleLeft),
                ("SL xuất", 15, DataGridViewContentAlignment.MiddleCenter),
                ("Tồn kho hiện tại", 20, DataGridViewContentAlignment.MiddleCenter),
                ("Ghi chú", 20, DataGridViewContentAlignment.MiddleLeft)
            }, 16, 48, pRight.Width - 32, pRight.Height - 70, anchorAll: true);
            pRight.Controls.Add(dgvGioXuat);

            this.Controls.Add(pnlXuatLeft);
            this.Controls.Add(pRight);
            this.ResumeLayout(false);
        }

        // --- GIAO DIỆN HELPERS ---
        private Panel BuildCard(int x, int y, int w, int h, bool anchorAll = false, bool anchorRight = false, bool anchorBottom = false)
        {
            var p = new Panel { BackColor = Color.White, Padding = new Padding(0) };
            if (x >= 0) p.Location = new Point(x, y >= 0 ? y : 0);
            if (w >= 0) p.Width = w; if (h >= 0) p.Height = h;
            if (anchorAll && !anchorRight && !anchorBottom) p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            else if (anchorAll && anchorRight && !anchorBottom) p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            else if (anchorAll && anchorBottom) p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            return p;
        }
        private void AddSectionTitle(Panel parent, string text, ref int y, int x = 16)
        {
            parent.Controls.Add(new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = Color.FromArgb(28, 38, 58) });
            parent.Controls.Add(new Panel { Location = new Point(x, y + 26), Width = parent.Width - x * 2, Height = 2, BackColor = Color.FromArgb(220, 225, 235) });
            y += 40;
        }
        private void AddSectionTitle(Panel parent, string text, int offsetY = 0) { int y = offsetY; AddSectionTitle(parent, text, ref y); }
        private void AddFieldLabel(Panel parent, string text, int y, int xOffset = 16) => parent.Controls.Add(new Label { Text = text, Location = new Point(xOffset, y), AutoSize = true, Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(90, 100, 120) });
        private void AddSeparator(Panel parent, int y) => parent.Controls.Add(new Panel { Location = new Point(16, y), Width = parent.Width - 32, Height = 1, BackColor = Color.FromArgb(220, 225, 235) });
        private TextBox MakeTextBox(int x, int y, int w) => new TextBox { Location = new Point(x, y), Width = w, Font = new Font("Segoe UI", 9.5f), BorderStyle = BorderStyle.FixedSingle };
        private ComboBox MakeCombo(int x, int y, int w) => new ComboBox { Location = new Point(x, y), Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5f), Height = 34 };
        private Button MakeStyledButton(string text, int x, int y, int w, Color color, int h = 32) => new Button { Text = text, Location = new Point(x, y), Width = w, Height = h, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold), Cursor = Cursors.Hand };
        private Button MakeIconButton(string icon, int x, int y, int size, Color color) { var btn = new Button { Text = icon, Location = new Point(x, y), Width = size, Height = size, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11f, FontStyle.Bold), Cursor = Cursors.Hand }; btn.FlatAppearance.BorderSize = 0; return btn; }

        private DataGridView BuildDgv((string header, int weight, DataGridViewContentAlignment align)[] cols, int x, int y, int w, int h, bool anchorAll = false)
        {
            var dgv = new DataGridView { Location = new Point(x, y), BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 36, RowTemplate = { Height = 30 }, GridColor = Color.FromArgb(220, 225, 235), CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal };
            if (w >= 0) dgv.Width = w; if (h >= 0) dgv.Height = h;
            if (anchorAll) dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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