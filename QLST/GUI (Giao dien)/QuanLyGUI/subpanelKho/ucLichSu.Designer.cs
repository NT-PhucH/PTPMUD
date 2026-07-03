using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucLichSu
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

        private Panel pFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private ComboBox cboLoaiPhieu;
        private Button btnLocLichSu;
        private Button btnRefreshLichSu;
        private Panel pListCard;
        private DataGridView dgvLichSuAll;
        private Panel pDetailCard;
        private Label lblChiTietTitle;
        private DataGridView dgvChiTietLichSu;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(242, 245, 250);
            this.Font = new Font("Segoe UI", 9.5f);
            this.Size = new Size(1000, 700);

            // 1. Bộ lọc (pFilter)
            pFilter = new Panel { Location = new Point(12, 12), Height = 50, BackColor = Color.White, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            StyleCard(pFilter);

            pFilter.Controls.Add(MakeSmallLabel("Từ ngày:", 12, 16));
            dtpFrom = new DateTimePicker { Location = new Point(72, 12), Width = 120, Format = DateTimePickerFormat.Short, Value = System.DateTime.Today.AddMonths(-1) };
            pFilter.Controls.Add(dtpFrom);

            pFilter.Controls.Add(MakeSmallLabel("Đến ngày:", 208, 16));
            dtpTo = new DateTimePicker { Location = new Point(278, 12), Width = 120, Format = DateTimePickerFormat.Short };
            pFilter.Controls.Add(dtpTo);

            pFilter.Controls.Add(MakeSmallLabel("Loại:", 414, 16));
            cboLoaiPhieu = new ComboBox { Location = new Point(446, 12), Width = 110, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiPhieu.Items.AddRange(new object[] { "Tất cả", "Phiếu nhập", "Phiếu xuất" });
            cboLoaiPhieu.SelectedIndex = 0;
            pFilter.Controls.Add(cboLoaiPhieu);

            btnLocLichSu = MakeStyledButton("🔍  Tìm kiếm", 572, 10, 110, Color.FromArgb(40, 90, 200), 30);
            pFilter.Controls.Add(btnLocLichSu);

            btnRefreshLichSu = MakeStyledButton("↺", 690, 10, 34, Color.FromArgb(100, 100, 120), 30);
            btnRefreshLichSu.Font = new Font("Segoe UI", 12f);
            pFilter.Controls.Add(btnRefreshLichSu);

            // 2. Danh sách phiếu (pListCard)
            pListCard = BuildCard(12, 74, this.Width - 24, this.Height - 24 - 50 - 12 - 220 - 8);
            AddSectionTitle(pListCard, "Danh sách phiếu", offsetY: 8);
            dgvLichSuAll = BuildDgv(new[] {
                ("Loại", 8, DataGridViewContentAlignment.MiddleCenter),
                ("Mã phiếu", 14, DataGridViewContentAlignment.MiddleLeft),
                ("Đối tác / Lý do", 28, DataGridViewContentAlignment.MiddleLeft),
                ("Nhân viên", 18, DataGridViewContentAlignment.MiddleLeft),
                ("Ngày", 14, DataGridViewContentAlignment.MiddleCenter),
                ("Giá trị", 18, DataGridViewContentAlignment.MiddleRight)
            }, 16, 46, pListCard.Width - 32, pListCard.Height - 60);
            pListCard.Controls.Add(dgvLichSuAll);

            // 3. Chi tiết phiếu (pDetailCard)
            pDetailCard = BuildCard(12, this.Height - 24 - 220, this.Width - 24, 220);
            lblChiTietTitle = new Label { Text = "Chi tiết phiếu — chọn một phiếu ở trên để xem", Location = new Point(16, 10), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(40, 90, 200) };
            pDetailCard.Controls.Add(lblChiTietTitle);

            dgvChiTietLichSu = BuildDgv(new[] {
                ("Tên sản phẩm", 38, DataGridViewContentAlignment.MiddleLeft),
                ("Mã vạch", 16, DataGridViewContentAlignment.MiddleLeft),
                ("Số lượng", 13, DataGridViewContentAlignment.MiddleCenter),
                ("Đơn giá", 17, DataGridViewContentAlignment.MiddleRight),
                ("NSX", 8, DataGridViewContentAlignment.MiddleCenter),
                ("HSD", 8, DataGridViewContentAlignment.MiddleCenter)
            }, 16, 38, pDetailCard.Width - 32, 152);
            pDetailCard.Controls.Add(dgvChiTietLichSu);

            this.Controls.Add(pFilter);
            this.Controls.Add(pListCard);
            this.Controls.Add(pDetailCard);

            // Đăng ký sự kiện tự động căn chỉnh kích thước
            this.Resize += UcLichSu_Resize;

            this.ResumeLayout(false);
        }

        private void UcLichSu_Resize(object sender, System.EventArgs e)
        {
            pFilter.Width = this.Width - 24;
            pListCard.Width = this.Width - 24;
            pListCard.Height = this.Height - 24 - 50 - 12 - 220 - 8;
            if (dgvLichSuAll != null)
            {
                dgvLichSuAll.Width = pListCard.Width - 32;
                dgvLichSuAll.Height = pListCard.Height - 60;
            }
            pDetailCard.Top = this.Height - 24 - 220;
            pDetailCard.Width = this.Width - 24;
            if (dgvChiTietLichSu != null)
            {
                dgvChiTietLichSu.Width = pDetailCard.Width - 32;
            }
        }

        // --- GIAO DIỆN HELPERS ---
        private Panel BuildCard(int x, int y, int w, int h)
        {
            var p = new Panel { BackColor = Color.White, Location = new Point(x, y), Width = w, Height = h };
            StyleCard(p);
            return p;
        }
        private void StyleCard(Panel p) { p.Padding = new Padding(0); }
        private void AddSectionTitle(Panel parent, string text, int offsetY = 0)
        {
            parent.Controls.Add(new Label { Text = text, Location = new Point(16, offsetY), AutoSize = true, Font = new Font("Segoe UI", 11f, FontStyle.Bold), ForeColor = Color.FromArgb(28, 38, 58) });
            parent.Controls.Add(new Panel { Location = new Point(16, offsetY + 26), Width = parent.Width - 32, Height = 2, BackColor = Color.FromArgb(220, 225, 235) });
        }
        private Label MakeSmallLabel(string text, int x, int y) => new Label { Text = text, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(80, 80, 100) };
        private Button MakeStyledButton(string text, int x, int y, int w, Color color, int h = 32) => new Button { Text = text, Location = new Point(x, y), Width = w, Height = h, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f, FontStyle.Bold), Cursor = Cursors.Hand };

        private DataGridView BuildDgv((string header, int weight, DataGridViewContentAlignment align)[] cols, int x, int y, int w, int h)
        {
            var dgv = new DataGridView { Location = new Point(x, y), Width = w, Height = h, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, RowHeadersVisible = false, AllowUserToAddRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersHeight = 36, RowTemplate = { Height = 30 }, GridColor = Color.FromArgb(220, 225, 235), CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal };
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