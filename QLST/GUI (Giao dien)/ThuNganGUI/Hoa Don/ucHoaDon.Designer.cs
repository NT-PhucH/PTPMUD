// ===================================================
// File: ucHoaDon.Designer.cs
// Đặt vào: GUI > ThuNganGUI > HoaDon
// ===================================================
namespace QLST.GUI__Giao_dien_.ThuNganGUI.Hoa_Don
{
    partial class ucHoaDon
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
            this._scroll = new System.Windows.Forms.Panel();
            this._paper = new System.Windows.Forms.Panel();
            this._scroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // _scroll
            // 
            this._scroll.AutoScroll = true;
            this._scroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this._scroll.Controls.Add(this._paper);
            this._scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scroll.Location = new System.Drawing.Point(0, 0);
            this._scroll.Name = "_scroll";
            this._scroll.Size = new System.Drawing.Size(400, 500);
            this._scroll.TabIndex = 0;
            this._scroll.Resize += new System.EventHandler(this.Scroll_Resize);
            // 
            // _paper
            // 
            this._paper.BackColor = System.Drawing.Color.White;
            this._paper.Location = new System.Drawing.Point(5, 5);
            this._paper.Name = "_paper";
            this._paper.Size = new System.Drawing.Size(302, 100);
            this._paper.TabIndex = 0;
            this._paper.Paint += new System.Windows.Forms.PaintEventHandler(this.Paper_Paint);
            // 
            // ucHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Controls.Add(this._scroll);
            this.Name = "ucHoaDon";
            this.Size = new System.Drawing.Size(400, 500);
            this._scroll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _scroll;
        private System.Windows.Forms.Panel _paper;
    }
}