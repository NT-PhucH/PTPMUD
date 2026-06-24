using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucQLSP : UserControl
    {
        private readonly SanPham_BLL _bll = new SanPham_BLL();

        private int _selectedID = -1;
        private string _selectedImagePath = "";
        private List<SanPham_DTO> _currentList = new List<SanPham_DTO>();
        private Panel _selectedCard = null;

        public ucQLSP()
        {
            InitializeComponent();
            LoadLoai();
            LoadData();
            GhepSuKien();
        }

        private void GhepSuKien()
        {
            txtTimKiem.TextChanged += (s, e) => LoadData();
            cboLocLoai.SelectedIndexChanged += (s, e) => LoadData();
            cboTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            btnThemLoai.Click += BtnThemLoai_Click;
            btnChonAnh.Click += BtnChonAnh_Click;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnTrangThai.Click += BtnTrangThai_Click;
            picThem.Click += (s, e) => {
                ClearForm();
                txtMaVach.Focus();
            };
            flpSanPham.Resize += (s, e) => UpdateCardMargins();
        }

        private void UpdateCardMargins()
        {
            if (flpSanPham.Controls.Count == 0) return;

            int cardWidth = 185;
            // Lấy chiều rộng thực tế (đã trừ đi thanh cuộn dọc nếu có)
            int panelWidth = flpSanPham.ClientSize.Width - flpSanPham.Padding.Left - flpSanPham.Padding.Right;

            int minMargin = 10;
            int cardTotalWidth = cardWidth + (minMargin * 2);

            int columns = panelWidth / cardTotalWidth;
            if (columns < 1) columns = 1;

            int leftoverSpace = panelWidth - (columns * cardTotalWidth);
            int extraMargin = leftoverSpace / (columns * 2);
            int finalMargin = minMargin + extraMargin;

            flpSanPham.SuspendLayout();
            foreach (Control card in flpSanPham.Controls)
            {
                card.Margin = new Padding(finalMargin, minMargin, finalMargin, minMargin);
            }
            flpSanPham.ResumeLayout();
        }

        private void LoadLoai()
        {
            var loaiList = _bll.GetAllLoai();

            cboLoai.Items.Clear();
            cboLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Chọn loại --" });
            foreach (var l in loaiList) cboLoai.Items.Add(l);
            cboLoai.DisplayMember = "TenLoai"; cboLoai.ValueMember = "LoaiSanPhamID";
            cboLoai.SelectedIndex = 0;

            cboLocLoai.Items.Clear();
            cboLocLoai.Items.Add(new LoaiSanPham_DTO { LoaiSanPhamID = 0, TenLoai = "-- Tất cả --" });
            foreach (var l in loaiList) cboLocLoai.Items.Add(l);
            cboLocLoai.DisplayMember = "TenLoai"; cboLocLoai.ValueMember = "LoaiSanPhamID";
            cboLocLoai.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new string[] { "-- Tất cả --", "Còn hàng", "Hết hàng", "Ngừng bán" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string kw = txtTimKiem?.Text ?? "";
            int loaiID = 0;
            if (cboLocLoai?.SelectedItem is LoaiSanPham_DTO selected) loaiID = selected.LoaiSanPhamID;

            _currentList = !string.IsNullOrWhiteSpace(kw) ? _bll.Search(kw) : _bll.GetByLoai(loaiID);

            string trangThaiLoc = cboTrangThai.SelectedItem?.ToString() ?? "-- Tất cả --";
            if (trangThaiLoc == "Còn hàng")
                _currentList = _currentList.Where(x => x.TrangThai == true && x.TonKhoTong > 0).ToList();
            else if (trangThaiLoc == "Hết hàng")
                _currentList = _currentList.Where(x => x.TrangThai == true && x.TonKhoTong == 0).ToList();
            else if (trangThaiLoc == "Ngừng bán")
                _currentList = _currentList.Where(x => x.TrangThai == false).ToList();

            if (_currentList != null)
            {
                lblTongTonKho.Text = $"Tổng tồn kho\n{_currentList.Sum(x => x.TonKhoTong)}";
                lblTongGiaTri.Text = $"Tổng giá trị\n{_currentList.Sum(x => x.GiaBanHienTai * x.TonKhoTong):N0} đ";
            }

            flpSanPham.Controls.Clear();
            foreach (var sp in _currentList)
            {
                flpSanPham.Controls.Add(CreateProductCard(sp));
            }
            ClearForm();
            UpdateCardMargins();
        }

        private Panel CreateProductCard(SanPham_DTO sp)
        {
            Panel card = new Panel
            {
                Size = new Size(185, 235),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = sp
            };
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray,
                    card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray,
                    card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray,
                    card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray,
                    card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid);
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(165, 140),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            if (!string.IsNullOrEmpty(sp.HinhAnh) && File.Exists(sp.HinhAnh))
                pic.Image = Image.FromFile(sp.HinhAnh);

            Label lblTen = new Label
            {
                Text = sp.TenSP,
                Location = new Point(10, 160),
                AutoSize = false,
                Size = new Size(165, 35),
                Font = new Font("Segoe UI", 9f),
                TextAlign = ContentAlignment.TopLeft
            };

            Label lblGia = new Label
            {
                Text = $"{sp.GiaBanHienTai:N0} đ",
                Location = new Point(10, 205),
                AutoSize = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold)
            };

            Label lblTon = new Label
            {
                Text = $"Tồn kho: {sp.TonKhoTong}",
                Location = new Point(95, 208),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = sp.TonKhoTong == 0 ? Color.Red : Color.DimGray
            };

            if (!sp.TrangThai || sp.TonKhoTong == 0)
            {
                Label lblStatus = new Label
                {
                    Text = !sp.TrangThai ? "NGỪNG BÁN" : "HẾT HÀNG",
                    Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(180, 0, 0, 0),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };
                lblStatus.Location = new Point(15, 60);
                pic.Controls.Add(lblStatus);
                pic.Image = BlurImage(pic.Image);
            }

            EventHandler clickEvent = (s, e) => SelectCard(card, sp);
            card.Click += clickEvent;
            pic.Click += clickEvent;
            lblTen.Click += clickEvent;
            lblGia.Click += clickEvent;
            lblTon.Click += clickEvent;

            card.Controls.Add(pic);
            card.Controls.Add(lblTen);
            card.Controls.Add(lblGia);
            card.Controls.Add(lblTon);

            return card;
        }

        private void SelectCard(Panel card, SanPham_DTO sp)
        {
            if (_selectedCard != null) _selectedCard.Invalidate();
            _selectedCard = card;
            _selectedCard.Invalidate();

            _selectedID = sp.SanPhamID;
            txtMaVach.Text = sp.MaVach;
            txtTenSP.Text = sp.TenSP;
            txtGia.Text = sp.GiaBanHienTai.ToString();
            txtMaVach.ReadOnly = true;
            txtMaVach.BackColor = Color.WhiteSmoke;

            foreach (LoaiSanPham_DTO item in cboLoai.Items)
            {
                if (item.LoaiSanPhamID == sp.LoaiSanPhamID) { cboLoai.SelectedItem = item; break; }
            }

            if (sp.TrangThai)
            {
                btnTrangThai.Text = "⏸ NGỪNG BÁN";
                btnTrangThai.BackColor = Color.FromArgb(192, 57, 43);
            }
            else
            {
                btnTrangThai.Text = "▶ BÁN LẠI";
                btnTrangThai.BackColor = Color.FromArgb(39, 174, 96);
            }

            if (!string.IsNullOrEmpty(sp.HinhAnh) && File.Exists(sp.HinhAnh))
            {
                _selectedImagePath = sp.HinhAnh;
                picAnh.Image = Image.FromFile(sp.HinhAnh);
            }
            else
            {
                _selectedImagePath = "";
                picAnh.Image = null;
            }
            // CẬP NHẬT: Thay đổi nhãn thành số lượng tồn và giá trị của riêng sản phẩm được chọn
            lblTongTonKho.Text = $"Tồn kho SP\n{sp.TonKhoTong}";
            lblTongGiaTri.Text = $"Giá trị tồn\n{(sp.GiaBanHienTai * sp.TonKhoTong):N0} đ";

            // --- THIẾT KẾ MỚI KHI ĐÃ CHỌN SẢN PHẨM ---
            btnThem.Visible = false;

            // Hiển thị nút SỬA ở nửa bên trái
            btnSua.Visible = true;
            btnSua.Location = new Point(18, 540);
            btnSua.Size = new Size(150, 35);

            // Hiển thị nút NGỪNG BÁN ở nửa bên phải (cách nút sửa một khoảng 15px)
            btnTrangThai.Visible = true;
            btnTrangThai.Location = new Point(183, 540); // 18 + 150 + 15 = 183
            btnTrangThai.Size = new Size(150, 35);
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {

        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm!"); return; }
            var sp = BuildDTO();
            if (sp == null) return;
            sp.SanPhamID = _selectedID;

            var (ok, msg) = _bll.SuaSanPham(sp);
            if (!ok) MessageBox.Show(msg, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadData(); }
        }

        private void BtnTrangThai_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) return;
            var (ok, msg) = _bll.ThayDoiTrangThai(_selectedID);
            if (ok) LoadData(); else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnThemLoai_Click(object sender, EventArgs e)
        {
            string tenLoai = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên loại sản phẩm:", "Thêm loại", "");
            if (string.IsNullOrWhiteSpace(tenLoai)) return;
            var (ok, msg) = _bll.ThemLoai(tenLoai);
            if (ok) LoadLoai();
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog() { Filter = "Image|*.jpg;*.jpeg;*.png" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = dlg.FileName;
                    picAnh.Image = Image.FromFile(_selectedImagePath);
                }
            }
        }

        private SanPham_DTO BuildDTO()
        {
            if (!int.TryParse(txtGia.Text.Trim().Replace(",", ""), out int gia)) return null;
            int loaiID = (cboLoai.SelectedItem as LoaiSanPham_DTO)?.LoaiSanPhamID ?? 0;
            return new SanPham_DTO
            {
                MaVach = txtMaVach.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                GiaBanHienTai = gia,
                LoaiSanPhamID = loaiID,
                HinhAnh = _selectedImagePath
            };
        }

        private void ClearForm()
        {
            _selectedID = -1; _selectedImagePath = ""; _selectedCard = null;
            txtMaVach.Text = ""; txtTenSP.Text = ""; txtGia.Text = "";
            if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;
            picAnh.Image = null;
            btnTrangThai.Text = "⏸ NGỪNG BÁN";
            txtMaVach.ReadOnly = false;
            txtMaVach.BackColor = Color.White;
            foreach (Control c in flpSanPham.Controls) c.Invalidate();

            if (_currentList != null)
            {
                lblTongTonKho.Text = $"Tổng tồn kho\n{_currentList.Sum(x => x.TonKhoTong)}";
                lblTongGiaTri.Text = $"Tổng giá trị\n{_currentList.Sum(x => x.GiaBanHienTai * x.TonKhoTong):N0} đ";
            }

            btnSua.Visible = false;
            btnTrangThai.Visible = false;

            btnThem.Visible = true;
            btnThem.Location = new Point(18, 540); // Đặt tại tọa độ X ban đầu
            btnThem.Size = new Size(315, 35);
        }

        private Image BlurImage(Image image)
        {
            if (image == null) return null;
            Bitmap blurred = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(blurred))
            {
                ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, blurred.Width, blurred.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return blurred;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}