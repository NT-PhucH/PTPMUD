namespace QLST.GUI__Giao_dien_.QuanLyGUI.QL_User
{
    partial class frmQuanLyNhanVien
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblCaLam = new System.Windows.Forms.Label();
            this.cboCaLam = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvNV = new System.Windows.Forms.DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblLocRole = new System.Windows.Forms.Label();
            this.cboLocRole = new System.Windows.Forms.ComboBox();
            this.chkLocNghiViec = new System.Windows.Forms.CheckBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1017, 50);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(412, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "👥 QUẢN LÝ NHÂN VIÊN (ADMIN)";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.lblMaNV);
            this.pnlLeft.Controls.Add(this.txtMaNV);
            this.pnlLeft.Controls.Add(this.btnAuto);
            this.pnlLeft.Controls.Add(this.lblTenNV);
            this.pnlLeft.Controls.Add(this.txtTenNV);
            this.pnlLeft.Controls.Add(this.lblUser);
            this.pnlLeft.Controls.Add(this.txtUser);
            this.pnlLeft.Controls.Add(this.lblPass);
            this.pnlLeft.Controls.Add(this.txtPass);
            this.pnlLeft.Controls.Add(this.chkShowPass);
            this.pnlLeft.Controls.Add(this.lblRole);
            this.pnlLeft.Controls.Add(this.cboRole);
            this.pnlLeft.Controls.Add(this.lblSDT);
            this.pnlLeft.Controls.Add(this.txtSDT);
            this.pnlLeft.Controls.Add(this.lblCaLam);
            this.pnlLeft.Controls.Add(this.cboCaLam);
            this.pnlLeft.Controls.Add(this.btnSave);
            this.pnlLeft.Controls.Add(this.btnClear);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 50);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(15);
            this.pnlLeft.Size = new System.Drawing.Size(350, 550);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(15, 20);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(72, 21);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã NV: *";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(15, 45);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(200, 29);
            this.txtMaNV.TabIndex = 1;
            // 
            // btnAuto
            // 
            this.btnAuto.BackColor = System.Drawing.Color.Teal;
            this.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuto.ForeColor = System.Drawing.Color.White;
            this.btnAuto.Location = new System.Drawing.Point(225, 44);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(100, 30);
            this.btnAuto.TabIndex = 2;
            this.btnAuto.Text = "Tạo Mã";
            this.btnAuto.UseVisualStyleBackColor = false;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Location = new System.Drawing.Point(15, 85);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(75, 21);
            this.lblTenNV.TabIndex = 3;
            this.lblTenNV.Text = "Họ & Tên: *";
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(15, 110);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(310, 29);
            this.txtTenNV.TabIndex = 4;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(15, 150);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(95, 21);
            this.lblUser.TabIndex = 5;
            this.lblUser.Text = "Username: *";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(15, 175);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(310, 29);
            this.txtUser.TabIndex = 6;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(15, 215);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(90, 21);
            this.lblPass.TabIndex = 7;
            this.lblPass.Text = "Password: *";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(15, 240);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(310, 29);
            this.txtPass.TabIndex = 8;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(250, 215);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(91, 25);
            this.chkShowPass.TabIndex = 9;
            this.chkShowPass.Text = "Hiện MK";
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(15, 280);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(114, 21);
            this.lblRole.TabIndex = 10;
            this.lblRole.Text = "Vai trò (Role): *";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.Items.AddRange(new object[] {
            "1 - Admin",
            "2 - Thu ngân",
            "3 - Kho",
            "4 - Bảo vệ"});
            this.cboRole.Location = new System.Drawing.Point(15, 305);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(310, 29);
            this.cboRole.TabIndex = 11;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(15, 345);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(41, 21);
            this.lblSDT.TabIndex = 12;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(15, 370);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(145, 29);
            this.txtSDT.TabIndex = 13;
            // 
            // lblCaLam
            // 
            this.lblCaLam.AutoSize = true;
            this.lblCaLam.Location = new System.Drawing.Point(170, 345);
            this.lblCaLam.Name = "lblCaLam";
            this.lblCaLam.Size = new System.Drawing.Size(61, 21);
            this.lblCaLam.TabIndex = 14;
            this.lblCaLam.Text = "Ca làm:";
            // 
            // cboCaLam
            // 
            this.cboCaLam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaLam.Items.AddRange(new object[] {
            "Sáng",
            "Chiều",
            "Tối",
            "Fulltime"});
            this.cboCaLam.Location = new System.Drawing.Point(170, 370);
            this.cboCaLam.Name = "cboCaLam";
            this.cboCaLam.Size = new System.Drawing.Size(155, 29);
            this.cboCaLam.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 485);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 35);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "💾 LƯU THÔNG TIN";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Gray;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(225, 485);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Làm Mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvNV);
            this.pnlRight.Controls.Add(this.pnlFilter);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(350, 50);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(667, 550);
            this.pnlRight.TabIndex = 0;
            // 
            // dgvNV
            // 
            this.dgvNV.AllowUserToAddRows = false;
            this.dgvNV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNV.BackgroundColor = System.Drawing.Color.White;
            this.dgvNV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNV.ColumnHeadersHeight = 29;
            this.dgvNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNV.Location = new System.Drawing.Point(10, 70);
            this.dgvNV.Name = "dgvNV";
            this.dgvNV.ReadOnly = true;
            this.dgvNV.RowHeadersVisible = false;
            this.dgvNV.RowHeadersWidth = 51;
            this.dgvNV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNV.Size = new System.Drawing.Size(647, 470);
            this.dgvNV.TabIndex = 0;
            this.dgvNV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNV_CellClick);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.lblLocRole);
            this.pnlFilter.Controls.Add(this.cboLocRole);
            this.pnlFilter.Controls.Add(this.chkLocNghiViec);
            this.pnlFilter.Controls.Add(this.btnLoc);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(647, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 20);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(103, 21);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Tìm kiếm:";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(90, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 29);
            this.txtSearch.TabIndex = 1;
            // 
            // lblLocRole
            // 
            this.lblLocRole.AutoSize = true;
            this.lblLocRole.Location = new System.Drawing.Point(310, 20);
            this.lblLocRole.Name = "lblLocRole";
            this.lblLocRole.Size = new System.Drawing.Size(70, 21);
            this.lblLocRole.TabIndex = 2;
            this.lblLocRole.Text = "Bộ phận:";
            // 
            // cboLocRole
            // 
            this.cboLocRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocRole.Items.AddRange(new object[] {
            "Tất cả",
            "Admin",
            "Thu ngân",
            "Kho",
            "Bảo vệ"});
            this.cboLocRole.Location = new System.Drawing.Point(380, 17);
            this.cboLocRole.Name = "cboLocRole";
            this.cboLocRole.Size = new System.Drawing.Size(130, 29);
            this.cboLocRole.TabIndex = 3;
            // 
            // chkLocNghiViec
            // 
            this.chkLocNghiViec.AutoSize = true;
            this.chkLocNghiViec.Location = new System.Drawing.Point(530, 20);
            this.chkLocNghiViec.Name = "chkLocNghiViec";
            this.chkLocNghiViec.Size = new System.Drawing.Size(159, 25);
            this.chkLocNghiViec.TabIndex = 4;
            this.chkLocNghiViec.Text = "Hiện cả TK bị khóa";
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.Teal;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(670, 15);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(80, 30);
            this.btnLoc.TabIndex = 5;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // frmQuanLyNhanVien
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1017, 600);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmQuanLyNhanVien";
            this.Text = "Quản lý nhân viên";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label lblTenNV;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblCaLam;
        private System.Windows.Forms.ComboBox cboCaLam;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblLocRole;
        private System.Windows.Forms.ComboBox cboLocRole;
        private System.Windows.Forms.CheckBox chkLocNghiViec;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.DataGridView dgvNV;
    }
}
