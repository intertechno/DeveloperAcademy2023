using System.Globalization;
using Microsoft.VisualBasic.FileIO;

string filePath = "Persons.csv";
string csvData = File.ReadAllText(filePath);
List<Person> people = ParseCsvData(csvData);

// Print the parsed data
foreach (var person in people)
{
    Console.WriteLine($"{person.Number}, {person.Name}, {person.Birthdate}");
}


List<Person> ParseCsvData(string csvData)
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