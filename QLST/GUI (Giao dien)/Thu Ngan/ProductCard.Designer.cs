namespace QLST.GUI__Giao_dien_
{
    partial class ProductCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSP = new System.Windows.Forms.Button();
            this.tlpSP = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductPrice = new System.Windows.Forms.Label();
            this.tlpSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSP
            // 
            this.btnSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSP.Location = new System.Drawing.Point(2, 2);
            this.btnSP.Name = "btnSP";
            this.btnSP.Size = new System.Drawing.Size(196, 146);
            this.btnSP.TabIndex = 0;
            this.btnSP.UseVisualStyleBackColor = true;
            // 
            // tlpSP
            // 
            this.tlpSP.BackColor = System.Drawing.Color.White;
            this.tlpSP.ColumnCount = 2;
            this.tlpSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpSP.Controls.Add(this.pictureBox1, 0, 0);
            this.tlpSP.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tlpSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSP.Location = new System.Drawing.Point(2, 2);
            this.tlpSP.Name = "tlpSP";
            this.tlpSP.RowCount = 1;
            this.tlpSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSP.Size = new System.Drawing.Size(196, 146);
            this.tlpSP.TabIndex = 1;
            this.tlpSP.Click += new System.EventHandler(this.tlpSP_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(52, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.tlpSP_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblProductName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblProductPrice, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(61, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(132, 140);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblProductName
            // 
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.Location = new System.Drawing.Point(3, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(126, 93);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "label1";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProductName.Click += new System.EventHandler(this.tlpSP_Click);
            // 
            // lblProductPrice
            // 
            this.lblProductPrice.AutoSize = true;
            this.lblProductPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductPrice.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPrice.Location = new System.Drawing.Point(3, 93);
            this.lblProductPrice.Name = "lblProductPrice";
            this.lblProductPrice.Size = new System.Drawing.Size(126, 47);
            this.lblProductPrice.TabIndex = 1;
            this.lblProductPrice.Text = "label2";
            this.lblProductPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProductPrice.Click += new System.EventHandler(this.tlpSP_Click);
            // 
            // ProductCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpSP);
            this.Controls.Add(this.btnSP);
            this.Name = "ProductCard";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(200, 150);
            this.tlpSP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSP;
        private System.Windows.Forms.TableLayoutPanel tlpSP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductPrice;
    }
}
