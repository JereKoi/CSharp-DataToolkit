using System.IO;
using System.Linq;

string line;
try
{
    //Pass the file path and file name to the StreamReader constructor
    StreamReader sr = new StreamReader("");

    line = sr.ReadLine();

    while (line != null)
    {
        Console.WriteLine(line);

        line = sr.ReadLine();
    }

    sr.Close();
    Console.ReadLine();
}
catch(Exception e)
{
    Console.WriteLine("Exception " + e.Message);
}
finally
{
    Console.WriteLine("Executing finally block!");
}