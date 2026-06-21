using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class frmQuanLyDonHang : Form
    {
        private QLHD_BLL _bll = new QLHD_BLL();
        private bool _dangLoad = false;
        public frmQuanLyDonHang()
        {
            // Lệnh bắt buộc để vẽ giao diện từ file Designer
            InitializeComponent();

            // Cấu hình thêm UI (Tạo cột, Đổ màu xen kẽ)
            CustomInitUI();

            // Mặc định load dữ liệu 7 ngày gần nhất khi mở tab
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            cboPhuongThuc.SelectedIndex = 0;

            LoadDuLieu(); 
        }

        private void CustomInitUI()
        {
            // 1. Thêm cột cho DataGridView Hóa đơn
            dgvHoaDon.Columns.Add("MaHD", "Mã HĐ");
            dgvHoaDon.Columns.Add("Ngay", "Thời gian");
            dgvHoaDon.Columns.Add("KH", "Khách hàng");
            dgvHoaDon.Columns.Add("SDT", "SĐT");
            dgvHoaDon.Columns.Add("PT", "Thanh toán");
            dgvHoaDon.Columns.Add("NV", "Thu ngân");
            dgvHoaDon.Columns.Add("Tong", "Tổng tiền");

            // 2. Thêm cột cho DataGridView Chi tiết
            dgvChiTiet.Columns.Add("TenSP", "Sản phẩm");
            dgvChiTiet.Columns.Add("SL", "Số lượng");
            dgvChiTiet.Columns.Add("Gia", "Đơn giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành tiền");

            // 3. Đổ màu chuẩn (Giống Kho)
            StyleDgv(dgvHoaDon);
            StyleDgv(dgvChiTiet);

            // 4. Bắt sự kiện co giãn
            this.Resize += (s, e) => {
                dgvHoaDon.Height = pnlMid.Height - 35;
                dgvChiTiet.Height = pnlBot.Height - 65;
            };
        }

        private void LoadDuLieu()
        {
            _dangLoad = true; // KHÓA SỰ KIỆN

            dgvHoaDon.Rows.Clear();
            dgvChiTiet.Rows.Clear();

            var from = dtpFrom.Value;
            var to = dtpTo.Value;
            var pttt = cboPhuongThuc.SelectedItem.ToString();
            var kh = txtTimKH.Text.Trim();

            var list = _bll.Search(from, to, pttt, kh);
            long tongDoanhThu = 0;

            foreach (var hd in list)
            {
                if (!string.IsNullOrWhiteSpace(txtThuNgan.Text) &&
                    !hd.TenNV.ToLower().Contains(txtThuNgan.Text.ToLower()))
                {
                    continue;
                }

                dgvHoaDon.Rows.Add(hd.MaHD, hd.ThoiGianTao.ToString("dd/MM/yyyy HH:mm"),
                                   hd.TenKH, hd.SDT, hd.PhuongThucThanhToan,
                                   hd.TenNV, hd.TongTienCung.ToString("N0") + " đ");

                dgvHoaDon.Rows[dgvHoaDon.Rows.Count - 1].Tag = hd.HoaDonID;
                tongDoanhThu += hd.TongTienCung;
            }

            lblTongDoanhThu.Text = $"📊 Tổng số đơn: {dgvHoaDon.Rows.Count}  |  💰 Tổng doanh thu: {tongDoanhThu:N0} đ";

            _dangLoad = false; // MỞ KHÓA SỰ KIỆN

            dgvHoaDon.ClearSelection();

            // (Tùy chọn) Nếu bạn muốn TỰ ĐỘNG hiển thị chi tiết của dòng đầu tiên luôn thay vì ClearSelection, hãy dùng code này:
            /* 
            if (dgvHoaDon.Rows.Count > 0)
            {
                dgvHoaDon.Rows.Selected = true;
                dgvHoaDon_SelectionChanged(null, null); // Gọi thủ công
            } 
            */
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangLoad) return;

            dgvChiTiet.Rows.Clear();
            if (dgvHoaDon.SelectedRows.Count == 0) return;

            var selected = dgvHoaDon.SelectedRows[0];
            if (selected.Tag == null) return;

            int hdID = (int)selected.Tag;
            var chiTiet = _bll.GetChiTiet(hdID);

            foreach (var ct in chiTiet)
            {
                dgvChiTiet.Rows.Add(ct.TenSP, ct.SoLuongMua,
                                    ct.DonGiaBan.ToString("N0") + " đ",
                                    ct.ThanhTien.ToString("N0") + " đ");
            }
        }

        // --- Hàm Helper ---
        private void StyleDgv(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 255);
        }

    }
}
