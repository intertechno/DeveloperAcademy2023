using System.Text.Json;
using System.Xml.Linq;

Console.WriteLine("Starting to read the USD/EUR exchange rate.");

string filename = "..\\..\\..\\Currency rates.xml";
XDocument xmlDoc = XDocument.Load(filename);
XNamespace gesmes = "http://www.gesmes.org/xml/2002-08-01";
XNamespace ns = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

// LINQ query to get the exchange rate
var cubes = xmlDoc.Descendants(ns + "Cube")
                  .Where(x => x.Attribute("currency") != null)
                  .Select(x => new
                  {
                      Currency = (string)x.Attribute("currency"),
                      Rate = (double)x.Attribute("rate")
                  });

double usdRate = 0.0;
foreach (var result in cubes)
{
    // Console.WriteLine($"{result.Currency}: {result.Rate}");
    if (result.Currency == "USD")
    {
        usdRate = result.Rate;
    }
}

Console.WriteLine("USD exchange rate: " + usdRate);

// read the salary data
filename = "..\\..\\..\\Salaries.xml";
xmlDoc = XDocument.Load(filename);
var employees = xmlDoc.Descendants("palkka")
                       .Select(x => new
                       {
                           PersonName = (string)x.Descendants("nimi").First(),
                           Salary = (double)x.Descendants("kuukausittain").First(),
                           HireDate = (string)x.Descendants("työsuhdealkoi").FirstOrDefault()
                       });

Console.WriteLine($"Read {employees.Count()} employees.");

List<EmployeeInfo> infos = new();
foreach (var employee in employees)
{
    EmployeeInfo info = new()
    {
        personName = employee.PersonName,
        salary = new() { monthly = (float)(employee.Salary * usdRate) },
        hireDate = employee.HireDate ?? ""
    };
    infos.Add(info);
}

// convert the data to JSON
JsonSerializerOptions options = new() { WriteIndented = true };
string json = JsonSerializer.Serialize(infos, options);
Console.WriteLine(json);
File.WriteAllText("salaries.json", json);
