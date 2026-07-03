namespace QLST
{
    partial class ucThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cboThoiGian = new System.Windows.Forms.ComboBox();
            this.tlpKPI = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKPI1 = new System.Windows.Forms.Panel();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlKPI2 = new System.Windows.Forms.Panel();
            this.lblLoiNhuan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlKPI3 = new System.Windows.Forms.Panel();
            this.lblTongDonHang = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlKPI4 = new System.Windows.Forms.Panel();
            this.lblGiaTriTrungBinh = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlTopProduct = new System.Windows.Forms.Panel();
            this.btnCTTopSp = new System.Windows.Forms.Button();
            this.dgvTopProduct = new System.Windows.Forms.DataGridView();
            this.lblTitleTop = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnXuatData = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.tlpKPI.SuspendLayout();
            this.pnlKPI1.SuspendLayout();
            this.pnlKPI2.SuspendLayout();
            this.pnlKPI3.SuspendLayout();
            this.pnlKPI4.SuspendLayout();
            this.tlpMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.pnlTopProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProduct)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpFilter, 0, 0);
            this.tlpMain.Controls.Add(this.tlpKPI, 0, 1);
            this.tlpMain.Controls.Add(this.tlpMiddle, 0, 2);
            this.tlpMain.Controls.Add(this.pnlBottom, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.Size = new System.Drawing.Size(1100, 800);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpFilter
            // 
            this.tlpFilter.BackColor = System.Drawing.Color.White;
            this.tlpFilter.ColumnCount = 5;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpFilter.Controls.Add(this.btnLoc, 4, 0);
            this.tlpFilter.Controls.Add(this.dtTuNgay, 0, 0);
            this.tlpFilter.Controls.Add(this.dtDenNgay, 1, 0);
            this.tlpFilter.Controls.Add(this.cboThoiGian, 2, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(5, 5);
            this.tlpFilter.Margin = new System.Windows.Forms.Padding(5);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(1090, 50);
            this.tlpFilter.TabIndex = 0;
            // 
            // btnLoc
            // 
            this.btnLoc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLoc.AutoSize = true;
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(947, 8);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(130, 33);
            this.btnLoc.TabIndex = 3;
            this.btnLoc.Text = "LỌC DỮ LIỆU";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTuNgay.Location = new System.Drawing.Point(13, 10);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Size = new System.Drawing.Size(140, 30);
            this.dtTuNgay.TabIndex = 1;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDenNgay.Location = new System.Drawing.Point(173, 10);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(140, 30);
            this.dtDenNgay.TabIndex = 2;
            // 
            // cboThoiGian
            // 
            this.cboThoiGian.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboThoiGian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboThoiGian.FormattingEnabled = true;
            this.cboThoiGian.Items.AddRange(new object[] {
            "Tùy chỉnh",
            "Theo tuần",
            "Theo tháng",
            "Theo năm"});
            this.cboThoiGian.Location = new System.Drawing.Point(333, 9);
            this.cboThoiGian.Name = "cboThoiGian";
            this.cboThoiGian.Size = new System.Drawing.Size(140, 31);
            this.cboThoiGian.TabIndex = 0;
            this.cboThoiGian.SelectedIndexChanged += new System.EventHandler(this.cboThoiGian_SelectedIndexChanged);
            // 
            // tlpKPI
            // 
            this.tlpKPI.ColumnCount = 4;
            this.tlpKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKPI.Controls.Add(this.pnlKPI1, 0, 0);
            this.tlpKPI.Controls.Add(this.pnlKPI2, 1, 0);
            this.tlpKPI.Controls.Add(this.pnlKPI3, 2, 0);
            this.tlpKPI.Controls.Add(this.pnlKPI4, 3, 0);
            this.tlpKPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpKPI.Location = new System.Drawing.Point(3, 63);
            this.tlpKPI.Name = "tlpKPI";
            this.tlpKPI.RowCount = 1;
            this.tlpKPI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKPI.Size = new System.Drawing.Size(1094, 94);
            this.tlpKPI.TabIndex = 1;
            // 
            // pnlKPI1
            // 
            this.pnlKPI1.BackColor = System.Drawing.Color.White;
            this.pnlKPI1.Controls.Add(this.lblDoanhThu);
            this.pnlKPI1.Controls.Add(this.label1);
            this.pnlKPI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI1.Location = new System.Drawing.Point(8, 5);
            this.pnlKPI1.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.pnlKPI1.Name = "pnlKPI1";
            this.pnlKPI1.Size = new System.Drawing.Size(257, 84);
            this.pnlKPI1.TabIndex = 0;
            this.pnlKPI1.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // lblDoanhThu
            // 
            this.lblDoanhThu.AutoSize = true;
            this.lblDoanhThu.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(182)))), ((int)(((byte)(122)))));
            this.lblDoanhThu.Location = new System.Drawing.Point(15, 45);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(80, 30);
            this.lblDoanhThu.TabIndex = 0;
            this.lblDoanhThu.Text = "0 VNĐ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "❌ Doanh Thu";
            // 
            // pnlKPI2
            // 
            this.pnlKPI2.BackColor = System.Drawing.Color.White;
            this.pnlKPI2.Controls.Add(this.lblLoiNhuan);
            this.pnlKPI2.Controls.Add(this.label2);
            this.pnlKPI2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI2.Location = new System.Drawing.Point(281, 5);
            this.pnlKPI2.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.pnlKPI2.Name = "pnlKPI2";
            this.pnlKPI2.Size = new System.Drawing.Size(257, 84);
            this.pnlKPI2.TabIndex = 1;
            this.pnlKPI2.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // lblLoiNhuan
            // 
            this.lblLoiNhuan.AutoSize = true;
            this.lblLoiNhuan.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblLoiNhuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblLoiNhuan.Location = new System.Drawing.Point(15, 45);
            this.lblLoiNhuan.Name = "lblLoiNhuan";
            this.lblLoiNhuan.Size = new System.Drawing.Size(80, 30);
            this.lblLoiNhuan.TabIndex = 0;
            this.lblLoiNhuan.Text = "0 VNĐ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "💎 Lợi Nhuận Gộp";
            // 
            // pnlKPI3
            // 
            this.pnlKPI3.BackColor = System.Drawing.Color.White;
            this.pnlKPI3.Controls.Add(this.lblTongDonHang);
            this.pnlKPI3.Controls.Add(this.label3);
            this.pnlKPI3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI3.Location = new System.Drawing.Point(554, 5);
            this.pnlKPI3.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.pnlKPI3.Name = "pnlKPI3";
            this.pnlKPI3.Size = new System.Drawing.Size(257, 84);
            this.pnlKPI3.TabIndex = 2;
            this.pnlKPI3.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // lblTongDonHang
            // 
            this.lblTongDonHang.AutoSize = true;
            this.lblTongDonHang.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTongDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTongDonHang.Location = new System.Drawing.Point(15, 45);
            this.lblTongDonHang.Name = "lblTongDonHang";
            this.lblTongDonHang.Size = new System.Drawing.Size(75, 30);
            this.lblTongDonHang.TabIndex = 0;
            this.lblTongDonHang.Text = "0 Đơn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "🧾 Tổng Hóa Đơn";
            // 
            // pnlKPI4
            // 
            this.pnlKPI4.BackColor = System.Drawing.Color.White;
            this.pnlKPI4.Controls.Add(this.lblGiaTriTrungBinh);
            this.pnlKPI4.Controls.Add(this.label4);
            this.pnlKPI4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI4.Location = new System.Drawing.Point(827, 5);
            this.pnlKPI4.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.pnlKPI4.Name = "pnlKPI4";
            this.pnlKPI4.Size = new System.Drawing.Size(259, 84);
            this.pnlKPI4.TabIndex = 3;
            this.pnlKPI4.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // lblGiaTriTrungBinh
            // 
            this.lblGiaTriTrungBinh.AutoSize = true;
            this.lblGiaTriTrungBinh.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblGiaTriTrungBinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblGiaTriTrungBinh.Location = new System.Drawing.Point(15, 45);
            this.lblGiaTriTrungBinh.Name = "lblGiaTriTrungBinh";
            this.lblGiaTriTrungBinh.Size = new System.Drawing.Size(80, 30);
            this.lblGiaTriTrungBinh.TabIndex = 0;
            this.lblGiaTriTrungBinh.Text = "0 VNĐ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(15, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "🛒 Trung Bình/Đơn";
            // 
            // tlpMiddle
            // 
            this.tlpMiddle.ColumnCount = 2;
            this.tlpMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpMiddle.Controls.Add(this.chartDoanhThu, 0, 0);
            this.tlpMiddle.Controls.Add(this.pnlTopProduct, 1, 0);
            this.tlpMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMiddle.Location = new System.Drawing.Point(3, 163);
            this.tlpMiddle.Name = "tlpMiddle";
            this.tlpMiddle.RowCount = 1;
            this.tlpMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMiddle.Size = new System.Drawing.Size(1094, 584);
            this.tlpMiddle.TabIndex = 2;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(5, 5);
            this.chartDoanhThu.Margin = new System.Windows.Forms.Padding(5);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(701, 574);
            this.chartDoanhThu.TabIndex = 0;
            // 
            // pnlTopProduct
            // 
            this.pnlTopProduct.BackColor = System.Drawing.Color.White;
            this.pnlTopProduct.Controls.Add(this.btnCTTopSp);
            this.pnlTopProduct.Controls.Add(this.dgvTopProduct);
            this.pnlTopProduct.Controls.Add(this.lblTitleTop);
            this.pnlTopProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopProduct.Location = new System.Drawing.Point(716, 5);
            this.pnlTopProduct.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTopProduct.Name = "pnlTopProduct";
            this.pnlTopProduct.Size = new System.Drawing.Size(373, 574);
            this.pnlTopProduct.TabIndex = 1;
            // 
            // btnCTTopSp
            // 
            this.btnCTTopSp.AutoSize = true;
            this.btnCTTopSp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCTTopSp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCTTopSp.Location = new System.Drawing.Point(0, 544);
            this.btnCTTopSp.Name = "btnCTTopSp";
            this.btnCTTopSp.Size = new System.Drawing.Size(373, 30);
            this.btnCTTopSp.TabIndex = 3;
            this.btnCTTopSp.Text = "Xem chi tiết >>";
            this.btnCTTopSp.UseVisualStyleBackColor = true;
            this.btnCTTopSp.Click += new System.EventHandler(this.btnCTTopSp_Click);
            // 
            // dgvTopProduct
            // 
            this.dgvTopProduct.AllowUserToAddRows = false;
            this.dgvTopProduct.AllowUserToDeleteRows = false;
            this.dgvTopProduct.AllowUserToResizeColumns = false;
            this.dgvTopProduct.AllowUserToResizeRows = false;
            this.dgvTopProduct.ColumnHeadersHeight = 29;
            this.dgvTopProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProduct.Location = new System.Drawing.Point(0, 35);
            this.dgvTopProduct.Name = "dgvTopProduct";
            this.dgvTopProduct.ReadOnly = true;
            this.dgvTopProduct.RowHeadersVisible = false;
            this.dgvTopProduct.RowHeadersWidth = 51;
            this.dgvTopProduct.RowTemplate.Height = 35;
            this.dgvTopProduct.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvTopProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProduct.Size = new System.Drawing.Size(373, 539);
            this.dgvTopProduct.TabIndex = 1;
            this.dgvTopProduct.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvTopProduct_MouseDown);
            // 
            // lblTitleTop
            // 
            this.lblTitleTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleTop.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleTop.Location = new System.Drawing.Point(0, 0);
            this.lblTitleTop.Name = "lblTitleTop";
            this.lblTitleTop.Size = new System.Drawing.Size(373, 35);
            this.lblTitleTop.TabIndex = 2;
            this.lblTitleTop.Text = "🏆 TOP SẢN PHẨM BÁN CHẠY";
            this.lblTitleTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnXuatData);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(3, 753);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1094, 44);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnXuatData
            // 
            this.btnXuatData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnXuatData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatData.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatData.ForeColor = System.Drawing.Color.White;
            this.btnXuatData.Location = new System.Drawing.Point(925, 5);
            this.btnXuatData.Name = "btnXuatData";
            this.btnXuatData.Size = new System.Drawing.Size(160, 32);
            this.btnXuatData.TabIndex = 4;
            this.btnXuatData.Text = "⬇ XUẤT DỮ LIỆU";
            this.btnXuatData.UseVisualStyleBackColor = false;
            this.btnXuatData.Click += new System.EventHandler(this.btnXuatData_Click);
            // 
            // ucThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.tlpMain);
            this.Name = "ucThongKe";
            this.Size = new System.Drawing.Size(1100, 800);
            this.tlpMain.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.tlpFilter.PerformLayout();
            this.tlpKPI.ResumeLayout(false);
            this.pnlKPI1.ResumeLayout(false);
            this.pnlKPI1.PerformLayout();
            this.pnlKPI2.ResumeLayout(false);
            this.pnlKPI2.PerformLayout();
            this.pnlKPI3.ResumeLayout(false);
            this.pnlKPI3.PerformLayout();
            this.pnlKPI4.ResumeLayout(false);
            this.pnlKPI4.PerformLayout();
            this.tlpMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.pnlTopProduct.ResumeLayout(false);
            this.pnlTopProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProduct)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.ComboBox cboThoiGian;
        private System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.TableLayoutPanel tlpKPI;
        private System.Windows.Forms.Panel pnlKPI1;
        private System.Windows.Forms.Label lblDoanhThu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlKPI2;
        private System.Windows.Forms.Label lblLoiNhuan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlKPI3;
        private System.Windows.Forms.Label lblTongDonHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlKPI4;
        private System.Windows.Forms.Label lblGiaTriTrungBinh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tlpMiddle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Panel pnlTopProduct;
        private System.Windows.Forms.Label lblTitleTop;
        private System.Windows.Forms.Button btnCTTopSp;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnXuatData;
        private System.Windows.Forms.DataGridView dgvTopProduct;
    }
}