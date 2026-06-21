namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class frmQuanLyDonHang
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cboPhuongThuc = new System.Windows.Forms.ComboBox();
            this.lblSearchKH = new System.Windows.Forms.Label();
            this.txtTimKH = new System.Windows.Forms.TextBox();
            this.lblThuNgan = new System.Windows.Forms.Label();
            this.txtThuNgan = new System.Windows.Forms.TextBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblChiTiet = new System.Windows.Forms.Label();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlBot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 50);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(428, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🛒 LỊCH SỬ GIAO DỊCH & ĐƠN HÀNG";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblMethod);
            this.pnlFilter.Controls.Add(this.cboPhuongThuc);
            this.pnlFilter.Controls.Add(this.lblSearchKH);
            this.pnlFilter.Controls.Add(this.txtTimKH);
            this.pnlFilter.Controls.Add(this.lblThuNgan);
            this.pnlFilter.Controls.Add(this.txtThuNgan);
            this.pnlFilter.Controls.Add(this.btnLoc);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(900, 100);
            this.pnlFilter.TabIndex = 2;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 20);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(68, 21);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(90, 17);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 29);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(240, 20);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(79, 21);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Đến ngày:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(320, 17);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 29);
            this.dtpTo.TabIndex = 3;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(480, 20);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(91, 21);
            this.lblMethod.TabIndex = 4;
            this.lblMethod.Text = "Thanh toán:";
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.Items.AddRange(new object[] {
            "Tất cả",
            "Tiền mặt",
            "Chuyển khoản",
            "Thẻ"});
            this.cboPhuongThuc.Location = new System.Drawing.Point(570, 17);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Size = new System.Drawing.Size(130, 29);
            this.cboPhuongThuc.TabIndex = 5;
            // 
            // lblSearchKH
            // 
            this.lblSearchKH.AutoSize = true;
            this.lblSearchKH.Location = new System.Drawing.Point(20, 60);
            this.lblSearchKH.Name = "lblSearchKH";
            this.lblSearchKH.Size = new System.Drawing.Size(165, 21);
            this.lblSearchKH.TabIndex = 6;
            this.lblSearchKH.Text = "Khách hàng (Tên/SĐT):";
            // 
            // txtTimKH
            // 
            this.txtTimKH.Location = new System.Drawing.Point(200, 57);
            this.txtTimKH.Name = "txtTimKH";
            this.txtTimKH.Size = new System.Drawing.Size(240, 29);
            this.txtTimKH.TabIndex = 7;
            // 
            // lblThuNgan
            // 
            this.lblThuNgan.AutoSize = true;
            this.lblThuNgan.Location = new System.Drawing.Point(480, 60);
            this.lblThuNgan.Name = "lblThuNgan";
            this.lblThuNgan.Size = new System.Drawing.Size(105, 21);
            this.lblThuNgan.TabIndex = 8;
            this.lblThuNgan.Text = "Tên Thu ngân:";
            // 
            // txtThuNgan
            // 
            this.txtThuNgan.Location = new System.Drawing.Point(570, 57);
            this.txtThuNgan.Name = "txtThuNgan";
            this.txtThuNgan.Size = new System.Drawing.Size(130, 29);
            this.txtThuNgan.TabIndex = 9;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(730, 17);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(150, 62);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "🔍 LỌC DỮ LIỆU";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // pnlBot
            // 
            this.pnlBot.Controls.Add(this.dgvChiTiet);
            this.pnlBot.Controls.Add(this.lblChiTiet);
            this.pnlBot.Controls.Add(this.lblTongDoanhThu);
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBot.Location = new System.Drawing.Point(0, 350);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBot.Size = new System.Drawing.Size(900, 250);
            this.pnlBot.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersHeight = 29;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvChiTiet.Location = new System.Drawing.Point(10, 90);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(880, 150);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // lblChiTiet
            // 
            this.lblChiTiet.AutoSize = true;
            this.lblChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChiTiet.Location = new System.Drawing.Point(10, 35);
            this.lblChiTiet.Name = "lblChiTiet";
            this.lblChiTiet.Size = new System.Drawing.Size(211, 21);
            this.lblChiTiet.TabIndex = 1;
            this.lblChiTiet.Text = "Chi tiết đơn hàng đã chọn:";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(10, 10);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(880, 30);
            this.lblTongDoanhThu.TabIndex = 2;
            this.lblTongDoanhThu.Text = "📊 Tổng số đơn: 0  |  💰 Tổng doanh thu: 0 đ";
            // 
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.dgvHoaDon);
            this.pnlMid.Controls.Add(this.lblDanhSach);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(0, 150);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMid.Size = new System.Drawing.Size(900, 200);
            this.pnlMid.TabIndex = 0;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.ColumnHeadersHeight = 29;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvHoaDon.Location = new System.Drawing.Point(10, 40);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(880, 150);
            this.dgvHoaDon.TabIndex = 0;
            this.dgvHoaDon.SelectionChanged += new System.EventHandler(this.dgvHoaDon_SelectionChanged);
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDanhSach.Location = new System.Drawing.Point(10, 5);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(160, 21);
            this.lblDanhSach.TabIndex = 1;
            this.lblDanhSach.Text = "Danh sách hóa đơn:";
            // 
            // frmQuanLyDonHang
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.pnlBot);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmQuanLyDonHang";
            this.Text = "Lịch sử đơn hàng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlBot.ResumeLayout(false);
            this.pnlBot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlMid.ResumeLayout(false);
            this.pnlMid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox cboPhuongThuc;
        private System.Windows.Forms.TextBox txtTimKH;
        private System.Windows.Forms.TextBox txtThuNgan;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label lblSearchKH;
        private System.Windows.Forms.Label lblThuNgan;

        private System.Windows.Forms.Panel pnlMid;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.DataGridView dgvHoaDon;

        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Label lblChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTiet;

    }
}
