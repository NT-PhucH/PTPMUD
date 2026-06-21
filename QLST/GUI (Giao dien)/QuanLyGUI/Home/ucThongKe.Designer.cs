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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cboCaLamViec = new System.Windows.Forms.ComboBox();
            this.cboThuNgan = new System.Windows.Forms.ComboBox();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
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
            this.lbTopProduct = new System.Windows.Forms.ListBox();
            this.lblTitleTop = new System.Windows.Forms.Label();
            this.tlpRisks = new System.Windows.Forms.TableLayoutPanel();
            this.pnlRisk1 = new System.Windows.Forms.Panel();
            this.txtHangHoaRisk = new System.Windows.Forms.TextBox();
            this.lblRisk1Title = new System.Windows.Forms.Label();
            this.pnlRisk2 = new System.Windows.Forms.Panel();
            this.txtGiaoDichRisk = new System.Windows.Forms.TextBox();
            this.lblRisk2Title = new System.Windows.Forms.Label();
            this.dgvGiaoDich = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnCTTopSp = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tlpKPI.SuspendLayout();
            this.pnlKPI1.SuspendLayout();
            this.pnlKPI2.SuspendLayout();
            this.pnlKPI3.SuspendLayout();
            this.pnlKPI4.SuspendLayout();
            this.tlpMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.pnlTopProduct.SuspendLayout();
            this.tlpRisks.SuspendLayout();
            this.pnlRisk1.SuspendLayout();
            this.pnlRisk2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlFilter, 0, 0);
            this.tlpMain.Controls.Add(this.tlpKPI, 0, 1);
            this.tlpMain.Controls.Add(this.tlpMiddle, 0, 2);
            this.tlpMain.Controls.Add(this.tlpRisks, 0, 3);
            this.tlpMain.Controls.Add(this.dgvGiaoDich, 0, 4);
            this.tlpMain.Controls.Add(this.pnlFooter, 0, 5);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.Size = new System.Drawing.Size(1100, 800);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnLoc);
            this.pnlFilter.Controls.Add(this.cboCaLamViec);
            this.pnlFilter.Controls.Add(this.cboThuNgan);
            this.pnlFilter.Controls.Add(this.dtDenNgay);
            this.pnlFilter.Controls.Add(this.dtTuNgay);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(5, 5);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(5);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1090, 50);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnLoc
            // 
            this.btnLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoc.AutoSize = true;
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(961, 8);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(119, 33);
            this.btnLoc.TabIndex = 0;
            this.btnLoc.Text = "LỌC DỮ LIỆU";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // cboCaLamViec
            // 
            this.cboCaLamViec.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCaLamViec.FormattingEnabled = true;
            this.cboCaLamViec.Location = new System.Drawing.Point(560, 11);
            this.cboCaLamViec.Name = "cboCaLamViec";
            this.cboCaLamViec.Size = new System.Drawing.Size(140, 31);
            this.cboCaLamViec.TabIndex = 1;
            this.cboCaLamViec.Text = "Ca làm việc ";
            // 
            // cboThuNgan
            // 
            this.cboThuNgan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboThuNgan.FormattingEnabled = true;
            this.cboThuNgan.Location = new System.Drawing.Point(390, 11);
            this.cboThuNgan.Name = "cboThuNgan";
            this.cboThuNgan.Size = new System.Drawing.Size(150, 31);
            this.cboThuNgan.TabIndex = 2;
            this.cboThuNgan.Text = "Nhân viên";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDenNgay.Location = new System.Drawing.Point(200, 11);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(170, 30);
            this.dtDenNgay.TabIndex = 3;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTuNgay.Location = new System.Drawing.Point(15, 11);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Size = new System.Drawing.Size(170, 30);
            this.dtTuNgay.TabIndex = 4;
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
            this.tlpMiddle.Size = new System.Drawing.Size(1094, 210);
            this.tlpMiddle.TabIndex = 2;
            // 
            // chartDoanhThu
            // 
            chartArea4.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea4);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend4);
            this.chartDoanhThu.Location = new System.Drawing.Point(5, 5);
            this.chartDoanhThu.Margin = new System.Windows.Forms.Padding(5);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(701, 200);
            this.chartDoanhThu.TabIndex = 0;
            // 
            // pnlTopProduct
            // 
            this.pnlTopProduct.BackColor = System.Drawing.Color.White;
            this.pnlTopProduct.Controls.Add(this.btnCTTopSp);
            this.pnlTopProduct.Controls.Add(this.lbTopProduct);
            this.pnlTopProduct.Controls.Add(this.lblTitleTop);
            this.pnlTopProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopProduct.Location = new System.Drawing.Point(716, 5);
            this.pnlTopProduct.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTopProduct.Name = "pnlTopProduct";
            this.pnlTopProduct.Size = new System.Drawing.Size(373, 200);
            this.pnlTopProduct.TabIndex = 1;
            // 
            // lbTopProduct
            // 
            this.lbTopProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbTopProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTopProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbTopProduct.FormattingEnabled = true;
            this.lbTopProduct.ItemHeight = 23;
            this.lbTopProduct.Items.AddRange(new object[] {
            "1. Thùng bia Heineken - 15,000,000 VNĐ",
            "2. Sữa bột Ensure - 12,300,000 VNĐ",
            "3. Dầu ăn Tường An - 8,500,000 VNĐ",
            "4. Gạo ST25 - 6,200,000 VNĐ"});
            this.lbTopProduct.Location = new System.Drawing.Point(0, 35);
            this.lbTopProduct.Name = "lbTopProduct";
            this.lbTopProduct.Size = new System.Drawing.Size(373, 165);
            this.lbTopProduct.TabIndex = 1;
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
            // tlpRisks
            // 
            this.tlpRisks.ColumnCount = 2;
            this.tlpRisks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRisks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRisks.Controls.Add(this.pnlRisk1, 0, 0);
            this.tlpRisks.Controls.Add(this.pnlRisk2, 1, 0);
            this.tlpRisks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRisks.Location = new System.Drawing.Point(3, 379);
            this.tlpRisks.Name = "tlpRisks";
            this.tlpRisks.RowCount = 1;
            this.tlpRisks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRisks.Size = new System.Drawing.Size(1094, 104);
            this.tlpRisks.TabIndex = 3;
            // 
            // pnlRisk1
            // 
            this.pnlRisk1.BackColor = System.Drawing.Color.White;
            this.pnlRisk1.Controls.Add(this.txtHangHoaRisk);
            this.pnlRisk1.Controls.Add(this.lblRisk1Title);
            this.pnlRisk1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRisk1.Location = new System.Drawing.Point(5, 5);
            this.pnlRisk1.Margin = new System.Windows.Forms.Padding(5);
            this.pnlRisk1.Name = "pnlRisk1";
            this.pnlRisk1.Size = new System.Drawing.Size(537, 94);
            this.pnlRisk1.TabIndex = 0;
            // 
            // txtHangHoaRisk
            // 
            this.txtHangHoaRisk.BackColor = System.Drawing.Color.White;
            this.txtHangHoaRisk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHangHoaRisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHangHoaRisk.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtHangHoaRisk.ForeColor = System.Drawing.Color.DimGray;
            this.txtHangHoaRisk.Location = new System.Drawing.Point(0, 30);
            this.txtHangHoaRisk.Multiline = true;
            this.txtHangHoaRisk.Name = "txtHangHoaRisk";
            this.txtHangHoaRisk.ReadOnly = true;
            this.txtHangHoaRisk.Size = new System.Drawing.Size(537, 64);
            this.txtHangHoaRisk.TabIndex = 0;
            this.txtHangHoaRisk.Text = "⚠️ 15 Sản phẩm chạm mức tồn tối thiểu cần nhập kho.\r\n⏰ 8 Mặt hàng sắp hết hạn sử " +
    "dụng trong tuần này.";
            // 
            // lblRisk1Title
            // 
            this.lblRisk1Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRisk1Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRisk1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblRisk1Title.Location = new System.Drawing.Point(0, 0);
            this.lblRisk1Title.Name = "lblRisk1Title";
            this.lblRisk1Title.Size = new System.Drawing.Size(537, 30);
            this.lblRisk1Title.TabIndex = 1;
            this.lblRisk1Title.Text = "🔔 CẢNH BÁO KHO HÀNG";
            this.lblRisk1Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRisk2
            // 
            this.pnlRisk2.BackColor = System.Drawing.Color.White;
            this.pnlRisk2.Controls.Add(this.txtGiaoDichRisk);
            this.pnlRisk2.Controls.Add(this.lblRisk2Title);
            this.pnlRisk2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRisk2.Location = new System.Drawing.Point(552, 5);
            this.pnlRisk2.Margin = new System.Windows.Forms.Padding(5);
            this.pnlRisk2.Name = "pnlRisk2";
            this.pnlRisk2.Size = new System.Drawing.Size(537, 94);
            this.pnlRisk2.TabIndex = 1;
            // 
            // txtGiaoDichRisk
            // 
            this.txtGiaoDichRisk.BackColor = System.Drawing.Color.White;
            this.txtGiaoDichRisk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGiaoDichRisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGiaoDichRisk.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtGiaoDichRisk.ForeColor = System.Drawing.Color.DimGray;
            this.txtGiaoDichRisk.Location = new System.Drawing.Point(0, 30);
            this.txtGiaoDichRisk.Multiline = true;
            this.txtGiaoDichRisk.Name = "txtGiaoDichRisk";
            this.txtGiaoDichRisk.ReadOnly = true;
            this.txtGiaoDichRisk.Size = new System.Drawing.Size(537, 64);
            this.txtGiaoDichRisk.TabIndex = 0;
            this.txtGiaoDichRisk.Text = "❌ 5 Hóa đơn yêu cầu hoàn trả/hủy đơn (Tổng: 3,500,000đ).\r\n🎫 Phát hiện 12 giao dị" +
    "ch áp dụng Voucher trùng lặp.";
            // 
            // lblRisk2Title
            // 
            this.lblRisk2Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRisk2Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRisk2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblRisk2Title.Location = new System.Drawing.Point(0, 0);
            this.lblRisk2Title.Name = "lblRisk2Title";
            this.lblRisk2Title.Size = new System.Drawing.Size(537, 30);
            this.lblRisk2Title.TabIndex = 1;
            this.lblRisk2Title.Text = "🚨 CẢNH BÁO VẬN HÀNH & GIAO DỊCH";
            this.lblRisk2Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvGiaoDich
            // 
            this.dgvGiaoDich.AllowUserToAddRows = false;
            this.dgvGiaoDich.AllowUserToDeleteRows = false;
            this.dgvGiaoDich.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiaoDich.BackgroundColor = System.Drawing.Color.White;
            this.dgvGiaoDich.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGiaoDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiaoDich.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGiaoDich.Location = new System.Drawing.Point(5, 491);
            this.dgvGiaoDich.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.ReadOnly = true;
            this.dgvGiaoDich.RowHeadersVisible = false;
            this.dgvGiaoDich.RowHeadersWidth = 51;
            this.dgvGiaoDich.RowTemplate.Height = 24;
            this.dgvGiaoDich.Size = new System.Drawing.Size(1090, 259);
            this.dgvGiaoDich.TabIndex = 4;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExportPDF);
            this.pnlFooter.Controls.Add(this.btnExportExcel);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFooter.Location = new System.Drawing.Point(3, 753);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1094, 44);
            this.pnlFooter.TabIndex = 5;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPDF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportPDF.Location = new System.Drawing.Point(917, 9);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(169, 32);
            this.btnExportPDF.TabIndex = 0;
            this.btnExportPDF.Text = "⬇ Xuất Báo Cáo PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Location = new System.Drawing.Point(734, 9);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(177, 32);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "⬇ Xuất Excel Báo Cáo";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            // 
            // btnCTTopSp
            // 
            this.btnCTTopSp.AutoSize = true;
            this.btnCTTopSp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCTTopSp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCTTopSp.Location = new System.Drawing.Point(0, 170);
            this.btnCTTopSp.Name = "btnCTTopSp";
            this.btnCTTopSp.Size = new System.Drawing.Size(373, 30);
            this.btnCTTopSp.TabIndex = 3;
            this.btnCTTopSp.Text = "Xem chi tiết >>";
            this.btnCTTopSp.UseVisualStyleBackColor = true;
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
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
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
            this.tlpRisks.ResumeLayout(false);
            this.pnlRisk1.ResumeLayout(false);
            this.pnlRisk1.PerformLayout();
            this.pnlRisk2.ResumeLayout(false);
            this.pnlRisk2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.ComboBox cboCaLamViec;
        private System.Windows.Forms.ComboBox cboThuNgan;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.DateTimePicker dtTuNgay;
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
        private System.Windows.Forms.ListBox lbTopProduct;
        private System.Windows.Forms.Label lblTitleTop;
        private System.Windows.Forms.TableLayoutPanel tlpRisks;
        private System.Windows.Forms.Panel pnlRisk1;
        private System.Windows.Forms.TextBox txtHangHoaRisk;
        private System.Windows.Forms.Label lblRisk1Title;
        private System.Windows.Forms.Panel pnlRisk2;
        private System.Windows.Forms.TextBox txtGiaoDichRisk;
        private System.Windows.Forms.Label lblRisk2Title;
        private System.Windows.Forms.DataGridView dgvGiaoDich;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnCTTopSp;
    }
}