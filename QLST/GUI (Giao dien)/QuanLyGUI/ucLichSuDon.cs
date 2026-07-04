// ucLichSuDon.cs
using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucLichSuDon : UserControl
    {
        private QLHD_BLL _bll = new QLHD_BLL();
        private bool _isLoadingData = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public ucLichSuDon()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();

            SendMessage(txtCustomer.Handle, EM_SETCUEBANNER, 0, "Nhập tên hoặc SĐT...");
            SendMessage(txtCashier.Handle, EM_SETCUEBANNER, 0, "Tên nhân viên...");
        }

        private void InitializeUI()
        {
            this.dgvInvoices.Columns.Clear();
            this.dgvInvoices.Columns.Add("MaHD", "Mã HĐ");
            this.dgvInvoices.Columns.Add("Ngay", "Thời gian");
            this.dgvInvoices.Columns.Add("KhachHang", "Khách hàng");
            this.dgvInvoices.Columns.Add("SDT", "SĐT");
            this.dgvInvoices.Columns.Add("PhuongThuc", "P. thanh toán");
            this.dgvInvoices.Columns.Add("ThuNgan", "Thu ngân");
            this.dgvInvoices.Columns.Add("TongTien", "Tổng tiền");

            this.dgvInvoices.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgvDetails.Columns.Clear();
            this.dgvDetails.Columns.Add("TenSP", "Sản phẩm");
            this.dgvDetails.Columns.Add("SoLuong", "Số lượng");
            this.dgvDetails.Columns.Add("DonGia", "Đơn giá");
            this.dgvDetails.Columns.Add("ThanhTien", "Thành tiền");

            this.dgvDetails.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvDetails.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetails.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dtpFromDate.Value = DateTime.Today.AddDays(-7);
            this.dtpToDate.Value = DateTime.Today;
            this.cboPaymentMethod.SelectedIndex = 0;

            ApplyModernStyle();
        }

        private void ApplyModernStyle()
        {
            this.pnlFilter.BackColor = Color.White;

            this.txtCustomer.BackColor = Color.White;
            this.txtCustomer.ForeColor = Color.FromArgb(50, 50, 50);
            this.txtCustomer.BorderStyle = BorderStyle.FixedSingle;
            this.txtCustomer.Font = new Font("Segoe UI", 10f);

            this.txtCashier.BackColor = Color.White;
            this.txtCashier.ForeColor = Color.FromArgb(50, 50, 50);
            this.txtCashier.BorderStyle = BorderStyle.FixedSingle;
            this.txtCashier.Font = new Font("Segoe UI", 10f);

            StyleDataGridView(this.dgvInvoices);
            StyleDataGridView(this.dgvDetails);
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 32;
            dgv.EnableHeadersVisualStyles = false;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 40, 60);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgv.DefaultCellStyle.Padding = new Padding(5);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252);
            dgv.RowTemplate.Height = 28;
            dgv.GridColor = Color.FromArgb(240, 240, 244);

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadData()
        {
            try
            {
                _isLoadingData = true;
                this.dgvInvoices.Rows.Clear();
                this.dgvDetails.Rows.Clear();

                var fromDate = this.dtpFromDate.Value;
                var toDate = this.dtpToDate.Value;
                var paymentMethod = this.cboPaymentMethod.SelectedItem.ToString();
                var customerSearch = this.txtCustomer.Text.Trim();
                var cashierSearch = this.txtCashier.Text.Trim();

                var invoices = _bll.Search(fromDate, toDate, paymentMethod, customerSearch);

                foreach (var invoice in invoices)
                {
                    if (!string.IsNullOrWhiteSpace(cashierSearch) &&
                        !invoice.TenNV.ToLower().Contains(cashierSearch.ToLower()))
                    {
                        continue;
                    }

                    this.dgvInvoices.Rows.Add(
                        invoice.MaHD,
                        invoice.ThoiGianTao.ToString("dd/MM/yyyy HH:mm"),
                        invoice.TenKH,
                        invoice.SDT,
                        GetPaymentMethodBadge(invoice.PhuongThucThanhToan),
                        invoice.TenNV,
                        invoice.TongTienCung.ToString("N0") + " đ"
                    );

                    this.dgvInvoices.Rows[this.dgvInvoices.Rows.Count - 1].Tag = invoice.HoaDonID;
                }

                this.dgvInvoices.ClearSelection();
                _isLoadingData = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isLoadingData = false;
            }
        }

        private string GetPaymentMethodBadge(string method)
        {
            return method;
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoadingData) return;

            this.dgvDetails.Rows.Clear();

            if (this.dgvInvoices.SelectedRows.Count == 0)
            {
                this.lblDetailTitle.Text = "Chi tiết đơn hàng - Chọn một đơn để xem chi tiết";
                return;
            }

            var selectedRow = this.dgvInvoices.SelectedRows[0];
            if (selectedRow.Tag == null) return;

            int invoiceID = (int)selectedRow.Tag;
            string invoiceCode = selectedRow.Cells["MaHD"].Value.ToString();

            try
            {
                var details = _bll.GetChiTiet(invoiceID);
                foreach (var detail in details)
                {
                    this.dgvDetails.Rows.Add(
                        detail.TenSP,
                        detail.SoLuongMua,
                        detail.DonGiaBan.ToString("N0") + " đ",
                        detail.ThanhTien.ToString("N0") + " đ"
                    );
                }
                this.lblDetailTitle.Text = $"Chi tiết đơn hàng: {invoiceCode}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            LoadData();
        }

        public void ResetFilters()
        {
            this.dtpFromDate.Value = DateTime.Today.AddDays(-7);
            this.dtpToDate.Value = DateTime.Today;
            this.cboPaymentMethod.SelectedIndex = 0;
            this.txtCustomer.Clear();
            this.txtCashier.Clear();
            LoadData();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}