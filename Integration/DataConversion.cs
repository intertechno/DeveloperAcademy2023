using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace XmlToJsonConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML-JSON conversion using Euroopan Central Bank's currency rates.");

            // reading the currency rates
            string filename = "..\\..\\..\\Currency rates.xml";
            XDocument xmlDoc = XDocument.Load(filename);
            XNamespace gesmes = "http://www.gesmes.org/xml/2002-08-01";
            XNamespace ns = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";
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

            Console.WriteLine($"USD currency rate is: {usdRate}");

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

            // convert XML to objects
            List<Employee> objectList = new();
            foreach (var employee in employees)
            {
                objectList.Add(new Employee()
                {
                    hireDate = Employee.HireDate,
                    personName = Employee.PersonName,
                    salary = new Palkkatiedot()
                    {
                        monthly = Employee.Salary * usdRate
                    }
                });
            }

            // JSON version and serialization
            Console.WriteLine("Conversion to JSON done:");
            string json = JsonSerializer.Serialize(objectList, new JsonSerializerOptions() { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}
