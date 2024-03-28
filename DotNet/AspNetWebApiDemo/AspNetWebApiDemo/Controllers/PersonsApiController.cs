using AspNetWebApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace AspNetWebApiDemo.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsApiController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Person> ReadPersonsFromCsv()
        {
            string filePath = "Persons.csv";
            string csvData = System.IO.File.ReadAllText(filePath);
            List<Person> persons = ParseCsvData(csvData);

            return persons;
        }

        private List<Person> ParseCsvData(string csvData)
        {
            List<Person> people = new List<Person>();

            using (TextFieldParser parser = new TextFieldParser(new System.IO.StringReader(csvData)))
            {
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                // Skip the header line
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length == 3)
                    {
                        int number = int.Parse(fields[0]);
                        string name = fields[1];
                        DateTime birthdate = DateTime.ParseExact(fields[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        Person person = new Person { Number = number, Name = name, Birthdate = birthdate };
                        people.Add(person);
                    }
                }
            }

            return people;
        }
    }
}
