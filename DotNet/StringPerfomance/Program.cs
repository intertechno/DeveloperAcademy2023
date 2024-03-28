
using System.Text;

Console.WriteLine("Starting perfomance calculation...");
DateTime start = DateTime.Now;
StringBuilder s = new();
for (int i = 0; i < 600_000; i++)
{
    // s = s + "A";
    s.Append("A");
}
string result = s.ToString();
DateTime stop = DateTime.Now;
Console.WriteLine("Perfomance calculation complete.");

/*
Console.WriteLine("Starting perfomance calculation...");
DateTime start = DateTime.Now;
string s = "";
for (int i = 0; i < 600_000; i++)
{
    // s = s + "A";
    s += "A";
}
DateTime stop = DateTime.Now;
Console.WriteLine("Perfomance calculation complete.");
*/

TimeSpan diff = stop - start;
Console.WriteLine("Time taken: " + diff.TotalSeconds + " sec ("+
                  diff.TotalMilliseconds + " ms).");
