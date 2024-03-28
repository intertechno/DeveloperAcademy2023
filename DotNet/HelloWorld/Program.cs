Console.WriteLine("Hello, World!");

DateTime today = DateTime.Today;
DateTime christmas = new(2024, 12, 24);

TimeSpan diff = christmas - today;
Console.WriteLine("Days until Christmas: " + diff.TotalDays);
