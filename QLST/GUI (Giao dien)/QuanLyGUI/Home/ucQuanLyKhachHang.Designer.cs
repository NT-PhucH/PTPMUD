namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucQuanLyKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvKH;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Panel pSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvKH = new System.Windows.Forms.DataGridView();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pLeft = new System.Windows.Forms.Panel();
            this.pRight = new System.Windows.Forms.Panel();
            this.pSearch = new System.Windows.Forms.Panel();

            System.Windows.Forms.Label lblSearch = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblSDT = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTen = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKH)).BeginInit();
            this.pSearch.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pRight.SuspendLayout();
            this.SuspendLayout();

            // 
            // ucQuanLyKhachHang
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Size = new System.Drawing.Size(1150, 680);
            this.Name = "ucQuanLyKhachHang";

            // 
            // pSearch
            // 
            this.pSearch.BackColor = System.Drawing.Color.White;
            this.pSearch.Controls.Add(lblSearch);
            this.pSearch.Controls.Add(this.txtTimKiem);
            this.pSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSearch.Height = 50;
            this.pSearch.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);

            lblSearch.AutoSize = true;
            lblSearch.Location = new System.Drawing.Point(15, 15);
            lblSearch.Text = "🔍 Tìm kiếm (Tên/SĐT):";

            this.txtTimKiem.Location = new System.Drawing.Point(170, 12);
            this.txtTimKiem.Width = 300;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // 
            // pLeft (Danh sách khách hàng)
            // 
            this.pLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pLeft.Controls.Add(this.dgvKH);
            this.pLeft.Location = new System.Drawing.Point(10, 60);
            this.pLeft.Size = new System.Drawing.Size(750, 610);

            // 
            // dgvKH
            // 
            this.dgvKH.AllowUserToAddRows = false;
            this.dgvKH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKH.BackgroundColor = System.Drawing.Color.White;
            this.dgvKH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKH.ColumnHeadersHeight = 34;
            this.dgvKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKH.EnableHeadersVisualStyles = false;
            this.dgvKH.ReadOnly = true;
            this.dgvKH.RowHeadersVisible = false;
            this.dgvKH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvKH.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.dgvKH.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvKH.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvKH.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));

            var colID = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colID", HeaderText = "ID", FillWeight = 5 };
            var colSDT = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colSDT", HeaderText = "SĐT", FillWeight = 16 };
            var colTen = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colTen", HeaderText = "Tên KH", FillWeight = 25 };
            var colDiem = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colDiem", HeaderText = "⭐ Điểm", FillWeight = 12 };
            var colHD = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colHD", HeaderText = "Số HĐ", FillWeight = 10 };
            var colTong = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colTong", HeaderText = "Tổng chi tiêu", FillWeight = 22 };
            var colHang = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colHang", HeaderText = "Hạng", FillWeight = 10 };

            this.dgvKH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colID, colSDT, colTen, colDiem, colHD, colTong, colHang });
            this.dgvKH.SelectionChanged += new System.EventHandler(this.dgvKH_SelectionChanged);

            // 
            // pRight (Form nhập liệu)
            // 
            this.pRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight.BackColor = System.Drawing.Color.White;
            this.pRight.Location = new System.Drawing.Point(770, 60);
            this.pRight.Size = new System.Drawing.Size(370, 610);

            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(15, 15);
            lblTitle.Text = "THÔNG TIN KHÁCH HÀNG";

            lblSDT.AutoSize = true;
            lblSDT.Location = new System.Drawing.Point(15, 60);
            lblSDT.Text = "Số điện thoại: *";

            this.txtSDT.Location = new System.Drawing.Point(15, 85);
            this.txtSDT.Width = 340;

            lblTen.AutoSize = true;
            lblTen.Location = new System.Drawing.Point(15, 125);
            lblTen.Text = "Tên khách hàng: *";

            this.txtTenKH.Location = new System.Drawing.Point(15, 150);
            this.txtTenKH.Width = 340;

            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(15, 200);
            this.btnSua.Size = new System.Drawing.Size(165, 35);
            this.btnSua.Text = "✏ SỬA";
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(190, 200);
            this.btnLamMoi.Size = new System.Drawing.Size(165, 35);
            this.btnLamMoi.Text = "🔄 LÀM MỚI";
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            this.pRight.Controls.Add(lblTitle);
            this.pRight.Controls.Add(lblSDT);
            this.pRight.Controls.Add(this.txtSDT);
            this.pRight.Controls.Add(lblTen);
            this.pRight.Controls.Add(this.txtTenKH);
            this.pRight.Controls.Add(this.btnSua);
            this.pRight.Controls.Add(this.btnLamMoi);

            // Add controls to UserControl
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pSearch);

            ((System.ComponentModel.ISupportInitialize)(this.dgvKH)).EndInit();
            this.pSearch.ResumeLayout(false);
            this.pSearch.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}