using System.Text.Json;
using WebApiClientDemo;

HttpClient client = new();

var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
string json = await response.Content.ReadAsStringAsync();

List<User> users = JsonSerializer.Deserialize<List<User>>(json) ?? new();
foreach (var user in users)
{
    Console.WriteLine($"{user.id}: {user.name}");
}

Console.WriteLine("Please enter the user id to display todo items:");
int userId = int.Parse(Console.ReadLine() ?? "0");

response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos");
json = await response.Content.ReadAsStringAsync();

List<ToDoItem> todos = JsonSerializer.Deserialize<List<ToDoItem>>(json) ?? new();
IEnumerable<ToDoItem> userTodos = todos.Where(
    t => (t.userId == userId) && (t.completed == false));

Console.WriteLine("Uncompleted todo items:");
foreach (var todo in userTodos)
{
    Console.WriteLine($"{todo.id}: {todo.title}");
}
