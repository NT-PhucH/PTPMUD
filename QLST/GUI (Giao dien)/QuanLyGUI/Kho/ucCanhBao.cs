// ===================================================
// 2. File logic: ucCanhBao.cs
// ===================================================
using QLST.BLL__Bat_ngoai_le_.Core;
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucCanhBao : UserControl
    {
        private readonly Kho_BLL _bll = new Kho_BLL();
        private readonly Setting_BLL _settingBll = new Setting_BLL();

        private int _nguongHetHang = 10;
        private int _nguongHetHan = 30;

        public ucCanhBao()
        {
            InitializeComponent();
            WireEvents();
            LoadSettings();
            LoadCanhBao();
        }

        private void WireEvents()
        {
            btnReloadSapHet.Click += (s, e) => LoadCanhBaoSapHet();
            btnReloadSapHan.Click += (s, e) => LoadCanhBaoSapHetHan();

            dgvSapHet.CellFormatting += DgvSapHet_CellFormatting;
            dgvSapHetHan.CellFormatting += DgvSapHetHan_CellFormatting;

            this.VisibleChanged += (s, e) => { if (this.Visible) LoadCanhBao(); };
        }

        private void LoadSettings()
        {
            var ts = _settingBll.GetCauHinh();
            if (ts != null)
            {
                _nguongHetHang = ts.NguongHetHang;
                _nguongHetHan = ts.NguongHetHan;
            }
            lblNguongInfo.Text = $"🔴 Ngưỡng hệ thống: Báo động khi tồn kho dưới {_nguongHetHang} sản phẩm.";
            lblHanInfo.Text = $"🟡 Ngưỡng hệ thống: Báo động khi hạn sử dụng còn dưới {_nguongHetHan} ngày.";
        }

        private void LoadCanhBao()
        {
            LoadCanhBaoSapHet();
            LoadCanhBaoSapHetHan();
        }

        private void LoadCanhBaoSapHet()
        {
            dgvSapHet.Rows.Clear();
            foreach (var c in _bll.GetHangSapHet())
            {
                dgvSapHet.Rows.Add(c.MaVach, c.TenSP, c.TenLoai, c.TonKhoTong);
            }
        }

        private void LoadCanhBaoSapHetHan()
        {
            dgvSapHetHan.Rows.Clear();
            foreach (var c in _bll.GetHangSapHetHan())
            {
                dgvSapHetHan.Rows.Add(
                    c.MaVach,
                    c.TenSP,
                    c.TenLoai,
                    c.HSD.HasValue ? c.HSD.Value.ToString("dd/MM/yyyy") : "—",
                    c.HSD.HasValue ? $"{(c.HSD.Value - DateTime.Today).Days} ngày" : "—"
                );
            }
        }

        private void DgvSapHet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int ton = dgvSapHet.Rows[e.RowIndex].Cells[3].Value is int v ? v : 0;
            if (ton == 0)
                dgvSapHet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
            else if (ton <= _nguongHetHang / 2)
                dgvSapHet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 210);
        }

        private void DgvSapHetHan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var s = dgvSapHetHan.Rows[e.RowIndex].Cells[4].Value as string;
            if (s != null && s.EndsWith(" ngày") && int.TryParse(s.Replace(" ngày", ""), out int days))
            {
                if (days <= 7)
                    dgvSapHetHan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 180);
                else if (days <= 14)
                    dgvSapHetHan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 210);
            }
        }
    }
}