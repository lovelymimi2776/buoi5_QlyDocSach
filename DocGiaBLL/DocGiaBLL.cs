using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;

namespace BLL
{
    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }

    public class Logger
    {
        private static string logPath = "log.txt";

        public static void Log(string message, LogLevel level = LogLevel.INFO)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry);
            }
            catch { }
        }
    }

    public class DocGiaBLL
    {
        private DocGiaDAL dal = new DocGiaDAL();

        public List<DocGiaDTO> GetAll() => dal.GetAll();

        public bool ThemDocGia(DocGiaDTO dg)
        {
            if (string.IsNullOrWhiteSpace(dg.HoTen))
            {
                Logger.Log("Thêm độc giả thất bại: Họ tên bị để trống.", LogLevel.WARNING);
                throw new Exception("Họ tên độc giả không được để trống!");
            }

            bool result = dal.Insert(dg);
            if (result)
            {
                Logger.Log($"Thêm thành công độc giả: {dg.HoTen}", LogLevel.INFO);
            }
            return result;
        }

        public bool SuaDocGia(DocGiaDTO dg)
        {
            if (dg.MaDocGia <= 0 || string.IsNullOrWhiteSpace(dg.HoTen))
            {
                Logger.Log("Cập nhật độc giả thất bại: Dữ liệu không hợp lệ.", LogLevel.WARNING);
                throw new Exception("Dữ liệu độc giả không hợp lệ!");
            }

            bool result = dal.Update(dg);
            if (result)
            {
                Logger.Log($"Cập nhật thành công thông tin độc giả ID: {dg.MaDocGia}", LogLevel.INFO);
            }
            return result;
        }

        public bool XoaDocGia(int maDocGia)
        {
            bool result = dal.Delete(maDocGia);
            if (result)
            {
                Logger.Log($"Xóa thành công độc giả ID: {maDocGia}", LogLevel.INFO);
            }
            else
            {
                Logger.Log($"Xóa thất bại độc giả ID: {maDocGia} (Không tìm thấy)", LogLevel.ERROR);
            }
            return result;
        }

        public List<DocGiaDTO> TimKiem(string keyword) => dal.Search(keyword);
    }
}