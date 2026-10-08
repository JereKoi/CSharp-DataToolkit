using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        string line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("products.csv");
            StreamWriter sw = new StreamWriter("prices.csv");
            line = sr.ReadLine();
            while (line != null)
            {
                {
                    string[] strPrices = line.Split(",");
                    int[] prices = new int[10];

                    if (int.TryParse(strPrices[2], out int price))
                    {
                        if (price < 50)
                        {
                            sw.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine("CSV row contained a non number character. Continuing parsing.");
                        continue;
                    }

  
                    Console.ReadLine();
                }
                line = sr.ReadLine();
            }
            sr.Close();
            sw.Close();
            Console.WriteLine("Parsing has finished!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block!");
        }
    }
}