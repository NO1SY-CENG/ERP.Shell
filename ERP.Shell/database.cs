using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows; // WPF MessageBox
using System.Configuration;

namespace ERP.Shell
{
    internal static class Database
    {
        // Tek bir connection string kullan
        private static readonly string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=C:\Users\Mertcan Boztoprak\Desktop\ERP.Shell\ERP.Shell\DataBase.mdf;
              Integrated Security=True;Connect Timeout=30";

        private static SqlConnection CreateConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Bağlantı Hatası:\n{ex.Message}",
                                "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                // Log metodu varsa ekle
                return null;
            }
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var conn = CreateConnection())
            {
                if (conn == null) return users;

                conn.Open();
                string query = "SELECT Id, Username, Password FROM [User]";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2)
                        });
                    }
                }
            }

            return users;
        }

        public static DataTable GetCustomers()
        {
            DataTable dt = new DataTable();

            using (var conn = CreateConnection())
            {
                if (conn == null) return dt;

                conn.Open();
                string query = "SELECT * FROM dbo.Customers";

                using (var cmd = new SqlCommand(query, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public static bool InsertUser(string username, string password)
        {
            string sql = "INSERT INTO [User] (Username, Password) VALUES (@username, @password)";

            using (var conn = CreateConnection())
            {
                if (conn == null) return false;

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = password;

                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected > 0;
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
