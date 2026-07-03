namespace QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don
{
    partial class frmThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThanhToan));
            this.tblTitleBar = new System.Windows.Forms.TableLayoutPanel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblTienThua = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtKhachDua = new QLST.UnderlineTextBox();
            this.btnInHD = new QLST.ModernButton();
            this.txtSDT = new QLST.UnderlineTextBox();
            this.paymentSelectorBar1 = new QLST.PaymentSelectorBar();
            this.tblTitleBar.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblTitleBar
            // 
            this.tblTitleBar.ColumnCount = 3;
            this.tblTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tblTitleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tblTitleBar.Controls.Add(this.btnMinimize, 1, 0);
            this.tblTitleBar.Controls.Add(this.btnClose, 2, 0);
            this.tblTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblTitleBar.Location = new System.Drawing.Point(0, 0);
            this.tblTitleBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblTitleBar.Name = "tblTitleBar";
            this.tblTitleBar.RowCount = 1;
            this.tblTitleBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTitleBar.Size = new System.Drawing.Size(1049, 40);
            this.tblTitleBar.TabIndex = 13;
            this.tblTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            this.tblTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseMove);
            this.tblTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseUp);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.Location = new System.Drawing.Point(963, 0);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(39, 40);
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1010, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Location = new System.Drawing.Point(29, 52);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 763);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.txtKhachDua);
            this.panel14.Controls.Add(this.lblTienThua);
            this.panel14.Controls.Add(this.label15);
            this.panel14.Controls.Add(this.label);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 264);
            this.panel14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(493, 90);
            this.panel14.TabIndex = 2;
            this.panel14.Paint += new System.Windows.Forms.PaintEventHandler(this.panel14_Paint);
            // 
            // lblTienThua
            // 
            this.lblTienThua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienThua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienThua.Location = new System.Drawing.Point(307, 30);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.Size = new System.Drawing.Size(149, 54);
            this.lblTienThua.TabIndex = 3;
            this.lblTienThua.Text = "label16";
            this.lblTienThua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(24, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 23);
            this.label15.TabIndex = 2;
            this.label15.Text = "Tiền thừa";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label.Location = new System.Drawing.Point(24, 16);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(151, 23);
            this.label.TabIndex = 0;
            this.label.Text = "Khách Thanh Toán";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel7.Controls.Add(this.txtSDT);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Location = new System.Drawing.Point(516, 54);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(493, 47);
            this.panel7.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "SĐT Khách hàng:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Location = new System.Drawing.Point(516, 119);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(493, 156);
            this.panel5.TabIndex = 11;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Location = new System.Drawing.Point(516, 283);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(493, 354);
            this.panel2.TabIndex = 12;
            // 
            // txtKhachDua
            // 
            this.txtKhachDua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKhachDua.BackColor = System.Drawing.Color.White;
            this.txtKhachDua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtKhachDua.LineColor = System.Drawing.Color.DarkGray;
            this.txtKhachDua.Location = new System.Drawing.Point(307, 11);
            this.txtKhachDua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKhachDua.Name = "txtKhachDua";
            this.txtKhachDua.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.txtKhachDua.SelectionStart = 0;
            this.txtKhachDua.Size = new System.Drawing.Size(149, 28);
            this.txtKhachDua.TabIndex = 4;
            this.txtKhachDua.Text = "0";
            this.txtKhachDua.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnInHD
            // 
            this.btnInHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(204)))));
            this.btnInHD.BorderRadius = 12;
            this.btnInHD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHD.FlatAppearance.BorderSize = 0;
            this.btnInHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnInHD.ForeColor = System.Drawing.Color.White;
            this.btnInHD.Location = new System.Drawing.Point(683, 759);
            this.btnInHD.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.btnInHD.Name = "btnInHD";
            this.btnInHD.Size = new System.Drawing.Size(156, 55);
            this.btnInHD.TabIndex = 2;
            this.btnInHD.Text = "Hoàn Tất";
            this.btnInHD.UseVisualStyleBackColor = false;
            this.btnInHD.Click += new System.EventHandler(this.btnInHD_Click);
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSDT.LineColor = System.Drawing.Color.Cyan;
            this.txtSDT.Location = new System.Drawing.Point(260, 22);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.txtSDT.SelectionStart = 0;
            this.txtSDT.Size = new System.Drawing.Size(196, 20);
            this.txtSDT.TabIndex = 1;
            this.txtSDT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSDT.Load += new System.EventHandler(this.txtSDT_Load);
            // 
            // paymentSelectorBar1
            // 
            this.paymentSelectorBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.paymentSelectorBar1.BackColor = System.Drawing.Color.Transparent;
            this.paymentSelectorBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.paymentSelectorBar1.Location = new System.Drawing.Point(516, 674);
            this.paymentSelectorBar1.Margin = new System.Windows.Forms.Padding(4);
            this.paymentSelectorBar1.Name = "paymentSelectorBar1";
            this.paymentSelectorBar1.Size = new System.Drawing.Size(493, 59);
            this.paymentSelectorBar1.TabIndex = 0;
            this.paymentSelectorBar1.Load += new System.EventHandler(this.paymentSelectorBar1_Load);
            // 
            // frmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 836);
            this.Controls.Add(this.tblTitleBar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnInHD);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.paymentSelectorBar1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmThanhToan";
            this.Text = "frmThanhToan";
            this.Load += new System.EventHandler(this.frmThanhToanold_Load);
            this.tblTitleBar.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PaymentSelectorBar paymentSelectorBar1;
        private System.Windows.Forms.Panel panel1;
        private ModernButton btnInHD;
        private System.Windows.Forms.Panel panel14;
        private UnderlineTextBox txtKhachDua;
        private System.Windows.Forms.Label lblTienThua;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel7;
        private UnderlineTextBox txtSDT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tblTitleBar;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
    }
}