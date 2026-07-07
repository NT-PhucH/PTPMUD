using QLST.BLL__Bat_ngoai_le_.QuanLyBLL;
using QLST.DTO__Type_OTP_.QuanLyDTO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace QLST.GUI__Giao_dien_.QuanLyGUI
{
    public partial class ucQLSP : UserControl
    {
        private readonly SanPham_BLL _bll = new SanPham_BLL();

        private int _selectedID = -1;
        private string _selectedImagePath = string.Empty;
        private string _oldImagePath = string.Empty;
        private List<SanPham_DTO> _currentList = new List<SanPham_DTO>();
        private Panel _selectedCard = null;
        private readonly string _productImagesPath;

        public ucQLSP()
        {
            InitializeComponent();
            _productImagesPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources", "Anh_SP"));
            LoadLoai();
            LoadData();
            GhepSuKien();
        }
        // Hàm gán sự kiện
        private void GhepSuKien()
        {
            txtTimKiem.TextChanged += (s, e) => LoadData();
            cboLocLoai.SelectedIndexChanged += (s, e) => LoadData();
            cboTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            btnThemLoai.Click += BtnThemLoai_Click;
            btnChonAnh.Click += BtnChonAnh_Click;

            btnSua.Click += BtnSua_Click;
            btnTrangThai.Click += BtnTrangThai_Click;
            flpSanPham.Resize += (s, e) => UpdateCardMargins();
        }

        // ── HÀM TẢI ẢNH AN TOÀN VÀ KHÔNG BỊ RÒ RỈ BỘ NHỚ ──────────────────────────
        private Image LoadImageNoLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var img = Image.FromStream(fs))
                {
                    return new Bitmap(img); // Clone hẳn sang bộ nhớ mới để giải phóng file gốc
                }
            }
        }

        private void UpdateCardMargins()
        {
            if (flpSanPham.Controls.Count == 0) return;

            int cardWidth = 185;
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
            FilterData();
            UpdateStatistics();
            ClearOldImages();
            RenderCards();

            ClearForm();
            UpdateCardMargins();
        }

        private void FilterData()
        {
            string kw = txtTimKiem?.Text ?? string.Empty;
            int loaiID = 0;
            if (cboLocLoai?.SelectedItem is LoaiSanPham_DTO selected) loaiID = selected.LoaiSanPhamID;

            if (!string.IsNullOrWhiteSpace(kw))
            {
                _currentList = _bll.Search(kw);
                if (loaiID > 0)
                {
                    _currentList = _currentList.Where(x => x.LoaiSanPhamID == loaiID).ToList();
                }
            }
            else
            {
                _currentList = _bll.GetByLoai(loaiID);
            }

            string trangThaiLoc = cboTrangThai.SelectedItem?.ToString() ?? "-- Tất cả --";
            if (trangThaiLoc == "Còn hàng")
                _currentList = _currentList.Where(x => x.TrangThai == true && x.TonKhoTong > 0).ToList();
            else if (trangThaiLoc == "Hết hàng")
                _currentList = _currentList.Where(x => x.TrangThai == true && x.TonKhoTong == 0).ToList();
            else if (trangThaiLoc == "Ngừng bán")
                _currentList = _currentList.Where(x => x.TrangThai == false).ToList();
        }

        private void UpdateStatistics()
        {
            if (_currentList != null)
            {
                lblTongTonKho.Text = $"Tổng tồn kho\n{_currentList.Sum(x => x.TonKhoTong)}";
                lblTongGiaTri.Text = $"Tổng giá trị\n{_currentList.Sum(x => (long)x.GiaBanHienTai * x.TonKhoTong):N0} đ";
            }
        }

        private void ClearOldImages()
        {
            foreach (Control card in flpSanPham.Controls)
            {
                foreach (Control c in card.Controls)
                {
                    if (c is PictureBox p && p.Image != null) p.Image.Dispose();
                }
                card.Dispose();
            }
            flpSanPham.Controls.Clear();
        }

        private void RenderCards()
        {
            foreach (var sp in _currentList)
            {
                flpSanPham.Controls.Add(CreateProductCard(sp));
            }
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
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray, card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray, card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray, card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid,
                    card == _selectedCard ? Color.DodgerBlue : Color.LightGray, card == _selectedCard ? 2 : 1, ButtonBorderStyle.Solid);
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(165, 140),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            string fullPath = string.IsNullOrEmpty(sp.HinhAnh) ? string.Empty : Path.Combine(_productImagesPath, sp.HinhAnh);
            if (File.Exists(fullPath))
            {
                pic.Image = LoadImageNoLock(fullPath);
            }

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

                if (pic.Image != null)
                {
                    Image oldImg = pic.Image;
                    // Đổi tên từ BlurImage thành SetImageOpacity cho đúng nghiệp vụ
                    pic.Image = SetImageOpacity(oldImg, 0.3f);
                    oldImg.Dispose();
                }
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

        // TÁCH NHỎ HÀM SELECT CARD THÀNH CÁC HÀM RIÊNG BIỆT
        private void SelectCard(Panel card, SanPham_DTO sp)
        {
            UpdateCardSelectionUI(card);
            LoadProductInfoToForm(sp);
            UpdateFormButtons(sp.TrangThai);
            LoadProductImageToForm(sp);
        }

        private void UpdateCardSelectionUI(Panel card)
        {
            if (_selectedCard != null) _selectedCard.Invalidate();
            _selectedCard = card;
            _selectedCard.Invalidate();
        }

        private void LoadProductInfoToForm(SanPham_DTO sp)
        {
            _selectedID = sp.SanPhamID;
            _oldImagePath = sp.HinhAnh;
            txtMaVach.Text = sp.MaVach;
            txtTenSP.Text = sp.TenSP;
            txtGia.Text = sp.GiaBanHienTai.ToString();
            txtMaVach.ReadOnly = true;
            txtMaVach.BackColor = Color.WhiteSmoke;

            foreach (LoaiSanPham_DTO item in cboLoai.Items)
            {
                if (item.LoaiSanPhamID == sp.LoaiSanPhamID) { cboLoai.SelectedItem = item; break; }
            }

            lblTongTonKho.Text = $"Tồn kho SP\n{sp.TonKhoTong}";
            lblTongGiaTri.Text = $"Giá trị tồn\n{((long)sp.GiaBanHienTai * sp.TonKhoTong):N0} đ";
        }

        private void UpdateFormButtons(bool trangThai)
        {
            if (trangThai)
            {
                btnTrangThai.Text = "⏸ NGỪNG BÁN";
                btnTrangThai.BackColor = Color.FromArgb(192, 57, 43);
            }
            else
            {
                btnTrangThai.Text = "▶ BÁN LẠI";
                btnTrangThai.BackColor = Color.FromArgb(39, 174, 96);
            }

            btnSua.Visible = true;
            btnSua.Location = new Point(18, 540);
            btnSua.Size = new Size(150, 35);

            btnTrangThai.Visible = true;
            btnTrangThai.Location = new Point(183, 540);
            btnTrangThai.Size = new Size(150, 35);
        }

        private void LoadProductImageToForm(SanPham_DTO sp)
        {
            string fullPath = string.IsNullOrEmpty(sp.HinhAnh) ? string.Empty : Path.Combine(_productImagesPath, sp.HinhAnh);
            if (File.Exists(fullPath))
            {
                _selectedImagePath = sp.HinhAnh;
                if (picAnh.Image != null) picAnh.Image.Dispose();
                picAnh.Image = LoadImageNoLock(fullPath);
            }
            else
            {
                _selectedImagePath = string.Empty;
                if (picAnh.Image != null) picAnh.Image.Dispose();
                picAnh.Image = null;
            }
            // CẬP NHẬT: Thay đổi nhãn thành số lượng tồn và giá trị của riêng sản phẩm được chọn
            lblTongTonKho.Text = $"Tồn kho SP\n{sp.TonKhoTong}";
            lblTongGiaTri.Text = $"Giá trị tồn\n{(sp.GiaBanHienTai * sp.TonKhoTong):N0} đ";

            // --- THIẾT KẾ MỚI KHI ĐÃ CHỌN SẢN PHẨM ---

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
            var sp = BuildDTO();
            if (sp == null) return;

            var (ok, msg) = _bll.ThemSanPham(sp);
            if (!ok) MessageBox.Show(msg, "Lỗi thêm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_selectedID <= 0) { MessageBox.Show("Vui lòng chọn sản phẩm!"); return; }
            var sp = BuildDTO();
            if (sp == null) return;
            sp.SanPhamID = _selectedID;

            // TRUYỀN THÊM _oldImagePath VÀO HÀM BLL
            var (ok, msg) = _bll.SuaSanPham(sp, _oldImagePath);

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
                    // Xóa ảnh cũ khỏi PictureBox
                    if (picAnh.Image != null)
                    {
                        picAnh.Image.Dispose();
                        picAnh.Image = null;
                    }

                    // Cơ chế xóa file ảnh cũ trong thư mục dự án
                    if (!string.IsNullOrEmpty(_selectedImagePath))
                    {
                        string duongDanAnhCu = Path.Combine(Application.StartupPath, _selectedImagePath);
                        if (File.Exists(duongDanAnhCu)) // Hàm có sẵn của thư viện để kiểm tra file tồn tại
                        {
                            File.Delete(duongDanAnhCu); // Hàm có sẵn của thư viện để xóa cứng file
                        }
                    }

                    // Copy ảnh mới vào project
                    string thuMucDich = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources", "Anh_SP");
                    if (!Directory.Exists(thuMucDich)) Directory.CreateDirectory(thuMucDich);

                    string tenFileMoi = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(dlg.FileName);
                    string duongDanDich = Path.Combine(thuMucDich, tenFileMoi);
                    File.Copy(dlg.FileName, duongDanDich, true);

                    // Lưu đường dẫn tương đối
                    _selectedImagePath = tenFileMoi;
                    picAnh.Image = LoadImageNoLock(duongDanDich);
                }
            }
        }

        private SanPham_DTO BuildDTO()
        {
            // Cải thiện format giá tiền theo culture, hoặc thử lùi lại bằng replace nếu culture parse thất bại.
            if (!int.TryParse(txtGia.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out int gia))
            {
                if (!int.TryParse(txtGia.Text.Trim().Replace(",", "").Replace(".", ""), out gia))
                    return null;
            }

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
            _selectedID = -1; _selectedImagePath = string.Empty; _selectedCard = null; _oldImagePath = string.Empty;
            txtMaVach.Text = string.Empty; txtTenSP.Text = string.Empty; txtGia.Text = string.Empty;
            if (cboLoai.Items.Count > 0) cboLoai.SelectedIndex = 0;

            if (picAnh.Image != null)
            {
                picAnh.Image.Dispose();
                picAnh.Image = null;
            }

            btnTrangThai.Text = "⏸ NGỪNG BÁN";
            txtMaVach.ReadOnly = false;
            txtMaVach.BackColor = Color.White;
            foreach (Control c in flpSanPham.Controls) c.Invalidate();

            if (_currentList != null)
            {
                lblTongTonKho.Text = $"Tổng tồn kho\n{_currentList.Sum(x => x.TonKhoTong)}";
                lblTongGiaTri.Text = $"Tổng giá trị\n{_currentList.Sum(x => (long)x.GiaBanHienTai * x.TonKhoTong):N0} đ";
            }

            btnSua.Visible = false;
            btnTrangThai.Visible = false;

        }

        private Image SetImageOpacity(Image image, float opacity)
        {
            if (image == null) return null;
            Bitmap transparentImg = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(transparentImg))
            {
                ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 0, 0, opacity, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, transparentImg.Width, transparentImg.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return transparentImg;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnChonAnh_Click_1(object sender, EventArgs e)
        {

        }
    }
}