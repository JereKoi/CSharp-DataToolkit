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
            StreamWriter wr = new StreamWriter("prices.csv");
            line = sr.ReadLine();
            while (line != null)
            {
                {
                    string[] strPrices = line.Split(",");
                    int[] prices = new int[10];
                    int price = int.Parse(strPrices[2]);
                    
                    if (price < 50)
                    {
                            wr.WriteLine(line);
                    }
                    Console.ReadLine();
                }
                line = sr.ReadLine();
            }
            sr.Close();
            wr.Close();
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