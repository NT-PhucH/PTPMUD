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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pFilter = new System.Windows.Forms.Panel();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoaiPhieu = new System.Windows.Forms.ComboBox();
            this.btnLocLichSu = new System.Windows.Forms.Button();
            this.btnRefreshLichSu = new System.Windows.Forms.Button();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.pListCard = new System.Windows.Forms.Panel();
            this.dgvLichSuAll = new System.Windows.Forms.DataGridView();
            this.line1 = new System.Windows.Forms.Panel();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.pDetailCard = new System.Windows.Forms.Panel();
            this.dgvChiTietLichSu = new System.Windows.Forms.DataGridView();
            this.lblChiTietTitle = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.pFilter.SuspendLayout();
            this.tlpContent.SuspendLayout();
            this.pListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuAll)).BeginInit();
            this.pDetailCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pFilter, 0, 0);
            this.tlpMain.Controls.Add(this.tlpContent, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1000, 700);
            this.tlpMain.TabIndex = 0;
            // 
            // pFilter
            // 
            this.pFilter.BackColor = System.Drawing.Color.White;
            this.pFilter.Controls.Add(this.lblTuNgay);
            this.pFilter.Controls.Add(this.dtpFrom);
            this.pFilter.Controls.Add(this.lblDenNgay);
            this.pFilter.Controls.Add(this.dtpTo);
            this.pFilter.Controls.Add(this.lblLoai);
            this.pFilter.Controls.Add(this.cboLoaiPhieu);
            this.pFilter.Controls.Add(this.btnLocLichSu);
            this.pFilter.Controls.Add(this.btnRefreshLichSu);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pFilter.Location = new System.Drawing.Point(3, 3);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(994, 54);
            this.pFilter.TabIndex = 0;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(12, 18);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(68, 21);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(75, 14);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 29);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(210, 18);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(79, 21);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(280, 14);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 29);
            this.dtpTo.TabIndex = 3;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(415, 18);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(42, 21);
            this.lblLoai.TabIndex = 4;
            this.lblLoai.Text = "Loại:";
            // 
            // cboLoaiPhieu
            // 
            this.cboLoaiPhieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhieu.FormattingEnabled = true;
            this.cboLoaiPhieu.Items.AddRange(new object[] {
            "Tất cả",
            "Phiếu nhập",
            "Phiếu xuất"});
            this.cboLoaiPhieu.Location = new System.Drawing.Point(455, 14);
            this.cboLoaiPhieu.Name = "cboLoaiPhieu";
            this.cboLoaiPhieu.Size = new System.Drawing.Size(110, 29);
            this.cboLoaiPhieu.TabIndex = 5;
            // 
            // btnLocLichSu
            // 
            this.btnLocLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            this.btnLocLichSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocLichSu.FlatAppearance.BorderSize = 0;
            this.btnLocLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocLichSu.ForeColor = System.Drawing.Color.White;
            this.btnLocLichSu.Location = new System.Drawing.Point(585, 11);
            this.btnLocLichSu.Name = "btnLocLichSu";
            this.btnLocLichSu.Size = new System.Drawing.Size(110, 30);
            this.btnLocLichSu.TabIndex = 6;
            this.btnLocLichSu.Text = "🔍 Tìm kiếm";
            this.btnLocLichSu.UseVisualStyleBackColor = false;
            // 
            // btnRefreshLichSu
            // 
            this.btnRefreshLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.btnRefreshLichSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshLichSu.FlatAppearance.BorderSize = 0;
            this.btnRefreshLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshLichSu.ForeColor = System.Drawing.Color.White;
            this.btnRefreshLichSu.Location = new System.Drawing.Point(705, 11);
            this.btnRefreshLichSu.Name = "btnRefreshLichSu";
            this.btnRefreshLichSu.Size = new System.Drawing.Size(34, 30);
            this.btnRefreshLichSu.TabIndex = 7;
            this.btnRefreshLichSu.Text = "↺";
            this.btnRefreshLichSu.UseVisualStyleBackColor = false;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpContent.Controls.Add(this.pListCard, 0, 0);
            this.tlpContent.Controls.Add(this.pDetailCard, 1, 0);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(3, 63);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 1;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(994, 634);
            this.tlpContent.TabIndex = 1;
            // 
            // pListCard
            // 
            this.pListCard.BackColor = System.Drawing.Color.White;
            this.pListCard.Controls.Add(this.dgvLichSuAll);
            this.pListCard.Controls.Add(this.line1);
            this.pListCard.Controls.Add(this.lblDanhSach);
            this.pListCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pListCard.Location = new System.Drawing.Point(3, 3);
            this.pListCard.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.pListCard.Name = "pListCard";
            this.pListCard.Padding = new System.Windows.Forms.Padding(10);
            this.pListCard.Size = new System.Drawing.Size(638, 628);
            this.pListCard.TabIndex = 0;
            // 
            // dgvLichSuAll
            // 
            this.dgvLichSuAll.AllowUserToAddRows = false;
            this.dgvLichSuAll.AllowUserToResizeColumns = false;
            this.dgvLichSuAll.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.dgvLichSuAll.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLichSuAll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuAll.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSuAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLichSuAll.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLichSuAll.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvLichSuAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLichSuAll.ColumnHeadersHeight = 36;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLichSuAll.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLichSuAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuAll.EnableHeadersVisualStyles = false;
            this.dgvLichSuAll.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.dgvLichSuAll.Location = new System.Drawing.Point(10, 37);
            this.dgvLichSuAll.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.dgvLichSuAll.Name = "dgvLichSuAll";
            this.dgvLichSuAll.ReadOnly = true;
            this.dgvLichSuAll.RowHeadersVisible = false;
            this.dgvLichSuAll.RowHeadersWidth = 51;
            this.dgvLichSuAll.RowTemplate.Height = 30;
            this.dgvLichSuAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuAll.Size = new System.Drawing.Size(618, 581);
            this.dgvLichSuAll.TabIndex = 0;
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.line1.Dock = System.Windows.Forms.DockStyle.Top;
            this.line1.Location = new System.Drawing.Point(10, 35);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(618, 2);
            this.line1.TabIndex = 1;
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.lblDanhSach.Location = new System.Drawing.Point(10, 10);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(618, 25);
            this.lblDanhSach.TabIndex = 2;
            this.lblDanhSach.Text = "Danh sách phiếu";
            // 
            // pDetailCard
            // 
            this.pDetailCard.BackColor = System.Drawing.Color.White;
            this.pDetailCard.Controls.Add(this.dgvChiTietLichSu);
            this.pDetailCard.Controls.Add(this.lblChiTietTitle);
            this.pDetailCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDetailCard.Location = new System.Drawing.Point(651, 3);
            this.pDetailCard.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.pDetailCard.Name = "pDetailCard";
            this.pDetailCard.Padding = new System.Windows.Forms.Padding(10);
            this.pDetailCard.Size = new System.Drawing.Size(340, 628);
            this.pDetailCard.TabIndex = 1;
            // 
            // dgvChiTietLichSu
            // 
            this.dgvChiTietLichSu.AllowUserToAddRows = false;
            this.dgvChiTietLichSu.AllowUserToResizeColumns = false;
            this.dgvChiTietLichSu.AllowUserToResizeRows = false;
            this.dgvChiTietLichSu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTietLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietLichSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTietLichSu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChiTietLichSu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvChiTietLichSu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTietLichSu.ColumnHeadersHeight = 36;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietLichSu.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvChiTietLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietLichSu.EnableHeadersVisualStyles = false;
            this.dgvChiTietLichSu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.dgvChiTietLichSu.Location = new System.Drawing.Point(10, 37);
            this.dgvChiTietLichSu.Name = "dgvChiTietLichSu";
            this.dgvChiTietLichSu.ReadOnly = true;
            this.dgvChiTietLichSu.RowHeadersVisible = false;
            this.dgvChiTietLichSu.RowHeadersWidth = 51;
            this.dgvChiTietLichSu.RowTemplate.Height = 30;
            this.dgvChiTietLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietLichSu.Size = new System.Drawing.Size(320, 581);
            this.dgvChiTietLichSu.TabIndex = 0;
            // 
            // lblChiTietTitle
            // 
            this.lblChiTietTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChiTietTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChiTietTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(200)))));
            this.lblChiTietTitle.Location = new System.Drawing.Point(10, 10);
            this.lblChiTietTitle.Name = "lblChiTietTitle";
            this.lblChiTietTitle.Size = new System.Drawing.Size(320, 27);
            this.lblChiTietTitle.TabIndex = 1;
            this.lblChiTietTitle.Text = "Chi tiết phiếu";
            // 
            // ucLichSu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "ucLichSu";
            this.Size = new System.Drawing.Size(1000, 700);
            this.tlpMain.ResumeLayout(false);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.tlpContent.ResumeLayout(false);
            this.pListCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuAll)).EndInit();
            this.pDetailCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietLichSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoaiPhieu;
        private System.Windows.Forms.Button btnLocLichSu;
        private System.Windows.Forms.Button btnRefreshLichSu;
        private System.Windows.Forms.Panel pListCard;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.Panel line1;
        private System.Windows.Forms.DataGridView dgvLichSuAll;
        private System.Windows.Forms.Panel pDetailCard;
        private System.Windows.Forms.Label lblChiTietTitle;
        private System.Windows.Forms.DataGridView dgvChiTietLichSu;
    }
}