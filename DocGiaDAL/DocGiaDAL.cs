using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class DocGiaDAL
    {
        // Thay đổi chuỗi kết nối (Connection String) phù hợp với máy của bạn
        string connStr = @"Data Source=TY\SQLEXPRESS;Initial Catalog=QlyDocGia;Integrated Security=True;TrustServerCertificate=True";

        public List<DocGiaDTO> GetAll()
        {
            List<DocGiaDTO> list = new List<DocGiaDTO>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DocGia", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DocGiaDTO
                    {
                        MaDocGia = Convert.ToInt32(reader["MaDocGia"]),
                        HoTen = reader["HoTen"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    });
                }
            }
            return list;
        }

        public bool Insert(DocGiaDTO dg)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO DocGia (HoTen, SoDienThoai, Email) VALUES (@HoTen, @SDT, @Email)", conn);
                cmd.Parameters.AddWithValue("@HoTen", dg.HoTen);
                cmd.Parameters.AddWithValue("@SDT", dg.SoDienThoai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", dg.Email ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(DocGiaDTO dg)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE DocGia SET HoTen=@HoTen, SoDienThoai=@SDT, Email=@Email WHERE MaDocGia=@MaDocGia", conn);
                cmd.Parameters.AddWithValue("@MaDocGia", dg.MaDocGia);
                cmd.Parameters.AddWithValue("@HoTen", dg.HoTen);
                cmd.Parameters.AddWithValue("@SDT", dg.SoDienThoai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", dg.Email ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maDocGia)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DocGia WHERE MaDocGia=@MaDocGia", conn);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<DocGiaDTO> Search(string keyword)
        {
            List<DocGiaDTO> list = new List<DocGiaDTO>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DocGia WHERE HoTen LIKE @kw OR SoDienThoai LIKE @kw", conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DocGiaDTO
                    {
                        MaDocGia = Convert.ToInt32(reader["MaDocGia"]),
                        HoTen = reader["HoTen"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        NgayTao = Convert.ToDateTime(reader["NgayTao"])
                    });
                }
            }
            return list;
        }
    }
}