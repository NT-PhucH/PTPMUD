// ucLichSuDon.Designer.cs
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
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
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInvoices = new System.Windows.Forms.Panel();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.lblInvoicesTitle = new System.Windows.Forms.Label();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.tlpData.SuspendLayout();
            this.pnlInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlFilter, 0, 0);
            this.tlpMain.Controls.Add(this.tlpData, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 700);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.tlpFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(3, 3);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(994, 94);
            this.pnlFilter.TabIndex = 2;
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 6;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpFilter.Controls.Add(this.lblFromDate, 0, 0);
            this.tlpFilter.Controls.Add(this.dtpFromDate, 0, 1);
            this.tlpFilter.Controls.Add(this.lblToDate, 1, 0);
            this.tlpFilter.Controls.Add(this.dtpToDate, 1, 1);
            this.tlpFilter.Controls.Add(this.lblPaymentMethod, 2, 0);
            this.tlpFilter.Controls.Add(this.cboPaymentMethod, 2, 1);
            this.tlpFilter.Controls.Add(this.lblCustomer, 3, 0);
            this.tlpFilter.Controls.Add(this.txtCustomer, 3, 1);
            this.tlpFilter.Controls.Add(this.lblCashier, 4, 0);
            this.tlpFilter.Controls.Add(this.txtCashier, 4, 1);
            this.tlpFilter.Controls.Add(this.roundedButton1, 5, 1);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(10, 10);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 2;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(972, 72);
            this.tlpFilter.TabIndex = 0;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblFromDate.Location = new System.Drawing.Point(3, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(100, 20);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(3, 28);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(148, 27);
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblToDate.Location = new System.Drawing.Point(164, 0);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(100, 20);
            this.lblToDate.TabIndex = 2;
            this.lblToDate.Text = "Đến ngày";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(164, 28);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(148, 27);
            this.dtpToDate.TabIndex = 3;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(325, 0);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(155, 20);
            this.lblPaymentMethod.TabIndex = 4;
            this.lblPaymentMethod.Text = "P.Thức thanh toán";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Items.AddRange(new object[] {
            "Tất cả",
            "Tiền mặt",
            "Thẻ",
            "Chuyển khoản"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(325, 28);
            this.cboPaymentMethod.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(148, 28);
            this.cboPaymentMethod.TabIndex = 5;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblCustomer.Location = new System.Drawing.Point(486, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(100, 20);
            this.lblCustomer.TabIndex = 6;
            this.lblCustomer.Text = "Khách hàng";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomer.Location = new System.Drawing.Point(486, 28);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(148, 27);
            this.txtCustomer.TabIndex = 7;
            // 
            // lblCashier
            // 
            this.lblCashier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblCashier.Location = new System.Drawing.Point(647, 0);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(100, 20);
            this.lblCashier.TabIndex = 8;
            this.lblCashier.Text = "Thu ngân";
            // 
            // txtCashier
            // 
            this.txtCashier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCashier.Location = new System.Drawing.Point(647, 28);
            this.txtCashier.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.txtCashier.Name = "txtCashier";
            this.txtCashier.Size = new System.Drawing.Size(148, 27);
            this.txtCashier.TabIndex = 9;
            // 
            // roundedButton1
            // 
            this.roundedButton1.AutoSize = true;
            this.roundedButton1.BackColor = System.Drawing.Color.DodgerBlue;
            this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton1.FlatAppearance.BorderSize = 0;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.roundedButton1.ForeColor = System.Drawing.Color.White;
            this.roundedButton1.Location = new System.Drawing.Point(808, 28);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(161, 41);
            this.roundedButton1.TabIndex = 10;
            this.roundedButton1.Text = "Lọc dữ liệu";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += new System.EventHandler(this.roundedButton1_Click);
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpData.Controls.Add(this.pnlInvoices, 0, 0);
            this.tlpData.Controls.Add(this.pnlDetail, 1, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(3, 103);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(994, 594);
            this.tlpData.TabIndex = 3;
            // 
            // pnlInvoices
            // 
            this.pnlInvoices.BackColor = System.Drawing.Color.White;
            this.pnlInvoices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInvoices.Controls.Add(this.dgvInvoices);
            this.pnlInvoices.Controls.Add(this.lblInvoicesTitle);
            this.pnlInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInvoices.Location = new System.Drawing.Point(3, 3);
            this.pnlInvoices.Name = "pnlInvoices";
            this.pnlInvoices.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInvoices.Size = new System.Drawing.Size(640, 588);
            this.pnlInvoices.TabIndex = 0;
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
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.EnableHeadersVisualStyles = false;
            this.dgvInvoices.Location = new System.Drawing.Point(10, 45);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.RowHeadersWidth = 51;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(618, 531);
            this.dgvInvoices.TabIndex = 1;
            this.dgvInvoices.SelectionChanged += new System.EventHandler(this.dgvInvoices_SelectionChanged);
            // 
            // lblInvoicesTitle
            // 
            this.lblInvoicesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInvoicesTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInvoicesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblInvoicesTitle.Location = new System.Drawing.Point(10, 10);
            this.lblInvoicesTitle.Name = "lblInvoicesTitle";
            this.lblInvoicesTitle.Size = new System.Drawing.Size(618, 35);
            this.lblInvoicesTitle.TabIndex = 2;
            this.lblInvoicesTitle.Text = "Danh sách hóa đơn";
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.White;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.dgvDetails);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(649, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(10);
            this.pnlDetail.Size = new System.Drawing.Size(342, 588);
            this.pnlDetail.TabIndex = 1;
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
            this.dgvDetails.Location = new System.Drawing.Point(10, 45);
            this.dgvDetails.MultiSelect = false;
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.Size = new System.Drawing.Size(320, 531);
            this.dgvDetails.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(320, 35);
            this.lblDetailTitle.TabIndex = 1;
            this.lblDetailTitle.Text = "Chi tiết đơn hàng";
            // 
            // ucLichSuDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ucLichSuDon";
            this.Size = new System.Drawing.Size(1000, 700);
            this.tlpMain.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.tlpFilter.PerformLayout();
            this.tlpData.ResumeLayout(false);
            this.pnlInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tlpMain;

        private System.Windows.Forms.Panel pnlFilter;
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
        private RoundedButton roundedButton1;

        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.Panel pnlInvoices;
        private System.Windows.Forms.Label lblInvoicesTitle;
        private System.Windows.Forms.DataGridView dgvInvoices;

        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.DataGridView dgvDetails;
    }
}