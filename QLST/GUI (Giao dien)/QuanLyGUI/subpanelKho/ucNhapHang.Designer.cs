namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucNhapHang
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
            this.pnlNhapLeft = new System.Windows.Forms.Panel();
            this.btnLuuPhieuNhap = new System.Windows.Forms.Button();
            this.lblTongTienNhap = new System.Windows.Forms.Label();
            this.btnXoaDongNhap = new System.Windows.Forms.Button();
            this.btnThemVaoGioNhap = new System.Windows.Forms.Button();
            this.dtpHSD = new System.Windows.Forms.DateTimePicker();
            this.chkHSD = new System.Windows.Forms.CheckBox();
            this.dtpNSX = new System.Windows.Forms.DateTimePicker();
            this.chkNSX = new System.Windows.Forms.CheckBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.lblGia = new System.Windows.Forms.Label();
            this.txtSLNhap = new System.Windows.Forms.TextBox();
            this.lblSl = new System.Windows.Forms.Label();
            this.btnThemSPNhap = new System.Windows.Forms.Button();
            this.cboSPNhap = new System.Windows.Forms.ComboBox();
            this.lblSp = new System.Windows.Forms.Label();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.lblNcc = new System.Windows.Forms.Label();
            this.lblTitleLeft = new System.Windows.Forms.Label();
            this.pRight = new System.Windows.Forms.Panel();
            this.dgvGioNhap = new System.Windows.Forms.DataGridView();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNSX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitleRight = new System.Windows.Forms.Label();
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.pnlNhapLeft.SuspendLayout();
            this.pRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNhapLeft
            // 
            this.pnlNhapLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlNhapLeft.BackColor = System.Drawing.Color.White;
            this.pnlNhapLeft.Controls.Add(this.pnlLine1);
            this.pnlNhapLeft.Controls.Add(this.btnLuuPhieuNhap);
            this.pnlNhapLeft.Controls.Add(this.lblTongTienNhap);
            this.pnlNhapLeft.Controls.Add(this.btnXoaDongNhap);
            this.pnlNhapLeft.Controls.Add(this.btnThemVaoGioNhap);
            this.pnlNhapLeft.Controls.Add(this.dtpHSD);
            this.pnlNhapLeft.Controls.Add(this.chkHSD);
            this.pnlNhapLeft.Controls.Add(this.dtpNSX);
            this.pnlNhapLeft.Controls.Add(this.chkNSX);
            this.pnlNhapLeft.Controls.Add(this.txtGiaNhap);
            this.pnlNhapLeft.Controls.Add(this.lblGia);
            this.pnlNhapLeft.Controls.Add(this.txtSLNhap);
            this.pnlNhapLeft.Controls.Add(this.lblSl);
            this.pnlNhapLeft.Controls.Add(this.btnThemSPNhap);
            this.pnlNhapLeft.Controls.Add(this.cboSPNhap);
            this.pnlNhapLeft.Controls.Add(this.lblSp);
            this.pnlNhapLeft.Controls.Add(this.btnThemNCC);
            this.pnlNhapLeft.Controls.Add(this.cboNCC);
            this.pnlNhapLeft.Controls.Add(this.lblNcc);
            this.pnlNhapLeft.Controls.Add(this.lblTitleLeft);
            this.pnlNhapLeft.Location = new System.Drawing.Point(12, 12);
            this.pnlNhapLeft.Name = "pnlNhapLeft";
            this.pnlNhapLeft.Size = new System.Drawing.Size(360, 676);
            this.pnlNhapLeft.TabIndex = 0;
            // 
            // btnLuuPhieuNhap
            // 
            this.btnLuuPhieuNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.btnLuuPhieuNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieuNhap.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieuNhap.Location = new System.Drawing.Point(16, 430);
            this.btnLuuPhieuNhap.Name = "btnLuuPhieuNhap";
            this.btnLuuPhieuNhap.Size = new System.Drawing.Size(328, 42);
            this.btnLuuPhieuNhap.TabIndex = 18;
            this.btnLuuPhieuNhap.Text = "LƯU PHIẾU NHẬP";
            this.btnLuuPhieuNhap.UseVisualStyleBackColor = false;
            // 
            // lblTongTienNhap
            // 
            this.lblTongTienNhap.AutoSize = true;
            this.lblTongTienNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTienNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            this.lblTongTienNhap.Location = new System.Drawing.Point(16, 395);
            this.lblTongTienNhap.Name = "lblTongTienNhap";
            this.lblTongTienNhap.Size = new System.Drawing.Size(141, 25);
            this.lblTongTienNhap.TabIndex = 17;
            this.lblTongTienNhap.Text = "Tổng tiền:  0 đ";
            // 
            // btnXoaDongNhap
            // 
            this.btnXoaDongNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnXoaDongNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDongNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaDongNhap.ForeColor = System.Drawing.Color.White;
            this.btnXoaDongNhap.Location = new System.Drawing.Point(182, 335);
            this.btnXoaDongNhap.Name = "btnXoaDongNhap";
            this.btnXoaDongNhap.Size = new System.Drawing.Size(162, 36);
            this.btnXoaDongNhap.TabIndex = 16;
            this.btnXoaDongNhap.Text = "Xóa dòng chọn";
            this.btnXoaDongNhap.UseVisualStyleBackColor = false;
            // 
            // btnThemVaoGioNhap
            // 
            this.btnThemVaoGioNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(140)))), ((int)(((byte)(80)))));
            this.btnThemVaoGioNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoGioNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVaoGioNhap.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoGioNhap.Location = new System.Drawing.Point(16, 335);
            this.btnThemVaoGioNhap.Name = "btnThemVaoGioNhap";
            this.btnThemVaoGioNhap.Size = new System.Drawing.Size(150, 36);
            this.btnThemVaoGioNhap.TabIndex = 15;
            this.btnThemVaoGioNhap.Text = "Thêm vào phiếu";
            this.btnThemVaoGioNhap.UseVisualStyleBackColor = false;
            // 
            // dtpHSD
            // 
            this.dtpHSD.Enabled = false;
            this.dtpHSD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHSD.Location = new System.Drawing.Point(182, 287);
            this.dtpHSD.Name = "dtpHSD";
            this.dtpHSD.Size = new System.Drawing.Size(162, 29);
            this.dtpHSD.TabIndex = 14;
            // 
            // chkHSD
            // 
            this.chkHSD.AutoSize = true;
            this.chkHSD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.chkHSD.Location = new System.Drawing.Point(182, 260);
            this.chkHSD.Name = "chkHSD";
            this.chkHSD.Size = new System.Drawing.Size(165, 25);
            this.chkHSD.TabIndex = 13;
            this.chkHSD.Text = "Hạn sử dụng (HSD)";
            this.chkHSD.UseVisualStyleBackColor = true;
            // 
            // dtpNSX
            // 
            this.dtpNSX.Enabled = false;
            this.dtpNSX.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNSX.Location = new System.Drawing.Point(16, 287);
            this.dtpNSX.Name = "dtpNSX";
            this.dtpNSX.Size = new System.Drawing.Size(150, 29);
            this.dtpNSX.TabIndex = 12;
            // 
            // chkNSX
            // 
            this.chkNSX.AutoSize = true;
            this.chkNSX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.chkNSX.Location = new System.Drawing.Point(16, 260);
            this.chkNSX.Name = "chkNSX";
            this.chkNSX.Size = new System.Drawing.Size(174, 25);
            this.chkNSX.TabIndex = 11;
            this.chkNSX.Text = "Ngày sản xuất (NSX)";
            this.chkNSX.UseVisualStyleBackColor = true;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(182, 208);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(162, 29);
            this.txtGiaNhap.TabIndex = 10;
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblGia.Location = new System.Drawing.Point(182, 188);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(106, 21);
            this.lblGia.TabIndex = 9;
            this.lblGia.Text = "Giá nhập (đ) *";
            // 
            // txtSLNhap
            // 
            this.txtSLNhap.Location = new System.Drawing.Point(16, 208);
            this.txtSLNhap.Name = "txtSLNhap";
            this.txtSLNhap.Size = new System.Drawing.Size(150, 29);
            this.txtSLNhap.TabIndex = 8;
            this.txtSLNhap.Text = "1";
            // 
            // lblSl
            // 
            this.lblSl.AutoSize = true;
            this.lblSl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblSl.Location = new System.Drawing.Point(16, 188);
            this.lblSl.Name = "lblSl";
            this.lblSl.Size = new System.Drawing.Size(123, 21);
            this.lblSl.TabIndex = 7;
            this.lblSl.Text = "Số lượng nhập *";
            // 
            // btnThemSPNhap
            // 
            this.btnThemSPNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            this.btnThemSPNhap.FlatAppearance.BorderSize = 0;
            this.btnThemSPNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSPNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThemSPNhap.ForeColor = System.Drawing.Color.White;
            this.btnThemSPNhap.Location = new System.Drawing.Point(310, 142);
            this.btnThemSPNhap.Name = "btnThemSPNhap";
            this.btnThemSPNhap.Size = new System.Drawing.Size(34, 25);
            this.btnThemSPNhap.TabIndex = 6;
            this.btnThemSPNhap.Text = "+";
            this.btnThemSPNhap.UseVisualStyleBackColor = false;
            // 
            // cboSPNhap
            // 
            this.cboSPNhap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSPNhap.FormattingEnabled = true;
            this.cboSPNhap.Location = new System.Drawing.Point(16, 142);
            this.cboSPNhap.Name = "cboSPNhap";
            this.cboSPNhap.Size = new System.Drawing.Size(288, 29);
            this.cboSPNhap.TabIndex = 5;
            // 
            // lblSp
            // 
            this.lblSp.AutoSize = true;
            this.lblSp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblSp.Location = new System.Drawing.Point(16, 122);
            this.lblSp.Name = "lblSp";
            this.lblSp.Size = new System.Drawing.Size(91, 21);
            this.lblSp.TabIndex = 4;
            this.lblSp.Text = "Sản phẩm *";
            // 
            // btnThemNCC
            // 
            this.btnThemNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(140)))), ((int)(((byte)(80)))));
            this.btnThemNCC.FlatAppearance.BorderSize = 0;
            this.btnThemNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNCC.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThemNCC.ForeColor = System.Drawing.Color.White;
            this.btnThemNCC.Location = new System.Drawing.Point(310, 76);
            this.btnThemNCC.Name = "btnThemNCC";
            this.btnThemNCC.Size = new System.Drawing.Size(34, 25);
            this.btnThemNCC.TabIndex = 3;
            this.btnThemNCC.Text = "+";
            this.btnThemNCC.UseVisualStyleBackColor = false;
            // 
            // cboNCC
            // 
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(16, 76);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(288, 29);
            this.cboNCC.TabIndex = 2;
            // 
            // lblNcc
            // 
            this.lblNcc.AutoSize = true;
            this.lblNcc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblNcc.Location = new System.Drawing.Point(16, 56);
            this.lblNcc.Name = "lblNcc";
            this.lblNcc.Size = new System.Drawing.Size(116, 21);
            this.lblNcc.TabIndex = 1;
            this.lblNcc.Text = "Nhà cung cấp *";
            // 
            // lblTitleLeft
            // 
            this.lblTitleLeft.AutoSize = true;
            this.lblTitleLeft.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.lblTitleLeft.Location = new System.Drawing.Point(16, 16);
            this.lblTitleLeft.Name = "lblTitleLeft";
            this.lblTitleLeft.Size = new System.Drawing.Size(189, 25);
            this.lblTitleLeft.TabIndex = 0;
            this.lblTitleLeft.Text = "Tạo phiếu nhập kho";
            // 
            // pRight
            // 
            this.pRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pRight.BackColor = System.Drawing.Color.White;
            this.pRight.Controls.Add(this.dgvGioNhap);
            this.pRight.Controls.Add(this.lblTitleRight);
            this.pRight.Location = new System.Drawing.Point(384, 12);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(604, 676);
            this.pRight.TabIndex = 1;
            // 
            // dgvGioNhap
            // 
            this.dgvGioNhap.AllowUserToAddRows = false;
            this.dgvGioNhap.AllowUserToResizeColumns = false;
            this.dgvGioNhap.AllowUserToResizeRows = false;
            this.dgvGioNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGioNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvGioNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioNhap.ColumnHeadersHeight = 36;
            this.dgvGioNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTenSP,
            this.colSL,
            this.colGia,
            this.colNSX,
            this.colHSD});
            this.dgvGioNhap.Location = new System.Drawing.Point(20, 44);
            this.dgvGioNhap.Name = "dgvGioNhap";
            this.dgvGioNhap.ReadOnly = true;
            this.dgvGioNhap.RowHeadersVisible = false;
            this.dgvGioNhap.RowHeadersWidth = 51;
            this.dgvGioNhap.RowTemplate.Height = 30;
            this.dgvGioNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioNhap.Size = new System.Drawing.Size(572, 606);
            this.dgvGioNhap.TabIndex = 2;
            // 
            // colTenSP
            // 
            this.colTenSP.HeaderText = "Tên sản phẩm";
            this.colTenSP.MinimumWidth = 6;
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            // 
            // colSL
            // 
            this.colSL.HeaderText = "SL";
            this.colSL.MinimumWidth = 6;
            this.colSL.Name = "colSL";
            this.colSL.ReadOnly = true;
            // 
            // colGia
            // 
            this.colGia.HeaderText = "Giá nhập";
            this.colGia.MinimumWidth = 6;
            this.colGia.Name = "colGia";
            this.colGia.ReadOnly = true;
            // 
            // colNSX
            // 
            this.colNSX.HeaderText = "NSX";
            this.colNSX.MinimumWidth = 6;
            this.colNSX.Name = "colNSX";
            this.colNSX.ReadOnly = true;
            // 
            // colHSD
            // 
            this.colHSD.HeaderText = "HSD";
            this.colHSD.MinimumWidth = 6;
            this.colHSD.Name = "colHSD";
            this.colHSD.ReadOnly = true;
            // 
            // lblTitleRight
            // 
            this.lblTitleRight.AutoSize = true;
            this.lblTitleRight.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.lblTitleRight.Location = new System.Drawing.Point(7, 7);
            this.lblTitleRight.Name = "lblTitleRight";
            this.lblTitleRight.Size = new System.Drawing.Size(403, 25);
            this.lblTitleRight.TabIndex = 1;
            this.lblTitleRight.Text = "Danh sách sản phẩm trong phiếu (Giỏ hàng)";
            // 
            // pnlLine1
            // 
            this.pnlLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.pnlLine1.Location = new System.Drawing.Point(16, 44);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(328, 2);
            this.pnlLine1.TabIndex = 19;
            // 
            // ucNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pnlNhapLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "ucNhapHang";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlNhapLeft.ResumeLayout(false);
            this.pnlNhapLeft.PerformLayout();
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioNhap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNhapLeft;
        private System.Windows.Forms.Label lblTitleLeft;
        private System.Windows.Forms.Label lblNcc;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Button btnThemNCC;
        private System.Windows.Forms.Label lblSp;
        private System.Windows.Forms.ComboBox cboSPNhap;
        private System.Windows.Forms.Button btnThemSPNhap;
        private System.Windows.Forms.Label lblSl;
        private System.Windows.Forms.TextBox txtSLNhap;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.CheckBox chkNSX;
        private System.Windows.Forms.DateTimePicker dtpNSX;
        private System.Windows.Forms.CheckBox chkHSD;
        private System.Windows.Forms.DateTimePicker dtpHSD;
        private System.Windows.Forms.Button btnThemVaoGioNhap;
        private System.Windows.Forms.Button btnXoaDongNhap;
        private System.Windows.Forms.Label lblTongTienNhap;
        private System.Windows.Forms.Button btnLuuPhieuNhap;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Label lblTitleRight;
        private System.Windows.Forms.DataGridView dgvGioNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNSX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHSD;
        private System.Windows.Forms.Panel pnlLine1;
    }
}