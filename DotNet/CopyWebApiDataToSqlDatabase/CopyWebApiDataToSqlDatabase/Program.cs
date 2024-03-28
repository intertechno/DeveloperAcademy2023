using CopyWebApiDataToSqlDatabase;
using Microsoft.Data.SqlClient;
using System.Text.Json;

Console.WriteLine("Reading data from JSON web API...");
HttpClient client = new();
var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
string json = await response.Content.ReadAsStringAsync();
List<User> users = JsonSerializer.Deserialize<List<User>>(json) ?? new();

Console.WriteLine("Starting to access SQL Server...");

string connStr = "Server=localhost\\SQLEXPRESS;Database=WebShop;Trusted_Connection=True;Encrypt=No;";
using SqlConnection conn = new(connStr);
conn.Open();
Console.WriteLine("Opened connection to SQL Server.");

foreach (var user in users)
{
    InsertUser(conn, user);
}
Console.WriteLine($"Added {users.Count} users to the SQL database.");
conn.Close();

static void InsertUser(SqlConnection connection, User user)
{
    // SQL INSERT statement
    string insertSql = @"INSERT INTO Users (id, name, username, email, street, city,
                                            zipcode, phone, website, companyName)
                         VALUES (@id, @name, @username, @email, @street, @city, @zipcode,
                                 @phone, @website, @companyName)";

    using SqlCommand command = new SqlCommand(insertSql, connection);

    // Add parameters to the SqlCommand
    command.Parameters.AddWithValue("@id", user.id);
    command.Parameters.AddWithValue("@name", user.name);
    command.Parameters.AddWithValue("@username", user.username);
    command.Parameters.AddWithValue("@email", user.email);
    command.Parameters.AddWithValue("@street", user.address.street);
    command.Parameters.AddWithValue("@city", user.address.city);
    command.Parameters.AddWithValue("@zipcode", user.address.zipcode);
    command.Parameters.AddWithValue("@phone", user.phone);
    command.Parameters.AddWithValue("@website", user.website);
    command.Parameters.AddWithValue("@companyName", user.company.name);

    // Execute the SqlCommand
    command.ExecuteNonQuery();
}
