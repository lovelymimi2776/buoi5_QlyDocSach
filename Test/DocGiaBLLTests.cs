using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DTO;
using System.Collections.Generic;

namespace DocGiaTests
{
    [TestClass]
    public class DocGiaBLLTests
    {
        private DocGiaBLL bll;

        [TestInitialize]
        public void SetUp()
        {
            bll = new DocGiaBLL();
        }

        [TestMethod]
        public void Test_GetAll_ReturnsValidList()
        {
            List<DocGiaDTO> danhSach = bll.GetAll();
            Assert.IsNotNull(danhSach, "Danh sách độc giả trả về bị null!");
        }

        [TestMethod]
        public void Test_ThemDocGia_Success()
        {
            DocGiaDTO dgMoi = new DocGiaDTO
            {
                HoTen = "Tran Thi Test",
                SoDienThoai = "0988888888",
                Email = "trantest@gmail.com"
            };

            bool ketQua = bll.ThemDocGia(dgMoi);
            Assert.IsTrue(ketQua, "Thêm độc giả thất bại!");
        }

        [TestMethod]
        public void Test_SuaDocGia_Success()
        {
            List<DocGiaDTO> danhSach = bll.GetAll();
            if (danhSach.Count > 0)
            {
                DocGiaDTO dgCanSua = danhSach[0];
                dgCanSua.HoTen = "Nguyen Van Updated";

                bool ketQua = bll.SuaDocGia(dgCanSua);
                Assert.IsTrue(ketQua, "Cập nhật thông tin độc giả thất bại!");
            }
            else
            {
                Assert.Inconclusive("Không có dữ liệu trong bảng để test chức năng Sửa.");
            }
        }

        [TestMethod]
        public void Test_TimKiem_ValidKeyword()
        {
            string keyword = "Test";
            List<DocGiaDTO> ketQua = bll.TimKiem(keyword);
            Assert.IsNotNull(ketQua, "Phương thức tìm kiếm trả về kết quả null!");
        }

        [TestMethod]
        public void Test_XoaDocGia_Success()
        {
            DocGiaDTO dgTam = new DocGiaDTO
            {
                HoTen = "User Tam De Xoa",
                SoDienThoai = "0911111111",
                Email = "xoa@gmail.com"
            };
            bll.ThemDocGia(dgTam);

            List<DocGiaDTO> danhSach = bll.GetAll();
            DocGiaDTO vuaThem = danhSach[danhSach.Count - 1];

            bool ketQua = bll.XoaDocGia(vuaThem.MaDocGia);
            Assert.IsTrue(ketQua, "Xóa độc giả thất bại!");
        }
    }
}