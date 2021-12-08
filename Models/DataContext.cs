using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace test.Models
{
    public class DataContext
    {
        public string ConnectionString { get; set; } // Biến thành viên

        public DataContext(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public int ThemDCL(DiemCachLyModel dcl)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into DIEMCACHLY values(@madiemcachly, @tendiemcachly, @diachi)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madiemcachly", dcl.MADIEMCACHLY);
                cmd.Parameters.AddWithValue("tendiemcachly", dcl.TENDIEMCACHLY);
                cmd.Parameters.AddWithValue("diachi", dcl.DIACHI);
                return (cmd.ExecuteNonQuery());
            }
        }

        public List<object> LietKeCN(int SoTC)
        {
            List<object> LietKeCN = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select CN.TENCONGNHAN, CN.NAMSINH, CN.NUOCVE, COUNT (*) as SoTrieuChung
                            From CONGNHAN CN, CN_TC
                            Where CN.MaCongNhan = CN_TC.MaCongNhan
                            Group by CN.TenCongNhan, CN.NamSinh, CN.NuocVe
                            Having  COUNT(*) >= @SoTCinput; ";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("SoTCinput", SoTC);
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        LietKeCN.Add(new
                        {
                            tenCN = reader["TENCONGNHAN"].ToString(),
                            NamSinh = Convert.ToInt32(reader["NAMSINH"]),
                            NuocVe = reader["NuocVe"].ToString(),
                            SoTC = Convert.ToInt32(reader["SoTrieuChung"])
                        });
                    }
                }
                conn.Close();
            }
            return (LietKeCN);
        }
    }
}
