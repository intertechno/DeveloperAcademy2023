using Microsoft.Data.SqlClient;

Console.WriteLine("Starting to access SQL Server...");

string connStr = "Server=localhost\\SQLEXPRESS;Database=WebShop;Trusted_Connection=True;Encrypt=No;";
using SqlConnection conn = new(connStr);
conn.Open();
Console.WriteLine("Opened connection to SQL Server.");

string sql = "SELECT * FROM Customers";
using SqlCommand cmd = new(sql, conn);

using SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine("Customer found: " + reader["Name"]);
}

/*
reader.Close();
cmd.Dispose();
conn.Close();
*/
