using System;
using System.Windows.Forms;
using BLL;
using DTO;

namespace DocGiaUI
{
    public partial class DocGiaUI : Form
    {
        private DocGiaBLL bll = new DocGiaBLL();

        public DocGiaUI()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvDocGia.DataSource = bll.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dg = new DocGiaDTO
                {
                    HoTen = txtHoTen.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text
                };
                if (bll.ThemDocGia(dg))
                {
                    MessageBox.Show("Thêm độc giả thành công!");
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMa.Text))
                {
                    MessageBox.Show("Vui lòng chọn độc giả cần sửa trên bảng!");
                    return;
                }

                DocGiaDTO dg = new DocGiaDTO
                {
                    MaDocGia = int.Parse(txtMa.Text),
                    HoTen = txtHoTen.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text
                };
                if (bll.SuaDocGia(dg))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMa.Text))
                {
                    int ma = int.Parse(txtMa.Text);
                    if (bll.XoaDocGia(ma))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                        ClearForm();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn độc giả cần xóa trên bảng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDocGia.DataSource = bll.TimKiem(txtTimKiem.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaDocGia"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
            }
        }

        private void ClearForm()
        {
            txtMa.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtHoTen.Focus();
        }
    }
}