namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucThemNCC
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblMaNCC = new System.Windows.Forms.Label();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.txtTenNCC = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Controls.Add(this.btnThem);
            this.pnlContainer.Controls.Add(this.txtDiaChi);
            this.pnlContainer.Controls.Add(this.lblDiaChi);
            this.pnlContainer.Controls.Add(this.txtSDT);
            this.pnlContainer.Controls.Add(this.lblSDT);
            this.pnlContainer.Controls.Add(this.txtTenNCC);
            this.pnlContainer.Controls.Add(this.lblTenNCC);
            this.pnlContainer.Controls.Add(this.lblHint);
            this.pnlContainer.Controls.Add(this.txtMaNCC);
            this.pnlContainer.Controls.Add(this.lblMaNCC);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContainer.Size = new System.Drawing.Size(350, 480);
            this.pnlContainer.TabIndex = 0;

            // 
            // lblMaNCC
            // 
            this.lblMaNCC.AutoSize = true;
            this.lblMaNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblMaNCC.Location = new System.Drawing.Point(20, 20);
            this.lblMaNCC.Name = "lblMaNCC";
            this.lblMaNCC.Size = new System.Drawing.Size(58, 17);
            this.lblMaNCC.TabIndex = 0;
            this.lblMaNCC.Text = "Mã NCC";

            // 
            // txtMaNCC
            // 
            this.txtMaNCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.txtMaNCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaNCC.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtMaNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.txtMaNCC.Location = new System.Drawing.Point(23, 44);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.ReadOnly = true;
            this.txtMaNCC.Size = new System.Drawing.Size(304, 26);
            this.txtMaNCC.TabIndex = 1;
            this.txtMaNCC.Text = "(Tự động sinh mã)";

            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.lblHint.Location = new System.Drawing.Point(20, 75);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(188, 15);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "* Hệ thống tự động phát sinh mã";

            // 
            // lblTenNCC
            // 
            this.lblTenNCC.AutoSize = true;
            this.lblTenNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTenNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTenNCC.Location = new System.Drawing.Point(20, 105);
            this.lblTenNCC.Name = "lblTenNCC";
            this.lblTenNCC.Size = new System.Drawing.Size(71, 17);
            this.lblTenNCC.TabIndex = 3;
            this.lblTenNCC.Text = "Tên NCC *";

            // 
            // txtTenNCC
            // 
            this.txtTenNCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenNCC.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTenNCC.Location = new System.Drawing.Point(23, 129);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(304, 26);
            this.txtTenNCC.TabIndex = 4;

            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblSDT.Location = new System.Drawing.Point(20, 173);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(90, 17);
            this.lblSDT.TabIndex = 5;
            this.lblSDT.Text = "Số điện thoại";

            // 
            // txtSDT
            // 
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSDT.Location = new System.Drawing.Point(23, 197);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(304, 26);
            this.txtSDT.TabIndex = 6;

            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDiaChi.Location = new System.Drawing.Point(20, 241);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(51, 17);
            this.lblDiaChi.TabIndex = 7;
            this.lblDiaChi.Text = "Địa chỉ";

            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtDiaChi.Location = new System.Drawing.Point(23, 265);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(304, 80);
            this.txtDiaChi.TabIndex = 8;

            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(165)))), ((int)(((byte)(80)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(23, 370);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(304, 45);
            this.btnThem.TabIndex = 9;
            this.btnThem.Text = "+";
            this.btnThem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnThem.UseVisualStyleBackColor = false;

            // 
            // ucThemNCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "ucThemNCC";
            this.Size = new System.Drawing.Size(350, 440);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblMaNCC;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.TextBox txtTenNCC;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Button btnThem;
    }
}