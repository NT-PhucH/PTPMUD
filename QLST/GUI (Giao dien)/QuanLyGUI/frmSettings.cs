// ===================================================
// File: frmSettings.cs
// Đặt vào: GUI (Giao dien) > QuanLyGUI
// ===================================================
using QLST.DAL__Connection_Query_DB_.Query_DB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public class frmSettings : Form
    {
        // Controls - Thông tin cửa hàng
        private TextBox txtTenCH, txtDiaChi, txtSDT, txtEmail;
        // Controls - Tài chính
        private TextBox txtVAT, txtDiemPer10k, txtNguongCanhBao;
        // Controls - In ấn
        private TextBox txtFooterHoaDon;
        private Button btnLuu, btnReset;
        private TabControl tabMain;

        public frmSettings()
        {
            BuildUI();
            LoadSettings();
        }

        private void BuildUI()
        {
            Text = "Cài đặt hệ thống";
            Size = new Size(750, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5f);
            BackColor = Color.FromArgb(245, 247, 250);

            var header = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(30, 40, 60) };
            header.Controls.Add(new Label
            {
                Text = "⚙️  CÀI ĐẶT HỆ THỐNG",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                Location = new Point(15, 13),
                AutoSize = true
            });

            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Padding = new Point(12, 6)
            };
            tabMain.TabPages.Add(BuildTabCuaHang());
            tabMain.TabPages.Add(BuildTabTaiChinh());
            tabMain.TabPages.Add(BuildTabInAn());

            // Nút lưu / reset ở dưới
            var pBottom = new Panel { Dock = DockStyle.Bottom, Height = 55, BackColor = Color.White, Padding = new Padding(15, 10, 15, 0) };
            btnLuu = new Button
            {
                Text = "💾 LƯU CÀI ĐẶT",
                Location = new Point(15, 10),
                Width = 160,
                Height = 34,
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold)
            };
            btnLuu.Click += BtnLuu_Click;

            btnReset = new Button
            {
                Text = "🔄 Tải lại",
                Location = new Point(185, 10),
                Width = 110,
                Height = 34,
                BackColor = Color.FromArgb(130, 130, 130),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold)
            };
            btnReset.Click += (s, e) => LoadSettings();
            pBottom.Controls.AddRange(new Control[] { btnLuu, btnReset });

            Controls.Add(tabMain);
            Controls.Add(pBottom);
            Controls.Add(header);
        }

        // ── TAB 1: THÔNG TIN CỬA HÀNG ────────────────────────────────────────
        private TabPage BuildTabCuaHang()
        {
            var tab = new TabPage("  🏪 Cửa hàng  ");
            tab.BackColor = Color.White;
            int y = 20;

            tab.Controls.Add(MakeBold("Thông tin cửa hàng (in trên hóa đơn)", 20, y));

            y += 40;
            tab.Controls.Add(MakeLabel("Tên cửa hàng:", 20, y));
            txtTenCH = MakeTextBox(20, y + 22, 650); tab.Controls.Add(txtTenCH);

            y += 65;
            tab.Controls.Add(MakeLabel("Địa chỉ:", 20, y));
            txtDiaChi = MakeTextBox(20, y + 22, 650); tab.Controls.Add(txtDiaChi);

            y += 65;
            tab.Controls.Add(MakeLabel("Số điện thoại:", 20, y));
            txtSDT = MakeTextBox(20, y + 22, 300); tab.Controls.Add(txtSDT);

            tab.Controls.Add(MakeLabel("Email:", 360, y));
            txtEmail = MakeTextBox(360, y + 22, 310); tab.Controls.Add(txtEmail);

            return tab;
        }

        // ── TAB 2: TÀI CHÍNH ─────────────────────────────────────────────────
        private TabPage BuildTabTaiChinh()
        {
            var tab = new TabPage("  💰 Tài chính  ");
            tab.BackColor = Color.White;
            int y = 20;

            tab.Controls.Add(MakeBold("Cài đặt tài chính", 20, y));

            y += 40;
            tab.Controls.Add(MakeLabel("Thuế VAT (%):", 20, y));
            txtVAT = MakeTextBox(20, y + 22, 120);
            txtVAT.Text = "0";
            tab.Controls.Add(txtVAT);
            tab.Controls.Add(new Label
            {
                Text = "Nhập 0 nếu không tính VAT. Ví dụ: 10 = 10%",
                Location = new Point(150, y + 25),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            });

            y += 65;
            tab.Controls.Add(MakeBold("Cài đặt tích điểm", 20, y));

            y += 35;
            tab.Controls.Add(MakeLabel("Cứ 10.000đ tích được (điểm):", 20, y));
            txtDiemPer10k = MakeTextBox(20, y + 22, 80);
            txtDiemPer10k.Text = "1";
            tab.Controls.Add(txtDiemPer10k);
            tab.Controls.Add(new Label
            {
                Text = "Ví dụ: 1 = cứ 10.000đ được 1 điểm",
                Location = new Point(110, y + 25),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            });

            y += 65;
            tab.Controls.Add(MakeBold("Cài đặt kho", 20, y));

            y += 35;
            tab.Controls.Add(MakeLabel("Ngưỡng cảnh báo hàng sắp hết (sl):", 20, y));
            txtNguongCanhBao = MakeTextBox(20, y + 22, 80);
            txtNguongCanhBao.Text = "10";
            tab.Controls.Add(txtNguongCanhBao);
            tab.Controls.Add(new Label
            {
                Text = "Sản phẩm có tồn kho ≤ ngưỡng sẽ được cảnh báo",
                Location = new Point(110, y + 25),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic)
            });

            return tab;
        }

        // ── TAB 3: IN ẤN ─────────────────────────────────────────────────────
        private TabPage BuildTabInAn()
        {
            var tab = new TabPage("  🖨️ In ấn  ");
            tab.BackColor = Color.White;
            int y = 20;

            tab.Controls.Add(MakeBold("Cài đặt in hóa đơn", 20, y));

            y += 40;
            tab.Controls.Add(MakeLabel("Dòng chân hóa đơn (lời cảm ơn, v.v.):", 20, y));
            txtFooterHoaDon = new TextBox
            {
                Location = new Point(20, y + 22),
                Width = 650,
                Height = 80,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            txtFooterHoaDon.Text = "Cảm ơn quý khách đã mua hàng!\nHẹn gặp lại!";
            tab.Controls.Add(txtFooterHoaDon);

            y += 120;
            // Preview mini
            var pPreview = new Panel
            {
                Location = new Point(20, y),
                Width = 300,
                Height = 200,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pPreview.Paint += (s, pe) => {
                var g = pe.Graphics;
                var f8 = new Font("Courier New", 8f);
                var f9b = new Font("Courier New", 9f, FontStyle.Bold);
                int py = 8;
                g.DrawString("================================", f8, Brushes.Black, 5, py); py += 14;
                g.DrawString(txtTenCH.Text.PadLeft(16 + txtTenCH.Text.Length / 2), f9b, Brushes.Black, 5, py); py += 16;
                g.DrawString(txtDiaChi.Text, f8, Brushes.Black, 5, py); py += 14;
                g.DrawString($"Tel: {txtSDT.Text}", f8, Brushes.Black, 5, py); py += 14;
                g.DrawString("================================", f8, Brushes.Black, 5, py); py += 14;
                g.DrawString("HÓA ĐƠN BÁN HÀNG", f9b, Brushes.Black, 60, py); py += 16;
                g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", f8, Brushes.Black, 5, py); py += 20;
                g.DrawString("--------------------------------", f8, Brushes.Black, 5, py); py += 14;
                g.DrawString("Sản phẩm    SL  Đơn giá  T.Tiền", f8, Brushes.Black, 5, py); py += 20;
                g.DrawString("================================", f8, Brushes.Black, 5, py); py += 14;
                g.DrawString(txtFooterHoaDon.Text, f8, Brushes.Black, 5, py);
            };
            tab.Controls.Add(new Label { Text = "📄 Preview hóa đơn:", Location = new Point(20, y - 20), AutoSize = true, Font = new Font("Segoe UI", 9f, FontStyle.Bold) });
            tab.Controls.Add(pPreview);

            var btnPreview = new Button
            {
                Text = "🔄 Cập nhật preview",
                Location = new Point(330, y + 80),
                Width = 180,
                Height = 32,
                BackColor = Color.FromArgb(100, 120, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnPreview.Click += (s, e) => pPreview.Invalidate();
            tab.Controls.Add(btnPreview);

            return tab;
        }

        // ══════════════════════════════════════════════════════════════════════
        // LOAD / LƯU ThamSoHeThong
        // ══════════════════════════════════════════════════════════════════════
        private void LoadSettings()
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery(
                    "SELECT MaThamSo, GiaTri FROM ThamSoHeThong");
                foreach (DataRow row in dt.Rows)
                {
                    string ma = row["MaThamSo"].ToString();
                    string val = row["GiaTri"] == DBNull.Value ? "" : row["GiaTri"].ToString();
                    switch (ma)
                    {
                        case "TEN_CUA_HANG": txtTenCH.Text = val; break;
                        case "DIA_CHI": txtDiaChi.Text = val; break;
                        case "SDT": txtSDT.Text = val; break;
                        case "EMAIL": txtEmail.Text = val; break;
                        case "VAT": txtVAT.Text = val; break;
                        case "DIEM_PER_10K": txtDiemPer10k.Text = val; break;
                        case "NGUONG_CANH_BAO": txtNguongCanhBao.Text = val; break;
                        case "FOOTER_HOA_DON": txtFooterHoaDon.Text = val; break;
                    }
                }
            }
            catch { /* Bảng chưa có dữ liệu - bỏ qua */ }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (!int.TryParse(txtVAT.Text.Trim(), out _))
            { MessageBox.Show("VAT phải là số!"); return; }
            if (!int.TryParse(txtDiemPer10k.Text.Trim(), out _))
            { MessageBox.Show("Điểm tích lũy phải là số!"); return; }
            if (!int.TryParse(txtNguongCanhBao.Text.Trim(), out _))
            { MessageBox.Show("Ngưỡng cảnh báo phải là số!"); return; }

            var settings = new (string ma, string val)[] {
                ("TEN_CUA_HANG",    txtTenCH.Text.Trim()),
                ("DIA_CHI",         txtDiaChi.Text.Trim()),
                ("SDT",             txtSDT.Text.Trim()),
                ("EMAIL",           txtEmail.Text.Trim()),
                ("VAT",             txtVAT.Text.Trim()),
                ("DIEM_PER_10K",    txtDiemPer10k.Text.Trim()),
                ("NGUONG_CANH_BAO", txtNguongCanhBao.Text.Trim()),
                ("FOOTER_HOA_DON",  txtFooterHoaDon.Text.Trim())
            };

            bool allOk = true;
            foreach (var (ma, val) in settings)
            {
                // MERGE: nếu đã có thì UPDATE, chưa có thì INSERT
                string sql = @"
                    IF EXISTS (SELECT 1 FROM ThamSoHeThong WHERE MaThamSo = @Ma)
                        UPDATE ThamSoHeThong SET GiaTri = @Val WHERE MaThamSo = @Ma
                    ELSE
                        INSERT INTO ThamSoHeThong (MaThamSo, GiaTri) VALUES (@Ma, @Val)";
                int rows = DataProvider.Instance.ExecuteNonQuery(sql, new SqlParameter[] {
                    new SqlParameter("@Ma",  ma),
                    new SqlParameter("@Val", val)
                });
                if (rows == 0) allOk = false;
            }

            MessageBox.Show(allOk ? "Lưu cài đặt thành công!" : "Một số cài đặt lưu thất bại!",
                "Thông báo", MessageBoxButtons.OK,
                allOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private Label MakeLabel(string t, int x, int y) =>
            new Label { Text = t, Location = new Point(x, y), AutoSize = true };
        private Label MakeBold(string t, int x, int y) =>
            new Label
            {
                Text = t,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 40, 60)
            };
        private TextBox MakeTextBox(int x, int y, int w) =>
            new TextBox { Location = new Point(x, y), Width = w };
    }
}