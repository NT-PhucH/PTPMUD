namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucQLSP
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.cboLocLoai = new System.Windows.Forms.ComboBox();
            this.lblLoc = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpSanPham = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnTrangThai = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThemLoai = new System.Windows.Forms.Button();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTongTonKho = new System.Windows.Forms.Label();
            this.lblTitleRight = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.pnlThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlpMain.Controls.Add(this.pnlTop, 0, 0);
            this.tlpMain.Controls.Add(this.flpSanPham, 0, 1);
            this.tlpMain.Controls.Add(this.pnlRight, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1185, 721);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.tlpMain.SetColumnSpan(this.pnlTop, 2);
            this.pnlTop.Controls.Add(this.lblStatus);
            this.pnlTop.Controls.Add(this.cboTrangThai);
            this.pnlTop.Controls.Add(this.cboLocLoai);
            this.pnlTop.Controls.Add(this.lblLoc);
            this.pnlTop.Controls.Add(this.txtTimKiem);
            this.pnlTop.Controls.Add(this.lblSearch);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1185, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(311, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(82, 21);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "--Tất cả--",
            "Còn hàng",
            "Hết hàng",
            "Ngừng bán"});
            this.cboTrangThai.Location = new System.Drawing.Point(394, 14);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(180, 29);
            this.cboTrangThai.TabIndex = 5;
            // 
            // cboLocLoai
            // 
            this.cboLocLoai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLocLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocLoai.FormattingEnabled = true;
            this.cboLocLoai.Location = new System.Drawing.Point(985, 15);
            this.cboLocLoai.Name = "cboLocLoai";
            this.cboLocLoai.Size = new System.Drawing.Size(180, 29);
            this.cboLocLoai.TabIndex = 4;
            // 
            // lblLoc
            // 
            this.lblLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoc.AutoSize = true;
            this.lblLoc.ForeColor = System.Drawing.Color.White;
            this.lblLoc.Location = new System.Drawing.Point(920, 18);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(66, 21);
            this.lblLoc.TabIndex = 3;
            this.lblLoc.Text = "Lọc loại:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Location = new System.Drawing.Point(655, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(250, 29);
            this.txtTimKiem.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(580, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(77, 21);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🛒 QUẢN LÝ SẢN PHẨM";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // flpSanPham
            // 
            this.flpSanPham.AutoScroll = true;
            this.flpSanPham.BackColor = System.Drawing.Color.White;
            this.flpSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSanPham.Location = new System.Drawing.Point(3, 58);
            this.flpSanPham.Name = "flpSanPham";
            this.flpSanPham.Padding = new System.Windows.Forms.Padding(10);
            this.flpSanPham.Size = new System.Drawing.Size(829, 660);
            this.flpSanPham.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(248)))));
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.btnTrangThai);
            this.pnlRight.Controls.Add(this.btnSua);
            this.pnlRight.Controls.Add(this.btnThemLoai);
            this.pnlRight.Controls.Add(this.cboLoai);
            this.pnlRight.Controls.Add(this.label4);
            this.pnlRight.Controls.Add(this.txtGia);
            this.pnlRight.Controls.Add(this.label3);
            this.pnlRight.Controls.Add(this.txtTenSP);
            this.pnlRight.Controls.Add(this.label2);
            this.pnlRight.Controls.Add(this.txtMaVach);
            this.pnlRight.Controls.Add(this.label1);
            this.pnlRight.Controls.Add(this.btnChonAnh);
            this.pnlRight.Controls.Add(this.picAnh);
            this.pnlRight.Controls.Add(this.pnlThongTin);
            this.pnlRight.Controls.Add(this.lblTitleRight);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(835, 55);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(350, 666);
            this.pnlRight.TabIndex = 1;
            // 
            // btnTrangThai
            // 
            this.btnTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnTrangThai.Location = new System.Drawing.Point(203, 540);
            this.btnTrangThai.Name = "btnTrangThai";
            this.btnTrangThai.Size = new System.Drawing.Size(130, 35);
            this.btnTrangThai.TabIndex = 15;
            this.btnTrangThai.Text = "⏸ NGỪNG BÁN";
            this.btnTrangThai.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(107, 540);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(85, 35);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "✏ SỬA";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThemLoai
            // 
            this.btnThemLoai.BackColor = System.Drawing.Color.SeaGreen;
            this.btnThemLoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemLoai.ForeColor = System.Drawing.Color.White;
            this.btnThemLoai.Location = new System.Drawing.Point(268, 492);
            this.btnThemLoai.Name = "btnThemLoai";
            this.btnThemLoai.Size = new System.Drawing.Size(65, 25);
            this.btnThemLoai.TabIndex = 12;
            this.btnThemLoai.Text = "+ Loại";
            this.btnThemLoai.UseVisualStyleBackColor = false;
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(92, 492);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(170, 29);
            this.cboLoai.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 495);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Loại SP:";
            // 
            // txtGia
            // 
            this.txtGia.Location = new System.Drawing.Point(92, 452);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(241, 29);
            this.txtGia.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Giá bán: *";
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(92, 412);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(241, 29);
            this.txtTenSP.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 415);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên SP: *";
            // 
            // txtMaVach
            // 
            this.txtMaVach.Location = new System.Drawing.Point(92, 372);
            this.txtMaVach.Name = "txtMaVach";
            this.txtMaVach.Size = new System.Drawing.Size(241, 29);
            this.txtMaVach.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã vạch:";
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(107)))), ((int)(((byte)(192)))));
            this.btnChonAnh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonAnh.ForeColor = System.Drawing.Color.White;
            this.btnChonAnh.Location = new System.Drawing.Point(107, 321);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(140, 30);
            this.btnChonAnh.TabIndex = 3;
            this.btnChonAnh.Text = "📷 Chọn ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = false;
            // 
            // picAnh
            // 
            this.picAnh.BackColor = System.Drawing.Color.White;
            this.picAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnh.Location = new System.Drawing.Point(107, 164);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(140, 140);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabIndex = 2;
            this.picAnh.TabStop = false;
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.Controls.Add(this.lblTongGiaTri);
            this.pnlThongTin.Controls.Add(this.lblTongTonKho);
            this.pnlThongTin.Location = new System.Drawing.Point(18, 55);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(315, 78);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblTongGiaTri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTongGiaTri.Location = new System.Drawing.Point(161, 10);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(145, 50);
            this.lblTongGiaTri.TabIndex = 1;
            this.lblTongGiaTri.Text = "Tổng giá trị\r\n0 đ";
            this.lblTongGiaTri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongTonKho
            // 
            this.lblTongTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblTongTonKho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTongTonKho.Location = new System.Drawing.Point(10, 10);
            this.lblTongTonKho.Name = "lblTongTonKho";
            this.lblTongTonKho.Size = new System.Drawing.Size(145, 50);
            this.lblTongTonKho.TabIndex = 0;
            this.lblTongTonKho.Text = "Tổng tồn kho\r\n0";
            this.lblTongTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleRight
            // 
            this.lblTitleRight.AutoSize = true;
            this.lblTitleRight.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleRight.Location = new System.Drawing.Point(14, 20);
            this.lblTitleRight.Name = "lblTitleRight";
            this.lblTitleRight.Size = new System.Drawing.Size(225, 25);
            this.lblTitleRight.TabIndex = 0;
            this.lblTitleRight.Text = "THÔNG TIN SẢN PHẨM";
            // 
            // ucQLSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "ucQLSP";
            this.Size = new System.Drawing.Size(1185, 721);
            this.tlpMain.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.pnlThongTin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cboLocLoai;
        private System.Windows.Forms.FlowLayoutPanel flpSanPham;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblTitleRight;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Label lblTongTonKho;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Button btnThemLoai;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblStatus;
    }
}