namespace QLST.GUI__Giao_dien_.ThuNganGUI.Ban_Hang
{
    partial class ucThongBaoDonTam
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.FlowLayoutPanel flpDanhSachThongBao;

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
            this.lblHeader = new System.Windows.Forms.Label();
            this.flpDanhSachThongBao = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblHeader.Size = new System.Drawing.Size(350, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Hóa đơn chờ thanh toán";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpDanhSachThongBao
            // 
            this.flpDanhSachThongBao.AutoScroll = true;
            this.flpDanhSachThongBao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.flpDanhSachThongBao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDanhSachThongBao.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDanhSachThongBao.Location = new System.Drawing.Point(0, 40);
            this.flpDanhSachThongBao.Name = "flpDanhSachThongBao";
            this.flpDanhSachThongBao.Size = new System.Drawing.Size(350, 360);
            this.flpDanhSachThongBao.TabIndex = 1;
            this.flpDanhSachThongBao.WrapContents = false;
            // 
            // ucThongBaoDonTam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flpDanhSachThongBao);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucThongBaoDonTam";
            this.Size = new System.Drawing.Size(350, 400);
            this.ResumeLayout(false);
        }
    }
}