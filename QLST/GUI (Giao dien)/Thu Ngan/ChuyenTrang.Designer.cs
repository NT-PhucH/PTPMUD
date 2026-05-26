namespace QLST.GUI__Giao_dien_.Thu_Ngan
{
    partial class ChuyenTrang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChuyenTrang));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pageNumber = new System.Windows.Forms.Label();
            this.btnTrai = new System.Windows.Forms.Button();
            this.btnPhai = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.pageNumber, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTrai, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPhai, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(653, 121);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pageNumber
            // 
            this.pageNumber.AutoSize = true;
            this.pageNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageNumber.Location = new System.Drawing.Point(230, 0);
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(189, 121);
            this.pageNumber.TabIndex = 0;
            this.pageNumber.Text = "1 / 1";
            this.pageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrai
            // 
            this.btnTrai.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTrai.FlatAppearance.BorderSize = 0;
            this.btnTrai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrai.Image = ((System.Drawing.Image)(resources.GetObject("btnTrai.Image")));
            this.btnTrai.Location = new System.Drawing.Point(129, 44);
            this.btnTrai.Name = "btnTrai";
            this.btnTrai.Size = new System.Drawing.Size(66, 32);
            this.btnTrai.TabIndex = 1;
            this.btnTrai.UseVisualStyleBackColor = true;
            // 
            // btnPhai
            // 
            this.btnPhai.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPhai.FlatAppearance.BorderSize = 0;
            this.btnPhai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhai.Image = ((System.Drawing.Image)(resources.GetObject("btnPhai.Image")));
            this.btnPhai.Location = new System.Drawing.Point(444, 44);
            this.btnPhai.Name = "btnPhai";
            this.btnPhai.Size = new System.Drawing.Size(85, 32);
            this.btnPhai.TabIndex = 2;
            this.btnPhai.UseVisualStyleBackColor = true;
            this.btnPhai.Click += new System.EventHandler(this.btnMuiTenPhai_Click);
            // 
            // ChuyenTrang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChuyenTrang";
            this.Size = new System.Drawing.Size(653, 121);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label pageNumber;
        private System.Windows.Forms.Button btnTrai;
        private System.Windows.Forms.Button btnPhai;
    }
}
