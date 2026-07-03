using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    partial class ucLichSuDon
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblCashier = new System.Windows.Forms.Label();
            this.txtCashier = new System.Windows.Forms.TextBox();
            this.roundedButton1 = new QLST.RoundedButton();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 76);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(271, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Lịch sử hóa đơn";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(150)))));
            this.lblSubtitle.Location = new System.Drawing.Point(22, 46);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(375, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Quản lý và theo dõi tất cả các đơn hàng đã bán";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.tlpFilter);
            this.pnlFilter.Controls.Add(this.lblFilterTitle);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 76);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(20);
            this.pnlFilter.Size = new System.Drawing.Size(1000, 198);
            this.pnlFilter.TabIndex = 2;
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 3;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFilter.Controls.Add(this.lblFromDate, 0, 0);
            this.tlpFilter.Controls.Add(this.dtpFromDate, 0, 1);
            this.tlpFilter.Controls.Add(this.lblToDate, 1, 0);
            this.tlpFilter.Controls.Add(this.dtpToDate, 1, 1);
            this.tlpFilter.Controls.Add(this.lblPaymentMethod, 2, 0);
            this.tlpFilter.Controls.Add(this.cboPaymentMethod, 2, 1);
            this.tlpFilter.Controls.Add(this.lblCustomer, 0, 2);
            this.tlpFilter.Controls.Add(this.txtCustomer, 0, 3);
            this.tlpFilter.Controls.Add(this.lblCashier, 1, 2);
            this.tlpFilter.Controls.Add(this.txtCashier, 1, 3);
            this.tlpFilter.Controls.Add(this.roundedButton1, 2, 3);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(20, 55);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tlpFilter.RowCount = 4;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpFilter.Size = new System.Drawing.Size(958, 121);
            this.tlpFilter.TabIndex = 0;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblFromDate.Location = new System.Drawing.Point(3, 10);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(100, 20);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(0, 34);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(304, 27);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.Value = new System.DateTime(2026, 6, 21, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblToDate.Location = new System.Drawing.Point(322, 10);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(100, 20);
            this.lblToDate.TabIndex = 2;
            this.lblToDate.Text = "Đến ngày";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(319, 34);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(304, 27);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.Value = new System.DateTime(2026, 6, 28, 0, 0, 0, 0);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(641, 10);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(100, 20);
            this.lblPaymentMethod.TabIndex = 4;
            this.lblPaymentMethod.Text = "Phương thức thanh toán";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Tất cả",
            "Tiền mặt",
            "Thẻ",
            "Chuyển khoản"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(638, 35);
            this.cboPaymentMethod.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(305, 28);
            this.cboPaymentMethod.TabIndex = 5;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblCustomer.Location = new System.Drawing.Point(3, 65);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(100, 20);
            this.lblCustomer.TabIndex = 6;
            this.lblCustomer.Text = "Tìm khách hàng";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomer.Location = new System.Drawing.Point(0, 89);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(304, 27);
            this.txtCustomer.TabIndex = 7;
            // 
            // lblCashier
            // 
            this.lblCashier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblCashier.Location = new System.Drawing.Point(322, 65);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(100, 20);
            this.lblCashier.TabIndex = 8;
            this.lblCashier.Text = "Thu ngân";
            // 
            // txtCashier
            // 
            this.txtCashier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCashier.Location = new System.Drawing.Point(319, 89);
            this.txtCashier.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.txtCashier.Name = "txtCashier";
            this.txtCashier.Size = new System.Drawing.Size(304, 27);
            this.txtCashier.TabIndex = 9;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.DodgerBlue;
            this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedButton1.FlatAppearance.BorderSize = 0;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.roundedButton1.ForeColor = System.Drawing.Color.White;
            this.roundedButton1.Location = new System.Drawing.Point(641, 88);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(314, 30);
            this.roundedButton1.TabIndex = 10;
            this.roundedButton1.Text = "Lọc";
            this.roundedButton1.UseVisualStyleBackColor = false;
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFilterTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblFilterTitle.Location = new System.Drawing.Point(20, 20);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.Size = new System.Drawing.Size(958, 35);
            this.lblFilterTitle.TabIndex = 1;
            this.lblFilterTitle.Text = "Bộ lọc";
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvInvoices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInvoices.BackgroundColor = System.Drawing.Color.White;
            this.dgvInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvInvoices.EnableHeadersVisualStyles = false;
            this.dgvInvoices.Location = new System.Drawing.Point(0, 274);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.RowHeadersWidth = 51;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(1000, 207);
            this.dgvInvoices.TabIndex = 1;
            this.dgvInvoices.SelectionChanged += new System.EventHandler(this.dgvInvoices_SelectionChanged);
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.White;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.dgvDetails);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 481);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDetail.Size = new System.Drawing.Size(1000, 219);
            this.pnlDetail.TabIndex = 0;
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.Location = new System.Drawing.Point(20, 45);
            this.dgvDetails.MultiSelect = false;
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.Size = new System.Drawing.Size(958, 152);
            this.dgvDetails.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(20, 20);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(958, 25);
            this.lblDetailTitle.TabIndex = 1;
            this.lblDetailTitle.Text = "Chi tiết đơn hàng";
            // 
            // ucLichSuDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.dgvInvoices);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucLichSuDon";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.tlpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFilterTitle;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblCashier;
        private System.Windows.Forms.TextBox txtCashier;

        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.DataGridView dgvDetails;
        private RoundedButton roundedButton1;
    }
}